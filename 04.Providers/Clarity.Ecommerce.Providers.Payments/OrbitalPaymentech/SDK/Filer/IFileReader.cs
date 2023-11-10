using System;
using System.IO;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for IFileReader.
    /// </summary>
    public interface IFileReader
    {
        /// <summary>
        /// Gets or sets the set of bytes that terminates a file. 
        /// In the case of TCP Batch, it would be EOFEOFEOF.
        /// </summary>
        byte[] FileTerminator { get; set; }
        /// <summary>
        /// Gets or sets a flag specifying if the filename
        /// of the file being read is already formatted for
        /// sending to the SFTP server.
        /// </summary>
        bool IsSFTPFileNameSet { get; set; }
        /// <summary>
        /// The file being read.
        /// </summary>
        FileInfo File { get; }

        /// <summary>
        /// The file header read from the beginning of the file.
        /// </summary>
        FileHeader FileHeader { get; }

        /// <summary>
        /// Determines if there are more records in the file to read.
        /// </summary>
        /// <returns>True if there are more records in the file to read, and false if there are no more.</returns>
        bool HasNextRecord { get; }
        /// <summary>
        /// Resets the reader back to the beginning of the file.
        /// </summary>
        void ResetReader();

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        OrderSeparator OrderSeparator { get; }

        /// <summary>
        /// Returns the next record from the file sequentially. The record read from the file will be
        /// decrypted automatically.
        /// </summary>
        /// <returns></returns>
        byte[] GetNextRecord();

        /// <summary>
        /// Closes the file file.
        /// </summary>
        void Close();

        /// <summary>
        /// Returns the name of the file being read. This name does not include
        /// the file extension.
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// Returns the fully-qualified path to the file being read.
        /// </summary>
        string FilePath { get; set; }

        /// <summary>
        /// Sets or gets the total number of records in the file.
        /// </summary>
        long TotalRecords { get; set; }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        void DeleteFile();

        /// <summary>
        /// The CommManager will use this to get the real name of the submission
        ///	in order to build the proper filename as it should be on the server.
        /// This removes the requirement that the batch file name on the client's
        /// computer must completely match the submission name.
        /// </summary>
        /// <returns></returns>
        string SubmissionName { get; set; }

        /// <summary>
        /// Gets or sets the name that the submission file will have when 
        /// it's sent to the payment processing server.
        /// </summary>
        /// <remarks>
        /// It's hard to say why the merchant will want to use this, but it does add flexibility.
        /// </remarks>
        string SFTPFilename { get; set; }
    }
}

