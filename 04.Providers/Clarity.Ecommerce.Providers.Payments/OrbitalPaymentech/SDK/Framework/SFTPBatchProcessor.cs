using System;
using System.Collections.Generic;
using JPMC.MSDK.Comm;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Filer;

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Performs all batch processing for the SFTP protocol.
    /// </summary>
    public class SFTPBatchProcessor : DispatcherBase, ISFTPBatchProcessor
    {
        /// <summary>
        /// Constructs a new object and sets the factory to be used.
        /// </summary>
        /// <param name="factory"></param>
        public SFTPBatchProcessor( IDispatcherFactory factory )
        {
            this.factory = factory;
        }

        #region IBatchProcessor Members

        /// <summary>
        /// Creates a Response object that contains the fields of the
        /// NetConnect error response file referenced by the given
        /// filename.
        /// </summary>
        /// <remarks>
        /// The file referenced by the FileInfo object is a PGP encrypted
        /// error response file that contains fields that describe the
        /// nature of the error.
        ///
        /// A typical file looks like this:
        ///
        /// The error response file looks something like this:
        ///
        /// Batch Sequence Number: [16601]
        /// User ID: [SVQ4HDW5]
        /// File ID: [945903.071112_120348.txt.pgp]
        /// File Direction: [Merchant to Stratus]
        /// ----------------------------------------
        /// 6826: Incorrect compression type for this user
        ///
        /// This method takes each line that contains the delimiter (":")
        /// and splits into a name/value pair. It then removes the brackets from the
        /// value and puts the pair into a map. Lines that do not contain the
        /// delimiter are skipped.
        ///
        /// The map is then passed to the converter, which gives me XML that is
        /// then plugged into a new Response object to return to the client.
        /// </remarks>
        /// <param name="manager">The ProtocolManager object that contains the keys necessary to decrypt the file.</param>
        /// <param name="pgpFile">The absolute filename of the error file. </param>
        /// <param name="sftpFileName">The name of the file on the SFTP server.</param>
        /// <returns>A Response object giving easy access to the error file's fields.</returns>
        public IResponse CreateNetConnectError( ConfigurationData configData, string pgpFile, string sftpFileName )
        {
            if ( configData.Protocol != CommModule.SFTPBatch )
            {
                var message = "SFTPBatchProcessor.CreateNetConnectError() implemented only for SFTPBatch";
                Logger.Error( message );
                throw new DispatcherException( Error.ArgumentMismatch, message );
            }

            TestForNull( configData, Error.NullConfigurationData, "ConfigurationData" );
            TestForNull( pgpFile, Error.NullFileName, "file name" );
            TestForNull( configData[ "PGPMerchantPrivateKey" ], Error.PrivateKeyNotSet, "PGP Merchant Private Key" );

            var fileConverter = factory.MakePGPFileConverter( configData );
            string data = null;
            try
            {
                data = fileConverter.ConvertErrorFrom( pgpFile );
            }
            catch ( Exception ex )
            {
                Logger.Debug( "Caught exception while trying to convert the error file. ", ex );
                return null;
            }

            if ( data == null )
            {
                return null;
            }

            Logger.Debug( "Detected a NetConnect error file." );

            if ( sftpFileName != null )
            {
                string path = null;
                try
                {
                    path = $"{new FileName(pgpFile).Path}\\{sftpFileName}";
                    FileMgr.Move( pgpFile, path );
                    pgpFile = path;
                }
                catch ( Exception ex )
                {
                    var msg = $"Failed to rename file \"{pgpFile}\" to \"{path}\".";
                    Logger.Warn( msg, ex );
                }
            }

            var converter = factory.MakeBatchConverter( configData );
            IResponse response = null;
            try
            {
                response = converter.ConvertSFTPErrorResponse( data, sftpFileName );
            }
            catch ( ConverterException )
            {
                return null;
            }

            return response;
        }

        /// <summary>
        /// Converts an existing PGP response file into an AES response file
        /// and returns a ResponseDescriptor to allow access to the AES file.
        /// </summary>
        /// <param name="responseFile">The path to the PGP file that must be converted.</param>
        /// <param name="aesFile">The path to the AES file that the response file will be converted to.</param>
        /// <param name="manager">The ProtocolManager object that contains the keys necessary to convert the file.</param>
        /// <returns>An IResponseDescriptor that provides access to the new AES encrypted file.</returns>
        public IResponseDescriptor CreateResponseDescriptor( string responseFile, string aesFile,
            ConfigurationData configData )
        {
            if ( configData.Protocol != CommModule.SFTPBatch )
            {
                var message = "SFTPBatchProcessor.CreateResponseDescriptor() implemented only for SFTPBatch";
                Logger.Error( message );
                throw new DispatcherException( Error.InvalidModule, message );
            }

            var converter = factory.MakePGPFileConverter( configData );
            try
            {
                converter.ConvertFrom( responseFile, aesFile );
            }
            catch ( FilerException ex )
            {
                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( DispatcherException )
            {
                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                throw;
            }
            catch ( Exception ex )
            {
                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                var message = "Exception occurred while converting from PGP to AES.";
                Logger.Error( message, ex );
                throw new DispatcherException( Error.UnknownError, message, ex );
            }

            try
            {
                return factory.MakeResponseDescriptor( aesFile, configData[ "SubmissionFilePassword" ], configData, null );
            }
            catch ( ResponseException ex )
            {
                throw new DispatcherException( ex );
            }
        }

        /// <summary>
        /// Deletes the specified file from the Stratus.
        /// </summary>
        /// <remarks>
        /// The file specified does not
        /// represent a file on the client's computer, but instead refers to a file
        /// on server. This method will properly delete the file
        /// from all servers (including backup servers).
        ///
        /// The method returns true if the file was successfully deleted from all
        /// servers. If it failed to delete it from even just one server, the method
        /// will return false. You can call this method as many times as it takes to
        /// delete all of the files. However, you may want to wait or call support
        /// if it fails to delete them all the first time.
        /// </remarks>
        /// <param name="filename">The name of the file to delete from the server.</param>
        /// <param name="manager">An object containing the security data required for the operation.</param>
        /// <returns>True if the file was successfully deleted, false if not.</returns>
        public bool DeleteServerFile( string filename, ConfigurationData configData )
        {
            var comm = factory.BatchCommManager;
            try
            {
                var args = new CommArgs( filename, configData );
                return comm.CompleteTransaction( args ) != null;
            }
            catch ( CommException ex )
            {
                Logger.Error( ex.Message, ex );
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
        }

        /// <summary>
        /// Sends the submission to the  server.
        /// </summary>
        /// <remarks>
        /// <p>The submission will get converted into the proper payload and
        /// then transmitted to the server using the specified protocol.</p>
        /// </remarks>
        /// <param name="submission">A descriptor that refers to the submission to be sent.</param>
        /// <param name="pgpFileName">The absolute path to the PGP file that will be created before sending.</param>
        public void SendSubmission( ISubmissionDescriptor submission, string pgpFileName )
        {
            SendSubmission( submission, submission.Config, pgpFileName );
        }

        /// <summary>
        /// Sends the submission to the  server.
        /// </summary>
        /// <remarks>
        /// <p>The submission has already been converted to PGP format,
        /// so it is just transmitted to the server using the specified protocol.</p>
        /// </remarks>
        /// <param name="configData">The ProtocolManager object that contains the
        /// keys necessary to send the file.</param>
        /// <param name="pgpFileName">The absolute path to the PGP file that will be created before sending.</param>
        public void SendSubmission( ConfigurationData configData, string pgpFileName )
        {
            SendSubmission( null, configData, pgpFileName );
        }

        /// <summary>
        /// Sends the submission to the  server.
        /// </summary>
        /// <remarks>
        /// <p>The submission will get converted into the proper payload and
        /// then transmitted to the server using the specified protocol.</p>
        /// </remarks>
        /// <param name="submission">A descriptor that refers to the submission to be sent.</param>
        /// <param name="manager">The ProtocolManager object that contains the
        /// keys necessary to send the file.</param>
        /// <param name="pgpFileName">The absolute path to the PGP file that will be created before sending.</param>
        private void SendSubmission( ISubmissionDescriptor submission, ConfigurationData configData, string pgpFileName )
        {
            TestForNull( configData, Error.NullConfigurationData, "ConfigurationData" );
            TestForNull( pgpFileName, Error.NullFileName, "PGP file name" );
            TestForNull( configData[ "RSAMerchantPrivateKey" ], Error.PrivateKeyNotSet, "RSA Merchant Private Key" );
            TestForNull( configData[ "PGPServerPublicKey" ], Error.PaymentechKeyNotSet, "PGP Server Public Key" );

            configData[ "RSAMerchantPrivateKey" ] = FileMgr.FindFilePath( configData[ "RSAMerchantPrivateKey" ], "config" );
            if ( configData[ "RSAMerchantPrivateKey" ] == null )
            {
                var message = "The RSAMerchantPrivateKey could not be found.";
                Logger.Error( message );
                throw new DispatcherException( Error.FileNotFound, message );
            }

            configData[ "PGPServerPublicKey" ] = FileMgr.FindFilePath( configData[ "PGPServerPublicKey" ], "config" );
            if ( configData[ "PGPServerPublicKey" ] == null )
            {
                var message = "The PGPServerPublicKey could not be found.";
                Logger.Error( message );
                throw new DispatcherException( Error.FileNotFound, message );
            }

            if ( submission != null )
            {
                TestForNull( configData[ "PGPServerPublicKey" ], Error.PaymentechKeyNotSet, "PGPServerPublicKey" );
                pgpFileName = CreatePGPSubmissionFile( submission, pgpFileName );
            }

            var reader = factory.MakeTextFileReader( pgpFileName );
            if ( submission == null )
            {
                reader.IsSFTPFileNameSet = true;
            }
            var comm = factory.BatchCommManager;
            reader.Close();

            try
            {
                var args = new CommArgs( reader, configData );
                comm.CompleteTransaction( args );
                if ( submission != null )
                {
                    Logger.InfoFormat( "Submission Filename={0} sent successfully as {1}", submission.FileName, reader.SFTPFilename );
                }
                else
                {
                    Logger.InfoFormat( "Submission Filename={0} sent successfully as {1}", reader.FileName, reader.SFTPFilename );
                }

                if ( submission != null )
                {
                    submission.SFTPFileName = reader.SFTPFilename;
                }
            }
            catch ( CommException ex )
            {
                Logger.Error( ex.Message, ex );
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            finally
            {
                if ( submission != null && configData.GetBool( "DeletePGPFile", true ) )
                {
                    reader.DeleteFile();
                }
            }
        }

        /// <summary>
        /// Downloads a PGP-encrypted response file from the server and
        /// returns the file path to it.
        /// </summary>
        /// <remarks>
        /// You cannot iterate through the response file downloaded using this
        /// method. You must first convert it to a proper AES-encrypted file
        /// and get an IResponseDescriptor object for it.
        ///
        /// This method is optional and is only used if you want to split the
        /// two time-consuming tasks of downloading and converting to AES into
        /// two separate tasks.
        /// </remarks>
        /// <param name="responseType">The type of response you wish to download.</param>
        /// <param name="manager">The ProtocolManager object that contains the keys
        /// necessary to download the PGP file.</param>
        /// <param name="pgpFile">The PGP file that will be downloaded from the server.</param>
        /// <param name="sftpFilename">The name of the file as it exists on the server.</param>
        /// <returns></returns>
        public string ReceivePGPResponse(
            string responseType,
            ConfigurationData configData,
            string pgpFile,
            out string sftpFilename )
        {
            TestForNull( configData, Error.NullConfigurationData, "ConfigurationData" );
            TestForNull( pgpFile, Error.NullFileName, "PGP filename" );
            TestForNull( configData[ "RSAMerchantPrivateKey" ], Error.PrivateKeyNotSet, "RSAMerchantPrivateKey" );

            sftpFilename = null;

            try
            {
                var args = new ConverterArgs();
                args.CommModule = configData.Protocol;
                args.StrData = responseType;
                args = factory.MakeBatchConverter( configData ).GetResponseRecordInfo( args );
            }
            catch ( Exception ex )
            {
                var msg = $"The Response Type \"{responseType}\" is not valid for this operation.";
                Logger.Error( msg, ex );
                throw new DispatcherException( Error.InvalidResponseType, msg, ex );
            }

            var writer = factory.MakeTextFileWriter( pgpFile, responseType );

            if ( configData[ "DebugReceiveFile" ] == null )
            {
                var comm = factory.BatchCommManager;
                try
                {
                    var args = new CommArgs( writer, null, configData );
                    if ( comm.CompleteTransaction( args ) == null )
                    {
                        writer.Close();
                        DeleteFile( pgpFile );
                        return null;
                    }
                }
                catch ( CommException ex )
                {
                    writer.Close();
                    DeleteFile( pgpFile );
                    throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
                }
            }
            else
            {
                writer.Close();
                FileMgr.Delete( pgpFile );
                FileMgr.Copy( configData[ "DebugReceiveFile" ], pgpFile );
                writer.SFTPFilename = pgpFile;
            }

            writer.Close();
            sftpFilename = writer.SFTPFilename;

            var fname = new FileName( pgpFile );
            fname.NameAndExtension = sftpFilename;
            var sftpFile = fname.ToString();

            if ( FileMgr.Exists( sftpFile ) )
            {
                return pgpFile;
            }

            try
            {
                FileMgr.Move( pgpFile, sftpFile );
                return sftpFile;
            }
            catch ( Exception ex )
            {
                var msg = $"Failed to rename {pgpFile} to {sftpFile}.";
                Logger.Warn( msg, ex );
            }

            return pgpFile;
        }

        /// <summary>
        /// Calls ICommManager's ReceiveFile method to download the PGP response
        /// file, and then converts it into an AES response file.
        /// </summary>
        /// <remarks>
        /// The CommManager will not use the FileWriter that ReceiveResponse
        /// creates to write to the file. Instead, it just uses it to get
        /// the filename, ProtocolManager, and ResponseType. So, this method
        /// must not call IFileWriter.CreateFile and does not need to call
        /// Close. If the file is created, then the CommManager would not
        /// be able to write to the file because the writer will already
        /// have it locked.
        /// </remarks>
        /// <param name="responseType">The type of response to being downloaded.</param>
        /// <param name="configData">
        /// The ProtocolManager that defines the SSH and PGP security
        /// parameters.
        /// </param>
        /// <param name="pgpFile">The path to the PGP file that the response
        /// file is downloaded to.
        /// </param>
        /// <param name="aesFile">The path to the AES file that the PGP file
        /// will be converted into. This will be the actual response file that
        /// is wrapped by the ResponseDescriptor.
        /// </param>
        /// <returns>
        /// A ResponseDescriptor that wraps the new AES-encrypted response
        /// file.
        /// .</returns>
        public IResponseDescriptor ReceiveResponse( string responseType, ConfigurationData configData,
            string pgpFile, string aesFile )
        {
            string sftpFilename;
            var filename = ReceivePGPResponse( responseType, configData, pgpFile, out sftpFilename );
            if ( filename == null )
            {
                return null;
            }

            var errorResponse = CreateNetConnectError( configData, filename, sftpFilename );
            if ( errorResponse != null )
            {
                Logger.Debug( "File is a NetConnect error file." );
                var response = factory.MakeResponseDescriptor( errorResponse, sftpFilename, configData );
                var deleteSuccessful = AutoDeleteServerFile( sftpFilename, configData );
                var impl = (IFullResponse) errorResponse;
                var elem = new DataElement( "DeleteSFTPFile", deleteSuccessful.ToString(), impl );
                var list = new List<DataElement>( impl.DataElements );
                list.Add( elem );
                impl.DataElements = list;
                return response;
            }

            Logger.Debug( "File is not a NetConnect error file." );

            var converter = factory.MakePGPFileConverter( configData );
            try
            {
                converter.ConvertFrom( filename, aesFile );
            }
            catch ( FilerException ex )
            {
                try
                {
                    FileMgr.Delete( filename );
                }
                catch { }

                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                Logger.Error( ex.Message, ex );
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( Exception ex )
            {
                try
                {
                    FileMgr.Delete( filename );
                }
                catch { }

                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                Logger.Error( ex.Message, ex );
                throw new DispatcherException( Error.UnknownError, ex.Message, ex );
            }

            DeleteFile( filename );

            try
            {
                var pword = configData[ "SubmissionFilePassword" ];
                var finalName = RenameResponseFile( responseType, aesFile, pword, configData );
                var deleteSuccessful = AutoDeleteServerFile( sftpFilename, configData );
                return factory.MakeResponseDescriptor( finalName, pword, configData, sftpFilename );
            }
            catch ( ResponseException ex )
            {
                Logger.Error( ex.Message, ex );
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( DispatcherException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new DispatcherException( Error.UnknownError, ex.Message, ex );
            }
        }

        private bool AutoDeleteServerFile( string sftpFilename, ConfigurationData configData )
        {
            if ( configData.GetBool( "DeleteSFTPFile", false ) && configData[ "DebugReceiveFile" ] == null )
            {
                return DeleteServerFile( sftpFilename, configData );
            }
            return false;
        }

        /// <summary>
        /// Not supported for SFTP.
        /// </summary>
        /// <param name="request"></param>
        /// <param name="password"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public IResponseDescriptor ReceiveResponse( IRequest request, string password, string file )
        {
            var message = "ReceiveResponse(IRequest, string, string) is not supported.";
            Logger.Error( message );
            throw new DispatcherException( Error.NotSupported, message );
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
        /// <param name="pgpFileName">The absolute path to the PGP file that is to be created.</param>
        /// <returns>The absolute path to the generated PGP file.</returns>
        public string CreatePGPSubmissionFile( ISubmissionDescriptor submission, string pgpFileName )
        {
            var filename = submission.FileName;
            var password = submission.Password;
            var subName = submission.Name;

            var reader = factory.MakeFileReader( filename, password, subName );
            reader.TotalRecords = submission.TotalRecords;
            try
            {
                var converter = factory.MakePGPFileConverter( submission.Config );
                converter.ConvertTo( reader, pgpFileName );
            }
            catch ( FilerException ex )
            {
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            finally
            {
                reader.Close();
            }

            return pgpFileName;
        }

        #endregion

        #region Private Methods

        /// <summary>
        /// Deletes the specified file from the Stratus.
        /// </summary>
        /// <remarks>
        ///  The file specified does not
        /// represent a file on the client's computer, but instead refers to a file
        /// on server. This method will properly delete the file
        /// from all servers (including backup servers).
        ///
        /// The method returns true if the file was successfully deleted from all
        /// servers. If it failed to delete it from even just one server, the method
        /// will return false. You can call this method as many times as it takes to
        /// delete all of the files. However, you may want to wait or call support
        /// if it fails to delete them all the first time.
        /// </remarks>
        /// <param name="filename">The name of the file to delete from the server.</param>
        /// <returns>True if the file was successfully deleted, false if not.</returns>
        private void DeleteFile( string filename )
        {
            if ( !FileMgr.Delete( filename ) )
            {
                Logger.WarnFormat( "The file \"{0}\" failed to delete.", filename );
            }
        }
        #endregion
    }
}
