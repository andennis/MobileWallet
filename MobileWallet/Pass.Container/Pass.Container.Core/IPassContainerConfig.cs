using Common.Repository;

namespace Pass.Container.Core
{
    public interface IPassContainerConfig : IPassTemplateConfig, IApplePassGeneratorConfig, IDbConfig
    {
    }
}
