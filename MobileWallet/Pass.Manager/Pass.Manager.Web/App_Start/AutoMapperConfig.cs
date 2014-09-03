using AutoMapper;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Pass.Manager.Web
{
    public static class AutoMapperConfig
    {
        public static void Configure()
        {
            Mapper.CreateMap<PassSite, PassSiteViewModel>();
            Mapper.CreateMap<PassSiteViewModel, PassSite>();
        }
    }
}