namespace SLMOnlineAuth
{
    using System;
    using JPMC.MSDK;

    internal class SLMOnlineAuth
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
                var request = dispatcher.CreateRequest("NewTransaction", "TCPConnect");
                // These particular config values are for HTTPSConnect requests only.
                // request.Config[ "Host" ] = "localhost";
                // request.Config[ "Port" ] = "5556";
                // request.Config[ "UserName" ] = "yournetconnectusername";
                // request.Config[ "UserPassword" ] = "yournetconnectpassword";
                // request.Config[ "ProxyUserName" ] = "yournetproxyusername";
                // request.Config[ "ProxyUserPassword" ] = "yourproxypassword";
                // Assign values for each available fields in NewTransaction
                request[SLM.NewTransaction.MerchantOrderNumber] = "Simple Auth";
                request[SLM.NewTransaction.MethodOfPayment] = "VI";
                request[SLM.NewTransaction.AccountNumber] = "897580000000006";
                request[SLM.NewTransaction.ExpirationDate] = "1107";
                request[SLM.NewTransaction.DivisionNumber] = "123456";
                request[SLM.NewTransaction.Amount] = "5300";
                request[SLM.NewTransaction.CurrencyCode] = "840";
                request[SLM.NewTransaction.TransactionType] = "1";
                request[SLM.NewTransaction.ActionCode] = "AU";
                // Assign  values for each available fields in BillToAddress
                request[SLM.NewTransaction.BillToAddress.TelephoneType] = "D";
                request[SLM.NewTransaction.BillToAddress.TelephoneNumber] = "12312312312316";
                request[SLM.NewTransaction.BillToAddress.NameText] = "MISTER *TEST1";
                request[SLM.NewTransaction.BillToAddress.AddressLine1] = "1231 AVS STEEET";
                request[SLM.NewTransaction.BillToAddress.CountryCode] = "US";
                request[SLM.NewTransaction.BillToAddress.City] = "LOUISVILLE";
                request[SLM.NewTransaction.BillToAddress.State] = "KY";
                request[SLM.NewTransaction.BillToAddress.PostalCode] = "4021312345";
                // Additional formats can also be added using the existing request object
                // and referencing fields using their fully qualified names
                // NOTE: The SLM class] = used above] = generates strings in this format.
                // This way of specifying field Object Identifiers can be useful for fields
                // that are not in the SLM class (like for new fields introduced in a
                // micro release).
                request["ShipToAddress.NameText"] = "JOE USER";
                request["ShipToAddress.AddressLine1"] = "1 MAIN ST";
                request["ShipToAddress.City"] = "MIAMI";
                request["ShipToAddress.State"] = "FL";
                Console.WriteLine("************************Dumping request object************************");
                Console.WriteLine(request.DumpFieldValues());
                Console.WriteLine("************************Process the request object************************");
                var resp = dispatcher.ProcessRequest(request);
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
                var respCode = resp.GetIntField(SLMResp.Online.Response.ResponseReasonCode);
                if (respCode >= 100 && respCode <= 199)
                {
                    Console.WriteLine("\n************************Successfully processed the request************************");
                }
                else
                {
                    Console.WriteLine("\n************************Failed to process the request****************************");
                }
                Console.WriteLine("Response code =>" + resp[SLMResp.Online.Response.ResponseReasonCode]);
                Console.WriteLine("Auth verification code =>" + resp[SLMResp.Online.Response.AuthVerificationCode]);
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
    }
}
