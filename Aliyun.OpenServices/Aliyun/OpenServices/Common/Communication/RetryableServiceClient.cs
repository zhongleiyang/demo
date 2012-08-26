namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices.Common;
    using Aliyun.OpenServices.Properties;
    using System;
    using System.Net;
    using System.Runtime.CompilerServices;
    using System.Threading;

    internal class RetryableServiceClient : IServiceClient
    {
        private const int _defaultRetryPauseScale = 300;
        private IServiceClient _innerClient;

        public RetryableServiceClient(IServiceClient innerClient)
        {
            this._innerClient = innerClient;
            this.MaxErrorRetry = 3;
        }

        public IAsyncResult BeginSend(ServiceRequest request, Aliyun.OpenServices.Common.Communication.ExecutionContext context, AsyncCallback callback, object state)
        {
            RetryableAsyncResult asyncResult = new RetryableAsyncResult(callback, state, request, context);
            this.BeginSendImpl(request, context, asyncResult);
            return asyncResult;
        }

        private void BeginSendImpl(ServiceRequest request, Aliyun.OpenServices.Common.Communication.ExecutionContext context, RetryableAsyncResult asyncResult)
        {
            if (asyncResult.InnerAsyncResult != null)
            {
                asyncResult.InnerAsyncResult.Dispose();
            }
            asyncResult.InnerAsyncResult = this._innerClient.BeginSend(request, context, new AsyncCallback(this.OnBeginSendCompleted), asyncResult) as AsyncResult;
        }

        public ServiceResponse EndSend(IAsyncResult ar)
        {
            ServiceResponse temp;
            RetryableAsyncResult retryableAsyncResult = ar as RetryableAsyncResult;
            try
            {
                ServiceResponse result = retryableAsyncResult.GetResult();
                retryableAsyncResult.Dispose();
                temp = result;
            }
            catch (ObjectDisposedException)
            {
                throw new InvalidOperationException(Resources.ExceptionEndOperationHasBeenCalled);
            }
            return temp;
        }

        private void OnBeginSendCompleted(IAsyncResult ar)
        {
            RetryableAsyncResult retryableAsyncResult = ar.AsyncState as RetryableAsyncResult;
            try
            {
                ServiceResponse result = this._innerClient.EndSend(ar);
                retryableAsyncResult.Complete(result);
            }
            catch (Exception ex)
            {
                if (this.ShouldRetry(ex, retryableAsyncResult.Retries))
                {
                    int temp;
                    retryableAsyncResult.Retries = (temp = retryableAsyncResult.Retries) + 1;
                    Pause(temp);
                    this.BeginSendImpl(retryableAsyncResult.Request, retryableAsyncResult.Context, retryableAsyncResult);
                }
                else
                {
                    retryableAsyncResult.Complete(ex);
                }
            }
        }

        private static void Pause(int retries)
        {
            int scale = 300;
            int delay = ((int) Math.Pow(2.0, (double) retries)) * scale;
            Thread.Sleep(delay);
        }

        public ServiceResponse Send(ServiceRequest request, Aliyun.OpenServices.Common.Communication.ExecutionContext context)
        {
            return this.SendImpl(request, context, 0);
        }

        private ServiceResponse SendImpl(ServiceRequest request, Aliyun.OpenServices.Common.Communication.ExecutionContext context, int retries)
        {
            try
            {
                return this._innerClient.Send(request, context);
            }
            catch (Exception ex)
            {
                if (!this.ShouldRetry(ex, retries))
                {
                    throw ex;
                }
                Pause(retries);
                return this.SendImpl(request, context, ++retries);
            }
        }

        private bool ShouldRetry(Exception ex, int retries)
        {
            if (retries > this.MaxErrorRetry)
            {
                return false;
            }
            WebException webException = ex as WebException;
            if (webException != null)
            {
                HttpWebResponse httpWebResponse = webException.Response as HttpWebResponse;
                if ((httpWebResponse != null) && ((httpWebResponse.StatusCode == HttpStatusCode.ServiceUnavailable) || (httpWebResponse.StatusCode == HttpStatusCode.InternalServerError)))
                {
                    return true;
                }
            }
            return ((this.ShouldRetryCallback != null) && this.ShouldRetryCallback(ex));
        }

        public int MaxErrorRetry { get; set; }

        public Func<Exception, bool> ShouldRetryCallback { get; set; }
    }
}

