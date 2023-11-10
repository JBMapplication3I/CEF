using System;

namespace JPMC.MSDK.Framework
{
	/// <summary>
	/// Summary description for BatchFileHeader.
	/// </summary>
	public struct BatchFileHeader
	{
        /// <summary>
        /// The encryption iteration count.
        /// </summary>
		public int encryptionIterationCount;
        /// <summary>
        /// The salt value used for encryption.
        /// </summary>
		public byte[] encryptionSalt;
        /// <summary>
        /// Specifies if this file is a submission or response.
        /// </summary>
		public BatchFileType fileType;
        /// <summary>
        /// The current position in the file.
        /// </summary>
		public long filePointer;
        /// <summary>
        /// Specifies if the batch has been closed, 
        /// and the trailers have been written.
        /// </summary>
		public int batchClosed;
        /// <summary>
        /// The totals.
        /// </summary>
		public long totals;
        /// <summary>
        /// The total number of orders.
        /// </summary>
		public long orders;
        /// <summary>
        /// The total number of records.
        /// </summary>
		public long records;
        /// <summary>
        /// The total amount of refunds.
        /// </summary>
		public long refunds;
        /// <summary>
        /// The total amount of sales.
        /// </summary>
		public long sales;
	}
}
