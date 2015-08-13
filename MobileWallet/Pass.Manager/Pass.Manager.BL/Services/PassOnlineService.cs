using System.Collections.Generic;
using System.IO;
using System.Linq;
using Common.BL;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Container.Core.SearchFilters;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Core.Exceptions;

namespace Pass.Manager.BL.Services
{
    public class PassOnlineService : IPassOnlineService
    {
        private readonly IPassService _passService;
        private readonly IPassContentService _passContentService;
        private readonly IPassContentFieldService _passContentFieldService;

        public PassOnlineService(IPassContentService passContentService, 
            IPassContentFieldService passContentFieldService,
            IPassService passService)
        {
            _passContentService = passContentService;
            _passContentFieldService = passContentFieldService;
            _passService = passService;
        }

        public int Register(int passContentId)
        {
            PassContent pc = _passContentService.Get(passContentId);
            if (pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has already been registered", passContentId));
            if (!pc.PassContentTemplate.PassContainerTemplateId.HasValue)
                throw new PassManagerGeneralException(string.Format("Pass content template has not been registered for the PassContentId: {0}", passContentId));

            IEnumerable<PassContentFieldView> fields = _passContentFieldService.GetListView(passContentId);
            pc.ContainerPassId = _passService.CreatePass(pc.PassContentTemplate.PassContainerTemplateId.Value, fields.Select(ConvertTo), pc.ExpDate);
            //TODO the pass should be removed from pass container if the update operation failed
            _passContentService.Update(pc);

            return pc.ContainerPassId.Value;
        }

        public void UpdateOnline(int passContentId)
        {
            PassContent pc = _passContentService.Get(passContentId);
            if (!pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has not been registered yet", passContentId));

            IEnumerable<PassContentFieldView> fields = _passContentFieldService.GetListView(passContentId);
            _passService.UpdatePassFields(pc.ContainerPassId.Value, fields.Select(ConvertTo).ToList());
        }

        private PassFieldInfo ConvertTo(PassContentFieldView field)
        {
            return new PassFieldInfo()
            {
                Name = field.FieldName,
                Label = field.FieldLabel,
                Value = field.FieldValue
            };
        }

        public FileContentInfo GetPassPackage(int passContentId)
        {
            PassContent pc = _passContentService.Get(passContentId);
            if (!pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has not been online yet", passContentId));

            string packagePath = _passService.GetPassPackage(pc.ContainerPassId.Value, ClientType.Apple);

            var fs = new FileStream(packagePath, FileMode.Open, FileAccess.Read);
            return new FileContentInfo()
            {
                ContentStream = fs,
                FileName = Path.GetFileName(packagePath), //string.Format("pass{0}.pkpass", pc.PassContentId),
                ContentType = "application/vnd.apple.pkpass"
            };

        }

        public SearchResult<RegistrationInfo> GetPassRegistrations(SearchContext searchContext, PassRegistrationFilter filter)
        {
            PassContent pc = _passContentService.Get(filter.PassId);
            if (!pc.ContainerPassId.HasValue)
                return new SearchResult<RegistrationInfo>();

            filter.PassId = pc.ContainerPassId.Value;
            return _passService.GetPassRegistrations(searchContext, filter);
        }

    }
}
