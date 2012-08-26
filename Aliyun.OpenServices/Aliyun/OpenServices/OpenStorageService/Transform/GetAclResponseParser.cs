namespace Aliyun.OpenServices.OpenStorageService.Transform
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.Common.Utilities;
    using Aliyun.OpenServices.OpenStorageService;
    using Aliyun.OpenServices.OpenStorageService.Model;
    using System;
    using System.IO;

    internal class GetAclResponseParser : ResponseDeserializer<AccessControlList, AccessControlPolicy>
    {
        private IDeserializer<System.IO.Stream, AccessControlPolicy> deserializer;

        public GetAclResponseParser(IDeserializer<Stream, AccessControlPolicy> contentDeserializer) : base(contentDeserializer)
        {
        }

        //public GetAclResponseParser(IDeserializer<Stream, AccessControlPolicy> deserializer)
        //{
        //    // TODO: Complete member initialization
        //    this.deserializer = deserializer;
        //}

        public override AccessControlList Deserialize(ServiceResponse response)
        {
            AccessControlPolicy model = base.ContentDeserializer.Deserialize(response.Content);
            AccessControlList acl = new AccessControlList {
                Owner = new Owner(model.Owner.Id, model.Owner.DisplayName)
            };
            foreach (string grant in model.Grants)
            {
                if (grant == CannedAccessControlList.PublicRead.GetStringValue())
                {
                    acl.GrantPermission(GroupGrantee.AllUsers, Permission.Read);
                }
                else if (grant == CannedAccessControlList.PublicReadWrite.GetStringValue())
                {
                    acl.GrantPermission(GroupGrantee.AllUsers, Permission.FullControl);
                }
            }
            return acl;
        }
    }
}

