namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;

    public class SingleRowQueryCriteria : RowQueryCriteria
    {
        private PrimaryKeyDictionary _primaryKeys;

        public SingleRowQueryCriteria(string tableName) : this(tableName, null)
        {
        }

        public SingleRowQueryCriteria(string tableName, string viewName) : this(tableName, viewName, null, null)
        {
        }

        public SingleRowQueryCriteria(string tableName, IDictionary<string, PrimaryKeyValue> primaryKeys, IEnumerable<string> columnNames) : this(tableName, null, primaryKeys, columnNames)
        {
        }

        public SingleRowQueryCriteria(string tableName, string viewName, IDictionary<string, PrimaryKeyValue> primaryKeys, IEnumerable<string> columnNames) : base(tableName, viewName, columnNames)
        {
            this._primaryKeys = new PrimaryKeyDictionary();
            if (primaryKeys != null)
            {
                foreach (KeyValuePair<string, PrimaryKeyValue> pk in primaryKeys)
                {
                    this.PrimaryKeys.Add(pk.Key, pk.Value);
                }
            }
        }

        public IDictionary<string, PrimaryKeyValue> PrimaryKeys
        {
            get
            {
                return this._primaryKeys;
            }
        }
    }
}

