using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using FileStorage.Core;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.NativePassGenerators
{
    public class BasePassGenerator
    {
        public readonly RepEntities.Pass _pass;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        //Repositories
        private readonly IRepository<RepEntities.Pass> _repPass;
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassField> _repPassField;
        private readonly IRepository<PassFieldValue> _repPassFieldValue;

        public BasePassGenerator(IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService, int passId)
        {
           _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            //Repositories
            _repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
            _repPassFieldValue = _pcUnitOfWork.GetRepository<PassFieldValue>();
            
            _pass = _repPass.Query().Filter(x => x.PassId == passId).Get().FirstOrDefault();
            if (_pass == null)
                throw new PassGenerationException(String.Format("Pass was not found. Pass Id: {0}", passId));
        }

        public string GetStorageItemPath(ref DateTime lastUpdateDateTime)
        {
            string storageItemPath = null;
            PassTemplate passTemplate = _repPassTemplate.Query().Filter(x => x.PassTemplateId == _pass.PassTemplateId).Get().FirstOrDefault();
            if (passTemplate != null)
            {
                lastUpdateDateTime = passTemplate.UpdatedDate;
                storageItemPath = _fsService.GetStorageItemPath(passTemplate.PackageId);
            }
            return storageItemPath;
        }

        public Dictionary<PassField, PassFieldValue> GetDynamicFields()
        {
            Dictionary<PassField, PassFieldValue> dynamicFields = null;
            PassTemplate passTemplate = _repPassTemplate.Query().Filter(x => x.PassTemplateId == _pass.PassTemplateId).Get().FirstOrDefault();
            if (passTemplate != null)
            {
                IQueryable<PassField> passFields = _repPassField.Query().Filter(x => x.PassTemplateId == _pass.PassTemplateId).Include(x => x.FieldValues).Get();
                if (passFields != null)
                {
                    dynamicFields = new Dictionary<PassField, PassFieldValue>();
                    foreach (PassField passField in passFields)
                    {
                        PassFieldValue passFieldValue = passField.FieldValues.FirstOrDefault(x => x.PassId == _pass.PassId);
                        dynamicFields.Add(passField, passFieldValue);
                    }
                }
            }
            return dynamicFields;
        }
    }
}
