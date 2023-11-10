using System;
using System.IO;
using JPMC.MSDK;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Filer
{
	/// <summary>
	/// Summary description for IFileWriter.
	/// </summary>
	public interface IFileWriter
	{
		/// <summary>
		/// Returns true if the end of file has been reached.
		/// </summary>
		bool EOF { get; }

		/// <summary>
		/// Returns the name of the file being written to.
		/// </summary>
		string FileName { get; }

		/// <summary>
		/// Returns the name of the file being written to.
		/// </summary>
		string FormatName { get; }

		/// <summary>
		/// The file header object.
		/// </summary>
		FileHeader FileHeader { get; }

		/// <summary>
		/// The record that separates one order from another in the file.
		/// </summary>
		OrderSeparator OrderSeparator { get; }

		/// <summary>
		/// The position where the data begins (after the file header).
		/// </summary>
		long StartPosition { get; }

		/// <summary>
		/// The current position in the file.
		/// </summary>
		long Position { get; }

		/// <summary>
		/// Moves the file pointer to a new position.
		/// </summary>
		/// <param name="offset">The new position relative to the origin.</param>
		/// <param name="origin">Species whether to move from the beginning, end, or current position.</param>
		void Seek(long offset, SeekOrigin origin);

		/// <summary>
		/// Seeks to the end of the file.
		/// </summary>
		void SeekToEnd();

		/// <summary>
		/// Write an integer to the file.
		/// </summary>
		/// <param name="num">The integer value to write.</param>
		void Write(int num);

		/// <summary>
		/// Returns true if the file is open, false if it is not.
		/// </summary>
		bool Open { get; }

		/// <summary>
		/// Returns the number of orders that have been successfully written to the batch.
		/// </summary>
		int OrderCount { get; } 

		/// <summary>
		/// Creates a new file and opens the stream.
		/// </summary>
		/// <param name="filename">The path to the file to create.</param>
		/// <param name="password">The password used for encryption.</param>
		/// <param name="header">The file header to write to the new file.</param>
		void CreateFile(string filename, string password, FileHeader header);

		/// <summary>
		/// Opens an existing file for writing. 
		/// </summary>
		/// <remarks>
		/// This is used to make changes to existing records in the file, 
		/// primarily for updating the header record.
		/// </remarks>
		/// <param name="filename">The name (and path) of the file to open.</param>
		/// <param name="password">The password used to encrypt the data that is written to the file.</param>
		void OpenFile(string filename, string password);

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
		void OpenFile(string filename, string password, bool forWriting);

		/// <summary>
		/// Write the specified bytes to the file. The bytes of the record will be encrypted before it is
		/// written.
		/// </summary>
		/// <param name="record">The bytes to encrypt and write to the file.</param>
		/// <returns>True if the record was written, false if it was not.</returns>
		bool WriteRecord(byte[] record);

		/// <summary>
		/// Write the specified bytes to the file. The bytes of the record will be encrypted before it is
		/// written.
		/// </summary>
		/// <param name="record">The bytes to encrypt and write to the file.</param>
		/// <param name="encrypt">True if the record is to be written encrypted, false if it is not.</param>
		/// <returns>True if the record was written, false if it was not.</returns>
		bool WriteRecord(byte[] record, bool encrypt);

		/// <summary>
		/// Decrypts the file into a new clear-text file. This is only supported in 
		/// SFTP.
		/// </summary>
		/// <returns>The filename of the decrypted file.</returns>
		string DecryptFile();

		/// <summary>
		/// Closes the file stream.
		/// </summary>
		void Close();

		/// <summary>
		/// Closes the file stream, closing the batch if closeBatch is true.
		/// </summary>
		/// <remarks>
		/// Closing the batch really just means write the final order separator, 
		/// and write the BatchClosed header value to 1.
		/// </remarks>
		/// <param name="closeBatch">Close the batch if true, false if not.</param>
		void Close(bool closeBatch);

		/// <summary>
		/// Returns true if the Incoming File exists, and false if it does not.
		/// </summary>
		bool Exists { get; }
    
		/// <summary>
		/// Returns a FileInfo object that represents the file.
		/// </summary>
		FileInfo File { get; }
		
		/// <summary>
		/// Gets and sets the bytes that mark the end of the file.
		/// </summary>
		byte[] FileTerminator { get; }

		/// <summary>
		/// Gets and sets the bytes that mark the end of an individual record.
		/// </summary>
		byte[] RecordTerminator { get; }

		/// <summary>
		/// The number of bytes to read at the beginning of the stream. 
		/// </summary>
		/// <remarks>
		/// This is used to determine the type of file being read and 
		/// the file terminator to be used.
		/// </remarks>
		int MinBytesToRead { get; }
    
		/// <summary>
		/// Specifies the type of data the file contains. 
		/// </summary>
		string ResponseType { get; }
    
		/// <summary>
		/// The size, in bytes, of the file.
		/// </summary>
		long Size { get; }

		/// <summary>
		/// The SFTP filename is used by the merchant when calling the Dispatcher's
		/// deleteServerFile method. The CommManager will set this field using this
		/// method, and the Dispatcher will wrap the call to getSFTPFilename for 
		/// the merchant to use.
		/// </summary>
		string SFTPFilename { get; set; }

		/// <summary>
		/// Flushes the stream to the file.
		/// </summary>
		void Flush();
	}
}
