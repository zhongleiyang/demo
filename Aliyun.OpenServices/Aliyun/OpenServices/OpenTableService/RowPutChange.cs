namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RowPutChange : RowChange
    {
        public RowPutChange()
        {
            this.AttributeColumns = new EntityDictionary<ColumnValue>();
        }

        public RowPutChange(IDictionary<string, PrimaryKeyValue> primaryKeys) : base(primaryKeys)
        {
            this.AttributeColumns = new EntityDictionary<ColumnValue>();
        }

        public RowPutChange(IDictionary<string, PrimaryKeyValue> primaryKeys, IDictionary<string, ColumnValue> columns) : base(primaryKeys)
        {
            if (columns == null)
            {
                throw new ArgumentNullException("columns");
            }
            this.AttributeColumns = new EntityDictionary<ColumnValue>(columns);
        }

        public IDictionary<string, ColumnValue> AttributeColumns { get; private set; }

        public Aliyun.OpenServices.OpenTableService.CheckingMode CheckingMode { get; set; }

        internal override string ModifyType
        {
            get
            {
                return "PUT";
            }
        }
    }
}

