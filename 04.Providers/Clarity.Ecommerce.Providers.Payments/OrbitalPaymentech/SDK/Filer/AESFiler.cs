using System;
using System.IO;
using JPMC.MSDK.Framework;
using JPMC.MSDK.Common;
using System.Text.RegularExpressions;


namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for AESFiler.
    /// </summary>
    public class AESFiler : FrameworkBase, IFiler
    {
        private FileHeader header = new FileHeader();
        private IStreamWrapper stream;
        private string filename;
        private long startPosition;
        private int orderCount;
        private int linesWritten;
		private AESProcessor processor = new AESProcessor();

        /// <summary>
        /// 
        /// </summary>
        public AESFiler()
            : this( null, new DispatcherFactory() )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        public AESFiler( FileHeader header )
            : this( header, new DispatcherFactory() )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="header"></param>
        /// <param name="factory"></param>
        public AESFiler( FileHeader header, IDispatcherFactory factory )
        {
            this.factory = factory;
            this.FileHeader = header;
        }

        #region IFiler Members

        /// <summary>
        /// An object that describes the file.
        /// </summary>
        public FileHeader FileHeader
        {
            get => header;
            set
            {
                header = value;
                if ( header == null )
                {
                    return;
                }
                InitEncryption();
            }
        }

        /// <summary>
        /// The length of each record after the record was encrypted.
        /// </summary>
        public int EncryptedRecordLength { get; private set; }

        /// <summary>
        /// Gets the position of the effective end of file.
        /// </summary>
        /// <remarks>
        /// This may not be the exact end of the file. It is at the end of the last good record.
        /// </remarks>
        public long EndOfFilePosition { get; private set; }

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        public OrderSeparator OrderSeparator { get; private set; }

        /// <summary>
        /// The length of each record.
        /// </summary>
        public int RecordLength => this.header.LineLength;

        /// <summary>
        /// Creates a new file and writes the file header.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        public void CreateFile( string filename )
        {
            TestForNull( header, Error.NullFileHeader, "File Header" );

            this.filename = filename;

            var fileMgr = factory.MakeFileManager();
            if ( fileMgr.Exists( filename ) && fileMgr.Length( filename ) > 0 )
            {
                var message = $"The file \"{filename}\" already exist.";
                Logger.Error( message );
                throw new FilerException( Error.FileExists, message );
            }

            if ( header.FileType.Length > 25 )
            {
                var msg = $"The header's FileType is too long (FileType={header.FileType})";
                Logger.Error( msg );
                throw new FilerException( Error.FileTypeTooLong, msg );
            }

            if ( header.FormatName.Length > 30 )
            {
                var msg = $"The header's FormatName is too long (FormatName={header.FormatName})";
                Logger.Error( msg );
                throw new FilerException( Error.FormatNameTooLong, msg );
            }

            if ( header.Version.Length > 20 )
            {
                var msg = $"The header's Version is too long (Version={header.Version})";
                Logger.Error( msg );
                throw new FilerException( Error.VersionTooLong, msg );
            }

            if ( header.RecordDelimiter.Length > 5 )
            {
                var msg = $"The header's RecordDelimiter is too long (RecordDelimiter={header.RecordDelimiter})";
                Logger.Error( msg );
                throw new FilerException( Error.RecordDelimiterTooLong, msg );
            }

            // Write the header.
            try
            {
                stream = factory.MakeStreamWrapper( filename, true, true );
                stream.Write( header.BatchClosed ? (int) 1 : (int) 0 );
                stream.Write( header.Version.PadRight( 20, ' ' ) );
                stream.Write( header.Salt );
                stream.Write( header.IterationCount );
                stream.Write( header.FileType.PadRight( 25, ' ' ) );
                stream.Write( header.FormatName.PadRight( 30, ' ' ) );
                stream.Write( header.LineLength );
                stream.Write( header.RecordDelimiter.PadRight( 5, ' ' ) );
                stream.Write( header.FieldDelimiter.PadRight( 5, ' ' ) );
                this.startPosition = stream.Position;
            }
            catch ( Exception ex )
            {
                var msg = "Exception caught while writing the header to the file.";
                Logger.Error( msg, ex );
                throw new FilerException( Error.FileCreationFailure, msg, ex );
            }
        }

        /// <summary>
        /// Returns true if the file is open, false if it is not.
        /// </summary>
        public bool Open
        {
            get
            {
                if ( stream == null )
                    return false;
                return stream.IsOpen;
            }
        }

        /// <summary>
        /// Encrypts a single record.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="password">The password to encrypt it with.</param>
        /// <returns>An array of bytes containing the encrypted data.</returns>
        public byte[] Encrypt( string data, string password )
        {
            return Encrypt( Utils.StringToByteArray( data ), password );
        }

        /// <summary>
         /// Encrypts a single record.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="password">The password to encrypt it with.</param>
        /// <returns>An array of bytes containing the encrypted data.</returns>
        public byte[] Encrypt( byte[] data, string password )
        {
            //AESProcessor processor = new AESProcessor();
            return processor.Encrypt( data, password, header.Salt );
        }

        /// <summary>
        /// Decrypts the data and returns it as a string.
        /// </summary>
        /// <param name="data">A byte array containing the data to decrypt.</param>
        /// <param name="password">The password used to decrypt the data.</param>
        /// <returns>A string representing the decrypted record.</returns>
        public string Decrypt( byte[] data, string password )
        {
            //AESProcessor processor = new AESProcessor();
            var bytes = processor.Decrypt(data, password, header.Salt);
            return Utils.ByteArrayToString(bytes);
        }

        private byte[] MakeSalt()
        {
            var SALT_CHARS = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789./".ToCharArray();

            var randomGenerator = new Random();
            var numSaltChars = SALT_CHARS.Length;
            // StringBuffer saltBuffer = new StringBuffer();
            var salt = new byte[20];
            for (var i = 0; i < 20; i++)
            {
                // saltBuffer.append(SALT_CHARS[Math.abs(randomGenerator.nextInt())
                // % numSaltChars]);
                salt[i] = (byte)SALT_CHARS[Math
                        .Abs(randomGenerator.Next()) % numSaltChars];
            }

            return salt;
        }

        /// <summary>
        /// Moves the file pointer to a new position.
        /// </summary>
        /// <param name="offset">The new position relative to the origin.</param>
        /// <param name="origin">Species whether to move from the beginning, end, or current position.</param>
        public void Seek( long offset, SeekOrigin origin )
        {
            TestForNull( stream, Error.FileNotOpen, "stream" );
            try
            {
                stream.Seek( offset, origin );
            }
            catch ( Exception ex )
            {
                var msg = $"Failed to seek to point {offset} from {origin.ToString()}";
                Logger.Error( msg , ex );
                throw new FilerException( Error.SeekFailed, msg, ex );
            }
        }

        /// <summary>
        /// Writes a record of bytes to the file. 
        /// </summary>
        /// <param name="bytes">The record to write.</param>
        public void Write( byte[] bytes )
        {
            TestForNull( stream, Error.FileNotOpen, "stream" );
            try
            {
                stream.Write( bytes );
            }
            catch ( Exception ex )
            {
                Logger.Error( "Failed to write data to the file.", ex );
                throw new FilerException( Error.WriteFailure, "Failed to write data to the file.", ex );
            }
        }

        /// <summary>
        /// Writes an integer as a string to the file.
        /// </summary>
        /// <param name="num">The integer to write.</param>
        public void Write( int num )
        {
            TestForNull( stream, Error.FileNotOpen, "stream" );
            try
            {
                stream.Write( num );
            }
            catch ( Exception ex )
            {
                Logger.Error( "Failed to write a number to the file.", ex );
                throw new FilerException( Error.WriteFailure, "Failed to write a number to the file.", ex );
            }
        }

        /// <summary>
        /// Writes a record of bytes to the file.
        /// </summary>
        /// <param name="record">The record to write.</param>
        /// <param name="orderPosition"></param>
        public void WriteRecord( byte[] record, int orderPosition )
        {
            this.WriteFixedLengthRecord( record, orderPosition );
        }

        /// <summary>
        /// Reads a record of bytes from the file.
        /// </summary>
        /// <returns></returns>
        public byte[] ReadRecord()
        {
            try
            {
                if ( header.FileType == "Batch" || header.FileType == ResponseType.Response )
                {
                    return stream.ReadBytes( EncryptedRecordLength );
                }

                var lineLength = ReadInt();
                if ( lineLength == 0 )
                {
                    return null;
                }
                return stream.ReadBytes( lineLength );
            }
            catch ( Exception ex )
            {
                var msg = "Exception caught when reading bytes from the file.";
                Logger.Error( msg, ex );
                throw new FilerException( Error.ReadFailure, msg, ex );
            }
        }

        /// <summary>
        /// Returns true if the end of the file has been reached.
        /// </summary>
        public bool EOF => stream.EOF;

        /// <summary>
        /// The size, in bytes, of the file.
        /// </summary>
        public long Length
        {
            get
            {
                var info = factory.MakeFileManager();
                return info.Length( filename );
            }
        }

        /// <summary>
        /// Reads an integer from the file.
        /// </summary>
        /// <returns></returns>
        public int ReadInt()
        {
            try
            {
                return stream.ReadInt();
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.ReadFailure, ex.Message, ex );
            }
        }

        /// <summary>
        /// Closes the file stream.
        /// </summary>
        public void CloseFile()
        {
            try
            {
                if (stream != null)
                {
                    stream.Close();
                    stream = null;
                }
				processor.Dispose();
            }
            catch { }

            Logger.DebugFormat( "{0} closed, {1} lines written.", filename, linesWritten );
        }

        /// <summary>
        /// Flushes the stream to the file.
        /// </summary>
        public void Flush()
        {
            stream.Flush();
        }

        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="filename">The path to the file to open.</param>
        public void OpenFile( string filename )
        {
            OpenFile( filename, false );
        }

        /// <summary>
        /// Opens the specified file for reading or writing.
        /// </summary>
        /// <param name="filename">The path to the file to open.</param>
        /// <param name="forWriting">True if the file is opened for writing, false if for reading.</param>
        public void OpenFile( string filename, bool forWriting )
        {
            TestForNull( filename, Error.NullFileName, "filename" );

            linesWritten = 0;

            this.filename = filename;

            var fileMgr = factory.MakeFileManager();
            if ( !fileMgr.Exists( filename ) )
            {
                var msg = $"The file \"{filename}\" does not exist.";
                Logger.Error( msg );
                throw new FilerException( Error.FileNotFound, msg );
            }

            this.header = new FileHeader();

            stream = factory.MakeStreamWrapper( filename, false, forWriting );

            try
            {
                var batchClosed = stream.ReadInt();
                header.BatchClosed = batchClosed == 1;
                header.Version = new string( stream.ReadChars( 20 ) ).Trim();
                // Check if this is an old version.
                if ( !ValidateVersion( header.Version ) )
                {
                    stream.Seek( -20, SeekOrigin.Current );
                    header.Version = "1.0.0";
                }
                header.Salt = stream.ReadBytes( 20 );
                header.IterationCount = stream.ReadInt();
                header.FileType = new string( stream.ReadChars( 25 ) ).Trim();
                if ( header.Version == "1.0.0" )
                {
                    header.FormatName = new string( stream.ReadChars( 10 ) ).Trim();
                }
                else
                {
                    header.FormatName = new string( stream.ReadChars( 30 ) ).Trim();
                }
                header.LineLength = stream.ReadInt();
                header.RecordDelimiter = new string( stream.ReadChars( 5 ) ).Replace( " ", "" );
                header.FieldDelimiter = new string( stream.ReadChars( 5 ) ).Replace( " ", "" );
                var div = (float) header.LineLength / 16.0F;
                var multiplier = (int) div;
                if ( div > multiplier )
                {
                    multiplier++;
                }
                EncryptedRecordLength = multiplier * 16;
                if ( header.FileType == "Response" || header.FileType == "Batch" )
                {
                    this.EndOfFilePosition = FindEffectiveEndOfFile();
                }
                else
                {
                    this.EndOfFilePosition = stream.Length;
                }
                this.startPosition = stream.Position;
                InitEncryption();
            }
            catch ( FilerException )
            {
                throw;
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.InvalidFileHeader, ex.Message, ex );
            }
        }

        private bool ValidateVersion( string version )
        {
            var vers = version.Split( '.' );
            if ( vers.Length < 3 || vers.Length > 4 )
            {
                return false;
            }

            if ( !Utils.IsNumeric( vers[ 0 ] ) || int.Parse( vers[ 0 ] ) == 0 )
            {
                return false;
            }

            for ( var i = 1; i < vers.Length; i++ )
            {
                if ( !Utils.IsNumeric( vers[ i ] ) )
                {
                    return false;
                }
            }

            return true;
        }

        private long FindEffectiveEndOfFile()
        {
            if ( header.LineLength == 0 )
            {
                return stream.Length;
            }

            var originalPosition = stream.Position;
            stream.Seek( 0, SeekOrigin.End );

            while ( stream.Position > 0 )
            {
                var pos = stream.Position;
                stream.Seek( -1, SeekOrigin.Current );
                var test = Utils.ByteArrayToString( stream.ReadBytes( 8 ) );
                stream.Seek( -8, SeekOrigin.Current );
                if ( test.StartsWith( OrderSeparator.PREFIX ) )
                {
                    test = Utils.ByteArrayToString( stream.ReadBytes( this.EncryptedRecordLength ) );
                    if ( test.Length == EncryptedRecordLength )
                    {
                        OrderSeparator = new OrderSeparator( test );
                        pos = stream.Position;
                        stream.SetLength( pos );
                        stream.Seek( originalPosition, SeekOrigin.Begin );
                        return pos;
                    }
                }
            }

            stream.Seek( originalPosition, SeekOrigin.Begin );
            return 0L;
        }

        /// <summary>
        /// The current position in the file.
        /// </summary>
        public long Position
        {
            get
            {
                TestForNull( stream, Error.FileNotOpen, "stream" );
                return stream.Position;
            }
        }

        /// <summary>
        /// The position where the data begins (after the file header).
        /// </summary>
        public long StartPosition
        {
            get
            {
                TestForNull( stream, Error.FileNotOpen, "stream" );
                return this.startPosition;
            }
        }
        #endregion

        #region Private Members

        private void InitEncryption()
        {
            if ( header.Salt == null )
            {
                header.Salt = MakeSalt();
            }

            var div = (float) header.LineLength / 16.0F;
            var multiplier = (int) div;
            if ( div > multiplier )
            {
                multiplier++;
            }

            EncryptedRecordLength = multiplier * 16;
        }

        private void TestForNull( object testObject, Error error, string name )
        {
            TestForNull( testObject, error, name, false );
        }

        private void TestForNull( object testObject, Error error, string name, bool isFullMessage )
        {
            if ( testObject == null )
            {
                var msg = isFullMessage ? name : $"The {name} is null.";
                Logger.Error( msg );
                throw new FilerException( error, msg );
            }
        }

        private void WriteFixedLengthRecord( byte[] record, int orderPosition )
        {
            try
            {
                if ( orderPosition == 1 )
                {
                    orderCount++;
                }
                stream.Write( record );
                this.linesWritten++;
            }
            catch ( Exception ex )
            {
                var msg = "Failed to write a fixed length message.";
                Logger.Error( msg, ex );
                throw new FilerException( Error.WriteFailure, msg, ex );
            }
        }
        #endregion
    }
}

