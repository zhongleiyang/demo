namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using Aliyun.OpenServices.OpenTableService.Model;

    internal class StartTransactionCommand : OtsCommand<StartTransactionResult>
    {
        private string _entityName;
        private PartitionKeyValue _partitionKeyValue;
        public const string ActionName = "StartTransaction";

        protected StartTransactionCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string entityName, PartitionKeyValue partitionKeyValue) : base(client, endpoint, context)
        {
            this._entityName = entityName;
            this._partitionKeyValue = partitionKeyValue;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("EntityName", this._entityName);
            parameters.Add("PartitionKeyValue", this._partitionKeyValue.ToParameterString());
            parameters.Add("PartitionKeyType", PartitionKeyTypeHelper.GetString(this._partitionKeyValue.ValueType));
        }

        public static StartTransactionCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string entityName, PartitionKeyValue partitionKeyValue)
        {
            if (string.IsNullOrEmpty(entityName) || !OtsUtility.IsEntityNameValid(entityName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "entityName");
            }
            return new StartTransactionCommand(client, endpoint, context, entityName, partitionKeyValue);
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
                return "StartTransaction";
            }
        }
    }
}

