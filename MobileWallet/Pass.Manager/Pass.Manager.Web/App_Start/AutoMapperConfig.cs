using AutoMapper;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Models;

namespace Pass.Manager.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<PassSite, PassSiteViewModel>();
            Mapper.CreateMap<PassSiteViewModel, PassSite>();

            Mapper.CreateMap<User, UserViewModel>();
            Mapper.CreateMap<UserViewModel, User>();

            Mapper.CreateMap<User, UserPasswordViewModel>();

            Mapper.CreateMap<PassProject, PassProjectViewModel>();
            Mapper.CreateMap<PassProjectViewModel, PassProject>();

            Mapper.CreateMap<PassCertificateApple, PassCertificateAppleViewModel>();
            Mapper.CreateMap<PassCertificateAppleViewModel, PassCertificateApple>();

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
        }
    }
}