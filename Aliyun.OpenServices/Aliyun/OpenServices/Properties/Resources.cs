namespace Aliyun.OpenServices.Properties
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode, CompilerGenerated]
    internal class Resources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal Resources()
        {
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static CultureInfo Culture
        {
            get
            {
                return resourceCulture;
            }
            set
            {
                resourceCulture = value;
            }
        }

        internal static string ExceptionEndOperationHasBeenCalled
        {
            get
            {
                return ResourceManager.GetString("ExceptionEndOperationHasBeenCalled", resourceCulture);
            }
        }

        internal static string ExceptionIfArgumentStringIsNullOrEmpty
        {
            get
            {
                return ResourceManager.GetString("ExceptionIfArgumentStringIsNullOrEmpty", resourceCulture);
            }
        }

        internal static string ExceptionInvalidResponse
        {
            get
            {
                return ResourceManager.GetString("ExceptionInvalidResponse", resourceCulture);
            }
        }

        internal static string ExceptionUnknowError
        {
            get
            {
                return ResourceManager.GetString("ExceptionUnknowError", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Aliyun.OpenServices.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
    }
}

