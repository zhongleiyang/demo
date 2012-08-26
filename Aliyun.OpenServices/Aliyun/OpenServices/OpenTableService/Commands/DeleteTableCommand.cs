namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;

    internal class DeleteTableCommand : OtsCommand
    {
        private string _tableName;
        public const string ActionName = "DeleteTable";

        protected DeleteTableCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName) : base(client, endpoint, context)
        {
            this._tableName = tableName;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", this._tableName);
        }

        public static DeleteTableCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName)
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            return new DeleteTableCommand(client, endpoint, context, tableName);
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
                return "DeleteTable";
            }
        }
    }
}

