using JPMC.MSDK.Filer;
using System;
using System.Runtime.InteropServices;

namespace JPMC.MSDK
{
	/// <summary>
	/// Represents errors that occur during the processing of 
	/// <see cref="ISubmissionDescriptor"/> tasks.
	/// </summary>
	/// <remarks>
	/// The error code will identify the type of error that has occurred,
	/// which you can use in your error handling to determine how to proceed.
	/// The message gives more detail about the exception.
	/// </remarks>
	[System.Serializable]
	[ComVisible( true )]
	public class SubmissionException : Exception
	{
		private Error error = Error.UnknownError;

		/// <summary>
		/// Constructs a SubmissionException passing the desired error code.
		/// </summary>
		/// <param name="error">The error type for the exception.</param>
		public SubmissionException(Error error)
		{
			this.error = error;
		}

		/// <summary>
		/// Constructs a SubmissionException passing the desired error code and message.
		/// </summary>
		/// <param name="error">The error type for the exception.</param>
		/// <param name="message">A descriptive message for the exception.</param>
		public SubmissionException(Error error, string message) : base(message)
		{
			this.error = error;
		}

		/// <summary>
		/// Constructs a SubmissionException passing the desired error code, a message, and the root exception.
		/// </summary>
		/// <param name="error">The error type for the exception.</param>
		/// <param name="message">A descriptive message for the exception.</param>
		/// <param name="exception">The exception that caused this exception.</param>
		public SubmissionException(Error error, string message, Exception exception) : base(message, exception)
		{
			this.error = error;
		}

		/// <summary>
		/// An enum that specifies the type of error that this exception 
		/// represents. 
		/// </summary>
		public Error ErrorCode => this.error;
    }
}
