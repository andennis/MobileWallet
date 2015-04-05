using System;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassProcessingAppleService : IDisposable
    {
        PassProcessingStatus RegisterDevice(string deviceLibraryIdentifier, string passTypeId, string serialNumber, string pushToken, string authToken);
        PassProcessingStatus UnregisterDevice(string deviceLibraryIdentifier, string passTypeId, string serialNumber, string authToken);
        PassProcessingStatus GetChangedPasses(string deviceLibraryIdentifier, string passTypeId, DateTime? passesUpdatedSince, out ChangedPassesInfo changedPassesInfo);
        PassProcessingStatus GetPassPackage(string passTypeId, string serialNumber, string authToken, DateTime? lastUpdatedDate, out PassPackageInfo packageInfo);
        void Log(string logMessage);
    }
}
