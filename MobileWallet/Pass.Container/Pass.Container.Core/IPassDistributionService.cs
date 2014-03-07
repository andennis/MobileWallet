using System;
using System.Collections.Generic;
using System.IO;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassDistributionService : IDisposable
	{
        int CreatePassByTemplate(int passTemplateId, IList<PassFieldValue> passFieldValues);
		Stream GetPassPackage(int passId, DeviceType deviceType);
        Stream GetPassPackageByTemplate(int passTemplateId, DeviceType deviceType);
        void UpdatePassFields(int passId, IList<PassFieldValue> passFieldValues);

        string GetPassToken(int passId);
        string GetPassTemplateToken(int passTempleteId);
        PassTokenInfo GetPassTokenInfo(string passToken);
	}
}