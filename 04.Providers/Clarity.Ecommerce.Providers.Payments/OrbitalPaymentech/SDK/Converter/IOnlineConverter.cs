using System;
using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;
using System.Xml;
using log4net;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;

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
    /// <p>Title: </p>
    /// 
    /// <p>Description: </p>
    /// 
    /// <p>Copyright: Copyright (c) 2018, Chase Paymentech Solutions, LLC. All rights
    /// reserved</p>
    /// 
    /// @author Rameshkumar Bhaskharanan
    /// @version 1.0
	/// 
	/// </summary>
	public interface IOnlineConverter : IConverter
    {
		bool MessageMatches( string requestFormat, string responseFormat );
    }
}
