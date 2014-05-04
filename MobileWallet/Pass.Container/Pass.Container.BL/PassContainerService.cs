using System;
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
                               PassTypeIdentifier = string.Empty,
                               AuthToken = GenerateAuthToken(),
                               Status = EntityStatus.Active,
                               ExpirationDate = expDate
                           };
            repPass.Insert(pass);

            //Create values for pass fields
            IList<PassField> passFields = repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get().ToList();
            foreach (PassField passField in passFields)
            {
                PassFieldInfo fieldInfo = fieldValues.FirstOrDefault(x => x.PassFieldId == passField.PassFieldId)
                    ?? fieldValues.FirstOrDefault(x => x.Name == passField.Name);

                PassFieldValue pfv;
                if (fieldInfo == null)
                {
                    pfv = new PassFieldValue()
                              {
                                  PassId = pass.PassId,
                                  PassFieldId = passField.PassFieldId,
                                  Value = passField.DefaultValue,
                                  Label = passField.DefaultLabel
                              };
                }
                else
                {
                    pfv = EntityConverter.PassFieldInfoToRepositoryFieldValue(fieldInfo);
                    pfv.PassId = pass.PassId;
                    pfv.PassFieldId = passField.PassFieldId;
                }

                repPassFieldVal.Insert(pfv);
            }

            _pcUnitOfWork.Save();
            return pass.PassId;
        }

        public IList<PassFieldInfo> GetPassFields(int passId)
        {
            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            return repPassFieldVal.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.PassField)
                .Get()
                .Select(x => new PassFieldInfo()
                {
                    PassFieldId = x.PassFieldId,
                    Name = x.PassField.Name,
                    Label = string.IsNullOrEmpty(x.Label) ? x.PassField.DefaultLabel : x.Label,
                    Value = string.IsNullOrEmpty(x.Value) ? x.PassField.DefaultValue : x.Value,
                })
                .ToList();
        }

        public void UpdatePassFields(int passId, IList<PassFieldInfo> passFieldValues)
        {
            throw new NotImplementedException();
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

            IPassGenerator2 pg = GetPassGenerator(pass.Template, clientType, templateFolder);
            IEnumerable<PassFieldInfo> fields = GetPassFields(pass);
            return pg.GeneratePass(pass.SerialNumber, fields, passFolder);
        }
        
        private IEnumerable<PassFieldInfo> GetPassFields(RepEntities.Pass pass)
        {
            return pass.FieldValues
                .Select(x => new PassFieldInfo()
                             {
                                 PassFieldId = x.PassFieldId,
                                 Name = x.PassField.Name,
                                 Label = string.IsNullOrEmpty(x.Label) ? x.PassField.DefaultLabel : x.Label,
                                 Value = string.IsNullOrEmpty(x.Value) ? x.PassField.DefaultValue : x.Value,
                             });
        }

        private string GetTemporaryTemplateFolder(int templateId, ClientType clientType)
        {
            return Path.Combine(_config.PassWorkingFolder, "T" + templateId, clientType.ToString());
        }
        private string GetTemporaryPassFolder(int templateId, int passId, ClientType clientType)
        {
            return Path.Combine(_config.PassWorkingFolder, "T" + templateId, clientType.ToString(), "Passes", passId.ToString());
        }

        private IPassGenerator2 GetPassGenerator(PassTemplate passTemplate, ClientType clientType, string srcTemplateFolder)
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

        private IPassGenerator2 GetApplePassGenerator(PassTemplateApple appleTemplate, string srcTemplateFolder)
        {
            X509Certificate2 cert = _certService.GetCertificate(appleTemplate.CertificateId);
            return new ApplePassGenerator2(_config, srcTemplateFolder, cert);
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
