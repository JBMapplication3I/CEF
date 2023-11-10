using System;
using System.IO;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for IFiler.
    /// </summary>
    public interface IFiler
    {
        /// <summary>
        /// Creates a new file and writes the file header.
        /// </summary>
        /// <param name="filename">The path to the file.</param>
        void CreateFile( string filename );
        /// <summary>
        /// Returns true if the file is open, false if it is not.
        /// </summary>
        bool Open { get; }
        /// <summary>
        /// Encrypts a single record.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="password">The password to encrypt it with.</param>
        /// <returns>An array of bytes containing the encrypted data.</returns>
        byte[] Encrypt( string data, string password );
        /// <summary>
        /// Encrypts a single record.
        /// </summary>
        /// <param name="data">The data to encrypt.</param>
        /// <param name="password">The password to encrypt it with.</param>
        /// <returns>An array of bytes containing the encrypted data.</returns>
        byte[] Encrypt( byte[] data, string password );
        /// <summary>
        /// Decrypts the data and returns it as a string.
        /// </summary>
        /// <param name="data">A byte array containing the data to decrypt.</param>
        /// <param name="password">The password used to decrypt the data.</param>
        /// <returns>A string representing the decrypted record.</returns>
        string Decrypt( byte[] data, string password );
        /// <summary>
        /// Moves the file pointer to a new position.
        /// </summary>
        /// <param name="offset">The new position relative to the origin.</param>
        /// <param name="origin">Species whether to move from the beginning, end, or current position.</param>
        void Seek( long offset, SeekOrigin origin );
        /// <summary>
        /// Writes a record of bytes to the file. 
        /// </summary>
        /// <param name="bytes">The record to write.</param>
        void Write( byte[] bytes );
        /// <summary>
        /// Writes an integer as a string to the file.
        /// </summary>
        /// <param name="num">The integer to write.</param>
        void Write( int num );
        /// <summary>
        /// Writes a record of bytes to the file.
        /// </summary>
        /// <param name="record">The record to write.</param>
        /// <param name="orderPosition"></param>
        void WriteRecord( byte[] record, int orderPosition );
        /// <summary>
        /// Reads a record of bytes from the file.
        /// </summary>
        /// <returns></returns>
        byte[] ReadRecord();
        /// <summary>
        /// Reads an integer from the file.
        /// </summary>
        /// <returns></returns>
        int ReadInt();
        /// <summary>
        /// The size, in bytes, of the file.
        /// </summary>
        long Length { get; }
        /// <summary>
        /// The length of each record.
        /// </summary>
        int RecordLength { get; }
        /// <summary>
        /// The length of each record after the record was encrypted.
        /// </summary>
        int EncryptedRecordLength { get; }
        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        OrderSeparator OrderSeparator { get; }
        /// <summary>
        /// Closes the file stream.
        /// </summary>
        void CloseFile();
        /// <summary>
        /// Opens the specified file.
        /// </summary>
        /// <param name="filename">The path to the file to open.</param>
        void OpenFile( string filename );
        /// <summary>
        /// Opens the specified file for reading or writing.
        /// </summary>
        /// <param name="filename">The path to the file to open.</param>
        /// <param name="forWriting">True if the file is opened for writing, false if for reading.</param>
        void OpenFile( string filename, bool forWriting );
        /// <summary>
        /// Gets the position of the effective end of file.
        /// </summary>
        /// <remarks>
        /// This may not be the exact end of the file. It is at the end of the last good record.
        /// </remarks>
        long EndOfFilePosition { get; }

        /// <summary>
        /// The current position in the file.
        /// </summary>
        long Position { get; }
        /// <summary>
        /// The position where the data begins (after the file header).
        /// </summary>
        long StartPosition { get; }
        /// <summary>
        /// Returns true if the end of the file has been reached.
        /// </summary>
        bool EOF { get; }
        /// <summary>
        /// An object that describes the file.
        /// </summary>
        FileHeader FileHeader { get; set; }
        /// <summary>
        /// Flushes the stream to the file.
        /// </summary>
        void Flush();
    }
}

