using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class PassTemplateService : IPassTemplateService
    {
        public int CreatePassTemlate(string passTemplatePath)
        {
            throw new NotImplementedException();
        }

        public void DeletePassTemplate(int passTemplateId)
        {
            throw new NotImplementedException();
        }

        public string GetNativePassTemplate(int passTemplateId, Core.Entities.Enums.PassTemplateType passTemplateType)
        {
            throw new NotImplementedException();
        }

        public IList<Core.Entities.PassField> GetPassFields(int passTemplateId)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassTemlate(string passTemplatePath)
        {
            throw new NotImplementedException();
        }
    }
}
