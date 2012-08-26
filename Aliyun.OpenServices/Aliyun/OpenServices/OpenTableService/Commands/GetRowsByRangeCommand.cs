namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using Aliyun.OpenServices.OpenTableService.Model;

    internal class GetRowsByRangeCommand : OtsCommand<GetRowsByRangeResult>
    {
        private RangeRowQueryCriteria _criteria;
        private string _transactionId;
        public const string ActionName = "GetRowsByRange";

        protected GetRowsByRangeCommand(IServiceClient client, Uri endpoint, ExecutionContext context, RangeRowQueryCriteria criteria, string transactionId) : base(client, endpoint, context)
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
                string prefix = string.Format(CultureInfo.InvariantCulture, "PK.{0}.", new object[] { pkIndex++ });
                parameters.Add(prefix + "Name", pk.Key);
                parameters.Add(prefix + "Value", pk.Value.ToParameterString());
                parameters.Add(prefix + "Type", PrimaryKeyTypeHelper.GetString(pk.Value.ValueType));
            }
            string rangePrefix = string.Format(CultureInfo.InvariantCulture, "PK.{0}.", new object[] { pkIndex });
            parameters.Add(rangePrefix + "Name", this._criteria.Range.PrimaryKeyName);
            parameters.Add(rangePrefix + "RangeBegin", this._criteria.Range.RangeBegin.ToParameterString());
            parameters.Add(rangePrefix + "RangeEnd", this._criteria.Range.RangeEnd.ToParameterString());
            parameters.Add(rangePrefix + "RangeType", PrimaryKeyTypeHelper.GetString(this._criteria.Range.RangeBegin.ValueType));
            int colIndex = 1;
            foreach (string col in this._criteria.ColumnNames)
            {
                parameters.Add(string.Format(CultureInfo.InvariantCulture, "Column.{0}.Name", new object[] { colIndex++ }), col);
            }
            if (this._criteria.Top >= 0)
            {
                parameters.Add("Top", this._criteria.Top.ToString(CultureInfo.InvariantCulture));
            }
            if (this._criteria.IsReverse)
            {
                parameters.Add("IsReverse", this._criteria.IsReverse.ToString(CultureInfo.InvariantCulture).ToUpperInvariant());
            }
            if (!string.IsNullOrEmpty(this._transactionId))
            {
                parameters.Add("TransactionID", this._transactionId);
            }
        }

        public static GetRowsByRangeCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, RangeRowQueryCriteria criteria, string transactionId)
        {
            ValidateParameter(criteria);
            return new GetRowsByRangeCommand(client, endpoint, context, criteria, transactionId);
        }

        private static void ValidateParameter(RangeRowQueryCriteria criteria)
        {
            if (criteria == null)
            {
                throw new ArgumentNullException("criteria");
            }
            if (criteria.Range == null)
            {
                throw new ArgumentException(OtsExceptions.RangeNotSet, "criteria");
            }
            PrimaryKeyValue rangeBegin = criteria.Range.RangeBegin;
            PrimaryKeyValue rangeEnd = criteria.Range.RangeEnd;
            if ((!criteria.IsReverse && ((rangeBegin == PrimaryKeyRange.InfMax) || (rangeEnd == PrimaryKeyRange.InfMin))) || (criteria.IsReverse && ((rangeBegin == PrimaryKeyRange.InfMin) || (rangeEnd == PrimaryKeyRange.InfMax))))
            {
                throw new ArgumentException(OtsExceptions.PKInfNotAllowed);
            }
            if ((!criteria.IsReverse && (rangeBegin.CompareTo(rangeEnd) > 0)) || (criteria.IsReverse && (rangeBegin.CompareTo(rangeEnd) < 0)))
            {
                throw new ArgumentException(OtsExceptions.RangeBeginGreaterThanRangeEnd);
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
                return "GetRowsByRange";
            }
        }
    }
}

