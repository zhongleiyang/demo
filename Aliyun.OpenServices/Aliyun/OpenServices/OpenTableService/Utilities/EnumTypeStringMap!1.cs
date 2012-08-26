namespace Aliyun.OpenServices.OpenTableService.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Reflection;

    internal class EnumTypeStringMap<T>
    {
        private IDictionary<string, T> _stringToTypeMap;
        private IDictionary<T, string> _typeToStringMap;

        public EnumTypeStringMap(IDictionary<T, string> typeToStringMap)
        {
            this._typeToStringMap = typeToStringMap;
            this._stringToTypeMap = new Dictionary<string, T>();
            foreach (KeyValuePair<T, string> pair in typeToStringMap)
            {
                this._stringToTypeMap.Add(pair.Value, pair.Key);
            }
        }

        [Conditional("DEBUG"), DebuggerNonUserCode]
        private void CheckMap()
        {
            FieldInfo[] temp = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static);
            for (int temp1 = 0; temp1 < temp.Length; temp1++)
            {
                FieldInfo info1 = temp[temp1];
            }
        }

        public string GetEnumString(T value)
        {
            return this._typeToStringMap[value];
        }

        public T GetEnumType(string value)
        {
            if (this._stringToTypeMap.Keys.Contains(value))
            {
                return this._stringToTypeMap[value];
            }
            return default(T);
        }
    }
}

