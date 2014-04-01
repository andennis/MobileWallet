using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Container.Core
{
    public interface IPassContainerService : IDisposable
    {
        int CreatePass(int passTemplateId, IList<PassFieldInfo> passFieldValues, DateTime? expDate = null);
        IList<PassFieldInfo> GetPassFields(int passId);
        void UpdatePassFields(int passId, IList<PassFieldInfo> passFieldValues);
    }
}