using Common.BL;
using Pass.Container.Core;

namespace Pass.Container.BL
{
    public class SerialNumberGenerator : ISerialNumberGenerator
    {
        private readonly ISequenceGenerator<int> _sequenceGenerator;
        public SerialNumberGenerator(ISequenceGenerator<int> sequenceGenerator)
        {
            _sequenceGenerator = sequenceGenerator;
        }

        public string GetNextSerialNumber(string serNumCounter)
        {
            return _sequenceGenerator.GetNextValue(serNumCounter).ToString();
        }
    }
}
