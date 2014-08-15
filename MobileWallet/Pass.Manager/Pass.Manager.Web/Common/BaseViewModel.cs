using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace Pass.Manager.Web.Common
{
    public abstract class BaseViewModel : IViewModel
    {
        public abstract string DisplayName { get; }
    }
}