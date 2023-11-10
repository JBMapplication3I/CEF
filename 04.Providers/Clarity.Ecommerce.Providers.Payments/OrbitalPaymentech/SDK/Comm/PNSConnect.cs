#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;


namespace JPMC.MSDK.Comm
{
    ///
    ///
    /// Title: PNSConnect communication module
    ///
    /// Description: This is the communication module for PNS over a TCP connection
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 2.0
    //

    public class PNSConnect : TCPBase
{
    /**
     * Constructor - Sets private fields
     * @param configurator ConfiguratorIF
     * @param logger Log - common SDK "engine" log
     */
    public PNSConnect( ConfigurationData cdata ) :
        base( cdata )
    {
    }
    // Change the default values of some of the variables defined in TCPBase
    override protected bool SendCloseOnNextReadDefault => false;

    override protected int CloseLingerSecsDefault => 3;

    override protected string HostDefault => "xxxx";

    override protected int PortDefault => 12000;

    /**
     * Call the sendMessage as PNS message
     */
    override protected void SendMessage( ClientInfo cInfo, byte [] data, int timeout,
        int loopwait )
    {
        CommUtils.SendMessage( cInfo.Channel, data, timeout, loopwait,
                MessageType.PNS );
    }

    /**
     * Call recvMessage as PNS message
     */
    override protected byte [] RecvMessage( ClientInfo cInfo, int timeout,
            int loopwait )
    {
        var retVal = CommUtils.RecvMessage( cInfo.Channel, timeout, loopwait,
                MessageType.PNS );
        return retVal.ExtractBytes();
    }

    /**
     *
     */
    override protected CommArgs CompleteTransactionImpl( CommArgs args )
    {
        var cvals = args.ControlValues;
        var outstr = args.Data;
        ClientInfo cInfo = null;

        cInfo = GetConnection();
        byte [] respBytes = null;
        if ( cInfo != null )
        {
            Logger.Debug( "writing " + outstr.Length +
                " bytes to: " + cInfo.ConnStr() );

            // start monitor thread if we are using persistent connections
            // and the closeIdleConnectionSecs are more than zero
            if ( PersistConnection &&
                CloseIdleConnectionSecs != 0 )
            {
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

            cInfo.LastAccessTime = Utils.GetCurrentMilliseconds();

            // send everything all at once for efficiency
            try
            {
                //sendMessage( cInfo, "$$UPTD00800000002" + new string( outstr ),
                SendMessage( cInfo, outstr, WriteTimeoutSecs, WriteLoopWaitMSecs );
            }
            catch ( CommException )
            {
                Close( false );
                throw;
            }

            // wait here for the response message
            try
            {
                var respsize = 0;

                // skip over any heartbeat messages coming in
                while ( respsize == 0 )
                {
                    respBytes = RecvMessage( cInfo,
                        ReadTimeoutSecs,
                        ReadLoopWaitMSecs );

                    // if we get a heartbeat, we will get a zero length
                    // buffer back instead of a null
                    if ( respBytes == null )
                    {
                        break;
                    }

                    respsize = respBytes.Length;
                }


            }
            catch ( CommException )
            {
                Close( false );
                throw;
            }
        }
        else
        {
            throw new CommException(
                    Error.ConnectFailure,
                    "Failed to connect to server" );
        }

        if ( respBytes == null || respBytes.Length == 0 )
        {
            Close( false);
            throw new CommException(
                Error.ResponseFailure,
                "Empty String returned" );
        }
        else
        {
            Logger.Debug( "read " + respBytes.Length +
            " bytes from: " + cInfo.ConnStr() );
        }

        cInfo.LastAccessTime = Utils.GetCurrentMilliseconds();
        cInfo.Busy = false;

        var response = new CommArgs( respBytes );

        //  Used for debugging
        //CommUtils.dumpToFile( "request.data", outstr, false );

        return response;
    }

    protected override string ModuleName => "PNSConnect";
}
}
