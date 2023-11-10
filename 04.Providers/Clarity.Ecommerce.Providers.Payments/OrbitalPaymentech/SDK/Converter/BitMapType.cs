using System;
using System.Collections.Generic;
using System.Text;

namespace JPMC.MSDK.Converter
{
	/// <summary>
	/// An enum that specifies all the possible values for the FormatType 
	/// attribute for a Format.
	/// </summary>
	public enum BitMapType
	{
		/// <summary>
		/// The default. The primary bitmaps at the start of a PNS record must be in binary.
		/// </summary>
		Binary,
		/// <summary>
		/// Some bitmaps that appear further into the PNS record are sent as hex strings.
		/// </summary>
		Hex
	}
}
