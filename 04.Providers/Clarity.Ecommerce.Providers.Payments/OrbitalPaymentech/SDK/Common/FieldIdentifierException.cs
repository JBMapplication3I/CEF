#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Runtime.InteropServices;
using JPMC.MSDK.Converter;
using JPMC.MSDK.Filer;

namespace JPMC.MSDK
{
    /// <summary>
    /// Represents errors that occur during the processing of
    /// <see cref="IResponse"/>
    /// and <see cref="IResponseDescriptor"/> tasks.
    /// </summary>
    /// <remarks>
    /// The error code will identify the type of error that has occurred,
    /// which you can use in your error handling to determine how to proceed.
    /// The message gives more detail about the exception.
    /// </remarks>
    [System.Serializable]
    [ComVisible( true )]
    public class FieldIdentifierException : Exception
    {
        private Error error = Error.UnknownError;

        /// <summary>
        /// Constructs a ResponseException passing the desired error code.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        public FieldIdentifierException(Error error)
        {
            this.error = error;
        }

        public FieldIdentifierException( FilerException ex ) : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        public FieldIdentifierException( ConverterException ex )
            : base( ex.Message, ex )
        {
            this.error = ex.ErrorCode;
        }

        /// <summary>
        /// Constructs a ResponseException passing the desired error code and message.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        /// <param name="message">A descriptive message for the exception.</param>
        public FieldIdentifierException(Error error, string message) : base(message)
        {
            this.error = error;
        }

        /// <summary>
        /// Constructs a ResponseException passing the desired error code, a message, and the root exception.
        /// </summary>
        /// <param name="error">The error type for the exception.</param>
        /// <param name="message">A descriptive message for the exception.</param>
        /// <param name="exception">The exception that caused this exception.</param>
        public FieldIdentifierException( Error error, string message, Exception exception )
            : base( message, exception )
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
