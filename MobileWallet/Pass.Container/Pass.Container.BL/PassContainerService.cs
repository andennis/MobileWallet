using System.Collections.Generic;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class PassContainerService : IPassContainerService
	{
        public void UpdatePassFields(IList<int> passIds, IList<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new System.NotImplementedException();
        }

        public void CreatePassTemplate(object templatePackage)
        {
            throw new System.NotImplementedException();
        }

        public void UpdatePassView(int passTemplateId, int templatePackageId)
        {
            throw new System.NotImplementedException();
        }

        public void CreatePass(int passTemplateId, IList<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new System.NotImplementedException();
        }

        public void GetClientPassPackage(int passId, Core.Entities.Enums.DeviceType deviceType)
        {
            throw new System.NotImplementedException();
        }

        public void GetPassPackage(int passTemplateId, Core.Entities.Enums.DeviceType deviceType)
        {
            throw new System.NotImplementedException();
        }
    }
}

