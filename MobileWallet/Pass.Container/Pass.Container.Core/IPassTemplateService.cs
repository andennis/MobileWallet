using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassTemplateService
    {
        int CreatePassTemlate(string passTemplatePath);
        void DeletePassTemplate(int passTemplateId);
        string GetNativePassTemplate(int passTemplateId, PassTemplateType passTemplateType);
        IList<PassField> GetPassFields(int passTemplateId);
        void UpdatePassTemlate(int passTemplateId, string passTemplatePath);
        bool ValidatePassTemplate(string passTemplateFilePath);
    }
}
