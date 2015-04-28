using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Common.Repository;
using FluentValidation.Attributes;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
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
        public int? ContainerPassId { get; set; }
        public int PassProjectId { get; set; }

        public IEnumerable<SelectListItem> PassContentTemplates { get; set; }
    }
}