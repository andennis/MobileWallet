using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class PassDistributionService : IPassDistributionService
    {
        public void CreateClientPass(int passTemplateId, IList<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }

        public void GetClientPassPackage(int passId, Core.Entities.Enums.DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void GetPassPackage(int passTemplateId, Core.Entities.Enums.DeviceType deviceType)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassFields(int passId, IList<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }
    }
}
