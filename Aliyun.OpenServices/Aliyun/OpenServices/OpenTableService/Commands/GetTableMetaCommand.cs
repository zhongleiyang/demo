namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.OpenTableService;
    using Aliyun.OpenServices.OpenTableService.Utilities;
    using System;
    using System.Collections.Generic;
    using Aliyun.OpenServices.OpenStorageService.Model;
    using Aliyun.OpenServices.OpenTableService.Model;

    internal class GetTableMetaCommand : OtsCommand<GetTableMetaResult>
    {
        private string _tableName;
        public const string ActionName = "GetTableMeta";

        protected GetTableMetaCommand(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName) : base(client, endpoint, context)
        {
            this._tableName = tableName;
        }

        protected override void AddRequestParameters(IDictionary<string, string> parameters)
        {
            parameters.Add("TableName", this._tableName);
        }

        public static GetTableMetaCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context, string tableName)
        {
            if (string.IsNullOrEmpty(tableName) || !OtsUtility.IsEntityNameValid(tableName))
            {
                throw new ArgumentException(OtsExceptions.NameFormatIsInvalid, "tableName");
            }
            return new GetTableMetaCommand(client, endpoint, context, tableName);
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
                return "GetTableMeta";
            }
        }
    }
}

