namespace Aliyun.OpenServices.Common.Communication
{
    using System;
    using System.Net;

    internal abstract class ServiceResponse : ServiceMessage, IDisposable
    {
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
        }

        public virtual void EnsureSuccessful()
        {
            if (!this.IsSuccessful())
            {
                if (this.Content != null)
                {
                    this.Content.Dispose();
                }
                throw this.Failure;
            }
        }

        public virtual bool IsSuccessful()
        {
            return (((int)this.StatusCode / (int)HttpStatusCode.Continue) == 2);
        }

        public abstract Exception Failure { get; }

        public abstract HttpStatusCode StatusCode { get; }
    }
}

