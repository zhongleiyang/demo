namespace Aliyun.OpenServices
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;
    using System.Security.Permissions;

    [Serializable]
    public class ServiceException : Exception
    {
        public ServiceException()
        {
        }

        public ServiceException(string message) : base(message)
        {
        }

        protected ServiceException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ServiceException(string message, Exception innerException) : base(message, innerException)
        {
        }

        [SecurityPermission(SecurityAction.LinkDemand, Flags=SecurityPermissionFlag.SerializationFormatter)]
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            base.GetObjectData(info, context);
        }

        public virtual string ErrorCode { get; internal set; }

        public virtual string HostId { get; internal set; }

        public virtual string RequestId { get; internal set; }
    }
}

