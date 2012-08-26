namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;

    internal class DeleteDataCommand : OtsCommand
    {
        private RowDeleteChange _rowChange;
        private string _tableName;
        private string _transactionId;
        public const string ActionName = "DeleteData";

        protected DeleteDataCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName, RowDeleteChange rowChange, string transactionId) : base(client, endpoint, context)
        {
            this._tableName = tableName;
            this._rowChange = rowChange;
            this._transactionId = transactionId;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", this._tableName);
            int pkIndex = 1;
            foreach (KeyValuePair<string, PrimaryKeyValue> pk in this._rowChange.PrimaryKeys)
            {
                string pre = string.Format(CultureInfo.InvariantCulture, "PK.{0}.", new object[] { pkIndex++ });
                parameters.Add(pre + "Name", pk.Key);
                parameters.Add(pre + "Value", pk.Value.ToParameterString());
                parameters.Add(pre + "Type", PrimaryKeyTypeHelper.GetString(pk.Value.ValueType));
            }
            int colIndex = 1;
            foreach (string col in this._rowChange.ColumnNames)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "Column.{0}.Name", new object[] { colIndex++ }), col);
            }
            if (!string.IsNullOrEmpty(this._transactionId))
            {
                parameters.Add("TransactionID", this._transactionId);
            }
        }

        public static DeleteDataCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName, RowDeleteChange rowChange, string transactionId)
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            ValidateRowChange(rowChange);
            return new DeleteDataCommand(client, endpoint, context, tableName, rowChange, transactionId);
        }

        private static void ValidateRowChange(RowDeleteChange rowChange)
        {
            if (rowChange == null)
            {
                throw new ArgumentNullException("rowChange");
            }
            if (rowChange.PrimaryKeys.Count == 0)
            {
                throw new ArgumentException(OtsExceptions.NoPrimaryKeySpecified, "rowChange");
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
                return "DeleteData";
            }
        }
    }
}

