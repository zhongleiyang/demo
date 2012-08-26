namespace Aliyun.OpenServices.Common.Transform
{
    using System;
    using System.Runtime.Serialization;

    [Serializable]
    internal class ResponseDeserializationException : InvalidOperationException, ISerializable
    {
        public ResponseDeserializationException()
        {
        }

        public ResponseDeserializationException(string message) : base(message)
        {
        }

        protected ResponseDeserializationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        public ResponseDeserializationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}

