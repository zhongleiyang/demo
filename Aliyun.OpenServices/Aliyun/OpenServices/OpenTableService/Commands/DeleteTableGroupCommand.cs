namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;

    internal class DeleteTableGroupCommand : OtsCommand
    {
        private string _tableGroupName;
        public const string ActionName = "DeleteTableGroup";

        protected DeleteTableGroupCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string tableGroupName) : base(client, endpoint, context)
        {
            this._tableGroupName = tableGroupName;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableGroupName", this._tableGroupName);
        }

        public static DeleteTableGroupCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string tableGroupName)
        {
            if (string.IsNullOrEmpty(tableGroupName) || !OtsUtility.IsEntityNameValid(tableGroupName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableGroupName");
            }
            return new DeleteTableGroupCommand(client, endpoint, context, tableGroupName);
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
                return "DeleteTableGroup";
            }
        }
    }
}

