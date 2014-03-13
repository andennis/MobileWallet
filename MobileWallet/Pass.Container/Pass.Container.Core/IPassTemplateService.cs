using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassTemplateService : IDisposable
    {
        int CreatePassTemlate(string passTemplatePath);
        void DeletePassTemplate(int passTemplateId);
        string GetNativePassTemplate(int passTemplateId, ClientType clientType);
        IList<PassFieldInfo> GetPassFields(int passTemplateId);
        void UpdatePassTemlate(int passTemplateId, string passTemplatePath);
        bool ValidatePassTemplate(string passTemplateFilePath);
    }
}
