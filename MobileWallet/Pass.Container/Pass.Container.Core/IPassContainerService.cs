using System.Collections.Generic;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassContainerService 
	{
		void UpdatePassFields(IList<int> passIds, IList<PassFieldValue> passFieldValues);
		void CreatePassTemplate(object templatePackage);
		void UpdatePassView(int passTemplateId, int templatePackageId);
        void CreatePass(int passTemplateId, IList<PassFieldValue> passFieldValues);
		void GetClientPassPackage(int passId, DeviceType deviceType);
        void GetPassPackage(int passTemplateId, DeviceType deviceType);
	}
}

