﻿namespace Pass.Container.Repository.Core.Entities
{
    public class PassTemplateNative
    {
        public int PassTemplateNativeId { get; set; }
        public int PassTemplateId { get; set; }
        public PassTemplate Template { get; set; }
    }
}