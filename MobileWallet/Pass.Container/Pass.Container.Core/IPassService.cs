using System;
using System.Collections.Generic;
using Common.BL;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.SearchFilters;

namespace Pass.Container.Core
{
    public interface IPassService : IDisposable
    {
        int CreatePass(int passTemplateId, IList<PassFieldInfo> passFieldValues, DateTime? expDate = null);
        IList<PassFieldInfo> GetPassFields(int passId);
        void UpdatePassFields(int passId, IList<PassFieldInfo> newFieldValues);
        string GetPassPackage(int passId, ClientType deviceType);
        SearchResult<RegistrationInfo> GetPassRegistrations(SearchContext searchContext, PassRegistrationFilter filter);
    }
}