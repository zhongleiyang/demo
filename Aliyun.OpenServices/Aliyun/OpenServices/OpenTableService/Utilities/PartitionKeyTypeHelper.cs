namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using Aliyun.OpenServices.OpenTableService;
    using System;
    using System.Collections.Generic;

    internal static class PartitionKeyTypeHelper
    {
        private static EnumTypeStringMap<PartitionKeyType> _innerMap;
        private static object _lockObj = new object();

        private static void EnsureMapCreated()
        {
            if (_innerMap == null)
            {
                Dictionary<PartitionKeyType, string> temp = new Dictionary<PartitionKeyType, string>();
                temp.Add(PartitionKeyType.String, "STRING");
                temp.Add(PartitionKeyType.Integer, "INTEGER");
                Dictionary<PartitionKeyType, string> map = temp;
                EnumTypeStringMap<PartitionKeyType> innerMap = new EnumTypeStringMap<PartitionKeyType>(map);
                lock (_lockObj)
                {
                    if (_innerMap == null)
                    {
                        _innerMap = innerMap;
                    }
                }
            }
        }

        public static string GetString(PartitionKeyType pkType)
        {
            EnsureMapCreated();
            return _innerMap.GetEnumString(pkType);
        }

        public static PartitionKeyType Parse(string value)
        {
            EnsureMapCreated();
            return _innerMap.GetEnumType(value);
        }
    }
}

