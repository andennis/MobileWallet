using AutoMapper;
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
            //Mapper.CreateMap<PassSiteViewModel, PassSite>();

            Mapper.CreateMap<User, UserViewModel>().ReverseMap();
            //Mapper.CreateMap<UserViewModel, User>();

            Mapper.CreateMap<User, UserPasswordViewModel>();

            Mapper.CreateMap<PassProject, PassProjectViewModel>().ReverseMap();
            //Mapper.CreateMap<PassProjectViewModel, PassProject>();

            Mapper.CreateMap<PassCertificateApple, PassCertificateAppleViewModel>().ReverseMap();
            //Mapper.CreateMap<PassCertificateAppleViewModel, PassCertificateApple>();

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

            Mapper.CreateMap<PassProjectFieldViewModel, PassProjectField>();
            Mapper.CreateMap<PassProjectField, PassProjectFieldViewModel>();

            Mapper.CreateMap<PassContentTemplateViewModel, PassContentTemplate>();
            Mapper.CreateMap<PassContentTemplate, PassContentTemplateViewModel>();
        }

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