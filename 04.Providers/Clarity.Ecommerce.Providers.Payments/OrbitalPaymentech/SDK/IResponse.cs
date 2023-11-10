using System;
using System.Runtime.InteropServices;

namespace JPMC.MSDK
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
	[Guid( "65E79167-41D9-473f-891F-A2CFFDD220EB" )]
	[InterfaceType( ComInterfaceType.InterfaceIsIDispatch )]
	[ComVisible( true )]
	public interface IResponse
	{
		/// <summary>
		/// This indexer gives a convenient interface into the GetField and 
		/// SetField methods. 
		/// </summary>
		/// <remarks>
		/// This indexer only returns string values, so you will still need 
		/// GetIntField and GetLongField (and their appropriate Set methods).
		/// </remarks>
		string this[ string fieldName ] { get; }

		/// <summary>
		/// It returns a string version of the value of the specified field. 
		/// This is identical to using the indexer. 
		/// </summary>
		/// <param name="fieldName">The name of the field whose value you want 
		/// to retrieve.</param>
		/// <returns>The value of the field as a string.</returns>
		string GetField( string fieldName );

		/// <summary>
		/// Returns true if the field exists in the response, false if it does not.
		/// </summary>
		/// <param name="fieldName">The name of the field to test.</param>
		/// <returns>True if the field exists in the response, false if it does not.</returns>
		bool HasField( string fieldName );

		/// <summary>
		/// It returns a string version of the masked value of the specified field. 
		/// This is identical to using the indexer. 
		/// </summary>
		/// <param name="fieldName">The name of the field whose value you want 
		/// to retrieve.</param>
		/// <returns>The value of the field as a string.</returns>
		string GetMaskedField( string fieldName );

		/// <summary>
		/// Get the value for a specific element in an array of like fields.
		/// </summary>
		/// <param name="fieldName">The name of the field whose value you want 
		/// to retrieve.</param>
		/// <param name="index">The array index that points to the value you want.</param>
		/// <returns>The value of the field specified by both the fieldName and index.</returns>
		string GetField( string fieldName, int index );

		/// <summary>
		/// Returns an integer version of the value of the specified field.
		/// </summary>
		/// <param name="fieldName">The name of the field whose value you want 
		/// to retrieve.</param>
		/// <returns>The value of the field as an integer.</returns>
		int GetIntField( string fieldName );

		/// <summary>
		/// Get the number of identically named fields. 
		/// Used in conjunction with GetField( string, int ).
		/// </summary>
		/// <param name="fieldName">The name of the field whose item count to return.</param>
		/// <returns>The number of fields in the array.</returns>
		int GetCount( string fieldName );

		/// <summary>
		/// Returns an long integer version of the value of the specified field.
		/// </summary>
		/// <param name="fieldName">The name of the field whose value you want 
		/// to retrieve.</param>
		/// <returns>The value of the field as a long.</returns>
		long GetLongField( string fieldName );

		/// <summary>
		/// Some responses have an array of fields. This returns an array of values 
		///	for the specified field.
		///
		/// It will throw an exception if there is no array for this field.
		/// </summary>
		/// <param name="elementPath"></param>
		/// <returns></returns>
		string[] GetFieldArray( string elementPath );

		/// <summary>
		/// Returns the entire XML of the response. This may be useful in testing
		/// and debugging.
		/// </summary>
		string XML { get; }

		/// <summary>
		/// Returns the entire masked XML of the response. This may be useful in testing
		/// and debugging.
		/// </summary>
		string MaskedXML { get; }

		/// <summary>
		/// Returns the actual string record returned by the server. 
		/// </summary>
		/// <remarks>
		/// The record 
		/// will contain many fields in a format defined by a Chase  
		/// specification. It is recommended that you only use this property 
		/// when you truly need to.
		/// </remarks>
		byte[] RawData { get; }

		/// <summary>
		/// Returns true if an error occurred when processing the data returned
		/// from the server.
		/// </summary>
		/// <remarks>
		/// This typically only happens when, for some reason, the server returns
		/// corrupt data. 
		/// 
		/// When this happens, this property will be set to true and the LeftoverData 
		/// property will contain all of the data that MSDK failed to process. 
		/// </remarks>
		bool IsConversionError { get; }

		/// <summary>
		/// Returns a readable description of the cause of the conversion error.
		/// </summary>
		/// <remarks>
		/// When the IsConversionError is true, this property will be set to a 
		/// string that describes the nature of the error.
		/// </remarks>
		string ErrorDescription { get; }

		/// <summary>
		/// Returns all of the data that MSDK failed to process.
		/// </summary>
		/// <remarks>
		/// This works in conjunction with <see cref="IsConversionError"/> and will
		/// always be set to null when IsConversionError is false.
		/// </remarks>
		string LeftoverData { get; }

		/// <summary>
		/// Returns the name of the host that the request was sent to.
		/// </summary>
		string Host { get; }

		/// <summary>
		/// Returns the name of the port that the request was sent to.
		/// </summary>
		string Port { get; }

		/// <summary>
		/// Returns a string that represents the version of the MSDK SDK that
		/// was used to send the request that this response belongs to.
		/// </summary>
		string SDKVersion { get; }

		/// <summary>
		/// Returns true if the delimited file record has more fields than expected.
		/// </summary>
		/// <remarks>
		/// This typically happens when new fields are added to the report, but the 
		/// SDK has not been upgraded to support the new field. 
		/// 
		/// These new fields will appear as fields with the names ExtraFieldX, where 
		/// X is the index for the new field (ExtraField1, ExtraField2, etc.).
		/// </remarks>
		bool HasExtraFields { get; }

		/// <summary>
		/// Returns the number of extra fields found in the record.
		/// </summary>
		/// <remarks>
		/// This is greater than zero when HasExtraFields is true.
		/// 
		/// This can be used to iterate through the ExtraField list.
		/// </remarks>
		int NumExtraFields { get; }

		/// <summary>
		/// Gets an array of element paths for all fields available in the 
		/// Response.
		/// </summary>
		string[] ResponseFieldIDs { get; }

		/// <summary>
		/// Gets a string containing a list of all the FieldIdentifiers and their values.
		/// </summary>
		/// <returns></returns>
		string DumpFieldValues();
		/// <summary>
		/// Gets a string containing a list of all the FieldIdentifiers and their values, masked if necessary.
		/// </summary>
		/// <returns></returns>
		string DumpMaskedFieldValues();

        /// <summary>
        /// Gets a string containing a list of all the FieldIdentifiers.
        /// </summary>
        /// <returns></returns>
        string DumpFieldIdentifiers();

		/// <summary>
		/// Returns a name that represents the type of response you are getting. 
		/// For instance, an Orbital request can return a NewOrderResp or a QuickResp.
		/// </summary>
		string ResponseType { get; }
	}
}
