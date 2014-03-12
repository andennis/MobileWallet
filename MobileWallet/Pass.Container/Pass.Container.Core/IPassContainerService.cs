using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Container.Core
{
    public interface IPassContainerService : IDisposable
    {
        int CreatePass(int passTemplateId, IList<PassFieldValue> passFieldValues, DateTime? expDate = null);
        void UpdatePassFields(int passId, IList<PassFieldValue> passFieldValues);
    }
}