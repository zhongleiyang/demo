namespace Aliyun.OpenServices
{
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Runtime.CompilerServices;

    internal class ServiceCredentials
    {
        public ServiceCredentials(string accessId, string accessKey)
        {
            if (string.IsNullOrEmpty(accessId))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "accessId");
            }
            this.AccessId = accessId;
            this.AccessKey = accessKey;
        }

        public string AccessId { get; private set; }

        public string AccessKey { get; private set; }
    }
}

