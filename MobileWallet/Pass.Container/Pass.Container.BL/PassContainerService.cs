using System;
using System.Collections.Generic;
using System.Linq;
using Common.Repository;
using Pass.Container.Core;
using Pass.Container.Core.Entities;
using Pass.Container.Repository.Core;
using Pass.Container.Repository.Core.Entities;
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

        public int CreatePass(int passTemplateId, IList<PassFieldInfo> fieldValues, DateTime? expDate = null)
        {
            IRepository<PassFieldValue> repPassFieldVal = _pcUnitOfWork.GetRepository<PassFieldValue>();
            IRepository<PassField> repPassField = _pcUnitOfWork.GetRepository<PassField>();
            IRepository<RepEntities.Pass> repPass = _pcUnitOfWork.GetRepository<RepEntities.Pass>();

            //Create Pass
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

            //Create values for pass fields
            IList<PassField> passFields = repPassField.Query().Filter(x => x.PassTemplateId == passTemplateId).Get().ToList();
            foreach (PassField passField in passFields)
            {
                PassFieldInfo fieldInfo = fieldValues.FirstOrDefault(x => x.PassFieldId == passField.PassFieldId)
                    ?? fieldValues.FirstOrDefault(x => x.Name == passField.Name);

                PassFieldValue pfv;
                if (fieldInfo == null)
                {
                    pfv = new PassFieldValue()
                              {
                                  PassId = pass.PassId,
                                  PassFieldId = passField.PassFieldId,
                                  Value = passField.DefaultValue,
                                  Label = passField.DefaultLabel
                              };
                }
                else
                {
                    pfv = EntityConverter.PassFieldInfoToFieldValue(fieldInfo);
                    pfv.PassId = pass.PassId;
                    pfv.PassFieldId = passField.PassFieldId;
                }

                repPassFieldVal.Insert(pfv);
            }

            /*
            foreach (PassFieldInfo fieldValue in fieldValues)
            {
                PassFieldValue pfv = EntityConverter.PassFieldInfoToFieldValue(fieldValue);
                pfv.PassId = pass.PassId;
                repPassFieldVal.Insert(pfv);
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
