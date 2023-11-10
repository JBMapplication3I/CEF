using System;
using JPMC.MSDK.Framework;
using JPMC.MSDK.Filer;
using JPMC.MSDK.Configurator;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570

namespace JPMC.MSDK.Comm
{

///
///
///
/// Title: Communication Manager Interface
///
/// Description: This interface specifies the public interaction with the
/// CommManager class
///
/// Copyright (c)2018, Paymentech, LLC. All rights reserved
///
/// Company: J. P. Morgan
///
/// @author Frank McCanna
/// @version 1.0
///

/// <summary>
/// The only purpose for this class is for building wrappers during unit testing of the layer above 
/// the CommManager. 
/// </summary>
public interface ICommManager
{
	/// <summary>
	/// This method will block after sending "outmsg.Data" to the client
	/// application and wait for a response.  The response data is returned in
	/// the data element of the CommArgs return value.
	/// </summary>
	/// <returns>contains response from the target application.</returns>
	CommArgs CompleteTransaction( CommArgs outmsg );

	/// <summary>
	/// This method must be called when it is time to close all comm objects
	/// </summary>
	void Close();

	/// <summary>
	/// This close is for PNS Uploads. Both uploads using the PNSUpload module (intranet) and uploads 
	/// using the HTTPSUpload module (internet)
	/// </summary>
	/// <param name="whichModule"></param>
	/// <param name="batchUID"></param>
	void Close( string batchUID, ConfigurationData cdata );

    /// <summary>
    /// This close is for a single comm object
    /// </summary>
    /// <param name="cdata"></param>
    void Close( ConfigurationData cdata );
}
}
