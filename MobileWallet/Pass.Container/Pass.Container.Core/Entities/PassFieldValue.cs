﻿
namespace Pass.Container.Core.Entities
{
    public class PassFieldValue
    {
        public int PassFieldValueId { get; set; }
        public string Value { get; set; }
        public Pass Pass { get; set; }
        public PassField PassField { get; set; }
    }
}
