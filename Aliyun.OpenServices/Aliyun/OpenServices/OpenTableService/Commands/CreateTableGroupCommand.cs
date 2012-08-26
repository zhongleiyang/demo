namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;

    internal class CreateTableGroupCommand : OtsCommand
    {
        private PartitionKeyType _partitionKeyType;
        private string _tableGroupName;
        public const string ActionName = "CreateTableGroup";

        protected CreateTableGroupCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string tableGroupName, PartitionKeyType partitionKeyType) : base(client, endpoint, context)
        {
            if (string.IsNullOrEmpty(tableGroupName) || !OtsUtility.IsEntityNameValid(tableGroupName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableGroupName");
            }
            this._tableGroupName = tableGroupName;
            this._partitionKeyType = partitionKeyType;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableGroupName", this._tableGroupName);
            parameters.Add("PartitionKeyType", PartitionKeyTypeHelper.GetString(this._partitionKeyType));
        }

        public static CreateTableGroupCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string tableGroupName, PartitionKeyType partitionKeyType)
        {
            return new CreateTableGroupCommand(client, endpoint, context, tableGroupName, partitionKeyType);
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
                return "CreateTableGroup";
            }
        }
    }
}

