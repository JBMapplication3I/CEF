namespace SLMBatchRFS
{
    using System;
    using JPMC.MSDK;

    internal class SLMBatchRFS
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
                var rfs = dispatcher.CreateRequest("SubmissionStatus", "TCPBatch");
                rfs["PID"] = "12345";
                rfs["PIDPassword"] = "RFS00000";
                rfs["SID"] = "12345";
                rfs["SIDPassword"] = "RFS00000";
                // Try getting the RFS file.
                // The ResponseDescriptor is your means of accessing the file
                // returned from the server.
                var batchResp = dispatcher.ReceiveResponse(rfs, "sdk");
                if (batchResp == null)
                {
                    Console.WriteLine("************************No RFS file found, contact customer support************************");
                    return;
                }
                Console.WriteLine("RFS file name =>" + batchResp.FileName);
                // loop thru the RFS file. This is the standard way to process a
                // delimited file.
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
                        // For an RFS response, a ReportType of "50" means that it's a Detail Record.
                        // These fields tell you the status of your submission.
                        var data = respRecord["ReportType"];
                        if (data == "50")
                        {
                            Console.WriteLine("***** Detail Record *****");
                            Console.WriteLine("Response File: " + respRecord[SLMResp.Batch.RFSReport.RFSDetail.ResponseFileReturned]);
                            Console.WriteLine("Received by Server Date: " + respRecord[SLMResp.Batch.RFSReport.RFSDetail.SubmissionReceivedDate]);
                            Console.WriteLine("Received by Server Time: " + respRecord[SLMResp.Batch.RFSReport.RFSDetail.SubmissionReceivedTime]);
                            Console.WriteLine("Status: " + respRecord[SLMResp.Batch.RFSReport.RFSDetail.SubmissionStatus]);
                            Console.WriteLine("Record Count: " + respRecord[SLMResp.Batch.RFSReport.RFSDetail.SubmissionRecordCount]);
                            Console.WriteLine();
                            Console.WriteLine(respRecord.DumpFieldValues());
                            Console.WriteLine();
                        }
                        // For an RFS response, a ReportType of "01" means that it's the Header Record.
                        if (data == "01")
                        {
                            Console.WriteLine("***** Detail Record *****");
                            Console.WriteLine(respRecord.DumpFieldValues());
                        }
                        // For an RFS response, a ReportType of "01" means that it's the Trailer Record.
                        if (data == "99")
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
                    catch (SubmissionException e)
                    {
                        Console.WriteLine("************************Exception while processing the record************************");
                        Console.WriteLine(e.ToString());
                        Console.WriteLine("Error Code=" + e.ErrorCode);
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
