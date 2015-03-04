using Common.Repository;

namespace Pass.Manager.Core.Entities
{
    public class PassBarcode : EntityVersionable
    {
        public int PassContentTemplateId { get; set; }
        public PassContentTemplate PassContentTemplate { get; set; }

        public string AltText { get; set; }
        public PassBarcodeFormat? Format { get; set; }
        public string Message { get; set; }
        public string MessageEncoding { get; set; }
    }
}
