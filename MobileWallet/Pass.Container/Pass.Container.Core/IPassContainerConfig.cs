using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pass.Container.Core
{
    public interface IPassContainerConfig
    {
        string PassTemplateFolderName { get; }
        string PassTemplateFileName { get;}
    }
}
