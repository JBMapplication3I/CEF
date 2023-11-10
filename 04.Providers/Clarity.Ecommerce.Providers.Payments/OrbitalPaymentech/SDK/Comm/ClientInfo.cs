#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
namespace JPMC.MSDK.Comm
{
    using System;
    using System.Net;
    using System.Threading;

    ///
    /// Title: Client Information
    ///
    /// Description: objects of this class hold information about a thread
    /// client that must be saved between calls to the interface methods
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 4.0
    ///
    /// objects of this class hold information about a thread
    /// client that must be saved between calls to the interface methods
    public class ClientInfo
    {
        private string localHost;
        private int localPort;

        // Properties

        /// <summary>
        /// The thread that owns this structure
        /// </summary>
        public Thread ThreadOwner { get; set; }

        /// <summary>
        /// The last time this client was used
        /// </summary>
        public long LastAccessTime { get; set; }

        /// <summary>
        /// whether this is the first time accessed
        /// </summary>
        public bool FirstAccess { get; set; }

        /// <summary>
        /// channel object used for this client
        /// </summary>
        public SocketChannelWrapper Channel { get; set; }

        /// <summary>
        /// The (remote) host name for this client
        /// </summary>
        public string Host { get; set; }

        /// <summary>
        /// The (remote) port used for this client
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// Counts the number of accessed
        /// </summary>
        public int AccessCounter { get; set; }

        /// <summary>
        /// Whether to encrypt the transmission
        /// </summary>
        public bool Encrypt { get; set; }

        /// <summary>
        /// Whether a thread is using this object or not
        /// </summary>
        public bool Busy { get; set; }

        /// <summary>
        /// Used by the destructor of communication modules (e.g. TCPConnect) to find
        /// this object when there are unmanaged resources (like connections) to
        /// recover.
        /// </summary>
        public object ObjectPtr { get; set; }

        /// <summary>
        /// The local side of the connection
        /// </summary>
        public string LocalHost
        {
            get
            {
                if ( localHost == null )
                {
                    try
                    {
                        localHost = CommUtils.GetIPAddress( Dns.GetHostName() ).ToString();
                    }
                    catch ( Exception )
                    {
                        localHost = null;
                    }
                }

                return localHost;
            }
            set => localHost = value;
        }

        /// <summary>
        /// Get the local port used by the socket
        /// </summary>
        public int LocalPort
        {
            get
            {
                if ( localPort == 0 )
                {
                    if ( Channel != null && Channel.Sock != null
                        && Channel.Sock.Connected )
                    {
                        var sock = Channel.Sock;
                        var iep = (IPEndPoint) sock.LocalEndPoint;

                        if ( iep != null )
                        {
                            localPort = iep.Port;
                        }
                    }
                }

                return localPort;
            }
        }

        // Constructors

        /// <summary>
        /// Constructor when all three values are known
        /// </summary>
        /// <param name="channel">object with socket used for this client</param>
        public ClientInfo( SocketChannelWrapper channel )
        {
            // This variable tells us if this is the first time
            // this client has been accessed.  This is
            // important because if it is the first time
            // we access the socket we want to give a
            // special error message
            FirstAccess = true;

            // access counter is the number reads or
            // writes before we change our special exception
            // message
            AccessCounter = 10;

            // Indicates whether a thread is using this object or not
            Busy = false;

            // Whether to encrypt transmission
            Encrypt = false;

            this.Channel = channel;
        }

        /// <summary>
        /// create empty one
        /// </summary>
        public ClientInfo() : this( null )
        {
        }

        /// <summary>
        /// firstAccess setter
        /// </summary>
        public void FirstAccessFalse()
        {
            if ( AccessCounter == 0 )
            {
                FirstAccess = false;
            }
            else
            {
                AccessCounter--;
            }
        }

        /// <summary>
        /// Checks to see whether the thread is still alive.  Normally if the
        /// thread is no longer alive, the socket should be closed and this
        /// object destroyed.
        /// </summary>
        /// <returns>true or false.</returns>
        public bool IsAlive()
        {
            var retVal = false;
            if ( ThreadOwner != null )
            {
                retVal = ThreadOwner.IsAlive;
            }
            return retVal;
        }

        /// <summary>
        /// Generic way of logging a connection
        /// </summary>
        /// <returns></returns>
        public string ConnStr()
        {
            var retVal = Host + ":" + Port;

            if ( LocalHost != null && LocalPort != 0 )
            {
                retVal = LocalHost + ":" + LocalPort + " <--> " + retVal;
            }

            return retVal;
        }
    }
}
