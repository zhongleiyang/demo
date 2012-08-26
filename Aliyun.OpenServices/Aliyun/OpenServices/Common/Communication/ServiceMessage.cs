namespace Aliyun.OpenServices.Common.Communication
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.CompilerServices;

    internal class ServiceMessage
    {
        private IDictionary<string, string> _headers = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);

        public virtual Stream Content { get; set; }

        public virtual IDictionary<string, string> Headers
        {
            get
            {
                return this._headers;
            }
        }
    }
}

