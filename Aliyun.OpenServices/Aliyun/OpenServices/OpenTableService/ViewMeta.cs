namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class ViewMeta
    {
        private EntityDictionary<ColumnType> _columns;
        private EntityDictionary<PrimaryKeyType> _primaryKeys;
        private string _viewName;

        private ViewMeta()
        {
            this._primaryKeys = new EntityDictionary<PrimaryKeyType>();
            this._columns = new EntityDictionary<ColumnType>();
        }

        public ViewMeta(string viewName) : this()
        {
            if (string.IsNullOrEmpty(viewName) || !OtsUtility.IsEntityNameValid(viewName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "viewName");
            }
            this.ViewName = viewName;
        }

        public IDictionary<string, ColumnType> AttributeColumns
        {
            get
            {
                return this._columns;
            }
        }

        public int PagingKeyLength { get; set; }

        public IDictionary<string, PrimaryKeyType> PrimaryKeys
        {
            get
            {
                return this._primaryKeys;
            }
        }

        public string ViewName
        {
            get
            {
                return this._viewName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || !OtsUtility.IsEntityNameValid(value))
                {
                    throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "value");
                }
                this._viewName = value;
            }
        }
    }
}

