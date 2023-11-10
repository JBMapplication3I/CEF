#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections.Generic;
using System.Threading;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;

namespace JPMC.MSDK.Comm
{

    ///
    ///
    /// Title: TCP Connect Communication Module
    ///
    /// Description: This communication module implements the
    /// "TCPConnect" application protocol
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 3.0
    ///

    /// <summary>
    /// This communication module implements the
    /// "TCPConnect" application protocol
    /// </summary>
    public sealed class TCPConnect : TCPBase
{
    /// <summary>
    /// Constructor - Sets private fields
    /// </summary>
    /// <param name="cdata">contents of TCPConnectConfig.xml</param>
    public TCPConnect( ConfigurationData cdata ) : base( cdata )
    {
        if ( CloseLingerSecs > 0 )
        {
            Logger.Warn("Setting the <CloseLingerSecs> element in TCPConnectConfig.xml to a value"
                + " greater than zero can cause a buildup of TIME_WAIT states.");
        }
    }

    override protected bool SendCloseOnNextReadDefault => true;

    /// <summary>
    /// Main method for sending a transaction
    /// </summary>
    /// <param name="args">request to send</param>
    /// <returns>response gotten back.</returns>
    protected override CommArgs CompleteTransactionImpl( CommArgs args )
    {
        var outstr = args.Data;
        ClientInfo cInfo = null;

        cInfo = GetConnection();

        CommArgs response = null;

        if ( cInfo != null )
        {
            Logger.Debug( "writing " + outstr.Length +
                " bytes to connection: " + cInfo.ConnStr() );

            // start monitor thread if we are using persistent connections
            // and the closeIdleConnectionSecs are more than zero
            if ( PersistConnection &&
                CloseIdleConnectionSecs != 0 && monitor == null )
            {
                // start the connection monitor if not already started
                lock( monitorStart )
                {
                    // if there was nothing to watch, monitor might have quit
                    if ( monitor == null
                        || ! monitor.IsAlive() )
                    {
                        monitor = new TCPConnectMonitor(
                                CloseIdleConnectionSecs,
                                clientList, Logger );
                    }
                }
            }

            if ( monitor != null )
            {
                // Update the last access time so connection monitor won't
                // kill it
                cInfo.LastAccessTime = Utils.GetCurrentMilliseconds();
            }

            // send everything all at once for efficiency
            try
            {
                SendMessage( cInfo, outstr,
                    WriteTimeoutSecs, WriteLoopWaitMSecs );
            }
            catch ( Exception ex )
            {
                Close( false );

                if ( ex is CommException )
                {
                    throw;
                }
                else
                {
                    throw new CommException( Error.WriteFailure,
                        "Unknown Failure Occurred while " +
                        "trying to send a transaction.",
                        ex );
                }
            }

            // wait here for the response message
            try
            {
                var buf = RecvMessage( cInfo,
                        ReadTimeoutSecs, ReadLoopWaitMSecs );

                if ( buf != null )
                {
                    response = new CommArgs( buf );
                }
            }
            catch ( Exception ex )
            {
                Close( false );

                if ( ex is CommException )
                {
                    throw;
                }
                else
                {
                    // we send a ReadFailure here so merchant knows
                    // that they may have to do a reversal or
                    // risk getting a dup if they retry
                    throw new CommException( Error.ReadFailure,
                        "Unknown Failure Occurred while " +
                        "trying to send a transaction.",
                        ex );
                }
            }
        }

        if ( response.Data == null || response.Data.Length == 0 )
        {
            Close( false );
            throw new CommException(
                Error.ReadFailure,
                "Empty String returned from proxy" );
        }
        else
        {
            Logger.Debug( "read " + response.Data.Length +
            " bytes from connection: " + cInfo.ConnStr() );
        }

        if ( monitor != null )
        {
            cInfo.LastAccessTime = Utils.GetCurrentMilliseconds();
        }

        return response;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cInfo"></param>
    /// <param name="data"></param>
    /// <param name="timeout"></param>
    /// <param name="loopwait"></param>
    protected override void SendMessage( ClientInfo cInfo, byte [] data, int timeout,
            int loopwait )
    {
        CommUtils.SendMessage( cInfo.Channel,
                data, timeout, loopwait, MessageType.SLM );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="cInfo"></param>
    /// <param name="timeout"></param>
    /// <param name="loopwait"></param>
    /// <returns></returns>
    protected override byte [] RecvMessage( ClientInfo cInfo, int timeout,
            int loopwait )
    {
        var retVal = CommUtils.RecvMessage( cInfo.Channel,
                timeout, loopwait, MessageType.SLM );
        return retVal.ExtractBytes();
    }

    /// <summary>
    /// It is possible that a thread does not close this object before
    /// letting it get garbage collected.  In which case we still need
    /// to close the socket.  Unfortunately the finalize is on a system
    /// (garbage collection) thread so we can't find the ClientInfo object
    /// with the current thread ID.  That is why when we created the
    /// ClientInfo we also saved our "this" ptr so we could find (and then
    /// delete) the ClientInfo object associated with this object.
    /// </summary>
    ~TCPConnect()
    {
        // all access to clientList is synchronized with it's SyncRoot
        lock( clientList )
        {
            var marked = new List<Thread>();

            // check to see if this object is still in the client list
            foreach ( var entry in clientList )
            {
                var cInfo = ( ClientInfo ) entry.Value;

                // if the ObjectPtr matches this object then we know
                // that this object is the one that created the
                // ClientInfo object, so we can delete it.
                if ( cInfo.ObjectPtr == this )
                {
                    cInfo.Channel.CloseConnection();

                    marked.Add( entry.Key );
                }

                // while we are at it, let's use this opportunity to
                // get rid of any stragglers that might still be in
                // the list because their thread went away but the
                // object continued to be used.   Therefore,
                // this destructor did not run for that (other) object
                if ( ! cInfo.ThreadOwner.IsAlive )
                {
                    cInfo.Channel.CloseConnection();

                    if ( cInfo.Channel != null )
                    {
                        cInfo.Channel.Sock = null;
                    }

                    marked.Add( entry.Key );
                }
            }

            // remove outside the loop

            foreach ( var victim in marked )
            {
                clientList.Remove( victim );
            }
        }
    }
    protected override string ModuleName => "TCPConnect";
}
}
