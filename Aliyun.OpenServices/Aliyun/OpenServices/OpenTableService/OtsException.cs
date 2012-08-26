namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices;
    using System;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class OtsException : ServiceException
    {
        public OtsException()
        {
        }

        public OtsException(string message) : base(message)
        {
        }

        protected OtsException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public OtsException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }
    }
}

