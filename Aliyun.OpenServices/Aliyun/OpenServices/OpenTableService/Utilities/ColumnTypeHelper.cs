namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;

    internal static class ColumnTypeHelper
    {
        private static EnumTypeStringMap<ColumnType> _innerMap;
        private static object _lockObj = new object();

        private static void EnsureMapCreated()
        {
            if (_innerMap == null)
            {
                Dictionary<ColumnType, string> temp = new Dictionary<ColumnType, string>();
                temp.Add(ColumnType.String, "STRING");
                temp.Add(ColumnType.Integer, "INTEGER");
                temp.Add(ColumnType.Boolean, "BOOLEAN");
                temp.Add(ColumnType.Double, "DOUBLE");
                Dictionary<ColumnType, string> map = temp;
                EnumTypeStringMap<ColumnType> innerMap = new EnumTypeStringMap<ColumnType>(map);
                lock (_lockObj)
                {
                    if (_innerMap == null)
                    {
                        _innerMap = innerMap;
                    }
                }
            }
        }

        public static string GetString(ColumnType pkType)
        {
            EnsureMapCreated();
            return _innerMap.GetEnumString(pkType);
        }

        public static ColumnType Parse(string value)
        {
            EnsureMapCreated();
            return _innerMap.GetEnumType(value);
        }
    }
}

