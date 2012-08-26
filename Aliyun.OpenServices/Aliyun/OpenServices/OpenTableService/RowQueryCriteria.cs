namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;

    public abstract class RowQueryCriteria
    {
        private IList<string> _columns;
        private string _tableName;
        private string _viewName;

        protected RowQueryCriteria(string tableName) : this(tableName, (string) null)
        {
        }

        protected RowQueryCriteria(string tableName, IEnumerable<string> columnNames) : this(tableName, null, columnNames)
        {
        }

        protected RowQueryCriteria(string tableName, string viewName) : this(tableName, viewName, null)
        {
            this._tableName = tableName;
            this._viewName = viewName;
        }

        protected RowQueryCriteria(string tableName, string viewName, IEnumerable<string> columnNames)
        {
            this._columns = new EntityNameList();
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            if (!string.IsNullOrEmpty(viewName) && !OtsUtility.IsEntityNameValid(viewName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "viewName");
            }
            this._tableName = tableName;
            this._viewName = viewName;
            if (columnNames != null)
            {
                foreach (string col in columnNames)
                {
                    this.ColumnNames.Add(col);
                }
            }
        }

        public IList<string> ColumnNames
        {
            get
            {
                return this._columns;
            }
        }

        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                if (string.IsNullOrEmpty(value) || !OtsUtility.IsEntityNameValid(value))
                {
                    throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "value");
                }
                this._tableName = value;
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
                if (!OtsUtility.IsEntityNameValid(value))
                {
                    throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "value");
                }
                this._viewName = value;
            }
        }
    }
}

