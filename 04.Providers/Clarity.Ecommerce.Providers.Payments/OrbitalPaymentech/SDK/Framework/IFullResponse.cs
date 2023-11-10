using System;
using System.Collections.Generic;
using JPMC.MSDK;
using JPMC.MSDK.Converter;
using System.Net;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Framework
{
	/// <summary>
	/// Represents a response from the server for an online request,
	/// or the processing results of an order from a batch response 
	/// file.
	/// </summary>
	/// <remarks>
	/// Access each field for the request type you specified by using
	/// the class' indexer. The available fields are based on the 
	/// the data returned from the server. Refer to the Developer's Guide
	/// for more information on what fields to expect here.
	/// 
	/// Example:
	///    <code>
	///    IResponse response = dispatcher.ReceiveResponse(request);
	///    string amount = response["Amount"];
	///    </code>
	/// </remarks>
	public interface IFullResponse : IResponse
	{
		new bool IsConversionError { get; set; }
		new string ErrorDescription { get; set; }
		new string LeftoverData { get; set; }
        new int NumExtraFields { get; set; }
        new string SDKVersion { get; set; }
        new byte[] RawData { get; set; }
        List<DataElement> DataElements { get; set; }
        WebHeaderCollection MIMEHeaders { get; set; }
		new string ResponseType { get;  set; }
	}
}
