namespace Aliyun.OpenServices.OpenTableService
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Globalization;
    using System.Resources;
    using System.Runtime.CompilerServices;

    [GeneratedCode("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0"), CompilerGenerated, DebuggerNonUserCode]
    internal class OtsExceptions
    {
        private static CultureInfo resourceCulture;
        private static System.Resources.ResourceManager resourceMan;

        internal OtsExceptions()
        {
        }

        internal static string CannotCastDoubleNaN
        {
            get
            {
                return ResourceManager.GetString("CannotCastDoubleNaN", resourceCulture);
            }
        }

        internal static string ColumnValueCannotBeDecoded
        {
            get
            {
                return ResourceManager.GetString("ColumnValueCannotBeDecoded", resourceCulture);
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

        internal static string InvalidPagingKeyLength
        {
            get
            {
                return ResourceManager.GetString("InvalidPagingKeyLength", resourceCulture);
            }
        }

        internal static string InvalidResponseMessage
        {
            get
            {
                return ResourceManager.GetString("InvalidResponseMessage", resourceCulture);
            }
        }

        internal static string NameFormatIsInvalid
        {
            get
            {
                return ResourceManager.GetString("NameFormatIsInvalid", resourceCulture);
            }
        }

        internal static string NoPrimaryKeySpecified
        {
            get
            {
                return ResourceManager.GetString("NoPrimaryKeySpecified", resourceCulture);
            }
        }

        internal static string NoRowForBatchModifyData
        {
            get
            {
                return ResourceManager.GetString("NoRowForBatchModifyData", resourceCulture);
            }
        }

        internal static string OpenTableServiceExceptionMessageFormat
        {
            get
            {
                return ResourceManager.GetString("OpenTableServiceExceptionMessageFormat", resourceCulture);
            }
        }

        internal static string OpenTableServiceInvalidResponseMessageFormat
        {
            get
            {
                return ResourceManager.GetString("OpenTableServiceInvalidResponseMessageFormat", resourceCulture);
            }
        }

        internal static string PKInfNotAllowed
        {
            get
            {
                return ResourceManager.GetString("PKInfNotAllowed", resourceCulture);
            }
        }

        internal static string PrimaryKeyValueIsNullOrEmpty
        {
            get
            {
                return ResourceManager.GetString("PrimaryKeyValueIsNullOrEmpty", resourceCulture);
            }
        }

        internal static string QueryTopValueNotSet
        {
            get
            {
                return ResourceManager.GetString("QueryTopValueNotSet", resourceCulture);
            }
        }

        internal static string QueryTopValueOutOfRange
        {
            get
            {
                return ResourceManager.GetString("QueryTopValueOutOfRange", resourceCulture);
            }
        }

        internal static string RangeBeginGreaterThanRangeEnd
        {
            get
            {
                return ResourceManager.GetString("RangeBeginGreaterThanRangeEnd", resourceCulture);
            }
        }

        internal static string RangeNotSet
        {
            get
            {
                return ResourceManager.GetString("RangeNotSet", resourceCulture);
            }
        }

        internal static string RangeTypeNotMatch
        {
            get
            {
                return ResourceManager.GetString("RangeTypeNotMatch", resourceCulture);
            }
        }

        [EditorBrowsable(EditorBrowsableState.Advanced)]
        internal static System.Resources.ResourceManager ResourceManager
        {
            get
            {
                if (object.ReferenceEquals(resourceMan, null))
                {
                    System.Resources.ResourceManager temp = new System.Resources.ResourceManager("Aliyun.OpenServices.OpenTableService.OtsExceptions", typeof(OtsExceptions).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }

        internal static string ResponseDataIncomplete
        {
            get
            {
                return ResourceManager.GetString("ResponseDataIncomplete", resourceCulture);
            }
        }

        internal static string ResponseDoesNotContainHeader
        {
            get
            {
                return ResourceManager.GetString("ResponseDoesNotContainHeader", resourceCulture);
            }
        }

        internal static string ResponseExpired
        {
            get
            {
                return ResourceManager.GetString("ResponseExpired", resourceCulture);
            }
        }

        internal static string ResponseFailedAuthorization
        {
            get
            {
                return ResourceManager.GetString("ResponseFailedAuthorization", resourceCulture);
            }
        }

        internal static string UnsupportedEncodingFormat
        {
            get
            {
                return ResourceManager.GetString("UnsupportedEncodingFormat", resourceCulture);
            }
        }

        internal static string ValueCastFailedFormat
        {
            get
            {
                return ResourceManager.GetString("ValueCastFailedFormat", resourceCulture);
            }
        }

        internal static string ValueCastInvalidTypeFormat
        {
            get
            {
                return ResourceManager.GetString("ValueCastInvalidTypeFormat", resourceCulture);
            }
        }
    }
}

