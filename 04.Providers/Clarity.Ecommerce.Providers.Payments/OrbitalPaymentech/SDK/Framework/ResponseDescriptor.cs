#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.IO;
using System.Text;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Filer;

namespace JPMC.MSDK.Framework
{
    /// <summary>
    /// Represents the contents of the response file returned from the
    /// server for a batch submission.
    /// </summary>
    /// <remarks>
    /// This class is only used for batch processing.
    ///
    /// The entire contents of the file are not stored
    /// in memory in the class, but are read as needed from the file.
    /// </remarks>
    public class ResponseDescriptor : DispatcherBase, IResponseDescriptorImpl
    {
        private string password;
        private IFiler filer;
        private long currentPosition;
        public ConfigurationData ConfigData { set; get; }
        private IResponse header;
        private IResponse trailer;
        private IResponse totals;
        private IResponse batchTotals;
        private IBatchConverter converter;

        /// <exclude />
        /// <summary>
        ///
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="commMode"></param>
        public ResponseDescriptor( string filename, string password, ConfigurationData configData )
            : this( filename, password, configData, null, new DispatcherFactory() )
        {
        }

        /// <exclude />
        /// <summary>
        /// The main constructor of the class. Is responsible for initializing the Filer.
        /// </summary>
        /// <remarks>
        /// This constructor verifies that the filename supplied is valid, and then tells
        /// the filer to open the file.
        /// </remarks>
        /// <param name="filename">The filename to read. This can be either a relative path or an absolute path.</param>
        /// <param name="password">The password for decrypting the file.</param>
        /// <param name="commMode">The communication module to use.</param>
        /// <param name="sftpFilename">The name of the file on the server.</param>
        /// <param name="factory">The factory class used to create the external resource objects.</param>
        public ResponseDescriptor( string filename, string password, ConfigurationData configData, string sftpFilename, IDispatcherFactory factory )
        {
            this.factory = factory;
            this.FileName = filename;
            this.password = password;
            this.ConfigData = configData;
            this.filer = factory.MakeAESFiler();
            this.SFTPFilename = sftpFilename;

            TestForNull( filename, Error.NullFileName, "filename" );
            TestForNull( password, Error.NullPassword, "password" );

            if ( FileName.IndexOf( "\\" ) == -1 )
            {
                FileName = $"{factory.HomeDirectory}\\incoming\\{filename}";

                if ( !FileMgr.Exists( FileName ) )
                {
                    FileName = $"{factory.HomeDirectory}\\outgoing\\{filename}";
                }
            }


            if ( !FileMgr.Exists( FileName ) )
            {
                var msg = $"The file \"{FileName}\" does not exist";
                Logger.Error( msg );
                throw new ResponseException( Error.FileNotFound, msg );
            }

            try
            {
                filer.OpenFile( FileName );
                currentPosition = filer.Position;


                // Verify that we can read an order.
                // If it fails, then the password is probably wrong.
                if ( filer.FileHeader.FormatName == "Response" )
                {
                    if ( this.GetNext() == null )
                    {
                        var resp = this.Header;
                    }
                }
                else
                {
                    var rec = this.GetNextRawRecord();
                }
                this.ResetReader();
            }
            catch ( FilerException ex )
            {
                try { filer.CloseFile(); }
                catch { }
                throw new ResponseException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( ResponseException )
            {
                try { filer.CloseFile(); }
                catch { }
                throw;
            }
        }

        public ResponseDescriptor( IResponse errorResponse, ConfigurationData configData, string sftpFilename, IDispatcherFactory factory )
        {
            this.ErrorResponse = errorResponse;
            this.ConfigData = configData;
            this.SFTPFilename = sftpFilename;
            this.factory = factory;
        }

        #region IResponseDescriptor Members

        /// <summary>
        /// Gets the path to the batch file.
        /// </summary>
        public string FileName { get; private set; }

        /// <summary>
        /// Gets the name of the submission from the header.
        /// </summary>
        public string Name
        {
            get
            {
                try
                {
                    return Header[ "FileName" ];
                }
                catch
                {
                    return null;
                }
            }
        }

        /// <summary>
        /// The type of response file this descriptor refers to.
        /// </summary>
        public string ResponseFileType => this.filer.FileHeader.FileType;

        /// <summary>
        /// Gets the header record from the file and returns it as a Response object.
        /// </summary>
        public IResponse Header
        {
            get
            {
                TestIfSupported();

                if ( header != null )
                {
                    return header;
                }

                TestForFileOpen();

                var currentPos = filer.Position;
                filer.Seek( filer.StartPosition, SeekOrigin.Begin );
                try
                {
                    this.header = GetNext( false );
                    return header;
                }
                catch ( ResponseException )
                {
                    throw;
                }
                catch ( Exception ex )
                {
                    Logger.Error( "Failed to retrieve Header", ex );
                    throw new ResponseException( Error.UnknownError, "Failed to retrieve Header", ex );
                }
                finally
                {
                    try
                    {
                        filer.Seek( currentPos, SeekOrigin.Begin );
                    }
                    catch ( FilerException ex )
                    {
                        throw new ResponseException( ex.ErrorCode, ex.Message, ex );
                    }
                    catch ( Exception ex )
                    {
                        Logger.Error( "Failed to retrieve Header", ex );
                        throw new ResponseException( Error.UnknownError, "Failed to retrieve Header", ex );
                    }
                }
            }
        }

        /// <summary>
        /// Gets the trailer record from the file and returns it as a Response object.
        /// </summary>
        public IResponse Trailer
        {
            get
            {
                if ( trailer != null )
                {
                    return trailer;
                }

                TestForFileOpen();
                TestIfSupported();

                trailer = this.GetOrderFromEnd( 1, true );
                return trailer;
            }
        }

        /// <summary>
        /// Gets the total record from the file and returns it as a Response object.
        /// </summary>
        public IResponse Totals
        {
            get
            {
                if ( totals != null )
                {
                    return totals;
                }

                TestForFileOpen();
                TestIfSupported();
                totals = this.GetOrderFromEnd( 2, true );
                return totals;
            }
        }

        /// <summary>
        /// Gets the total record for the batch from the file and returns it as a Response object.
        /// </summary>
        public IResponse BatchTotals
        {
            get
            {
                if ( batchTotals != null )
                {
                    return batchTotals;
                }

                TestForFileOpen();
                batchTotals = this.GetOrderFromEnd( 3, true );
                return batchTotals;
            }
        }

        /// <summary>
        /// Resets the reader back to the beginning of the file.
        /// </summary>
        public void ResetReader()
        {
            try
            {
                TestForFileOpen();
                filer.Seek( filer.StartPosition, SeekOrigin.Begin );
            }
            catch ( FilerException ex )
            {
                Logger.Error( "Failed to ResetReader", ex );
                throw new ResponseException( ex.ErrorCode, "Failed to ResetReader", ex );
            }
        }

        /// <summary>
        /// Returns true if there are more orders in the file before the
        /// current position, false if there are none.
        /// </summary>
        public bool HasPreviousOrder
        {
            get
            {
                TestForFileOpen();
                TestIfSupported();
                if ( filer.Position > filer.EncryptedRecordLength * 4 )
                    return true;
                return false;
            }
        }

        /// <summary>
        /// Gets the NetConnect error response data, if one exists.
        /// </summary>
        /// <remarks>Returns null if no error exists.</remarks>
        public IResponse ErrorResponse { get; private set; }

        public bool HasNext
        {
            get
            {
                if ( ErrorResponse != null )
                {
                    return false;
                }

                if ( filer.FileHeader.FileType == "Batch" ||
                     filer.FileHeader.FileType == ResponseType.Response )
                {
                    return HasNextOrder;
                }

                try
                {
                    return HasNextRecord;
                }
                catch ( ResponseException )
                {
                    return false;
                }
            }
        }


        /// <summary>
        /// Returns true if there are more orders in the file to read, and false if there are no more.
        /// </summary>
        public bool HasNextOrder
        {
            get
            {
                if ( filer == null )
                {
                    return false;
                }
                TestForFileOpen();
                TestIfSupported();

                try
                {
                    var end = filer.Length - filer.Position;
                    if ( filer.Position == filer.StartPosition )
                    {
                        end -= filer.EncryptedRecordLength * 2;
                    }
                    if ( filer.FileHeader.BatchClosed )
                    {
                        end -= filer.EncryptedRecordLength * 6;
                    }

                    if ( end >= filer.EncryptedRecordLength )
                    {
                        return true;
                    }
                }
                catch ( Exception ex )
                {
                    Logger.Error( "HasNextOrder failed", ex );
                    throw new ResponseException( Error.UnknownError, "HasNextOrder failed", ex );
                }

                return false;
            }
        }

        /// <summary>
        /// Returns the last order before the current position in
        /// the file.
        /// </summary>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        public IResponse GetPreviousOrder()
        {
            return GetPreviousOrder( true );
        }

        /// <summary>
        /// Returns the last order in the file.
        /// </summary>
        /// <remarks>
        /// This method simply seeks to the end of the file and then
        /// calls GetPreviousOrder.
        /// </remarks>
        /// <returns>A response file that represents the last order in the file.</returns>
        public IResponse GetLastOrder()
        {
            TestForFileOpen();
            filer.Seek( filer.EndOfFilePosition, SeekOrigin.Begin );
            return GetPreviousOrder();
        }

        /// <summary>
        /// Returns the next order from the file sequentially.
        /// </summary>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        [Obsolete( "No longer supported. Call GetNext() from now on.", false )]
        public IResponse GetNextOrder()
        {
            //TestForFileOpen();
            return GetNext( true );
        }

        /// <summary>
        /// Returns the next order from the file sequentially.
        /// </summary>
        /// <returns></returns>
        public IResponse GetNext()
        {
            var header = filer.FileHeader;
            var fileType = header.FileType;
            if ( fileType == "Batch" || fileType == ResponseType.Response )
            {
                return GetNext( true );
            }

            return GetNextParsedRecord();
        }


        /// <summary>
        /// Returns the next order from the file sequentially as a Request
        /// object. This is to make it easy to recover a corrupted
        /// Submission file.
        /// </summary>
        /// <remarks>
        /// Use this in conjunction with <see cref="JPMC.MSDK.Dispatcher.OpenDescriptor"/>
        /// to recover a corrupted Submission file. This will get the next order from the file, but
        /// will return it as an IRequest object that you can then add to a new
        /// <see cref="JPMC.MSDK.ISubmissionDescriptor"/>.
        /// </remarks>
        /// <returns>An IResponse object that provides easy access to the order record.</returns>
        public IRequest GetNextRequest()
        {
            TestForFileOpen();
            if ( ResponseFileType != "Batch" )
            {
                var msg = "GetNextRequest is only allowed on Batch Submission files.";
                Logger.Error( msg );
                throw new ResponseException( Error.InvalidOperation, msg );
            }

            var response = GetNext();
            if ( response == null )
            {
                return null;
            }

            try
            {
                return factory.MakeRequest( response.XML, response.RawData, ConfigData );
            }
            catch ( DispatcherException ex )
            {
                throw new ResponseException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( Exception ex )
            {
                Logger.Error( "GetNextRequest failed to create a new request", ex );
                throw new ResponseException( Error.UnknownError, "GetNextRequest failed to create a new request", ex );
            }
        }

        /// <summary>
        /// Gets a line of text from a variable length file, parses its fields
        /// into a Response object and returns it to the user.
        /// </summary>
        /// <remarks>
        /// Use <see cref="JPMC.MSDK.IResponseDescriptor.HasNextRecord">
        /// HasNextRecord</see> to see if there is another record in the file.
        /// <br/>
        /// You can use <see cref="JPMC.MSDK.IResponseDescriptor.GetNextRecord">
        /// GetNextRecord</see> to get an unparsed version of the record.
        /// </remarks>
        /// <returns>A Response object that gives easy access to the record's fields.</returns>
        public IResponse GetNextParsedRecord()
        {
            string record;
            return GetNextParsedRecord( true, out record );
        }

        public IResponse GetNextParsedRecord( bool throwOnInvalidType, out string rawRecord )
        {
            TestForFileOpen();

            var header = filer.FileHeader;
            if ( header.FileType == "Batch" || header.FileType == ResponseType.Response )
            {
                if ( !throwOnInvalidType )
                {
                    rawRecord = null;
                    return null;
                }
                var msg = "GetNextParsedRecord is not supported for this file type.";
                Logger.Error( msg );
                throw new ResponseException( Error.NotSupported, msg );
            }

            rawRecord = GetNextRawRecord();
            if ( rawRecord == null )
            {
                return null;
            }

            if ( converter == null )
            {
                converter = factory.MakeBatchConverter( ConfigData );
                var info = converter.GetResponseRecordInfo( rawRecord, null, ConfigData.Protocol );
            }

            var args = converter.ConvertResponse( rawRecord, false );

            LogResponseAndPayload( args );

            var response = args.ResponseData;
            return response;
        }


        /// <summary>
        /// Reads a single record from the response file.
        /// </summary>
        /// <remarks>
        /// The record returned is in the format that the
        /// server can read. This method is typically never used, and is
        /// only useful if you need to extract the actual payload of the
        /// batch exactly as it was processed on the server.
        /// </remarks>
        /// <returns>A string represent the current record.</returns>
        public string GetNextRecord()
        {
            // Calling GetNextParsedRecord ensures that any attempt
            // by the client to call GetNextParsedRecord after calling GetNextRecord
            // will properly convert the record.
            string record;
            var response = GetNextParsedRecord( false, out record );

            if ( record == null )
            {
                return GetNextRawRecord();
            }

            return record;
        }

        private string GetNextRawRecord()
        {
            try
            {
                byte[] data = null;
                do
                {
                    data = filer.ReadRecord();
                    if ( data == null || data.Length == 0 )
                    {
                        return null;
                    }
                    if ( this.filer.FileHeader.FormatName != "Response" && this.filer.FileHeader.FormatName != "" )
                    {
                        break;
                    }
                }
                while ( Utils.ByteArrayToString( data ).StartsWith( "Order" ) );

                var record = filer.Decrypt( data, this.password );
                return record;
            }
            catch ( FilerException ex )
            {
                throw new ResponseException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( Exception ex )
            {
                var msg = $"GetNextRecord failed: {ex.Message}";
                if ( ex.Message.ToLower().Contains( "padding" ) )
                {
                    msg = "The data could not be decrypted. Make sure your password is correct.";
                    Logger.Error( msg, ex );
                    throw new ResponseException( Error.InvalidPassword, msg, ex );
                }
                msg = "The data could not be decrypted. Unexpected error occurred.";
                Logger.Error( msg, ex );
                throw new ResponseException( Error.UnknownError, msg, ex );
            }
        }

        /// <summary>
        /// Returns true if there is another record that can be retrieved from
        /// the response file, false if there are none.
        /// </summary>
        /// <remarks>
        /// This is to be used in conjunction with <see cref="GetNextRecord"/>.
        /// These methods are typically never used. They are only made
        /// available for special cases.
        /// </remarks>
        public bool HasNextRecord
        {
            get
            {
                try
                {
                    if ( filer.FileHeader.LineLength == 0 )
                    {
                        return filer.Position < filer.EndOfFilePosition;
                    }

                    if ( filer.FileHeader.FileType == ResponseType.Response || filer.FileHeader.FileType == "Batch" )
                    {
                        return filer.EndOfFilePosition - filer.Position > filer.EncryptedRecordLength * 2;
                    }
                    return filer.EndOfFilePosition - filer.Position >= filer.EncryptedRecordLength;
                }
                catch ( Exception ex )
                {
                    var msg = $"GetNextRecord failed: {ex.Message}";
                    Logger.Error( msg, ex );
                    throw new ResponseException( Error.UnknownError, msg, ex );
                }
            }
        }

        /// <summary>
        /// Returns the name of the response file as it resides on the server.
        /// </summary>
        /// <remarks>
        /// The SFTP filename is used by the merchant when calling the
        /// Dispatcher's deleteServerFile method. The CommManager will set
        /// this field using setSFTPFilename, and the Dispatcher will wrap the
        /// call to this method for the merchant to use.
        /// </remarks>
        public string SFTPFilename { get; set; }

        /// <summary>
        /// Closes the response file.
        /// </summary>
        public void Close()
        {
            filer.CloseFile();
        }


        /// <summary>
        /// Returns the next order from the file sequentially.
        /// </summary>
        /// <returns></returns>
        private IResponse GetNext( bool skipSpecials )
        {
            TestForFileOpen();
            TestIfSupported();

            if ( filer.EOF )
            {
                Logger.Warn( "Premature end of the file." );
                return null;
            }

            // If there are no more orders, just return null.
            if ( skipSpecials && !HasNextOrder )
                return null;

            var order = new StringBuilder();
            OrderSeparator separator = null;
            try
            {
                do
                {
                    var encryptedRecord = filer.ReadRecord();
                    var sep = Utils.ByteArrayToString( encryptedRecord );
                    if ( sep.StartsWith( OrderSeparator.PREFIX ) )
                    {
                        separator = new OrderSeparator( sep );
                        if ( skipSpecials && separator.Type == "Sp" )
                        {
                            order = new StringBuilder();
                            if ( filer.EOF )
                                break;
                            continue;
                        }

                        break;
                    }
                    var decryptedRecord = filer.Decrypt( encryptedRecord, this.password );
                    order.Append( decryptedRecord );

                    if ( filer.EOF )
                        break;

                } while ( true );
            }
            catch ( FilerException ex )
            {
                var msg = "Decryption failed. Make sure you are using the correct password.";
                throw new ResponseException( ex.ErrorCode, msg, ex );
            }
            catch ( Exception ex )
            {
                if ( ex.Message.ToLower().Contains( "padding" ) )
                {
                    var msg = "The data could not be decrypted. Make sure your password is correct.";
                    Logger.Error( msg, ex );
                    throw new ResponseException( Error.InvalidPassword, msg, ex );
                }
                var message = "The data could not be decrypted. Unexpected error occurred.";
                Logger.Error( message, ex );
                throw new ResponseException( Error.UnknownError, message, ex );
            }

            currentPosition = filer.Position;

            try
            {
                if ( converter == null )
                {
                    converter = factory.MakeBatchConverter( ConfigData );
                }
                var orderString = order.ToString();
                var args = converter.ConvertResponse( orderString, false );

                LogResponseAndPayload( args );

                var response = args.ResponseData;
                if ( separator != null )
                {
                    factory.SetOrderResponse( response, separator.Type == "Or" );
                }
                if ( skipSpecials && separator != null && separator.Type == "Sp" )
                {
                    return null;
                }
                return response;
            }
            catch ( ConverterException ex )
            {
                throw new ResponseException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( DispatcherException ex )
            {
                throw new ResponseException( ex.ErrorCode, ex.Message, ex );
            }
            catch ( Exception ex )
            {
                if ( ex.Message.ToLower().Contains( "padding" ) )
                {
                    var msg = "The data could not be decrypted. Make sure your password is correct.";
                    Logger.Error( msg, ex );
                    throw new ResponseException( Error.InvalidPassword, msg, ex );
                }
                var message = "The data could not be decrypted. Unexpected error occurred.";
                Logger.Error( message, ex );
                throw new ResponseException( Error.UnknownError, message, ex );
            }
        }

        #endregion

        #region Private Members
        /// <summary>
        /// Returns the last order in the file, allowing you to return
        /// special records as orders.
        /// </summary>
        /// <remarks>
        /// This method simply seeks to the end of the file and then
        /// calls GetPreviousOrder.
        ///
        /// This overload of GetLastOrder will return the Trailer record
        /// as an order if skipSpecials is set to true.
        /// </remarks>
        /// <returns>A response file that represents the last order in the file.</returns>
        private IResponse GetLastOrder( bool skipSpecials )
        {
            TestForFileOpen();
            TestIfSupported();
            filer.Seek( filer.EndOfFilePosition, SeekOrigin.Begin );
            return GetPreviousOrder( skipSpecials );
        }

        /// <summary>
        /// Checks if the Filer is open and throws an exception if it is not.
        /// </summary>
        private void TestForFileOpen()
        {
            if ( filer == null || !filer.Open )
            {
                Logger.Error( "The descriptor is closed." );
                throw new ResponseException( Error.FileNotOpen, "The descriptor is closed." );
            }
        }

        /// <summary>
        /// Returns the last order before the current position in
        /// the file.
        /// </summary>
        /// <returns></returns>
        private IResponse GetPreviousOrder( bool skipSpecials )
        {
            TestForFileOpen();
            TestIfSupported();
            if ( !HasPreviousOrder )
            {
                return null;
            }

            var recordLength = filer.EncryptedRecordLength;
            var position = filer.Position;
            IResponse response = null;
            OrderSeparator sep = null;

            do
            {
                if ( position < recordLength )
                {
                    return null;
                }

                filer.Seek( -recordLength, SeekOrigin.Current );
                var record = Utils.ByteArrayToString( filer.ReadRecord() );
                if ( !record.StartsWith( OrderSeparator.PREFIX ) )
                {
                    return null;
                }
                sep = new OrderSeparator( record );
                filer.Seek( -( recordLength * ( sep.Size + 1 ) ), SeekOrigin.Current );
                position = filer.Position;

                response = GetNext( skipSpecials );
                filer.Seek( position, SeekOrigin.Begin );
            }
            while ( skipSpecials && sep.Type == "Sp" );

            factory.SetOrderResponse( response, sep.Type == "Or" );
            return response;
        }

        private IResponse GetOrderFromEnd( int numOfRecordsFromEnd, bool mustBeSpecial )
        {
            var pos = filer.Position;
            var response = GetLastOrder( false );
            for ( var i = 0; i < numOfRecordsFromEnd - 1; i++ )
            {
                response = GetPreviousOrder( false );
            }

            if ( response != null && mustBeSpecial && factory.IsOrderResponse( response ) )
            {
                response = null;
            }

            filer.Seek( pos, SeekOrigin.Begin );

            return response;
        }

        private new void TestForNull( object testObject, Error error, string name )
        {
            if ( testObject == null )
            {
                var msg = $"The {name} is null.";
                Logger.Error( msg );
                throw new ResponseException( error, msg );
            }
        }

        /// <summary>
        /// Some methods are not supported for report files. This method tests
        /// if the file is a report and throws a NotSupported if it is true.
        /// </summary>
        private void TestIfSupported()
        {
            if ( filer == null )
            {
                return;
            }

            var header = filer.FileHeader;

            if ( header.FileType != "Batch" && header.FileType != ResponseType.Response )
            {
                var msg = "The method or property called is not supported for this file type.";
                Logger.Error( msg );
                throw new ResponseException( Error.NotSupported, msg );
            }
        }

        #endregion
    }
}

