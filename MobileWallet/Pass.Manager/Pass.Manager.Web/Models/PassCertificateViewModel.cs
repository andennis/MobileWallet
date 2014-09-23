using System;
using System.Collections.Generic;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Models
{
    public class PassCertificateViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Certificate"; } }
        public int EntityID { get { return PassCertificateId; } }

        public int PassCertificateId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime ExpDate { get; set; }
        public int CertificateStorageId { get; set; }
    }
}