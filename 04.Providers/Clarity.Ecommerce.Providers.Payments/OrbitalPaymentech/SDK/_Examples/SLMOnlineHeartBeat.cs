namespace SLMOnlineHeartBeat
{
    using System;
    using JPMC.MSDK;

    internal class SLMOnlineHeartBeat
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
                // Create a new online transaction request object
                // The request object uses the XML templates along with field values
                // to construct a online transaction message for processing.
                var request = dispatcher.CreateRequest("HeartBeat", "TCPConnect");
                // These particular config values are for HTTPSConnect requests only.
                // request.Config["UserName"] = "yournetconnectusername";
                // request.Config["UserPassword"] = "yournetconnectpassword";
                // request.Config["ProxyUserName"] = "yournetproxyusername";
                // request.Config["ProxyUserPassword"] = "yourproxypassword";
                // Process the heart beat message.
                // All of the fields required for the heart beat are set automatically.
                var resp = dispatcher.SendHeartbeat(request);
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
                Console.WriteLine("Heartbeat Sequence Number =>" + resp[SLMResp.Online.MerchantHeartBeatResponse.SequenceNumber]);
                Console.WriteLine("Heartbeat GMT Time =>" + resp[SLMResp.Online.MerchantHeartBeatResponse.GMTTime]);
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
