using System;
using System.Net;
using System.Threading;
using JPMC.MSDK.Configurator;
using JPMC.MSDK.Common;
using JPMC.MSDK.Framework;
using log4net;

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
/// Title: HTTPS Upload Communication Module
///
/// Description: This communication module implements the
/// "HTTPSConnect" application protocol but adds the PNS upload functionality
///
/// Copyright (c)2018, Paymentech, LLC. All rights reserved
///
/// Company: J. P. Morgan
///
/// @author Frank McCanna
/// @version 2.0
///

/// <summary>
/// This communication module implements HTTPS PNS upload
/// </summary>
public sealed class HTTPSUpload : HTTPSBase
{
	/// <summary>
	/// maps saved cookies with PNS Upload UID
	/// </summary>
	private SafeDictionary<string, string[] > cookieBoxes = 
		new SafeDictionary<string, string[] >();

	/// <summary>
	/// Constructor - Sets private fields
	/// </summary>
	/// <param name="configurator">contents of HTTPSConnectConfig.xml</param>
	public HTTPSUpload( ConfigurationData cdata ) 
		: base( cdata ) // really part of the HTTPSConnect module
	{
	}
 
	/// <summary>
	/// This method implements the completeTransaction method 
	/// which is the main public method for this class
	/// </summary>
	/// <param name="request">object with request to send, and 
	/// options</param>
	/// <returns>object with response data.</returns>
	public override CommArgs CompleteTransactionImpl( CommArgs request )
	{
		CommArgs response = null;
		var firstTry = true;	// set to false on second attempt	
		byte[] resp = null;
		HttpWebObjectWrapper httpObject = null;
		var cvals = request.ControlValues;

		var uploadID = cvals.PNSBatchUID +
			Thread.CurrentThread.ManagedThreadId;

        if ( ! cvals.GetBoolValue("IsPNSBatchHeader")
            && ! IsActiveBatch( uploadID ) )
        {
            throw new CommException( Error.BatchNotOpen );
        }

		// This loop facilitates failover by allowing a switch to 
		// the failover host and port		
		for ( var i = 0; i < 2; i++ )
		{
			// First connect and send the request
			try
			{
				Logger.Debug( "Sending " +
					request.GetDataLength() + " bytes" );

				httpObject = DoRequest( request );
			}
			catch ( Exception ex )
			{
				ClientFailure( httpObject );

				// if we only tried once, try again
				if ( cvals.GetBoolValue( "IsPNSBatchHeader" ) && firstTry 
					&& FailoverHost != null
					&& FailoverPort != 0 )
				{
					Logger.Warn(
					"Retrying using alternate host" );

					firstTry = false;
					continue;
				}
				// just throw "as is"
				else if ( ex is CommException )
				{
					throw (CommException) ex;
				}
				// wrap it in a CommException
				else
				{
					throw new CommException(
						Error.UnknownFailure,
						ex.Message, ex );
				}
			}

			// if we got here we succeeded in sending the request
			break;
		}

		if ( httpObject != null )
		{
			// Now get the response
			// we don't failover because failing over a "read" could cause
			// a dup
			try
			{
				resp = DoResponse( httpObject );
				if ( resp != null )
				{
					Logger.Debug( "Received " +
						resp.Length + " bytes" );
				}
				else
				{
					Logger.Debug( "DoResponse returned null" );
				}
			}
			catch ( CommException )
			{
				ClientFailure( httpObject );
				throw;
			}
			// if other exception then wrap it in a CommException
			catch ( Exception ex )
			{
				ClientFailure( httpObject );
				throw new CommException( Error.UnknownFailure,
					ex.Message, ex );
			}
		}

		if ( resp != null )
		{
			// if this is the response we got from the first transaction 
			// (i.e. sending the header)
			if ( cvals.GetBoolValue( "IsPNSBatchHeader" ) )
			{
				// get the cookies sent back to us from the server
				var setCookieValues = httpObject.ResponseHeaders.GetValues(
					"Set-Cookie" );

                if ( setCookieValues != null )
                { 
                    cookieBoxes[uploadID] = setCookieValues;
                }
                else
                {
                    // there will only be no cookies if we are in test
                    // put an empty array in there to indicate it is still an active batch
                    cookieBoxes[uploadID] = new string[0];
                }
			}
		}

		// clean up if this is the trailer
		if ( cvals.GetBoolValue( "IsPNSBatchTrailer" ) )
		{
            cookieBoxes.Remove(uploadID);
		}

		response = new CommArgs( resp );

		ClientSuccess( httpObject );

        httpObject.Close();
      
		return response;
	}

	/// <summary>
	/// Put all the required fields in the mime header
	/// </summary>
	/// <param name="httpObject">object that will send the request</param>
	/// <param name="pman">security info</param>
	/// <param name="contentLength">length of data</param>
	public override void PopulateMimeHeader( HttpWebObjectWrapper httpObject,
		ConfigurationData config, TransactionControlValues control, int contentLength )
	{
		var uploadID = control.PNSBatchUID +
			Thread.CurrentThread.ManagedThreadId;

		httpObject.RequestHeaders.Set( MimeVersionHeader, 
			MimeVersionDefault );

        httpObject.ContentType = control["ContentType"];

		httpObject.RequestHeaders.Add( "Stateless-Transaction", "false" );

        AddMimeHeader(control, "AuthMid", httpObject, "Auth-MID" );
        AddMimeHeader(control, "AuthTid", httpObject, "Auth-TID");
        AddMimeHeader(control, "ContentTransferEncoding", httpObject, "Content-Transfer-Encoding");
        AddMimeHeader(control, "RequestNumber", httpObject, "Request-Number");
        AddMimeHeader(control, "DocumentType", httpObject, "Document-type");
        AddMimeHeader(control, "Interface-Version", httpObject, "Interface-Version");
       
        // these two are in the ConfigurationData
        httpObject.RequestHeaders.Set("Auth-User", config["UserName"]);
        httpObject.RequestHeaders.Set("Auth-Password", config["UserPassword"]);

		if ( control.GetBoolValue( "IsPNSBatchHeader" ) )
		{
			httpObject.RequestHeaders.Add( "Header-Record", "true" );
		}
		// If this isn't the first transaction, then use specs from header and
		// add the cookies we got with the response from the first transaction
        // don't do it if the cookie is already there
		else if ( httpObject.RequestHeaders.Get("Cookie") == null )
		{
            httpObject.RequestHeaders.Remove( "Header-Record");

            var cookies = cookieBoxes[uploadID];

			if ( cookies != null )
			{
				for ( var i=0; i < cookies.Length; i++ )
				{
					httpObject.RequestHeaders.Add( HttpRequestHeader.Cookie, cookies[ i ] );
				}
			}
		}
	}

    public void Close( string batchID )
    {
        cookieBoxes.Remove(batchID);
    }
	/// <summary>
	/// See if we are uploading an active batch for this batch ID
	/// </summary>
	/// <param name="batchID"></param>
	/// <returns></returns>
	public bool IsActiveBatch( string batchID )
	{
		return cookieBoxes[ batchID ] != null;
	}

    protected override string ModuleName => "HTTPSUpload";
}
}  // end of namespace

