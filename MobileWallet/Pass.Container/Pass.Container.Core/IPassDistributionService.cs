﻿using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassDistributionService : IDisposable
	{
        void CreateClientPass(int passTemplateId, IList<PassFieldValue> passFieldValues);
		void GetClientPassPackage(int passId, DeviceType deviceType);
        void GetPassPackage(int passTemplateId, DeviceType deviceType);
        void UpdatePassFields(int passId, IList<PassFieldValue> passFieldValues);

        string GetPassToken(int passId);
        string GetPassTemplateToken(int passTempleteId);
        PassTokenInfo GetPassTokenInfo(string passToken);
	}
}