﻿using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassContentTemplateService : IBaseService<PassContentTemplate, PassContentTemplateFilter>
    {
        PassContentTemplate GetDetails(int entityId);
    }
}