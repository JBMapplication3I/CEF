#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.InteropServices;
using JPMC.MSDK.Comm;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Converter;

namespace JPMC.MSDK
{
    /// <summary>
    /// Represents errors that occur during the processing of
    /// <see cref="IDispatcher"/> tasks.
    /// </summary>
    /// <remarks>
    /// The error code will identify the type of error that has occurred,
    /// which you can use in your error handling to determine how to proceed.
    /// The message gives more detail about the exception.
    /// </remarks>
    [System.Serializable]
    [ComVisible( true )]
    public class DispatcherException : Exception
    {
        private Error error = Error.UnknownError;
        private string hostName;
        private string port;

        /// <summary>
        /// Constructs a ResponseException passing the desired error code.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        public DispatcherException(Error error)
        {
            this.error = error;
        }

        /// <summary>
        /// Constructs a ResponseException passing the desired error code and message.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        /// <param name="message">A descriptive message for the exception.</param>
        public DispatcherException(Error error, string message) : base(message)
        {
            this.error = error;
        }

        /// <summary>
        /// Constructs a ResponseException passing the desired error code, a message, and the root exception.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        /// <param name="message">A descriptive message for the exception.</param>
        /// <param name="exception">The exception that caused this exception.</param>
        public DispatcherException(Error error, string message, Exception exception) : base(message, exception)
        {
            this.error = error;
        }

        /// <summary>
        /// Constructs a ResponseException passing the desired error code, a message, and the root exception.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        /// <param name="message">A descriptive message for the exception.</param>
        /// <param name="hostName">The name of the host that was being </param>
        /// <param name="port">The port that was  being communicated with when the exception occurred.</param>
        /// <param name="exception">The exception that caused this exception.</param>
        public DispatcherException(Error error, string message, string hostName, string port,  Exception exception) : base(message, exception)
        {
            this.error = error;
            this.hostName = hostName;
            this.port = port;
        }

        public DispatcherException( RequestException ex ) : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        public DispatcherException( ResponseException ex )
            : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        public DispatcherException( ConfiguratorException ex )
            : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        public DispatcherException( ConverterException ex )
            : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        public DispatcherException( SubmissionException ex )
            : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        public DispatcherException( CommException ex )
            : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        /// <summary>
        /// An enum that specifies the type of error that this exception
        /// represents.
        /// </summary>
        public Error ErrorCode => this.error;

        /// <summary>
        /// Gets the name of the server that the proxy had last tried to send
        /// a request to.
        /// </summary>
        public string HostName => this.hostName;

        /// <summary>
        /// Gets the port of the server that the proxy had last tried to send
        /// a request to.
        /// </summary>
        public string Port => this.port;
    }
}
