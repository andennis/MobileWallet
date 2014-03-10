using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IApplePassProcessingService : IDisposable
    {
        void RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken, string authToken, out PassProcessingStatus status);
        void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken, string authToken, out PassProcessingStatus status);
        IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, ref DateTime lastUpdated, out PassProcessingStatus status, DateTime? passesUpdatedSince = null);
        void GetPass(string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status);
        void Log(string logMessage);
    }
}
