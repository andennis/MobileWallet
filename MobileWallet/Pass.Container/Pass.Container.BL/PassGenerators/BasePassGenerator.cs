using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repository;
using FileStorage.Core;
using Pass.Container.Core.Exceptions;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.PassGenerators
{
    public class BasePassGenerator
    {
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        //Repositories
        protected readonly IRepository<Repository.Core.Entities.Pass> _repPass;
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassField> _repPassField;
        private readonly IRepository<PassFieldValue> _repPassFieldValue;

        public BasePassGenerator(IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService)
        {
           _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            //Repositories
            _repPass = _pcUnitOfWork.GetRepository<Repository.Core.Entities.Pass>();
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
            _repPassFieldValue = _pcUnitOfWork.GetRepository<PassFieldValue>();
        }

        protected RepEntities.Pass GetPass(int passId)
        {
            RepEntities.Pass pass = _repPass.Query().Filter(x => x.PassId == passId).Get().FirstOrDefault();
            if (pass == null)
                throw new PassGenerationException(String.Format("Pass was not found. Pass Id: {0}", passId));

            return pass;
        }

        protected string GetStorageItemPath(int passTemplateId, out DateTime lastUpdateDateTime)
        {
            lastUpdateDateTime = DateTime.MinValue;
            string storageItemPath = null;
            PassTemplate passTemplate = _repPassTemplate.Query().Filter(x => x.PassTemplateId == passTemplateId).Get().FirstOrDefault();
            if (passTemplate != null)
            {
                lastUpdateDateTime = passTemplate.UpdatedDate;
                storageItemPath = _fsService.GetStorageItemPath(passTemplate.PackageId);
            }
            return storageItemPath;
        }

        protected Dictionary<PassField, PassFieldValue> GetDynamicFields(Repository.Core.Entities.Pass pass)
        {
            Dictionary<PassField, PassFieldValue> dynamicFields = null;
            PassTemplate passTemplate = _repPassTemplate.Query().Filter(x => x.PassTemplateId == pass.PassTemplateId).Get().FirstOrDefault();
            if (passTemplate != null)
            {
                IQueryable<PassField> passFields = _repPassField.Query().Filter(x => x.PassTemplateId == pass.PassTemplateId).Include(x => x.FieldValues).Get();
                if (passFields != null)
                {
                    dynamicFields = new Dictionary<PassField, PassFieldValue>();
                    foreach (PassField passField in passFields)
                    {
                        PassFieldValue passFieldValue = passField.FieldValues.FirstOrDefault(x => x.PassId == pass.PassId);
                        dynamicFields.Add(passField, passFieldValue);
                    }
                }
            }
            return dynamicFields;
        }
    }
}
