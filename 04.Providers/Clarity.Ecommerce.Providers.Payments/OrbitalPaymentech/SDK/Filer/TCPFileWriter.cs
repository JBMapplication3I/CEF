using System;
using System.IO;
using System.Text;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Framework;
using log4net;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for FileWriter.
    /// </summary>
    public class TCPFileWriter : IFileWriter
    {
        private string lastMRecord;
        private IDispatcherFactory factory;
        private string filename;
        private string password;
        private FileHeader header;
        private IFiler filer;
        private IFileWriter txtFileWriter;
        private int orderPosition;
        private int orderCount;
        private int recordCount;
        private IBatchConverter converter;
        private StringBuilder buffer = new StringBuilder();
        private string delimiter;
        private int recordLength;
        private OrderSeparator separator;
        private string format;

        private string fileTerminator;
        private string sftpFileName;
        private string responseType;
        private bool fileOpen;
        private ConfigurationData configData;

        /// <summary>
        /// If this flag is true, then there is no valid data to
        /// write to the file and the writer must ignore all calls
        /// to WriteRecord.
        /// </summary>
        private bool doNotWriteFile;
        private bool endOfFile;
        private IRuleEngine ruleEngine;
        private ILog logger;

        private bool prevWasMrecord;

        private int count;


        /// <summary>
        ///
        /// </summary>
        /// <param name="configData"></param>
        /// <param name="factory"></param>
        public TCPFileWriter( ConfigurationData configData, IDispatcherFactory factory )
        {
            this.factory = factory;
            this.configData = configData;
            converter = factory.MakeBatchConverter( configData );
            logger = factory.Logger;
        }

        #region IFileWriter Members

        /// <summary>
        /// Creates a new file and opens the stream.
        /// </summary>
        /// <param name="filename">The path to the file to create.</param>
        /// <param name="password">The password used for encryption.</param>
        /// <param name="header">The file header to write to the new file.</param>
        public void CreateFile( string filename, string password, FileHeader header )
        {
            this.filename = filename;
            this.password = password;
            this.header = header;
            this.fileOpen = true;
        }

        /// <summary>
        /// Returns true if the end of file has been reached.
        /// </summary>
        public bool EOF
        {
            get
            {
                if ( ruleEngine != null )
                {
                    ruleEngine.SetEndRead();
                    if ( ruleEngine.Satisfied )
                    {
                        endOfFile = true;
                    }
                }
                return endOfFile;
            }
        }

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        public OrderSeparator OrderSeparator
        {
            get
            {
                if ( filer == null )
                    return null;
                return this.filer.OrderSeparator;
            }
        }

        /// <summary>
        /// Opens an existing file for writing.
        /// </summary>
        /// <remarks>
        /// This is used to make changes to existing records in the file,
        /// primarily for updating the header record.
        /// </remarks>
        /// <param name="filename">The name (and path) of the file to open.</param>
        /// <param name="password">The password used to encrypt the data that is written to the file.</param>
        public void OpenFile( string filename, string password )
        {
            OpenFile( filename, password, false );
        }

        /// <summary>
        /// Opens an existing file for writing.
        /// </summary>
        /// <remarks>
        /// This is used to make changes to existing records in the file,
        /// primarily for updating the header record.
        /// </remarks>
        /// <param name="filename">The name (and path) of the file to open.</param>
        /// <param name="password">The password used to encrypt the data that is written to the file.</param>
        /// <param name="forWriting">True if the file is to be opened for writing
        /// and false if it should be opened for reading.</param>
        public void OpenFile( string filename, string password, bool forWriting )
        {
            if ( this.filename != null )
                throw new FilerException( Error.InitializationFailure, "FileWriter is already initialized." );

            this.filename = filename;
            this.password = password;

            filer = factory.MakeAESFiler( header );
            filer.OpenFile( filename, forWriting );
            this.header = filer.FileHeader;
            this.fileOpen = true;
        }

        /// <summary>
        /// Write an integer to the file.
        /// </summary>
        /// <param name="num">The integer value to write.</param>
        public void Write( int num )
        {
            if ( filer != null )
                filer.Write( num );
        }

        /// <summary>
        /// Returns true if the file is open, false if it is not.
        /// </summary>
        public bool Open
        {
            get
            {
                if ( filer != null && filer.Open )
                    return true;
                return this.fileOpen;
            }
        }

        /// <summary>
        /// Returns the number of orders that have been successfully written to the batch.
        /// </summary>
        public int OrderCount => this.orderCount;

        /// <summary>
        /// Returns the name of the file being written to.
        /// </summary>
        public string FileName => filename;

        /// <summary>
        /// Returns the name of the file being written to.
        /// </summary>
        public string FormatName => this.format;

        /// <summary>
        /// The file header object.
        /// </summary>
        public FileHeader FileHeader
        {
            get
            {
                if ( filer == null )
                    return null;

                return filer.FileHeader;
            }
        }

        /// <summary>
        /// The position where the data begins (after the file header).
        /// </summary>
        public long StartPosition
        {
            get
            {
                if ( filer == null )
                    return 0L;
                return filer.StartPosition;
            }
        }

        /// <summary>
        /// The current position in the file.
        /// </summary>
        public long Position
        {
            get
            {
                if ( filer == null )
                    return 0L;
                return filer.Position;
            }
        }

        /// <summary>
        /// Moves the file pointer to a new position.
        /// </summary>
        /// <param name="offset">The new position relative to the origin.</param>
        /// <param name="origin">Species whether to move from the beginning, end, or current position.</param>
        public void Seek( long offset, SeekOrigin origin )
        {
            if ( filer != null )
                filer.Seek( offset, origin );
        }

        /// <summary>
        /// Seeks to the end of the file.
        /// </summary>
        public void SeekToEnd()
        {
            if ( filer != null )
                filer.Seek( filer.EndOfFilePosition, SeekOrigin.Begin );
        }

        /// <summary>
        /// Writes a record of bytes to the file.
        /// </summary>
        /// <remarks>
        /// This method determines if the record being written is the first
        /// record of an order and will set the order position accordingly.
        /// </remarks>
        /// <param name="record">The byte array to be written to the file.</param>
        /// <returns>True if the record was written, false if it was not.</returns>
        public bool WriteRecord( byte[] record )
        {
            return WriteRecord( record, true );
        }

        /// <summary>
        /// Flushes the stream to the file.
        /// </summary>
        public void Flush()
        {
            filer.Flush();
        }

        /// <summary>
        /// Writes a record of bytes to the file.
        /// </summary>
        /// <remarks>
        /// This method determines if the record being written is the first
        /// record of an order and will set the order position accordingly.
        /// </remarks>
        /// <param name="bytes">The byte array to be written to the file.</param>
        /// <param name="encrypt">True if the record is to be encrypted, false if it is not.</param>
        /// <returns>True if the record was written, false if it was not.</returns>
        public bool WriteRecord( byte[] bytes, bool encrypt )
        {
            if ( bytes == null )
            {
                return false;
            }

            var record = Utils.ByteArrayToString( bytes );

            if ( header.FileType == "Batch" && record.StartsWith( "Order" ) && !encrypt )
            {
                filer.WriteRecord( Utils.StringToByteArray( record ), orderPosition );
                recordCount++;
                return true;
            }

            if ( ruleEngine != null && bytes.Length == 0 )
            {
                ruleEngine.SetEndRead();
            }

            buffer.Append( record );

            if ( responseType == null )
            {
                GetResponseType( buffer.ToString() );
            }
            else
            {
                if ( ruleEngine != null )
                {
                    var terminator = ruleEngine.AddData( bytes );
                    if ( ruleEngine.Satisfied )
                    {
                        endOfFile = true;
                        this.fileTerminator = terminator;
                    }
                }
            }

            var retVal = false;

            // We buffered up enough
            if ( responseType != null && !this.doNotWriteFile )
            {
                // This indicates that the file is PGP compress/encrypt
                // Just right the file as it is
                // no aes is required at this time
                if ( this.responseType.Equals( "TCPPGP" ) )
                {
                    retVal = WriteTextRecord();
                }

                if ( this.responseType == JPMC.MSDK.ResponseType.Response
                    || this.header.FileType == "Batch" )
                {
                    retVal = WriteFixedLengthRecord( encrypt );
                }
                else
                {
                    retVal = WriteVariableLengthRecord( encrypt );

                }
            }

            if ( endOfFile && buffer.Length > 0 )
            {
                buffer.Remove( 0, buffer.Length );
            }

            return retVal;
        }
        /// <summary>
        /// This write the data to the text files
        /// Used in TCP PGP
        /// </summary>
        /// <returns>True if the record was successfully written.</returns>
        protected bool WriteTextRecord()
        {
            //boolean hasWritten = false;
            if ( ruleEngine != null && ruleEngine.Satisfied )
            {
                endOfFile = true;
            }
            try
            {
                txtFileWriter.WriteRecord( Utils.StringToByteArray( buffer.ToString() ) );
                buffer.Remove( 0, buffer.Length );
            }
            catch ( Exception )
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Decrypts the file into a new clear-text file. This is only supported in
        /// SFTP.
        /// </summary>
        /// <returns>The filename of the decrypted file.</returns>
        public string DecryptFile()
        {
            // TODO:  Add FileWriter.DecryptFile implementation
            return null;
        }

        /// <summary>
        /// Closes the file stream.
        /// </summary>
        public void Close()
        {
            if ( Open )
            {
                Close( true );
            }
        }

        /// <summary>
        /// Closes the file stream, closing the batch if closeBatch is true.
        /// </summary>
        /// <remarks>
        /// Closing the batch really just means write the final order separator,
        /// and write the BatchClosed header value to 1.
        /// </remarks>
        /// <param name="closeBatch">Close the batch if true, false if not.</param>
        public void Close( bool closeBatch )
        {
            // Its a PGP file, do the simple close
            if ( buffer.Length > 0 )
            {
                logger.Info( "SDK didn't write " + buffer.Length + " bytes of data to the aes file" );
            }
            if ( txtFileWriter != null )
            {
                logger.Debug( "Received PGP encrypted/compressed file over TCP, temp file name is [" +
                    txtFileWriter.FileName + "]" );
                txtFileWriter.Close();
                txtFileWriter = null;
                return;
            }

            if ( Open && filer != null )
            {
                if ( closeBatch )
                {
                    WriteSeparator( null, false );
                    this.Seek( 0L, SeekOrigin.Begin );
                    this.Write( 1 );
                }
                filer.CloseFile();
                filer = null;
                fileOpen = false;
            }
        }

        /// <summary>
        /// Returns true if the Incoming File exists, and false if it does not.
        /// </summary>
        public bool Exists
        {
            get
            {
                var mgr = factory.MakeFileManager();
                return mgr.Exists( filename );
            }
        }

        /// <summary>
        /// Returns a FileInfo object that represents the file.
        /// </summary>
        public System.IO.FileInfo File => new FileInfo( filename );

        /// <summary>
        /// Gets and sets the bytes that mark the end of the file.
        /// </summary>
        public byte[] FileTerminator
        {
            get
            {
                if ( fileTerminator == null )
                    return null;
                return Utils.StringToByteArray( fileTerminator );
            }
        }

        /// <summary>
        /// Gets and sets the bytes that mark the end of an individual record.
        /// </summary>
        public byte[] RecordTerminator
        {
            get
            {
                if ( delimiter == null )
                {
                    if ( filer != null )
                        return Utils.StringToByteArray( filer.FileHeader.RecordDelimiter );
                    else
                        return null;
                }
                return Utils.StringToByteArray( delimiter );
            }
        }

        /// <summary>
        /// The number of bytes to read at the beginning of the stream.
        /// </summary>
        /// <remarks>
        /// This is used to determine the type of file being read and
        /// the file terminator to be used.
        /// </remarks>
        public int MinBytesToRead => this.converter.MinBytesToRead;

        /// <summary>
        /// Specifies the type of data the file contains.
        /// </summary>
        public string ResponseType => this.responseType;

        /// <summary>
        /// The size, in bytes, of the file.
        /// </summary>
        public long Size => filer.Length;

        /// <summary>
        /// The SFTP filename is used by the merchant when calling the Dispatcher's
        /// deleteServerFile method. The CommManager will set this field using this
        /// method, and the Dispatcher will wrap the call to getSFTPFilename for
        /// the merchant to use.
        /// </summary>
        public string SFTPFilename
        {
            get => this.sftpFileName;
            set => sftpFileName = value;
        }

        #endregion

        #region Private Methods

        private void GetResponseType( string record )
        {
            // Don't call GetResponseAttr when sending a batch.
            if ( this.header.FileType == "Batch" )
            {
                if ( filer == null )
                {
                    filer = factory.MakeAESFiler( header );
                    filer.CreateFile( filename );
                    this.fileOpen = true;
                }
                this.responseType = "Batch";
                this.delimiter = header.RecordDelimiter;
                this.recordLength = header.LineLength;
                return;
            }

            // We don't have enough data yet.
            if ( record.Length < converter.MinBytesToRead )
                return;

            var stringToTest = record;
            if ( record.Length > converter.MinBytesToRead )
                stringToTest = record.Substring( 0, converter.MinBytesToRead );
            ConverterArgs args = null;
            try
            {
                args = converter.GetResponseRecordInfo( stringToTest.TrimStart( ' ' ), null, configData.Protocol );
            }
            catch ( ConverterException ex )
            {
                throw new FilerException( ex.ErrorCode, ex.Message, ex );
            }

            this.responseType = args.StrData;
            this.delimiter = args.RecordDelimiter;
            this.recordLength = args.IntData;
            this.fileTerminator = args.FileTerminator;
            this.format = args.Format;

            if ( !record.Contains( this.delimiter ) )
            {
                var delim = this.delimiter == "\r" ? "\n" : "\r";
                if ( record.Contains( delim ) )
                {
                    this.delimiter = delim;
                }
            }

            if ( responseType == null )
            {
                var msg = "The ResponseType was not returned from the Converter.";
                logger.Error( msg );
                throw new FilerException( Error.InvalidResponseType, msg );
            }

            ruleEngine = factory.MakeRuleEngine( this.responseType );
            ruleEngine.AddData( Utils.StringToByteArray( record ) );
            if ( ruleEngine.Satisfied )
            {
                endOfFile = true;
            }

            // If SkipWrite is true, then the file has no data and the writer
            // is not to write any data beyond this point.
            if ( args.SkipWrite )
            {
                doNotWriteFile = true;
                return;
            }

            // This block is for handling TCP PGP
            // Just create an empty file for writing  the PGP as it is
            if ( filer == null )
            {
                if ( this.responseType.Equals( "TCPPGP" ) )
                {
                    try
                    {
                        var fileMgr = factory.MakeFileManager();
                        filename = fileMgr.CreateTemp( "rec.pgp", FileType.Incoming, configData );
                        txtFileWriter = factory.MakeTextFileWriter( filename, responseType );
                        logger.Debug(
                        "Receiving PGP encrypted/compressed file over TCP, " +
                        "temp file name is [" + txtFileWriter.FileName + "]" );
                    }
                    catch ( DispatcherException ex )
                    {
                        throw new FilerException( Error.WriteFailure,
                            ex.Message, ex );
                    }
                }
                else
                {
                    this.header.RecordDelimiter = delimiter;
                    this.header.FieldDelimiter = args.FieldDelimiter;
                    this.header.LineLength = recordLength;
                    this.header.FileType = this.responseType;
                    this.header.FormatName = args.Format;
                    filer = factory.MakeAESFiler( header );
                    filer.CreateFile( filename );
                }
            }
        }

        private OrderSeparator CreateSeparator( byte[] record )
        {
            var line = Utils.ByteArrayToString( record );
            var sep = new OrderSeparator();
            if ( line.StartsWith( "PID" ) || line.StartsWith( "B " ) || line.StartsWith( "T " ) )
                sep.Type = "Sp";
            return sep;
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="record"></param>
        /// <param name="prevWasMrecord"></param>
        protected void WriteSeparator( string record, bool prevWasMrecord )
        {
            if ( header.FileType == "Batch" )
                return;

            if ( separator != null && !prevWasMrecord )
            {
                separator.OrderCount = this.orderCount;
                separator.RecordCount = this.recordCount;
                separator.Size = this.orderPosition;
                filer.WriteRecord( Utils.StringToByteArray( separator.ToString() ), orderPosition );
            }

            if ( record != null )
                separator = CreateSeparator( Utils.StringToByteArray( record ) );
        }

        /// <summary>
        ///
        /// </summary>
        /// <param name="record"></param>
        /// <returns></returns>
        protected bool GetRecordFromBuffer( out string record )
        {
            record = null;

            // Bail if there aren't enough characters buffered yet.
            if ( buffer.Length < recordLength )
                return false;

            record = buffer.ToString( 0, recordLength );
            buffer.Remove( 0, recordLength );
            return true;
        }

        /// <summary>
        /// Writes a record to a variable-record-length response file.
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        protected bool WriteVariableLengthRecord( bool encrypt )
        {
            var hasWritten = false;

            do
            {
                // Do we have a record separator? Bail if we don't yet.
                var index = buffer.ToString().IndexOf( this.delimiter );
                if ( index == -1 )
                    return hasWritten;

                // This is required for PGP over TCP
                // The content of the PGP file won't have the delimiter at the end of the file
                // to avoid losing the last record, the PGP process will always write an extra
                // delimiter and the writer ignore if only the delimiter is to write
                if ( index == 0 && buffer.Length == 1 )
                {
                    buffer.Remove( 0, 1 );
                    return true;
                }

                var record = buffer.ToString( 0, index + delimiter.Length );
                buffer.Remove( 0, index + delimiter.Length );
                if ( record.Length == delimiter.Length )
                {
                    continue;
                }
                // Don't write the file terminator if it comes in.
                var term = ConformedTerminator();
                var rec = record;
                if ( rec.Length > term.Length )
                {
                    rec = rec.Substring( 0, term.Length );
                }
                if ( !string.IsNullOrEmpty( term ) && rec == term )
                {
                    logger.Debug( "Skipping terminator" );
                    continue;
                }

                if ( encrypt )
                {
                    var encrypted = filer.Encrypt( Utils.StringToByteArray( record ), password );
                    filer.Write( encrypted.Length );
                    filer.WriteRecord( encrypted, orderPosition );
                    recordCount++;
                }
                else
                {
                    filer.WriteRecord( Utils.StringToByteArray( record ), orderPosition );
                    recordCount++;
                }
                hasWritten = true;
            } while ( true );
        }

        private string ConformedTerminator()
        {
            count++;
            try
            {
                var terminator = this.fileTerminator;
                if ( terminator == null )
                {
                    return "";
                }
                while ( terminator.Length > this.delimiter.Length && terminator.StartsWith( delimiter ) )
                {
                    terminator = terminator.Substring( delimiter.Length );
                }

                var pos = terminator.IndexOf( this.delimiter );
                if ( pos == -1 )
                {
                    return terminator;
                }

                terminator = terminator.Substring( 0, pos );
                return terminator;
            }
            catch ( Exception ex )
            {
                throw;
            }
        }

        /// <summary>
        /// Writes a record to a fixed-record-length response file.
        /// </summary>
        /// <param name="encrypt"></param>
        /// <returns></returns>
        protected bool WriteFixedLengthRecord( bool encrypt )
        {
            var hasWritten = false;

            if ( ruleEngine != null && ruleEngine.Satisfied )
            {
                endOfFile = true;
            }

            do
            {
                string record;
                if ( !GetRecordFromBuffer( out record ) )
                    return hasWritten;

                if ( header.FileType == "Batch" )
                {
                    if ( record.StartsWith( "M" ) && record == lastMRecord )
                        continue;

                    if ( record.StartsWith( "M" ) )
                        lastMRecord = record;
                }

                prevWasMrecord = HandleResponseOrderSeparation( record, prevWasMrecord );

                if ( encrypt )
                {
                    var encrypted = filer.Encrypt( Utils.StringToByteArray( record ), password );
                    filer.WriteRecord( encrypted, orderPosition );
                    recordCount++;
                }
                else
                {
                    filer.WriteRecord( Utils.StringToByteArray( record ), orderPosition );
                    recordCount++;
                }
                hasWritten = true;
            } while ( true );
        }

        private bool HandleResponseOrderSeparation( string record, bool prevWasMrecord )
        {
            if ( this.header.FileType != JPMC.MSDK.ResponseType.Response )
                return false;

            if ( record.StartsWith( "M" ) || record.StartsWith( "B" ) || record.StartsWith( "T" ) ||
                record.StartsWith( "PID" ) || record.StartsWith( "S" ) )
            {
                WriteSeparator( record, prevWasMrecord );
                if ( record.StartsWith( "S" ) )
                    this.orderCount++;
                if ( !record.StartsWith( "S" ) || record.StartsWith( "S" ) && !prevWasMrecord )
                    this.orderPosition = 1;
                else
                    orderPosition++;
            }
            else
                this.orderPosition++;

            return record.StartsWith( "M" );
        }

        #endregion
    }
}

