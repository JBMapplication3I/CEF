using System;
using System.Runtime.Serialization;
namespace JPMC.MSDK.Comm
{

/// 
/// 
/// 
/// Title: Communication Exception
///
/// Description: Exceptions that occur in Communications code
///
/// Copyright (c)2018, Paymentech, LLC. All rights reserved
///
/// Company: J. P. Morgan
///
/// @author Frank McCanna
/// @version 2.0
/// 
/// <summary>
/// Represents errors that occur during communications processing.
/// </summary>
/// <remarks>
/// The error code will identify the type of error that has occurred,
/// which you can use in your error handling to determine how to proceed.
/// The message gives more detail about the exception.
/// </remarks>
[Serializable]
public class CommException : Exception
{
    [NonSerializedAttribute]
	private Error errorCode = Error.UnknownError;
	
	/// <summary>
	/// External errors can occur within several domains where
	/// the values can overlap so we must identify which
	/// domain the error value comes from
	/// </summary>
	public enum ErrorDomain
	{
		/// <summary>
		/// 
		/// </summary>
		CommunicationProtocol,
		/// <summary>
		/// 
		/// </summary>
		OperatingSystem,
		/// <summary>
		/// 
		/// </summary>
		RemoteApplication
	}
	
	/// <summary>
	/// Constructs a CommException passing the desired error code.
	/// </summary>
	/// <param name="error">The error type for the exception.</param>
	public CommException( Error error )
	{
		errorCode = error;
	}

	/// <summary>
	/// Constructs a CommException passing the desired error code and message.
	/// </summary>
	/// <param name="error">The error type for the exception.</param>
	/// <param name="message">A descriptive message for the exception.</param>
	public CommException( Error error, string message ) : base ( message )
	{
		errorCode = error;
	}

	/// <summary>
	/// Constructs a CommException passing the desired error code, a message, and the root exception.
	/// </summary>
	/// <param name="error">The error type for the exception.</param>
	/// <param name="message">A descriptive message for the exception.</param>
	/// <param name="exception">The exception that caused this exception.</param>
	public CommException( Error error, string message, Exception exception )
		: base ( message, exception )
	{
		errorCode = error;
	}

	/// <summary>
	/// An enum that specifies the type of error that this exception 
	/// represents. 
	/// </summary>
	public Error ErrorCode 
	{ 
		get => errorCode;
        set => errorCode = value;
    }

	/// <summary>
	/// The HTTP status code
	/// </summary>
	public int StatusCode { get; set; }

    /// <summary>
    /// Error in WebException 
    /// </summary>
    public int WindowsError { get; set; }

	/// <summary>
	/// The Error-Code from the HTTP mime header
	/// </summary>
	public int ExternalError { get; set; }

	/// <summary>
	/// what type of entity generated the error
	/// </summary>
	public ErrorDomain ExternalErrorDomain { get; set; }

}
}
