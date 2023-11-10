#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK
{
    public interface IDispatcher
    {
        /// <summary>
        /// Initiates a heartbeat to the server. It takes the Request object that you
        /// constructed and sends it to the server. It will verify that the Request
        /// object you sent is a correct Heartbeat request, and it will also
        /// automatically fill in the required fields for it.
        /// </summary>
        /// <param name="request">The heartbeat request to send to the server.</param>
        /// <returns>The response from the server.</returns>
        IResponse SendHeartbeat( IRequest request );

        /// <summary>
        /// Either creates a new SubmissionDescriptor or returns an existing one that
        /// has yet to be closed.
        ///
        /// It will return an existing one if both the filename and password supplied
        /// matches one of the already opened submissions. If the filename does not
        /// match any opened submissions, a new one will be created. If a match is
        /// found it will be returned. However, if a submission with a matching name
        /// is found, but the password does not match, an exception will be thrown.
        /// </summary>
        /// <param name="fileName">The name of the file that the SubmissionDescriptor will be associated with.</param>
        /// <param name="password">The password to use for encrypting the batch file.</param>
        /// <param name="configName">Specifies the ConfigurationData to use for the submission.</param>
        /// <returns>A SubmissionDescriptor used to add orders to for submitting.</returns>
        ISubmissionDescriptor GetSubmission( string fileName, string password, string configName );

        /// <summary>
        /// Either creates a new SubmissionDescriptor or returns an existing one that
        /// has yet to be closed.
        ///
        /// It will return an existing one if both the filename and password supplied
        /// matches one of the already opened submissions. If the filename does not
        /// match any opened submissions, a new one will be created. If a match is
        /// found it will be returned. However, if a submission with a matching name
        /// is found, but the password does not match, an exception will be thrown.
        /// </summary>
        /// <param name="fileName">The name of the file that the SubmissionDescriptor will be associated with.</param>
        /// <param name="password">The password to use for encrypting the batch file.</param>
        /// <param name="configData">The ConfigurationData to use for the submission.</param>
        /// <returns>A SubmissionDescriptor used to add orders to for submitting.</returns>
        ISubmissionDescriptor GetSubmission( string fileName, string password, ConfigurationData configData );

        /// <summary>
        /// Opens a submission file.
        /// </summary>
        /// <remarks>
        /// If the batch has already been closed for this submission (by having
        /// called closeBatch), then you cannot add more orders to the opened
        /// SubmissionDescriptor. You can add more orders if the submission hasn't
        /// been closed yet.
        /// </remarks>
        /// <param name="fileName">The name of the file that the SubmissionDescriptor will be associated with.</param>
        /// <param name="password">The password to use for encrypting the batch file.</param>
        /// <param name="configName">Specifies the ConfigurationData to use for the submission.</param>
        /// <returns>The new descriptor that the client will use to add requests to the file.</returns>
        ISubmissionDescriptor OpenSubmission( string fileName, string password, string configName );

        /// <summary>
        /// Opens a submission file.
        /// </summary>
        /// <remarks>
        /// If the batch has already been closed for this submission (by having
        /// called closeBatch), then you cannot add more orders to the opened
        /// SubmissionDescriptor. You can add more orders if the submission hasn't
        /// been closed yet.
        /// </remarks>
        /// <param name="fileName">The name of the file that the SubmissionDescriptor will be associated with.</param>
        /// <param name="password">The password to use for encrypting the batch file.</param>
        /// <param name="configData">The ConfigurationData to use for the submission.</param>
        /// <returns>The new descriptor that the client will use to add requests to the file.</returns>
        ISubmissionDescriptor OpenSubmission( string fileName, string password, ConfigurationData configData );

        /// <summary>
        /// Sends the submission to the server. The submission will
        /// get converted into the proper payload and then transmitted to the
        /// server using the specified protocol.
        /// </summary>
        /// <remarks>
        /// NOTE: If the submission has never been closed (by calling
        /// ISubmissionDescriptor.closeBatch()), closeBatch will be called
        /// automatically at this time.
        /// </remarks>
        /// <param name="submission">The descriptor for the submission to send.</param>
        void SendSubmission( ISubmissionDescriptor submission );

        /// <summary>
        /// Creates a new ResponseDescriptor for an existing response file and
        /// returns it to the client. The client can now read that file and process
        /// its records.
        /// </summary>
        /// <param name="fileName">The name of the file to open. It could be a complete path, or relative to MSDK_HOME.</param>
        /// <param name="password">The password to be used to decrypt the file.</param>
        /// <param name="configName">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>A ResponseDescriptor giving access to all the records.</returns>
        IResponseDescriptor OpenDescriptor( string fileName, string password, string configName );

        /// <summary>
        /// Creates a new ResponseDescriptor for an existing response file and
        /// returns it to the client. The client can now read that file and process
        /// its records.
        /// </summary>
        /// <param name="fileName">The name of the file to open. It could be a complete path, or relative to MSDK_HOME.</param>
        /// <param name="password">The password to be used to decrypt the file.</param>
        /// <param name="configData">The ConfigurationData to use for the descriptor.</param>
        /// <returns>A ResponseDescriptor giving access to all the records.</returns>
        IResponseDescriptor OpenDescriptor( string fileName, string password, ConfigurationData configData );

        /// <summary>
        /// Creates a new Request for the client. The client can set all of the data
        /// fields for the request and then pass it to the processRequest call. The
        /// Request object will only allow the client to enter fields that are valid
        /// for the chosen transaction type.
        /// </summary>
        /// <param name="transactionType">The type of transaction the client is requesting.</param>
        /// <param name="configName">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>The created request that is loaded with the fields appropriate for the specified transaction.</returns>
        IRequest CreateRequest( string transactionType, string configName );

        /// <summary>
        /// Creates a new Request for the client. The client can set all of the data
        /// fields for the request and then pass it to the processRequest call. The
        /// Request object will only allow the client to enter fields that are valid
        /// for the chosen transaction type.
        /// </summary>
        /// <param name="transactionType">The type of transaction the client is requesting.</param>
        /// <param name="configData">The ConfigurationData to use for the request.</param>
        /// <returns>The created request that is loaded with the fields appropriate for the specified transaction.</returns>
        IRequest CreateRequest( string transactionType, ConfigurationData configData );

        /// <summary>
        /// This method will turn the data of the supplied Request object into the
        /// appropriate payload format and then passes it to the communication layer
        /// of the SDK.
        /// </summary>
        /// <remarks>
        /// The method will then block until the comm layer returns the server's
        /// response. The processRequest will convert the response payload into a
        /// Response object and return it to the client.
        ///
        /// The one task that is not so apparent is that it must verify that the
        /// response received belongs to the request that was sent. The converter
        /// will handle most of this by supplying a message format string for each
        /// payload, and then comparing them in the call to validateResponse.
        ///
        /// NOTE: This overloaded method differs from its original in that you cannot
        /// specify the Comm Module to use for the request. The module is determined
        /// by a value in the config file.
        /// </remarks>
        /// <param name="request">The Request object created by the client.</param>
        /// <returns>A client-friendly object containing the data of the server's response.</returns>
        IResponse ProcessRequest( IRequest request );

        /// <summary>
        /// Releases all references and resources, and cleans up. This should be
        /// called whenever the client is done with the Dispatcher, as this method
        /// closes the connection with the server.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Tells the communication layer to get the file and then receives the file
        /// from the communication layer. This method will block until the response
        /// has finished downloading. If an RFR is needed to initiate the download, a
        /// default RFR request will be created automatically.
        /// </summary>
        /// <param name="configData">The ConfigurationData to use for the operation.</param>
        /// <returns>A ResponseDescriptor that provides access to the records of the response.</returns>
        IResponseDescriptor ReceiveResponse( ConfigurationData configData );

        /// <summary>
        /// Tells the communication layer to get the file and then receives the file
        /// from the communication layer. This method will block until the response
        /// has finished downloading. If an RFR is needed to initiate the download, a
        /// default RFR request will be created automatically.
        /// </summary>
        /// <param name="responseType">The type of response to download. This could be a normal response file, or one of several report file types.</param>
        /// <param name="password">The password to use when encrypting the file.</param>
        /// <param name="configName">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>A ResponseDescriptor that provides access to the records of the response.</returns>
        IResponseDescriptor ReceiveResponse( string responseType, string password, string configName );

        /// <summary>
        /// Tells the communication layer to get the file and then receives the file
        /// from the communication layer. This method will block until the response
        /// has finished downloading. If an RFR is needed to initiate the download, a
        /// default RFR request will be created automatically.
        /// </summary>
        /// <param name="responseType">The type of response to download. This could be a normal response file, or one of several report file types.</param>
        /// <param name="configData">The ConfigurationData to use for the descriptor.</param>
        /// <returns>A ResponseDescriptor that provides access to the records of the response.</returns>
        IResponseDescriptor ReceiveResponse( string responseType, ConfigurationData configData );

        /// <summary>
        /// Deletes the specified file from the server. The file
        /// specified does not represent a file on the client's computer, but instead
        /// refers to a file on the server. This method will properly
        /// delete the file from all servers (including backup servers).
        /// </summary>
        /// <remarks>
        /// The method returns true if the file was successfully deleted from all
        /// servers. If it failed to delete it from even just one server, the method
        /// will return false. You can call this method as many times as it takes to
        /// delete all of the files. However, you may want to wait or call support if
        /// it fails to delete them all the first time.
        /// </remarks>
        /// <param name="filename">The name of the file to delete from the server.</param>
        /// <param name="configData">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>True if the file was successfully deleted, false if not.</returns>
        bool DeleteServerFile( string filename, ConfigurationData configData );

        /// <summary>
        /// This method simply decrypts the submission file so that the client can
        /// transmit the submission to the Stratus manually.
        /// </summary>
        /// <param name="submission">The submission to decrypt.</param>
        /// <returns>The absolute path to the decrypted file.</returns>
        string CreatePGPSubmissionFile( ISubmissionDescriptor submission );

        /// <summary>
        /// This method simply decrypts the submission file so that the client can
        /// transmit the submission to the Stratus manually.
        /// </summary>
        /// <param name="filename">The path to the submission file.</param>
        /// <param name="configData">Configuration Data for the operation.</param>
        /// <returns></returns>
        string CreatePGPSubmissionFile( string filename, ConfigurationData configData );

        /// <summary>
        /// This is only used if the client wants to receive his response file from
        /// the server on her own, without the use of the SDK. He
        /// can pass the filename of the response file he downloaded via SFTP into
        /// this method, and it will convert the file to the appropriate AES
        /// encrypted file and return a valid ResponseDescriptor for it.
        /// </summary>
        /// <param name="filename">The filename of the response file.</param>
        /// <param name="configName">The name of a configuration defined in MSDKConfig.xml.</param>
        /// <returns>A ResponseDescriptor that provides access to the records of the response.</returns>
        IResponseDescriptor CreateResponseDescriptor( string filename, string configName );

        /// <summary>
        /// This is only used if the client wants to receive his response file from
        /// the server on her own, without the use of the SDK. He
        /// can pass the filename of the response file he downloaded via SFTP into
        /// this method, and it will convert the file to the appropriate AES
        /// encrypted file and return a valid ResponseDescriptor for it.
        /// </summary>
        /// <param name="filename">The filename of the response file.</param>
        /// <param name="configData">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>A ResponseDescriptor that provides access to the records of the response.</returns>
        IResponseDescriptor CreateResponseDescriptor( string filename, ConfigurationData configData );

        /// <summary>
        /// Converts an existing response error file that the client downloaded
        /// separately into a IResponse object that can be queried within MSDK.
        /// </summary>
        /// <param name="filename">The path to the PGP file that is received from the server.</param>
        /// <param name="configName">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>A Response object that provides access the fields of the error response.</returns>
        IResponse CreateNetConnectError( string filename, string configName );

        /// <summary>
        /// Converts an existing response error file that the client downloaded
        /// separately into a IResponse object that can be queried within MSDK.
        /// </summary>
        /// <param name="filename">The path to the PGP file that is received from the server.</param>
        /// <param name="configData">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>A Response object that provides access the fields of the error response.</returns>
        IResponse CreateNetConnectError( string filename, ConfigurationData configData );

        /// <summary>
        /// Tells the communication layer to get the file and then receives the file
        /// from the communication layer. This method will block until the response
        /// has finished downloading. It will download the file as named on the Chase
        /// server, and will then rename it to the merchant-supplied
        /// filename. The merchant-supplied filename was specified when the original
        /// submission was sent, and is embedded in the file as one record.
        /// </summary>
        /// <param name="request">The RFR request object that describes the type of response to retrieve.</param>
        /// <param name="password">The password to encrypt the received file with.</param>
        /// <returns>A Response object that provides access the fields of the response.</returns>
        IResponseDescriptor ReceiveResponse( IRequest request, string password );

        /// <summary>
        /// Tells the communication layer to get the file and then receives the file
        /// from the communication layer. This method will block until the response
        /// has finished downloading. It will download the file as named on the Chase
        /// server, and will then rename it to the merchant-supplied
        /// filename. The merchant-supplied filename was specified when the original
        /// submission was sent, and is embedded in the file as one record.
        /// </summary>
        /// <remarks>
        /// NOTE: This method assumes that the SubmissionFilePassword field is set in the
        /// Request's ConfigurationData.
        /// </remarks>
        /// <param name="request">The RFR request object that describes the type of response to retrieve.</param>
        /// <returns>A Response object that provides access the records of the response file.</returns>
        IResponseDescriptor ReceiveResponse( IRequest request );

        /// <summary>
        /// Processes the request to mark the start of an upload batch.
        /// </summary>
        /// <param name="request">The header request.</param>
        /// <returns>The response from the request.</returns>
        IResponse StartPNSUpload( IRequest request );

        /// <summary>
        /// Processes the request as part of an upload batch.
        /// </summary>
        /// <param name="request">The order request.</param>
        /// <returns>The response from the request.</returns>
        IResponse ProcessPNSUpload( IRequest request );

        /// <summary>
        /// Processes the request to mark the end of an upload batch.
        /// </summary>
        /// <param name="request">The trailer request.</param>
        /// <returns>The response from the request.</returns>
        IResponse EndPNSUpload( IRequest request );

        /// <summary>
        /// Gets a user-modifiable ConfigurationData object based on the
        /// name supplied.
        /// </summary>
        /// <param name="configName">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>The object that allows you to override the default configuration.</returns>
        ConfigurationData GetConfig( string configName );

        /// <summary>
        /// Gets a user-modifiable ConfigurationData object based on the
        /// name supplied.
        /// </summary>
        /// <param name="module">Specifies the ConfigurationData to use for the descriptor.</param>
        /// <returns>The object that allows you to override the default configuration.</returns>
        ConfigurationData GetConfig( CommModule module );

        /// <summary>
        /// Sends the submission to the server. The filename can
        /// reference either an AES submission file or a PGP submission file. If the
        /// fileName refers to an AES file, it will be converted into a PGP file and
        /// then that will be transmitted to the server.
        /// </summary>
        /// <param name="fileName">The name of the file to be sent.</param>
        /// <param name="configData">The ConfigurationData that contains data required for sending the file.</param>
        void SendSubmission( string fileName, ConfigurationData configData );

        /// <summary>
        /// Converts the request to its proper message format and returns it as a byte array.
        /// </summary>
        /// <param name="request">The Request object containing the message data.</param>
        /// <returns>A byte array containing the converted message.</returns>
        byte[] BuildMessage( IRequest request );

        /// <summary>
        /// Gets the path to where the SDK was installed.
        /// </summary>
        string HomeDirectory { get; }

        void CancelPNSUpload(string mid, string tid, ConfigurationData configData);
    }
}
