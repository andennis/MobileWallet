﻿using System;
using System.Web.Mvc;
using System.Web.Mvc.Filters;

namespace Pass.Manager.Web.Common
{
    public class FormAuthenticationFilterAttribute : ActionFilterAttribute, IAuthenticationFilter
    {
        public void OnAuthentication(AuthenticationContext filterContext)
        {
        }

        public void OnAuthenticationChallenge(AuthenticationChallengeContext filterContext)
        {
            var baseController = filterContext.Controller as BaseController;
            if (baseController == null)
                throw new Exception("Controller should be inherited from BaseController");

            if (baseController.AuthUserContext.IsAuthenticated)
                return;
            
            if (filterContext.HttpContext.Request.IsAjaxRequest())
                filterContext.HttpContext.Response.SuppressFormsAuthenticationRedirect = true;

             filterContext.Result = new HttpUnauthorizedResult();
        }
    }
}