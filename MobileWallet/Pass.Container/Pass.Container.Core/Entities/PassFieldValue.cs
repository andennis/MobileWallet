
namespace Pass.Container.Core.Entities
{
    public class PassFieldValue
    {
        public int PassFieldValueId { get; set; }
        public string Value { get; set; }
        public Core.Entities.Pass Pass { get; set; }
    }
}
