namespace Aliyun.OpenServices.OpenStorageService
{
    using System;
    using System.Runtime.CompilerServices;

    public class Grant
    {
        public Grant(IGrantee grantee, Aliyun.OpenServices.OpenStorageService.Permission permission)
        {
            if (grantee == null)
            {
                throw new ArgumentNullException("grantee");
            }
            this.Grantee = grantee;
            this.Permission = permission;
        }

        public override bool Equals(object obj)
        {
            Grant g = obj as Grant;
            if (g == null)
            {
                return false;
            }
            return ((this.Grantee.Identifier == g.Grantee.Identifier) && (this.Permission == g.Permission));
        }

        public override int GetHashCode()
        {
            return (this.Grantee.Identifier + ":" + this.Permission.ToString()).GetHashCode();
        }

        public IGrantee Grantee { get; private set; }

        public Aliyun.OpenServices.OpenStorageService.Permission Permission { get; private set; }
    }
}

