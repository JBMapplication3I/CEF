namespace JPMC.MSDK.Framework
{
    using System;
    using Comm;
    using Common;
    using Configurator;
    using Converter;

    /// <exclude />
    /// <summary>
    /// Processes all online requests for both protocols.
    /// </summary>
    public class OnlineProcessor : DispatcherBase, IOnlineProcessor
    {
        private static int heartbeatSequenceNumber;
        private bool setHostIsDisabled = false;
        private bool getHostIsDisabled = false;
        private bool setPortIsDisabled = false;

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="factory"></param>
        public OnlineProcessor(IDispatcherFactory factory)
        {
            this.factory = factory;
        }

        /// <summary>
        ///
        /// </summary>
        public static void Reset()
        {
            heartbeatSequenceNumber = 0;
        }

        private void ValidateMidAndTid(IRequestImpl request)
        {
            string authMid;
            string authTid = null;
            var padLength = 10;

            if (request.MessageFormat == MessageFormat.ORB)
            {
                return;
            }

            // This only matters for HTTPS, so return if it isn't.
            if (request.Config.Protocol != CommModule.HTTPSConnect && request.Config.Protocol != CommModule.HTTPSUpload)
            {
                return;
            }

            var skip = false;
            var authMidName = "DivisionNumber";
            try
            {
                if (request.MessageFormat == MessageFormat.PNS)
                {
                    authMid = request["Bit42.CardAcquirerId"];
                    authTid = request["Bit41.CardAcquirerTerminalId"];
                    authMidName = "Bit42.CardAcquirerId";
                    padLength = 12;
                    skip = request["MessageHeader.MessageType"].Equals("1800");
                    if (skip)
                    {
                        authMid = authMid ?? "";
                        authTid = authTid ?? "";
                    }
                }
                else
                {
                    authMid = request["DivisionNumber"];
                }
            }
            catch (RequestException ex)
            {
                throw new DispatcherException(ex);
            }

            if (authMid == null && !skip)
            {
                Logger.Debug(authMidName + " must be set in the Request.");
                throw new DispatcherException(Error.InvalidField, authMidName + " must be set in the Request.");
            }

            if (request.MessageFormat == MessageFormat.PNS && authTid == null && !skip)
            {
                Logger.Debug("Bit41.CardAcquirerTerminalId must be set in the Request.");
                throw new DispatcherException(Error.InvalidField, "Bit41.CardAcquirerTerminalId must be set in the Request.");
            }

            // Make sure there are 10 digits
            authMid = Utils.PadLeft(authMid, '0', padLength);
            request.TransactionControlValues["AuthMid"] = authMid;
            if (request.MessageFormat == MessageFormat.PNS)
            {
                authTid = Utils.PadLeft(authTid, '0', 3);
                request.TransactionControlValues["AuthTid"] = authTid;
            }
        }

        #region IOnlineProcessor Members
        /// <exclude />
        /// <summary>
        /// Turns the data of the supplied Request object into the appropriate payload format
        /// and then passes it to the communication layer of the SDK.
        /// </summary>
        /// <remarks>
        /// The method will then block until the comm layer returns the server's response. The processRequest
        /// will convert the response payload into a Response object and return it to the client.
        ///
        /// The one task that is not so apparent is that it must verify that the response received belongs to
        /// the request that was sent. The converter will handle most of this by supplying a message format
        /// string for each payload, and then comparing them in the call to validateResponse.
        /// </remarks>
        /// <param name="req">The Request object created by the client.</param>
        /// <returns>A client-friendly object containing the data of the server's response.</returns>
        public IResponse ProcessRequest(IRequest req)
        {
            var request = (IRequestImpl)req;
            request.SetDefaultValues();
            var converter = factory.MakeOnlineConverter(req.Config, request.MessageFormat);
            ValidateMidAndTid(request);
            SetHost(request);
            if (request.MessageFormat == MessageFormat.ORB)
            {
                request.TransactionControlValues["ContentType"] = "application/" + factory.GetOrbitalSchema().PTIVersion;
                request.TransactionControlValues["MerchantID"] = request["MerchantID"];
            }
            var reqArgs = converter.ConvertRequest(request);
            string requestOrderNumber = null;
            var skipConsistencyCheck = false;
            try
            {
                if (request.TransactionType == "NewTransaction")
                {
                    requestOrderNumber = request["MerchantOrderNumber"];
                }
                else
                {
                    skipConsistencyCheck = true;
                }
            }
            catch
            {
                skipConsistencyCheck = true;
            }
            LogRequestAndPayload(request, reqArgs);
            var returnArgs = CompleteTransaction(reqArgs.ReqByteArray, request.Config, request.TransactionControlValues, request.Metrics);
            var responsePayload = returnArgs.Data;
            var isPNS = request.Config.MessageFormat == "PNS";
            var respArgs = converter.ConvertResponse(responsePayload, isPNS);
            if (!isPNS)
            {
                ValidateResponse(reqArgs.Format, respArgs.Format, converter);
            }
            var response = respArgs.ResponseData;
            LogResponseAndPayload(respArgs);
            if (!skipConsistencyCheck && requestOrderNumber != response["MerchantOrderNumber"])
            {
                factory.Comm.Close();
                var msg = "Request/Response inconsistency.";
                Logger.Error(msg);
                throw new DispatcherException(Error.TransactionInconsistency, msg);
            }
            GetHostOrPort(response, request.Config, "!h", "Host");
            GetHostOrPort(response, request.Config, "!p", "Port");
            var persist = request.Config.GetBool("PersistConnection", false);
            if ((request.Config.Protocol == CommModule.TCPConnect || request.Config.Protocol == CommModule.PNSConnect)
                && !request.Config.GetBool("PersistConnection", false))
            {
                factory.Comm.Close();
            }
            return response;
        }

        private void LogRequestAndPayload(IRequest request, ConverterArgs args)
        {
            var detailLogger = factory.DetailLogger;
            if (!detailLogger.IsDebugEnabled)
            {
                return;
            }
            try
            {
                // The request payload that is sent to the server.
                detailLogger.Debug("Request: [" + args.MaskedRequest + "]");
                // The response object given back to the client.
                detailLogger.Debug("[" + request.DumpMaskedFieldValues() + "]");
            }
            catch (Exception ex)
            {
                Logger.Warn("Failed to convert the masked request.", ex);
            }
        }

        /// <exclude />
        /// <summary>
        /// Sends a heartbeat request to the server.
        /// </summary>
        /// <remarks>
        /// This
        /// method validates the request parameter to make sure it is
        /// correct for the operation and then sends it to the appropriate
        /// OnlineProcessor.
        /// </remarks>
        /// <param name="request">A Request object that must be using the HeartBeat template.</param>
        /// <returns>An IResponse object that reports the result of the request.</returns>
        public IResponse SendHeartbeat(IRequest request)
        {
            string sequenceNumber = null;
            try
            {
                sequenceNumber = request["SequenceNumber"];
            }
            catch
            {
                // Do Nothing
            }
            // Set the SequenceNumber if it's not set.
            if (string.IsNullOrEmpty(sequenceNumber))
            {
                heartbeatSequenceNumber++;
                var sequence = heartbeatSequenceNumber.ToString().PadLeft(8, '0');
                request["SequenceNumber"] = sequence;
            }
            // Set the CurrentGMTTime field.
            string time = null;
            try
            {
                time = request["CurrentGMTTime"];
            }
            catch
            {
                // Do Nothing
            }
            if (string.IsNullOrEmpty(time))
            {
                time = DateTime.UtcNow.ToString("ddHHmmss");
                request["CurrentGMTTime"] = time;
            }
            return ProcessRequest(request);
        }
        #endregion

        #region Private Methods
        /// <summary>This method wraps the call to CommManager.completeTransaction, and then determines what to do with
        /// the result.</summary>
        /// <remarks>In most cases, the result is simply returned as-is. However, there is a case in which the result
        /// must be handled differently:
        /// <p>
        /// If the response from the proxy begins with "!e", then an exception was thrown by the proxy and a
        /// DispatcherException must be thrown to handle it.
        /// The format is:<br/>
        /// !exceptionname\rmessage\rhost\rport\r<br/><br/>
        /// where "exceptionname" is a string that specifies what type of exception was thrown, "message" is the
        /// exception's message, "host" is the host that the transaction was being sent to, and "port" is the port the
        /// transaction was being sent on. Currently, the port field is being ignored and is there for future use. The
        /// host field is being set into via the DispatcherException.setHost method. The message is the message of the
        /// new exception, and the exceptionname will map to a DispatcherIF.Error enum.
        /// </p>
        /// <p>
        /// There is one special exception that must be handled differently. This is the "ProxyRequestTimeout" which has
        /// special handling. This is when a timeout to the Stratus occurs.
        /// </p></remarks>
        /// <param name="payload">      The message to be sent to the server.</param>
        /// <param name="configData">   Information describing the configuration.</param>
        /// <param name="controlValues">The control values.</param>
        /// <param name="metrics">      The metrics.</param>
        /// <returns>A string containing the response from the server.</returns>
        public CommArgs CompleteTransaction(
            byte[] payload, ConfigurationData configData, TransactionControlValues controlValues, SDKMetrics metrics)
        {
            if (metrics != null)
            {
                if (controlValues == null)
                {
                    Logger.Error("The TransactionControlValues is null.");
                    throw new DispatcherException(Error.NullParameter, "The TransactionControlValues is null.");
                }
                controlValues["Interface-Version"] = metrics.ToBase64();
            }
            var args = new CommArgs(payload, configData, controlValues);
            try
            {
                var result = factory.Comm.CompleteTransaction(args);
                var data = result.Data;
                var stringData = Utils.ByteArrayToString(data);
                if (stringData.StartsWith("!e"))
                {
                    HandleProxyException(stringData);
                }
                return result;
            }
            catch (CommException ex)
            {
                throw new DispatcherException(ex);
            }
        }

        private void SetHost(IRequest request)
        {
            if (request.Config.Protocol != CommModule.TCPConnect && request.Config.Protocol != CommModule.PNSConnect)
            {
                return;
            }
            var host = request.Host;
            if (!setHostIsDisabled && host != null)
            {
                Logger.Debug("Setting GetHost in the proxy");
                var response = SendProxyMessage("host", "!H", host, request.Config);
                if (response == null || !response.StartsWith("!H"))
                {
                    factory.Logger.InfoFormat("Bad response for Set Host, Response={0}", response);
                }
            }
            var port = request.Port;
            if (!setPortIsDisabled && port != null)
            {
                Logger.Debug("Setting GetPort in the proxy");
                var response = SendProxyMessage("host", "!P", port, request.Config);
                if (response == null || !response.StartsWith("!P"))
                {
                    factory.Logger.InfoFormat("Bad response for Set Port, Response={0}", response);
                }
            }
        }

        private void GetHostOrPort(IResponse response, ConfigurationData configData, string proxyMessage, string name)
        {
            if (configData.Protocol != CommModule.TCPConnect && configData.Protocol != CommModule.PNSConnect)
            {
                return;
            }
            var getHost = configData.GetBool("GetHost", false);
            if (getHostIsDisabled || !getHost)
            {
                return;
            }
            Logger.Debug("Get" + name + " Enabled");
            var receive = SendProxyMessage("host", proxyMessage, "", configData);
            var hostName = receive.Substring(2).Trim();
            if (name == "Host")
            {
                factory.SetResponseHost(hostName, response);
            }
            else
            {
                factory.SetResponsePort(hostName, response);
            }
            Logger.DebugFormat("Setting response {0} to [{1}]", name.ToLower(), hostName);
        }

        private string SendProxyMessage(string name, string message, string val, ConfigurationData configData)
        {
            var payload = $"{message}{val}\r";
            factory.Logger.DebugFormat("Sending message to the proxy: {0}", payload);
            var receive = Utils.ByteArrayToString(
                CompleteTransaction(Utils.StringToByteArray(payload), configData, null, null).Data);
            // Did the request go out to the Stratus?
            // If so, then we're not using the proxy, and we should disable
            // this feature.
            if (receive.StartsWith("T1"))
            {
                var msg = "MSDK is not configured to use the Proxy. GetHost and GetPort only work with the proxy. " +
                    "Disable GetHost and GetPort in the configuration file.";
                Logger.Error(msg);
                throw new DispatcherException(Error.ProxyException, msg);
            }
            if (!receive.StartsWith("!"))
            {
                var failureMessage = "GetHost returned an invalid value. Set GetHost and GetPort only when using the MSDK Proxy.";
                Logger.Error(failureMessage);
                throw new DispatcherException(Error.InvalidRequest, failureMessage);
            }
            if (!receive.StartsWith(message))
            {
                Logger.WarnFormat("Unexpected response from the proxy while trying to get the host");
                Logger.WarnFormat("Response was: {0}", receive);
                return null;
            }
            return receive;
        }

        private void HandleProxyException(string result)
        {
            factory.Comm.Close();
            var parms = result.Substring(2).Split('\r');
            var hostName = "UNKNOWN";
            var port = "UNKNOWN";
            if (parms.Length > 2)
            {
                hostName = parms[2];
            }
            if (parms.Length > 3)
            {
                port = parms[3];
            }
            string msg;
            if (parms[0] == "ProxyRequestTimeout")
            {
                msg = $"The MSDK Proxy timed out while trying to read the response from host:port \"{hostName}:{port}\".";
                Logger.Error(msg);
                throw new DispatcherException(Error.ProxyTimeoutToStratus, msg, hostName, port, null);
            }
            msg = "An unknown exception was thrown by the proxy.";
            if (parms[1].Length > 0)
            {
                msg = parms[1];
            }
            if (hostName == "UNKNOWN" || hostName.Length == 0)
            {
                hostName = null;
            }
            Logger.Error(msg);
            throw new DispatcherException(Error.ProxyException, msg);
        }

        /// <summary>
        /// There is a possibility that, in certain situations, the response that the
        /// Dispatcher receives from completeTransaction does not belong to the
        /// request that was sent into the method. This method will have the
        /// converter verify whether or not the response belongs to the request. If
        /// it does, then the method will do nothing. If it does not match, then we
        /// must close the connection to the CommManager, because it is out of synch,
        /// and throw an appropriate exception.
        ///
        /// The most likely situation where this could happen is when the SDK is
        /// being used without a communication proxy and server-initiated heartbeats
        /// is turned on. Since the proxy is responsible for throwing out heartbeat
        /// messages, a heartbeat message that is received while waiting for a
        /// request response when no proxy is running will cause the Comm component
        /// to return the heartbeat as a response to the request.
        ///</summary>
        ///
        /// <param name="sendFormat">The special format string for the request
        /// generated by the converter.</param>
        /// <param name="receiveFormat">The special format string for the response
        /// generated by the converter.</param>
        /// <param name="converter">The converter object to use for matching.</param>
        private void ValidateResponse(string sendFormat, string receiveFormat, IOnlineConverter converter)
        {
            if (!converter.MessageMatches(sendFormat, receiveFormat))
            {
                factory.Comm.Close();
                var msg =
                    "No response was received. The request may or may not have succeeded.";
                Logger.Error(msg);
                throw new DispatcherException(Error.NoResponseReceivedError, msg);
            }
        }
        #endregion
    }
}
