namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RangeRowQueryCriteria : RowQueryCriteria
    {
        private PrimaryKeyDictionary _primaryKeyValues;
        private PrimaryKeyRange _range;
        private int _top;

        public RangeRowQueryCriteria(string tableName) : this(tableName, null)
        {
        }

        public RangeRowQueryCriteria(string tableName, string viewName) : this(tableName, viewName, null, null)
        {
        }

        public RangeRowQueryCriteria(string tableName, IDictionary<string, PrimaryKeyValue> primaryKeys, IEnumerable<string> columnNames) : this(tableName, null, primaryKeys, columnNames)
        {
        }

        public RangeRowQueryCriteria(string tableName, string viewName, IDictionary<string, PrimaryKeyValue> primaryKeys, IEnumerable<string> columnNames) : base(tableName, viewName, columnNames)
        {
            this._primaryKeyValues = new PrimaryKeyDictionary();
            this._top = -1;
            if (primaryKeys != null)
            {
                foreach (KeyValuePair<string, PrimaryKeyValue> pk in primaryKeys)
                {
                    this.PrimaryKeys.Add(pk.Key, pk.Value);
                }
            }
        }

        public bool IsReverse { get; set; }

        public IDictionary<string, PrimaryKeyValue> PrimaryKeys
        {
            get
            {
                return this._primaryKeyValues;
            }
        }

        public PrimaryKeyRange Range
        {
            get
            {
                return this._range;
            }
            set
            {
                if (value == null)
                {
                    throw new ArgumentNullException("value");
                }
                this._range = value;
            }
        }

        public int Top
        {
            get
            {
                return this._top;
            }
            set
            {
                this._top = value;
            }
        }
    }
}

