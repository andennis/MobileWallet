using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core
{
    public interface IAppleDevicePassProcessingService
    {
        void RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushTokenJson);
        void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushTokenJson);
        IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, string passesUpdatedSince = null);
        void GetPass(string passTypeIdentifier, string serialNumber);
        void Log(string logMessage);
    }
}
