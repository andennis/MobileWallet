﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Pass.Manager.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Pass.Manager.Resources.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Certificate file should be specified.
        /// </summary>
        public static string CertificateFileNotSpecified {
            get {
                return ResourceManager.GetString("CertificateFileNotSpecified", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Register Online.
        /// </summary>
        public static string RegisterOnlineLink {
            get {
                return ResourceManager.GetString("RegisterOnlineLink", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to register this pass online?.
        /// </summary>
        public static string RegisterPassOnlineConformation {
            get {
                return ResourceManager.GetString("RegisterPassOnlineConformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The pass has been registered successully.
        /// </summary>
        public static string RegisterPassOnlineSuccess {
            get {
                return ResourceManager.GetString("RegisterPassOnlineSuccess", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Are you sure you want to register this template online?.
        /// </summary>
        public static string RegisterTemplateOnlineConformation {
            get {
                return ResourceManager.GetString("RegisterTemplateOnlineConformation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The template has been registered successully.
        /// </summary>
        public static string RegisterTemplateOnlineSuccess {
            get {
                return ResourceManager.GetString("RegisterTemplateOnlineSuccess", resourceCulture);
            }
        }
    }
}
