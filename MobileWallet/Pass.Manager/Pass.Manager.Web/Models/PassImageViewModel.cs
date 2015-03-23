using System.Web;
using FluentValidation.Attributes;
using Pass.Manager.Core.Entities;
using Pass.Manager.Web.Common;
using Pass.Manager.Web.Validators;

namespace Pass.Manager.Web.Models
{
    [Validator(typeof(PassImageViewModelValidator))]
    public class PassImageViewModel : BaseViewModel
    {
        public override string DisplayName { get { return "Pass Image"; } }
        public override int EntityId
        {
            get { return PassImageId; }
        }

        public int PassImageId { get; set; }
        public PassImageType ImageType { get; set; }
        public int? FileStorageId { get; set; }
        public int? FileStorage2xId { get; set; }
        public int PassContentTemplateId { get; set; }
        public HttpPostedFileBase ImageFile { get; set; }
        public HttpPostedFileBase ImageFile2x { get; set; }
    }
}