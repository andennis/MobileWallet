using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Container.Core
{
    public interface IPassTemplateService : IDisposable
    {
        int CreatePassTemlate(string passTemplatePath);
        void DeletePassTemplate(int passTemplateId);
        IList<PassFieldInfo> GetPassTemplateFields(int passTemplateId);
        void UpdatePassTemlate(int passTemplateId, string passTemplatePath);
    }
}
