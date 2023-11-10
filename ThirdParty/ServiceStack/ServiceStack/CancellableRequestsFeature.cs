using System;
using System.Collections.Concurrent;
using System.Configuration;
using System.Diagnostics;
using System.Threading;
using ServiceStack.Web;

namespace ServiceStack
{
    public class CancellableRequestsFeature : IPlugin
    {
        public string AtPath { get; set; }

        internal ConcurrentDictionary<string, ICancellableRequest> RequestsMap = new();

        public CancellableRequestsFeature()
        {
            AtPath = "/current-requests/{Tag}/cancel";
        }

        internal void UnregisterCancellableRequest(string requestTag)
        {
            if (RequestsMap.TryRemove(requestTag, out var existing))
            {
                existing.Dispose();
            }
        }

        public void Register(IAppHost appHost)
        {
            appHost.RegisterService(typeof(CancellableRequestService), AtPath);
        }
    }

    [DefaultRequest(typeof(CancelRequest))]
    public class CancellableRequestService : Service
    {
        public object Any(CancelRequest request)
        {
            if (request.Tag.IsNullOrEmpty())
            {
                throw new ArgumentNullException("Tag");
            }

            using (var cancallableReq = Request.GetCancellableRequest(request.Tag))
            {
                if (cancallableReq == null)
                {
                    throw HttpError.NotFound($"Request with Tag '{request.Tag}' does not exist");
                }

                cancallableReq.TokenSource.Cancel();

                return new CancelRequestResponse
                {
                    Tag = request.Tag,
                    Elapsed = cancallableReq.Elapsed,
                };
            }
        }
    }

    public interface ICancellableRequest : IDisposable
    {
        CancellationToken Token { get; }
        CancellationTokenSource TokenSource { get; }
        TimeSpan Elapsed { get; }
    }

    internal class CancellableRequest : ICancellableRequest
    {
        private readonly CancellableRequestsFeature feature;
        private readonly string requestTag;
        private readonly Stopwatch stopwatch;

        public CancellationToken Token { get; private set; }
        public CancellationTokenSource TokenSource { get; private set; }

        public CancellableRequest(CancellableRequestsFeature feature, IRequest req, string tag)
        {
            TokenSource = new();
            Token = TokenSource.Token;
            this.feature = feature;
            requestTag = tag;
            stopwatch = Stopwatch.StartNew();

            this.feature.UnregisterCancellableRequest(requestTag);

            req.Items[typeof(CancellableRequest).Name] = feature.RequestsMap[tag] = this;
        }

        public TimeSpan Elapsed => stopwatch.Elapsed;

        public void Dispose()
        {
            stopwatch.Stop();
            feature.UnregisterCancellableRequest(requestTag);
        }
    }

    internal class EmptyCancellableRequest : ICancellableRequest
    {
        private readonly Stopwatch stopwatch;
        public CancellationToken Token { get; private set; }
        public CancellationTokenSource TokenSource { get; private set; }

        public EmptyCancellableRequest()
        {
            TokenSource = new();
            Token = TokenSource.Token;
            stopwatch = Stopwatch.StartNew();
        }

        public TimeSpan Elapsed => stopwatch.Elapsed;

        public void Dispose()
        {
            stopwatch.Stop();
        }
    }

    public static class CancellableRequestsExtensions
    {
        public static ICancellableRequest CreateCancellableRequest(this IRequest req)
        {
            var feature = HostContext.GetPlugin<CancellableRequestsFeature>();
            if (feature == null)
            {
                throw new("Requires CancellableRequestsFeature plugin");
            }

            var xTag = req.GetHeader(HttpHeaders.XTag);
            if (xTag != null)
            {
                return new CancellableRequest(feature, req, xTag);
            }

            return new EmptyCancellableRequest();
        }

        public static ICancellableRequest GetCancellableRequest(this IRequest req, string tag)
        {
            var feature = HostContext.GetPlugin<CancellableRequestsFeature>();
            if (feature == null)
            {
                throw new("Requires CancellableRequestsFeature plugin");
            }

            if (feature.RequestsMap.TryGetValue(tag, out var cancellableReq))
            {
                return cancellableReq;
            }

            return null;
        }
    }
}