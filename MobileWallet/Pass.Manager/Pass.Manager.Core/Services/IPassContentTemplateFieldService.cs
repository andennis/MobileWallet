﻿using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassContentTemplateFieldService : IPassManagerServiceBase<PassContentTemplateField, PassContentTemplateFieldFilter>
    {
        IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId, int? curPassProjectFieldId = null);
    }
}