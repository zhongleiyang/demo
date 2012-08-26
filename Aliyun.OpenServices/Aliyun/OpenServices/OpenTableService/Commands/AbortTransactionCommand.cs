namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Collections.Generic;

    internal class AbortTransactionCommand : OtsCommand
    {
        private string _transactionId;
        public const string ActionName = "AbortTransaction";

        protected AbortTransactionCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string transactionId) : base(client, endpoint, context)
        {
            if (string.IsNullOrEmpty(transactionId))
            {
                throw new ArgumentException(Resources.ExceptionIfArgumentStringIsNullOrEmpty, "transactionId");
            }
            this._transactionId = transactionId;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TransactionID", this._transactionId);
        }

        public static AbortTransactionCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string transactionId)
        {
            return new AbortTransactionCommand(client, endpoint, context, transactionId);
        }

        protected override HttpMethod Method
        {
            get
            {
                return HttpMethod.Get;
            }
        }

        protected override string ResourcePath
        {
            get
            {
                return "AbortTransaction";
            }
        }
    }
}

