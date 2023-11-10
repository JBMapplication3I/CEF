#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System.Threading;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;


namespace JPMC.MSDK.Comm
{
    ///
    ///
    /// Title: PNS Upload Communication Module
    ///
    /// Description: This communication module implements the "PNSUpload" application protocol
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 1.0
    ///

    public class PNSUpload : TCPBase
{
    /**
     * maps saved batch connections with PNS Batch UID
     * Batch UID is MID+TID+threadID
     */
    private SafeDictionary<string, ClientInfo> batchList =
        new SafeDictionary<string, ClientInfo>();

    /**
     * Constructor - Sets private fields
     * @param configurator IConfigurator
     * @param logger Log - common SDK "engine" log
     */
    public PNSUpload( ConfigurationData cdata ) :
        base( cdata )
    {
    }

    // Change the default values of some of the variables defined in TCPBase
    override protected int FailoverDurationSecsDefault => 120;

    override protected int MaxConnectionsDefault => 100;

    override protected int CloseLingerSecsDefault => 3;

    override protected int CloseIdleConnectionSecsDefault => 600;

    override protected bool SynchronizeSSLConnectDefault => false;

    override protected bool SynchronizeConnectDefault => false;

    override protected bool SendCloseOnNextReadDefault => false;

    public override void Close()
    {
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cInfo"></param>
    /// <param name="data"></param>
    /// <param name="timeout"></param>
    /// <param name="loopwait"></param>
    override protected void SendMessage( ClientInfo cInfo, byte [] data, int timeout,
        int loopwait )
    {
        CommUtils.SendMessage( cInfo.Channel, data, timeout, loopwait,
                MessageType.PNS );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cInfo"></param>
    /// <param name="timeout"></param>
    /// <param name="loopwait"></param>
    /// <returns></returns>
    override protected byte [] RecvMessage( ClientInfo cInfo, int timeout,
        int loopwait )
    {
        byte[] retVal = null;

        var buf = CommUtils.RecvMessage( cInfo.Channel, timeout, loopwait,
                MessageType.PNS );

        if ( buf != null )
        {
            retVal = buf.ExtractBytes();
        }

        return retVal;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    override protected CommArgs CompleteTransactionImpl( CommArgs args )
    {
        var outstr = args.Data;
        ClientInfo cInfo = null;
        var cvals = args.ControlValues;
        var cdata = args.Config;
        var batchID = cvals.PNSBatchUID + Thread.CurrentThread.ManagedThreadId;

        if ( cvals.GetBoolValue( "IsPNSBatchHeader" ) )
        {
            cInfo = DoConnect();

            if ( cInfo == null )
            {
                throw new CommException( Error.ConnectFailure,
                    "Could not connect to host " + Host + ":" + Port );
            }
            batchList[ batchID ] = cInfo;
        }
        else
        {
            cInfo = ( ClientInfo ) batchList[ batchID ];
        }

        CommArgs response = null;
        byte [] respBytes = null;
        if ( cInfo != null )
        {
            Logger.Debug( "writing " + outstr.Length +
                " bytes to: " + cInfo.ConnStr() );

            // send everything all at once for efficiency
            try
            {
                SendMessage( cInfo, outstr, WriteTimeoutSecs,
                    WriteLoopWaitMSecs );
            }
            catch ( CommException ex )
            {
                Close( batchID );

                if ( ex.ErrorCode == Error.ReadFailure )
                {
                    throw new CommException(
                        Error.ArgumentMismatch,
                        "Upload socket closed. Either there was an error or" +
                        " ProtocolManagerObj.isPNSBatchTrailer(true) was not called" +
                        " with trailer message (message type 1500");
                }

                throw;
            }

            // wait here for the response message
            try
            {
                respBytes = RecvMessage( cInfo, ReadTimeoutSecs,
                    ReadLoopWaitMSecs );
            }
            catch ( CommException ex )
            {
                // make this one no longer active
                Close( batchID );

                if ( ex.ErrorCode == Error.ReadFailure )
                {
                    throw new CommException(
                        Error.ArgumentMismatch,
                        "Upload socket closed. Either there was an error or" +
                        " ProtocolManagerObj.isPNSBatchTrailer(true) was not called" +
                        " with trailer message (message type 1500");
                }

                throw;
            }
        }
        else
        {
            // make this one no longer active
            Close( batchID );

            throw new CommException(
                Error.ArgumentMismatch,
                "No upload currently in progress for MID/TID" +
                cdata[ "AuthMID" ] + "/" +
                cdata[ "AuthTID" ] );
        }

        if ( respBytes == null || respBytes.Length == 0 )
        {
            batchList.Remove( batchID );
            throw new CommException(
                Error.ResponseFailure,
                "Empty string returned from PNS server" );
        }
        else
        {
            Logger.Debug( "read " + respBytes.Length +
            " bytes from: " + cInfo.ConnStr() );
        }

        response = new CommArgs( respBytes );

        if ( cvals.GetBoolValue( "IsPNSBatchTrailer" ) )
        {
            Close( batchID );
        }

        return response;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="batchID"></param>
    public void Close( string batchID )
    {
        ClientInfo cInfo = null;

        cInfo = batchList[ batchID ];
        batchList.Remove( batchID );

        if ( cInfo != null )
        {
            // This has to be last because it could throw an exception
            if ( CloseLingerSecs == 0 )
            {
                cInfo.Channel.CloseConnection( 0, 0 );
            }
            else
            {
                cInfo.Channel.CloseConnection(
                    WriteTimeoutSecs,
                    CloseLingerSecs );
            }
        }
    }

    /**
     * See if we are uploading an active batch for this batch ID
     * @param batchID
     * @return
     */
    public bool IsActiveBatch( string batchID )
    {
        return batchList[ batchID ] != null;
    }

    override protected string ModuleName => "PNSUpload";
}
}
