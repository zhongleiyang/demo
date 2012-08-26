namespace Aliyun.OpenServices.Common.Communication
{
    using System;
    using System.Net;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    internal static class HttpExtensions
    {
        private static MethodInfo _addInternalMethod;

        internal static void AddInternal(this WebHeaderCollection headers, string key, string value)
        {
            if (_addInternalMethod == null)
            {
                _addInternalMethod = typeof(WebHeaderCollection).GetMethod("AddInternal", BindingFlags.NonPublic | BindingFlags.Instance, null, new Type[] { typeof(string), typeof(string) }, null);
            }
            _addInternalMethod.Invoke(headers, new object[] { key, value });
        }
    }
}

