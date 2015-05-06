using Common.Repository;

namespace Pass.Manager.Core
{
    public interface IPassManagerConfig : IDbConfig
    {
        //string SecurityKey { get; }
        string WorkingFolder { get; }
        string WebDistributionUrl { get; }
    }
}
