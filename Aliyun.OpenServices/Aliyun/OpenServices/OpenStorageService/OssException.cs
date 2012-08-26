namespace Aliyun.OpenServices.OpenStorageService
{
    using Aliyun.OpenServices;
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class OssException : ServiceException
    {
        public OssException()
        {
        }

        public OssException(string message) : base(message)
        {
        }

        protected OssException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public OssException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

