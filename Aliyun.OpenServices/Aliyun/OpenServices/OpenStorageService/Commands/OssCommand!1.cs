namespace Aliyun.OpenServices.OpenStorageService.Commands
{
    using Aliyun.OpenServices.Common.Communication;
    using Aliyun.OpenServices.Common.Transform;
    using Aliyun.OpenServices.OpenStorageService.Utilities;
    using System;

    internal abstract class OssCommand<T> : OssCommand
    {
        private IDeserializer<ServiceResponse, T> _deserializer;

        public OssCommand(IServiceClient client, Uri endpoint, ExecutionContext context, IDeserializer<ServiceResponse, T> deserializer) : base(client, endpoint, context)
        {
            this._deserializer = deserializer;
        }

        public T Execute()
        {
            T temp;
            ServiceResponse response = base.Execute();
            try
            {
                temp = this._deserializer.Deserialize(response);
            }
            catch (ResponseDeserializationException ex)
            {
                throw ExceptionFactory.CreateInvalidResponseException(ex);
            }
            finally
            {
                if (!this.LeaveResponseOpen)
                {
                    response.Dispose();
                }
            }
            return temp;
        }

        protected virtual bool LeaveResponseOpen
        {
            get
            {
                return false;
            }
        }
    }
}

