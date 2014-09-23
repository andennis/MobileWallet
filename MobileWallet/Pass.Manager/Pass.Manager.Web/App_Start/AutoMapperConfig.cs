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

            Mapper.CreateMap<PassCertificate, PassCertificateViewModel>();
            Mapper.CreateMap<PassCertificateViewModel, PassCertificate>();
        }
    }
}