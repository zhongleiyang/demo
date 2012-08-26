namespace Aliyun.OpenServices.OpenStorageService
{
    using System;
    using System.Globalization;
    using System.Runtime.CompilerServices;

    public class Bucket
    {
        public Bucket(string name)
        {
            this.Name = name;
        }

        public override string ToString()
        {
            return string.Format(CultureInfo.InvariantCulture, "OSS Bucket [Name={0}], [Owner={1}], [CreationTime={2}]", new object[] { this.Name, this.Owner, this.CreationDate });
        }

        public DateTime CreationDate { get; internal set; }

        public string Name { get; internal set; }

        public Aliyun.OpenServices.OpenStorageService.Owner Owner { get; internal set; }
    }
}

