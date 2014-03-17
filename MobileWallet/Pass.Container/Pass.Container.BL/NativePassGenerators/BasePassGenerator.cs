﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using FileStorage.Core;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
using RepEntities = Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL.NativePassGenerators
{
    public class BasePassGenerator
    {
        private readonly RepEntities.Pass _pass;
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;
        private readonly IFileStorageService _fsService;
        //Repositories
        private readonly IRepository<PassTemplate> _repPassTemplate;
        private readonly IRepository<PassField> _repPassField;
        private readonly IRepository<PassFieldValue> _repPassFieldValue;

        public BasePassGenerator(IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService, RepEntities.Pass pass)
        {
            _pass = pass;
            _pcUnitOfWork = pcUnitOfWork;
            _fsService = fsService;

            //Repositories
            _repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            _repPassField = _pcUnitOfWork.GetRepository<PassField>();
            _repPassFieldValue = _pcUnitOfWork.GetRepository<PassFieldValue>();
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
                IQueryable<PassField> passFields = _repPassField.Query().Filter(x => x.PassTemplateId == _pass.PassTemplateId).Get();
                if (passFields != null)
                {
                    dynamicFields = new Dictionary<PassField, PassFieldValue>();
                    IQueryable<PassFieldValue> passFieldValues = _repPassFieldValue.Query().Filter(x => x.PassId == _pass.PassId).Get();
                    foreach (PassField passField in passFields)
                    {
                        PassFieldValue passFieldValue = passFieldValues.FirstOrDefault(x => x.PassFieldId == passField.PassFieldId);
                        dynamicFields.Add(passField, passFieldValue);
                    }
                }
            }
            return dynamicFields;
        }
    }
}