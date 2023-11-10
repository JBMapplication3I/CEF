using System;
using System.Collections.Generic;
using JPMC.MSDK.Common;
using JPMC.MSDK.Converter;

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
	/// Summary description for IRequestImpl.
	/// </summary>
	public interface IRequestImpl : IRequest
	{
        bool HasField( string name );

        bool IsBatch { get; }

        List<string> GetArrayValues( string fieldName );

        void SetDefaultValues();

        SDKMetrics Metrics { get; set; }
        Format GetFormat( string name );
        bool UsesFormat( string usedName );
        byte[] GetBinaryField( string fieldName );

        void SetField( string fieldName, string value, bool setHidden );

        void SetSkipDuplicateCheck( bool skip );
    }
}
