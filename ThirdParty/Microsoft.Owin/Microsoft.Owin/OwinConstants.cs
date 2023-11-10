// Decompiled with JetBrains decompiler
// Type: Microsoft.Owin.OwinConstants
// Assembly: Microsoft.Owin, Version=4.1.1.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35
// MVID: 4DE47499-3BF7-44CA-A7C0-ECB91429FDE6
// Assembly location: C:\Users\jotha\.nuget\packages\microsoft.owin\4.1.1\lib\net45\Microsoft.Owin.dll

namespace Microsoft.Owin
{
    /// <summary>An owin constants.</summary>
    internal static class OwinConstants
    {
        /// <summary>The call cancelled.</summary>
        public const string CallCancelled = "owin.CallCancelled";

        /// <summary>The owin version.</summary>
        public const string OwinVersion = "owin.Version";

        /// <summary>The request body.</summary>
        public const string RequestBody = "owin.RequestBody";

        /// <summary>The request headers.</summary>
        public const string RequestHeaders = "owin.RequestHeaders";

        /// <summary>The request method.</summary>
        public const string RequestMethod = "owin.RequestMethod";

        /// <summary>Full pathname of the request file.</summary>
        public const string RequestPath = "owin.RequestPath";

        /// <summary>The request path base.</summary>
        public const string RequestPathBase = "owin.RequestPathBase";

        /// <summary>The request protocol.</summary>
        public const string RequestProtocol = "owin.RequestProtocol";

        /// <summary>The request query string.</summary>
        public const string RequestQueryString = "owin.RequestQueryString";

        /// <summary>The request scheme.</summary>
        public const string RequestScheme = "owin.RequestScheme";

        /// <summary>The response body.</summary>
        public const string ResponseBody = "owin.ResponseBody";

        /// <summary>The response headers.</summary>
        public const string ResponseHeaders = "owin.ResponseHeaders";

        /// <summary>The response protocol.</summary>
        public const string ResponseProtocol = "owin.ResponseProtocol";

        /// <summary>The response reason phrase.</summary>
        public const string ResponseReasonPhrase = "owin.ResponseReasonPhrase";

        /// <summary>The response status code.</summary>
        public const string ResponseStatusCode = "owin.ResponseStatusCode";

        /// <summary>A builder.</summary>
        internal static class Builder
        {
            /// <summary>The add signature conversion.</summary>
            public const string AddSignatureConversion = "builder.AddSignatureConversion";

            /// <summary>The default application.</summary>
            public const string DefaultApp = "builder.DefaultApp";
        }

        /// <summary>A common keys.</summary>
        internal static class CommonKeys
        {
            /// <summary>The addresses.</summary>
            public const string Addresses = "host.Addresses";

            /// <summary>Name of the application.</summary>
            public const string AppName = "host.AppName";

            /// <summary>The capabilities.</summary>
            public const string Capabilities = "server.Capabilities";

            /// <summary>The client certificate.</summary>
            public const string ClientCertificate = "ssl.ClientCertificate";

            /// <summary>The host.</summary>
            public const string Host = "host";

            /// <summary>The is local.</summary>
            public const string IsLocal = "server.IsLocal";

            /// <summary>The local IP address.</summary>
            public const string LocalIpAddress = "server.LocalIpAddress";

            /// <summary>The local port.</summary>
            public const string LocalPort = "server.LocalPort";

            /// <summary>The on application disposing.</summary>
            public const string OnAppDisposing = "host.OnAppDisposing";

            /// <summary>The on sending headers.</summary>
            public const string OnSendingHeaders = "server.OnSendingHeaders";

            /// <summary>Full pathname of the file.</summary>
            public const string Path = "path";

            /// <summary>The port.</summary>
            public const string Port = "port";

            /// <summary>The remote IP address.</summary>
            public const string RemoteIpAddress = "server.RemoteIpAddress";

            /// <summary>The remote port.</summary>
            public const string RemotePort = "server.RemotePort";

            /// <summary>The scheme.</summary>
            public const string Scheme = "scheme";

            /// <summary>The trace output.</summary>
            public const string TraceOutput = "host.TraceOutput";
        }

        /// <summary>An opaque constants.</summary>
        internal static class OpaqueConstants
        {
            /// <summary>The call cancelled.</summary>
            public const string CallCancelled = "opaque.CallCancelled";

            /// <summary>The stream.</summary>
            public const string Stream = "opaque.Stream";

            /// <summary>The upgrade.</summary>
            public const string Upgrade = "opaque.Upgrade";

            /// <summary>The version.</summary>
            public const string Version = "opaque.Version";
        }

        /// <summary>A security.</summary>
        internal static class Security
        {
            /// <summary>The authenticate.</summary>
            public const string Authenticate = "security.Authenticate";

            /// <summary>The challenge.</summary>
            public const string Challenge = "security.Challenge";

            /// <summary>The sign in.</summary>
            public const string SignIn = "security.SignIn";

            /// <summary>The sign out.</summary>
            public const string SignOut = "security.SignOut";

            /// <summary>The sign out properties.</summary>
            public const string SignOutProperties = "security.SignOutProperties";

            /// <summary>The user.</summary>
            public const string User = "server.User";
        }

        /// <summary>A send files.</summary>
        internal static class SendFiles
        {
            /// <summary>The concurrency.</summary>
            public const string Concurrency = "sendfile.Concurrency";

            /// <summary>The send asynchronous.</summary>
            public const string SendAsync = "sendfile.SendAsync";

            /// <summary>The support.</summary>
            public const string Support = "sendfile.Support";

            /// <summary>The version.</summary>
            public const string Version = "sendfile.Version";
        }

        /// <summary>A web socket.</summary>
        internal static class WebSocket
        {
            /// <summary>The accept.</summary>
            public const string Accept = "websocket.Accept";

            /// <summary>The call cancelled.</summary>
            public const string CallCancelled = "websocket.CallCancelled";

            /// <summary>Information describing the client close.</summary>
            public const string ClientCloseDescription = "websocket.ClientCloseDescription";

            /// <summary>The client close status.</summary>
            public const string ClientCloseStatus = "websocket.ClientCloseStatus";

            /// <summary>The close asynchronous.</summary>
            public const string CloseAsync = "websocket.CloseAsync";

            /// <summary>The receive asynchronous.</summary>
            public const string ReceiveAsync = "websocket.ReceiveAsync";

            /// <summary>The send asynchronous.</summary>
            public const string SendAsync = "websocket.SendAsync";

            /// <summary>The sub protocol.</summary>
            public const string SubProtocol = "websocket.SubProtocol";

            /// <summary>The version.</summary>
            public const string Version = "websocket.Version";
        }
    }
}
