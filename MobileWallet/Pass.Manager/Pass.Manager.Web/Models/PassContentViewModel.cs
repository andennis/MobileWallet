using System;
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
        public bool IsValid { get; set; }
        public EntityStatus Status { get; set; }
        public int PassContentTemplateId { get; set; }
        public int PassProjectId { get; set; }//TODO it should be set manually
    }
}