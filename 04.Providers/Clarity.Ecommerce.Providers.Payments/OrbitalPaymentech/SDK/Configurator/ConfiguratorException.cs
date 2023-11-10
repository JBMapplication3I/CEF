using System;

namespace JPMC.MSDK.Configurator
{
    /// <summary>
    /// Summary description for ConfiguratorException.
    /// </summary>
    /// <p>Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
    /// reserved</p>
    ///
    /// @author Rameshkumar Bhaskharan
    /// @version 1.0
    ///
    public class ConfiguratorException : Exception
	{
		private Error error = Error.UnknownError;
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="error"></param>
		public ConfiguratorException(Error error)
		{
			this.error = error;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="error"></param>
		/// <param name="message"></param>
		public ConfiguratorException(Error error, string message) : base(message)
		{
			this.error = error;
		}
		/// <summary>
		/// Constructor
		/// </summary>
		/// <param name="error"></param>
		/// <param name="message"></param>
		/// <param name="exception"></param>
		public ConfiguratorException(Error error, string message, Exception exception) : base(message, exception)
		{
			this.error = error;
		}
		/// <summary>
		/// Return error code
		/// </summary>
		public Error ErrorCode => this.error;
    }
}
