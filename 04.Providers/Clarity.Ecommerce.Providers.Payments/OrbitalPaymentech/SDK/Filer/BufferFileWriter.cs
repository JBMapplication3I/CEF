using System;
using System.Text;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for BufferFileWriter.
    /// </summary>
    public class BufferFileWriter : IFileWriter
    {
        /// <summary>
        /// 
        /// </summary>
        public StringBuilder builder = new StringBuilder();

        /// <summary>
        /// 
        /// </summary>
        public BufferFileWriter()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #region IFileWriter Members

        /// <summary>
        /// Returns true if the end of file has been reached.
        /// </summary>
        public bool EOF => false;

        /// <summary>
        /// Returns the number of bytes to read from the socket and write to the file at a time.
        /// </summary>
        public int RecordSize => 0;

        /// <summary>
        /// Seeks to the end of the file.
        /// </summary>
        public void SeekToEnd()
        {

        }

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        public OrderSeparator OrderSeparator => null;

        /// <summary>
        /// Returns the name of the file being written to.
        /// </summary>
        public string FileName => null;

        /// <summary>
        /// Returns the name of the file being written to.
        /// </summary>
        public string FormatName => null;

        /// <summary>
        /// The file header object.
        /// </summary>
        public FileHeader FileHeader => null;

        /// <summary>
        /// The position where the data begins (after the file header).
        /// </summary>
        public long StartPosition => 0;

        /// <summary>
        /// The current position in the file.
        /// </summary>
        public long Position => 0;

        /// <summary>
        /// Moves the file pointer to a new position.
        /// </summary>
        /// <param name="offset">The new position relative to the origin.</param>
        /// <param name="origin">Species whether to move from the beginning, end, or current position.</param>
        public void Seek( long offset, System.IO.SeekOrigin origin )
        {
            // TODO:  Add BufferFileWriter.Seek implementation
        }

        /// <summary>
        /// Write an integer to the file.
        /// </summary>
        /// <param name="num">The integer value to write.</param>
        public void Write( int num )
        {
            // TODO:  Add BufferFileWriter.Write implementation
        }

        /// <summary>
        /// Returns true if the file is open, false if it is not.
        /// </summary>
        public bool Open => true;

        /// <summary>
        /// Returns the number of orders that have been successfully written to the batch.
        /// </summary>
        public int OrderCount => 0;

        /// <summary>
        /// Creates a new file and opens the stream.
        /// </summary>
        /// <param name="filename">The path to the file to create.</param>
        /// <param name="password">The password used for encryption.</param>
        /// <param name="header">The file header to write to the new file.</param>
        public void CreateFile( string filename, string password, FileHeader header )
        {
            // TODO:  Add BufferFileWriter.CreateFile implementation
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
            // TODO:  Add BufferFileWriter.OpenFile implementation
        }

        /// <summary>
        /// Write the specified bytes to the file. The bytes of the record will be encrypted before it is
        /// written.
        /// </summary>
        /// <param name="record">The bytes to encrypt and write to the file.</param>
        /// <returns>True if the record was written, false if it was not.</returns>
        public bool WriteRecord( byte[] record )
        {
            var line = Utils.ByteArrayToString( record );
            builder.Append( line );
            return true;
        }

        /// <summary>
        /// Write the specified bytes to the file. The bytes of the record will be encrypted before it is
        /// written.
        /// </summary>
        /// <param name="record">The bytes to encrypt and write to the file.</param>
        /// <param name="encrypt">True if the record is to be written encrypted, false if it is not.</param>
        /// <returns>True if the record was written, false if it was not.</returns>
        public bool WriteRecord( byte[] record, bool encrypt )
        {
            return WriteRecord( record );
        }

        /// <summary>
        /// Decrypts the file into a new clear-text file. This is only supported in 
        /// SFTP.
        /// </summary>
        /// <returns>The filename of the decrypted file.</returns>
        public string DecryptFile()
        {
            return builder.ToString();
        }

        /// <summary>
        /// Flushes the stream to the file.
        /// </summary>
        public void Flush()
        {
        }

        /// <summary>
        /// Closes the file stream.
        /// </summary>
        public void Close()
        {
            // TODO:  Add BufferFileWriter.Close implementation
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
            // TODO:  Add BufferFileWriter.Close implementation
        }

        /// <summary>
        /// Returns true if the Incoming File exists, and false if it does not.
        /// </summary>
        public bool Exists => false;

        /// <summary>
        /// Returns a FileInfo object that represents the file.
        /// </summary>
        public System.IO.FileInfo File => null;

        /// <summary>
        /// Gets and sets the bytes that mark the end of the file.
        /// </summary>
        public byte[] FileTerminator => null;

        /// <summary>
        /// Gets and sets the bytes that mark the end of an individual record.
        /// </summary>
        public byte[] RecordTerminator => null;

        /// <summary>
        /// The number of bytes to read at the beginning of the stream. 
        /// </summary>
        /// <remarks>
        /// This is used to determine the type of file being read and 
        /// the file terminator to be used.
        /// </remarks>
        public int MinBytesToRead => 0;

        /// <summary>
        /// Specifies the type of data the file contains. 
        /// </summary>
        public string ResponseType => null;

        /// <summary>
        /// The size, in bytes, of the file.
        /// </summary>
        public long Size => builder.Length;

        /// <summary>
        /// The SFTP filename is used by the merchant when calling the Dispatcher's
        /// deleteServerFile method. The CommManager will set this field using this
        /// method, and the Dispatcher will wrap the call to getSFTPFilename for 
        /// the merchant to use.
        /// </summary>
        public string SFTPFilename
        {
            get => null;
            set
            {
                // TODO:  Add BufferFileWriter.SFTPFilename setter implementation
            }
        }

        #endregion
    }
}
