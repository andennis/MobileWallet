﻿using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Repository;
using FluentValidation.Attributes;
using Pass.Manager.Web.Areas.Site.Validators;
using Pass.Manager.Web.Common;

namespace Pass.Manager.Web.Areas.Site.Models
{
    [Validator(typeof(PassContentViewModelValidator))]
    public class PassContentViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Content"; } }
        public override int EntityId
        {
            get { return PassContentId; }
        }

        public int PassContentId { get; set; }
        public string SerialNumber { get; set; }
        public string AuthToken { get; set; }
        public DateTime? ExpDate { get; set; }
        public bool IsVoided { get; set; }
        public EntityStatus Status { get; set; }
        public int? PassContentTemplateId { get; set; }
        public string PassContentTemplateName { get; set; }
        public int PassContentTemplateVersion { get; set; }
        public int PassProjectId { get; set; }
        public string ProjectName { get; set; }

        public int? ContainerPassId { get; set; }
        public bool IsOnline { get; set; }
        public string DistributionLink { get; set; }
        public IEnumerable<SelectListItem> PassContentTemplates { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}