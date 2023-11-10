namespace PNSUploadArrayReq
{
    using System;
    using System.Threading;
    using JPMC.MSDK;

    internal class PNSUploadArrayReq
    {
        private static string authID;
        private static string mid = "123456";
        private static string tid = "001";
        private static string batchnum = "000021"; // increment this with each upload

        internal static void Main(string[] args)
        {
            IDispatcher dispatcher;
            try
            {
                // Create a Dispatcher - This is your entry point to  the SDK.
                // The Dispatcher loads its properties from configuration files in the
                // MSDK_HOME/config directory. The Dispatcher looks for the "config"
                // directory under the directory specified by the MSDK_HOME environment
                // variable. If no MSDK_HOME environment variable
                // exists, pass the absolute path to msdk home to the
                // Dispatcher constructor
                // eg: dispatcher = new Dispatcher( "C:\\JPMC_MSDK_X.X.X");
                dispatcher = new Dispatcher();
            }
            catch (DispatcherException e)
            {
                // Catch any dispatcher initialization exception
                Console.WriteLine("************************Exception while creating Dispatcher************************");
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error Code=" + e.ErrorCode);
                throw;
            }
            try
            {
                // make the auth transaction
                var request = MakeAuth(dispatcher);
                // send the auth transaction
                var resp = dispatcher.ProcessRequest(request);
                CheckResponse(resp);
                authID = GetTextField(resp, PNS.Bit38.AuthorizationIDResponse);
                Console.Write("Sleeping . . .");
                // sleep a few seconds to allow batch server to catch up
                try
                {
                    Thread.Sleep(3000);
                }
                catch
                {
                    // Do Nothing
                }
                // first do header
                Console.WriteLine("\nSending batch header");
                request = MakeHeader(dispatcher);
                resp = dispatcher.StartPNSUpload(request);
                CheckResponse(resp);
                // followed by body
                Console.WriteLine("\nSending batch body");
                request = MakeCapture(dispatcher);
                resp = dispatcher.ProcessPNSUpload(request);
                CheckResponse(resp);
                // then trailer
                Console.WriteLine("\nSending batch trailer");
                request = MakeTrailer(dispatcher);
                resp = dispatcher.EndPNSUpload(request);
                CheckResponse(resp);
            }
            catch (DispatcherException e)
            {
                // All of the SDK exceptions have an error code that you can test
                // to determine how to handle a particular error.
                Console.WriteLine("************************Exception while processing the request************************");
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error Code=" + e.ErrorCode);
                if (e.ErrorCode == Error.ConnectFailure)
                {
                    // Perform connect error handling...
                }
                throw;
            }
            catch (RequestException e)
            {
                Console.WriteLine("************************Exception while processing the request************************");
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error Code=" + e.ErrorCode);
                throw;
            }
            catch (ResponseException e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error Code=" + e.ErrorCode);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("************************Exception while processing the request************************");
                Console.WriteLine(e.ToString());
                throw;
            }
        }

        /// <summary>
        /// Puts an exception catching wrapper on the getField method to get a value in the response
        /// </summary>
        /// <param name="resp"></param>
        /// <param name="field"></param>
        /// <returns></returns>
        private static string GetTextField(IResponse resp, string field)
        {
            try
            {
                return resp.GetField(field);
            }
            catch (ResponseException ex)
            {
                Console.WriteLine("No " + field + " to get.");
                Console.WriteLine("Error Code=" + ex.ErrorCode);
                throw;
            }
        }

        /// <summary>
        /// Look at the typical fields of interest in the response
        /// </summary>
        /// <param name="resp"></param>
        private static void CheckResponse(IResponse resp)
        {
            var retVal = false;
            var isCode23 = false;
            // Check for any conversion error
            if (resp.IsConversionError)
            {
                Console.WriteLine("************************Error while converting the response************************");
                Console.WriteLine("There is data that the SDK didn't expect. These must have been added after");
                Console.WriteLine("this version of the SDK was released. Consider upgrading to the latest version.");
                Console.WriteLine(resp.LeftoverData);
            }
            // Use the response field name to get the value from the response object.
            // Refer to the SDK documentation for return formats & data elements.
            var respCode = GetTextField(resp, PNSResp.Bit39.ResponseCode);
            if (respCode == "00")
            {
                Console.WriteLine("************************Successfully processed the request************************");
                retVal = true;
            }
            else
            {
                Console.WriteLine("************************Failed to process the request****************************");
                Console.WriteLine("Response code =>" + respCode);
                Console.WriteLine("HostResponseText: " + GetTextField(resp, PNSResp.Bit62.H2.HostResponseText));
                Console.WriteLine("HostResponseErrorNumber: " + GetTextField(resp, PNSResp.Bit62.H3.HostResponseErrorNumber));
                isCode23 = respCode == "23";
            }
            Console.WriteLine("************************Dumping response object************************");
            Console.WriteLine(resp.DumpFieldValues());
            if (retVal)
            {
                return;
            }
            if (!isCode23)
            {
                throw new Exception("Got bad response code: " + respCode + " so quitting.");
            }
            Console.WriteLine("Response Code of 23 typically means that an upload with the batch number you used");
            Console.WriteLine("has already been sent today. The batch number must be changed for each batch in");
            Console.WriteLine("a given day.");
            throw new Exception("Got bad response code: " + respCode + " so quitting.");
        }

        /// <summary>
        /// Make the auth request message
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private static IRequest MakeAuth(IDispatcher dispatcher)
        {
            // Create a new online transaction request object
            // The request object uses the XML templates along with field values
            // to construct a online transaction message for processing.
            var request = dispatcher.CreateRequest("PNSNewTransaction", "HTTPSConnectPNS");
            // Use the Config property to get or set any communication-
            // specific configuration values for this transaction.
            // Any changes you make to the config here will ONLY apply
            // to this transaction.
            SetProtocolVariables(request);
            // The following fields are specific to this particular transaction
            request[PNS.MessageHeader.MessageType] = "1100";
            request[PNS.Bit2.PrimaryAccountNumber] = "4788250000028291";
            request[PNS.Bit3.TransactionType] = "00";
            request[PNS.Bit3.AccountTypeFrom] = "91";
            request[PNS.Bit3.AccountTypeTo] = "00";
            request[PNS.Bit4.TransactionAmount] = "000000001500";
            request[PNS.Bit12.TimeLocalTrans] = "140739";
            request[PNS.Bit13.DateLocalTrans] = "03082012";
            request[PNS.Bit14.DateExpire] = "1212";
            request[PNS.Bit22.POSEntryMode] = "010";
            request[PNS.Bit25.POSConditionCode] = "00";
            request[PNS.Bit37.RetrievalReferenceNumber] = "000000000004";
            request[PNS.Bit41.CardAcquirerTerminalId] = tid;
            request[PNS.Bit42.CardAcquirerId] = mid;
            request[PNS.Bit60.A1.AttendedTerminalData] = "01";
            request[PNS.Bit60.A1.TerminalLocation] = "01";
            request[PNS.Bit60.A1.CardholderAttendance] = "01";
            request[PNS.Bit60.A1.CardPresentIndicator] = "1";
            request[PNS.Bit60.A1.CardholderActivatedTerminalInformation] = "06";
            request[PNS.Bit60.A1.TerminalEntryCapability] = "05";
            Console.WriteLine("************************Dumping request object************************");
            Console.WriteLine(request.DumpFieldValues());
            return request;
        }

        /// <summary>
        /// Make the upload/batch header message
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private static IRequest MakeHeader(IDispatcher dispatcher)
        {
            // Create a new online transaction request object
            // The request object uses the XML templates along with field values
            // to construct a online transaction message for processing.
            var request = dispatcher.CreateRequest("PNSNewTransaction", "HTTPSUploadPNS");
            // Use the Config property to get or set any communication-
            // specific configuration values for this transaction.
            // Any changes you make to the config here will ONLY apply
            // to this transaction.
            SetProtocolVariables(request);
            request[PNS.MessageHeader.MessageType] = "1500";
            request[PNS.Bit3.TransactionType] = "92";
            request[PNS.Bit3.AccountTypeFrom] = "00";
            request[PNS.Bit3.AccountTypeTo] = "00";
            request[PNS.Bit12.TimeLocalTrans] = "140739";
            request[PNS.Bit13.DateLocalTrans] = "03092012";
            request[PNS.Bit37.RetrievalReferenceNumber] = "000000000004";
            request[PNS.Bit41.CardAcquirerTerminalId] = tid;
            request[PNS.Bit42.CardAcquirerId] = mid;
            request[PNS.Bit62.B2.BatchUploadType] = "RU";
            request[PNS.Bit62.B2.BatchNumber] = batchnum;
            request[PNS.Bit62.B2.SalesTransactionCount] = "000001";
            request[PNS.Bit62.B2.TotalSalesAmount] = "000000001500";
            request[PNS.Bit62.B2.ReturnTransactionCount] = "000000";
            request[PNS.Bit62.B2.TotalReturnsAmount] = "000000000000";
            request[PNS.Bit62.T1.ApplicationName] = "PNS SDK";
            request[PNS.Bit62.T1.ReleaseDate] = "021512";
            request[PNS.Bit62.T1.EPROMVersionNumber] = "0123456789";
            Console.WriteLine("************************Dumping request object************************");
            Console.WriteLine(request.DumpFieldValues());
            return request;
        }

        /// <summary>
        /// Make the terminal capture message
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private static IRequest MakeCapture(IDispatcher dispatcher)
        {
            // Create a new online transaction request object
            // The request object uses the XML templates along with field values
            // to construct a online transaction message for processing.
            var request = dispatcher.CreateRequest("PNSNewTransaction", "HTTPSUploadPNS");
            // Use the Config property to get or set any communication-
            // specific configuration values for this transaction.
            // Any changes you make to the config here will ONLY apply
            // to this transaction.
            SetProtocolVariables(request);
            // The following fields are specific to this particular transaction
            request[PNS.MessageHeader.MessageType] = "1300";
            request[PNS.Bit2.PrimaryAccountNumber] = "4788250000028291";
            request[PNS.Bit3.TransactionType] = "00";
            request[PNS.Bit3.AccountTypeFrom] = "91";
            request[PNS.Bit3.AccountTypeTo] = "00";
            request[PNS.Bit4.TransactionAmount] = "000000001500";
            request[PNS.Bit11.SystemTraceAuditNumber] = "000001";
            request[PNS.Bit12.TimeLocalTrans] = "140739";
            request[PNS.Bit13.DateLocalTrans] = "03092012";
            request[PNS.Bit14.DateExpire] = "1212";
            request[PNS.Bit22.POSEntryMode] = "010";
            request[PNS.Bit25.POSConditionCode] = "00";
            request[PNS.Bit37.RetrievalReferenceNumber] = "000000000004";
            request[PNS.Bit38.AuthorizationIDResponse] = authID;
            request[PNS.Bit41.CardAcquirerTerminalId] = tid;
            request[PNS.Bit42.CardAcquirerId] = mid;
            request[PNS.Bit60.A1.AttendedTerminalData] = "01";
            request[PNS.Bit60.A1.TerminalLocation] = "01";
            request[PNS.Bit60.A1.CardholderAttendance] = "01";
            request[PNS.Bit60.A1.CardPresentIndicator] = "1";
            request[PNS.Bit60.A1.CardholderActivatedTerminalInformation] = "06";
            request[PNS.Bit60.A1.TerminalEntryCapability] = "05";
            request[PNS.Bit62.P1.HardwareVendorIdentifier] = "0018";
            request[PNS.Bit62.P1.HardwareSerialNumber] = "R1M00               ";
            request[PNS.Bit62.P1.SoftwareIdentifier] = "001C";
            request[PNS.Bit62.P2.Bit2.P2HostProcessingPlatform] = "0C";
            request[PNS.Bit62.P2.Bit3.P3MessageFormatSupport1] = "80";
            request[PNS.Bit62.P2.Bit5.P5PeripheralSupport1] = "40";
            request[PNS.Bit62.P2.Bit7.P7CommunicationInformation1] = "20";
            request[PNS.Bit62.P2.Bit9.P9IndustryInformation1] = "02";
            request[PNS.Bit62.P2.Bit11.P11ClassComplianceCertification] = "00";
            request[PNS.Bit48.P8.EnhancedAuthorizationRequestIndicator] = "00";
            request[PNS.Bit48.D1.DataEntrySource] = "45";
            request[PNS.Bit48.S1.CardType] = "VI";
            request[PNS.Bit48.S2.ActualResponseCode] = "00";
            request[PNS.Bit48.S2.AuthorizerResponseValues] = "01";
            // This is a case where you can have multiple instances of PromptData
            // The SDK supports this by allowing an "array" notation for PromptData.
            // It is zero-based, and works as shown below.
            // NOTE: The PNS helper class does not support this, so you must
            // specify the Field Identifier as a string, as shown here.
            request["Bit48.H1.PromptData[0].AmountType"] = "4U";
            request["Bit48.H1.PromptData[0].Amount"] = "300";
            request["Bit48.H1.PromptData[1].AmountType"] = "4S";
            request["Bit48.H1.PromptData[1].Amount"] = "400";
            Console.WriteLine("************************Dumping request object************************");
            Console.WriteLine(request.DumpFieldValues());
            return request;
        }

        /// <summary>
        /// Make the upload/batch trailer
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private static IRequest MakeTrailer(IDispatcher dispatcher)
        {
            // Create a new online transaction request object
            // The request object uses the XML templates along with field values
            // to construct a online transaction message for processing.
            var request = dispatcher.CreateRequest("PNSNewTransaction", "HTTPSUploadPNS");
            // Use the Config property to get or set any communication-
            // specific configuration values for this transaction.
            // Any changes you make to the config here will ONLY apply
            // to this transaction.
            SetProtocolVariables(request);
            // The following fields are specific to this particular transaction
            request[PNS.MessageHeader.MessageType] = "1500";
            request[PNS.Bit3.TransactionType] = "93";
            request[PNS.Bit3.AccountTypeFrom] = "00";
            request[PNS.Bit3.AccountTypeTo] = "00";
            request[PNS.Bit12.TimeLocalTrans] = "140739";
            request[PNS.Bit13.DateLocalTrans] = "03092012";
            request[PNS.Bit37.RetrievalReferenceNumber] = "000000000004";
            request[PNS.Bit41.CardAcquirerTerminalId] = tid;
            request[PNS.Bit42.CardAcquirerId] = mid;
            Console.WriteLine("************************Dumping request object************************");
            Console.WriteLine(request.DumpFieldValues());
            return request;
        }

        private static void SetProtocolVariables(IRequest request)
        {
            // These two values can be set in the configuration file for this
            // protocol or can be set at run-time as we are doing here
            request.Config["UserName"] = "SDKUSER";
            request.Config["UserPassword"] = "SDKPWORD";
            // set these if you have a firewall proxy to go through and it will
            // accept BASIC authentication
            request.Config["ProxyUserName"] = "PROXYUSER";
            request.Config["ProxyUserPassword"] = "PROXYPWORD";
            request.Config["HostProcessingSystem"] = "TCS";
        }
    }
}
