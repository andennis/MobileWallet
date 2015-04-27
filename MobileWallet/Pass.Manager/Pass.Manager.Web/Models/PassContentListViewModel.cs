using System.Collections.Generic;
using System.Web.Mvc;

namespace Pass.Manager.Web.Models
{
    public class PassContentListViewModel
    {
        public int? PassContentTemplateId { get; set; }
        public IEnumerable<SelectListItem> PassContentTemplates { get; set; }
    }
}