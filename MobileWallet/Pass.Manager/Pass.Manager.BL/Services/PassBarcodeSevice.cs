using System;
using Common.BL;
using Common.Repository;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassBarcodeService : BaseService<PassBarcode, PassBarcodeFilter, IPassManagerUnitOfWork>, IPassBarcodeService
    {
        public PassBarcodeService(IPassManagerUnitOfWork unitOfWork)
            : base(unitOfWork)
        {
        }

      
    }
}

