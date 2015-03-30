using System;
using System.IO;
using Common.BL;
using FileStorage.Core;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Core.SearchFilters;
using Pass.Manager.Core.Services;

namespace Pass.Manager.BL.Services
{
    public class PassImageService : PassManagerServiceBase<PassImage, PassImageFilter>, IPassImageService
    {
        private readonly IFileStorageService _fileStorageService;

        public PassImageService(IPassManagerUnitOfWork unitOfWork, IFileStorageService fileStorageService)
            : base(unitOfWork)
        {
            _fileStorageService = fileStorageService;
        }

        public override void Create(PassImage entity)
        {
            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
                entity.FileStorageId = _fileStorageService.Put(entity.ImageFile);
            if (entity.ImageFile2x != null && entity.ImageFile2x.Length > 0)
                entity.FileStorage2xId = _fileStorageService.Put(entity.ImageFile2x);

            if (!entity.FileStorageId.HasValue && !entity.FileStorage2xId.HasValue)
                throw new Exception("No images are specified");

            base.Create(entity);
        }

        public override void Update(PassImage entity)
        {
            if (entity.ImageFile != null && entity.ImageFile.Length > 0)
                entity.FileStorageId = _fileStorageService.Put(entity.ImageFile);
            if (entity.ImageFile2x != null && entity.ImageFile2x.Length > 0)
                entity.FileStorage2xId = _fileStorageService.Put(entity.ImageFile2x);

            if (!entity.FileStorageId.HasValue && !entity.FileStorage2xId.HasValue)
                throw new Exception("No images are specified");

            PassImage pm = this.Get(entity.PassImageId);
            
            base.Update(entity);

            //Remove old images from file storage
            if (pm.FileStorageId.HasValue && pm.FileStorageId != entity.FileStorageId)
                _fileStorageService.DeleteStorageItem(pm.FileStorageId.Value);
            if (pm.FileStorage2xId.HasValue && pm.FileStorage2xId != entity.FileStorage2xId)
                _fileStorageService.DeleteStorageItem(pm.FileStorage2xId.Value);
        }

        //public Stream GetImageFileStream()


        public override SearchResult<PassImage> Search(SearchContext searchContext, PassImageFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }

    }
}
