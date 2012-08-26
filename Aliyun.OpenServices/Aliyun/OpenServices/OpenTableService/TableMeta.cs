namespace Aliyun.OpenServices.OpenTableService
{
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class TableMeta
    {
        private EntityDictionary<PrimaryKeyType> _primaryKeys;
        private string _tableGroupName;
        private string _tableName;

        private TableMeta()
        {
            this._primaryKeys = new EntityDictionary<PrimaryKeyType>();
            this.Views = new List<ViewMeta>();
        }

        public TableMeta(string tableName) : this()
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            this.TableName = tableName;
        }

        public TableMeta(string tableName, IDictionary<string, PrimaryKeyType> primaryKeys)
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            if (primaryKeys == null)
            {
                throw new ArgumentNullException("primaryKeys");
            }
            this.TableName = tableName;
            this._primaryKeys = new EntityDictionary<PrimaryKeyType>(primaryKeys);
            this.Views = new List<ViewMeta>();
        }

        public int PagingKeyLength { get; set; }

        public IDictionary<string, PrimaryKeyType> PrimaryKeys
        {
            get
            {
                return this._primaryKeys;
            }
        }

        public string TableGroupName
        {
            get
            {
                return this._tableGroupName;
            }
            set
            {
                if (!string.IsNullOrEmpty(value) && !OtsUtility.IsEntityNameValid(value))
                {
                    throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "value");
                }
                this._tableGroupName = value;
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

        public IList<ViewMeta> Views { get; private set; }
    }
}

