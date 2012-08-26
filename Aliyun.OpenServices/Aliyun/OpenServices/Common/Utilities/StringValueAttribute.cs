namespace Aliyun.OpenServices.Common.Utilities
{
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class StringValueAttribute : Attribute
    {
        public StringValueAttribute(string value)
        {
            this.Value = value;
        }

        public string Value { get; private set; }
    }
}

