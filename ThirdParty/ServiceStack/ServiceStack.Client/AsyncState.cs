// Copyright (c) ServiceStack, Inc. All Rights Reserved.
// License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

namespace ServiceStack
{
    using System;
    using System.IO;
    using System.Net;
    using System.Text;
    using System.Threading;
    using ServiceStack.Text;

    public class AsyncState<TResponse> : IDisposable
    {
        private bool timedOut; // Pass the correct error back even on Async Calls

        public AsyncState(int bufferSize)
        {
            BufferRead = new byte[bufferSize];
            TextData = StringBuilderCache.Allocate();
            BytesData = MemoryStreamFactory.GetStream(bufferSize);
            WebRequest = null;
            ResponseStream = null;
        }

        public string HttpMethod;

        public string Url;

        public StringBuilder TextData;

        public MemoryStream BytesData;

        public byte[] BufferRead;

        public object Request;

        public HttpWebRequest WebRequest;

        public HttpWebResponse WebResponse;

        public Stream ResponseStream;

        public int Completed;

        public int RequestCount;

#if !NETSTANDARD1_1
        public ITimer Timer;
#endif

        public CancellationToken Token;

        public Action<WebResponse> OnResponseInit;

        public Action<TResponse> OnSuccess;

        public Action<object, Exception> OnError;

        public SynchronizationContext UseSynchronizationContext;

        public bool HandleCallbackOnUIThread;

        public long ResponseBytesRead;

        public long ResponseContentLength;

        public void HandleSuccess(TResponse response)
        {
            StopTimer();

            if (OnSuccess == null)
            {
                return;
            }

            if (UseSynchronizationContext != null)
            {
                UseSynchronizationContext.Post(asyncState => OnSuccess(response), this);
            }
            else if (HandleCallbackOnUIThread)
            {
                PclExportClient.Instance.RunOnUiThread(() => OnSuccess(response));
            }
            else
            {
                OnSuccess(response);
            }
        }

        public void HandleError(object response, Exception ex)
        {
            StopTimer();

            if (OnError == null)
            {
                return;
            }

            var toReturn = ex;
            if (timedOut)
            {
                toReturn = PclExportClient.Instance.CreateTimeoutException(ex, "The request timed out");
            }

            if (UseSynchronizationContext != null)
            {
                UseSynchronizationContext.Post(asyncState => OnError(response, toReturn), this);
            }
            else if (HandleCallbackOnUIThread)
            {
                PclExportClient.Instance.RunOnUiThread(() => OnError(response, toReturn));
            }
            else
            {
                OnError(response, toReturn);
            }
        }

        public void StartTimer(TimeSpan timeOut)
        {
#if !NETSTANDARD1_1
            Timer = PclExportClient.Instance.CreateTimer(TimedOut, timeOut, this);
#endif
        }

        public void StopTimer()
        {
#if !NETSTANDARD1_1
            if (Timer != null)
            {
                Timer.Cancel();
                Timer = null;
            }
#endif
        }

#if NETFX_CORE
            public void TimedOut(ThreadPoolTimer timer)
            {
                if (Interlocked.Increment(ref Completed) == 1)
                {
                    if (this.WebRequest != null)
                    {
                        timedOut = true;
                        this.WebRequest.Abort();
                    }
                }

                StopTimer();

                this.Dispose();
            }
#else
        public void TimedOut(object state)
        {
            if (Interlocked.Increment(ref Completed) == 1)
            {
                if (WebRequest != null)
                {
                    timedOut = true;
                    WebRequest.Abort();
                }
            }

            StopTimer();

            Dispose();
        }
#endif

        public void Dispose()
        {
            if (TextData != null)
            {
                StringBuilderCache.Free(TextData);
                TextData = null;
            }
            if (BytesData != null)
            {
                BytesData.Dispose();
                BytesData = null;
            }
#if !NETSTANDARD1_1
            if (Timer != null)
            {
                Timer.Dispose();
                Timer = null;
            }
#endif
        }
    }
}