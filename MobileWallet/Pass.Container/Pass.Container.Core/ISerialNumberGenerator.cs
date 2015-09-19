namespace Pass.Container.Core
{
    public interface ISerialNumberGenerator
    {
        string GetNextSerialNumber(string serNumCounter);
    }
}