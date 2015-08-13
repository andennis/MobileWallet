using System.Collections.Generic;
using Common.BL;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;

namespace Pass.Manager.Core.Services
{
    public interface IPassContentFieldService : IBaseService<PassContentField, PassContentFieldFilter>
    {
        PassContentFieldView GetView(int passContentId, int passProjectFieldId);
        IEnumerable<PassContentFieldView> GetListView(int passContentId);
    }
}