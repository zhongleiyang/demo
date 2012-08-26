namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Aliyun.OpenServices.OpenTableService.Model;

    internal class GetRowsByOffsetCommand : OtsCommand<GetRowsByOffsetResult>
    {
        private OffsetRowQueryCriteria _criteria;
        private string _transactionId;
        public const string ActionName = "GetRowsByOffset";

        protected GetRowsByOffsetCommand(IServiceClient client, Uri endpoint, ExecutionContext context, OffsetRowQueryCriteria criteria, string transactionId) : base(client, endpoint, context)
        {
            this._criteria = criteria;
            this._transactionId = transactionId;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", OtsUtility.GetFullQueryTableName(this._criteria));
            int pkIndex = 1;
            foreach (KeyValuePair<string, PrimaryKeyValue> pk in this._criteria.PagingKeys)
            {
                string pkPre = string.Format(CultureInfo.InvariantCulture, "Paging.{0}.", new object[] { pkIndex++ });
                parameters.Add(pkPre + "Name", pk.Key);
                parameters.Add(pkPre + "Value", pk.Value.ToParameterString());
                parameters.Add(pkPre + "Type", PrimaryKeyTypeHelper.GetString(pk.Value.ValueType));
            }
            int colIndex = 1;
            foreach (string col in this._criteria.ColumnNames)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "Column.{0}.Name", new object[] { colIndex++ }), col);
            }
            parameters.Add("Offset", this._criteria.Offset.ToString(CultureInfo.InvariantCulture));
            parameters.Add("Top", this._criteria.Top.ToString(CultureInfo.InvariantCulture));
            if (this._criteria.IsReverse)
            {
                parameters.Add("IsReverse", this._criteria.IsReverse.ToString(CultureInfo.InvariantCulture).ToUpperInvariant());
            }
            if (!string.IsNullOrEmpty(this._transactionId))
            {
                parameters.Add("TransactionID", this._transactionId);
            }
        }

        public static GetRowsByOffsetCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, OffsetRowQueryCriteria criteria, string transactionId)
        {
            ValidateParameter(criteria);
            return new GetRowsByOffsetCommand(client, endpoint, context, criteria, transactionId);
        }

        private static void ValidateParameter(OffsetRowQueryCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }
            if (criteria.PagingKeys.Count == 0)
            {
                throw new ArgumentException(OtsExceptions.NoPrimaryKeySpecified, "criteria");
            }
            if (!criteria.isTopSet)
            {
                throw new ArgumentException(OtsExceptions.QueryTopValueNotSet, "criteria");
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
                return "GetRowsByOffset";
            }
        }
    }
}

