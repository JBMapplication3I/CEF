namespace SLMBatchSubmit
{
    using System;
    using JPMC.MSDK;
    using JPMC.MSDK.Common;

    internal class SLMBatchSubmit
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
                // This specifies the method of communication you will use
                // for the transactions.
                var configName = "TCPBatch";
                // Gets a batch name that is unique for this test.
                var batchName = SDKUtils.GetUniqueBatchName("batch", configName);
                Console.WriteLine("Creating Batch named " + System.IO.Path.GetFileName(batchName));
                Console.WriteLine();
                // Create a new SubmissionDescriptor object.
                // Set the desired file name & password for encryption.
                // By default file will be created under
                // <Directories><outgoing>outgoing</outgoing></Directories>
                // specified in MSDKConfig.xml
                // NOTE: This sample works for both TCP and SFTP. Just
                // specify the correct CommModule in this method call.
                var batch = dispatcher.GetSubmission(batchName, "sdk", configName);
                // Use the Config property to get or set any communication-
                // specific configuration values for this transaction.
                // Any changes you make to the config here will ONLY apply
                // to this submission operation.
                //batch.Config[ "Host" ] = "desthost.com";
                //batch.Config[ "Port" ] = "5555";
                // The following fields are used for SFTPBatch
                // batch.Config[ "UserName"] = "MyUserName";
                // batch.Config[ "UserPassword"] = "SDKPWORD";
                // batch.Config[ "RSAMerchantPrivateKey" ] = "Absolute Path for RSA private key";
                // batch.Config[ "RSAMerchantPassPhrase" ] = "Passphrase for RSA private key if any";
                // batch.Config[ "PGPMerchantPrivateKey"] = "Absolute Path for PGP private key";
                // batch.Config[ "PGPMerchantPassPhrase"] = "Passphrase for PGP private key if any";
                // Initialize the batch with the new submission header
                var header = dispatcher.CreateRequest("SubmissionHeader", configName);
                header["PID"] = "2355";
                header["PIDPassword"] = "MyPass";
                header["SID"] = "2355";
                header["SIDPassword"] = "MyPass";
                // Create the batch, using the header request.
                // This creates the file and prepares it for adding orders.
                batch.CreateBatch(header);
                // Create a new order for the batch and set its fields.
                var order = dispatcher.CreateRequest("SubmissionOrder", configName);
                order[SLM.SubmissionOrder.MerchantDescriptorRecordCreditCard.MerchantNameItemDescription] = "Salem book depot";
                order[SLM.SubmissionOrder.MerchantDescriptorRecordCreditCard.MerchantCityCSRPhone] = "Salem NH";
                order[SLM.SubmissionOrder.MethodOfPayment] = "CZ";
                order[SLM.SubmissionOrder.TransactionType] = "1";
                order[SLM.SubmissionOrder.Amount] = "550000";
                order[SLM.SubmissionOrder.ActionCode] = "DC";
                order[SLM.SubmissionOrder.MerchantOrderNumber] = "4636730000000006";
                order[SLM.SubmissionOrder.DivisionNumber] = "12345";
                order[SLM.SubmissionOrder.AccountNumber] = "4444444444444448";
                order[SLM.SubmissionOrder.CurrencyCode] = "840";
                order[SLM.SubmissionOrder.MerchantSpace] = "Auth";
                // Add Fraud record to the current order
                order[SLM.SubmissionOrder.ProductRecordFraud001.CardSecurityValue] = "321";
                order[SLM.SubmissionOrder.ProductRecordFraud001.CardSecurityPresence] = "1";
                // Add the order to batch
                batch.Add(order);
                // No more orders, close the batch file
                // This will add the totals and trailer records for you,
                // so once this is called, you cannot re-open the file
                // and add more orders.
                // Calling batch.close() closes the file without adding
                // the ending records, so that you can add to it later.
                batch.CloseBatch();
                // Send the submission for processing.
                dispatcher.SendSubmission(batch);
                Console.WriteLine("Done.");
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
            catch (SubmissionException e)
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
