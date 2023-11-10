using System;
using JPMC.MSDK.Comm;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Filer;

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Summary description for TCPBatchProcessor.
    /// </summary>
    public class TCPBatchProcessor : DispatcherBase, ITCPBatchProcessor
    {
        /// <summary>
        /// Constructs a new object and sets the factory to be used.
        /// </summary>
        /// <param name="factory"></param>
        public TCPBatchProcessor( IDispatcherFactory factory )
        {
            this.factory = factory;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="responseFile"></param>
        /// <param name="aesFile"></param>
        /// <param name="configData"></param>
        /// <returns></returns>
        public IResponseDescriptor CreateResponseDescriptor( string responseFile, string aesFile,
            ConfigurationData configData )
        {
            string finalName = null;

            if ( configData == null )
            {
                var message = "TCPBatchProcessor.CreateResponseDescriptor() ConfigurationData Required";
                Logger.Error( message );
                throw new DispatcherException( Error.NullConfigurationData, message );
            }

            if ( configData.Protocol != CommModule.TCPBatch )
            {
                var message = "TCPBatchProcessor.CreateResponseDescriptor() implemented only for TCPBatch";
                Logger.Error( message );
                throw new DispatcherException( Error.ArgumentMismatch, message );
            }

            // First, decrypt the PGP file.
            configData.Protocol = CommModule.SFTPBatch;
            var fileConverter = factory.MakePGPFileConverter( new ConfigurationData( configData ) );
            configData.Protocol = CommModule.TCPBatch;

            try
            {
                Logger.Debug( "Decrypting the response file \"" + responseFile + "\"." );

                fileConverter.ConvertFrom( responseFile, aesFile );

                var fileType = fileConverter.FileType;
                finalName = RenameResponseFile( fileType, aesFile, configData[ "SubmissionFilePassword" ], configData );

                if ( finalName == null )
                {
                    var message = "Could not rename file: " + aesFile;
                    Logger.Error( message );
                    throw new DispatcherException( Error.FileCreationFailure, message );
                }

            }
            catch ( DispatcherException )
            {
                try
                {
                    FileMgr.Delete( responseFile );
                }
                catch { }

                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                throw;
            }
            catch ( FilerException ex )
            {
                try
                {
                    FileMgr.Delete( responseFile );
                }
                catch { }

                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                throw new DispatcherException(
                    Error.DecryptionFailed,
                    "Could not convert PGP file to AES file", ex );
            }
            catch ( Exception ex )
            {
                try
                {
                    FileMgr.Delete( responseFile );
                }
                catch { }

                try
                {
                    FileMgr.Delete( aesFile );
                }
                catch { }

                Logger.Error( "Could not convert PGP file to AES file", ex );
                throw new DispatcherException( Error.DecryptionFailed, "Could not convert PGP file to AES file", ex );
            }

            Logger.Debug( "The encrypted file is \"" + finalName + "\"." );

            return factory.MakeResponseDescriptor( finalName, configData[ "SubmissionFilePassword" ], configData );
        }


        /// <summary>
        /// Sends the submission to the payment processing server. 
        /// </summary>
        /// <remarks>
        /// <p>The submission will get converted into the proper payload and
        /// then transmitted to the server using the specified protocol.</p>
        /// </remarks>
        /// <param name="submission">A descriptor that refers to the submission to be sent.</param>
        /// <param name="pgpFileName">Not used. Its value is always ignored.</param>
        public void SendSubmission( ISubmissionDescriptor submission, string pgpFileName )
        {
            var reader = factory.MakeFileReader( submission.FileName, submission.Password, submission.Name );
            var comm = factory.BatchCommManager;
            var converter = factory.MakeBatchConverter( submission.Config );
            reader.FileTerminator = converter.RequestFileTerminator;
            try
            {
                var args = new CommArgs( reader, submission.Config );
                comm.CompleteTransaction( args );
                Logger.InfoFormat( "Submission sent successfully, Filename={0}", submission.FileName );
            }
            catch ( CommException ex )
            {
                throw new DispatcherException( ex );
            }
            finally
            {
                reader.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="request"></param>
        /// <param name="password"></param>
        /// <param name="file"></param>
        /// <returns></returns>
        public IResponseDescriptor ReceiveResponse( IRequest request, string password, string file )
        {
            var converter = factory.MakeBatchConverter( request.Config );
            var rfrArgs = converter.ConvertRequest( factory.RequestToImpl( request ) );
            IResponseDescriptor retVal = null;
            var configData = request.Config;

            var fileHeader = new FileHeader();
            fileHeader.BatchClosed = false;
            fileHeader.RecordDelimiter = rfrArgs.RecordDelimiter;
            fileHeader.FileType = null;
            fileHeader.FormatName = rfrArgs.Format;
            fileHeader.FieldDelimiter = rfrArgs.FieldDelimiter;
            fileHeader.LineLength = converter.BatchRecordLength;

            var writer = factory.MakeFileWriter( request.Config );
            writer.CreateFile( file, password, fileHeader );

            // The file terminator must be added to the request payload before sending.
            var payload = string.Concat( rfrArgs.Request, rfrArgs.FileTerminator );

            try
            {
                var args = new CommArgs( writer, Utils.StringToByteArray( payload ), request.Config );
                factory.BatchCommManager.CompleteTransaction( args );

                if ( FileMgr.Length( writer.FileName ) == 0 )
                {
                    writer.Close();
                    Logger.Debug( "No file is available for download." );
                    FileMgr.Delete( file );
                    return null;
                }
            }
            catch ( CommException ex )
            {
                writer.Close();
                FileMgr.Delete( file );
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( FilerException ex )
            {
                writer.Close();
                FileMgr.Delete( file );
                throw new DispatcherException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( Exception ex )
            {
                writer.Close();
                FileMgr.Delete( file );
                Logger.Error( ex.Message, ex );
                throw new DispatcherException( Error.UnknownError, ex.Message, ex );
            }

            writer.Close();

            string tmpName = null;

            try
            {
                if ( writer.ResponseType.Equals( "TCPPGP" ) )
                {
                    if ( password != null )
                    {
                        configData[ "SubmissionFilePassword" ] = password;
                    }

                    retVal = CreateResponseDescriptor( writer.FileName, file, configData );
                }
                else
                {
                    var typeName = writer.ResponseType;

                    tmpName = RenameResponseFile( typeName, file, password, request.Config );

                    if ( tmpName == null )
                    {
                        var msg = "Could not rename file: " + file;
                        Logger.Error( msg );
                        throw new DispatcherException( Error.FileCreationFailure, msg );
                    }

                    retVal = factory.MakeResponseDescriptor( tmpName, password, request.Config );
                }
            }
            finally
            {
                if ( writer != null )
                {
                    FileMgr.Delete( writer.FileName );
                }
            }

            return retVal;
        }

    }
}
