using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class AppleDevicePassProcessingService : IAppleDevicePassProcessingService
    {
        public void RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushTokenJson)
        {
            throw new NotImplementedException();
        }

        public void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushTokenJson)
        {
            throw new NotImplementedException();
        }

        public IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, string passesUpdatedSince = null)
        {
            throw new NotImplementedException();
        }

        public void GetPass(string passTypeIdentifier, string serialNumber)
        {
            throw new NotImplementedException();
        }

        public void Log(string logMessage)
        {
            throw new NotImplementedException();
        }
    }
}
