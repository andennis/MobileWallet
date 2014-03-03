using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FileStorage.Core;
using Pass.Container.BL.Helpers;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.BL.NativePassGenerators
{
    public class ApplePassGenerator : BasePassGenerator
    {
        private ApplePassGeneratorHelper _generatorHelper;

        public ApplePassGenerator(IPassContainerUnitOfWork pcUnitOfWork, IFileStorageService fsService, Core.Entities.Pass pass)
            :base(pcUnitOfWork, fsService, pass)
        {
            _generatorHelper = new ApplePassGeneratorHelper();
        }

        public PassTemplateType PassTemplateType
        {
            get { return PassTemplateType.AppleTemplate; }
        }

        public string GeneratePass()
        {
            string storageItemPath = GetStorageItemPath();
            Dictionary<PassField, PassFieldValue> dynamicFields = GetDynamicFields();

            if (storageItemPath == null)
                return null;
            return null;
        }
    }
}
