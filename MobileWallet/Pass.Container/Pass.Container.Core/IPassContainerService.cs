using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Container.Core
{
    public interface IPassContainerService : IDisposable
    {
        int CreatePass(int passTemplateId, IEnumerable<PassFieldValue> passFieldValues);
        void UpdatePassFields(int passId, IEnumerable<PassFieldValue> passFieldValues);
    }
}