using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repository;
using FileStorage.Core;
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
        private readonly IFileStorageService _fileStorageService;
        private readonly IPassCertificateService _certService;

        private readonly IDictionary<ClientType, IPassGenerator> _passGenerators; 

        public PassContainerService(IPassContainerConfig config, 
            IPassContainerUnitOfWork pcUnitOfWork, 
            IFileStorageService fileStorageService,
            IPassCertificateService certService)
        {
            _config = config;
            _pcUnitOfWork = pcUnitOfWork;
            _fileStorageService = fileStorageService;
            _certService = certService;

            _passGenerators = new Dictionary<ClientType, IPassGenerator>()
                              {
                                  {ClientType.Apple, new ApplePassGenerator(_config, _pcUnitOfWork, _fileStorageService)}
                              };
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

            /*
            foreach (PassFieldInfo fieldValue in fieldValues)
            {
                PassFieldValue pfv = EntityConverter.PassFieldInfoToRepositoryFieldValue(fieldValue);
                pfv.PassId = pass.PassId;
                repPassFieldVal.Insert(pfv);
            }
            */

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
            IPassGenerator pg = GetPassGenerator(clientType);
            return pg.GeneratePass(passId);
        }

        private IPassGenerator GetPassGenerator(ClientType clientType)
        {
            IPassGenerator pg;
            if (!_passGenerators.TryGetValue(clientType, out pg))
                throw new PassGenerationException(string.Format("ClientType: {0} is not supported", clientType));

            return pg;
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
