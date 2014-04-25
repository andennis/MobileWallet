namespace Pass.Container.Core
{
    public interface IPassTemplateConfig
    {
        string PassTemplateFolderName { get; }
        string PassTemplateFileName { get; }
        //string PassGeneratorTempFolderPath { get; }
        string ApplePassTemplateWebServerUrl { get; }
    }
}