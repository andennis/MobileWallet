using System;
using System.IO;
using System.Linq;
using System.Web;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Core.Exceptions;

namespace Pass.Manager.BL.Services
{
    public class PassOnlineService : IPassOnlineService
    {
        private readonly IPassManagerConfig _pmConfig;
        private readonly IPassService _passService;
        private readonly IPassContentService _passContentService;

        public PassOnlineService(IPassManagerConfig pmConfig, IPassContentService passContentService, IPassService passService)
        {
            _pmConfig = pmConfig;
            _passService = passService;
            _passContentService = passContentService;
        }

        public void Register(int passContentId)
        {
            PassContent pc = _passContentService.GetDetails(passContentId);
            if (pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has already been registered", passContentId));

            pc.ContainerPassId = _passService.CreatePass(pc.PassContentTemplateId, pc.Fields.Select(ConvertTo).ToList(), pc.ExpDate);
        }

        private PassFieldInfo ConvertTo(PassContentField field)
        {
            return new PassFieldInfo()
            {
                Name = field.PassProjectField.Name,
                Label = field.FieldLabel,
                Value = field.FieldValue
            };
        }

        public void UpdateOnline(int passContentId)
        {
            throw new NotImplementedException();
        }

        public FileContentInfo GetPassPackage(int passContentId)
        {
            PassContent pc = _passContentService.GetDetails(passContentId);
            if (!pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has not been online yet", passContentId));

            string packagePath = _passService.GetPassPackage(pc.ContainerPassId.Value, ClientType.Apple);

            var fs = new FileStream(packagePath, FileMode.Open, FileAccess.Read);
            return new FileContentInfo()
            {
                ContentStream = fs,
                FileName = string.Format("pass{0}.pkpass", pc.PassContentId),
                ContentType = "application/vnd.apple.pkpass"
            };

        }
    }
}
