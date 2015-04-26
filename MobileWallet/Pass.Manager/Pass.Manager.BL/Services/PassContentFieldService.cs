﻿using System;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassContentFieldService : PassManagerServiceBase<PassContentField, PassContentFieldFilter>, IPassContentFieldService
    {
        public PassContentFieldService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassContentField> Search(SearchContext searchContext, PassContentFieldFilter searchFilter)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentId == searchFilter.PassContentId);
        }

    }
}
