﻿using System;
using ServiceStack.Web;

namespace ServiceStack.Host.Handlers
{
    public class StaticContentHandler : HttpAsyncTaskHandler
    {
        private readonly string textContents;
        private readonly byte[] bytes;
        private readonly string contentType;

        private StaticContentHandler(string contentType)
        {
            this.contentType = contentType ?? throw new ArgumentNullException(nameof(contentType));
            RequestName = nameof(StaticContentHandler);
        }

        public StaticContentHandler(string textContents, string contentType)
            : this(contentType)
        {
            this.textContents = textContents;
        }

        public StaticContentHandler(byte[] bytes, string contentType)
            : this(contentType)
        {
            this.bytes = bytes;
        }

        public override void ProcessRequest(IRequest httpReq, IResponse httpRes, string operationName)
        {
            if (HostContext.ApplyCustomHandlerRequestFilters(httpReq, httpRes))
            {
                return;
            }

            if (textContents == null && bytes == null)
            {
                return;
            }

            httpRes.ContentType = contentType;

            if (textContents != null)
            {
                httpRes.Write(textContents);
            }
            else if (bytes != null)
            {
                httpRes.OutputStream.Write(bytes, 0, bytes.Length);
            }

            httpRes.Flush();
            httpRes.EndHttpHandlerRequest(skipHeaders: true);
        }
    }
}