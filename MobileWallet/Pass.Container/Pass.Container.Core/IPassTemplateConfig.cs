namespace Pass.Container.Core
{
    public interface IPassTemplateConfig
    {
        string PassTemplateFolderName { get; }
        string ApplePassTemplateWebServerUrl { get; }
        string PassTemplateWorkingFolder { get; }
    }
}