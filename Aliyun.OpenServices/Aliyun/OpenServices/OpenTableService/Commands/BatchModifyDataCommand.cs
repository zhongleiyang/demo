namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    internal class BatchModifyDataCommand : OtsCommand
    {
        private IEnumerable<RowChange> _rowChanges;
        private string _tableName;
        private string _transactionId;
        public const string ActionName = "BatchModifyData";

        protected BatchModifyDataCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName, IEnumerable<RowChange> rowChanges, string transactionId) : base(client, endpoint, context)
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "transactionId");
            }
            this._tableName = tableName;
            this._rowChanges = rowChanges;
            this._transactionId = transactionId;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", this._tableName);
            int itemIndex = 1;
            foreach (RowChange rowChange in this._rowChanges)
            {
                string pre = string.Format(CultureInfo.InvariantCulture, "Modify.{0}.", new object[] { itemIndex++ });
                parameters.Add(pre + "Type", rowChange.ModifyType.ToString().ToUpperInvariant());
                int pkIndex = 1;
                foreach (KeyValuePair<string, PrimaryKeyValue> pk in rowChange.PrimaryKeys)
                {
                    string pkPre = string.Format(CultureInfo.InvariantCulture, "{0}PK.{1}.", new object[] { pre, pkIndex++ });
                    parameters.Add(pkPre + "Name", pk.Key);
                    parameters.Add(pkPre + "Value", pk.Value.ToParameterString());
                    parameters.Add(pkPre + "Type", PrimaryKeyTypeHelper.GetString(pk.Value.ValueType));
                }
                int colIndex = 1;
                RowPutChange rowPutChange = rowChange as RowPutChange;
                if (rowPutChange != null)
                {
                    foreach (KeyValuePair<string, ColumnValue> col in rowPutChange.AttributeColumns)
                    {
                        string colPre = string.Format(CultureInfo.InvariantCulture, "{0}Column.{1}.", new object[] { pre, colIndex++ });
                        parameters.Add(colPre + "Name", col.Key);
                        parameters.Add(colPre + "Value", col.Value.ToParameterString());
                        parameters.Add(colPre + "Type", ColumnTypeHelper.GetString(col.Value.ValueType));
                    }
                    parameters.Add(pre + "Checking", rowPutChange.CheckingMode.ToString().ToUpperInvariant());
                }
                else
                {
                    RowDeleteChange rowDelChange = rowChange as RowDeleteChange;
                    foreach (string col in rowDelChange.ColumnNames)
                    {
                        string colName = string.Format(CultureInfo.InvariantCulture, "{0}Column.{1}.Name", new object[] { pre, colIndex++ });
                        parameters.Add(colName, col);
                    }
                }
            }
            parameters.Add("TransactionID", this._transactionId);
        }

        public static BatchModifyDataCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName, IEnumerable<RowChange> rowChanges, string transactionId)
        {
            ValidateRowChanges(rowChanges);
            return new BatchModifyDataCommand(client, endpoint, context, tableName, rowChanges, transactionId);
        }

        private static void ValidateRowChanges(IEnumerable<RowChange> rowChanges)
        {
            if (rowChanges == null)
            {
                throw new ArgumentNullException("rowChanges");
            }
            List<RowChange> rowChangeList = rowChanges.ToList<RowChange>();
            if (rowChangeList.Count == 0)
            {
                throw new ArgumentException(OtsExceptions.NoRowForBatchModifyData, "rowChanges");
            }
            foreach (RowChange r in rowChangeList)
            {
                if (r.PrimaryKeys.Count == 0)
                {
                    throw new ArgumentException(OtsExceptions.NoPrimaryKeySpecified, "rowChanges");
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
                return "BatchModifyData";
            }
        }
    }
}

