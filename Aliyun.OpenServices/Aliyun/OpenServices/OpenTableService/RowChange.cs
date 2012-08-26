namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class RowChange
    {
        protected RowChange()
        {
            this.PrimaryKeys = new PrimaryKeyDictionary();
        }

        protected RowChange(IDictionary<string, PrimaryKeyValue> primaryKeys) : this()
        {
            if (primaryKeys == null)
            {
                throw new ArgumentNullException("primaryKeys");
            }
            foreach (KeyValuePair<string, PrimaryKeyValue> pk in primaryKeys)
            {
                this.PrimaryKeys.Add(pk);
            }
        }

        internal abstract string ModifyType { get; }

        public IDictionary<string, PrimaryKeyValue> PrimaryKeys { get; private set; }
    }
}

