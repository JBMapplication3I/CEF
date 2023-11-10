#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Net.Sockets;
using System.Threading;
using JPMC.MSDK.Common;
using JPMC.MSDK.Configurator;


namespace JPMC.MSDK.Comm
{
    /// <summary>
    /// Title: TCP Communication Module Base Class
    ///
    /// Description: This communication module implements the
    ///"TCPBase" application protocol
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 2.0
    /// </summary>

    public abstract class TCPBase : CommBase, ICommModule
{
    /// <summary>
    ///
    /// </summary>
    /// <param name="cInfo"></param>
    /// <param name="data"></param>
    /// <param name="timeout"></param>
    /// <param name="loopwait"></param>
    protected abstract void SendMessage(ClientInfo cInfo, byte[] data, int timeout,
            int loopwait);

    /// <summary>
    /// Derived classes have individualized ways of receiving messages
    /// </summary>
    /// <param name="cInfo"></param>
    /// <param name="timeout"></param>
    /// <param name="loopwait"></param>
    /// <returns></returns>
    protected abstract byte[] RecvMessage(ClientInfo cInfo, int timeout,
            int loopwait);

    /// <summary>
    /// Derived classes have individualized main transaction handlers
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    protected abstract CommArgs CompleteTransactionImpl(CommArgs args);

    /// <summary>
    /// Main method for sending a transaction
    /// </summary>
    /// <param name="args"></param>
    /// <returns></returns>
    public CommArgs CompleteTransaction(CommArgs args)
    {
        return CompleteTransactionImpl(args);
    }

    /// <summary>
    ///  Map of thread ID to socket and other information
    /// </summary>
    protected SafeDictionary<Thread, ClientInfo> clientList =
        new SafeDictionary<Thread, ClientInfo>();

    /// <summary>
    ///
    /// </summary>
    protected TCPConnectMonitor monitor = null;
    /// <summary>
    ///
    /// </summary>
    protected object monitorStart = new object();
    /// <summary>
    ///
    /// </summary>
    protected object syncConnect = new object();

    /// <summary>
    ///
    /// </summary>
    /// <param name="cdata"></param>
    public TCPBase( ConfigurationData cdata ) : base( cdata )
    {
        CommUtils.Logger = Logger;

        connectFailRetries = cdata.GetInteger("ConnectFailRetries", ConnectFailRetriesDefault);
        verifyHost = cdata.GetBool("VerifyHost", VerifyHostDefault);
        maxConnections = cdata.GetInteger("MaxConnections", MaxConnectionsDefault);
        closeLingerSecs = cdata.GetInteger("CloseLingerSecs", CloseLingerSecsDefault);
        closeIdleConnectionSecs = cdata.GetInteger("CloseIdleConnectionSecs", CloseIdleConnectionSecsDefault);
        failoverDurationSecs = cdata.GetInteger("FailoverDurationSecs", FailoverDurationSecsDefault);
        socketTimeoutSecs = cdata.GetInteger("SocketTimeoutSecs", SocketTimeoutSecsDefault);
        socketReceiveBufferSize = cdata.GetInteger("SocketReceiveBufferSize", SocketReceiveBufferSizeDefault);
        socketSendBufferSize = cdata.GetInteger("SocketSendBufferSize", SocketSendBufferSizeDefault);
        connectTimeoutSecs = cdata.GetInteger("ConnectTimeoutSecs", ConnectTimeoutSecsDefault);
        readTimeoutSecs = cdata.GetInteger("ReadTimeoutSecs", ReadTimeoutSecsDefault);
        writeTimeoutSecs = cdata.GetInteger("WriteTimeoutSecs", WriteTimeoutSecsDefault);
        connectLoopWaitMSecs = cdata.GetInteger("ConnectLoopWaitMSecs", ConnectLoopWaitMSecsDefault);
        readLoopWaitMSecs = cdata.GetInteger("ReadLoopWaitMSecs", ReadLoopWaitMSecsDefault);
        writeLoopWaitMSecs = cdata.GetInteger("WriteLoopWaitMSecs", WriteLoopWaitMSecsDefault);
        connectResourceRetryWaitMSecs = cdata.GetInteger("ConnectResourceRetryWaitMSecs",
                ConnectResourceRetryWaitMSecsDefault);
        blockingConnect = cdata.GetBool("BlockingConnect", BlockingConnectDefault);
        blockingRead = cdata.GetBool("BlockingRead", BlockingReadDefault);
        blockingWrite = cdata.GetBool("BlockingWrite", BlockingWriteDefault);
        socketReuseAddress = cdata.GetBool("SocketReuseAddress", SocketReuseAddressDefault);
        socketKeepAlive = cdata.GetBool("SocketKeepAlive", SocketKeepAliveDefault);
        socketTcpNoDelay = cdata.GetBool("SocketTcpNoDelay", SocketTcpNoDelayDefault);
        encrypt = cdata.GetBool("Encrypt", EncryptDefault);
        synchronizeSSLConnect = cdata.GetBool("SynchronizeSSLConnect", SynchronizeSSLConnectDefault);
        synchronizeConnect = cdata.GetBool("SynchronizeConnect", SynchronizeConnectDefault);
        sendCloseOnNextRead = cdata.GetBool("SendCloseOnNextRead", SendCloseOnNextReadDefault);
        persistConnection = cdata.GetBool("PersistConnection", PersistConnectionDefault);
        getHost = cdata.GetBool("GetHost", GetHostDefault );
        getPort = cdata.GetBool("GetPort", GetPortDefault );
        port = cdata.GetInteger("Port", PortDefault );
        host = cdata.GetString("Host", HostDefault );
        failoverPort = cdata.GetInteger("FailoverPort", FailoverPortDefault );
        failoverHost = cdata.GetString("FailoverHost", FailoverHostDefault );
        logRawData = cdata.GetBool("LogRawData", LogRawDataDefault );


        // The following have non-configurable default values

        serverCN = cdata.GetString("ServerCN", null);
        failoverServerCN = cdata.GetString("FailoverServerCN", null);
        trustStorePath = cdata.GetString("TrustStorePath", null);
        var value = cdata.GetString("Ciphers", null);
        ciphers = cdata.StringToStringArray(value);
        value = cdata.GetString("Protocols", null);
        protocols = cdata.StringToStringArray(value);

    }

    protected virtual  int ConnectFailRetriesDefault => 3;

    protected virtual  bool VerifyHostDefault => true;

    protected virtual  int MaxConnectionsDefault => 100;

    protected virtual  int CloseLingerSecsDefault => 0;

    protected virtual  int CloseIdleConnectionSecsDefault => 600;

    protected virtual  int FailoverDurationSecsDefault => 0;

    protected virtual  int SocketTimeoutSecsDefault => 0;

    protected virtual  int SocketReceiveBufferSizeDefault => 1024;

    protected virtual  int SocketSendBufferSizeDefault => 1024;

    protected virtual  int ConnectTimeoutSecsDefault => 60;

    protected virtual  int ReadTimeoutSecsDefault => 60;

    protected virtual  int WriteTimeoutSecsDefault => 60;

    protected virtual  int ConnectLoopWaitMSecsDefault => 10;

    protected virtual  int ReadLoopWaitMSecsDefault => 10;

    protected virtual  int WriteLoopWaitMSecsDefault => 1000;

    protected virtual  int ConnectResourceRetryWaitMSecsDefault => 10;

    protected virtual  bool BlockingConnectDefault => true;

    protected virtual  bool BlockingReadDefault => true;

    protected virtual  bool BlockingWriteDefault => true;

    protected virtual  bool SocketReuseAddressDefault => false;

    protected virtual  bool SocketKeepAliveDefault => false;

    protected virtual  bool SocketTcpNoDelayDefault => true;

    protected virtual  bool EncryptDefault => false;

    protected virtual  bool SynchronizeSSLConnectDefault => false;

    protected virtual  bool SynchronizeConnectDefault => false;

    protected virtual  bool SendCloseOnNextReadDefault => false;

    protected virtual  bool PersistConnectionDefault => false;

    protected virtual  bool GetHostDefault => false;

    protected virtual  bool GetPortDefault => false;

    protected virtual  string HostDefault => null;

    protected virtual  int PortDefault => 0;

    protected virtual  int FailoverPortDefault => 0;

    protected virtual  string FailoverHostDefault => null;

    protected virtual bool LogRawDataDefault => false;

    protected override string ModuleName => "TCPBase";

    private int port;
    protected int Port => port;

    private string host;
    protected string Host => host;

    private int connectFailRetries;
    protected int ConnectFailRetries => connectFailRetries;

    private string failoverHost;
    protected string FailoverHost => failoverHost;

    private bool verifyHost;
    protected bool VerifyHost => verifyHost;

    private bool logRawData;
    protected bool LogRawData => logRawData;

    private int maxConnections;
    protected int MaxConnections => maxConnections;

    private int closeLingerSecs;
    protected int CloseLingerSecs => closeLingerSecs;

    private int closeIdleConnectionSecs;
    protected int CloseIdleConnectionSecs => closeIdleConnectionSecs;

    private int failoverPort;
    protected int FailoverPort => failoverPort;

    private int failoverDurationSecs;
    protected int FailoverDurationSecs => failoverDurationSecs;

    private int socketReceiveBufferSize;
    protected int SocketReceiveBufferSize => socketReceiveBufferSize;

    private int socketSendBufferSize;
    protected int SocketSendBufferSize => socketSendBufferSize;

    private int socketTimeoutSecs;
    protected int SocketTimeoutSecs => socketTimeoutSecs;

    private bool blockingConnect = true;
    protected bool BlockingConnect => blockingConnect;

    private int connectLoopWaitMSecs;
    protected int ConnectLoopWaitMSecs => connectLoopWaitMSecs;

    private int connectTimeoutSecs;
    protected int ConnectTimeoutSecs => connectTimeoutSecs;

    private int connectResourceRetryWaitMSecs;
    protected int ConnectResourceRetryWaitMSecs => connectResourceRetryWaitMSecs;

    private bool socketReuseAddress;
    protected bool SocketReuseAddress => socketReuseAddress;

    private bool socketKeepAlive;
    protected bool SocketKeepAlive => socketKeepAlive;

    private bool socketTcpNoDelay = true;
    protected bool SocketTcpNoDelay => socketTcpNoDelay;

    private bool encrypt;
    protected bool Encrypt => encrypt;

    private bool synchronizeSSLConnect;
    protected bool SynchronizeSSLConnect => synchronizeSSLConnect;

    private bool synchronizeConnect;
    protected bool SynchronizeConnect => synchronizeConnect;

    private bool sendCloseOnNextRead;
    protected bool SendCloseOnNextRead => sendCloseOnNextRead;

    private string serverCN;
    protected string ServerCN => serverCN;

    private string failoverServerCN;
    protected string FailoverServerCN => failoverServerCN;

    private string trustStorePath;
    protected string TrustStorePath => trustStorePath;

    private string[] ciphers;
    protected string[] Ciphers
    {
        get
        {
            if ( ciphers == null )
            {
                ciphers = new string[0];
            }
            return ciphers;
        }
    }

    private string[] protocols;
    protected string[] Protocols
    {
        get
        {
            if ( protocols == null )
            {
                protocols = new string[0];
            }
            return protocols;
        }
    }

    private bool blockingRead = true;
    protected bool BlockingRead => blockingRead;

    private int readLoopWaitMSecs;
    protected int ReadLoopWaitMSecs => readLoopWaitMSecs;

    private int readTimeoutSecs;
    protected int ReadTimeoutSecs => readTimeoutSecs;

    private bool blockingWrite = true;
    protected bool BlockingWrite => blockingWrite;

    private int writeLoopWaitMSecs;
    protected int WriteLoopWaitMSecs => writeLoopWaitMSecs;

    private int writeTimeoutSecs;
    protected int WriteTimeoutSecs => writeTimeoutSecs;

    private bool persistConnection = true;
    protected bool PersistConnection => persistConnection;

    private bool getHost = true;
    protected bool GetHost => getHost;

    private bool getPort = true;
    protected bool GetPort => getPort;

    /// <summary>
    /// parsed list of either exceptions or WebException types that would warrant a retry on a failed connect
    /// </summary>
    private  string[] connectRetryReasons = null;
    public string[] ConnectRetryReasons => connectRetryReasons;

    /// <summary>
    /// This method encapsulates all the activities associated with
    /// getting a connection to use for the transaction so the
    /// all the code can be easily synchronized
    /// </summary>
    /// <returns></returns>
    protected ClientInfo GetConnection()
    {
        ClientInfo retVal = null;

        var thread = Thread.CurrentThread;

        // we save each connection by thread ID
        retVal = clientList[ thread ];
        // check to see if an old object created this socket, if so
        // claim it!
        if ( retVal != null )
        {
            retVal.Busy = true;
            if ( retVal.ObjectPtr != this )
            {
                retVal.ObjectPtr = this;
            }

            if ( retVal.Channel == null ||
                ! retVal.Channel.IsConnected() )
            {
                clientList.Remove( thread );
                retVal = null;
            }
        }

        if ( retVal == null )
        {
            if ( clientList.Count >= MaxConnections )
            {
                throw new CommException(
                    Error.MaxThreadsExceeded,
                "Maximum allowable threads/connections exceeded" );
            }

            var doSync = false;

            if ( Encrypt )
            {
                if ( SynchronizeSSLConnect )
                {
                    doSync = true;
                }
            }
            else
            {
                if ( SynchronizeConnect )
                {
                    doSync = true;
                }
            }

            if ( doSync )
            {
                lock( syncConnect )
                {
                    retVal = DoConnect();
                }
            }
            else
            {
                retVal = DoConnect();
            }

            if ( retVal != null )
            {
                retVal.ThreadOwner = thread;
                retVal.Busy = true;
                // we save the object pointer of the object that
                // created this element of the clientList.  That
                // way we have an alternate way of finding it if
                // we have to destroy it in the finalize() method
                // which is a system thread not the original thread
                // that destroyed it.
                retVal.ObjectPtr = ( object ) this ;

                // now save this guy in our clientList so
                // this thread can use this connection
                // again, for his next message
                clientList[ thread ] = retVal;
            }
        }

        return retVal;
    }

    /// <summary>
    ///
    /// </summary>
    public virtual void Close()
    {
        Close( true );
    }

    /// <summary>
    /// We need to close the socket when the thread stops using it
    /// </summary>
    /// <param name="stillValid"></param>
    public void Close( bool stillValid )
    {
        var thread = Thread.CurrentThread;
        ClientInfo cInfo;

        cInfo = clientList[ thread ];
        clientList.Remove( thread );

        if ( cInfo != null )
        {
            cInfo.Busy = true;

            cInfo.LastAccessTime = Utils.GetCurrentMilliseconds();

            // tell the proxy to call its close method
            // after it gets a read event as a result of
            // our close
            if ( SendCloseOnNextRead && stillValid &&
                ! persistConnection && Encrypt )
            {
                SendCloseOnNextReadEvent( cInfo );
            }

            // This has to be last because it could throw an exception
            cInfo.Channel.CloseConnection( WriteTimeoutSecs, CloseLingerSecs );

            if ( cInfo.Channel != null )
            {
                cInfo.Channel.Sock = null;
            }
        }
    }

    /// <summary>
    /// Sends the distinctive string !c\r to tell the proxy to close this
    /// socket the next time it gets a read event
    /// </summary>
    /// <param name="info"></param>
    private void SendCloseOnNextReadEvent( ClientInfo info )
    {
        var outstr = "!c\r";
        byte [] respBytes = null;

        // send everything all at once for efficiency
        try
        {
            SendMessage( info, Utils.StringToByteArray( outstr ),
                WriteTimeoutSecs,
                WriteLoopWaitMSecs );
        }
        catch ( Exception )
        {
            Logger.Info( "Sending of CloseOnNextReadEvent failed" );
        }

        // wait here for the response message
        try
        {
            respBytes = RecvMessage( info,
                    ReadTimeoutSecs,
                    ReadLoopWaitMSecs );

            if ( respBytes == null || respBytes.Length == 0 )
            {
                Logger.Info( "Empty Response Unexpected for: "
                        + "CloseOnNextReadEvent command");
            }
            else
            {
                var respString = respBytes.ToString();

                if ( ! respString.Equals( outstr ) )
                {

                    Logger.Info( "Response: " + respString + " Unexpected for: "
                        + "CloseOnNextReadEvent command");
                }
            }
        }
        catch ( Exception )
        {
            Logger.Info( "Receiving response for CloseOnNextReadEvent failed" );
        }
    }

    /// <summary>
    /// Sets the read and write timout values for the socket.
    /// </summary>
    /// <param name="channel">The socket whose value to set.</param>
    protected void SetSocketTimeouts( SocketChannelWrapper channel )
    {
        var writeTimeoutMSecs = WriteTimeoutSecs * 1000;
        var readTimeoutMSecs = ReadTimeoutSecs * 1000;

        channel.SendTimeout = writeTimeoutMSecs;

        channel.ReceiveTimeout = readTimeoutMSecs;

    }

    /// <summary>
    /// custom read/write buffer sizes for improved performance
    /// </summary>
    /// <param name="channel"></param>
    protected void SetSocketBufferSizes( SocketChannelWrapper channel )
    {
        if ( SocketReceiveBufferSize >= 0 )
        {
            channel.ReceiveBufferSize = SocketReceiveBufferSize;
        }

        if ( SocketSendBufferSize >= 0 )
        {
            channel.SendBufferSize = SocketSendBufferSize;
        }
    }

    /// <summary>
    /// Try to connect to primary and then to failover if there is one
    /// </summary>
    /// <returns></returns>
    protected ClientInfo DoConnect()
    {
        // First connect to server
        var channel = SocketChannelWrapper.GetInstance();

        channel.Open( null, Encrypt, ServerCN );

        SetSocketTimeouts( channel );

        channel.NoDelay = true;

        SetSocketBufferSizes( channel );

        try
        {
            channel.Connect( Host, Port, ConnectTimeoutSecs,
                ConnectLoopWaitMSecs, ConnectFailRetries,
                ConnectRetryReasons, BlockingConnect );
        }
        catch ( CommException ex )
        {
            Logger.Warn( "Could not connect to: " + Host + ":" + Port );

            if ( ex.ErrorCode != Error.ConnectFailure )
            {
                throw;
            }
        }

        if ( !channel.IsConnected() )
        {
            channel.Close();

            if ( FailoverHost != null && FailoverPort != 0 )
            {
                Logger.Info( "Attempting connect to failover : "
                    + FailoverHost + ":" + FailoverPort );

                channel = SocketChannelWrapper.GetInstance();

                channel.Open( null, Encrypt, FailoverServerCN );

                SetSocketTimeouts( channel );

                channel.NoDelay = true;

                SetSocketBufferSizes( channel );

                channel.Connect( FailoverHost, FailoverPort,
                    ConnectTimeoutSecs,
                    ConnectLoopWaitMSecs, ConnectFailRetries,
                    ConnectRetryReasons, BlockingConnect );

                if ( channel.IsConnected() )
                {
                    Logger.Info( "Successfully connected to failover : "
                        + FailoverHost + ":" + FailoverPort );
                }
            }

            // if still not connected, throw an exception
            if ( !channel.IsConnected() )
            {
                throw new CommException( Error.ConnectFailure,
                    "Connection attempt has failed" );
            }
        }


        var cInfo = SaveInfo( channel, CloseLingerSecs );

        channel.NoDelay = SocketTcpNoDelay;
/*
        System.Console.Out.WriteLine( "Receive buffer is: "
            + cInfo.Channel.GetReceiveBufferSize() );
        System.Console.Out.WriteLine( "Send buffer is: "
            + cInfo.Channel.GetSendBufferSize() );
        System.Console.Out.WriteLine( "NoDelay is: "
            + cInfo.Channel.GetNoDelay() );
*/
        return cInfo;
    }

    /// <summary>
    /// Save our new socket (and other stuff) in a ClientInfo object
    /// @param channel SocketChannelWrapper - socket to save
    /// @return ClientInfo
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="closeLingerSecs"></param>
    /// <returns></returns>
    private ClientInfo SaveInfo( SocketChannelWrapper channel,
        int closeLingerSecs )
    {
        ClientInfo cInfo = null;
        // save all the Information about this connection
        if ( channel != null )
        {
            cInfo = new ClientInfo( channel );

            cInfo.Port = channel.RemotePort;

            LingerOption lingerOption = null;

            if ( closeLingerSecs > 0 )
            {
                lingerOption = new LingerOption(
                    true, closeLingerSecs * 1000 );
            }
            else
            {
                lingerOption = new LingerOption( true, 0 );
            }

            // make sure close is orderly
            channel.SetSocketOption( SocketOptionLevel.Socket,
                SocketOptionName.Linger,
                lingerOption );

            SetSocketTimeouts( channel );

            // if you can, save the remote host
            cInfo.Host = channel.RemoteHost;

            // everything is blocking I/O now
            try
            {
                channel.Blocking = true;
            }
            catch ( Exception ex )
            {
                channel.CloseConnection();

                // we can't do much else, so get out now
                throw new CommException(
                    Error.ConnectFailure,
                    "Failed to set blocking I/O mode",
                    ex );
            }
        }

        return cInfo;
    }
}
}
