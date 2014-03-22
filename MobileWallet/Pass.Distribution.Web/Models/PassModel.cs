using System.Collections.Generic;
using Pass.Container.Core.Entities;

namespace Pass.Distribution.Web.Models
{
    public class PassModel
    {
        public string PassToken { get; set; }
        public string Email { get; set; }
        public IList<PassFieldInfo> PassFields { get; set; }
    }
}