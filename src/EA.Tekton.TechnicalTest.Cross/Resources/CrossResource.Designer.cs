//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace EA.Tekton.TechnicalTest.Cross.Resources {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    public class CrossResource {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal CrossResource() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        public static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("EA.Tekton.TechnicalTest.Cross.Resources.CrossResource", typeof(CrossResource).Assembly);
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
        ///   Looks up a localized string similar to Se produjo un error al sembrar la base de datos..
        /// </summary>
        public static string ProgramMainErrorDataBase {
            get {
                return ResourceManager.GetString("ProgramMainErrorDataBase", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to webmaster@CoreLineBase.net.
        /// </summary>
        public static string ProgramUserExecuteDefaultMail {
            get {
                return ResourceManager.GetString("ProgramUserExecuteDefaultMail", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Aa1234567890..
        /// </summary>
        public static string ProgramUserExecuteDefaultPassword {
            get {
                return ResourceManager.GetString("ProgramUserExecuteDefaultPassword", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to WEBMASTER.
        /// </summary>
        public static string ProgramUserExecuteDefaultUserName {
            get {
                return ResourceManager.GetString("ProgramUserExecuteDefaultUserName", resourceCulture);
            }
        }
    }
}