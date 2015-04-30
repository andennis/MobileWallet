﻿using System;
using System.IO;
using System.Linq;
using Common.Utils;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Services;
using Pass.Manager.Core.Exceptions;

namespace Pass.Manager.BL.Services
{
    public class PassOnlineService : IPassOnlineService
    {
        private readonly IPassService _passService;
        private readonly IPassContentService _passContentService;

        public PassOnlineService(IPassContentService passContentService, IPassService passService)
        {
            _passService = passService;
            _passContentService = passContentService;
        }

        public void Register(int passContentId)
        {
            PassContent pc = _passContentService.GetDetails(passContentId);
            if (pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has already been registered", passContentId));
            if (!pc.PassContentTemplate.PassContainerTemplateId.HasValue)
                throw new PassManagerGeneralException(string.Format("Pass content template has not been registered for the PassContentId: {0}", passContentId));

            pc.ContainerPassId = _passService.CreatePass(pc.PassContentTemplate.PassContainerTemplateId.Value, pc.Fields.Select(ConvertTo).ToList(), pc.ExpDate);
            //TODO the pass should be removed from pass container if the update operation failed
            _passContentService.Update(pc);
            
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