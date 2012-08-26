namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class CreateTableCommand : OtsCommand
    {
        private TableMeta _tableMeta;
        public const string ActionName = "CreateTable";

        protected CreateTableCommand(IServiceClient client, Uri endpoint, ExecutionContext context, TableMeta tableMeta) : base(client, endpoint, context)
        {
            this._tableMeta = tableMeta;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", this._tableMeta.TableName);
            int pkIndex = 1;
            foreach (KeyValuePair<string, PrimaryKeyType> pk in this._tableMeta.PrimaryKeys)
            {
                string pre = "PK." + pkIndex.ToString(CultureInfo.InvariantCulture);
                parameters.Add(pre + ".Name", pk.Key);
                parameters.Add(pre + ".Type", PrimaryKeyTypeHelper.GetString(pk.Value));
                pkIndex++;
            }
            if (this._tableMeta.PagingKeyLength > 0)
            {
                parameters.Add("PagingKeyLen", this._tableMeta.PagingKeyLength.ToString(CultureInfo.InvariantCulture));
            }
            int viewIndex = 1;
            foreach (ViewMeta view in this._tableMeta.Views)
            {
                string viewPre = "View." + viewIndex.ToString(CultureInfo.InvariantCulture);
                parameters.Add(viewPre + ".Name", view.ViewName);
                int viewPkIndex = 1;
                foreach (KeyValuePair<string, PrimaryKeyType> pk in view.PrimaryKeys)
                {
                    string pre = viewPre + ".PK." + viewPkIndex.ToString(CultureInfo.InvariantCulture);
                    parameters.Add(pre + ".Name", pk.Key);
                    parameters.Add(pre + ".Type", PrimaryKeyTypeHelper.GetString(pk.Value));
                    viewPkIndex++;
                }
                int viewColIndex = 1;
                foreach (KeyValuePair<string, ColumnType> col in view.AttributeColumns)
                {
                    string pre = viewPre + ".Column." + viewColIndex.ToString(CultureInfo.InvariantCulture);
                    parameters.Add(pre + ".Name", col.Key);
                    parameters.Add(pre + ".Type", ColumnTypeHelper.GetString(col.Value));
                    viewColIndex++;
                }
                if (view.PagingKeyLength > 0)
                {
                    parameters.Add(viewPre + ".PagingKeyLen", view.PagingKeyLength.ToString(CultureInfo.InvariantCulture));
                }
                viewIndex++;
            }
            if (!string.IsNullOrEmpty(this._tableMeta.TableGroupName))
            {
                parameters.Add("TableGroupName", this._tableMeta.TableGroupName);
            }
        }

        public static CreateTableCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, TableMeta tableMeta)
        {
            ValidateTableMeta(tableMeta);
            return new CreateTableCommand(client, endpoint, context, tableMeta);
        }

        private static void ValidateTableMeta(TableMeta tableMeta)
        {
            if (tableMeta == null)
            {
                throw new ArgumentNullException("tableMeta");
            }
            if (string.IsNullOrEmpty(tableMeta.TableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableMeta");
            }
            if (tableMeta.PrimaryKeys.Count == 0)
            {
                throw new ArgumentException(OtsExceptions.NoPrimaryKeySpecified, "tableMeta");
            }
            if ((tableMeta.PagingKeyLength < 0) || (tableMeta.PagingKeyLength >= tableMeta.PrimaryKeys.Count))
            {
                throw new ArgumentException(OtsExceptions.InvalidPagingKeyLength, "tableMeta");
            }
            foreach (ViewMeta view in tableMeta.Views)
            {
                if ((view.PagingKeyLength < 0) || (view.PagingKeyLength >= view.PrimaryKeys.Count))
                {
                    throw new ArgumentException(OtsExceptions.InvalidPagingKeyLength, "tableMeta");
                }
            }
        }

        protected override HttpMethod Method
        {
            get
            {
                return HttpMethod.Post;
            }
        }

        protected override string ResourcePath
        {
            get
            {
                return "CreateTable";
            }
        }
    }
}

