namespace NCMOnlineAuth
{
    using System;
    using System.Text;
    using JPMC.MSDK;
    using JPMC.MSDK.Common;

    internal class NCMOnlineAuth
    {
        public static void Main(string[] args)
        {
            IDispatcher dispatcher;
            MessageProcessor request;
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
                request = new MessageProcessor();
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
                var message = "P74VABC123456789DEF       AX371234567890123    " +
                        "120600001234560000000075758401    AU AZ03079     USFR 9876\r";
                // Get the configuration to use for this transaction. This
                // specifies which config file to use.
                var config = dispatcher.GetConfig("HTTPSConnectSLM");
                // Now we can set some values in the config, which
                // override the values in the config file.
                config["Host"] = "wsvar1.chasepaymentech.com";
                //config[ "Port" ] = "5555";
                config["UserName"] = "yournetproxyusername";
                config["UserPassword"] = "yourproxypassword";
                // Set whichever properties are required for the
                // type of message you want to send. The Merchant ID
                // is required for SLM.
                request.MerchantID = "123456";
                Console.WriteLine("************************Process the request object************************");
                var resp = request.Process(Encoding.ASCII.GetBytes(message), config);
                var str = Utils.ByteArrayToString(resp);
                Console.WriteLine(str);
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
            catch (Exception e)
            {
                Console.WriteLine("************************Exception while processing the request************************");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
