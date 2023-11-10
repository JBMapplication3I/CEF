namespace PNSOnlineAuth
{
    using System;
    using JPMC.MSDK;

    internal class PNSOnlineAuth
    {
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
                // Create a new online transaction request object
                // The request object uses the XML templates along with field values
                // to construct a online transaction message for processing.
                var request = dispatcher.CreateRequest("PNSNewTransaction", "HTTPSConnectPNS");
                // Use the Config property to get or set any communication-
                // specific configuration values for this transaction.
                // Any changes you make to the config here will ONLY apply
                // to this transaction.
                request.Config["UserName"] = "SDKUSER";
                request.Config["UserPassword"] = "SDKPWORD";
                request.Config["ProxyUserName"] = "PROXYUSER";
                request.Config["ProxyUserPassword"] = "PROXYPWORD";
                request.Config["HostProcessingSystem"] = "TCS";
                // The following fields are specific to this particular transaction
                request[PNS.MessageHeader.MessageType] = "1100";
                request[PNS.Bit2.PrimaryAccountNumber] = "4788250000028291";
                request[PNS.Bit3.TransactionType] = "00";
                request[PNS.Bit3.AccountTypeFrom] = "91";
                request[PNS.Bit3.AccountTypeTo] = "00";
                request[PNS.Bit4.TransactionAmount] = "000000000100";
                request[PNS.Bit12.TimeLocalTrans] = "140739";
                request[PNS.Bit13.DateLocalTrans] = "03082012";
                request[PNS.Bit14.DateExpire] = "1212";
                request[PNS.Bit22.POSEntryMode] = "010";
                request[PNS.Bit25.POSConditionCode] = "00";
                request[PNS.Bit37.RetrievalReferenceNumber] = "000000000003";
                request[PNS.Bit41.CardAcquirerTerminalId] = "001";
                request[PNS.Bit42.CardAcquirerId] = "45645";
                request[PNS.Bit60.A1.AttendedTerminalData] = "01";
                request[PNS.Bit60.A1.TerminalLocation] = "01";
                request[PNS.Bit60.A1.CardholderAttendance] = "01";
                request[PNS.Bit60.A1.CardPresentIndicator] = "1";
                request[PNS.Bit60.A1.CardholderActivatedTerminalInformation] = "06";
                request[PNS.Bit60.A1.TerminalEntryCapability] = "05";
                Console.WriteLine("\n************************Dumping request object************************");
                Console.WriteLine(request.DumpFieldValues());
                Console.WriteLine("\n************************Process the request object************************");
                var resp = dispatcher.ProcessRequest(request);
                // Check for any conversion error
                if (resp.IsConversionError)
                {
                    Console.WriteLine("\n************************Error while converting the response************************");
                    Console.WriteLine("There is data that the SDK didn't expect. These must have been added after");
                    Console.WriteLine("this version of the SDK was released. Consider upgrading to the latest version.");
                    Console.WriteLine(resp.LeftoverData);
                }
                // Use the response field name to get the value from the response object.
                // Refer to the SDK documentation for return formats & data elements.
                var respCode = GetTextField(resp, PNSResp.Bit39.ResponseCode);
                var retVal = false;
                if (respCode == "00")
                {
                    // Good response
                    Console.WriteLine("\n************************Successfully processed the request************************");
                    retVal = true;
                }
                else
                {
                    Console.WriteLine("\n************************Failed to process the request****************************");
                    Console.WriteLine("Response code =>" + respCode);
                    Console.WriteLine("HostResponseText: " + GetTextField(resp, PNSResp.Bit62.H2.HostResponseText));
                    Console.WriteLine("HostResponseErrorNumber: " + GetTextField(resp, PNSResp.Bit62.H3.HostResponseErrorNumber));
                }
                Console.WriteLine("\n************************Dumping response object************************");
                Console.WriteLine(resp.DumpFieldValues());
                if (!retVal)
                {
                    throw new Exception("Got bad response code: " + respCode + " so quitting.");
                }
                // Some fields are not always returned in a response.
                // You can test if the field exists before using it.
                if (resp.HasField(PNSResp.Bit38.AuthorizationIDResponse))
                {
                    Console.WriteLine("Auth ID is: " + resp.GetField(PNSResp.Bit38.AuthorizationIDResponse));
                }
                Console.WriteLine("\n************************Dumping response object************************");
                Console.WriteLine(resp.DumpFieldValues());
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
            var retVal = "";
            try
            {
                retVal = resp.GetField(field);
            }
            catch (ResponseException ex)
            {
                Console.WriteLine("No " + field + " to get.");
                Console.WriteLine("Error Code=" + ex.ErrorCode);
                throw;
            }
            return retVal;
        }
    }
}
