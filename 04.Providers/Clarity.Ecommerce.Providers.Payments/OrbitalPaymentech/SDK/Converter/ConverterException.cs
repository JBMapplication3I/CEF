using System;

namespace JPMC.MSDK.Converter
{
	/// <p>Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
	/// reserved</p>
	///
	/// @author Rameshkumar Bhaskharan
	/// @version 1.0
	///
	/// <summary>
	/// Summary description for ConverterException.
	/// </summary>
	public class ConverterException : Exception
	{
		private Error error = Error.UnknownError;

        /// <summary>
		/// Constructor
		/// </summary>
		/// <param name="error"></param>
		/// <param name="message"></param>
		public ConverterException(Error error, string message) : base(message)
		{
			this.error = error;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="error"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public ConverterException(Error error, string message, Exception exception) : base(message, exception)
		{
			this.error = error;
		}
		/// <summary>
		/// Error code
		/// </summary>
		public Error ErrorCode => this.error;
    }
}
