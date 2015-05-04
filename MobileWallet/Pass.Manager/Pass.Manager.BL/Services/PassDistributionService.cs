using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Utils;
using Pass.Container.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassDistributionService : IPassDistributionService
    {
        private readonly IPassService _passService;
        private readonly IPassContentService _passContentService;

        public PassDistributionService(IPassContentService passContentService, IPassService passService)
        {
            _passService = passService;
            _passContentService = passContentService;
            
        }
        public string GetPassCode(int passContentId)
        {
            string code = string.Format("{0}:{1}", "p", passContentId);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
        }

        public string GetPassTemplateCode(int passContentTemplateId)
        {
            string code = string.Format("{0}:{1}", "t", passContentTemplateId);
            return Convert.ToBase64String(Encoding.UTF8.GetBytes(code));
        }

        public FileContentInfo GetPassPackage(string passCode)
        {
            byte[] data = Convert.FromBase64String(passCode);
            string code = Encoding.UTF8.GetString(data);
            return null;
        }
    }
}
