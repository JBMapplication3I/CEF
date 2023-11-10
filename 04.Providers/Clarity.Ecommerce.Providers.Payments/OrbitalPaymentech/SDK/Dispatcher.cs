#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Runtime.InteropServices;
    using System.Text;
    using Comm;
    using Common;
    using Configurator;
    using Converter;
    using Framework;

    /// <summary>
    /// The JPMC.MSDK namespace contains all of the types that
    /// the merchant needs to make use of. The merchant only needs to use this
    /// namespace for using the SDK.
    /// </summary>
    [System.Runtime.CompilerServices.CompilerGenerated]
    internal class NamespaceDoc
    {
        // This code is included in order to add XML doc comments to the namespace.
    }

    /// <summary>
    /// The primary API for accessing the SDK.
    /// </summary>
    /// <remarks>
    /// This is your entry point to the MSDK. Every operation that you wish to perform is done by calling a
    /// method of the Dispatcher. Use the Dispatcher to create new transaction requests, new batches,
    /// and to send out the requests and batch files to the server.
    /// </remarks>
    [ComVisible(true)]
    [Guid("B01B9A98-2EA1-4dab-934A-B166469D9021")]
    [ClassInterface(ClassInterfaceType.None)]
    [ProgId("JPMC.MSDK.Framework.Dispatcher")]
    public class Dispatcher : DispatcherBase, IDispatcher, IDisposable
    {
        private static string submissionLock = "For currentSubmission";
        private static string uploadLock = "For pnsUploads";
        private static List<string> pnsUploads;

        #region Constructors & Initialization
        /// <summary>
        /// The default constructor.
        /// </summary>
        /// <remarks>
        /// </remarks>
        public Dispatcher()
            : this(null, new DispatcherFactory())
        {
        }

        public Dispatcher(string sdkHome)
            : this(sdkHome, new DispatcherFactory())
        {
        }

        /// <exclude />
        /// <summary>
        /// For internal use only.
        /// </summary>
        /// <param name="msdkHome"></param>
        /// <param name="factory"></param>
        public Dispatcher(string msdkHome, IDispatcherFactory factory)
        {
            this.factory = factory;
            Initialize(msdkHome);
        }

        /// <summary>Initializes the Dispatcher.</summary>
        /// <remarks>All members that need to be set or configured are done so here.
        /// Initialization is done here because only the default constructor, which takes no parameters, is available
        /// via COM Interop. All COM clients must call Initialize after instantiating the Dispatcher object. The other
        /// constructors will call Initialize automatically so that .Net languages do not need to call Initialize.</remarks>
        /// <param name="msdkHome">The path to where MSDK exists, or null if the default is to be used.</param>
        [DispId(2)]
        public void Initialize(string msdkHome)
        {
            var msdk = msdkHome;
            if (msdk == "")
            {
                msdk = null;
            }
            SetMSDKHome(msdk);
            // This creates the static online CommManager.
            // It needs to be done at startup only.
            factory.MakeCommManager();
            initialized = true;
            Logger.DebugFormat("MSDK started using MSDK Home={0}.", factory.HomeDirectory);
        }
        #endregion

        #region Common Methods
        /// <summary>
        /// Returns the path to where the MSDK exists.
        /// </summary>
        /// <remarks>
        /// This value is set in the constructor either by having it passed into the constructor, or
        /// by searching for it in the following ways:
        /// <list type="number">
        /// <item>The MSDK_HOME environment variable, if set.</item>
        /// <item>The Assembly's physical location.</item>
        /// <item>One directory back from the Assembly's location.</item>
        /// <item>The application's location.</item>
        /// </list>
        /// </remarks>
        [DispId(3)]
        public string HomeDirectory => factory.HomeDirectory;

        /// <inheritdoc/>
        [DispId(6)]
        public IRequest CreateRequest(string transactionType, ConfigurationData configData)
        {
            Logger.DebugFormat("Creating Request \"{0}\"", transactionType);
            AssertInitialized();
            if (transactionType == null)
            {
                Logger.Error("The request type is null.");
                throw new DispatcherException(Error.InvalidRequestType, "The request type is null.");
            }
            try
            {
                var request = factory.MakeRequest(transactionType, configData);
                return request;
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>Gives you direct access to the Configurator component of MSDK.</summary>
        /// <remarks>This allows you to access the data that is stored in the config file.</remarks>
        /// <value>The configurator.</value>
        [DispId(8)]
        public new IConfigurator Configurator => base.Configurator;

        /// <inheritdoc/>
        [DispId(9)]
        public ISubmissionDescriptor GetSubmission(string fileName, string password, ConfigurationData configData)
        {
            AssertInitialized();
            try
            {
                lock (submissionLock)
                {
                    return GetSubmissionImpl(fileName, password, configData);
                }
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <inheritdoc/>
        [DispId(11)]
        public ISubmissionDescriptor OpenSubmission(string fileName, string password, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(fileName, Error.NullFileName, "filename");
            TestForNull(password, Error.NullPassword, "password");
            TestForNull(configData, Error.NullConfigurationData, "config data");
            var path = Utils.FormatDirectoryPath(fileName);
            ValidateFileLength(path);
            if (!Utils.IsAbsolutePath(path))
            {
                if (path.IndexOf("\\") != -1)
                {
                    path = FileName.Combine(factory.HomeDirectory, path);
                }
                else
                {
                    var outgoingDir = configData["OutgoingBatchDirectory"];
                    path = FileName.Combine(Utils.IsAbsolutePath(outgoingDir)
                        ? outgoingDir
                        : FileName.Combine(factory.HomeDirectory, outgoingDir), path);
                }
            }
            if (!factory.MakeFileManager().Exists(path))
            {
                var msg = $"The file \"{path}\" could not be found.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNotFound, msg);
            }
            try
            {
                var submission = factory.OpenSubmission(path, password, configData);
                return submission;
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (SubmissionException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (IOException ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.FileAccessError, ex.Message, ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Sends the submission to the server.
        /// </summary>
        /// <remarks>
        /// <p>The submission will get converted into the proper payload and
        /// then transmitted to the Stratus using the specified protocol.</p>
        /// </remarks>
        /// <param name="submission">The descriptor for the submission to send.</param>
        [DispId(12)]
        public void SendSubmission(ISubmissionDescriptor submission)
        {
            AssertInitialized();
            TestForNull(submission, Error.NullSubmission, "SubmissionDescriptor");
            ValidateConfigData(submission.Config, false, submission.Password, MessageFormat.SLM);
            if (submission.Config.Protocol != CommModule.SFTPBatch && submission.Config.Protocol != CommModule.TCPBatch)
            {
                var msg = "Dispatcher.SendSubmission() only supports SFTPBatch and TCPBatch modules";
                Logger.Error(msg);
                throw new DispatcherException(Error.ArgumentMismatch, msg);
            }
            var fname = MakeFileName(submission);
            switch (submission.Config.Protocol)
            {
                case CommModule.SFTPBatch:
                    AssertValidPID(submission.Config);
                    var sftp = factory.MakeSFTPBatchProcessor();
                    sftp.SendSubmission(submission, fname);
                    break;
                case CommModule.TCPBatch:
                    var tcp = factory.MakeTCPBatchProcessor();
                    tcp.SendSubmission(submission, fname);
                    break;
            }
        }

        /// <inheritdoc/>
        [DispId(15)]
        public IResponseDescriptor OpenDescriptor(string fileName, string password, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(fileName, Error.NullFileName, "filename");
            TestForNull(password, Error.NullPassword, "password");
            fileName = Utils.FormatDirectoryPath(fileName);
            var path = fileName;
            if (!Utils.IsAbsolutePath(path))
            {
                path = FileName.Combine(factory.HomeDirectory, fileName);
                if (!factory.MakeFileManager().Exists(path))
                {
                    path = FileName.Combine(factory.HomeDirectory, configData["IncomingBatchDirectory"]);
                    path = FileName.Combine(path, fileName);
                }
                if (!factory.MakeFileManager().Exists(path))
                {
                    path = FileName.Combine(factory.HomeDirectory, configData["OutgoingBatchDirectory"]);
                    path = FileName.Combine(path, fileName);
                }
            }
            if (!factory.MakeFileManager().Exists(path))
            {
                var msg = $"The file \"{path}\" could not be found.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNotFound, msg);
            }
            try
            {
                return factory.MakeResponseDescriptor(path, password, configData);
            }
            catch (ResponseException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (ConfiguratorException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (IOException ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.FileAccessError, ex.Message, ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }
        #endregion

        #region Intranet Methods
        /// <exclude />
        /// <summary>
        /// Releases all references and resources, and cleans up.
        /// </summary>
        /// <remarks>
        /// This should be called whenever the client
        /// is done with the Dispatcher, as this method closes the connection with the server.
        /// </remarks>
        [DispId(16)]
        public void Dispose()
        {
            try
            {
                factory.Comm.Close();
                pnsUploads?.Clear();
            }
            catch (CommException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <exclude />
        /// <summary>
        /// Sends a heartbeat request to the server.
        /// </summary>
        /// <remarks>
        /// This
        /// method validates the request parameter to make sure it is
        /// correct for the operation and then sends it to the appropriate
        /// OnlineProcessor.
        /// </remarks>
        /// <param name="request">A Request object that must be using the HeartBeat template.</param>
        /// <returns></returns>
        [DispId(19)]
        public IResponse SendHeartbeat(IRequest request)
        {
            AssertInitialized();
            if (request == null)
            {
                Logger.Error("Request is null");
                throw new DispatcherException(Error.NullRequest, "Request is null");
            }
            if (request.TransactionType != "HeartBeat")
            {
                var msg = "Invalid Request type for a heartbeat.";
                Logger.Error(msg);
                throw new DispatcherException(Error.InvalidHeartbeat, msg);
            }
            if (request.Config.Protocol != CommModule.TCPConnect)
            {
                var msg = "Invalid CommModule type for a heartbeat.";
                Logger.Error(msg);
                throw new DispatcherException(Error.InvalidModule, msg);
            }
            try
            {
                var processor = factory.MakeOnlineProcessor();
                return processor.SendHeartbeat(request);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (ConverterException ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(ex.ErrorCode, ex.Message, ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <exclude />
        /// <summary>
        /// Sends an RFR request and then receives the file from the communication layer.
        /// </summary>
        /// <remarks>
        /// This method will block until the response has finished downloading.
        /// It will download the file as named on the
        /// Stratus, and will then rename it to the merchant-supplied filename.
        /// The merchant-supplied filename was specified when the original submission
        /// was sent, and is embedded in the file as one record.
        /// </remarks>
        /// <param name="request">The RFR request.</param>
        /// <param name="password">The password to use when encrypting the response file.</param>
        /// <returns>A ResponseDescriptor that can be used to iterate through the response file.</returns>
        [DispId(20)]
        public IResponseDescriptor ReceiveResponse(IRequest request, string password)
        {
            if (password == null)
            {
                var msg = "Dispatcher.ReceiveResponse() password must be set.";
                Logger.Error(msg);
                throw new DispatcherException(Error.NullPassword, msg);
            }
            if (request == null)
            {
                var msg = "Dispatcher.ReceiveResponse() request must be valid.";
                Logger.Error(msg);
                throw new DispatcherException(Error.NullRequest, msg);
            }
            if (request.Config.Protocol != CommModule.TCPBatch)
            {
                var msg = "Invalid CommModule for method call. CommModule=" + request.Config.Protocol.ToString();
                Logger.Error(msg);
                throw new DispatcherException(Error.InvalidModule, msg);
            }
            request.Config["SubmissionFilePassword"] = password;
            return ReceiveResponse(request);
        }

        /// <exclude />
        /// <summary>
        /// Sends an RFR request and then receives the file from the communication layer.
        /// </summary>
        /// <remarks>
        /// This method will block until the response has finished downloading.
        /// It will download the file as named on the
        /// Stratus, and will then rename it to the merchant-supplied filename.
        /// The merchant-supplied filename was specified when the original submission
        /// was sent, and is embedded in the file as one record.
        /// </remarks>
        /// <param name="request">The RFR request.</param>
        /// <returns>A ResponseDescriptor that can be used to iterate through the response file.</returns>
        [DispId(22)]
        public IResponseDescriptor ReceiveResponse(IRequest request)
        {
            AssertInitialized();
            if (request == null)
            {
                var msg = "The Request is null.";
                Logger.Error(msg);
                throw new DispatcherException(Error.NullRequest, msg);
            }
            string ptext;
            ptext = request.Config["SubmissionFilePassword"];
            if (ptext == null)
            {
                var msg = "The AESPassword must be set in ProtocolManager object.";
                Logger.Error(msg);
                throw new DispatcherException(Error.NullPassword, msg);
            }
            try
            {
                var req = factory.RequestToImpl(request);
                if (req.HasField("CreationDate"))
                {
                    req.SetField("CreationDate", GetCurrentDateString(), true);
                }
            }
            catch
            {
                Logger.Warn("Error setting CreationDate");
            }
            var file = FileMgr.CreateTemp("rec", FileType.Incoming, request.Config);
            try
            {
                var processor = factory.MakeTCPBatchProcessor();
                return processor.ReceiveResponse(request, ptext, file);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (ConverterException ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(ex.ErrorCode, ex.Message, ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Begins a batch upload for PNS processing
        /// </summary>
        /// <remarks>
        /// <para>This method must be used to begin a PNS batch upload.
        /// The Request object must contain a valid header.
        /// </para>
        ///
        /// <para>
        /// This method must be used in conjunction with ProcessPNSUpload()
        /// and EndPNSUpload(). You must not use the normal processRequest()
        /// method for PNS upload.
        /// </para>
        /// </remarks>
        /// <param name="req">A valid header request</param>
        /// <returns>An IResponse object that contains the response fields for the header request sent.</returns>
        [DispId(23)]
        public IResponse StartPNSUpload(IRequest req)
        {
            lock (uploadLock)
            {
                var request = (IRequestImpl)req;
                string key = null;
                try
                {
                    key = BuildPNSUploadKey((IRequestImpl)request);
                    ValidatePNSUploadMessageType(request, "Headers", key);

                    if (pnsUploads == null)
                    {
                        pnsUploads = new List<string>();
                    }

                    if (pnsUploads.Contains(key))
                    {
                        var msg =
                            $"An upload has already been started for this thread. MID={request["Bit42.CardAcquirerId"]}, TID={request["Bit41.CardAcquirerTerminalId"]}";
                        factory.Comm.Close(key, request.Config);
                        // In case it's already been called for this key.
                        pnsUploads.Remove(key);
                        Logger.Error(msg);
                        throw new DispatcherException(Error.InvalidRequest, msg);
                    }

                    request.TransactionControlValues["IsPNSBatchHeader"] = "true";
                    pnsUploads.Add(key);
                    Logger.Info($"*** Starting a PNS Upload [{key}]");
                    var response = ProcessPNSUpload(request);
                    return response;
                }
                catch (RequestException ex)
                {
                    ClosePNSUpload(key, req.Config);
                    throw new DispatcherException(ex.ErrorCode, "StartPNSUpload failed.", ex);
                }
                catch (DispatcherException)
                {
                    ClosePNSUpload(key, req.Config);
                    throw;
                }
                catch (Exception ex)
                {
                    ClosePNSUpload(key, req.Config);
                    Logger.Error("StartPNSUpload failed", ex);
                    throw new DispatcherException(Error.RequestFailure, "StartPNSUpload failed", ex);
                }
            }
        }

        private void ValidatePNSResponseCode(IResponse response, string key, ConfigurationData configData)
        {
            try
            {
                var respCode = response["Bit39.ResponseCode"];
                if (Utils.StringToInt(respCode, -1) != 0)
                {
                    Logger.ErrorFormat("The request returned a response code of {0}. Closing batch.", respCode);
                    ClosePNSUpload(key, configData);
                }
            }
            catch (ResponseException ex)
            {
                throw new DispatcherException(ex.ErrorCode, "Failure verifying reponse code", ex);
            }
        }

        /// <summary>
        /// Processes a detail record a PNS batch upload.
        /// </summary>
        /// <remarks>
        /// <para>This method must be used to send a request to the batch upload.
        /// You must call StartPNSUpload() first before you can call this method.
        /// </para>
        ///
        /// <para>
        /// This method must be used in conjunction with StartPNSUpload()
        /// and EndPNSUpload(). You must not use the normal processRequest()
        /// method for PNS upload.
        /// </para>
        /// </remarks>
        /// <param name="req">An IRequest object that contains the fields and formats for the request.</param>
        /// <returns>An IResponse object that contains the response fields for the request sent.</returns>
        [DispId(24)]
        public IResponse ProcessPNSUpload(IRequest req)
        {
            lock (uploadLock)
            {
                var request = (IRequestImpl)req;
                string key = null;
                try
                {
                    key = BuildPNSUploadKey(request);
                    if (pnsUploads == null || !pnsUploads.Contains(key))
                    {
                        var msg =
                            $"No upload has been started on this thread for MID: {request["Bit42.CardAcquirerId"]}, and TID: {request["Bit41.CardAcquirerTerminalId"]}. Call startPNSUpload first.";
                        Logger.Error(msg);
                        throw new DispatcherException(Error.InvalidRequest, msg);
                    }

                    var response = ProcessRequest(request);
                    ValidatePNSResponseCode(response, key, req.Config);
                    return response;
                }
                catch (RequestException ex)
                {
                    ClosePNSUpload(key, req.Config);
                    throw new DispatcherException(ex.ErrorCode, "Failure processing PNS upload", ex);
                }
                catch (DispatcherException)
                {
                    ClosePNSUpload(key, req.Config);
                    throw;
                }
                catch (Exception ex)
                {
                    ClosePNSUpload(key, req.Config);
                    Logger.Error("Failure processing PNS upload", ex);
                    throw new DispatcherException(Error.RequestFailure, "Failure processing PNS upload", ex);
                }
            }
        }

        private void ClosePNSUpload(string key, ConfigurationData configData)
        {
            factory.Comm.Close(key, configData);
            if (pnsUploads != null)
            {
                pnsUploads.Remove(key);
            }
        }

        /// <summary>
        /// Ends a batch upload for PNS processing
        /// </summary>
        /// <remarks>
        /// <para>This method must be used to end an existing PNS batch upload.
        /// The Request object must contain a valid trailer.
        /// </para>
        ///
        /// <para>
        /// This method must be used in conjunction with StartPNSUpload()
        /// and ProcessPNSUpload(). You must not use the normal processRequest()
        /// method for PNS upload.
        /// </para>
        /// </remarks>
        /// <param name="req">A valid header request</param>
        /// <returns>An IResponse object that contains the response for sending the trailer.</returns>
        [DispId(25)]
        public IResponse EndPNSUpload(IRequest req)
        {
            lock (uploadLock)
            {
                var request = (IRequestImpl)req;
                string key = null;
                try
                {
                    key = BuildPNSUploadKey(request);
                    ValidatePNSUploadMessageType(request, "Trailers", key);
                    request.TransactionControlValues["IsPNSBatchTrailer"] = "true";
                    var response = ProcessPNSUpload(request);
                    return response;
                }
                catch (RequestException ex)
                {
                    throw new DispatcherException(ex.ErrorCode, "Failure ending a PNS upload", ex);
                }
                finally
                {
                    ClosePNSUpload(key, req.Config);
                }
            }
        }

        private string BuildPNSUploadKey(IRequestImpl request)
        {
            var requestMID = request["Bit42.CardAcquirerId"];
            var requestTID = request["Bit41.CardAcquirerTerminalId"];

            if (requestMID != null && requestTID != null)
            {
                request.TransactionControlValues["AuthMid"] = requestMID;
                request.TransactionControlValues["AuthTid"] = requestTID;
            }

            var key = $"{requestMID}{requestTID}";
            return key;
        }

        private void ValidatePNSUploadMessageType(IRequest request, string type, string key)
        {
            try
            {
                if (request["MessageHeader.MessageType"] != "1500")
                {
                    var msg = $"Invalid MessageType. The MessageType for Upload {type} must be 1500.";
                    factory.Comm.Close(key, request.Config);
                    Logger.Error(msg);
                    throw new DispatcherException(Error.InvalidField, msg);
                }
            }
            catch (RequestException ex)
            {
                throw new DispatcherException(ex.ErrorCode, "Failure to validate the message type", ex);
            }
        }

        ///<summary>
        /// Cancels a PNS Upload for the specified MID and TID.
        /// </summary>
        /// <remarks>
        /// NOTE: This only flushes the upload from the SDK itself. It does not cancel the upload
        /// from the server. You will need to wait the appropriate amount of time to allow the
        /// upload to timeout on the server before starting a new one.
        /// </remarks>
        /// <param name="mid"></param>
        /// <param name="tid"></param>
        /// <param name="configData"></param>
        public void CancelPNSUpload(string mid, string tid, ConfigurationData configData)
        {
            var key = $"{mid}{tid}";
            try
            {
                var comm = factory.Comm;
                comm.Close(key, configData);
            }
            catch (DispatcherException e)
            {
                Logger.Error("Exception occurred while trying to close the upload connection.", e);
                throw;
            }
            finally
            {
                pnsUploads?.Remove(key);
            }
        }
        #endregion

        #region Internet Methods
        /// <summary>
        /// Sends the submission to the server.
        /// </summary>
        /// <remarks>
        /// <p>The submission will get converted into the proper payload and
        /// then transmitted to the Stratus using the specified protocol.</p>
        /// </remarks>
        /// <param name="submissionFileName">The filename of the submission file to send.</param>
        [DispId(28)]
        public void SendSubmission(string submissionFileName, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(configData, Error.NullConfigurationData, "ConfigurationData");
            TestForNull(submissionFileName, Error.NullFileName, "submission's file name");
            AssertValidPID(configData);
            if (!Utils.IsAbsolutePath(submissionFileName))
            {
                submissionFileName = FileMgr.FindFilePath(submissionFileName, "outgoing");
            }
            submissionFileName = Utils.FormatDirectoryPath(submissionFileName);
            try
            {
                if (IsSubmissionPGP(submissionFileName))
                {
                    SendPGPSubmission(submissionFileName, configData);
                    return;
                }
                var submission = OpenSubmission(submissionFileName, configData["SubmissionFilePassword"], configData);
                SendSubmission(submission);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        private string MakeFileName(ISubmissionDescriptor submission)
        {
            string retVal = null;
            AssertInitialized();
            TestForNull(submission, Error.NullSubmission, "SubmissionDescriptor");
            if (!submission.Open && !submission.BatchClosed)
            {
                var msg = "The batch was never created. Call CreateBatch first.";
                Logger.Error(msg);
                throw new DispatcherException(Error.BatchNotOpen, msg);
            }
            try
            {
                submission.CloseBatch();
            }
            catch (SubmissionException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (Exception ex)
            {
                var msg = $"Closing the batch failed: {ex.Message}";
                Logger.Error(msg);
                throw new DispatcherException(Error.BatchNotClosed, msg);
            }
            try
            {
                VerifyResponseFileDoesNotExist(submission.Name, submission.Config);
                if (submission.Config.Protocol == CommModule.SFTPBatch)
                {
                    var fname = new FileName(submission.FileName) { Extension = "asc" };
                    retVal = fname.ToString();
                }
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
            return retVal;
        }

        /// <summary>
        /// Tells the communication layer to get the file and then receives the file
        /// from the communication layer.
        /// </summary>
        /// <remarks>
        /// This method will block until the response
        /// has finished downloading. It will download the file as named on the
        /// Stratus, and will then rename it to the merchant-supplied filename.
        /// The merchant-supplied filename was specified when the original submission
        /// was sent, and is embedded in the file as one record.
        /// </remarks>
        [DispId(32)]
        public IResponseDescriptor ReceiveResponse(string responseType, ConfigurationData configData)
        {
            ValidateConfigData(configData, true, null, MessageFormat.SLM);
            try
            {
                var fileMgr = factory.MakeFileManager();
                configData["RSAMerchantPrivateKey"] = fileMgr.FindFilePath(configData["RSAMerchantPrivateKey"], "config");
                if (configData["RSAMerchantPrivateKey"] == null)
                {
                    Logger.Error("The RSAMerchantPrivateKey could not be found.");
                    throw new DispatcherException(Error.FileNotFound, "The RSAMerchantPrivateKey could not be found.");
                }
                var fname = BuildResponseFileName(responseType, configData);
                var processor = factory.MakeSFTPBatchProcessor();
                return processor.ReceiveResponse(responseType, configData, FileName.Combine(fname.Path, fname.Name), fname.ToString());
            }
            catch (ConfiguratorException e)
            {
                throw new DispatcherException(e);
            }
        }

        private FileName BuildResponseFileName(string responseType, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(configData["SubmissionFilePassword"], Error.NullPassword, "SubmissionFilePassword");

            TestForNull(configData["PGPMerchantPrivateKey"], Error.PrivateKeyNotSet, "PGP Private Key");
            FileName fname = null;
            try
            {
                var fileMgr = factory.MakeFileManager();

                configData["PGPMerchantPrivateKey"] = fileMgr.FindFilePath(configData["PGPMerchantPrivateKey"], "config");
                if (configData["PGPMerchantPrivateKey"] == null)
                {
                    Logger.Error("The PGPMerchantPrivateKey could not be found.");
                    throw new DispatcherException(Error.FileNotFound, "The PGPMerchantPrivateKey could not be found.");
                }

                string pgpFile = null;
                do
                {
                    pgpFile = fileMgr.CreateTemp("rec", FileType.Incoming, configData);
                }
                while (fileMgr.Exists(pgpFile + ".response"));

                fname = new FileName(pgpFile);
                fname.Extension = "response";
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
            return fname;
        }

        /// <summary>
        /// Turns the data of the supplied Request object into the appropriate payload format
        /// and then passes it to the communication layer of the SDK.
        /// </summary>
        /// <remarks>
        /// The method will then block until the comm layer returns the server's response. The processRequest
        /// will convert the response payload into a Response object and return it to the client.
        ///
        /// The one task that is not so apparent is that it must verify that the response received belongs to
        /// the request that was sent. The converter will handle most of this by supplying a message format
        /// string for each payload, and then comparing them in the call to validateResponse.
        /// </remarks>
        /// <param name="request">The Request object created by the client.</param>
        /// <returns>A client-friendly object containing the data of the server's response.</returns>
        [DispId(34)]
        public IResponse ProcessRequest(IRequest request)
        {
            AssertInitialized();
            TestForNull(request, Error.NullRequest, "request");

            ValidateConfigData(request.Config, true, null, request.MessageFormat);

            ValidateConnectMode(request.Config.Protocol);

            // Add the metrics to the request.
            // Tandem does not include metrics
            // Orbital and NetConnect put the metrics data in the MIME header "Interface-version", this
            // is done elsewhere.
            try
            {
                if (request.MessageFormat == MessageFormat.SLM && !request.TransactionType.Equals("HeartBeat"))
                {
                    request["Version.VersionInfo"] = ((IRequestImpl)request).Metrics.ToBase64();
                }
            }
            catch (Exception ex)
            {
                Logger.Error("Error while setting metrics: " + ex.Message, ex);
            }


            ValidatePNSUpload((IRequestImpl)request);

            try
            {
                var processor = factory.MakeOnlineProcessor();
                return processor.ProcessRequest(request);
            }
            catch (RequestException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (ConverterException ex)
            {
                throw new DispatcherException(ex);
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        private void ValidateConfigData(ConfigurationData config, bool receive, string aesPassword, MessageFormat messageType)
        {
            if (config == null)
            {
                Logger.Error("The ConfigurationData is null.");
                throw new DispatcherException(Error.NullConfigurationData, "The ConfigurationData is null.");
            }

            var isError = false;
            var msg = new StringBuilder("The following fields are null and must be set in the ConfigurationData: ");

            // ReceiveSubmission (SFTP_BATCH)
            if (config.Protocol == CommModule.SFTPBatch && receive)
            {
                var value = config["PGPMerchantPrivateKey"];
                isError = TestForNull(value, "PGPMerchantPrivateKey", isError, msg);
                value = config["PGPMerchantPassPhrase"];
                value = config["SubmissionFilePassword"];
                if (TestForNull(value, "SubmissionFilePassword", isError, msg))
                {
                    config.SetField("SubmissionFilePassword", aesPassword, false);
                    value = config["SubmissionFilePassword"];
                    isError = TestForNull(value, "SubmissionFilePassword", isError, null);
                }

            }

            // SendSubmission (SFTP_BATCH)
            else if (config.Protocol == CommModule.SFTPBatch && !receive)
            {
                var value = config["PGPServerPublicKey"];
                isError = TestForNull(value, "PGPServerPublicKey", isError, msg);
                value = config["SubmissionFilePassword"];
                if (TestForNull(value, "SubmissionFilePassword", isError, msg))
                {
                    config.SetField("SubmissionFilePassword", aesPassword, false);
                    value = config["SubmissionFilePassword"];
                    isError = TestForNull(value, "SubmissionFilePassword", isError, null);
                }
            }

            // SendSubmission (TCP_BATCH) and receiveSubmission (TCP_BATCH)
            else if (config.Protocol == CommModule.TCPBatch)
            {
                var value = config["SubmissionFilePassword"];
                if (TestForNull(value, "SubmissionFilePassword", isError, msg))
                {
                    config.SetField("SubmissionFilePassword", aesPassword, false);
                    value = config["SubmissionFilePassword"];
                    isError = TestForNull(value, "SubmissionFilePassword", isError, null);
                }
            }

            // processRequest (HTTP_CONNECT)
            else if (config.Protocol == CommModule.HTTPSConnect && messageType != MessageFormat.ORB)
            {
                var value = config["UserName"];
                isError = TestForNull(value, "UserName", isError, msg);
                value = config["UserPassword"];
                isError = TestForNull(value, "UserPassword", isError, msg);
            }

            if (isError)
            {
                Logger.Error(msg.ToString());
                throw new DispatcherException(Error.InvalidField, msg.ToString());
            }
        }

        private bool TestForNull(string value, string name, bool isError, StringBuilder msg)
        {
            if (value == null)
            {
                if (isError && msg != null)
                {
                    msg.Append(", ");
                }
                if (msg != null)
                {
                    msg.Append(name);
                }
                isError = true;
            }
            return isError;
        }

        /// <summary>
        /// Throws an exception if the supplied module is for BATCH processing.
        /// </summary>
        /// <param name="module"></param>
        private void ValidateConnectMode(CommModule module)
        {
            if (module == CommModule.SFTPBatch || module == CommModule.TCPBatch || module == CommModule.Unknown)
            {
                var msg = "The OnlineCommunicationMode was not set in MSDKConfig.xml.";
                Logger.Error(msg);
                throw new DispatcherException(Error.InvalidModule, msg);
            }
        }

        private void ValidatePNSUpload(IRequestImpl request)
        {
            if (request.Config.Protocol == CommModule.PNSUpload || request.Config.Protocol == CommModule.HTTPSUpload)
            {
                var ctrl = request.TransactionControlValues;
                var isHeaderTrailer = ctrl.GetBoolValue("IsPNSBatchHeader") || ctrl.GetBoolValue("IsPNSBatchTrailer");

                var msgType = 0;
                try
                {
                    var type = request["MessageHeader.MessageType"];
                    msgType = Utils.StringToInt(type, 0);
                }
                catch
                {
                }

                if (!(msgType >= 1300))
                {
                    Logger.Error("This method is for PNS uploads only");
                    throw new DispatcherException(Error.InvalidRequest, "This method is for PNS uploads only");
                }

                if (msgType >= 1500 && !isHeaderTrailer)
                {
                    var msg = "Message Type: " + msgType + " must be a upload header or trailer.";
                    Logger.Error(msg);
                    throw new DispatcherException(Error.InvalidRequest, msg);
                }
            }
        }




        /// <summary>
        /// Deletes the specified file from the Stratus.
        /// </summary>
        /// <remarks>
        ///  The file specified does not
        /// represent a file on the client's computer, but instead refers to a file
        /// on the server. This method will properly delete the file
        /// from all servers (including backup servers).
        ///
        /// The method returns true if the file was successfully deleted from all
        /// servers. If it failed to delete it from even just one server, the method
        /// will return false. You can call this method as many times as it takes to
        /// delete all of the files. However, you may want to wait or call support
        /// if it fails to delete them all the first time.
        /// </remarks>
        /// <param name="filename">The name of the file to delete from the server.</param>
        [DispId(40)]
        public bool DeleteServerFile(string filename, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(filename, Error.NullFileName, "filename");
            TestForNull(configData, Error.NullConfigurationData, "Security Manager");
            TestForNull(configData["RSAMerchantPrivateKey"], Error.PrivateKeyNotSet, "RSA Private Key");

            if (configData.Protocol != CommModule.SFTPBatch)
            {
                var msg = "Invalid CommModule for method call. CommModule=" + configData.Protocol.ToString();
                Logger.Error(msg);
                throw new DispatcherException(Error.InvalidModule, msg);
            }

            try
            {
                var fileMgr = factory.MakeFileManager();
                configData["RSAMerchantPrivateKey"] = fileMgr.FindFilePath(configData["RSAMerchantPrivateKey"], "config");

                // FindFilePath may return null if it can't produce an absolute path to the file.
                TestForNull(configData["RSAMerchantPrivateKey"], Error.PrivateKeyNotSet, "RSA Private Key");

                var processor = factory.MakeSFTPBatchProcessor();
                return processor.DeleteServerFile(filename, configData);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Converts a PGP response file that was downloaded outside of MSDK into
        /// a AES encrypted response file and returns a ResponseDescriptor.
        /// </summary>
        /// <remarks>
        /// This is only used if the client wants to receive his response file from
        /// the Stratus on his own, without the use of the SDK. He can pass the
        /// filename of the response file he downloaded via SFTP into this method,
        /// and it will convert the file to the appropriate AES encrypted file and
        /// return a valid ResponseDescriptor for it.
        /// </remarks>
        /// <param name="filename">The filename of the response file.</param>
        [DispId(43)]
        public IResponseDescriptor CreateResponseDescriptor(string filename, ConfigurationData configData)
        {
            try
            {
                AssertInitialized();
                TestForNull(filename, Error.NullFileName, "filename");
                TestForNull(configData, Error.NullConfigurationData, "ConfigurationData");
                ValidateConfigData(configData, true, null, MessageFormat.SLM);
                var name = new FileName(filename);
                name.Extension = "response";
                var fname = name.ToString();
                var resp = CreateResponseDescriptor(filename, fname, configData);
                var responseType = resp.ResponseFileType;
                var aesFilename = resp.FileName;
                resp.Close();
                var newName = RenameResponseFile(responseType, aesFilename, configData["SubmissionFilePassword"], configData);
                return OpenDescriptor(newName, configData["SubmissionFilePassword"], configData);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (ConfiguratorException e)
            {
                throw new DispatcherException(e);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new DispatcherException(Error.UnknownError, e.Message, e);
            }
        }

        /// <summary>
        /// Creates a Response object that contains the fields of the
        /// NetConnect error response file referenced by the given
        /// filename.
        /// </summary>
        /// <remarks>
        /// <p>The file referenced by the FileInfo object is a PGP encrypted
        /// error response file that contains fields that describe the
        /// nature of the error.</p>
        ///
        /// <p>A typical file looks like this:</p>
        ///
        /// <p>The error response file looks something like this:</p>
        ///
        /// <p>Batch Sequence Number: [16601]<br />
        /// User ID: [SVQ4HDW5]<br />
        /// File ID: [945903.071112_120348.txt.pgp]<br />
        /// File Direction: [Merchant to Stratus]<br />
        /// ----------------------------------------<br />
        /// 6826: Incorrect compression type for this user</p>
        ///
        /// <p>This method takes each line that contains the delimiter (":")
        /// and splits into a name/value pair. It then removes the brackets from the
        /// value and puts the pair into a map. Lines that do not contain the
        /// delimiter are skipped.</p>
        ///
        /// <p>The map is then passed to the converter, which gives me XML that is
        /// then plugged into a new Response object to return to the client.</p>
        /// </remarks>
        /// <param name="filename">The filename of the error file. This can be an absolute path or be relative to MSDK_HOME.</param>
        /// <returns>A Response object giving easy access to the error file's fields.</returns>
        [DispId(44)]
        public IResponse CreateNetConnectError(string filename, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(configData, Error.NullConfigurationData, "ConfigurationData");
            TestForNull(filename, Error.NullFileName, "filename");
            filename = Utils.FormatDirectoryPath(filename);
            if (configData.Protocol != CommModule.SFTPBatch)
            {
                var msg = "Invalid CommModule for method call. CommModule=" + configData.Protocol.ToString();
                Logger.Error(msg);
                throw new DispatcherException(Error.InvalidModule, msg);
            }
            var fileMgr = factory.MakeFileManager();
            var path = filename;
            if (!Utils.IsAbsolutePath(path))
            {
                path = $"{factory.HomeDirectory}\\{filename}";
            }
            if (!fileMgr.Exists(path))
            {
                var msg = $"The file \"{path}\" could not be found.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNotFound, msg);
            }
            try
            {
                var processor = factory.MakeSFTPBatchProcessor();
                var response = processor.CreateNetConnectError(configData, path, null);
                return response;
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Creates a Response object that contains the fields of the
        /// NetConnect error response file referenced by the given
        /// filename.
        /// </summary>
        /// <remarks>
        /// <p>The file referenced by the FileInfo object is a PGP encrypted
        /// error response file that contains fields that describe the
        /// nature of the error.</p>
        ///
        /// <p>A typical file looks like this:</p>
        ///
        /// <p>The error response file looks something like this:</p>
        ///
        /// <p>Batch Sequence Number: [16601]<br />
        /// User ID: [SVQ4HDW5]<br />
        /// File ID: [945903.071112_120348.txt.pgp]<br />
        /// File Direction: [Merchant to Stratus]<br />
        /// ----------------------------------------<br />
        /// 6826: Incorrect compression type for this user</p>
        ///
        /// <p>This method takes each line that contains the delimiter (":")
        /// and splits into a name/value pair. It then removes the brackets from the
        /// value and puts the pair into a map. Lines that do not contain the
        /// delimiter are skipped.</p>
        ///
        /// <p>The map is then passed to the converter, which gives me XML that is
        /// then plugged into a new Response object to return to the client.</p>
        /// </remarks>
        /// <param name="filename">The filename of the error file. This can be an absolute path or be relative to MSDK_HOME.</param>
        /// <returns>A Response object giving easy access to the error file's fields.</returns>
        [DispId(45)]
        public IResponse CreateNetConnectError(string filename, string configName)
        {
            AssertInitialized();
            TestForNull(filename, Error.NullFileName, "filename");
            try
            {
                return CreateNetConnectError(filename, Configurator.GetProtocolConfig(configName));
            }
            catch (ConfiguratorException ex)
            {
                throw new DispatcherException(ex);
            }
        }

        /// <exclude />
        /// <summary>
        /// Converts a PGP response file that was downloaded outside of MSDK into
        /// a AES encrypted response file and returns a ResponseDescritor.
        /// </summary>
        /// <remarks>
        /// This is only used if the client wants to receive his response file from
        /// the Stratus on his own, without the use of the SDK. He can pass the
        /// filename of the response file he downloaded via SFTP into this method,
        /// and it will convert the file to the appropriate AES encrypted file and
        /// return a valid ResponseDescriptor for it.
        /// </remarks>
        /// <param name="pgpFile">A FileInfo object referencing the PGP encrypted response file.</param>
        /// <param name="aesFile">A FileInfo object referencing the new AES encrypted response file.</param>
        /// <param name="configData">The object that contains all of the
        /// 				  data required for transferring the file.</param>
        /// <returns>A IResponseDescriptor that wraps the file.</returns>
        private IResponseDescriptor CreateResponseDescriptor(string pgpFile, string aesFile, ConfigurationData configData)
        {
            AssertInitialized();
            TestForNull(pgpFile, Error.NullFileInfo, "PGP file");
            TestForNull(aesFile, Error.NullFileInfo, "AES file");
            TestForNull(configData, Error.NullConfigurationData, "ConfigurationData");
            TestForNull(configData["SubmissionFilePassword"], Error.NullPassword, "SubmissionFilePassword");
            pgpFile = Utils.FormatDirectoryPath(pgpFile);
            aesFile = Utils.FormatDirectoryPath(aesFile);
            if (configData["PGPMerchantPrivateKey"] == null)
            {
                Logger.Error("PGP Merchant Private Key is not set.");
                throw new DispatcherException(Error.PrivateKeyNotSet, "PGP Merchant Private Key is not set.");
            }
            var fileMgr = factory.MakeFileManager();
            if (!fileMgr.Exists(pgpFile))
            {
                var msg = $"The PGP file \"{pgpFile}\" does not exist.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNotFound, msg);
            }
            try
            {
                var processor = factory.MakeSFTPBatchProcessor();
                return processor.CreateResponseDescriptor(pgpFile, aesFile, configData);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Converts an existing AES-encrypted submission file into a PGP-
        /// encrypted submission file.
        /// </summary>
        /// <remarks>
        /// This is to allow the client to perform the potentially time
        /// consuming task of generating the PGP file ahead of time before
        /// sending the submission.
        /// </remarks>
        /// <param name="submission">The AES submission file to convert.</param>
        /// <returns>The absolute path to the generated PGP file.</returns>
        [DispId(47)]
        public string CreatePGPSubmissionFile(ISubmissionDescriptor submission)
        {
            TestForNull(submission, Error.NullSubmission, "submission");
            TestForNull(submission.Config, Error.NullConfigurationData, "configuration");
            if (!submission.BatchClosed)
            {
                Logger.Error("The batch is still open.");
                throw new DispatcherException(Error.BatchNotClosed, "The batch is still open.");
            }
            if (submission.Config.Protocol != CommModule.SFTPBatch)
            {
                Logger.Error("The CommModule is invalid for this operation.");
                throw new DispatcherException(Error.InvalidModule, "The CommModule is invalid for this operation.");
            }
            AssertValidPID(submission.Config);
            submission.Close();
            var fname = new FileName(submission.FileName)
            {
                NameAndExtension = Utils.GetUniqueFileName(
                    submission.Config["FileExtension"], submission.Config["PID"], submission.Name)
            };
            var pgpFile = fname.ToString();
            try
            {
                var processor = factory.MakeSFTPBatchProcessor();
                return processor.CreatePGPSubmissionFile(submission, pgpFile);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Converts an existing AES-encrypted submission file into a PGP-
        /// encrypted submission file.
        /// </summary>
        /// <remarks>
        /// This is to allow the client to perform the potentially time
        /// consuming task of generating the PGP file ahead of time before
        /// sending the submission.
        /// </remarks>
        /// <param name="filename">The path to the AES submission file to convert.</param>
        /// <returns>The absolute path to the generated PGP file.</returns>
        [DispId(49)]
        public string CreatePGPSubmissionFile(string filename, ConfigurationData configData)
        {
            TestForNull(filename, Error.NullFileName, "filename");
            TestForNull(configData["SubmissionFilePassword"], Error.NullPassword, "AES Password in the ProtocolManager");
            TestForNull(configData["PGPServerPublicKey"], Error.PaymentechKeyNotSet, "PGPServerPublicKey in the ConfigurationData");
            AssertValidPID(configData);
            filename = Utils.FormatDirectoryPath(filename);
            ValidateFileLength(filename);
            var password = configData["SubmissionFilePassword"];
            var submission = OpenSubmission(filename, password, configData);
            try
            {
                submission.CloseBatch();
            }
            catch (SubmissionException ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(ex.ErrorCode, ex.Message, ex);
            }
            return CreatePGPSubmissionFile(submission);
        }

        private void ValidateFileLength(string fileName)
        {
            if (fileName.Length < 1)
            {
                var msg = $"The filename \"{fileName}\" has fewer than 1 character.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNameTooShort, msg);
            }
            if (FileName.GetName(fileName).Length > 8)
            {
                var msg = $"The filename \"{fileName}\" is greater than 8 characters.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNameTooLong, msg);
            }
        }

        private bool IsSubmissionPGP(string fileName)
        {
            byte[] bytes;
            if (Utils.IsAbsolutePath(fileName) && !FileMgr.Exists(fileName))
            {
                var msg = $"The file \"{fileName}\" does not exist.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNotFound, msg);
            }
            try
            {
                var stream = factory.MakeStreamWrapper(fileName, false, false);
                try
                {
                    bytes = stream.ReadBytes(128);
                }
                finally
                {
                    stream.Close();
                }
            }
            catch (IOException ex)
            {
                var msg = $"The submission \"{fileName}\" is in use.";
                Logger.Error(msg, ex);
                throw new DispatcherException(Error.BatchNotClosed, msg, ex);
            }
            return bytes != null && Utils.ByteArrayToString(bytes).IndexOf("BEGIN PGP MESSAGE") > -1;
        }

        /// <summary>
        /// Decrypts an existing batch submission or response file.
        /// </summary>
        /// <remarks>
        /// This method will create a clear text file that contains
        /// all of the data in the submission or response.
        /// You are responsible for ensuring that the data is secure.
        /// </remarks>
        /// <param name="filename">The path to the file to be decrypted.</param>
        /// <param name="password">The password used to encrypt the file.</param>
        /// <returns>Returns the path to unencrypted file.</returns>
        [DispId(52)]
        public string DecryptFile(string filename, string password)
        {
            TestForNull(filename, Error.NullFileName, "file name");
            TestForNull(password, Error.NullPassword, "password");
            var outputFilename = Path.GetFileNameWithoutExtension(filename);
            outputFilename = FileName.Combine(Path.GetDirectoryName(filename), outputFilename);
            outputFilename = string.Concat(outputFilename, ".clear");
            var file = factory.MakeFileManager();
            if (file.Exists(outputFilename))
            {
                var msg = "The output file \"" + outputFilename + "\" already exists.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileExists, msg);
            }
            var reader = factory.MakeFileReader(filename, password, null);
            var writer = factory.MakeTextFileWriter(outputFilename, "");
            try
            {
                while (reader.HasNextRecord)
                {
                    var line = reader.GetNextRecord();
                    writer.WriteRecord(line);
                    writer.WriteRecord(Utils.StringToByteArray("\n"));
                }
            }
            finally
            {
                reader.Close();
                writer.Close();
            }
            return outputFilename;
        }
        #endregion

        #region Private Methods
        private void SendPGPSubmission(string fileName, ConfigurationData configData)
        {
            try
            {
                var processor = factory.MakeSFTPBatchProcessor();
                processor.SendSubmission(configData, fileName);
            }
            catch (DispatcherException ex)
            {
                throw;
            }
            catch (Exception ex)
            {
                Logger.Error(ex.Message, ex);
                throw new DispatcherException(Error.UnknownError, ex.Message, ex);
            }
        }

        /// <summary>
        /// Finds where MSDK was installed and stores it away for future use.
        /// The path is stored in the MSDKHome property.
        /// If the client supplied a MSDK home path, it will check if it is valid and use that one.
        /// It will throw an exception if the supplied path is not valid.
        /// If null was passed in for MSDK home, then it will search for a valid instance of it.
        /// </summary>
        /// <param name="msdkHome">The path to MSDK supplied by the client, or null if no path was specified.</param>
        private void SetMSDKHome(string msdkHome)
        {
            msdkHome = Utils.FormatDirectoryPath(msdkHome);
            try
            {
                if (factory.IsConfiguratorInitialized)
                {
                    return;
                }
                if (IsEmpty(msdkHome))
                {
                    factory.InitializeConfigurator();
                }
                else
                {
                    factory.InitializeConfigurator(msdkHome);
                }
            }
            catch (ConfiguratorException ex)
            {
                throw new DispatcherException(ex.ErrorCode, ex.Message, ex);
            }
        }

        /// <summary>Looks in the incoming directory for a response file for the submission whose name is the same as the
        /// one passed in.</summary>
        /// <remarks>The incoming directory name is stored in the config file, so it must be read from there. The name
        /// parameter is the submission's name. The response file is the submission name plus the extension ".response".
        /// This method throws an exception if the file exists.</remarks>
        /// <param name="name">      The submission name.</param>
        /// <param name="configData">Information describing the configuration.</param>
        private void VerifyResponseFileDoesNotExist(string name, ConfigurationData configData)
        {
            var incoming = configData["IncomingBatchDirectory"];
            var path = $"{name}.response";
            path = FileName.Combine(!Utils.IsAbsolutePath(incoming) ? FileName.Combine(factory.HomeDirectory, incoming) : incoming, path);
            if (!factory.MakeFileManager().Exists(path))
            {
                return;
            }
            var msg = $"The Response file {name} already exists in the incoming directory. The file must be deleted before sending this submission.";
            Logger.Error(msg);
            throw new DispatcherException(Error.FileExists, msg);
        }

        /// <summary>Verifies that the PID stored in the Security Manager is not null and is numeric.</summary>
        /// <param name="configData">Information describing the configuration.</param>
        private void AssertValidPID(ConfigurationData configData)
        {
            if (configData.Protocol != CommModule.SFTPBatch)
            {
                return;
            }
            TestForNull(configData["PID"], Error.NullPID, "PID in the ConfigurationData");
            if (Utils.IsNumeric(configData["PID"]))
            {
                return;
            }
            Logger.Error("The PID is not numeric.");
            throw new DispatcherException(Error.PIDIsNotNumeric, "The PID is not numeric.");
        }

        private ISubmissionDescriptor GetSubmissionImpl(string fileName, string password, ConfigurationData configData)
        {
            TestForNull(fileName, Error.NullFileName, "filename");
            fileName = Utils.FormatDirectoryPath(fileName);
            var name = fileName;
            if (Utils.IsAbsolutePath(fileName))
            {
                name = FileName.GetNameExtension(fileName);
            }
            var submission = factory.GetSubmission(name, password);
            if (submission != null)
            {
                if (password != null && submission.Password != password)
                {
                    var msg = "A submission already exists with this name, but with a different password.";
                    Logger.Error(msg);
                    throw new DispatcherException(Error.FileExists, msg);
                }
                if (!submission.BatchClosed)
                {
                    return submission;
                }
                factory.RemoveSubmission(submission.Name);
            }
            TestForNull(password, Error.NullPassword, "password");
            TestForNull(configData, Error.NullConfigurationData, "ConfigurationData");
            if (Utils.IsAbsolutePath(fileName))
            {
                if (FileName.GetName(fileName).Length > 8)
                {
                    var msg = "The submission name is too long.";
                    Logger.Error(msg);
                    throw new DispatcherException(Error.FileNameTooLong, msg);
                }

                if (FileMgr.Exists(fileName))
                {
                    var msg = $"The file \"{fileName}\" already exists.";
                    Logger.Error(msg);
                    throw new DispatcherException(Error.FileExists, msg);
                }
            }
            else if (fileName.Length > 8)
            {
                var msg = "The submission name is too long.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNameTooLong, msg);
            }
            if (fileName.Length == 0)
            {
                var msg = "The submission name is an empty string.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileNameTooShort, msg);
            }
            var path = this.GetFileName(fileName, null, configData["OutgoingBatchDirectory"]);
            if (path != null)
            {
                var msg = $"The file \"{path}\" already exists.";
                Logger.Error(msg);
                throw new DispatcherException(Error.FileExists, msg);
            }
            var newSubmission = factory.MakeSubmission(fileName, password, configData);
            factory.AddSubmission(newSubmission);
            return newSubmission;
        }

        /// <exclude />
        /// <summary>
        /// Throw an exception if the Dispatcher is not initialized.
        /// </summary>
        private void AssertInitialized()
        {
            if (!initialized)
            {
                var msg = "You must call Dispatcher.Initialize() before you can use the Dispatcher.";
                throw new DispatcherException(Error.NotInitialized, msg);
            }
        }
        #endregion

        #region IDispatcher Methods
        public ISubmissionDescriptor GetSubmission(string fileName, string password, string configName)
        {
            return GetSubmission(fileName, password, Configurator.GetProtocolConfig(configName));
        }

        public ISubmissionDescriptor OpenSubmission(string fileName, string password, string configName)
        {
            return OpenSubmission(fileName, password, Configurator.GetProtocolConfig(configName));
        }

        public IResponseDescriptor OpenDescriptor(string fileName, string password, string configName)
        {
            try
            {
                return OpenDescriptor(fileName, password, Configurator.GetProtocolConfig(configName));
            }
            catch (ConfiguratorException e)
            {
                throw new DispatcherException(e);
            }
        }

        public IRequest CreateRequest(string transactionType, string configName)
        {
            try
            {
                return CreateRequest(transactionType, GetConfig(configName));
            }
            catch (DispatcherException e)
            {
                throw;
            }
            catch (ConfiguratorException e)
            {
                throw new DispatcherException(e);
            }
            catch (Exception e)
            {
                throw new DispatcherException(Error.UnknownError, e.Message, e);
            }
        }

        public IResponseDescriptor ReceiveResponse(ConfigurationData configData)
        {
            return ReceiveResponse(ResponseType.Response, configData);
        }

        public IResponseDescriptor ReceiveResponse(string responseType, string password, string configName)
        {
            var configData = Configurator.GetProtocolConfig(configName);
            configData["SubmissionFilePassword"] = password;
            return ReceiveResponse(responseType, configData);
        }

        public IResponseDescriptor CreateResponseDescriptor(string filename, string configName)
        {
            TestForNull(configName, Error.NullConfigurationData, "ConfigName");

            try
            {
                return CreateResponseDescriptor(filename, Configurator.GetProtocolConfig(configName));
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (ConfiguratorException e)
            {
                throw new DispatcherException(e);
            }
            catch (Exception e)
            {
                Logger.Error(e.Message, e);
                throw new DispatcherException(Error.UnknownError, e.Message, e);
            }
        }

        public ConfigurationData GetConfig(string configName)
        {
            return Configurator.GetProtocolConfig(configName);
        }

        public ConfigurationData GetConfig(CommModule module)
        {
            try
            {
                return Configurator.GetProtocolConfig(module);
            }
            catch (DispatcherException)
            {
                throw;
            }
            catch (ConfiguratorException e)
            {
                throw new DispatcherException(e);
            }
            catch (Exception e)
            {
                throw new DispatcherException(Error.UnknownError, e.Message, e);
            }
        }

        public byte[] BuildMessage(IRequest req)
        {
            var request = factory.RequestToImpl(req);

            if (request.Config.Protocol == CommModule.SFTPBatch || request.Config.Protocol == CommModule.TCPBatch)
            {
                try
                {
                    request.SetDefaultValues();
                    var converter = factory.MakeBatchConverter(request.Config);
                    var args = converter.ConvertRequest(request);
                    return args.ReqByteArray;
                }
                catch (ConverterException e)
                {
                    throw new DispatcherException(e);
                }
                catch (RequestException e)
                {
                    throw new DispatcherException(e);
                }
            }
            try
            {
                request.SetDefaultValues();
                var converter = factory.MakeOnlineConverter(request.Config, request.MessageFormat);
                var args = converter.ConvertRequest(request);
                return args.ReqByteArray;
            }
            catch (ConverterException e)
            {
                throw new DispatcherException(e);
            }
            catch (RequestException e)
            {
                throw new DispatcherException(e);
            }
        }
        #endregion
    }
}
