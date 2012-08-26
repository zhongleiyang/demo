namespace Aliyun.OpenServices.Common
{
    using System;

    internal class AsyncResult<T> : AsyncResult
    {
        private T _result;

        public AsyncResult(AsyncCallback callback, object state) : base(callback, state)
        {
        }

        public void Complete(T result)
        {
            this._result = result;
            base.NotifyCompletion();
        }

        public T GetResult()
        {
            base.WaitForCompletion();
            return this._result;
        }
    }
}

