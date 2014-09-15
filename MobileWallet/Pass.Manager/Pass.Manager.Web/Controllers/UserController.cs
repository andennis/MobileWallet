﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Web;
using System.Web.Mvc;
using AutoMapper;
using Common.Extensions;
using Common.Utils;
using FluentValidation.Results;
using Pass.Manager.Core;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Models;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Controllers
{
    public class UserController : BaseEntityController<UserViewModel, User>
    {
        public UserController(IUserService userService)
            : base(userService)
        {
        }

        public override ActionResult Create(UserViewModel userViewModel)
        {
            userViewModel.Password = Crypto.CalculateHash(userViewModel.UserName.ToLower(), userViewModel.Password);
            return base.Create(userViewModel);
        }

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public override ActionResult Edit(UserViewModel userViewModel)
        //{
        //    User user = _userService.Get(userViewModel.UserId);
        //    userViewModel.Password = user.Password;
        //    return base.Edit(userViewModel);
        //}
    }
}