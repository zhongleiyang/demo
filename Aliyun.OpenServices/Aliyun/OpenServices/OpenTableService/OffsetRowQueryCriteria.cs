namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class OffsetRowQueryCriteria : RowQueryCriteria
    {
        private int _offset;
        private PrimaryKeyDictionary _pagingKeys;
        private int _top;

        public OffsetRowQueryCriteria(string tableName) : this(tableName, null)
        {
        }

        public OffsetRowQueryCriteria(string tableName, string viewName) : this(tableName, viewName, null, null)
        {
        }

        public OffsetRowQueryCriteria(string tableName, IDictionary<string, PrimaryKeyValue> pagingKeys, IEnumerable<string> columnNames) : this(tableName, null, pagingKeys, columnNames)
        {
        }

        public OffsetRowQueryCriteria(string tableName, string viewName, IDictionary<string, PrimaryKeyValue> pagingKeys, IEnumerable<string> columnNames) : base(tableName, viewName, columnNames)
        {
            this._top = -1;
            this._pagingKeys = new PrimaryKeyDictionary();
            if (pagingKeys != null)
            {
                foreach (KeyValuePair<string, PrimaryKeyValue> pk in pagingKeys)
                {
                    this.PagingKeys.Add(pk.Key, pk.Value);
                }
            }
        }

        public bool IsReverse { get; set; }

        internal bool isTopSet
        {
            get
            {
                return (this._top >= 0);
            }
        }

        public int Offset
        {
            get
            {
                return this._offset;
            }
            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", OtsExceptions.QueryTopValueOutOfRange);
                }
                this._offset = value;
            }
        }

        public IDictionary<string, PrimaryKeyValue> PagingKeys
        {
            get
            {
                return this._pagingKeys;
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
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException("value", OtsExceptions.QueryTopValueOutOfRange);
                }
                this._top = value;
            }
        }
    }
}

