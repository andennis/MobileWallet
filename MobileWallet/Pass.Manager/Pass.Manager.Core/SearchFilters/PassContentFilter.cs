using Common.BL;

namespace Pass.Manager.Core.SearchFilters
{
    public class PassContentFilter : SearchFilterBase
    {
        public int PassSiteId { get; set; }
        public int? PassProjectId { get; set; }
        public int? PassContentTemplateId { get; set; }
    }
}
