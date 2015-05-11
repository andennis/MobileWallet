using System;
using System.Collections.Generic;
using Common.Repository;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassService : IDisposable
    {
        int CreatePass(int passTemplateId, IList<PassFieldInfo> passFieldValues, DateTime? expDate = null);
        IList<PassFieldInfo> GetPassFields(int passId);
        void UpdatePassFields(int passId, IList<PassFieldInfo> newFieldValues);
        string GetPassPackage(int passId, ClientType deviceType);
        IList<RegistrationInfo> GetPassRegistrations(int passId, EntityStatus? status);
    }
}