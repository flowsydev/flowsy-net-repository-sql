﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Flowsy.Repository.Sql.Resources {
    using System;
    
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Shared_es_mx {
        
        private static System.Resources.ResourceManager resourceMan;
        
        private static System.Globalization.CultureInfo resourceCulture;
        
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Shared_es_mx() {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager {
            get {
                if (object.Equals(null, resourceMan)) {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Flowsy.Repository.Sql.Resources.Shared_es_mx", typeof(Shared_es_mx).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        internal static string CouldNotGetValidConnection {
            get {
                return ResourceManager.GetString("CouldNotGetValidConnection", resourceCulture);
            }
        }
        
        internal static string InvalidConnectionKeyNoPeriod {
            get {
                return ResourceManager.GetString("InvalidConnectionKeyNoPeriod", resourceCulture);
            }
        }
        
        internal static string NoConnectionConfigurationProvided {
            get {
                return ResourceManager.GetString("NoConnectionConfigurationProvided", resourceCulture);
            }
        }
        
        internal static string InvalidValueForParameter {
            get {
                return ResourceManager.GetString("InvalidValueForParameter", resourceCulture);
            }
        }
        
        internal static string CouldNotParseValue {
            get {
                return ResourceManager.GetString("CouldNotParseValue", resourceCulture);
            }
        }
    }
}
