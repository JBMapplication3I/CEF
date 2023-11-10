using System;
using System.IO;
using JPMC.MSDK.Framework;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for FileReader.
    /// </summary>
    public class FileReader : IFileReader
    {
        private IFiler filer;
        private IDispatcherFactory factory;
        private string password;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="submissionName"></param>
        public FileReader( string filename, string password, string submissionName ) :
            this( filename, password, submissionName, new DispatcherFactory() )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="password"></param>
        /// <param name="submissionName"></param>
        /// <param name="factory"></param>
        public FileReader( string filename, string password, string submissionName, IDispatcherFactory factory )
        {
            this.FilePath = filename;
            this.password = password;
            this.SubmissionName = submissionName;
            this.factory = factory;

            filer = factory.MakeAESFiler();
            filer.OpenFile( filename );
        }

        #region IFileReader Members

        /// <summary>
        /// Gets or sets the set of bytes that terminates a file. 
        /// In the case of TCP Batch, it would be EOFEOFEOF.
        /// </summary>
        public byte[] FileTerminator { get; set; }

        /// <summary>
        /// Gets or sets a flag specifying if the filename
        /// of the file being read is already formatted for
        /// sending to the SFTP server.
        /// </summary>
        public bool IsSFTPFileNameSet { get; set; }

        /// <summary>
        /// The file being read.
        /// </summary>
        public System.IO.FileInfo File => new FileInfo( FilePath );

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        public OrderSeparator OrderSeparator => this.filer.OrderSeparator;

        /// <summary>
        /// The file header read from the beginning of the file.
        /// </summary>
        public FileHeader FileHeader => this.filer.FileHeader;

        /// <summary>
        /// Determines if there are more records in the file to read.
        /// </summary>
        /// <returns>True if there are more records in the file to read, and false if there are no more.</returns>
        public bool HasNextRecord
        {
            get
            {
                if ( filer.FileHeader.LineLength > 0 )
                {
                    if ( filer.Position >= filer.Length - filer.FileHeader.LineLength * 2 )
                    {
                        var pos = filer.Position;
                        var bytes = filer.ReadRecord();
                        filer.Seek( pos, SeekOrigin.Begin );
                        if ( Utils.ByteArrayToString( bytes ).StartsWith( "Order" ) )
                        {
                            return false;
                        }
                    }
                    return filer.Position < filer.Length - filer.FileHeader.LineLength;
                }
                else
                {
                    return !filer.EOF;
                }
            }
        }

        /// <summary>
        /// Resets the reader back to the beginning of the file.
        /// </summary>
        public void ResetReader()
        {
            if ( filer.Open )
            {
                filer.Seek( 0, SeekOrigin.Begin );
            }
        }

        /// <summary>
        /// Returns the next record from the file sequentially. The record read from the file will be
        /// decrypted automatically.
        /// </summary>
        /// <returns></returns>
        public byte[] GetNextRecord()
        {
            byte[] encrypted = null;
            do
            {
                if ( filer.EOF )
                {
                    return null;
                }
                encrypted = filer.ReadRecord();
            }
            while ( Utils.ByteArrayToString( encrypted ).StartsWith( OrderSeparator.PREFIX ) );

            try
            {
                var line = filer.Decrypt( encrypted, password );
                return Utils.StringToByteArray( line );
            }
            catch ( Exception ex )
            {
                var msg = "Failure decrypting next record.";
                factory.Logger.Error( msg, ex );
                throw new FilerException( Error.DecryptionFailed, msg, ex );
            }
        }

        /// <summary>
        /// Closes the file file.
        /// </summary>
        public void Close()
        {
            filer.CloseFile();
        }

        /// <summary>
        /// Returns the name of the file being read. This name does not include
        /// the file extension.
        /// </summary>
        public string FileName => JPMC.MSDK.Common.FileName.GetName( FilePath );

        /// <summary>
        /// Returns the fully-qualified path to the file being read.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Sets or gets the total number of records in the file.
        /// </summary>
        public long TotalRecords { get; set; }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        public void DeleteFile()
        {
            var mgr = factory.MakeFileManager();
            mgr.Delete( FilePath );
        }

        /// <summary>
        /// The CommManager will use this to get the real name of the submission
        ///	in order to build the proper filename as it should be on the server.
        /// This removes the requirement that the batch file name on the client's
        /// computer must completely match the submission name.
        /// </summary>
        /// <returns></returns>
        public string SubmissionName { get; set; }

        /// <summary>
        /// Gets or sets the name that the submission file will have when 
        /// it's sent to the payment processing server.
        /// </summary>
        /// <remarks>
        /// It's hard to say why the merchant will want to use this, but it does add flexibility.
        /// </remarks>
        public string SFTPFilename { get; set; }

        #endregion
    }
}

