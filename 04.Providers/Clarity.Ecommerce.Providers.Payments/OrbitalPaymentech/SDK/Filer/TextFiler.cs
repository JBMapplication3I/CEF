using System;
using System.IO;
using JPMC.MSDK.Common;
using JPMC.MSDK.Framework;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for AESFiler.
    /// </summary>
    public class TextFiler : FrameworkBase, IFiler
    {
        private FileHeader header = new FileHeader();
        private IStreamWrapper stream;
        private int encryptedRecordLength = 0;
        private long startPosition;

        /// <summary>
        /// Default constructor.
        /// </summary>
        public TextFiler()
            : this( new DispatcherFactory() )
        {
        }

        /// <summary>
        /// Constructor with factory.
        /// </summary>
        /// <param name="factory"></param>
        public TextFiler( IDispatcherFactory factory )
        {
            this.factory = factory;
        }

        #region IFiler Members

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        public OrderSeparator OrderSeparator => null;

        /// <summary>
        /// Gets the position of the effective end of file.
        /// </summary>
        /// <remarks>
        /// This may not be the exact end of the file. It is at the end of the last good record.
        /// </remarks>
        public long EndOfFilePosition => stream.Length;

        /// <summary>
        /// An object that describes the file.
        /// </summary>
        public FileHeader FileHeader
        {
            get => header;
            set => header = value;
        }

        /// <summary>
        /// The length of each record after the record was encrypted.
        /// </summary>
        public int EncryptedRecordLength => this.encryptedRecordLength;

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

            var fileMgr = factory.MakeFileManager();
            if ( fileMgr.Exists( filename ) )
            {
                var message = $"The file \"{filename}\" already exist.";
                Logger.Error( message );
                throw new FilerException( Error.FileExists, message );
            }

            // Write the header.
            try
            {
                stream = factory.MakeStreamWrapper( filename, true, true );
                this.startPosition = stream.Position;
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.FileCreationFailure, ex.Message, ex );
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
            return Utils.StringToByteArray( data );
        }

        /// <summary>
        /// Encrypts a single record.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="password">The password to encrypt it with.</param>
        /// <returns>An array of bytes containing the encrypted data.</returns>
        public byte[] Encrypt( byte[] data, string password )
        {
            return data;
        }

        /// <summary>
        /// Decrypts the data and returns it as a string.
        /// </summary>
        /// <param name="data">A byte array containing the data to decrypt.</param>
        /// <param name="password">The password used to decrypt the data.</param>
        /// <returns>A string representing the decrypted record.</returns>
        public string Decrypt( byte[] data, string password )
        {
            return Utils.ByteArrayToString( data );
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
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.SeekFailed, ex.Message, ex );
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
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.WriteFailure, ex.Message, ex );
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
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.WriteFailure, ex.Message, ex );
            }
        }

        /// <summary>
        /// Writes a record of bytes to the file.
        /// </summary>
        /// <param name="record">The record to write.</param>
        /// <param name="orderPosition"></param>
        public void WriteRecord( byte[] record, int orderPosition )
        {
            if ( header.LineLength > 0 )
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
                return stream.ReadBytes( this.encryptedRecordLength );
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.ReadFailure, ex.Message, ex );
            }
        }

        /// <summary>
        /// Returns true if the end of the file has been reached.
        /// </summary>
        public bool EOF => stream.EOF;

        /// <summary>
        /// The size, in bytes, of the file.
        /// </summary>
        public long Length => stream.Length;

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
            if ( stream == null || !stream.IsOpen )
                return;
            try { stream.Close(); }
            catch { }
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
                this.startPosition = stream.Position;
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

        /// <summary>
        /// Flushes the stream to the file.
        /// </summary>
        public void Flush() { }
        #endregion

        #region Private Members
        private void TestForNull( object testObject, Error error, string name )
        {
            TestForNull( testObject, error, name, false );
        }

        private void TestForNull( object testObject, Error error, string name, bool isFullMessage )
        {
            if ( testObject == null )
            {
                string msg = null;
                if ( isFullMessage )
                {
                    msg = name;
                }
                else
                {
                    msg = $"The {name} is null.";
                }
                Logger.Error( msg );
                throw new FilerException( error, msg );
            }
        }

        private void WriteFixedLengthRecord( byte[] record, int orderPosition )
        {
            try
            {
                stream.Write( record );
            }
            catch ( Exception ex )
            {
                Logger.Error( ex.Message, ex );
                throw new FilerException( Error.WriteFailure, ex.Message, ex );
            }
        }
        #endregion
    }
}

