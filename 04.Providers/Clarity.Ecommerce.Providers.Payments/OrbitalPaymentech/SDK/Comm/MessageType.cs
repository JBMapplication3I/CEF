namespace JPMC.MSDK
{
///
///
/// Title: Message Type
///
/// Description: This is the type of file format that is sent to the payment servers
///
/// Copyright (c)2018, Paymentech, LLC. All rights reserved
/// reserved
///
/// Company: J. P. Morgan
///
/// @author Frank McCanna
/// @version 1.0
///

	/// <summary>
	/// This enum specifies whether the message is in PNS
	/// format or Salem format
	/// </summary>
	public enum MessageType
	{
		/// <summary>
		/// PNS message
		/// </summary>
		PNS,

		/// <summary>
		/// Salem message
		/// </summary>
		SLM
	}
}
