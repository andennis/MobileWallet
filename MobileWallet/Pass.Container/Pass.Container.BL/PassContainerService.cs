using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Common.Repository;
using Pass.Container.BL.PassGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public class PassContainerService : IPassContainerService
    {
        private readonly IPassContainerConfig _config;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IPassCertificateService _certService;
        private readonly IPassTemplateStorageService _templateStorageService;

        public PassContainerService(IPassContainerConfig config, 
            IPassContainerUnitOfWork pcUnitOfWork, 
            IPassCertificateService certService,
            IPassTemplateStorageService templateStorageService)
        {
            _config = config;
            _pcUnitOfWork = pcUnitOfWork;
            _certService = certService;
            _templateStorageService = templateStorageService;
        }

        public int CreatePass(int passTemplateId, IList<PassFieldInfo> fieldValues, DateTime? expDate = null)
        {
            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IRepository<PassField> repPassField = _pcUnitOfWork.GetRepository<PassField>();
            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();

            //Create Pass
            var pass = new RepEntities.Pass()
                           {
                               PassTemplateId = passTemplateId,
                               SerialNumber = GenerateSerialNumber(),
                               //PassTypeIdentifier = string.Empty,
                               AuthToken = GenerateAuthToken(),
                               Status = EntityStatus.Active,
                               ExpirationDate = expDate
                           };
            repPass.Insert(pass);

            //Create values for pass fields
            IList<PassField> passFields = repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get().ToList();
            foreach (PassField passField in passFields)
            {
                PassFieldInfo fieldInfo = (fieldValues != null)
                    ? fieldValues.FirstOrDefault(x => x.Name == passField.Name)
                    : null;

                var pfv = new PassFieldValue()
                              {
                                  PassId = pass.PassId,
                                  PassFieldId = passField.PassFieldId,
                              };

                if (fieldInfo == null)
                {
                    pfv.Label = passField.DefaultLabel;
                    pfv.Value = passField.DefaultValue;
                }
                else
                {
                    pfv.Label = fieldInfo.Label ?? passField.DefaultLabel;
                    pfv.Value = fieldInfo.Value ?? passField.DefaultValue;
                }

                repPassFieldVal.Insert(pfv);
            }

            _pcUnitOfWork.Save();
            return pass.PassId;
        }

        public IList<PassFieldInfo> GetPassFields(int passId)
        {
            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IList<PassFieldInfo> fields = repPassFieldVal.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.PassField)
                .Get()
                .AsEnumerable()
                .Select(x => EntityConverter.RepositoryFieldValueToPassFieldInfo(x, false))
                .ToList();

            if (!fields.Any())
            {
                IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
                if (repPass.Find(passId) == null)
                    throw new PassContainerException(string.Format("Pass ID:{0} not found", passId));
            }

            return fields;
        }

        public void UpdatePassFields(int passId, IList<PassFieldInfo> newFieldValues)
        {
            if (newFieldValues == null)
                throw new ArgumentNullException("newFieldValues");

            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IList<PassFieldValue> oldFieldValues = repPassFieldVal.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.PassField)
                .Get()
                .ToList();

            if (!oldFieldValues.Any())
            {
                IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
                if (repPass.Find(passId) == null)
                    throw new PassContainerException(string.Format("Pass ID:{0} not found", passId));
            }

            foreach (PassFieldInfo newFieldValue in newFieldValues)
            {
                PassFieldValue pfv = oldFieldValues.FirstOrDefault(x => x.PassField.Name == newFieldValue.Name);
                if (pfv == null)
                    throw new PassContainerException(string.Format("Field '{0}' does not exist in pass ID: {1}", newFieldValue.Name, passId));

                pfv.Label = newFieldValue.Label;
                pfv.Value = newFieldValue.Value;

                repPassFieldVal.Update(pfv);
            }

            _pcUnitOfWork.Save();
        }

        public string GetPassPackage(int passId, ClientType clientType)
        {
            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
            RepEntities.Pass pass = repPass.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.Template.NativeTemplates)
                .Include(x => x.Template.PassFields)
                .Include(x => x.FieldValues)
                .Get()
                .FirstOrDefault();
            if (pass == null)
                throw new PassContainerException(string.Format("Pass ID:{0} not found", passId));

            string templateFolder = GetTemporaryTemplateFolder(pass.Template.PassTemplateId, clientType);
            if (!Directory.Exists(templateFolder))
            {
                Directory.CreateDirectory(templateFolder);
                _templateStorageService.GetNativeTemplateFiles(pass.Template.PackageId, clientType, templateFolder);
            }

            string passFolder = GetTemporaryPassFolder(pass.Template.PassTemplateId, passId, clientType);
            if (Directory.Exists(passFolder))
                Directory.Delete(passFolder, true);
            Directory.CreateDirectory(passFolder);

            IPassGenerator pg = GetPassGenerator(pass.Template, clientType, templateFolder);
            IEnumerable<PassFieldInfo> fields = pass.FieldValues.Select(x => EntityConverter.RepositoryFieldValueToPassFieldInfo(x, false));
            return pg.GeneratePass(pass.SerialNumber, fields, passFolder);
        }
        
        private string GetTemporaryTemplateFolder(int templateId, ClientType clientType)
        {
            return Path.Combine(_config.PassWorkingFolder, "T" + templateId, clientType.ToString());
        }
        private string GetTemporaryPassFolder(int templateId, int passId, ClientType clientType)
        {
            return Path.Combine(_config.PassWorkingFolder, "T" + templateId, clientType.ToString(), "Passes", passId.ToString());
        }

        private IPassGenerator GetPassGenerator(PassTemplate passTemplate, ClientType clientType, string srcTemplateFolder)
        {
            switch (clientType)
            {
                case ClientType.Apple:
                    var appleTemplate = passTemplate.NativeTemplates.OfType<PassTemplateApple>().FirstOrDefault();
                    if (appleTemplate == null)
                        throw new PassContainerException("Apple pass template not found");

                    return GetApplePassGenerator(appleTemplate, srcTemplateFolder);
                default:
                    throw new PassContainerException("Pass generated is not specified for client type: " + clientType);
            }

            
        }
        private IPassGenerator GetApplePassGenerator(PassTemplateApple appleTemplate, string srcTemplateFolder)
        {
            X509Certificate2 cert = _certService.GetCertificate(appleTemplate.CertificateId);
            return new ApplePassGenerator(_config, srcTemplateFolder, cert);
        }

        private string GenerateSerialNumber()
        {
            return Guid.NewGuid().ToString();
        }
        private string GenerateAuthToken()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}
