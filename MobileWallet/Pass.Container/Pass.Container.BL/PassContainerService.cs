using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class PassContainerService : IPassContainerService
    {
        private readonly IPassContainerUnitOfWork _pcUnitOfWork;

        public PassContainerService(IPassContainerUnitOfWork pcUnitOfWork)
        {
            _pcUnitOfWork = pcUnitOfWork;
        }

        public int CreatePass(int passTemplateId, IEnumerable<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }

        public void UpdatePassFields(int passId, IEnumerable<Core.Entities.PassFieldValue> passFieldValues)
        {
            throw new NotImplementedException();
        }

        #region IDisposable
        public void Dispose()
        {
            _pcUnitOfWork.Dispose();
        }
        #endregion
    }
}
