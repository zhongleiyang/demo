namespace Aliyun.OpenServices.Common.Communication
{
    using Aliyun.OpenServices;
    using Aliyun.OpenServices.Common;
    using Aliyun.OpenServices.Common.Utilities;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net;
    using System.Runtime.CompilerServices;

    internal class ServiceClientImpl : ServiceClient
    {
        public ServiceClientImpl(ClientConfiguration configuration) : base(configuration)
        {
        }

        protected override IAsyncResult BeginSendCore(ServiceRequest serviceRequest, ExecutionContext context, AsyncCallback callback, object state)
        {
            HttpWebRequest request = HttpFactory.CreateWebRequest(serviceRequest, base.Configuration);
            HttpAsyncResult asyncResult = new HttpAsyncResult(callback, state) {
                WebRequest = request,
                Context = context
            };
            SetRequestContent(request, serviceRequest, true, delegate {
                request.BeginGetResponse(new AsyncCallback(this.OnGetResponseCompleted), asyncResult);
            });
            return asyncResult;
        }

        private static ServiceResponse HandlException(WebException ex)
        {
            if (!(ex.Response is HttpWebResponse))
            {
                throw ex;
            }
            return new ResponseImpl(ex);
        }

        private void OnGetResponseCompleted(IAsyncResult ar)
        {
            HttpAsyncResult asyncResult = ar.AsyncState as HttpAsyncResult;
            try
            {
                HttpWebResponse response = asyncResult.WebRequest.EndGetResponse(ar) as HttpWebResponse;
                ServiceResponse res = new ResponseImpl(response);
                ServiceClient.HandleResponse(res, asyncResult.Context.ResponseHandlers);
                asyncResult.Complete(res);
            }
            catch (WebException ex)
            {
                try
                {
                    ServiceResponse res = HandlException(ex);
                    ServiceClient.HandleResponse(res, asyncResult.Context.ResponseHandlers);
                    asyncResult.WebRequest.Abort();
                    asyncResult.Complete(res);
                }
                catch (Exception ex2)
                {
                    asyncResult.WebRequest.Abort();
                    asyncResult.Complete(ex2);
                }
            }
            catch (Exception ex3)
            {
                asyncResult.WebRequest.Abort();
                asyncResult.Complete(ex3);
            }
        }

        protected override ServiceResponse SendCore(ServiceRequest serviceRequest, ExecutionContext context)
        {
            HttpWebRequest request = HttpFactory.CreateWebRequest(serviceRequest, base.Configuration);
            SetRequestContent(request, serviceRequest, false, null);
            try
            {
                return new ResponseImpl(request.GetResponse() as HttpWebResponse);
            }
            catch (WebException ex)
            {
                return HandlException(ex);
            }
        }

        private static void SetRequestContent(HttpWebRequest webRequest, ServiceRequest serviceRequest, bool async, Action asyncCallback)
        {
            Stream data = serviceRequest.BuildRequestContent();
            if ((data == null) || ((serviceRequest.Method != HttpMethod.Put) && (serviceRequest.Method != HttpMethod.Post)))
            {
                if (async)
                {
                    asyncCallback();
                }
            }
            else
            {
                webRequest.ContentLength = data.Length;
                if (async)
                {
                    webRequest.BeginGetRequestStream(delegate (IAsyncResult ar) {
                        using (Stream requestStream = webRequest.EndGetRequestStream(ar))
                        {
                            using (data)
                            {
                                data.WriteTo(requestStream);
                            }
                        }
                        asyncCallback();
                    }, null);
                }
                else
                {
                    using (Stream requestStream = webRequest.GetRequestStream())
                    {
                        using (data)
                        {
                            data.WriteTo(requestStream);
                        }
                    }
                }
            }
        }

        private class HttpAsyncResult : AsyncResult<ServiceResponse>
        {
            public HttpAsyncResult(AsyncCallback callback, object state) : base(callback, state)
            {
            }

            public ExecutionContext Context { get; set; }

            public HttpWebRequest WebRequest { get; set; }
        }

        private class ResponseImpl : ServiceResponse
        {
            private bool _disposed;
            private Exception _failure;
            private IDictionary<string, string> _headers;
            private HttpWebResponse _response;

            public ResponseImpl(HttpWebResponse httpWebResponse)
            {
                this._response = httpWebResponse;
            }

            public ResponseImpl(WebException failure)
            {
                HttpWebResponse httpWebResponse = failure.Response as HttpWebResponse;
                this._failure = failure;
                this._response = httpWebResponse;
            }

            protected override void Dispose(bool disposing)
            {
                base.Dispose(disposing);
                if (!this._disposed && disposing)
                {
                    if (this._response != null)
                    {
                        this._response.Close();
                        this._response = null;
                    }
                    this._disposed = true;
                }
            }

            private static IDictionary<string, string> GetResponseHeaders(HttpWebResponse response)
            {
                WebHeaderCollection headers = response.Headers;
                Dictionary<string, string> result = new Dictionary<string, string>(headers.Count);
                for (int i = 0; i < headers.Count; i++)
                {
                    string key = headers.Keys[i];
                    string value = headers.Get(key);
                    result.Add(key, HttpUtils.ReEncode(value, "iso-8859-1", "utf-8"));
                }
                return result;
            }

            private void ThrowIfObjectDisposed()
            {
                if (this._disposed)
                {
                    throw new ObjectDisposedException(base.GetType().Name);
                }
            }

            public override Stream Content
            {
                get
                {
                    Stream temp;
                    this.ThrowIfObjectDisposed();
                    try
                    {
                        temp = (this._response != null) ? this._response.GetResponseStream() : null;
                    }
                    catch (ProtocolViolationException ex)
                    {
                        throw new InvalidOperationException(ex.Message, ex);
                    }
                    return temp;
                }
            }

            public override Exception Failure
            {
                get
                {
                    return this._failure;
                }
            }

            public override IDictionary<string, string> Headers
            {
                get
                {
                    this.ThrowIfObjectDisposed();
                    if (this._headers == null)
                    {
                        this._headers = GetResponseHeaders(this._response);
                    }
                    return this._headers;
                }
            }

            public override HttpStatusCode StatusCode
            {
                get
                {
                    return this._response.StatusCode;
                }
            }
        }
    }
}

