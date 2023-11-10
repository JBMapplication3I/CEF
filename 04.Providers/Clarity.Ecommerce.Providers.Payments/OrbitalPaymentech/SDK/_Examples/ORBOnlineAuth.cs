namespace ORBOnlineAuth
{
    using System;
    using JPMC.MSDK;

    internal class ORBOnlineAuth
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
                var request = dispatcher.CreateRequest("NewOrder", "HTTPSConnectORB");
                // Config data
                //request.Config[ "Host" ] = "wsvar1.chasepaymentech.com";
                //request.Config[ "Port" ] = "5555";
                // Use the Config property to get or set any communication-
                // specific configuration values for this transaction.
                // Any changes you make to the config here will ONLY apply
                // to this transaction.
                request[ORB.NewOrder.OrbitalConnectionUsername] = "TEST";
                request[ORB.NewOrder.OrbitalConnectionPassword] = "TEST";
                // Basic Information
                request[ORB.NewOrder.CurrencyCode] = "840";
                request[ORB.NewOrder.IndustryType] = "EC";
                request[ORB.NewOrder.MessageType] = "A";
                request[ORB.NewOrder.MerchantID] = "041756";
                request[ORB.NewOrder.BIN] = "000001";
                request[ORB.NewOrder.OrderID] = "122003SA";
                request[ORB.NewOrder.AccountNum] = "5191409037560100";
                request[ORB.NewOrder.Amount] = "25000";
                request[ORB.NewOrder.Exp] = "1209";
                request[ORB.NewOrder.TerminalID] = "001";
                // AVS Information
                request[ORB.NewOrder.AVSname] = "Jon Smith";
                request[ORB.NewOrder.AVSaddress1] = "4200 W Cypress St";
                request[ORB.NewOrder.AVScity] = "Tampa";
                request[ORB.NewOrder.AVSstate] = "FL";
                request[ORB.NewOrder.AVSzip] = "33607";
                // Common Optional
                request[ORB.NewOrder.Comments] = "This is Java SDK";
                request[ORB.NewOrder.ShippingRef] = "FEDEX WB12345678 Pri 1";
                // PC 2 Data
                request[ORB.NewOrder.TaxInd] = "1";
                request[ORB.NewOrder.Tax] = "100";
                request[ORB.NewOrder.PCOrderNum] = "PO8347465";
                request[ORB.NewOrder.PCDestZip] = "33607";
                request[ORB.NewOrder.PCDestName] = "Terry";
                request[ORB.NewOrder.PCDestAddress1] = "88301 Teak Street";
                request[ORB.NewOrder.PCDestAddress2] = "Apt 5";
                request[ORB.NewOrder.PCDestCity] = "Hudson";
                request[ORB.NewOrder.PCDestState] = "FL";
                Console.WriteLine("************************Dumping request object************************");
                Console.WriteLine(request.DumpFieldValues());
                Console.WriteLine("************************Process the request object************************");
                var resp = dispatcher.ProcessRequest(request);
                // Use the response field name to get the value from the response object.
                // Refer to the SDK documentation for return formats & data elements.
                if (resp.ResponseType == "QuickResp")
                {
                    Console.WriteLine("\n************************Quick response************************");
                    Console.WriteLine("ProcStatus=" + resp[ORBResp.QuickResp.ProcStatus]);
                    Console.WriteLine("StatusMsg=" + resp[ORBResp.QuickResp.StatusMsg]);
                }
                if (resp.HasField(ORBResp.NewOrderResp.ProcStatus))
                {
                    var respCode = resp.GetField("NewOrderResp.RespCode");
                    if (respCode == "0")
                    {
                        Console.WriteLine("\n************************Successfully processed the request************************");
                        Console.WriteLine("Auth code =>" + resp["NewOrderResp.AuthCode"]);
                    }
                    else
                    {
                        Console.WriteLine("\n************************Failed to process the request****************************");
                    }
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
    }
}
