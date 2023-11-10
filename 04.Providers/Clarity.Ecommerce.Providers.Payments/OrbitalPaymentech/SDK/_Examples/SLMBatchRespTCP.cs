namespace SLMBatchRespTCP
{
    using System;
    using JPMC.MSDK;

    internal class SLMBatchRespTCP
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
                return;
            }
            try
            {
                // Send one RFR request for each outstanding batch response file
                // For number of retries & wait time between the retries, consult
                // merchant certification group.
                var rfrRequest = dispatcher.CreateRequest("SubmissionResponse", "TCPBatch");
                rfrRequest["PID"] = "12345";
                rfrRequest["PIDPassword"] = "PIDPW";
                rfrRequest["SID"] = "12345";
                rfrRequest["SIDPassword"] = "SIDPW";
                // Use the Config property to get or set any communication-
                // specific configuration values for this transaction.
                // Any changes you make to the config here will ONLY apply
                // to this submission operation.
                rfrRequest.Config["SubmissionFilePassword"] = "sdk";
                var batchResp = dispatcher.ReceiveResponse(rfrRequest, "sdk");
                if (batchResp == null)
                {
                    Console.WriteLine("************************No submission response found, contact customer support************************");
                    return;
                }
                // Get the submission attributes
                // Use the Name property to match the request file name & response file
                // name.
                Console.WriteLine("Response File Name =>" + batchResp.FileName);
                // Get the submission attributes
                // Use the Name property to match the request file name & response file
                // name.
                // So, if your submission was named "SubName", you can test it like this
                // to see if this response file belongs to it.
                if (batchResp.Name != "SubName")
                {
                    // This response file does not match the submission I want to
                    // process...
                    return;
                }
                Console.WriteLine("Response Name =>" + batchResp.Name);
                // loop thru the RFS file. This is the standard way to process a
                // response file.
                Console.WriteLine("************************Response order record************************");
                while (batchResp.HasNext)
                {
                    try
                    {
                        var respOrder = batchResp.GetNext();
                        if (respOrder == null)
                        {
                            // Seems we got a null response object, skip and
                            // This shouldn't happen. Perform any error handling
                            // here.
                            // For this sample, we'll just skip it.
                            continue;
                        }
                        if (respOrder.IsConversionError)
                        {
                            Console.WriteLine("************************Conversion Error************************");
                            Console.WriteLine("There is data that the SDK didn't expect. These must have been added after");
                            Console.WriteLine("this version of the SDK was released. Consider upgrading to the latest version.");
                            Console.WriteLine(respOrder.LeftoverData);
                            continue;
                        }
                        // These fields tell you the status of your submission.
                        if (respOrder[SLMResp.Batch.Order.ResponseReasonCode] != null)
                        {
                            var respCode = respOrder.GetIntField(SLMResp.Batch.Order.ResponseReasonCode);
                            if (respCode >= 100 && respCode <= 199)
                            {
                                Console.WriteLine("************************Successfully processed the order************************");
                            }
                            else
                            {
                                Console.WriteLine("************************Failed to process the order****************************");
                            }
                            Console.WriteLine("Order Number  =>" + respOrder[SLMResp.Batch.Order.MerchantOrderNumber]);
                            Console.WriteLine("Response code =>" + respOrder[SLMResp.Batch.Order.ResponseReasonCode]);
                            Console.WriteLine("Auth verification code =>" + respOrder[SLMResp.Batch.Order.AuthVerCode]);
                        }
                        Console.WriteLine(respOrder.DumpFieldValues());
                    }
                    catch (ResponseException e)
                    {
                        // All of the SDK exceptions have an error code that you can test
                        // to determine how to handle a particular error.
                        Console.WriteLine("************************Exception while processing the record************************");
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("Error Code=" + e.ErrorCode);
                        if (e.ErrorCode == Error.FieldDoesNotExist)
                        {
                            // Perform connect error handling...
                        }
                    }
                }
                // Get the batch header
                var header = batchResp.Header;
                Console.WriteLine("************************Response batch header************************");
                Console.WriteLine(header.DumpFieldValues());
                // Get the batch batch total
                var batchTotal = batchResp.BatchTotals;
                Console.WriteLine("************************Response batch total************************");
                Console.WriteLine(batchTotal.DumpFieldValues());
                // Get the totals
                var totals = batchResp.Totals;
                Console.WriteLine("************************Response total******************************");
                Console.WriteLine(totals.DumpFieldValues());
                // Get the trailers
                var trailer = batchResp.Trailer;
                Console.WriteLine("************************Response trailer*****************************");
                Console.WriteLine(trailer.DumpFieldValues());
                batchResp.Close();
            }
            catch (DispatcherException e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error Code=" + e.ErrorCode);
                throw;
            }
            catch (RequestException e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
