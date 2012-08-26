namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;

    internal static class PrimaryKeyTypeHelper
    {
        private static EnumTypeStringMap<PrimaryKeyType> _innerMap;
        private static object _lockObj = new object();

        private static void EnsureMapCreated()
        {
            if (_innerMap == null)
            {
                Dictionary<PrimaryKeyType, string> temp = new Dictionary<PrimaryKeyType, string>();
                temp.Add(PrimaryKeyType.String, "STRING");
                temp.Add(PrimaryKeyType.Integer, "INTEGER");
                temp.Add(PrimaryKeyType.Boolean, "BOOLEAN");
                Dictionary<PrimaryKeyType, string> map = temp;
                EnumTypeStringMap<PrimaryKeyType> innerMap = new EnumTypeStringMap<PrimaryKeyType>(map);
                lock (_lockObj)
                {
                    if (_innerMap == null)
                    {
                        _innerMap = innerMap;
                    }
                }
            }
        }

        public static string GetString(PrimaryKeyType pkType)
        {
            EnsureMapCreated();
            return _innerMap.GetEnumString(pkType);
        }

        public static PrimaryKeyType Parse(string value)
        {
            EnsureMapCreated();
            return _innerMap.GetEnumType(value);
        }
    }
}

