// Disables warnings for XML doc comments.
// ReSharper disable VirtualMemberCallInConstructor
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Comm
{
    using System;
    using System.Collections;
    using System.Net;
    using System.Net.Security;
    using System.Net.Sockets;
    using System.Security.Cryptography.X509Certificates;
    using System.Text;
    using System.Threading;
    using Common;
    using Configurator;
    using Framework;

    /// Title: Base class for the HTTPS Connect Communication Module
    ///
    /// Description: This communication module implements the
    /// "HTTPSConnect" application protocol
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 2.0
    ///
    /// <summary>
    /// This communication module implements the
    /// "HTTPSConnect" application protocol
    /// </summary>
    public abstract class HTTPSBase : CommBase, ICommModule
    {
        /// <summary>
        /// Constructor - Sets private fields
        /// </summary>
        /// <param name="configurator">contents of HTTPSConnectConfig.xml</param>
        public HTTPSBase(ConfigurationData cdata) : base(cdata)
        {
            Host = cdata.GetString("Host", HostDefault);
            ProxyHost = cdata.GetString("ProxyHost", null);
            FailoverHost = cdata.GetString("FailoverHost", FailoverHostDefault);
            Port = cdata.GetInteger("Port", PortDefault);
            ProxyPort = cdata.GetInteger("ProxyPort", ProxyPortDefault);
            FailoverPort = cdata.GetInteger("FailoverPort", FailoverPortDefault);
            TimeoutSecs = cdata.GetInteger("TimeoutSecs", TimeoutSecsDefault);
            AuthorizationURI = cdata.GetString("AuthorizationURI", AuthorizationURIDefault);
            ConnectFailRetries = cdata.GetInteger("ConnectFailRetries", ConnectFailRetriesDefault);
            ProxyAuthScheme = cdata.GetString("ProxyAuthScheme", ProxyAuthSchemeDefault);
            ProxyAuthPreempt = cdata.GetBool("ProxyAuthPreempt", ProxyAuthPreemptDefault);
            VerifyHost = cdata.GetBool("VerifyHost", VerifyHostDefault);
            Encrypt = cdata.GetBool("Encrypt", EncryptDefault);
            MaxConnections = cdata.GetInteger("MaxConnections", MaxConnectionsDefault);
            ConnectLoopWaitMSecs = cdata.GetInteger("ConnectLoopWaitMSecs", ConnectLoopWaitMSecsDefault);
            CloseLingerSecs = cdata.GetInteger("CloseLingerSecs", CloseLingerSecsDefault);
            CloseIdleConnectionSecs = cdata.GetInteger("CloseIdleConnectionSecs", CloseIdleConnectionSecsDefault);
            FailoverDurationSecs = cdata.GetInteger("FailoverDurationSecs", FailoverDurationSecsDefault);
            MaxInBufferSize = cdata.GetInteger("MaxInBufferSize", MaxInBufferSizeDefault);
            InBufferSize = cdata.GetInteger("InBufferSize", InBufferSizeDefault);
            var value = cdata.GetString("Ciphers", null);
            ciphers = cdata.StringToStringArray(value);
            value = cdata.GetString("Protocols", null);
            protocols = cdata.StringToStringArray(value);
            value = cdata.GetString("ConnectFailRetryReasons", null);
            connectFailRetryReasons = CommUtils.StringToStringArray(value);

            if (AuthorizationURI == null || AuthorizationURI.Length == 0)
            {
                throw new CommException(
                Error.InitializationFailure,
                "AuthorizationURI in HTTPSConnectConfig.xml must"
                + " be set");
            }

            if (MaxConnections == 0)
            {
                throw new CommException(
                Error.InitializationFailure,
                "MaxConnections must be greater than 0");
            }

            // This option allows the host name in the server certificate
            // to be ignored.  Should only be set to "false" in a test
            // environment
            if (VerifyHost == false)
            {
                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;
            }
        }

        //The following Get the default values for variables if they are not
        //set in HTTPSConnectConfig.xml

        protected virtual string HostDefault => null;

        protected virtual string FailoverHostDefault => null;

        protected virtual int FailoverDurationSecsDefault => 1200;

        protected virtual string AuthorizationURIDefault => "/NetConnect/controller";

        protected virtual int PortDefault => 443;

        protected virtual int FailoverPortDefault => 443;

        protected virtual int ProxyPortDefault => 0;

        protected virtual int TimeoutSecsDefault => 90;

        protected virtual string ProxyAuthSchemeDefault => "DIGEST";

        protected virtual bool ProxyAuthPreemptDefault => true;

        protected virtual int MaxConnectionsDefault => 20;

        protected virtual int CloseLingerSecsDefault => 10;

        protected virtual bool VerifyHostDefault => true;

        protected virtual bool EncryptDefault => true;

        protected virtual int ConnectLoopWaitMSecsDefault => 1000;

        protected virtual int ConnectFailRetriesDefault => 3;

        protected virtual int CloseIdleConnectionSecsDefault => 600;

        protected virtual int MaxInBufferSizeDefault => 1024 * 1024;

        protected virtual int InBufferSizeDefault => 1024;

        protected override string ModuleName => "HTTPSBase";

        private string[] ciphers;
        protected string[] Ciphers
        {
            get
            {
                if (ciphers == null)
                {
                    ciphers = new string[0];
                }
                return ciphers;
            }
        }

        private string[] protocols;
        protected string[] Protocols
        {
            get
            {
                if (protocols == null)
                {
                    protocols = new string[0];
                }
                return protocols;
            }
        }

        protected long FailoverStart { get; private set; }
        protected int Port { get; private set; }
        protected string Host { get; private set; }
        protected int TimeoutSecs { get; private set; }
        protected string AuthorizationURI { get; private set; }
        protected string ProxyHost { get; private set; }
        protected int ConnectFailRetries { get; private set; }
        protected int ProxyPort { get; private set; }
        protected string ProxyAuthScheme { get; private set; }
        protected string FailoverHost { get; private set; }
        protected bool ProxyAuthPreempt { get; private set; }
        protected bool VerifyHost { get; private set; }
        protected bool Encrypt { get; private set; }
        protected int MaxConnections { get; private set; }
        protected int ConnectLoopWaitMSecs { get; private set; }
        protected int CloseLingerSecs { get; private set; }
        protected int CloseIdleConnectionSecs { get; private set; }
        protected int FailoverPort { get; private set; }
        protected int FailoverDurationSecs { get; private set; }
        protected long MaxInBufferSize { get; private set; }
        protected long InBufferSize { get; private set; }

        protected static string LastHost { get; set; }
        protected static int LastPort { get; set; }
        protected static bool LastConnectFailed { get; set; }

        /// <summary>
        /// Constants for mime header
        /// </summary>
        protected const string MimeVersionHeader = "MIME-Version";
        protected const string MimeVersionDefault = "1.1";

        /// <summary>
        /// The following is the list of exceptions or WebException status name that, if received while
        /// attempting to connect, will cause a retry of the connect attempt
        /// This feature is only active if the <ConnectFailRetryReasons> element
        /// in the HTTPSConnectConfig.xml file is present and has exception and status names
        /// </summary>
        private string[] connectFailRetryReasons;
        protected string[] ConnectFailRetryReasons
        {
            get
            {
                if (connectFailRetryReasons == null)
                {
                    connectFailRetryReasons = new string[0];
                }
                return connectFailRetryReasons;
            }
        }

        /// <summary>
        /// Used only for argument to RemoteCertificateValidationCallback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="certificate"></param>
        /// <param name="chain"></param>
        /// <param name="sslPolicyErrors"></param>
        /// <returns></returns>
        public bool ValidateServerCertificate(
            object sender,
            X509Certificate certificate,
            X509Chain chain,
            SslPolicyErrors sslPolicyErrors)
        {
            // This is set up if VerifyHost is false which means we are okay with
            // a mismatched name
            if (sslPolicyErrors == SslPolicyErrors.None
                || sslPolicyErrors == SslPolicyErrors.RemoteCertificateNameMismatch)
            {
                return true;
            }

            //Console.WriteLine( "Certificate error: {0}", sslPolicyErrors );

            // Other errors we still want to fail
            return false;
        }

        /// <summary>
        /// dummy method for interface
        /// </summary>
        public void Close()
        {
            // not needed for this protocol because all interface objects
            // are one time use
        }

        public abstract CommArgs CompleteTransactionImpl(CommArgs request);

        /// <summary>
        /// This method implements the completeTransaction method
        /// which is the main public method for this class
        /// </summary>
        /// <param name="request">object with request to send, and
        /// options</param>
        /// <returns>object with response data.</returns>
        public CommArgs CompleteTransaction(CommArgs request)
        {
            return CompleteTransactionImpl(request);
        }

        /// <summary>
        /// Determines the correct host to use
        /// </summary>
        /// <param name="proxyUser">user</param>
        /// <param name="proxyPassword">password</param>
        /// <returns>The appropriate http interface object to use.</returns>
        protected virtual HttpWebObjectWrapper DetermineClient(ConfigurationData cdata,
        TransactionControlValues cvals, int dataLength)
        {
            var primaryHost = Host;
            var primaryPort = Port;
            var proxyUser = cdata["ProxyUserName"];
            var proxyPassword = cdata["ProxyPassword"];
            HttpWebObjectWrapper retVal;
            var currTime = Utils.GetCurrentMilliseconds();
            var useFailover = false;

            // see if we even have a failover option
            if (FailoverHost != null && FailoverPort > 0)
            {
                if (LastConnectFailed)
                {
                    if (LastHost == primaryHost &&
                    LastPort == primaryPort)
                    {
                        useFailover = true;

                        FailoverStart = currTime;
                    }
                    else
                    {
                        useFailover = false;

                        FailoverStart = 0;
                    }
                }
                else    // last connect was a success
                {
                    // check to see if it is time to return to
                    // primary
                    if (FailoverStart > 0 &&
                    FailoverDurationSecs * 1000
                    + FailoverStart > currTime)
                    {
                        useFailover = true;
                    }
                    else
                    {
                        useFailover = false;
                    }
                }
            }
            if (useFailover)
            {
                retVal = GetHttpClient(FailoverHost,
                FailoverPort, ProxyHost, ProxyPort,
                proxyUser, proxyPassword);
            }
            else
            {
                retVal = GetHttpClient(
                    primaryHost,
                    primaryPort,
                    ProxyHost,
                    ProxyPort,
                    proxyUser,
                    proxyPassword);
            }
            // populate the mime header
            PopulateMimeHeader(retVal, cdata, cvals, dataLength);
            return retVal;
        }
        /// <summary>
        /// This method checks the exception to see if we should try to
        /// connect again
        /// </summary>
        /// <param name="exception">exception that we got back when we tried to
        /// connect</param>
        /// <returns>true if we should try again, false otherwise.</returns>
        private bool RetryConnect(Exception exception)
        {
            var retVal = true;  // if nothing is specified, we always retry
            var baseEx = exception.GetBaseException();

            // When the <ConnectFailRetryReasons> element in the
            // HTTPSConnectConfig.xml file is set, we retry only for specific
            // exceptions and WebException statuses
            if (ConnectFailRetryReasons != null
                && ConnectFailRetryReasons.Length > 0)
            {
                var baseExType = baseEx.GetType();
                var status = "";

                // if something is specified in the reasons array then we don't
                // retry unless something matches
                retVal = false;

                if (baseEx is System.Net.WebException)
                {
                    var ex = (WebException)exception;

                    status = ex.Status.ToString();
                }

                var exceptionFullName = baseExType.FullName;
                var exceptionName = baseExType.Name;

                Logger.Debug("Retry handler got exception: "
                    + exceptionName);

                // Look to see if it is one of the exceptions or
                // statuses that should cause us to retry
                for (var i = 0; i < ConnectFailRetryReasons.Length;
                    i++)
                {
                    if (ConnectFailRetryReasons[i] ==
                        exceptionName ||
                        ConnectFailRetryReasons[i] ==
                        exceptionFullName ||
                        ConnectFailRetryReasons[i] ==
                        status)
                    {
                        retVal = true;
                        break;
                    }
                }
            }

            return retVal;
        }

        /// <summary>
        /// Connect and send request
        /// </summary>
        /// <param name="request">data and options for request</param>
        /// <returns>object we used to send the request.</returns>
        protected HttpWebObjectWrapper DoRequest(CommArgs request)
        {
            var cdata = request.Config;
            var cvals = request.ControlValues;

            // this will be the return value
            HttpWebObjectWrapper httpObject;

            var requestBytes = request.Data;

            try
            {
                httpObject = DoConnect(cdata, cvals,
                    request.Data.Length);
            }
            catch (Exception ex)
            {
                if (Logger.IsDebugEnabled)
                {
                    CommUtils.PrintException(ex);
                }

                var reason = " Got Exception: ";
                WebException wex;
                var err = Error.ConnectFailure;
                var externalErr = 0;

                reason += ex.GetType().FullName;

                // see if we got the special web exception that
                // has an informative status in it
                if (ex is WebException)
                {
                    wex = (WebException)ex;
                    externalErr = (int)wex.Status;
                    reason += " with status: " +
                        wex.Status;

                    switch (wex.Status)
                    {
                        case WebExceptionStatus.Timeout:
                            err = Error.ConnectTimeoutFailure;
                            break;
                        case WebExceptionStatus.TrustFailure:
                            err = Error.TrustFailure;
                            break;
                    }
                }
                CommException newEx;
                if (ex is CommException)
                {
                    newEx = (CommException)ex;
                }
                else
                {
                    newEx = new CommException(err,
                        "Failed to connect. " + reason);
                }
                if (externalErr != 0)
                {
                    newEx.ExternalError = externalErr;
                    // WebExceptionStatus is an operating system
                    // generated error
                    newEx.ExternalErrorDomain = CommException.ErrorDomain.OperatingSystem;
                }
                throw newEx;
            }
            // if connect succeeded
            if (httpObject != null)
            {
                try
                {
                    //DumpMessage( httpObject, requestBytes );
                    httpObject.Write(requestBytes, 0, requestBytes.Length);
                    Logger.Debug("Sent Mime Header:\r\n" + GetMimeHeader(httpObject));
                }
                catch (Exception ex)
                {
                    if (Logger.IsDebugEnabled)
                    {
                        CommUtils.PrintException(ex);
                    }
                    // strange but true, an exception while
                    // writing can have a response in it.
                    if (ex is WebException wex)
                    {
                        httpObject.Response = (HttpWebResponse)
                            wex.Response;
                        Logger.Debug("WebException status is: " + wex.Status +
                            " with value: " + (int)wex.Status);
                    }
                    // this will always throw an exception but it
                    // will look closely at what it has before
                    // doing so
                    CreateException(httpObject, ex, true);
                }
            }
            return httpObject;
        }

        /// <summary>
        /// used for debugging
        /// </summary>
        /// <param name="webObj"></param>
        /// <param name="data"></param>
        private void DumpMessage(HttpWebObjectWrapper webObj, byte[] data)
        {
            CommUtils.DumpToFile("requestHeader.data", Utils.StringToByteArray(GetMimeHeader(webObj)), false);
            CommUtils.DumpToFile("requestBody.data", data, false);
        }

        private string GetMimeHeader(HttpWebObjectWrapper webObj)
        {
            var headers = webObj.RequestHeaders;
            var arry = headers.AllKeys;
            var str = new StringBuilder();
            foreach (var curr in arry)
            {
                str.Append(curr + ": "
                + headers.Get(curr) + "\r\n");
            }
            str.Append("\r\n");
            return str.ToString();
        }

        /// <summary>
        /// Connect to the remote host
        /// </summary>
        /// <param name="pman">security info</param>
        /// <param name="dataLength">length of the data we need to send</param>
        /// <returns>new connection stream.</returns>
        private HttpWebObjectWrapper DoConnect(ConfigurationData cdata, TransactionControlValues cvals, int dataLength)
        {
            HttpWebObjectWrapper httpObject = null;
            var connectSuccess = false;
            for (var i = 0; i <= ConnectFailRetries && !connectSuccess; i++)
            {
                try
                {
                    // see if we need to talk to the primary
                    // server or the failover
                    httpObject = DetermineClient(cdata, cvals, dataLength);
                }
                catch (Exception ex)
                {
                    var message = "Got exception: " +
                        ex.GetType().Name +
                        " with message: " + ex.Message;
                    Logger.Warn(message);
                    throw new CommException(
                        Error.InitializationFailure,
                        message +
                        "\nFailed to create httpclient", ex);
                }
                try
                {
                    //DumpHeader( httpObject );
                    connectSuccess = httpObject.Connect();
                }
                catch (Exception ex)
                {
                    if (ex is WebException wex)
                    {
                        Logger.Debug("WebException status is: " + wex.Status +
                            " with value: " + (int)wex.Status);
                    }
                    // test to see if this is an exception
                    // for which we should retry, if not,
                    // throw the exception
                    if (RetryConnect(ex))
                    {
                        // make sure we haven't reached our
                        // retry limit already
                        if (i >= ConnectFailRetries)
                        {
                            ClientFailure(httpObject);
                            throw;
                        }
                    }
                    // we have been told not to retry this exception
                    else
                    {
                        ClientFailure(httpObject);
                        throw;
                    }
                }
                if (!connectSuccess && ConnectLoopWaitMSecs > 0)
                {
                    Thread.Sleep(ConnectLoopWaitMSecs);
                    Logger.Debug("Retrying HTTPS request . . .");
                }
            }
            return httpObject;
        }

        /// <summary>
        /// Look at response that came back
        /// </summary>
        /// <param name="httpObject">http request object</param>
        /// <returns>response data.</returns>
        protected byte[] DoResponse(HttpWebObjectWrapper httpObject)
        {
            byte[] resp = null;  // return value
            try
            {
                // this will throw an exception if the
                // stream was auto closed which happens (for
                // example) when a proxy authentication fails
                httpObject.GetResponse();
                if (!httpObject.ResponseReceived)
                {
                    throw new CommException(Error.ReadFailure,
                        "Null returned from GetResponse()");
                }
                Logger.Debug("Received Mime Header Response:\r\n" + GetMimeHeader(httpObject));
                if (httpObject.ResponseContentLength > MaxInBufferSize)
                {
                    throw new CommException(Error.BadDataError, "Response Data Length: " +
                    httpObject.ResponseContentLength + " greater than maximum allowed: " + MaxInBufferSize);
                }
                resp = GetResponseBody(httpObject);
                if (resp == null)
                {
                    throw new CommException(Error.ReadFailure,
                        "Null returned from GetStreamMessage()");
                }
            }
            catch (Exception ex)
            {
                if (Logger.IsDebugEnabled)
                {
                    CommUtils.PrintException(ex);
                }
                if (ex is WebException wex)
                {
                    httpObject.Response = (HttpWebResponse)wex.Response;
                    Logger.Debug("WebException status is: " + wex.Status +
                        " with value: " + (int)wex.Status);
                }
                // build a nice exception
                CreateException(httpObject, ex, false);
            }
            return resp;
        }

        /// <summary>
        /// </summary>
        /// <returns>body of message returned.</returns>
        private byte[] GetResponseBody(HttpWebObjectWrapper httpObject)
        {
            var blockList = new Queue();
            byte[] retVal = null;
            ulong total = 0;
            int numRead;

            do
            {
                // create a new buffer for each bunch of data
                var inbuf = new byte[InBufferSize];

                // try to read a full buffer full of data
                numRead = httpObject.Read(inbuf, 0, (int)InBufferSize);

                // if we got some data, save it
                if (numRead > 0)
                {
                    // if we did a partial read, then make a new
                    // (smaller) buffer for it
                    if (numRead < InBufferSize)
                    {
                        // make new buffer smaller to fit less
                        // data
                        var newbuf = new byte[numRead];

                        // copy bytes from larger buffer to new
                        // smaller one
                        for (var i = 0; i < numRead; i++)
                        {
                            newbuf[i] = inbuf[i];
                        }

                        // put this new smaller buffer on the
                        // queue
                        blockList.Enqueue(newbuf);
                    }
                    else // normal full buffer of data
                    {
                        blockList.Enqueue(inbuf);
                    }

                    // keep a running total
                    total += (ulong)numRead;

                    if (total > (ulong)MaxInBufferSize)
                    {
                        throw new CommException(Error.BadDataError,
                        "Response length exceeded maximum: " + MaxInBufferSize);
                    }
                }
            }
            while (numRead > 0); // quit if we read zero bytes.R

            // create a byte buffer that will hold all the data
            if (total > 0)
            {
                retVal = new byte[total];

                // make an iterator to go through the input blocks
                var myEnumerator = blockList.GetEnumerator();
                var baseIndex = 0;
                // loop through the list and add each buffer to the complete buffer
                while (myEnumerator.MoveNext())
                {
                    var buf = (byte[])myEnumerator.Current;
                    for (var i = 0; i < buf.Length; i++, baseIndex++)
                    {
                        retVal[baseIndex] = buf[i];
                    }
                }
            }

            var contentLength = (ulong)httpObject.ResponseContentLength;

            if (total != contentLength)
            {
                Logger.Warn("content-length mime header value was " + contentLength
                + " but we read " + total + " bytes from the body of the response message");
            }

            return retVal;
        }

        protected void AddMimeHeader(TransactionControlValues control, string mapName,
            HttpWebObjectWrapper httpClient, string mimeName)
        {
            var value = control[mapName];
            if (value != null && value.Length > 0)
            {
                httpClient.RequestHeaders.Add(mimeName, value);
            }
        }

        /// <summary>
        /// Throws appropriate exception when a failure happens
        /// </summary>
        /// <param name="httpObject">response object</param>
        /// <param name="origEx">the exception we are evaluating</param>
        /// <param name="isWrite">is this exception from a "write"
        /// operation</param>
        private void CreateException(HttpWebObjectWrapper httpObject,
            Exception origEx, bool isWrite)
        {
            string respStr = null;
            var respCode = HttpStatusCode.OK;
            var externalCode = 0;
            WebHeaderCollection hdr = null;
            var wStatus = GetWebExceptionStatus(origEx);

            if (httpObject != null)
            {
                try
                {
                    respCode = httpObject.StatusCode;
                    // Get the headers associated with the response.
                    hdr = httpObject.ResponseHeaders;
                }
                catch { }

                if (hdr != null)
                {
                    respStr = hdr.Get("Error-Reason");
                    try
                    {
                        externalCode = int.Parse(
                            hdr.Get("Error-Code"));
                    }
                    catch { }  // ignore errors
                }
            }
            // if we didn't get a string out of the response
            if (respStr == null)
            {
                respStr = origEx.Message;
            }
            // if the "Message" was null also
            if (respStr == null)
            {
                respStr = "Unknown Error";
            }
            CommException ex;
            // Look for the special response code that the gateway gives
            // us back
            if (respCode == HttpStatusCode.PreconditionFailed)
            {
                ex = new CommException(Error.GatewayFailure,
                    respStr, origEx);
            }
            // if it is a CommException, don't wrap it again in another
            // CommException
            else if (origEx is CommException)
            {
                ex = (CommException)origEx;
            }
            // wrap it in a CommException
            else
            {
                var err = respCode == HttpStatusCode.OK ? TranslateError(origEx, isWrite, wStatus) : Error.HTTPSFailure;
                ex = new CommException(err, respStr, origEx);
            }
            ex.StatusCode = (int)respCode;
            ex.ExternalError = externalCode;
            ex.ExternalErrorDomain = CommException.ErrorDomain.RemoteApplication;
            ex.WindowsError = (int)wStatus;
            // set the error domain based on whether we got an http response or not
            ex.ExternalErrorDomain = respCode != HttpStatusCode.OK
                ? CommException.ErrorDomain.CommunicationProtocol
                : CommException.ErrorDomain.OperatingSystem;
            throw ex;
        }

        /// <summary>
        /// Translate the WebException status to a MSDK error
        /// </summary>
        /// <param name="ex">original exception</param>
        /// <param name="isWrite">was it a write to the socket that
        /// generated this exception?</param>
        /// <returns>MSDK error equivalent.</returns>
        private Error TranslateError(Exception ex, bool isWrite, WebExceptionStatus winErr)
        {
            var retVal = Error.UnknownFailure;

            switch (winErr)
            {
                case WebExceptionStatus.Timeout:
                    retVal = isWrite ? Error.WriteTimeoutFailure : Error.ReadTimeoutFailure;
                    break;
                // Sometimes in .NET 2.0 we can get this failure when
                // it really is a timeout
                case WebExceptionStatus.ReceiveFailure:
                    var bex = ex.GetBaseException();
                    retVal = Error.ReadFailure;
                    if (bex is SocketException)
                    {
                        var sex = (SocketException)bex;

                        // check for socket timeout
                        if (SocketError.TimedOut.Equals(sex.ErrorCode))
                        {
                            retVal =
                            Error.ReadTimeoutFailure;
                            break;
                        }
                    }
                    break;
                case WebExceptionStatus.ConnectFailure:
                    retVal = Error.ConnectFailure;
                    break;
                case WebExceptionStatus.TrustFailure:
                    retVal = Error.TrustFailure;
                    break;
                case WebExceptionStatus.ProtocolError:
                    retVal = Error.HTTPSFailure;
                    break;
                default:
                    retVal = isWrite ? Error.WriteFailure : Error.ReadFailure;
                    break;
            }
            return retVal;
        }

        /// <summary>
        /// Get the external error out of the base WebException
        /// </summary>
        /// <param name="ex">original exception</param>
        /// <returns>MSDK error equivalent.</returns>
        private WebExceptionStatus GetWebExceptionStatus(Exception ex)
        {
            var retVal = WebExceptionStatus.Success;
            WebException wex;
            switch (ex) {
                case null: return retVal;
                case WebException webException:
                {
                    wex = webException;
                    retVal = wex.Status;
                    break;
                }
                default:
                {
                    var innerEx = ex.InnerException;
                    if (!(innerEx is WebException exception))
                    {
                        return retVal;
                    }
                    wex = exception;
                    retVal = wex.Status;
                    break;
                }
            }
            return retVal;
        }

        /// <summary>
        /// Create a HttpWebObjectWrapper object and initialize it
        /// </summary>
        /// <param name="hostname">host to connect to</param>
        /// <param name="portnum">port to connect to</param>
        /// <param name="proxyHost">proxy host (if there is one)</param>
        /// <param name="proxyPort">proxy port (if there is one)</param>
        /// <param name="proxyUser">user to authenticate to proxy</param>
        /// <param name="proxyPassword">password for authenticating to proxy</param>
        /// <returns>new HttpWebObjectWrapper object.</returns>
        protected HttpWebObjectWrapper GetHttpClient(
            string hostname,
            int portnum,
            string proxyHost,
            int proxyPort,
            string proxyUser,
            string proxyPassword)
        {
            var scheme = Encrypt ? "https" : "http";
            var myUri = new Uri($"{scheme}://{hostname}:{portnum}{AuthorizationURI}");
            Logger.Debug("URI is: " + myUri);
            var retVal = HttpWebObjectWrapper.GetInstance();
            // this calls the HttpWebRequest.Create()
            retVal.Initialize(myUri);
            if (proxyHost != null)
            {
                var proxy = new WebProxy("http://" + proxyHost + ":" + proxyPort, true);
                retVal.Proxy = proxy;
                // Create a NetworkCredential object and associate it
                // with the Proxy property of request object.
                proxy.Credentials = new NetworkCredential(proxyUser, proxyPassword);
                retVal.Proxy = proxy;
            }
            // We can't just leave the "Proxy" unset, if we don't set it to the
            // empty one, windows will use the Internet Explorer's database to
            // find one.
            else
            {
                retVal.Proxy = WebRequest.DefaultWebProxy = null;
            }
            // if this is set to zero it means "don't time out"
            if (TimeoutSecs == 0)
            {
                retVal.Timeout = -1;
            }
            else
            {
                retVal.Timeout = TimeoutSecs * 1000;
            }
            retVal.Expect100Continue = false;
            retVal.KeepAlive = false;
            retVal.AllowAutoRedirect = true;
            retVal.ConnectionLimit = MaxConnections;
            retVal.Method = "POST";
            return retVal;
        }

        /// <summary>
        /// Whenever a communication succeeds (from a connectivity standpoint)
        /// then call this method
        /// </summary>
        /// <param name="httpObject">last used request object</param>
        protected void ClientSuccess(HttpWebObjectWrapper httpObject)
        {
            LastConnectFailed = false;
            SetLastHost(httpObject);
        }

        /// <summary>
        /// Same as above but when failure
        /// </summary>
        /// <param name="httpObject">last used request object</param>
        protected void ClientFailure(HttpWebObjectWrapper httpObject)
        {
            LastConnectFailed = true;
            try
            {
                httpObject.Close();
            }
            catch (Exception)
            {
                // ignore any exception while trying to close
            }
            SetLastHost(httpObject);
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="httpObject"></param>
        private void SetLastHost(HttpWebObjectWrapper httpObject)
        {
            if (httpObject == null)
            {
                return;
            }
            LastHost = httpObject.Host;
            LastPort = httpObject.Port;
        }

        /// <summary>
        /// Put all the required fields in the mime header
        /// </summary>
        /// <param name="httpObject">object that will send the request</param>
        /// <param name="pman">security info</param>
        /// <param name="contentLength">length of data</param>
        public abstract void PopulateMimeHeader(
            HttpWebObjectWrapper httpObject, ConfigurationData cdata, TransactionControlValues cvals, int contentLength);
    }
}
