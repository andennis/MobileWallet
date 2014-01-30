namespace Pass.Container.Core.Entities.Templates.NativePassTemplates.ApplePassTemplate
{
    public class PassImage
    {
        public ImageType ImageType { get; set; }
        public string ImagePath { get; set; }
        public bool Image2X { get; set; }
    }

    public enum ImageType
    {
        Background = 0, 
        Footer = 1, 
        Icon = 2, 
        Logo = 3,
        Strip = 4,
        Thumbnail = 5
    }
}
