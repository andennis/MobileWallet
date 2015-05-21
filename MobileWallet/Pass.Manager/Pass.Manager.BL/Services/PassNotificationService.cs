﻿using System;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.Exceptions;
using Pass.Manager.Core.Services;
using Pass.Notification.Core;

namespace Pass.Manager.BL.Services
{
    public class PassNotificationService : IPassNotificationService
    {
        private readonly IPushNotificationService _pushService;
        private readonly IPassContentService _passContentService;
        private readonly IPassContentTemplateService _passContentTemplateService;
        private readonly Core.Services.IPassCertificateService _certificateService;
        private readonly IPassService _passService;

        public PassNotificationService(IPushNotificationService pushService, 
            IPassContentService passContentService,
            IPassContentTemplateService passContentTemplateService,
            Core.Services.IPassCertificateService certificateService,
            IPassService passService)
        {
            _pushService = pushService;
            _passContentService = passContentService;
            _passContentTemplateService = passContentTemplateService;
            _certificateService = certificateService;
            _passService = passService;
        }

        public void NotifyClientDevice(int passContentId, int clientDeviceId)
        {
            PassContent pc = _passContentService.Get(passContentId);
            if (!pc.ContainerPassId.HasValue)
                throw new PassManagerGeneralException(string.Format("PassContentId: {0} has not been registered yet", passContentId));

            PassContentTemplate pct = _passContentTemplateService.GetDetails(pc.PassContentTemplateId);
            PassCertificate cert = _certificateService.Get(pct.PassProject.PassCertificateId);
            RegistrationInfo regInfo = _passService.GetPassRegistration(pc.ContainerPassId.Value, clientDeviceId);

            _pushService.SendPushNotifications(cert.CertificateStorageId, new[] { regInfo.PushToken });
        }

        public void NotifyPassClientDevices(int passContentId)
        {
            throw new NotImplementedException();
        }

        public void NotifyPassTemplateClientDevices(int passContentTemplateId)
        {
            throw new NotImplementedException();
        }
    }
}
