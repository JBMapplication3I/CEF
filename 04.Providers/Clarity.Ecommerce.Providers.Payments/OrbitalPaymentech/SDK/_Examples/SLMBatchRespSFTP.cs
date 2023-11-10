namespace SLMBatchRespSFTP
{
    using System;
    using JPMC.MSDK;

    internal class SLMBatchRespSFTP
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
                return;
            }
            try
            {
                // Use the GetConfig method to get or set any communication-
                // specific configuration values for this transaction.
                // Any changes you make to the config here will ONLY apply
                // to this submission operation.
                var config = dispatcher.GetConfig(CommModule.SFTPBatch);
                config["PID"] = "12345";
                config["UserName"] = "MyUserName";
                config["UserPassword"] = "SDKPWORD";
                config["RSAMerchantPrivateKey"] = "Absolute Path for RSA private key";
                config["RSAMerchantPassPhrase"] = "Passphrase for RSA private key if any";
                config["PGPMerchantPrivateKey"] = "Absolute Path for PGP private key";
                config["PGPMerchantPassPhrase"] = "Passphrase for PGP private key if any";
                config["SubmissionFilePassword"] = "Password for file at rest";
                var batchResp = dispatcher.ReceiveResponse(config);
                if (batchResp == null)
                {
                    Console.WriteLine("************************No submission response found, contact customer support************************");
                    return;
                }
                // If this is not null, then a NetConnect error file was returned from the server.
                // In this case, you must process the error file and not process the response.
                if (batchResp.ErrorResponse != null)
                {
                    Console.WriteLine("Batch_Sequence_Number:" + batchResp.ErrorResponse[NCError.Batch_Sequence_Number]);
                    Console.WriteLine("User_ID:" + batchResp.ErrorResponse[NCError.User_ID]);
                    Console.WriteLine("File_ID:" + batchResp.ErrorResponse[NCError.File_ID]);
                    Console.WriteLine("File_Direction:" + batchResp.ErrorResponse[NCError.File_Direction]);
                    Console.WriteLine("Error_Code:" + batchResp.ErrorResponse[NCError.Error_Code]);
                    Console.WriteLine("Error_Description:" + batchResp.ErrorResponse[NCError.Error_Description]);
                    // The response file (NetConnect error file or response) is automatically
                    // deleted when successfully downloaded. This specifies if it was successfully
                    // deleted.
                    Console.WriteLine("Server file deleted? " + batchResp.ErrorResponse[NCError.DeleteSFTPFile]);
                    // To see all the data in the file.
                    Console.WriteLine(batchResp.ErrorResponse.DumpFieldValues());
                    // Fix the error in the batch file and re-send it again
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
            catch (ResponseException e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
