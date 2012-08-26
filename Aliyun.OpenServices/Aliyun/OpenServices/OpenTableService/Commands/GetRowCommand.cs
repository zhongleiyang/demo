namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Aliyun.OpenServices.OpenTableService.Model;

    internal class GetRowCommand : OtsCommand<GetRowResult>
    {
        private SingleRowQueryCriteria _criteria;
        private string _transactionId;
        public const string ActionName = "GetRow";

        protected GetRowCommand(IServiceClient client, Uri endpoint, ExecutionContext context, SingleRowQueryCriteria criteria, string transactionId) : base(client, endpoint, context)
        {
            this._criteria = criteria;
            this._transactionId = transactionId;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", OtsUtility.GetFullQueryTableName(this._criteria));
            int pkIndex = 1;
            foreach (KeyValuePair<string, PrimaryKeyValue> pk in this._criteria.PrimaryKeys)
            {
                string pkPre = string.Format(CultureInfo.InvariantCulture, "PK.{0}.", new object[] { pkIndex++ });
                parameters.Add(pkPre + "Name", pk.Key);
                parameters.Add(pkPre + "Value", pk.Value.ToParameterString());
                parameters.Add(pkPre + "Type", PrimaryKeyTypeHelper.GetString(pk.Value.ValueType));
            }
            int colIndex = 1;
            foreach (string col in this._criteria.ColumnNames)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "Column.{0}.Name", new object[] { colIndex++ }), col);
            }
            if (!string.IsNullOrEmpty(this._transactionId))
            {
                parameters.Add("TransactionID", this._transactionId);
            }
        }

        public static GetRowCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, SingleRowQueryCriteria criteria, string transactionId)
        {
            ValidateParameter(criteria);
            return new GetRowCommand(client, endpoint, context, criteria, transactionId);
        }

        private static void ValidateParameter(SingleRowQueryCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }
            if (criteria.PrimaryKeys.Count == 0)
            {
                throw new ArgumentException(OtsExceptions.NoPrimaryKeySpecified, "criteria");
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
                return "GetRow";
            }
        }
    }
}

