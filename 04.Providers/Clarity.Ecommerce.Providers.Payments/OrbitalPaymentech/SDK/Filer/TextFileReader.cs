using System;
using System.IO;
using JPMC.MSDK.Framework;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for TextFileReader.
    /// </summary>
    public class TextFileReader : IFileReader
    {
        private IDispatcherFactory factory;
        private string filename;
        private IStreamWrapper stream;
        private string name;
        private long totalRecords;
        private bool isSFTPFileNameSet;
        private string sftpFileName;
        private byte[] fileTerminator;
        private FileHeader header = new FileHeader();

        /// <summary>
        /// The constructor.
        /// </summary>
        /// <param name="filename"></param>
        public TextFileReader( string filename )
            : this( filename, new DispatcherFactory() )
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        /// <param name="factory"></param>
        public TextFileReader( string filename, IDispatcherFactory factory )
        {
            this.filename = filename;
            this.factory = factory;
            this.header.LineLength = 121;

            var fname = new FileName( filename );
            this.name = fname.NameAndExtension;

            stream = factory.MakeStreamWrapper( filename, false, false );
        }

        #region IFileReader Members

        /// <summary>
        /// Gets or sets the set of bytes that terminates a file. 
        /// In the case of TCP Batch, it would be EOFEOFEOF.
        /// </summary>
        public byte[] FileTerminator
        {
            get => fileTerminator;
            set => fileTerminator = value;
        }

        /// <summary>
        /// Gets or sets a flag specifying if the filename
        /// of the file being read is already formatted for
        /// sending to the SFTP server.
        /// </summary>
        public bool IsSFTPFileNameSet
        {
            get => this.isSFTPFileNameSet;
            set => this.isSFTPFileNameSet = value;
        }

        /// <summary>
        /// The file being read.
        /// </summary>
        public FileInfo File => new FileInfo( filename );

        /// <summary>
        /// The file header read from the beginning of the file.
        /// </summary>
        public FileHeader FileHeader => header;

        /// <summary>
        /// Determines if there are more records in the file to read.
        /// </summary>
        /// <returns>True if there are more records in the file to read, and false if there are no more.</returns>
        public bool HasNextRecord => stream.Position < stream.Length;

        /// <summary>
        /// Resets the reader back to the beginning of the file.
        /// </summary>
        public void ResetReader()
        {
            stream.Seek( 0L, SeekOrigin.Begin );
        }

        /// <summary>
        /// The record that separates one order from another in the file.
        /// </summary>
        public OrderSeparator OrderSeparator => null;

        /// <summary>
        /// Returns the next record from the file sequentially. The record read from the file will be
        /// decrypted automatically.
        /// </summary>
        /// <returns></returns>
        public byte[] GetNextRecord()
        {
            var record = stream.ReadBytes( 121 );
            return record;
        }

        /// <summary>
        /// Closes the file file.
        /// </summary>
        public void Close()
        {
            stream.Close();
        }

        /// <summary>
        /// Returns the name of the file being read. This name does not include
        /// the file extension.
        /// </summary>
        public string FileName
        {
            get
            {
                if ( this.isSFTPFileNameSet )
                    return name;
                else
                    return JPMC.MSDK.Common.FileName.GetName( name );
            }
        }

        /// <summary>
        /// Returns the fully-qualified path to the file being read.
        /// </summary>
        public string FilePath
        {
            get => filename;
            set { }
        }

        /// <summary>
        /// Sets or gets the total number of records in the file.
        /// </summary>
        public long TotalRecords
        {
            get => totalRecords;
            set => totalRecords = value;
        }

        /// <summary>
        /// Deletes the file.
        /// </summary>
        public void DeleteFile()
        {
            stream.Close();
            var info = new FileInfo( filename );
            info.Delete();
        }

        /// <summary>
        /// The CommManager will use this to get the real name of the submission
        ///	in order to build the proper filename as it should be on the server.
        /// This removes the requirement that the batch file name on the client's
        /// computer must completely match the submission name.
        /// </summary>
        /// <returns></returns>
        public string SubmissionName
        {
            get => null;
            set { }
        }

        /// <summary>
        /// Gets or sets the name that the submission file will have when 
        /// it's sent to the payment processing server.
        /// </summary>
        /// <remarks>
        /// It's hard to say why the merchant will want to use this, but it does add flexibility.
        /// </remarks>
        public string SFTPFilename
        {
            get => sftpFileName;
            set => sftpFileName = value;
        }
        #endregion
    }
}

