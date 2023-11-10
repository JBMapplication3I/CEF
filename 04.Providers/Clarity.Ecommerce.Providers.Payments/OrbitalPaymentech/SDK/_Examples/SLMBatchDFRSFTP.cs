namespace SLMBatchDFRSFTP
{
    using System;
    using JPMC.MSDK;

    internal class SLMBatchDFRSFTP
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
                var batchResp = dispatcher.ReceiveResponse(ResponseType.DelimitedFileReport, config);
                if (batchResp == null)
                {
                    Console.WriteLine("************************No DFR file found, contact customer support************************");
                    throw new Exception("No DFR File found, contact customer support");
                }
                Console.WriteLine("DFR File Name =>" + batchResp.FileName);
                Console.WriteLine("************************DFR records************************");
                while (batchResp.HasNext)
                {
                    try
                    {
                        var respRecord = batchResp.GetNext();
                        if (respRecord == null)
                        {
                            // Seems we got a null response object, skip and
                            // This shouldn't happen. Perform any error handling
                            // here.
                            // For this sample, we'll just skip it.
                            continue;
                        }
                        if (respRecord.IsConversionError)
                        {
                            Console.WriteLine("************************Conversion Error************************");
                            Console.WriteLine("There is data that the SDK didn't expect. These must have been added after");
                            Console.WriteLine("this version of the SDK was released. Consider upgrading to the latest version.");
                            Console.WriteLine(respRecord.LeftoverData);
                            continue;
                        }
                        // getNext() gets the next record in the file.
                        // You must check the ReportType to find out what type of
                        // record it is. For a standard DFR, like this sample illustrates,
                        // we're looking for a FIN0010 report. You must check
                        // the specs to identify the ReportType for the reports
                        // you get.
                        var reportType = respRecord[SLMResp.Batch.DFR.ReportType];
                        // The detail record of the report.
                        if (reportType == "RFIN0010")
                        {
                            Console.WriteLine("***** FIN10 Detail Record *****");
                            Console.WriteLine(respRecord.DumpFieldValues());
                            Console.WriteLine();
                            Console.WriteLine("MOP=" + respRecord[SLMResp.Batch.DFR.DepositActivity.MethodOfPayment]);
                            Console.WriteLine("Amount=" + respRecord[SLMResp.Batch.DFR.DepositActivity.Amount]);
                            Console.WriteLine("Entity #=" + respRecord[SLMResp.Batch.DFR.DepositActivity.EntityNumber]);
                            Console.WriteLine();
                        }
                        if (reportType == "*DFRBEG")
                        {
                            Console.WriteLine("***** Header Record *****");
                            Console.WriteLine(respRecord.DumpFieldValues());
                        }
                        if (reportType == "HFIN0010")
                        {
                            Console.WriteLine("***** FIN10 Header Record *****");
                            Console.WriteLine(respRecord.DumpFieldValues());
                        }
                        if (reportType == "*DFREND")
                        {
                            Console.WriteLine("***** Trailer Record *****");
                            Console.WriteLine(respRecord.DumpFieldValues());
                        }
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
                            // Perform error handling...
                        }
                        throw;
                    }
                }
                batchResp.Close();
            }
            catch (DispatcherException e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                Console.WriteLine("Error Code=" + e.ErrorCode);
                throw;
            }
            catch (Exception e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
