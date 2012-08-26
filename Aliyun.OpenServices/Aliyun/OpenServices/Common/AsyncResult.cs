namespace Aliyun.OpenServices.Common
{
    using System;
    using System.Diagnostics;
    using System.Threading;

    internal abstract class AsyncResult : IAsyncResult, IDisposable
    {
        private object _asyncState;
        private ManualResetEvent _asyncWaitEvent;
        private bool _completedSynchronously;
        private Exception _exception;
        private bool _isCompleted;
        private AsyncCallback _userCallback;

        protected AsyncResult(AsyncCallback callback, object state)
        {
            this._userCallback = callback;
            this._asyncState = state;
        }

        public void Complete(Exception ex)
        {
            this._exception = ex;
            this.NotifyCompletion();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this._asyncWaitEvent != null))
            {
                this._asyncWaitEvent.Close();
                this._asyncWaitEvent = null;
            }
        }

        [DebuggerNonUserCode]
        protected void NotifyCompletion()
        {
            this._isCompleted = true;
            if (this._asyncWaitEvent != null)
            {
                this._asyncWaitEvent.Set();
            }
            if (this._userCallback != null)
            {
                this._userCallback(this);
            }
        }

        protected void WaitForCompletion()
        {
            if (!this.IsCompleted)
            {
                this._asyncWaitEvent.WaitOne();
            }
            if (this._exception != null)
            {
                throw this._exception;
            }
        }

        [DebuggerNonUserCode]
        public object AsyncState
        {
            get
            {
                return this._asyncState;
            }
        }

        [DebuggerNonUserCode]
        public WaitHandle AsyncWaitHandle
        {
            get
            {
                if (this._asyncWaitEvent == null)
                {
                    ManualResetEvent manualResetEvent = new ManualResetEvent(false);
                    if (Interlocked.CompareExchange<ManualResetEvent>(ref this._asyncWaitEvent, manualResetEvent, null) != null)
                    {
                        manualResetEvent.Close();
                    }
                    if (this.IsCompleted)
                    {
                        this._asyncWaitEvent.Set();
                    }
                }
                return this._asyncWaitEvent;
            }
        }

        [DebuggerNonUserCode]
        public bool CompletedSynchronously
        {
            get
            {
                return this._completedSynchronously;
            }
            protected set
            {
                this._completedSynchronously = value;
            }
        }

        [DebuggerNonUserCode]
        public bool IsCompleted
        {
            get
            {
                return this._isCompleted;
            }
        }
    }
}

