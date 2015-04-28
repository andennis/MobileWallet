using System.Web;
using AutoMapper;
using Common.Utils;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Models;
using Pass.Manager.Web.Models.GeneralPassTemplate;

namespace Pass.Manager.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<PassSite, PassSiteViewModel>().ReverseMap();

            Mapper.CreateMap<User, UserViewModel>().ReverseMap();
            Mapper.CreateMap<User, UserPasswordViewModel>();

            Mapper.CreateMap<PassProject, PassProjectViewModel>().ReverseMap();
            Mapper.CreateMap<PassCertificateApple, PassCertificateAppleViewModel>().ReverseMap();

            Mapper.CreateMap<PassSiteUser, PassSiteUserViewModel>()
                .ForMember(dst => dst.UserId, x => x.MapFrom(src => src.User.UserId))
                .ForMember(dst => dst.FirstName, x => x.MapFrom(src => src.User.FirstName))
                .ForMember(dst => dst.LastName, x => x.MapFrom(src => src.User.LastName))
                .ForMember(dst => dst.UserName, x => x.MapFrom(src => src.User.UserName));
            Mapper.CreateMap<PassSiteUserViewModel, PassSiteUser>();
            Mapper.CreateMap<PassSiteUserViewModel, User>();
            /*
                .ForMember(dst => dst.User, x => x.MapFrom(src => new User()
                                                                  {
                                                                      UserName = src.UserName,
                                                                      FirstName = src.FirstName,
                                                                      LastName = src.LastName,
                                                                  }));
            */

            Mapper.CreateMap<PassSiteCertificate, PassSiteCertificateViewModel>()
                .ForMember(dst => dst.PassCertificateId, x => x.MapFrom(src => src.PassCertificate.PassCertificateId))
                .ForMember(dst => dst.Name, x => x.MapFrom(src => src.PassCertificate.Name))
                .ForMember(dst => dst.Description, x => x.MapFrom(src => src.PassCertificate.Description))
                .ForMember(dst => dst.ExpDate, x => x.MapFrom(src => src.PassCertificate.ExpDate));
            Mapper.CreateMap<PassSiteCertificateViewModel, PassSiteCertificate>();
            Mapper.CreateMap<PassSiteCertificateViewModel, PassCertificate>();
            Mapper.CreateMap<PassSiteCertificateViewModel, PassCertificateApple>();

            Mapper.CreateMap<PassProjectType, PassStyle>().ConvertUsing(PassProjectTypeToPassStyle);

            Mapper.CreateMap<PassProjectField, PassProjectFieldViewModel>().ReverseMap();
            Mapper.CreateMap<PassContentTemplate, PassContentTemplateViewModel>().ReverseMap();

            Mapper.CreateMap<PassContentTemplateField, PassContentTemplateFieldViewModel>().ReverseMap();
            Mapper.CreateMap<PassContentTemplateFieldView, PassContentTemplateFieldViewModel>();

            Mapper.CreateMap<PassImage, PassImageViewModel>()
                .ForMember(dst => dst.ImageFile, x => x.Ignore())
                .ForMember(dst => dst.ImageFile2x, x => x.Ignore());
            Mapper.CreateMap<PassImageViewModel, PassImage>()
                .ForMember(dst => dst.ImageFile, x => x.MapFrom(src => HttpPostedFileToFileContentInfo(src.ImageFile)))
                .ForMember(dst => dst.ImageFile2x, x => x.MapFrom(src => HttpPostedFileToFileContentInfo(src.ImageFile2x)));

            Mapper.CreateMap<PassBeaconViewModel, PassBeacon>().ReverseMap();
            Mapper.CreateMap<PassLocationViewModel, PassLocation>().ReverseMap();

            Mapper.CreateMap<PassContentView, PassContentViewModel>();
            Mapper.CreateMap<PassContent, PassContentViewModel>()
                .ForMember(dst => dst.PassProjectId, x => x.MapFrom(src => src.PassContentTemplate.PassProjectId))
                .ForMember(dst => dst.PassContentTemplateName, x => x.MapFrom(src => src.PassContentTemplate.Name));
            Mapper.CreateMap<PassContentViewModel, PassContent>()
                .ForMember(dst => dst.ContainerPassId, x => x.Ignore());

            Mapper.CreateMap<PassContentFieldView, PassContentFieldViewModel>();
            //Mapper.CreateMap<PassContentField, PassContentFieldViewModel>();
            Mapper.CreateMap<PassContentFieldViewModel, PassContentField>()
                .ForMember(dst => dst.PassProjectFieldId, x => x.Ignore())
                .ForMember(dst => dst.PassContentId, x => x.Ignore())
                .ForMember(dst => dst.PassContentFieldId, x => x.Ignore());
        }


        private static FileContentInfo HttpPostedFileToFileContentInfo(HttpPostedFileBase file)
        {
            if (file == null)
                return null;

            return new FileContentInfo()
                   {
                       FileName = file.FileName,
                       ContentStream = file.InputStream,
                       ContentType = file.ContentType
                   };
        }

        //TODO it should be removed
        private static PassStyle PassProjectTypeToPassStyle(PassProjectType passProjectType)
        {
            switch (passProjectType)
            {
                case PassProjectType.Coupon:
                    return PassStyle.Coupon;
                case PassProjectType.BoardingPass:
                    return PassStyle.BoardingPass;
                case PassProjectType.EventTicket:
                    return PassStyle.EventTicket;
                case PassProjectType.StoreCard:
                    return PassStyle.StoreCard;
                //case PassProjectType.Custom:
                //    return PassStyle.Generic;
            }

            return PassStyle.Generic;
        }
    }
}