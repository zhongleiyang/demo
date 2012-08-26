namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using System;
    using Aliyun.OpenServices.OpenTableService.Model;

    internal class ListTableCommand : OtsCommand<ListTableResult>
    {
        public const string ActionName = "ListTable";

        protected ListTableCommand(IServiceClient client, Uri endpoint, ExecutionContext context) : base(client, endpoint, context)
        {
        }

        public static ListTableCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context)
        {
            return new ListTableCommand(client, endpoint, context);
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
                return "ListTable";
            }
        }
    }
}

