namespace SLMBatchDFRTCP
{
    using System;
    using JPMC.MSDK;

    internal class SLMBatchDFRTCP
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
                // Send one RFR request for each outstanding batch response file
                // For number of retries & wait time between the retries, consult
                // merchant certification group.
                var rfrRequest = dispatcher.CreateRequest("SubmissionResponse", "TCPBatch");
                rfrRequest["PID"] = "12345";
                rfrRequest["PIDPassword"] = "DFR00000";
                rfrRequest["SID"] = "12345";
                rfrRequest["SIDPassword"] = "DFR00000";
                // Use the Config property to get or set any communication-
                // specific configuration values for this transaction.
                // Any changes you make to the config here will ONLY apply
                // to this submission operation.
                rfrRequest.Config["VerifyPGPSignature"] = "false";
                var batchResp = dispatcher.ReceiveResponse(rfrRequest, "sdk");
                if (batchResp == null)
                {
                    Console.WriteLine("************************No DFR file found, contact customer support************************");
                    throw new Exception("No DFR file found, contact customer support");
                }
                Console.WriteLine("DFR File Name =>" + batchResp.FileName);
                // loop thru the DFR file. This is the standard way to process a
                // response file.
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
                            // Perform connect error handling...
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
            catch (RequestException e)
            {
                Console.WriteLine("************************Exception while processing the response************************");
                Console.WriteLine(e.ToString());
                throw;
            }
        }
    }
}
