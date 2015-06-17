using Common.BL;

namespace Pass.Container.Core.SearchFilters
{
    public class PassRegistrationFilter : SearchFilterBase
    {
        public int PassId { get; set; }
        public int? StatusId { get; set; }
    }
}
