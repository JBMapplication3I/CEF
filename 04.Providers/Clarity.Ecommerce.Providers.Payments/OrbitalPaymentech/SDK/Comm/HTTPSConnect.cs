// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Comm
{
    using System;
    using Configurator;
    using Framework;

    /// Title: HTTPS Connect Communication Module
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
    public sealed class HTTPSConnect : HTTPSBase
    {
        /// <summary>
        /// Constructor - Sets private fields
        /// </summary>
        /// <param name="configurator">contents of HTTPSConnectConfig.xml</param>
        public HTTPSConnect(ConfigurationData cdata)
            : base(cdata)
        {
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public override CommArgs CompleteTransactionImpl(CommArgs request)
        {
            var firstTry = true; // set to false on second attempt
            byte[] resp;
            HttpWebObjectWrapper httpObject = null;
            // This loop facilitates failover by allowing a switch to
            // the failover host and port
            for (var i = 0; i < 2; i++)
            {
                // First connect and send the request
                try
                {
                    Logger.Debug($"Sending {request.GetDataLength()} bytes");
                    httpObject = DoRequest(request);
                    ClientSuccess(httpObject);
                }
                catch (Exception ex)
                {
                    ClientFailure(httpObject);
                    // if we only tried once, try again
                    if (firstTry && FailoverHost != null
                        && FailoverPort != 0)
                    {
                        if (LastHost != null)
                        {
                            Logger.Warn($"Failed using host: {LastHost}");
                        }
                        Logger.Warn("Retrying using alternate host");
                        firstTry = false;
                        continue;
                    }
                    // just throw "as is"
                    if (ex is CommException exception)
                    {
                        throw exception;
                    }
                    // wrap it in a CommException
                    throw new CommException(
                        Error.UnknownFailure,
                        ex.Message, ex);
                }
                // if we got here we succeeded in sending the request
                break;
            }
            // Now get the response
            // we don't failover because failing over a "read" could cause
            // a dup
            try
            {
                resp = DoResponse(httpObject);
                if (resp != null)
                {
                    Logger.Debug("Received " +
                        resp.Length + " bytes");
                }
                else
                {
                    Logger.Debug("DoResponse returned null");
                }
            }
            catch (CommException)
            {
                ClientFailure(httpObject);
                throw;
            }
            // if other exception then wrap it in a CommException
            catch (Exception ex)
            {
                ClientFailure(httpObject);
                throw new CommException(Error.UnknownFailure,
                    ex.Message, ex);
            }
            //Console.WriteLine("GOT RESPONSE: " + Utils.ByteArrayToString(resp));
            var response = new CommArgs(resp);
            ClientSuccess(httpObject);
            httpObject.Close();
            return response;
        }

        /**
         * This method creates and initializes the http method object
         * @param httpClient HttpWrapper
         * @param request CommArgs
         * @return HttpPost
         * @throws CommException
         */
        public override void PopulateMimeHeader(HttpWebObjectWrapper httpClient,
            ConfigurationData config, TransactionControlValues control, int contentLength)
        {
            httpClient.ContentType = control["ContentType"];

            // populate the mime headers
            httpClient.RequestHeaders.Set(MimeVersionHeader, MimeVersionDefault);

            // these two only happen if it is Orbital and trace number is set
            AddMimeHeader(control, "TraceNumber", httpClient, "Trace-Number");
            AddMimeHeader(control, "MerchantID", httpClient, "Merchant-ID");

            // only Orbital will have metrics in the control args
            AddMimeHeader(control, "Interface-Version", httpClient, "Interface-Version");

            // PNS has both auth mid and tid, salem has only auth mid
            AddMimeHeader(control, "AuthMid", httpClient, "Auth-MID");

            // we have to check whether this one is there because SALEM protocol
            // does not have this in the header
            AddMimeHeader(control, "AuthTid", httpClient, "Auth-TID");

            AddMimeHeader(control, "ContentTransferEncoding", httpClient, "Content-Transfer-Encoding");
            AddMimeHeader(control, "RequestNumber", httpClient, "Request-Number");
            AddMimeHeader(control, "DocumentType", httpClient, "Document-type");
            AddMimeHeader(control, "StatelessTransaction", httpClient, "Stateless-Transaction");

            /*
             * Specific headers required by the netconnect gateway but not by Orbital
             */
            if (config["UserName"] != null)
            {
                httpClient.RequestHeaders.Add("Auth-User", config["UserName"]);
                httpClient.RequestHeaders.Add("Auth-Password", config["UserPassword"]);
            }
        }
        protected override string ModuleName => "HTTPSConnect";
    }
}  // end of namespace

