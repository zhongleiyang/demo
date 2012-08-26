namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RowDeleteChange : RowChange
    {
        public RowDeleteChange()
        {
            this.InitializeColumns();
        }

        public RowDeleteChange(IDictionary<string, PrimaryKeyValue> primaryKeys) : base(primaryKeys)
        {
            this.InitializeColumns();
        }

        public RowDeleteChange(IDictionary<string, PrimaryKeyValue> primaryKeys, IEnumerable<string> columnNames) : this(primaryKeys)
        {
            if (columnNames == null)
            {
                throw new ArgumentNullException("columnNames");
            }
            this.ColumnNames = new EntityNameList(columnNames);
        }

        private void InitializeColumns()
        {
            this.ColumnNames = new EntityNameList();
        }

        public ICollection<string> ColumnNames { get; private set; }

        internal override string ModifyType
        {
            get
            {
                return "DELETE";
            }
        }
    }
}

