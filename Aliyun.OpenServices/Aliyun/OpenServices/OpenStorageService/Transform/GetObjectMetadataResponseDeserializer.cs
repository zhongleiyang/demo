namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Utilities;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class GetObjectMetadataResponseDeserializer : ResponseDeserializer<ObjectMetadata, ObjectMetadata>
    {
        public GetObjectMetadataResponseDeserializer() : base(null)
        {
        }

        public override ObjectMetadata Deserialize(ServiceResponse response)
        {
            ObjectMetadata metadata = new ObjectMetadata();
            foreach (KeyValuePair<string, string> header in response.Headers)
            {
                if (header.Key.StartsWith("x-oss-meta-", false, CultureInfo.InvariantCulture))
                {
                    metadata.UserMetadata.Add(header.Key.Substring("x-oss-meta-".Length), header.Value);
                }
                else
                {
                    if (string.Equals(header.Key, "Content-Length", StringComparison.InvariantCultureIgnoreCase))
                    {
                        metadata.ContentLength = long.Parse(header.Value, CultureInfo.InvariantCulture);
                        continue;
                    }
                    if (string.Equals(header.Key, "ETag", StringComparison.InvariantCultureIgnoreCase))
                    {
                        metadata.ETag = OssUtils.TrimETag(header.Value);
                        continue;
                    }
                    if (string.Equals(header.Key, "Last-Modified", StringComparison.InvariantCultureIgnoreCase))
                    {
                        metadata.LastModified = DateUtils.ParseRfc822Date(header.Value);
                        continue;
                    }
                    metadata.AddHeader(header.Key, header.Value);
                }
            }
            return metadata;
        }
    }
}

