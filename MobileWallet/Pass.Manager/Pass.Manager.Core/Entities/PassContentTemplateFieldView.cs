using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Manager.Core.Entities
{
    public class PassContentTemplateFieldView
    {
        public int PassContentTemplateFieldId { get; set; }
        public PassContentFieldKind FieldKind { get; set; }
        public string AttributedValue { get; set; }
        public string ChangeMessage { get; set; }
        public string Label { get; set; }
        public TextAlignment? TextAlignment { get; set; }
        public int PassProjectFieldId { get; set; }
        public int PassContentTemplateId { get; set; }
        public string FieldName { get; set; }
    }
}
