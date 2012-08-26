namespace Aliyun.OpenServices.OpenTableService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using System;
    using Aliyun.OpenServices.OpenTableService.Model;
    internal class ListTableGroupCommand : OtsCommand<ListTableGroupResult>
    {
        public const string ActionName = "ListTableGroup";

        protected ListTableGroupCommand(IServiceClient client, Uri endpoint, ExecutionContext context) : base(client, endpoint, context)
        {
        }

        public static ListTableGroupCommand Create(IServiceClient client, Uri endpoint, ExecutionContext context)
        {
            return new ListTableGroupCommand(client, endpoint, context);
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
                return "ListTableGroup";
            }
        }
    }
}

