namespace PNSUploadQryArrayResp
{
    using System;
    using JPMC.MSDK;

    internal class PNSUploadQryArrayResp
    {
        private static string mid = "123456";
        private static string tid = "001";
        private static readonly int batchOption = 1;

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
                // Make and send the HCS transaction, then verify that it was successful
                var request = MakeTransaction(dispatcher);
                var resp = dispatcher.ProcessRequest(request);
                var success = CheckResponse(resp);
                Console.WriteLine("\n************************Dumping array data************************");
                // In rare cases, a response may come with more than one instance of a subtag.
                // In this sample, that subtag is Bit62.D1.Account. The three ways in
                // which you can work with this array are shown below.
                if (!success || batchOption <= 0)
                {
                    return;
                }
                // Option #1: Get an array of values
                // Get an array of values for a field.
                var auths = resp.GetFieldArray("Bit62.D1.Account.AuthorizationCode");
                var types = resp.GetFieldArray("Bit62.D1.Account.CardType");
                Console.WriteLine("Getting arrays of field values:");
                for (var i = 0; i < auths.Length; i++)
                {
                    Console.WriteLine(" CardType: " + types[i] + " had auth code: " + auths[i]);
                }
                // Option #2: Use special version of getField()
                // Get individual field values by specifying the array index as a argument to getField.
                Console.WriteLine("\nGetting individual elements of an array:");
                string auth;
                string type;
                for (var i = 0; i < resp.GetCount("Bit62.D1.Account.AuthorizationCode"); i++)
                {
                    auth = resp.GetField("Bit62.D1.Account.AuthorizationCode", i);
                    type = resp.GetField("Bit62.D1.Account.CardType", i);
                    Console.WriteLine(" CardType: " + type + " had auth code: " + auth);
                }
                // Option #3: Specify array indexer in Field Identifier.
                // Get individual field values by treating the Field Identifier as an array.
                Console.WriteLine("\nGetting individual elements of an array by FieldID:");
                auth = resp.GetField("Bit62.D1.Account[0].AuthorizationCode");
                type = resp.GetField("Bit62.D1.Account[0].CardType");
                Console.WriteLine(" CardType: " + type + " had auth code: " + auth);
                auth = resp.GetField("Bit62.D1.Account[1].AuthorizationCode");
                type = resp.GetField("Bit62.D1.Account[1].CardType");
                Console.WriteLine(" CardType: " + type + " had auth code: " + auth);
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
            catch (ResponseException)
            {
                Console.WriteLine("No " + field + " to get.");
                throw;
            }
        }

        /// <summary>
        /// Look at the typical fields of interest in the response
        /// </summary>
        /// <param name="resp"></param>
        /// <returns></returns>
        private static bool CheckResponse(IResponse resp)
        {
            var success = false;
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
            var respCode = GetTextField(resp, "ResponseCode");
            if (respCode == "00")
            {
                Console.WriteLine("\n************************Successfully processed the request************************");
                success = true;
            }
            else
            {
                Console.WriteLine("\n************************Failed to process the request****************************");
                Console.WriteLine("Response code =>" + respCode);
                Console.WriteLine("HostResponseText: " + GetTextField(resp, "HostResponseText"));
                Console.WriteLine("HostResponseErrorNumber: " + GetTextField(resp, "HostResponseErrorNumber"));
            }
            Console.WriteLine("\n************************Dumping response object************************");
            Console.WriteLine(resp.DumpFieldValues());
            return success;
        }

        /// <summary>
        /// Create a Host Capture message
        /// </summary>
        /// <param name="dispatcher"></param>
        /// <returns></returns>
        private static IRequest MakeTransaction(IDispatcher dispatcher)
        {
            // Create a new online transaction request object
            // The request object uses the XML templates along with field values
            // to construct a online transaction message for processing.
            var request = dispatcher.CreateRequest("PNSNewTransaction", "HTTPSConnectPNS");
            // Use the getConfig() method to get or set any communication-
            // specific configuration values for this transaction.
            // Any changes you make to the config here will ONLY apply
            // to this transaction.
            SetProtocolVariables(request);
            // The following fields are specific to this particular transaction
            request[PNS.MessageHeader.MessageType] = "1500";
            request[PNS.Bit3.TransactionType] = "91";
            request[PNS.Bit3.AccountTypeFrom] = "00";
            request[PNS.Bit3.AccountTypeTo] = "00";
            request[PNS.Bit12.TimeLocalTrans] = "104510";
            request[PNS.Bit13.DateLocalTrans] = "07182012";
            request[PNS.Bit37.RetrievalReferenceNumber] = "000000000146";
            request[PNS.Bit41.CardAcquirerTerminalId] = tid;
            request[PNS.Bit42.CardAcquirerId] = mid;
            // Enhanced batch query
            if (batchOption > 0)
            {
                request[PNS.Bit48.BT.CardTypeRequested] = "99"; // all card types
                // 1 tran detail, 2 batch totals, 3 merchant totals
                request[PNS.Bit48.BT.InquiryType] = "" + batchOption;
                request[PNS.Bit48.R5.RequestStatus] = "I";
            }
            request[PNS.Bit62.T1.ApplicationName] = "PNS SDK";
            request[PNS.Bit62.T1.ReleaseDate] = "021512";
            request[PNS.Bit62.T1.EPROMVersionNumber] = "0123456789";
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
            request.Config["HostProcessingSystem"] = "HCS";
        }
    }
}
