using System;
using System.Runtime.InteropServices;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Framework;

namespace JPMC.MSDK
{
	/// <summary>
	/// Provides the API for creating and manipulating batch submission files.
	/// </summary>
	/// <remarks>
	/// This class wraps the encrypted submission files and provides the 
	/// methods and properties necessary to create submissions and to add
	/// orders them. 
	/// 
	/// This class provides write-only access to the batch file. The 
	/// <see cref="JPMC.MSDK.IResponseDescriptor">
	/// ResponseDescriptor</see> is used to read submission and response files.
	/// </remarks>
	[ComVisible( true )]
	public interface ISubmissionDescriptor
	{
		/// <summary>
		/// The ProtocolManager that describes the communication settings 
		/// for the submision.
		/// </summary>
        ConfigurationData Config { get; }

		/// <summary>
		/// Gets the header record for the submission.
		/// </summary>
		IRequest Header { get; }

		/// <summary>
		/// Returns true if the submission's batch has been closed, otherwise 
		/// it returns false. 
		/// </summary>
		/// <remarks>
		/// This method does not test if the file itself is open. Instead it 
		/// only tests if the batch has been closed and the submission is 
		/// reading for sending. Use the 
		/// <see cref="JPMC.MSDK.ISubmissionDescriptor.Open">
		/// Open</see> property to tell if the file is open.
		/// </remarks>
		bool BatchClosed { get; }
		/// <summary>
		/// Gets a value indicating whether the submission is still open.
		/// </summary>
		/// <remarks>
		/// A submission is "Closed" when its totals records and trailer record
		/// have been written to it. Once that has happened, no more records
		/// are supposed to be written to the batch. 
		/// 
		/// The submission is marked as Closed when the 
		/// <see cref="JPMC.MSDK.ISubmissionDescriptor.CloseBatch">
		/// CloseBatch</see> method is called.
		/// </remarks>
		/// <value>True if CloseBatch was called on this submission, false 
		/// if it hasn't.</value>
		bool Open { get; }
		/// <summary>
		/// Retuns the absolute path to the submission file that this class wraps.
		/// </summary>
		/// <value>The absolute path to the submission file.</value>
		string FileName { get; }
		/// <summary>
		/// The password used to encrypt the records when they are written to the 
		/// file.
		/// </summary>
		/// <value>A string representing the encryption password.</value>
		string Password { get; }
		/// <summary>
		/// A symbolic name for the submission.
		/// </summary>
		/// <remarks>
		/// This name is specified by the client when creating the submission, 
		/// and is stored in the submission's header record. It will also be 
		/// in the header record of the response file that belongs to this 
		/// submission. This allows the client to compare the submission name
		/// to the response's name in order to match responses to submissions.
		/// The client can compare this property to the 
		/// <see cref="JPMC.MSDK.IResponseDescriptor.Name">
		/// IResponseDescriptor.Name</see> property to do the matching.
		/// 
		/// The value of this property is limited to eight characters in 
		/// length. Names can be shorter than this limit, but cannot be 
		/// longer. 
		/// </remarks>
		/// <value>An 8-character or less string that uniquely identifies this submission.</value>
		string Name { get; }

		/// <summary>
		/// Add a new Request to the submission. It will be stored in the file
		/// in the order that it's added.
		/// </summary>
		/// <param name="request">The request to add to the batch.</param>
		void Add(IRequest request);

		/// <summary>
		/// Returns the number of order records in the submission.
		/// </summary>
		/// <value>The number of orders added to the submission file.</value>
		long OrderCount { get; }

		/// <summary>
		/// Closes the file, but does not complete the batch. 
		/// </summary>
		/// <remarks>
		/// Close() differs from CloseBatch() in that CloseBatch() writes 
		/// the trailer records to the file and prepares the batch for 
		/// sending. Close() simply closes the stream to the file so that
		/// other objects can access it. 
		/// </remarks>
		void Close();

		/// <summary>
		/// Writes the trailer records and marks the Submission as closed. 
		/// </summary>
		/// <remarks>
		/// At this point, no more Requests can be added to it. Also, the
		/// trailer will be built at this time. There will be some fields in the trailer that contain a sum
		/// of a specific field in the various Order Requests that have been added to the Submission. The
		/// client has the option of calculating these sums himself and setting them in his Trailer Request
		/// object. If he chose not to set them, this method will set them for him.
		/// </remarks>
		void CloseBatch();

		/// <summary>
		/// Creates a new submission file and stores the filename. 
		/// </summary>
		/// <remarks>
		/// The submission file is created in the subdirectory specified in 
		/// the MSDKConfig.xml file by the PropertyList/BatchDirectories/outgoing 
		/// tag.
		///  
		/// Once this method has been called, closeBatch must be called to 
		/// close the file.
		/// </remarks>
		/// <param name="header">The Request object that defines the header record.</param>
		/// <returns>True if the batch was created successfully, false if not.</returns>
		void CreateBatch( IRequest header );

		/// <summary>
		/// Creates a new submission file and stores the filename. 
		/// </summary>
		/// <remarks>
		/// The submission file is created in the subdirectory specified in 
		/// the MSDKConfig.xml file by the PropertyList/BatchDirectories/outgoing 
		/// tag.
		///  
		/// Once this method has been called, closeBatch must be called to 
		/// close the file.
		/// </remarks>
		void CreateBatch();

		/// <summary>
		/// Return the total number of records added to the submission.
		/// </summary>
		long TotalRecords { get; }

		/// <summary>
		/// Return the total of all amounts for the submission.
		/// </summary>
		long TotalAmounts { get; }

		/// <summary>
		/// Return  the total of all sales amounts for the submission.
		/// </summary>
		long TotalSalesAmounts { get; }

		/// <summary>
		/// Return the refund amount for the given order.
		/// </summary>
		long TotalRefundAmounts { get; }

		/// <summary>
		/// Gets or sets the name that the submission file will have when 
		/// it's sent to the payment processing server.
		/// </summary>
		/// <remarks>
		/// When you receive an error response (by calling 
		/// <see cref="JPMC.MSDK.IDispatcher.ReceiveNetConnectError()"/>
		/// ), the error response will specify the filename of the submission
		/// that has the error. But the filename it gives is for the 
		/// file that is on the server. You can use this property to match 
		/// the errored file to the appropriate submission. 
		/// </remarks>
		string SFTPFileName { get; set; }

        /// <summary>
        /// Get and set the metrics tracker for this submission
        /// </summary>
        SDKMetrics Metrics { get; set; }

	}
}
