using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using Common.BL;
using Common.Extensions;
using Common.Repository;
using Common.Utils;
using FileStorage.Core;
using FileStorage.Core.Entities;
using Pass.Container.BL.PassGenerators;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.Exceptions;
using Pass.Container.Core.SearchFilters;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public class PassService : IPassService
    {
        private readonly IPassContainerConfig _config;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IPassCertificateService _certService;
        private readonly IPassTemplateStorageService _templateStorageService;
        private readonly IFileStorageService _fileStorageService;

        public PassService(IPassContainerConfig config, 
            IPassContainerUnitOfWork pcUnitOfWork, 
            IPassCertificateService certService,
            IPassTemplateStorageService templateStorageService,
            IFileStorageService fileStorageService)
        {
            _config = config;
            _pcUnitOfWork = pcUnitOfWork;
            _certService = certService;
            _templateStorageService = templateStorageService;
            _fileStorageService = fileStorageService;
        }

        public int CreatePass(int passTemplateId, IList<PassFieldInfo> fieldValues, DateTime? expDate = null)
        {
            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IRepository<PassField> repPassField = _pcUnitOfWork.GetRepository<PassField>();
            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
            IRepository<PassApple> repPassApple = _pcUnitOfWork.GetRepository<PassApple>();
            IRepository<PassTemplate> repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();

            PassTemplate passTemplate = repPassTemplate.Query()
                .Include(x => x.NativeTemplates)
                .Filter(x => x.PassTemplateId == passTemplateId)
                .Get()
                .FirstOrDefault();

            if (passTemplate == null)
                throw new PassContainerException(string.Format("Pass template ID: {0} not found", passTemplateId));

            //Create Pass
            var pass = new RepEntities.Pass()
                           {
                               PassTemplateId = passTemplateId,
                               SerialNumber = GenerateSerialNumber(),
                               //PassTypeId = ptApple.PassTypeId,
                               AuthToken = GenerateAuthToken(),
                               Status = EntityStatus.Active,
                               ExpirationDate = expDate
                           };
            repPass.Insert(pass);

            //Create native passes
            foreach (PassTemplateNative ptn in passTemplate.NativeTemplates)
            {
                PassNative pn = CreateNativePasseByNativeTemplate(ptn);
                pn.PassId = pass.PassId;

                var pnApple = pn as PassApple;
                if (pnApple != null)
                    repPassApple.Insert(pnApple);

                //TODO add native passes for other client types
            }

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

        private PassNative CreateNativePasseByNativeTemplate(PassTemplateNative ptn)
        {
            var ptApple = ptn as PassTemplateApple;
            if (ptApple != null)
            {
                return new PassApple()
                       {
                           PassTypeId = ptApple.PassTypeId,
                           DeviceType = ptApple.DeviceType,
                           PassTemplateNativeId = ptApple.PassTemplateNativeId
                       };
            }

            //TODO create template for other client types

            throw new PassContainerException(string.Format("{0} is not supported", ptn.GetType().Name));
        }

        public IList<PassFieldInfo> GetPassFields(int passId)
        {
            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IList<PassFieldInfo> fields = repPassFieldVal.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.PassField)
                .Get()
                .AsEnumerable()
                .Select(x => EntityConverter.RepositoryFieldValueToPassFieldInfo(x, true))
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
            if (!newFieldValues.Any())
                return;

            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
            RepEntities.Pass pass = repPass.Find(passId);
            if (pass == null)
                throw new PassContainerException(string.Format("Pass ID:{0} not found", passId));

            pass.UpdatedDate = DateTime.Now;
            repPass.Update(pass);

            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IList<PassFieldValue> oldFieldValues = repPassFieldVal.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.PassField)
                .Get()
                .ToList();

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
            //TODO it should be replaced by SP call (GetPassDetails)
            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
            RepEntities.Pass pass = repPass.Query()
                .Filter(x => x.PassId == passId)
                .Include(x => x.Template.NativeTemplates)
                .Include(x => x.Template.PassFields)//TODO it may be removed
                .Include(x => x.FieldValues)
                .Include(x => x.NativePasses)
                .Get()
                .FirstOrDefault();

            if (pass == null)
                throw new PassContainerException(string.Format("Pass ID:{0} not found", passId));

            PassNative pn = pass.NativePasses.FirstOrDefault(x => x.DeviceType == clientType);
            if (pn == null)
                throw new PassContainerException(string.Format("Native pass: '{0}' of Pass ID:{1} not found", clientType, passId));

            string dstPassFolder = GetTemporaryPassFolder(pass.Template, clientType);
            Directory.CreateDirectory(dstPassFolder);

            //If the pass or its template was not updated then try to get pass file from file storage or temporary folder (cache)
            if (pn.PassFileStorageId.HasValue && pass.UpdatedDate <= pn.UpdatedDate && pass.UpdatedDate > pass.Template.UpdatedDate)
            {
                //Try to get pass file from temporary folder (cache)
                string filePath = Path.Combine(dstPassFolder, GetTemporaryPassFileName(pn));
                if (File.Exists(filePath))
                    return filePath;

                //Try to get pass file from file storage
                using (StorageFileInfo fileInfo = _fileStorageService.GetFile(pn.PassFileStorageId.Value, true))
                {
                    fileInfo.FileStream.SaveToFile(filePath);
                    return filePath;
                }
            }

            //Copy template files for specified client type
            string templateFolder = GetTemporaryTemplateFolder(pass.Template, clientType);
            var di = new DirectoryInfo(templateFolder);
            bool copyTemplate = !di.Exists;
            if (di.Exists && di.CreationTime <= pass.Template.UpdatedDate)
            {
                Directory.Delete(templateFolder, true);
                copyTemplate = true;
            }
            if (copyTemplate)
            {
                Directory.CreateDirectory(templateFolder);
                _templateStorageService.GetNativeTemplateFiles(pass.Template.PackageId, clientType, templateFolder);
            }

            //Generate pass file
            IPassGenerator pg = GetPassGenerator(pass.Template, clientType, templateFolder);
            IEnumerable<PassFieldInfo> fields = pass.FieldValues.Select(x => EntityConverter.RepositoryFieldValueToPassFieldInfo(x, true));
            string passFile = pg.GeneratePass(pass.AuthToken, pass.SerialNumber, fields, dstPassFolder);

            SavePassToFileStorage(pn, passFile);

            return FileHelper.Rename(passFile, GetTemporaryPassFileName(pn));
        }

        public SearchResult<RegistrationInfo> GetPassRegistrations(SearchContext searchContext, PassRegistrationFilter filter)
        {
            IEnumerable<QueryParameter> searchParams = filter.ObjectPropertiesToDictionary().Select(x => new QueryParameter(){Name = x.Key, Value = x.Value});
            searchParams = searchParams.Union(searchContext.ObjectPropertiesToDictionary().Select(x => new QueryParameter(){Name = x.Key, Value = x.Value}));

            int totalRecords;
            IRepository<Registration> repReg = _pcUnitOfWork.GetRepository<Registration>();
            IEnumerable<RegistrationInfo> regs = repReg.Search<RegistrationInfo>(searchParams, out totalRecords);

            return new SearchResult<RegistrationInfo>()
                    {
                        Data = regs,
                        TotalCount = totalRecords
                    };

        }

        public RegistrationInfo GetPassRegistration(int passId, int clientDeviceId)
        {
            IRepository<Registration> repReg = _pcUnitOfWork.GetRepository<Registration>();
            Registration reg = repReg.Query()
                .Filter(x => x.PassId == passId && x.ClientDeviceId == clientDeviceId)
                .Include(x => x.ClientDevice)
                .Get()
                .FirstOrDefault();

            if (reg == null)
                throw new PassContainerException(string.Format("Registration does not exist (PassId: {0}, ClientDeviceId: {1})", passId, clientDeviceId));

            return ConvertTo(reg);
        }

        private RegistrationInfo ConvertTo(Registration reg)
        {
            return new RegistrationInfo()
                   {
                       ClientDeviceId = reg.ClientDeviceId,
                       PassId = reg.PassId,
                       DeviceId = reg.ClientDevice.DeviceId,
                       DeviceType = reg.ClientDevice.DeviceType,
                       PushToken = reg.ClientDevice is ClientDeviceApple ? ((ClientDeviceApple)reg.ClientDevice).PushToken : null,
                       Status = reg.Status,
                       CreatedDate = reg.CreatedDate,
                       UpdatedDate = reg.UpdatedDate
                   };
        }

        private void SavePassToFileStorage(PassNative pn, string filePath)
        {
            //TODO Old file should be removed or versioned
            if (pn.PassFileStorageId.HasValue)
                _fileStorageService.DeleteStorageItem(pn.PassFileStorageId.Value);

            pn.PassFileStorageId = _fileStorageService.Put(filePath);

            _pcUnitOfWork.GetRepository<PassNative>().Update(pn);
            _pcUnitOfWork.Save();
        }

        private string GetTemporaryTemplateFolder(PassTemplate pt, ClientType clientType)
        {
            return Path.Combine(_config.PassWorkingFolder, string.Format("T{0}-{1}", pt.PassTemplateId, pt.PackageId), clientType.ToString(), "Template");
        }
        private string GetTemporaryPassFolder(PassTemplate pt, ClientType clientType)
        {
            return Path.Combine(_config.PassWorkingFolder, string.Format("T{0}-{1}", pt.PassTemplateId, pt.PackageId), clientType.ToString(), "Passes");
        }
        private string GetTemporaryPassFileName(PassNative pn)
        {
            if (pn.DeviceType == ClientType.Apple)
                return string.Format("p{0}-{1}.pkpass", pn.PassId, pn.PassFileStorageId);

            throw new PassContainerException(string.Format("Native pass '{0}' is not supported", pn.DeviceType));
        }

        private IPassGenerator GetPassGenerator(PassTemplate passTemplate, ClientType clientType, string srcTemplateFolder)
        {
            switch (clientType)
            {
                case ClientType.Apple:
                    var appleTemplate = passTemplate.NativeTemplates.OfType<PassTemplateApple>().FirstOrDefault();
                    if (appleTemplate == null)
                        throw new PassContainerException("Apple pass template not found");

                    return CreateApplePassGenerator(appleTemplate, srcTemplateFolder);
                default:
                    throw new PassContainerException("Pass generated is not specified for client type: " + clientType);
            }

            
        }
        private ApplePassGenerator CreateApplePassGenerator(PassTemplateApple appleTemplate, string srcTemplateFolder)
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

