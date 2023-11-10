using System;
using System.Collections.Generic;
using System.Xml;
using log4net;
using JPMC.MSDK.Common;
using JPMC.MSDK.Framework;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Converter
{
	/// <summary>
	/// 
	/// </summary>
    public class ConverterArgs
    {
	    /// <summary>
		/// enum for different amount types
	    /// </summary>
	    public enum AmtType
	    {
			/// <summary>
			/// 
			/// </summary>
		    Sales,
			/// <summary>
			/// 
			/// </summary>
		    Refund,
			/// <summary>
			/// 
			/// </summary>
		    None
	    }

		/// <summary>
		/// Default constructor
		/// </summary>
	    public ConverterArgs()
	    {
            CommModule = MSDK.CommModule.Unknown;
            OrderType = AmtType.None;
	    }

		/// <summary>
		/// 
		/// </summary>
        public byte[] ReqByteArray { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public byte[] MaskedReqByteArray { get; set; }

	    /// <summary>
		/// Return the comm type
	    /// </summary>

        public CommModule CommModule { get; set; }

        public IResponse ResponseData { get; set; }

		/// <summary>
		/// Return the file terminator
		/// </summary>
        public string FileTerminator { get; set; }

		/// <summary>
		/// Return the format
		/// </summary>
        public string Format { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public string FieldDelimiter { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public string RecordDelimiter { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public AmtType OrderType { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public string StrData { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public int IntData { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public long LongData { get; set; }

		/// <summary>
	    /// Return the request in text form NOTE: This is for Batch Files only!
	    /// Online and PNS must use getReqByteArray().
		/// </summary>
        public string Request { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public string MaskedRequest { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public string Response { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public string MaskedResponse { get; set; }

		/// <summary>
		/// 
		/// </summary>
        public bool SkipWrite { get; set; }
    }
}
