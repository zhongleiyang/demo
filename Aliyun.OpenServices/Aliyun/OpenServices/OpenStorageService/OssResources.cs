namespace Aliyun.OpenServices.OpenStorageService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [CompilerGenerated, GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), DebuggerNonUserCode]
    internal class OssResources
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal OssResources()
        {
        }

        internal static string BucketNameInvalid
        {
            get
            {
                return ResourceManager.GetString("BucketNameInvalid", resourceCulture);
            }
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

        internal static string EndpointNotSupportedProtocal
        {
            get
            {
                return ResourceManager.GetString("EndpointNotSupportedProtocal", resourceCulture);
            }
        }

        internal static string ObjectKeyInvalid
        {
            get
            {
                return ResourceManager.GetString("ObjectKeyInvalid", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Aliyun.OpenServices.OpenStorageService.OssResources", typeof(OssResources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
    }
}

