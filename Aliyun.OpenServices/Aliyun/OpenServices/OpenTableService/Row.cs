namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class Row : IEnumerable<KeyValuePair<string, ColumnValue>>, IEnumerable
    {
        private EntityDictionary<ColumnValue> _columns = new EntityDictionary<ColumnValue>();

        public IEnumerator<KeyValuePair<string, ColumnValue>> GetEnumerator()
        {
            return this.Columns.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

        public IDictionary<string, ColumnValue> Columns
        {
            get
            {
                return this._columns;
            }
        }
    }
}

