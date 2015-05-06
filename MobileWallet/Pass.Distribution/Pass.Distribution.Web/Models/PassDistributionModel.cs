using System.Collections.Generic;
using Pass.Distribution.Core.Entities;

namespace Pass.Distribution.Web.Models
{
    public class PassDistributionModel
    {
        public int PassContentId { get; set; }
        public string Email { get; set; }
        public IList<DistribField> PassFields { get; set; }
    }
}