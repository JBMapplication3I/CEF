using System.Runtime.InteropServices;

namespace JPMC.MSDK
{
    /// <summary>
    /// This enum contains all of the error codes that the MSDK throws.
    /// These are separated numerically by component.
    /// </summary>
    [ComVisible( true )]
    public enum Error
    {
        /// <summary>
        /// No error was encountered.
        /// </summary>
        None = 0,

        /// <summary>
        /// An unknown error was encountered.
        /// </summary>
        UnknownError = 1,

        // Dispatcher Errors

        /// <summary>
        /// This is the base for Dispatcher errors.
        /// This should never be used.
        /// </summary>
        DispatcherErrorBase = 100,
        /// <summary>
        /// The method or property used is not supported.
        /// </summary>
        /// <remarks>
        /// This should never be thrown in production. If it does, contact support.
        /// </remarks>
        NotSupported,

        /// <summary>
        /// The Dispatcher was never initialized.
        /// </summary>
        /// <remarks>
        /// This is most likely to happen when using MSDK via COM interop.
        /// When you create the Dispatcher as a COM object, you must call its
        /// <see cref="JPMC.MSDK.Dispatcher.Initialize(JPMC.MSDK.NetMode)"/>
        /// method first before calling any method or accessing any property.
        /// If you do not, you are likely to get this error.
        /// </remarks>
        NotInitialized,

        /// <summary>
        /// The directory path where the SDK resides on the computer could
        /// not be found, or the path supplied to the Dispatcher does not
        /// reference a valid directory.
        /// </summary>
        /// <remarks>
        /// There are two ways to specify where the SDK is located: by
        /// setting the MSDK_HOME environment variable or by passing
        /// the path in when you construct (or initialize) the Dispatcher.
        /// The Dispatcher verifies that the path that it tries to use
        /// is valid (meaning it's a real directory and the SDK files
        /// are in it). If it is not valid, this error will be thrown.
        /// </remarks>
        InvalidHomeDirectory,

        /// <summary>
        /// The transaction type specified in
        /// <see cref="JPMC.MSDK.Dispatcher.CreateRequest"/>
        /// is not valid.
        /// </summary>
        /// <remarks>
        /// You must specify a valid transaction type. Check the spelling
        /// and look it up in the Developer's Guide.
        /// </remarks>
        InvalidRequestType,

        /// <summary>
        /// An attempt was made to pass the Request of an Additional Format
        /// to ProcessRequest.
        /// </summary>
        /// <remarks>
        /// You can only pass IRequest objects into ProcessRequest that were
        /// created by calling <see cref="JPMC.MSDK.Dispatcher.CreateRequest"/>.
        /// You will get this error if you pass in a Request returned by the
        /// <see cref="JPMC.MSDK.IRequest.GetAdditionalFormat"/>
        /// method.
        /// </remarks>
        InvalidRequest,

        /// <summary>
        /// A Request object that is not valid for heartbeats was passed into
        /// <see cref="JPMC.MSDK.Dispatcher.SendHeartbeat"/>.
        /// </summary>
        /// <remarks>
        /// Refer to the Developer's Guide to determine which transaction
        /// types are valid for Heartbeat Requests.
        /// </remarks>
        InvalidHeartbeat,

        /// <summary>
        /// The file path supplied does not exist or is not valid for the
        /// operation.
        /// </summary>
        /// <remarks>
        /// Make sure that the path you specified exists.
        /// </remarks>
        InvalidPath,

        /// <summary>
        /// The <see cref="JPMC.MSDK.Module"/> that was
        /// passed to the method is not valid for this operation.
        /// </summary>
        /// <remarks>
        /// Not all Modules are valid for every operation. Refer to
        /// the Developer's Guide to find out which Modules are available
        /// for the method you are trying to call.
        /// </remarks>
        InvalidModule,

        /// <summary>
        /// The <see cref="JPMC.MSDK.ResponseType"/> is
        /// not valid for this operation.
        /// </summary>
        InvalidResponseType,

        /// <summary>
        /// The ErrorDelimiter was not set in the SFTPBatch section of the
        /// SFTPBatchConfig.xml file.
        /// </summary>
        /// <remarks>
        /// The ErrorDelimiter field in SFTPBatch property section in the
        /// SFTPBatchConfig.xml file must be set to a valid value. Typically, this
        /// value is a colon (:), but it can be any delimiter that you
        /// have specified when certifying with JPMC.
        /// </remarks>
        NullDelimiter,

        /// <summary>
        /// The Request parameter is null.
        /// </summary>
        NullRequest,

        /// <summary>
        /// The template name parameter is null.
        /// </summary>
        NullTemplateName,

        /// <summary>
        /// The header name parameter is null.
        /// </summary>
        NullHeaderName,
        /// <summary>
        /// The password parameter is null.
        /// </summary>
        NullPassword,

        /// <summary>
        /// The PID property in the ProtocolManager is not set.
        /// </summary>
        NullPID,

        /// <summary>
        /// The filename parameter is null.
        /// </summary>
        NullFileName,

        /// <summary>
        /// The FileInfo parameter is null.
        /// </summary>
        NullFileInfo,

        /// <summary>
        /// The SubmissionDescriptor parameter is null.
        /// </summary>
        NullSubmission,

        /// <summary>
        /// The ProtocolManager parameter is null.
        /// </summary>
        NullConfigurationData,

        /// <summary>
        /// The PID property in the ProtocolManager is not a numeric value.
        /// </summary>
        PIDIsNotNumeric,

        /// <summary>
        /// The specified header info block does not exist in the config
        /// file.
        /// </summary>
        /// <remarks>
        /// The name you specified when calling this method must refer to
        /// an element in the GlobalBatch/HeaderInfo block in the config file.
        /// </remarks>
        InvalidHeaderInfo,

        /// <summary>
        /// The specified file already exists.
        /// </summary>
        FileExists,

        /// <summary>
        /// The specified file could not be found.
        /// </summary>
        FileNotFound,

        /// <summary>
        /// The batch is not closed.
        /// </summary>
        /// <remarks>
        /// <p>The operation cannot be performed on an open batch submission.
        /// You must first call <see cref="JPMC.MSDK.ISubmissionDescriptor.CloseBatch"/>
        /// before you can call this method.</p>
        /// <p>
        /// <strong>NOTE:</strong> Once you have closed the batch, you cannot
        /// add any more orders to it. It is now ready for sending to the server.
        /// </p>
        /// </remarks>
        BatchNotClosed,

        /// <summary>
        /// The batch has already been closed and cannot be modified.
        /// </summary>
        /// <remarks>
        /// Once a batch submission is closed, it is effectively read-only.
        /// The files trailers have been appended to the file and so it
        /// is complete.
        /// </remarks>
        BatchNotOpen,

        /// <summary>
        /// One of the Private Key fields in the <see cref="JPMC.MSDK.ProtocolManager"/>
        /// that you passed to this method
        /// has not been set.
        /// </summary>
        /// <remarks>
        /// You should consult the Developer's Guide to determine what key
        /// fields must be set in the ProtocolManager before passing it
        /// to the method.
        /// </remarks>
        PrivateKeyNotSet,

        /// <summary>
        /// The PGPPaymentechPrivateKey field of the
        /// <see cref="JPMC.MSDK.ProtocolManager"/>
        /// must be set before performing this operation.
        /// </summary>
        PaymentechKeyNotSet,

        /// <summary>
        /// A response was received, but it does belong to the Request
        /// that was sent.
        /// </summary>
        /// <remarks>
        /// If you received this error, then for some reason the
        /// server sent the wrong response back for your
        /// request. You may need to consult support to resolve this.
        /// </remarks>
        NoResponseReceivedError,

        /// <summary>
        /// A response was received, but it does appear to belong to the
        /// Request that was sent.
        /// </summary>
        /// <remarks>
        /// The MerchantOrderNumber field of the response does not match
        /// that of the request. This indicates that the response does not
        /// belong to the request that was sent. You may need to consult
        /// support to resolve this.
        /// </remarks>
        TransactionInconsistency,

        /// <summary>
        /// The specified filename is too long.
        /// </summary>
        /// <remarks>
        /// Submission names must not be longer that 8 characters.
        /// </remarks>
        FileNameTooLong,

        /// <summary>
        /// The submission name passed is empty (0 characters in length).
        /// </summary>
        FileNameTooShort,

        /// <summary>
        /// The SDK encountered an error while trying to send a transaction
        /// to the payment processing server.
        /// </summary>
        ProxyException,

        /// <summary>
        /// The SDK timed out while trying to send the transaction to
        /// payment processing server.
        /// </summary>
        ProxyTimeoutToStratus,

        // Request Errors

        /// <summary>
        /// This is the base for Request errors.
        /// This should never be used.
        /// </summary>
        RequestErrorBase = 200,

        /// <summary>
        /// The specified field is required and must be set to a value.
        /// </summary>
        RequiredFieldNotSet,

        /// <summary>
        /// The data node was not found. The template may be corrupt.
        /// </summary>
        /// <remarks>
        /// You should never get this. If you do, then there may be a
        /// problem with the template file. Contact support.
        /// </remarks>
        DataElementNotFound,

        /// <summary>
        /// The specified field does not exist.
        /// </summary>
        FieldDoesNotExist,

        /// <summary>
        /// The field name parameter is null.
        /// </summary>
        NullFieldName,

        /// <summary>
        /// The field is not valid. It is either read-only or does not exist.
        /// </summary>
        InvalidField,

        /// <summary>
        /// The specified field's value contains non-numeric characters.
        /// </summary>
        /// <remarks>
        /// An attempt was made to call GetIntField or GetLongField on a field
        /// that does not contain a numeric value.
        /// </remarks>
        FieldNotNumeric,

        /// <summary>
        /// An attempt was made to get an additional format when the maximum
        /// allowed number of that specific format type has already been created.
        /// </summary>
        MaxFormatsExceeded,

        /// <summary>
        /// An attempt was made to process the request before the minimum number
        /// of the specified additional formats has been created.
        /// </summary>
        MinFormatsNotMet,

        /// <summary>
        /// An attempt was made to set a value to a field that is read-only.
        /// </summary>
        ReadOnlyField,


        // Configurator Errors
        /// <summary>
        /// An error occurred while trying to initialze the configuration
        /// of the SDK.
        /// </summary>
        /// <remarks>
        /// This is typically caused by a problem with one of the config files.
        /// Make sure the file exists in the config directory of MSDK_HOME
        /// and that it is not corrupt.
        /// </remarks>
        ConfigInitError   = 300,


        /// Communication Module Errors
        ///
        /// <summary>
        /// This is the base for CommManager errors.
        /// This should never be used.
        /// </summary>
        CommModuleErrorBase = 400,

        /// Socket Errors
        ///
        /// <summary>
        /// An exception occurred during a TCP connect attempt.
        /// </summary>
        ConnectFailure,

        /// <summary>
        /// A TCP connect attempt failed because it took too much time
        /// to finish.
        /// </summary>
        ConnectTimeoutFailure,

        /// <summary>
        /// An exception occurred while trying to write to a socket
        /// </summary>
        /// <remarks>
        /// If you receive this error it is safe to retry without fear
        /// of generating a duplicate auth/transaction
        /// </remarks>
        WriteFailure,

        /// <summary>
        /// A write to a socket failed because it took too much time
        /// to finish.
        /// </summary>
        WriteTimeoutFailure,

        /// <summary>
        /// An exception occurred while trying to read from a socket
        /// </summary>
        /// <remarks>
        /// When receiving this error, the transaction may have succeeded
        /// so there is a risk of duplicating a transaction if it is
        /// retried
        /// </remarks>
        ReadFailure,

        /// <summary>
        /// A read from a socket failed because it took too much time
        /// to finish.
        /// </summary>
        /// <remarks>
        /// When receiving this error, the transaction may have succeeded
        /// so there is a risk of duplicating a transaction if it is
        /// retried
        /// </remarks>
        ReadTimeoutFailure,

        /// <summary>
        /// A failure occurred while using a socket "select"
        /// </summary>
        SelectFailure,

        /// <summary>
        /// A failure occurred while closing a socket
        /// </summary>
        CloseFailure,

        /// <summary>
        /// Could not find the IP address that goes with
        /// the specified host or could not find the
        /// specified IP address
        /// </summary>
        IPNotFound,


        /// HTTPS Module Errors
        ///
        /// <summary>
        /// The JPMC netconnect gateway has returned an error
        /// </summary>
        GatewayFailure,

        /// <summary>
        /// There was an error returned from HTTPS
        /// </summary>
        HTTPSFailure,

        /// <summary>
        /// Server certificate checks have failed
        /// </summary>
        TrustFailure,

        /// <summary>
        /// Something went wrong while doing the SSL handshake
        /// </summary>
        SSLHandshakeFailure,


        /// Internal Errors
        ///
        /// <summary>
        /// Requested communication module is not available
        /// </summary>
        ModuleUnavailable,

        /// <summary>
        /// An error occured while initializing the communication
        /// module.
        /// </summary>
        InitializationFailure,

        /// <summary>
        /// Arguments passed to communication module are not appropriate
        /// </summary>
        ArgumentMismatch,

        /// <summary>
        /// Arguments passed to the communication module are invalid
        /// </summary>
        ArgumentMalformed,

        /// <summary>
        /// The limit to the number of threads that can use the communication
        /// module has been reached
        /// </summary>
        MaxThreadsExceeded,

        /// <summary>
        /// An error occurring while a response is interpreted
        /// </summary>
        ResponseFailure,

        /// <summary>
        /// An unknown exception has occurred while interfacing with
        /// the operating system.
        /// </summary>
        UnknownFailure,

        /// <summary>
        /// The specified SFTP directory in SFTPBatchConfig.xml is not valid
        /// </summary>
        UnknownSFTPDirectroy,

        /// <summary>
        /// An error while writing the file to the SFTP server
        /// </summary>
        SFTPWriteError,

        /// <summary>
        /// An error while reading the file from the SFTP server
        /// </summary>
        SFTPReadError,

        /// <summary>
        /// Missing SFTP related attributes in SFTPBatchConfig.xml or SFTP security manager
        /// </summary>
        SFTPMissingAttr,

        // Converter Exception
        //,RequestXMLParser = 500
        /// <summary>
        /// Error while initializing the MSDK request template
        /// </summary>
        RequestInit = 500,

        /// <summary>
        /// Request template is missing the format definition
        /// </summary>
        RequestFormatNotFound,

        /// <summary>
        /// Request contains bad data
        /// </summary>
        RequestBadData,

        /// <summary>
        /// Request template is missing required format/field attribute
        /// </summary>
        RequestMissingAttr,

        /// <summary>
        /// Mimatch in merchant template and converter template
        /// </summary>
        RequestUnknownVersion,

        /// <summary>
        /// Error while initializing the MSDK response template
        /// </summary>
        ResponseInit,

        /// <summary>
        /// Error while parsing the response template
        /// </summary>
        ResponseXmlParser,

        /// <summary>
        /// Response contains bad data
        /// </summary>
        ResponseBadData,

        /// <summary>
        /// Response template is missing required format/field attribute
        /// </summary>
        ResponseFormatNotFound,

        /// <summary>
        /// Error while creating the response xml object
        /// </summary>
        ResponseXmlCreation,

        /// <summary>
        /// Error while initializing the response xml object
        /// </summary>
        XMLInitError,

        /// <summary>
        /// NO data to add to the  xml object
        /// </summary>
        XMLNoData,

        // SubmissionDescriptor Errors

        /// <summary>
        /// The Request parameter is not a valid header.
        /// </summary>
        InvalidHeaderRequest = 600,

        /// <summary>
        /// The submission file is not open.
        /// </summary>
        FileNotOpen,

        /// <summary>
        /// An error occurred while creating the submission file.
        /// </summary>
        FileCreationFailure,

        /// <summary>
        /// An error occurred while trying to add an order to the
        /// submission file. The add operation failed.
        /// </summary>
        AddToSubmissionFailed,

        // Response Errors

        /// <summary>
        /// The Response parameter is null.
        /// </summary>
        NullResponse = 700,

        // Filer Errors

        /// <summary>
        /// The file header parameter is null.
        /// </summary>
        /// <remarks>
        /// This is an internal error. Contact support.
        /// </remarks>
        NullFileHeader = 800,

        /// <summary>
        /// Internal error. Contact support.
        /// </summary>
        FileTypeTooLong,

        /// <summary>
        /// Internal error. Contact support.
        /// </summary>
        FormatNameTooLong,

        /// <summary>
        /// Internal error. Contact support.
        /// </summary>
        RecordDelimiterTooLong,

        /// <summary>
        /// Internal error. Contact support.
        /// </summary>
        VersionTooLong,

        /// <summary>
        /// Internal error. Contact support.
        /// </summary>
        InvalidFileHeader,

        /// <summary>
        /// An encryption error occurred.
        /// </summary>
        /// <remarks>
        /// If this occurred while adding an order to a batch submission,
        /// then it is likely that you did not supply the correct password.
        /// If it occurred while trying to send the submission, then make
        /// sure your PGP keys are set properly.
        /// </remarks>
        EncryptionFailed,

        /// <summary>
        /// An order failed to decrypt.
        /// </summary>
        /// <remarks>
        /// If this occurred while iterating through a response or submission
        /// file, then it is likely that you did not supply the correct
        /// password. If it occurred while receiving the response file, then
        /// make sure your PGP keys are set properly.
        /// </remarks>
        DecryptionFailed,

        /// <summary>
        /// A problem occurred while trying to read the submission file.
        /// </summary>
        /// <remarks>
        /// This could indicate a corrupt file. Contact support.
        /// </remarks>
        SeekFailed,

        /// <summary>
        /// Failed to initialize the batch Rule Engine. This is typically
        /// because the RuleEngine.xml could not be loaded.
        /// </summary>
        RuleEngineInitFailed,

        /// <summary>
        /// The order separator has invalid data.
        /// </summary>
        InvalidOrderSeparator,

        // ResponseDescriptor Errors

        /// <summary>
        /// An attempt was made to read beyond the end of the file.
        /// </summary>
        EndOfFile = 900,

        /// <summary>
        /// A problem occurred while trying to open the file.
        /// </summary>
        /// <remarks>
        /// This is likely a permissions problem. Make sure that no other
        /// process is accessing this file and that the file has the
        /// right security permissions.
        /// </remarks>
        FileAccessError,

        /// <summary>
        /// An attempt was made to access the descriptor after it was closed.
        /// </summary>
        DescriptorClosed,

        /// <summary>
        /// Some protocols require a ProtocolManager object
        /// </summary>
        TcpSecurityManagerMissing,

        /// <summary>
        /// A required format was not found during conversion.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        FormatNotFoundError,

        /// <summary>
        /// Unexpected data was found while parsing the converter templates.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        BadDataError,

        /// <summary>
        /// A required attribute was missing in a converter template.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        MissingAttrError,

        /// <summary>
        /// An error occurred while parsing the converter templates.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        XMLParserError,

        /// <summary>
        /// An entity in the converter template has the wrong length.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        LengthError,

        /// <summary>
        /// The converter template version is wrong.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        UnknownVerError,

        /// <summary>
        /// The converter encountered an error while creating XML.
        /// </summary>
        /// <remarks>
        /// This can indicated a problem with the converter templates.
        /// </remarks>
        XMLCreationError,

        /// <summary>
        /// A request to process a request has failed.
        /// </summary>
        RequestFailure,

        /// <summary>
        /// The protocol-specific ConfigurationData does not exist.
        /// </summary>
        /// <remarks>
        /// You might have specified a configuration name that does not exist in MSDKConfig.xml.
        /// </remarks>
        ConfigurationNotFound,

        /// <summary>
        /// A generic error occurred while trying to perform an operation.
        /// </summary>
        InvalidOperation,

        /// <summary>
        /// Failure while loading or accessing the Definitions.jar file.
        /// </summary>
        DefinitionsFileFailure,

        /// <summary>
        /// The definitions.jar is not valid for this version of the SDK.
        /// </summary>
        TemplateLibraryMismatch,

        /// <summary>
        /// A parameter to a method was null.
        /// </summary>
        NullParameter,
        /// <summary>
        /// The password is not valid.
        /// </summary>
        InvalidPassword,
        /// <summary>
        /// The value for this field is too long.
        /// </summary>
        FieldTooLong,
        /// <summary>
        /// The field is hidden and cannot be set.
        /// </summary>
        FieldHidden
    }
}
