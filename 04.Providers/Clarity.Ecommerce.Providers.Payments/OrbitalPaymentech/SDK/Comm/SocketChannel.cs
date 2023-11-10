#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Security;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using JPMC.MSDK.Common;
using log4net;


namespace JPMC.MSDK.Comm
{
    ///
    ///
    /// Title: Socket Channel
    ///
    /// Description: Similar to the Java SocketChannel class
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 3.0
    ///


    /// <summary>
    /// Similar to the Java SocketChannel class
    /// </summary>
    public class SocketChannel : IDisposable
{
    private string remoteHost;
    private int remotePort;
    private IPEndPoint remoteEndPoint;
    private object SyncRoot = new object();

    private static SafeDictionary<SslStream, string> CNTable =
        new SafeDictionary<SslStream, string>();

    // Use common SDK logger by default, normally this will be reset
    private static ILog logger;

    /// <summary>
    /// Used to synchronize connect completion callback with thread that
    /// started the non-blocking connect
    /// </summary>
    private ManualResetEvent waitEvent;

    /// <summary>
    /// Constructor when we already have a socket
    /// </summary>
    /// <param name="sock"></param>
    protected SocketChannel( Socket sock )
    {
        Sock = sock;
    }

    // Implement IDisposable.
    // Do not make this method virtual.
    // A derived class should not be able to override this method.
    public void Dispose()
    {
        Dispose(true);
        // This object will be cleaned up by the Dispose method.
        // Therefore, you should call GC.SupressFinalize to
        // take this object off the finalization queue
        // and prevent finalization code for this object
        // from executing a second time.
        GC.SuppressFinalize(this);
    }

    // Dispose(bool disposing) executes in two distinct scenarios.
    // If disposing equals true, the method has been called directly
    // or indirectly by a user's code. Managed and unmanaged resources
    // can be disposed.
    // If disposing equals false, the method has been called by the
    // runtime from inside the finalizer and you should not reference
    // other objects. Only unmanaged resources can be disposed.
    protected virtual void Dispose(bool disposing)
    {
        // Check to see if Dispose has already been called.
        if (!this.disposed)
        {
            // If disposing equals true, dispose all managed
            // and unmanaged resources.
            if (disposing)
            {
                if (Sock != null)
                {
                    Sock.Dispose();
                }

                if ( waitEvent != null )
                {
                    waitEvent.Dispose();
                }
            }

            // Note disposing has been done.
            disposed = true;

        }
    }
    private bool disposed;

    /// <summary>
    /// socket getter
    /// </summary>
    public Socket Sock { get; set; }

    /// <summary>
    /// The last exception that was thrown by this socket
    /// </summary>
    public Exception LastException { get; set; }

    /// <summary>
    /// Stream used for SSL connections
    /// </summary>
    public SslStream SSLStream { get; set; }

    /// <summary>
    /// Do we encrypt this connection?
    /// </summary>
    public bool Encrypt { get; set; }

    /// <summary>
    /// The certificate subject's "Common Name"
    /// </summary>
    public string ServerCN { get; set; }

    /// <summary>
    /// stored server certificate
    /// </summary>
    public static X509Certificate2 ServerCertificate { get; set; }

    /// <summary>
    /// Check to see if a connection is connected, the
    /// Socket.Connected property cannot be trusted.
    /// Must poll for errors to get accurate reading
    /// </summary>
    /// <returns>true if connected.</returns>
    public bool IsConnected()
    {
        var retVal = false;

        try
        {
            if ( Sock != null && Sock.Connected )
            {
                retVal = ! Sock.Poll( 0, SelectMode.SelectError );
            }
        }
        catch ( Exception )
        {
            // CommUtils.PrintException( ex );
            // ignore
        }

        return retVal;
    }

    /// <summary>
    /// Make a new SocketChannel
    /// </summary>
    /// <returns>SocketChannel.</returns>
    public static SocketChannel Open()
    {
        return Open( null, false, null );
    }

    /// <summary>
    /// alternate open
    /// </summary>
    /// <param name="sock"></param>
    /// <returns></returns>
    public static SocketChannel Open( Socket sock )
    {
        return Open( sock, false, null );
    }

    /// <summary>
    /// alternate open
    /// </summary>
    /// <param name="sock"></param>
    /// <param name="encrypt_data"></param>
    /// <param name="CN"></param>
    /// <returns></returns>
    public static SocketChannel Open( Socket sock, bool encrypt_data,
        string CN )
    {
        var retVal = new SocketChannel( sock );

        if ( sock == null )
        {
            retVal.Sock = new Socket( AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp );
        }
        if ( encrypt_data )
        {
            retVal.Encrypt = true;
            retVal.ServerCN = CN;
        }
        return retVal;
        }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public int ReceiveTimeout
    {
        get => Sock.ReceiveTimeout;
        set
        {
            if ( value > 0 ||
                value == System.Threading.Timeout.Infinite )
            {
                Sock.ReceiveTimeout = value;

                if ( Encrypt && SSLStream != null )
                {
                    SSLStream.ReadTimeout = value;
                }
            }
        }
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public int SendTimeout
    {
        get => Sock.SendTimeout;
        set
        {
            if ( value > 0 ||
                value == System.Threading.Timeout.Infinite )
            {
                Sock.SendTimeout = value;

                if ( Encrypt && SSLStream != null )
                {
                    SSLStream.WriteTimeout = value;
                }
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    public int ReceiveBufferSize
    {
        get => Sock.ReceiveBufferSize;
        set => Sock.ReceiveBufferSize = value;
    }

    /// <summary>
    ///
    /// </summary>
    public int SendBufferSize
    {
        get => Sock.SendBufferSize;
        set => Sock.SendBufferSize = value;
    }

    /// <summary>
    /// </summary>
    /// <returns></returns>
    public bool NoDelay
    {
        get => Sock.NoDelay;
        set => Sock.NoDelay = value;
    }

    /// <summary>
    /// Accept a connection
    /// </summary>
    /// <returns></returns>
    public SocketChannel Accept()
    {
        return Accept( 0 );
    }

    /// <summary>
    /// alternate accept
    /// </summary>
    /// <param name="timeOutSecs"></param>
    /// <returns></returns>
    public SocketChannel Accept( int timeOutSecs )
    {
        Socket newsock = null;
        SocketChannel retVal = null;
        var restoreNonBlocking = false;
        var restoreNoDelay = false;
        var timeOutMSecs = timeOutSecs * 1000;

        var prevTimeoutMSecs = ReceiveTimeout;

        if ( prevTimeoutMSecs != timeOutMSecs )
        {
            ReceiveTimeout = timeOutMSecs;
        }

        if ( ! Sock.Blocking )
        {
            Sock.Blocking = true;
            restoreNonBlocking = true;
        }

        if ( ! NoDelay )
        {
            NoDelay = true;

            restoreNoDelay = true;
        }

        try
        {
            newsock = Sock.Accept();

            if ( newsock != null )
            {
                retVal = new SocketChannel( newsock );

                var remote = ( IPEndPoint ) newsock.RemoteEndPoint;

                retVal.RemoteHost = remote.Address.ToString();

                retVal.RemotePort = remote.Port;
            }

            if ( Encrypt )
            {
                retVal.Encrypt = true;
                retVal.DoSSL( false );
            }
        }
        catch ( Exception )
        {
            if ( retVal != null )
            {
                CloseConnection( retVal, 0, 0 );
            }
            // only do this if retVal is null meaning newsock never got saved in it
            else if ( newsock != null )
            {
                newsock.Close();
            }
            throw;
        }
        finally
        {
            if ( restoreNonBlocking )
            {
                Sock.Blocking = false;
            }

            if ( restoreNoDelay )
            {
                NoDelay = false;
            }

            if ( prevTimeoutMSecs != timeOutMSecs )
            {
                ReceiveTimeout = prevTimeoutMSecs;
            }
        }

        return retVal;
    }

    /// <summary>
    /// When non-blocking connect finishes, the network layer calls this
    /// method.
    /// </summary>
    /// <param name="ar">defined callback object</param>
    private static void ConnectCallback( IAsyncResult ar )
    {
        SocketChannel client = null;

        if ( ar != null && ar.AsyncState != null )
        {
            // Retrieve the socket from the state object.
            client = ( SocketChannel ) ar.AsyncState;
            Socket sock = null;

            try
            {
                if ( client != null )
                {
                    sock = client.Sock;
                }

                if ( sock != null )
                {
                    // Complete the connection.
                    sock.EndConnect( ar );

                    Logger.Debug( "Completed Connect" );
                }
            }
            catch ( Exception ex )
            {
                client.LastException = ex;
            }

            lock ( client.SyncRoot )
            {
                // no matter what, wake up the thread that is waiting
                if ( client != null && client.waitEvent != null )
                {
                    client.waitEvent.Set();
                }
            }
        }
    }

    /// <summary>
    /// Connect with reason based retries
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="timeOutSecs"></param>
    /// <param name="connectLoopWaitMSecs"></param>
    /// <param name="failRetries"></param>
    /// <param name="retryReasons"></param>
    /// <param name="doBlocking"></param>
    public void Connect( string host, int port, int timeOutSecs,
        int connectLoopWaitMSecs, int failRetries,
        string [] retryReasons, bool doBlocking )
    {
        var count = 0;

        // only for "continue" to work
        while ( true )
        {
            try
            {
                if ( doBlocking )
                {
                    BlockingConnect( host, port, timeOutSecs );
                }
                else
                {
                    Connect( host, port, timeOutSecs,
                        connectLoopWaitMSecs );
                }
            }
            catch( Exception ex )
            {
                if ( count++ < failRetries )
                {
                    SocketException sex = null;

                    if ( ex is CommException )
                    {
                        if ( ex.InnerException is SocketException )
                        {
                            sex = ( SocketException ) ex.InnerException;
                        }
                    }
                    else if ( ex is SocketException )
                    {
                        sex = ( SocketException ) ex;
                    }

                    if ( sex != null )
                    {
                        var serr =
                            ( SocketError ) sex.ErrorCode;

                        // Retry if there are no reasons defined or if the reason
                        // matches one of the ones specified.
                        if ( retryReasons == null || retryReasons.Length == 0
                            || CommUtils.StringInArray( serr.ToString(),
                            retryReasons ) )
                        {
                            //Console.WriteLine("SLEEPING: " +
                            //connectLoopWaitMSecs + " milliseconds");
                            Thread.Sleep( connectLoopWaitMSecs );
                            continue;
                        }
                    }
                }

                // otherwise just throw the exception
                if ( ex is CommException )
                {
                    throw;
                }
                else
                {
                    throw new CommException( Error.ConnectFailure,
                        "Connection Failure", ex );
                }
            }

            // no exception so quit
            break;
        }
    }

    /// <summary>
    ///  Do a non-blocking connect
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="timeOutSecs"></param>
    /// <param name="connectLoopWaitMSecs"></param>
    public void Connect( string host, int port, int timeOutSecs,
        int connectLoopWaitMSecs )
    {
        var wasTimeout = false;

        RemoteHost = host;
        RemotePort = port;

        var ip = CommUtils.GetIPAddress( RemoteHost );

        if ( ip == null )
        {
            throw new CommException( Error.IPNotFound,
                "Could not convert: " + RemoteHost + " to an IP address" );
        }

        var dest = new IPEndPoint( ip, RemotePort );

        if ( Sock == null )
        {
            Sock = new Socket( AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp );
        }

        Sock.Blocking = false;

        // make a delegate that points at our callback method
        AsyncCallback callback = null;
            object thisPtr = null;
        LastException = null;

        callback = new AsyncCallback( ConnectCallback );
        thisPtr = this;

        // create a new event object so the callback method can signal this
        // method to finish when the connect finishes.  Initialize to "not set"
        // create it before the call to BeginConnect so there isn't a race
        // condition
        waitEvent = new ManualResetEvent( false );

        try
        {
            // pass the "this" pointer so we can get at the socket
            // as well as the wait event saved in this object
            Sock.BeginConnect( dest, callback, thisPtr );

            Logger.Debug( "Attempting to connect to host: " +
                RemoteHost + ":" +
                RemotePort );
        }
        catch( Exception ex )
        {
            Logger.Debug("Got exception: " + ex.Message +
                " of type: " + ex.GetType().Name );

            if ( ex.InnerException != null )
            {
                Logger.Debug(
                    " with inner exception message: "
                    + ex.InnerException.Message );
            }

            if ( ex is SocketException )
            {
                var sex = ( SocketException ) ex;
                Logger.Debug( "Got SocketException: " + sex.Message +
                        " with native code: " + sex.NativeErrorCode );
            }

            LastException = null;

            CloseConnection();

            Sock = null;
            throw;
        }

        // if we can timeout
        if ( timeOutSecs > 0 )
        {
            var startTime = Utils.GetCurrentMilliseconds();

            // callback is not reliable so we can end up waiting forever if
            // our wait event is not set.  Add a timeout to the wait event
            // and do a "no wait" poll to confirm in the case where
            // the event does not get set
            for ( var i=0; Sock != null &&
                ! Sock.Connected; i++ )
            {
                // this detects the error we get when we hit the user port limit
                // on XP, it chooses 5000 but doesn't let you use it
                if ( Sock.Poll( 0, SelectMode.SelectError ) )
                {
                    break;
                }

                // wait for thread blocker to be set by callback method
                // WaitOne() takes timeout
                if ( waitEvent.WaitOne( connectLoopWaitMSecs, false ) )
                {
                    // sometimes (don't ask me why) even after the EndConnect
                    // gets called, it takes some time to finish connecting.
                    // this loop gives that extra time if necessary
                    while ( LastException == null && ! Sock.Connected )
                    {
                        // wait just a small amount of time
                        if ( ReachedTimeout( startTime, connectLoopWaitMSecs,
                            connectLoopWaitMSecs ) )
                        {
                            break;
                        }
                    }

                    break;
                }
                else
                {
                    waitEvent.Reset();
                }

                if ( ReachedTimeout( startTime, timeOutSecs * 1000, connectLoopWaitMSecs ) )
                {
                    wasTimeout = true;
                    break;
                }
            }

            lock ( SyncRoot )
            {
                waitEvent = null;
            }

            if ( Sock != null && ! Sock.Connected )
            {
                Exception ex = null;

                ex = LastException;

                LastException = null;

                CloseConnection();

                Sock = null;

                if ( ex != null && ex is SocketException )
                {
                    var sex = ( SocketException ) ex;

                    if ( sex.ErrorCode == ( int ) SocketError.TimedOut )
                    {
                        wasTimeout = true;
                    }
                }

                if ( ex is ObjectDisposedException )
                {
                    var addr = ( IPEndPoint ) Sock.LocalEndPoint;

                    Logger.Debug( "Bind failure on port: " + addr.Port );
                }

                if ( wasTimeout )
                {
                    throw new CommException(
                        Error.ConnectTimeoutFailure,
                        "Connection Timeout Failure",
                        new SocketException(
                        ( int ) SocketError.TimedOut ) );
                }
                else
                {
                    if ( ex == null )
                    {
                        ex = new SocketException(
                        ( int ) SocketError.AddressNotAvailable );
                    }


                    throw new CommException(
                        Error.ConnectFailure,
                        "Connection Failure", ex );
                }
            }
        }
        else
        {
            lock ( SyncRoot )
            {
                waitEvent = null;
            }
        }

        LastException = null;

        if ( Sock != null )
        {
            // only proxy does non-blocking I/O and it sets it
            // to false right after this
            Sock.Blocking = true;

            if ( Encrypt )
            {
                DoSSL( true );
            }
        }
    }

    /// <summary>
    /// Check whether or not we timed out and sleep if we didn't
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="timeOutMSecs"></param>
    /// <param name="waitMSecs"></param>
    /// <returns></returns>
    private bool ReachedTimeout( long startTime, long timeOutMSecs, int waitMSecs )
    {
        var retVal = false;

        // check to see if we timed out
        var elapsedTime = Utils.GetCurrentMilliseconds() - startTime;

        if ( elapsedTime > timeOutMSecs )
        {
            retVal = true;
        }
        else if ( waitMSecs > 0 )
        {
            Thread.Sleep( waitMSecs );
        }
        return retVal;
    }

    /// <summary>
    /// A blocking version of connect
    /// </summary>
    /// <param name="host"></param>
    /// <param name="port"></param>
    /// <param name="timeOutSecs"></param>
    public void BlockingConnect( string host, int port, int timeOutSecs )
    {
        RemoteHost = host;
        RemotePort = port;
        var sendTimeoutMSecs = timeOutSecs * 1000;
        var receiveTimeoutMSecs = sendTimeoutMSecs;

        var ip = CommUtils.GetIPAddress( host );

        if ( ip == null )
        {
            throw new CommException( Error.IPNotFound,
                "Could not convert: " + RemoteHost + " to an IP address" );
        }

        var dest = new IPEndPoint( ip, port );

        if ( Sock == null )
        {
            Sock = new Socket( AddressFamily.InterNetwork,
                SocketType.Stream, ProtocolType.Tcp );
        }

        var prevSendTimeoutMSecs = SendTimeout;
        var prevReceiveTimeoutMSecs = ReceiveTimeout;
        var prevNoDelay = NoDelay;

        Sock.Blocking = true;

        if ( sendTimeoutMSecs != prevSendTimeoutMSecs )
        {
            SendTimeout = sendTimeoutMSecs;
        }

        if ( receiveTimeoutMSecs != prevReceiveTimeoutMSecs )
        {
            ReceiveTimeout = receiveTimeoutMSecs;
        }

        if ( ! prevNoDelay )
        {
            NoDelay = true;
        }

        try
        {
            // pass the "this" pointer so we can get at the socket
            // as well as the wait event saved in this object
            Sock.Connect( dest );

            if ( Encrypt )
            {
                DoSSL( true );
            }
        }
        catch ( SocketException ex )
        {
            Logger.Debug( "Got SocketException: " + ex.Message +
                " with native code: " + ex.NativeErrorCode );

            CloseConnection();
            Sock = null;
            throw;
        }
        catch ( Exception ex )
        {
            Logger.Debug( "Got exception: " + ex.Message +
                " of type: " + ex.GetType().Name );

            if ( ex.InnerException != null )
            {
                Logger.Debug(
                    " with inner exception message: "
                    + ex.InnerException.Message );
            }

            CloseConnection();
            Sock = null;
            throw;
        }
        finally
        {
            if ( Sock != null )
            {
                if ( sendTimeoutMSecs != prevSendTimeoutMSecs )
                {
                    SendTimeout = prevSendTimeoutMSecs;
                }

                if ( receiveTimeoutMSecs != prevReceiveTimeoutMSecs )
                {
                    ReceiveTimeout = prevReceiveTimeoutMSecs;
                }

                if ( ! prevNoDelay )
                {
                    NoDelay = false;
                }

            }
        }

        if ( ! IsConnected() )
        {
            var ex = LastException;

            LastException = null;
            CloseConnection();
            Sock = null;

            throw new CommException(
                Error.ConnectFailure,
                "Connection Failure", ex );
        }
    }




    /// <summary>
    ///
    /// </summary>
    public IPEndPoint RemoteEndPoint
    {
        get
        {
            if ( remoteEndPoint == null )
            {
                if ( Sock != null )
                {
                    remoteEndPoint = (IPEndPoint) Sock.RemoteEndPoint;

                    if ( remoteEndPoint != null )

                    {
                        if ( remoteEndPoint.Address != null )
                        {
                            remoteHost = remoteEndPoint.Address.ToString();
                        }
                        remotePort = remoteEndPoint.Port;
                    }

                }
            }
            return remoteEndPoint;
        }
    }

    /// <summary>
    /// The host on the "other" side of the connection
    /// </summary>
    public string RemoteHost
    {
        get
        {
            if ( remoteHost == null )
            {
                var endpoint = RemoteEndPoint;

                if ( endpoint != null )
                {
                    remoteHost = endpoint.Address.ToString();
                }
            }

            return remoteHost;
        }
        set => remoteHost = value;
    }


    /// <summary>
    /// The port on the "other" side of the connection
    /// </summary>
    public int RemotePort
    {
        get
        {
            if ( remotePort == 0 )
            {
                var endpoint = RemoteEndPoint;

                if ( endpoint != null )
                {
                    remotePort = endpoint.Port;
                }
            }

            return remotePort;
        }
        set => remotePort = value;
    }



    /// <summary>
    /// The Logger to use
    /// </summary>
    public static ILog Logger
    {
        get
        {
            if (logger == null)
            {
                logger = new LoggingWrapper().EngineLogger;
            }
            return logger;
        }
        set => logger = value;
    }

    /// <summary>
    /// Read only so many bytes into a buffer
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    public int Read( ByteBuffer buff )
    {
        var retVal = 0;

        try
        {
            retVal = ( int ) Sock.Receive( buff.Array(), ( int ) buff.Position,
                    ( int ) buff.Remaining(), SocketFlags.None );
        }
        catch ( Exception ex )
        {
            //CommUtils.PrintException( ex );

            SocketException sex = null;

            if ( ex is IOException )
            {
                var iex = ( IOException ) ex;
                // This special exception is thrown when we are non-blocking and there
                // is no data to read
                if ( iex.InnerException is SocketException )
                {
                    sex = ( SocketException ) iex.InnerException;
                }
            }

            if ( ex is SocketException )
            {
                sex = ( SocketException ) ex;
            }

            // only ignore the WouldBlock exception
            if ( sex == null || sex.ErrorCode != ( int ) SocketError.WouldBlock )
            {
                if ( sex != null && sex.ErrorCode == ( int ) SocketError.TimedOut )
                {
                    throw new CommException( Error.ReadTimeoutFailure, "read timeout",
                    sex );
                }
                else
                {
                    throw new CommException( Error.ReadFailure, "read failure",
                        sex == null ? ex : sex );
                }
            }
        }

        if ( retVal == 0 && Sock.Poll( 0, SelectMode.SelectRead ) && Sock.Available == 0 )
        {
            throw new CommException( Error.EndOfFile, "Peer has closed connection" );
        }

        if ( retVal > 0 )
        {
            buff.AddPosition( ( ulong ) retVal );
        }

        return retVal;
    }

    /// <summary>
    /// Write ByteBuffer bytes to a socket
    /// </summary>
    /// <param name="buff"> ByteBuffer with bytes to send</param>
    /// <returns>number of bytes written.</returns>
    public long Write( ByteBuffer buff )
    {
        var retVal = 0;

        // if everything is
        if ( buff != null && buff.Remaining() > 0 )
        {
            try
            {
                retVal = Sock.Send( buff.Array(), ( int ) buff.Position,
                    ( int ) buff.Remaining(), SocketFlags.None );
            }
            catch ( SocketException ex )
            {
                if ( ex.ErrorCode != ( int ) SocketError.WouldBlock )
                {
                    throw;
                }
            }
        }

        if ( retVal > 0 )
        {
            buff.AddPosition( ( ulong ) retVal );
        }

        return retVal;
    }



    /// <summary>
    /// The following method is invoked by the RemoteCertificateValidationDelegate.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="certificate"></param>
    /// <param name="chain"></param>
    /// <param name="sslPolicyErrors"></param>
    /// <returns></returns>
    private static bool ValidateServerCertificate(
          object sender,
          X509Certificate certificate,
          X509Chain chain,
          SslPolicyErrors sslPolicyErrors )
    {
        var stream = (SslStream) sender;

        var serverCN = ( string ) CNTable[ stream ];

        // if we have errors and it isn't serverCN is null and its because
        // remote name doesn't match
        if ( sslPolicyErrors != SslPolicyErrors.None &&
            ! ( serverCN == null && sslPolicyErrors
                == SslPolicyErrors.RemoteCertificateNameMismatch ) )
        {
            Logger.Error( "Certificate error: " + sslPolicyErrors );
            return false;
        }

        if ( serverCN != null )
        {
            //get the common name from the first cert
            var dn = certificate.Subject;

            var cn = CommUtils.GetCN( dn );

            if ( serverCN == cn )
            {
                Logger.Debug( "Server CN: " + cn + " is as expected" );
            }
            else
            {
                var outstr = "SSL Server Common Name (CN) invalid: "
                    + "expected '" + serverCN +
                    "', received '" + cn + "'";
                Logger.Warn( outstr );
                throw new CommException( Error.TrustFailure, outstr );
            }
        }

        // Do not allow this client to communicate with unauthenticated servers.
        return true;
    }

    /// <summary>
    /// wrapper for DoSSLImpl that just resets blocking and nodelay if necessary
    /// </summary>
    /// <param name="isClient"></param>
    public void DoSSL( bool isClient )
    {
        var restoreNonBlocking = false;
        var restoreNoDelay = false;
        try
        {
            // Make the socket block while we do the handshake
            if ( !Sock.Blocking )
            {
                Sock.Blocking = true;
                restoreNonBlocking = true;
            }

            if ( ! NoDelay )
            {
                NoDelay = true;
                restoreNoDelay = true;
            }

            DoSSLImpl( isClient );
        }
        finally
        {
            if ( Sock != null )
            {
                if ( restoreNonBlocking )
                {
                    Sock.Blocking = false;
                }
                if ( restoreNoDelay )
                {
                    NoDelay = false;
                }

            }

        }
    }
    /// <summary>
    /// Do the SSL handshake
    /// </summary>
    /// <param name="isClient"></param>
    /// <returns></returns>
    public void DoSSLImpl( bool isClient )
    {
        var stream = new NetworkStream( Sock, false );

        if ( isClient )
        {
            var remote =
                new RemoteCertificateValidationCallback(
                ValidateServerCertificate );

            SSLStream = new SslStream( stream, false, remote );

            CNTable.Add( SSLStream, ServerCN );
        }
        else
        {
            SSLStream = new SslStream( stream, false );
        }

        try
        {
            if ( isClient )
            {
                if ( ServerCN == null )
                {
                    SSLStream.AuthenticateAsClient( "" );
                }
                else
                {
                    SSLStream.AuthenticateAsClient( ServerCN );
                }
            }
            else
            {

                SSLStream.AuthenticateAsServer( ServerCertificate );
                //SSLStream.AuthenticateAsServer( ServerCertificate,
                    //false, SslProtocols.Default, true );
            }
        }
        catch ( TimeoutException ex )
        {
            Logger.Error( "Peer is not responding to SSL handshake.  " +
                "Is peer configured for SSL encryption?" );
            throw new CommException( Error.SSLHandshakeFailure,
                "Peer is not responding to SSL handshake.", ex );
        }
        catch ( ProtocolViolationException ex )
        {
            if ( isClient )
            {
                Logger.Error( "SSL Handshake has failed.\nCheck to make sure trust store "
                    + "has remote server\'s root certificate." );
            }
            else
            {
                Logger.Error( "SSL Handshake has failed.\nCheck to make sure " +
                " server certificate is valid." );
            }
            throw new CommException( Error.SSLHandshakeFailure,
                "SSL Handshake has failed.", ex );
        }
        catch ( Exception ex )
        {
            // don't print out error for health monitors
            if ( ! ex.Message.Contains( "remote party has closed" ) )
            {
                Logger.Error( "An SSL connection could not be established.\n"
                    + "Check to be sure the remote peer "
                    + "is set up to do SSL connections" );
            }

            SocketException sex = null;

            if ( ex is IOException )
            {
                if ( ex.InnerException is SocketException )
                {
                    sex = ( SocketException ) ex.InnerException;
                }
            }

            throw new CommException( Error.SSLHandshakeFailure,
                "SSL Handshake has failed.", sex == null ? ex : sex );
        }
        finally
        {
            if ( isClient )
            {
                CNTable.Remove( SSLStream );
            }
        }
    }

    /// <summary>
    /// Used for SSL sockets
    /// </summary>
    /// <param name="outbuf"></param>
    /// <returns></returns>
    public int WriteStream( ByteBuffer outbuf )
    {
        var pos = ( int ) outbuf.Position;
        ulong retVal = 0;

        // SslStream.Write doesn't return
        // the number of bytes it successfully wrote.
        try
        {
            SSLStream.Write( outbuf.Array(),
                    ( int ) outbuf.Position,
                    ( int ) outbuf.Remaining() );

            // so all we can do is assume it wrote all the bytes.
            retVal = outbuf.Remaining();
        }
        catch ( IOException ex )
        {
            var ignore = false;

            if ( ex.InnerException is SocketException )
            {
                var sex = ( SocketException ) ex.InnerException;

                if ( sex.SocketErrorCode == System.Net.Sockets.SocketError.WouldBlock )
                {
                    ignore = true;
                }
            }

            if ( !ignore )
            {
                throw;
            }
        }

        SSLStream.Flush();

        if ( retVal > 0 )
        {
            outbuf.AddPosition( retVal );
        }

        return ( int ) retVal;
    }

    /// <summary>
    /// Read a ByteBuffer full of stuff from the socket's stream
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    public int ReadStream( ByteBuffer buff )
    {
        var len = 0;
        var noneAvailable = Sock.Available == 0;

        try
        {
            len = SSLStream.Read( buff.Array(), ( int ) buff.Position,
                ( int ) buff.Remaining() );
        }
        catch ( IOException iex )
        {
            SocketException sex = null;

            if ( iex.InnerException is SocketException )
            {
                sex = ( SocketException ) iex.InnerException;
            }

            if ( sex != null && sex.ErrorCode == ( int ) SocketError.TimedOut )
            {
                throw new CommException( Error.ReadTimeoutFailure, "read timeout",
                sex );
            }
            else
            {
                throw new CommException( Error.ReadFailure, "read failure",
                    sex == null ? ( Exception ) iex : sex );
            }
        }

        if ( len == 0 && Sock.Poll( 0, SelectMode.SelectRead ) && noneAvailable )
        {
            throw new CommException( Error.EndOfFile, "Peer has closed connection" );
        }

        if ( len > 0 )
        {
            buff.AddPosition( ( ulong ) len );
        }
        return len;
    }

    /// <summary>
    ///
    /// </summary>
    /// <returns></returns>
    public bool CloseConnection()
    {
        return CloseConnection( this, 0, 0 );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="timeoutSecs"></param>
    /// <param name="lingerSecs"></param>
    /// <returns></returns>
    public bool CloseConnection( int timeoutSecs, int lingerSecs )
    {
        return CloseConnection( this, timeoutSecs, lingerSecs );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="lingerSecs"></param>
    /// <returns></returns>
    public bool CloseConnection( SocketChannel channel,
        int timeoutSecs, int lingerSecs )
    {
        var retVal = true;

        if ( channel == null )
        {
            return false;
        }

        if ( channel.Encrypt )
        {
            var sslStream = channel.SSLStream;

            if ( sslStream != null )
            {
                sslStream.Close();
                channel.SSLStream = null;
            }
        }

        var rawsock = channel.Sock;

        if ( rawsock != null )
        {
            // make sure we can do this close because otherwise we
            // may lose a file descriptor
            try
            {
                rawsock.Blocking = true;

                LingerOption lingerOption = null;

                if ( lingerSecs > 0 )
                {
                    lingerOption = new LingerOption( true, lingerSecs * 1000 );
                    rawsock.SetSocketOption( SocketOptionLevel.Socket,
                        SocketOptionName.Linger, lingerOption );
                    rawsock.Shutdown( SocketShutdown.Both );

                    rawsock.Close( timeoutSecs );
                }
                else
                {
                    lingerOption = new LingerOption( true, 0 );
                    rawsock.SetSocketOption( SocketOptionLevel.Socket,
                        SocketOptionName.Linger, lingerOption );
                    rawsock.Close();
                }

            }
            catch ( ObjectDisposedException )
            {
                // ignore
                Logger.Debug( "Socket already closed" );
            }
            catch ( Exception e )
            {
                Logger.Info(
                "Failed to close socket, exception is: "
                + e.GetType().Name + " with message: "
                + e.Message );
                retVal = false;
            }

            //channel.Sock = null;
        }

        return retVal;
    }

    /// <summary>
    ///
    /// </summary>
    public bool Blocking
    {
        get => Sock.Blocking;
        set => Sock.Blocking = value;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="readers"></param>
    /// <param name="writers"></param>
    /// <param name="errors"></param>
    /// <param name="microSecToWait"></param>
    public void Select( IList readers, IList writers, IList errors,
        int microSecToWait )
    {
        Socket.Select( readers, writers, errors, microSecToWait );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="readers"></param>
    /// <param name="writers"></param>
    /// <param name="errors"></param>
    /// <param name="microSecToWait"></param>
    public void Select( IList<Socket> readers, IList<Socket> writers, IList<Socket> errors,
        int microSecToWait )
    {
        System.Collections.IList oreaders = null;
        System.Collections.IList owriters = null;
        System.Collections.IList oerrors = null;
        var doReaders = readers != null && readers.Count > 0;
        var doWriters = writers != null && writers.Count > 0;
        var doErrors = errors != null && errors.Count > 0;

        // convert the generic IList to the old one that Select needs
        if ( doReaders )
        {
            oreaders = new ArrayList();
            foreach ( var sock in readers )
            {
                oreaders.Add( sock );
            }
        }
        if ( doWriters )
        {
            owriters = new ArrayList();
            foreach ( var sock in writers )
            {
                owriters.Add( sock );
            }
        }
        if ( doErrors )
        {
            oerrors = new ArrayList();
            foreach ( var sock in errors )
            {
                oerrors.Add( sock );
            }
        }

        Select( oreaders, owriters, oerrors, microSecToWait );

        // now copy any return values back
        if ( doReaders )
        {
            readers.Clear();
            foreach ( Socket sock in oreaders )
            {
                readers.Add( sock );
            }
        }
        if ( doWriters )
        {
            writers.Clear();
            foreach ( Socket sock in owriters )
            {
                writers.Add( sock );
            }
        }
        if ( doErrors )
        {
            errors.Clear();
            foreach ( Socket sock in oerrors )
            {
                errors.Add( sock );
            }
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="where"></param>
    public void Bind( EndPoint where )
    {
        Sock.Bind( where );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="backlog"></param>
    public void Listen( int backlog )
    {
        Sock.Listen( backlog );
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="microSecs"></param>
    /// <param name="mode"></param>
    /// <returns></returns>
    public bool Poll( int microSecs, SelectMode mode )
    {
        return Sock.Poll( microSecs, mode );
    }

    /// <summary>
    ///
    /// </summary>
    public void Close()
    {
        // calling close on a null socket is ok so just check before using it
        if ( Sock != null )
        {
            Sock.Close();
        }
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="level"></param>
    /// <param name="optname"></param>
    /// <param name="lingerOption"></param>
    public void SetSocketOption( SocketOptionLevel level,
            SocketOptionName optname, LingerOption lingerOption )
    {
        Sock.SetSocketOption( level, optname, lingerOption );
    }
}
}
