using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IApplePassProcessingService : IDisposable
    {
        void RegisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string pushToken, string authToken, out PassProcessingStatus status);
        void UnregisterDevice(string deviceLibraryIdentifier, string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status);
        IList<string> GetSerialNumbersOfPasses(string deviceLibraryIdentifier, string passTypeIdentifier, out DateTime lastUpdated, 
            out PassProcessingStatus status, DateTime? passesUpdatedSince = null);
        string GetPass(string passTypeIdentifier, string serialNumber, string authToken, out PassProcessingStatus status,
            DateTime? lastUpdatedDate = null);
        void Log(string logMessage);
    }
}
