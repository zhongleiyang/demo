namespace Aliyun.OpenServices.Common.Authentication
{
    using Aliyun.OpenServices.Properties;
    using System;

    internal abstract class ServiceSignature
    {
        protected ServiceSignature()
        {
        }

        public string ComputeSignature(string key, string data)
        {
            if (string.IsNullOrEmpty(key))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "key");
            }
            if (string.IsNullOrEmpty(data))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "data");
            }
            return this.ComputeSignatureCore(key, data);
        }

        protected abstract string ComputeSignatureCore(string key, string data);
        public static ServiceSignature Create()
        {
            return new HmacSHA1Signature();
        }

        public abstract string SignatureMethod { get; }

        public abstract string SignatureVersion { get; }
    }
}

