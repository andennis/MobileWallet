using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Common.BL;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassContentTemplateFieldService : BaseService<PassContentTemplateField, PassContentTemplateFieldFilter, IPassManagerUnitOfWork>, IPassContentTemplateFieldService
    {
        public PassContentTemplateFieldService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

        public override SearchResult<PassContentTemplateField> Search(SearchContext searchContext, PassContentTemplateFieldFilter searchFilter)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }

        public IEnumerable<PassProjectField> GetUnmappedFields(int passContentTemplateId, int? curPassProjectFieldId = null)
        {
            return _unitOfWork.PassContentTemplateFieldRepository.GetUnmappedFields(passContentTemplateId, curPassProjectFieldId);
        }

        protected override void Validate(PassContentTemplateField entity)
        {
            base.Validate(entity);
            IEnumerable<PassProjectField> fields = _unitOfWork.PassContentTemplateFieldRepository.GetUnmappedFields(entity.PassContentTemplateId, entity.PassProjectFieldId);
            if (fields.All(x => x.PassProjectFieldId != entity.PassProjectFieldId))
                throw new PassManagerGeneralException(string.Format("PassProjectFieldId: {0} cannot be mapped twice to PassContentTemplateId: {1}", 
                    entity.PassProjectFieldId, entity.PassContentTemplateId));
        }
    }
}
