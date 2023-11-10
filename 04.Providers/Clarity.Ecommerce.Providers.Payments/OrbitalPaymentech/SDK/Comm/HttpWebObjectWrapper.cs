#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Comm
{
    using System;
    using System.IO;
    using System.Net;
    using Common;
    using log4net;

    ///
    ///
    /// Title: Http Web Object Wrapper
    ///
    /// Description: Encapsulates use of HttpWebRequest and HttpWebResponse
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 2.0
    ///
    /// <summary>
    ///
    /// </summary>
    public class HttpWebObjectWrapper : WrapperBase<HttpWebObjectWrapper>
    {
        // Use common SDK logger by default, normally this will be reset
        private static ILog logger;
        private HttpWebRequest request;
        private HttpWebResponse response;
        private Stream requestStream;
        private Stream responseStream;

        /// <summary>This constructor needs to be public and parameter-less to be compatible with WrapperBase generic
        /// base class.</summary>
        // ReSharper disable once EmptyConstructor
        public HttpWebObjectWrapper()
        {
        }

        /// <summary>
        /// Only create the request object, the response object will be created for us
        /// </summary>
        /// <param name="url"></param>
        public virtual void Initialize(Uri url)
        {
            request = (HttpWebRequest)WebRequest.Create(url);
        }

        public static ILog Logger
        {
            get => logger ?? (logger = new LoggingWrapper().EngineLogger);
            set => logger = value;
        }

        // Make all methods virtual so that they can easily be overridden by a stub class for nunit testing

        /// <summary>
        /// TCP KeepAlive setting
        /// </summary>
        public virtual bool KeepAlive
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.KeepAlive = value;
            }
        }

        /// <summary>
        /// Turn on the Expect 100 continue behaviour
        /// </summary>
        public virtual bool Expect100Continue
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.ServicePoint.Expect100Continue = value;
            }
        }

        /// <summary>
        /// Turn on the Allow Auto Redirect behavior
        /// </summary>
        public virtual bool AllowAutoRedirect
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.AllowAutoRedirect = value;
            }
        }

        /// <summary>
        /// Have the object limit the number of simultaneous connections
        /// </summary>
        public virtual int ConnectionLimit
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.ServicePoint.ConnectionLimit = value;
            }
        }

        /// <summary>
        /// Tell request object how long to wait for response
        /// </summary>
        public virtual int Timeout
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.Timeout = value;
            }
        }

        /// <summary>
        /// Connect() is more intuitive and so we change the name to that
        /// </summary>
        /// <returns></returns>
        public virtual bool Connect()
        {
            if (requestStream != null)
            {
                try
                {
                    requestStream.Close();
                }
                catch (Exception)
                {
                    // Do Nothing
                }
            }

            requestStream = request.GetRequestStream();

            return requestStream != null;
        }

        /// <summary>
        /// remote port
        /// </summary>
        public virtual int Port
        {
            get
            {
                if (request == null || request.Address == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }

                return request.Address.Port;
            }
        }

        /// <summary>
        /// remote host. It could be a host name or an IP address
        /// </summary>
        public virtual string Host
        {
            get
            {
                if (request == null || request.Address == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                return request.Address.Host;
            }
        }

        /// <summary>
        /// Write to the established connection
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public virtual void Write(byte[] buffer, int offset, int count)
        {
            if (requestStream == null)
            {
                throw new ObjectDisposedException("Object not properly initialized");
            }
            requestStream.Write(buffer, offset, count);
        }

        /// <summary>
        /// Read from the established connection's response stream
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        public virtual int Read(byte[] buffer, int offset, int count)
        {
            if (responseStream == null)
            {
                throw new ObjectDisposedException("Object not properly initialized");
            }
            return responseStream.Read(buffer, offset, count);
        }

        /// <summary>
        /// Get the response stream
        /// </summary>
        public virtual void GetResponse()
        {
            if (request == null)
            {
                throw new ObjectDisposedException("Object not properly initialized");
            }
            response = (HttpWebResponse)request.GetResponse();
            responseStream = response.GetResponseStream();
        }

        /// <summary>
        /// Response property
        /// </summary>
        public virtual HttpWebResponse Response
        {
            set => response = value;
        }

        /// <summary>
        /// Close both the request and response streams
        /// </summary>
        public virtual void Close()
        {
            if (responseStream != null)
            {
                try
                {
                    responseStream.Close();
                }
                catch (Exception)
                {
                    // Do Nothing
                }
                finally
                {
                    responseStream = null;
                }
            }
            if (requestStream == null)
            {
                return;
            }
            try
            {
                requestStream.Close();
            }
            catch (Exception)
            {
                // Do Nothing
            }
            finally
            {
                requestStream = null;
            }
        }

        /// <summary>
        /// Quick check for response
        /// </summary>
        public virtual bool ResponseReceived => response != null;

        /// <summary>
        /// Get the response status code
        /// </summary>
        public virtual HttpStatusCode StatusCode
        {
            get
            {
                if (response == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                return response.StatusCode;
            }
        }

        /// <summary>
        /// Get the response MIME header
        /// </summary>
        public virtual WebHeaderCollection ResponseHeaders
        {
            get
            {
                if (response == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                return response.Headers;
            }
        }

        /// <summary>
        /// Get/Set request headers
        /// </summary>
        public virtual WebHeaderCollection RequestHeaders
        {
            get
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                return request.Headers;
            }
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.Headers = value;
            }
        }

        /// <summary>
        /// Set proxy structure using a property
        /// </summary>
        public virtual IWebProxy Proxy
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.Proxy = value;
            }
        }

        /// <summary>
        /// Set request method using a property
        /// </summary>
        public virtual string Method
        {
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.Method = value;
            }
        }

        /// <summary>
        /// Set content type mime header using a property
        /// </summary>
        public virtual string ContentType
        {
            get
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                return request.ContentType;
            }
            set
            {
                if (request == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                request.ContentType = value;
            }
        }

        /// <summary>
        /// Set content length using property
        /// </summary>
        public virtual long ResponseContentLength
        {
            get
            {
                if (response == null)
                {
                    throw new ObjectDisposedException("Object not properly initialized");
                }
                return response.ContentLength;
            }
        }
    }
}
