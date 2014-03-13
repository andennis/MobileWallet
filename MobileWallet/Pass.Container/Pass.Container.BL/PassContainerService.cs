using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Repository;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Repository.Core;
using RepEntities = Pass.Container.Repository.Core.Entities;


namespace Pass.Container.BL
{
    public class PassContainerService : IPassContainerService
    {
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;

        public PassContainerService(IPassContainerUnitOfWork pcUnitOfWork)
        {
            _pcUnitOfWork = pcUnitOfWork;
        }

        public int CreatePass(int passTemplateId, IList<PassFieldInfo> passFieldValues, DateTime? expDate = null)
        {
            IRepository<RepEntities.PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<RepEntities.PassFieldValue>();
            //IRepository<PassTemplate> repPassTemplate = _pcUnitOfWork.GetRepository<PassTemplate>();
            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();

            var pass = new RepEntities.Pass()
                           {
                               PassTemplateId = passTemplateId,
                               SerialNumber = GenerateSerialNumber(),
                               PassTypeIdentifier = string.Empty,
                               AuthToken = GenerateAuthToken(),
                               Status = EntityStatus.Active,
                               ExpirationDate = expDate
                           };
            repPass.Insert(pass);

            /*
            foreach (RepEntities.PassFieldValue passFieldValue in passFieldValues)
            {
                passFieldValue.PassId = pass.PassId;
                repPassFieldVal.Insert(passFieldValue);
            }
            */

            _pcUnitOfWork.Save();
            return pass.PassId;
        }

        public void UpdatePassFields(int passId, IList<PassFieldInfo> passFieldValues)
        {
            throw new NotImplementedException();
        }

        private string GenerateSerialNumber()
        {
            return Guid.NewGuid().ToString();
        }

        private string GenerateAuthToken()
        {
            return Guid.NewGuid().ToString().ToUpper();
        }

        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}
