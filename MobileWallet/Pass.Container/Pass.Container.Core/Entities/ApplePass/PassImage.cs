using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core.Entities.ApplePass
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
