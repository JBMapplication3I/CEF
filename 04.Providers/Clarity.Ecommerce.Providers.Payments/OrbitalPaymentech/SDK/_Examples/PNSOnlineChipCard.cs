namespace PNSOnlineChipCard
{
    using System;
    using JPMC.MSDK;
    using JPMC.MSDK.Common;

    internal class PNSOnlineChipCard
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
                request.Config["HostProcessingSystem"] = "HCS";
                // The following fields are specific to this particular transaction
                request[PNS.MessageHeader.MessageType] = "1200";
                request[PNS.Bit2.PrimaryAccountNumber] = "4176661000001015";
                request[PNS.Bit3.TransactionType] = "00";
                request[PNS.Bit3.AccountTypeFrom] = "91";
                request[PNS.Bit3.AccountTypeTo] = "00";
                request[PNS.Bit4.TransactionAmount] = "000000000100";
                request[PNS.Bit12.TimeLocalTrans] = "121000";
                request[PNS.Bit13.DateLocalTrans] = "01202012";
                request[PNS.Bit18.MerchantCategoryCode] = "5999";
                request[PNS.Bit22.POSEntryMode] = "051";
                request[PNS.Bit23.EMVContactlessPANSeqNumber] = "01";
                request[PNS.Bit25.POSConditionCode] = "00";
                request[PNS.Bit35.Track2Data] = "4176661000001015=121120110425788";
                request[PNS.Bit37.RetrievalReferenceNumber] = "100000000037";
                request[PNS.Bit41.CardAcquirerTerminalId] = "001";
                request[PNS.Bit42.CardAcquirerId] = "4568";
                request[PNS.Bit48.C8.CreditDebitPrepaidSupportIndicator] = "Y";
                request[PNS.Bit48.D1.DataEntrySource] = "36";
                // This is the key to using Bit55's ChipCardData field.
                // The binary data must be converted to a hexadecimal string
                // with its length prepended to that string.
                var hexStr = Utils.ByteArrayToHex(ChipCardBinaryData);
                request[PNS.Bit55.ChipCardData] = hexStr.Length + hexStr;
                request[PNS.Bit60.A1.AttendedTerminalData] = "00";
                request[PNS.Bit60.A1.TerminalLocation] = "00";
                request[PNS.Bit60.A1.CardholderAttendance] = "00";
                request[PNS.Bit60.A1.CardPresentIndicator] = "0";
                request[PNS.Bit60.A1.CardholderActivatedTerminalInformation] = "00";
                request[PNS.Bit60.A1.TerminalEntryCapability] = "01";
                request[PNS.Bit62.P1.HardwareVendorIdentifier] = "0024";
                request[PNS.Bit62.P1.SoftwareIdentifier] = "001C";
                request[PNS.Bit62.P1.HardwareSerialNumber] = "40001234";
                request[PNS.Bit62.P2.Bit2.P2HostProcessingPlatform] = "40";
                request[PNS.Bit62.P2.Bit3.P3MessageFormatSupport1] = "80";
                request[PNS.Bit62.P2.Bit4.P4EMVSupport] = "FF";
                request[PNS.Bit62.P2.Bit5.P5PeripheralSupport1] = "80";
                request[PNS.Bit62.P2.Bit7.P7CommunicationInformation1] = "10";
                request[PNS.Bit62.P2.Bit9.P9IndustryInformation1] = "06";
                request[PNS.Bit62.P2.Bit11.P11ClassComplianceCertification] = "60";
                Console.WriteLine("************************Dumping request object************************");
                Console.WriteLine(request.DumpFieldValues());
                // send the transaction
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
                var respCode = GetTextField(resp, PNSResp.Bit39.ResponseCode);
                if (respCode == "00" || respCode == "99" || respCode == "14")
                {
                    Console.WriteLine("************************Successfully processed the request************************");
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
                // Some fields are not always returned in a response.
                // You can test if the field exists before using it.
                if (resp.HasField(PNS.Bit38.AuthorizationIDResponse))
                {
                    Console.WriteLine("Auth ID is: " + GetTextField(resp, PNSResp.Bit38.AuthorizationIDResponse));
                }
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
        /// This is just sample binary data. It has no real value,
        /// and is used only to help illustrate how to set binary
        /// data into the Bit55.ChipCardData field.
        /// </summary>
        private static byte[] ChipCardBinaryData
        {
            get
            {
                byte[] chipData = {
                    0x9F, 2, 6, 0, 0, 0, 0, 1, 0, 0x9F, 3, 6, 0, 0, 0, 0, 0, 0, 0x9F, 0x26, 8, 0x38, 0xF3,
                    0xCD, 0x41, 0xC2, 0xF8, 0x72, 0x3B, 0x82, 2, 0x5C, 0, 0x9F, 0x36, 2, 0, 4, 0x9C, 1, 0,
                    0x9F, 0x10, 7, 6, 1, 0x0A, 3, 0xA4, 0x20, 0, 0x9F, 0x33, 3, 0xE0, 0xB8, 0xC8, 0x9F,
                    0x1A, 2, 7, 0x24, 0x9A, 3, 0x12, 3, 0x12, 0x9F, 0x35, 1, 0x22, 0x95, 5, 0, 0, 0, 0, 0, 0x5F, 0x2A, 2, 9,
                    0x78, 0x9F, 0x21, 3, 0x16, 0x14, 9, 0x9F, 0x37, 4, 0x2A, 0x66, 0x75, 0x9E
                };
                return chipData;
            }
        }
    }
}
