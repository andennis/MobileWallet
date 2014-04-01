using System;
using System.Collections.Generic;
using Pass.Container.Core.Entities;
using Pass.Container.Core.Entities.Enums;

namespace Pass.Container.Core
{
    public interface IPassDistributionService : IDisposable
	{
        /// <summary>
        /// 
        /// </summary>
        /// <param name="passToken"></param>
        /// <param name="deviceType"></param>
        /// <param name="passFields">the fields from distribution page</param>
        /// <returns>It returns the file path</returns>
        string GetPassPackage(PassTokenInfo passToken, ClientType deviceType);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="passToken"></param>
        /// <returns>It returns the fields that should be filled out on distribution page</returns>
        IList<PassFieldInfo> GetPassFields(PassTokenInfo passToken);

        string GetPassToken(int passId);
        string GetPassTemplateToken(int passTempleteId);
        PassTokenInfo DecryptPassToken(string passToken);
	}
}