﻿
namespace Pass.Container.BL.Helpers
{
    public static class ApplePass
    {
        public const string TemplateImageFolder = "Images";
        public const string PassTemplateFileName = "pass_t.json";
        public const string PassFileName = "pass.json";
        public const string ManifestTemplateFileName = "manifest_t.json";
        public const string ManifestFileName = "manifest.json";
        public const string SignatureFileName = "signature";

        public const string FieldLabelFormat = "LB$${0}$$";
        public const string FieldValueFormat = "VL$${0}$$";
        public const string FieldSerialNumber = "$$SerialNumber$$";
        public const string FieldAuthToken = "$$AuthToken$$";
        public const string FieldWebServiceUrl = "$$WebServiceUrl$$";
    }
}
