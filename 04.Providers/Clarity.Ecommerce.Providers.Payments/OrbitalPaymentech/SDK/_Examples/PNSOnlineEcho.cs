namespace PNSOnlineEcho
{
    using System;
    using JPMC.MSDK;

    internal class PNSOnlineEcho
    {
        private static string mid = "123456";
        private static string tid = "001";
        private static bool EHP = false; // Enhanced Host Processing, usually off

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
                // eg: dispatcher = new Dispatcher( "/opt/JPMC_MSDK_X.X.X");
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
                // Make the HCS transaction
                var request = MakeTransaction(dispatcher);
                // Send the HCS transaction
                var resp = dispatcher.ProcessRequest(request);
                // Verify the response
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
        /// Puts an exception catching wrapper on the GetField method to get a value in the response
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
        /// Looks at the fields that are important in evaluating a response
        /// </summary>
        /// <param name="resp"></param>
        private static void CheckResponse(IResponse resp)
        {
            var retVal = false;
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
            }
            Console.WriteLine("************************Dumping response object************************");
            Console.WriteLine(resp.DumpFieldValues());
            if (!retVal)
            {
                throw new Exception("Got bad response code: " + respCode + " so quitting.");
            }
        }

        /// <summary>
        /// Make the auth request message
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private static IRequest MakeTransaction(IDispatcher dispatcher)
        {
            // Create a new online transaction request object
            // The request object uses the XML templates along with field values
            // to construct a online transaction message for processing.
            var request = dispatcher.CreateRequest("PNSNewTransaction", "PNSConnect");
            // Use the Config method to get or set any communication-
            // specific configuration values for this transaction.
            // Any changes you make to the config here will ONLY apply
            // to this transaction.
            request.Config["HostProcessingSystem"] = "HCS";
            // The following fields are specific to this particular transaction
            request[PNS.MessageHeader.MessageType] = "1800";
            request[PNS.Bit12.TimeLocalTrans] = "000000";
            request[PNS.Bit13.DateLocalTrans] = "04302029";
            request[PNS.Bit37.RetrievalReferenceNumber] = "000000000000";
            request[PNS.Bit41.CardAcquirerTerminalId] = tid;
            request[PNS.Bit42.CardAcquirerId] = mid;
            request[PNS.Bit60.A1.AttendedTerminalData] = "01";
            request[PNS.Bit60.A1.TerminalLocation] = "01";
            request[PNS.Bit60.A1.CardholderAttendance] = "01";
            request[PNS.Bit60.A1.CardPresentIndicator] = "1";
            request[PNS.Bit60.A1.CardholderActivatedTerminalInformation] = "06";
            request[PNS.Bit60.A1.TerminalEntryCapability] = "05";
            // don't put in mid/tid and bit 62 T1 if Enhanced Host Processing is
            // turned on
            if (!EHP)
            {
                request[PNS.Bit41.CardAcquirerTerminalId] = tid;
                request[PNS.Bit42.CardAcquirerId] = mid;

                // below we specify the full path to an element name using the PNS
                // class
                request[PNS.Bit62.T1.ApplicationName] = "EchoApp";
                request[PNS.Bit62.T1.EPROMVersionNumber] = "0000000000";
                request[PNS.Bit62.T1.ReleaseDate] = "043029";
            }
            // the following tells it to echo
            request[PNS.Bit70.NetworkManagementInformationCode] = "301";
            Console.WriteLine("************************Dumping request object************************");
            Console.WriteLine(request.DumpFieldValues());
            return request;
        }
    }
}
