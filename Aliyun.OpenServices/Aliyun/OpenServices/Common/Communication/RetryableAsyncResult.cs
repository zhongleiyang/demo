namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices.Common;
    using System;
    using System.Runtime.CompilerServices;

    internal class RetryableAsyncResult : AsyncResult<ServiceResponse>
    {
        public RetryableAsyncResult(AsyncCallback callback, object state, ServiceRequest request, ExecutionContext context) : base(callback, state)
        {
            this.Request = request;
            this.Context = context;
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing && (this.InnerAsyncResult != null))
            {
                this.InnerAsyncResult.Dispose();
                this.InnerAsyncResult = null;
            }
        }

        public ExecutionContext Context { get; private set; }

        public AsyncResult InnerAsyncResult { get; set; }

        public ServiceRequest Request { get; private set; }

        public int Retries { get; set; }
    }
}

