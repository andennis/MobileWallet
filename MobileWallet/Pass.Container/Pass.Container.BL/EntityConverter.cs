using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core.Entities;
using Pass.Container.Repository.Core.Entities;

namespace Pass.Container.BL
{
    public static class EntityConverter
    {
        public static PassFieldValue PassFieldInfoToFieldValue(PassFieldInfo passFieldInfo)
        {
            return new PassFieldValue()
                       {
                           PassFieldId = passFieldInfo.PassFieldId,
                           Value = passFieldInfo.Value,
                           Label = passFieldInfo.Label,
                       };
        }
    }
}
