using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web.Controllers
{
    public class PassContentFieldController : BaseEntityController<PassContentFieldViewModel, PassContentField, PassContentFieldView, IPassContentFieldService, PassContentFieldFilter>
    {
        public PassContentFieldController(IPassContentFieldService fieldService)
            : base(fieldService)
        {
        }

    }
}