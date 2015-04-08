﻿using System;
using System.Collections.Generic;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassContentTemplateFieldService : PassManagerServiceBase<PassContentTemplateField, PassContentTemplateFieldFilter>, IPassContentTemplateFieldService
    {
        public PassContentTemplateFieldService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassContentTemplateField> Search(SearchContext searchContext, PassContentTemplateFieldFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }

        public IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId, int? curPassProjectFieldId = null)
        {
            return _unitOfWork.PassContentTemplateFieldRepository.GetUnmappedFields(passContentTemplateId, curPassProjectFieldId);
        }
    }
}