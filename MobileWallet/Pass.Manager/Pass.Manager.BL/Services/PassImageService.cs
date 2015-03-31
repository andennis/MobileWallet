using System;
using System.IO;
using System.Web;
using Common.BL;
using FileStorage.Core;
using FileStorage.Core.Entities;
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
            if (IsImageEmpty(entity.ImageFile))
                entity.FileStorageId = _fileStorageService.Put(entity.ImageFile.ContentStream, entity.ImageFile.FileName);
            if (IsImageEmpty(entity.ImageFile2x))
                entity.FileStorage2xId = _fileStorageService.Put(entity.ImageFile2x.ContentStream, entity.ImageFile2x.FileName);

            if (!entity.FileStorageId.HasValue && !entity.FileStorage2xId.HasValue)
                throw new Exception("No images are specified");

            base.Create(entity);
        }

        public override void Update(PassImage entity)
        {
            int? fileStorageId = entity.FileStorageId;
            int? fileStorage2xId = entity.FileStorage2xId;

            if (IsImageEmpty(entity.ImageFile))
                entity.FileStorageId = _fileStorageService.Put(entity.ImageFile.ContentStream, entity.ImageFile.FileName);
            if (IsImageEmpty(entity.ImageFile2x))
                entity.FileStorage2xId = _fileStorageService.Put(entity.ImageFile2x.ContentStream, entity.ImageFile2x.FileName);

            if (!entity.FileStorageId.HasValue && !entity.FileStorage2xId.HasValue)
                throw new Exception("No images are specified");
            
            base.Update(entity);

            //Remove old images from file storage
            if (fileStorageId.HasValue && fileStorageId != entity.FileStorageId)
                _fileStorageService.DeleteStorageItem(fileStorageId.Value);
            if (fileStorage2xId.HasValue && fileStorage2xId != entity.FileStorage2xId)
                _fileStorageService.DeleteStorageItem(fileStorage2xId.Value);
        }

        public override void Delete(int entityId)
        {
            PassImage pi = this.Get(entityId);
            if (pi == null)
                throw new Exception(string.Format("PassImmage ID:{0} does not exist", entityId));

            base.Delete(entityId);

            if (pi.FileStorageId.HasValue)
                _fileStorageService.DeleteStorageItem(pi.FileStorageId.Value);
            if (pi.FileStorage2xId.HasValue)
                _fileStorageService.DeleteStorageItem(pi.FileStorage2xId.Value);
        }

        private static bool IsImageEmpty(FileContentInfo imageInfo)
        {
            return (imageInfo != null && imageInfo.ContentStream != null && imageInfo.ContentStream.Length > 0);
        }

        public FileContentInfo GetImage(int imageId)
        {
            StorageFileInfo sfi = _fileStorageService.GetFile(imageId, true);
            return new FileContentInfo()
                   {
                       FileName = sfi.Name, 
                       ContentStream = sfi.FileStream, 
                       ContentType = MimeMapping.GetMimeMapping(sfi.Name)
                   };
        }

        public override SearchResult<PassImage> Search(SearchContext searchContext, PassImageFilter searchFilter = null)
        {
            if (searchFilter == null)
                throw new ArgumentNullException("searchFilter");

            return Search(searchContext, x => x.PassContentTemplateId == searchFilter.PassContentTemplateId);
        }

    }
}
