using log4net;
using JPMC.MSDK.Common;
using System;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
using System.Security.Authentication;
using System.Net.Security;
using System.Collections;
using System.Collections.Generic;
using System.Net;

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
/// Title: Socket Channel Wrappper
///
/// Description: A wrapper for the SocketChannel class
///
/// Copyright (c)2018, Paymentech, LLC. All rights reserved
///
/// Company: J. P. Morgan
///
/// @author Frank McCanna
/// @version 1.0
///

/// <summary>
/// A wrapper for the SocketChannel class. This allows all methods to be virtual and therefore able to be 
/// overridden in a derived stub class during testing.
/// </summary>
[System.Runtime.InteropServices.ComVisible(false)]
public class SocketChannelWrapper : WrapperBase<SocketChannelWrapper>
{
	private SocketChannel channel;

	/// <summary>
	/// We make this public only because it is required for Type argument in the generic class
	/// WrapperFactory. In reality we want everyone to use the factory (GetInstance) so this class 
	/// can be replaced with a stub when testing.
	/// </summary>
	public SocketChannelWrapper()
	{
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public virtual bool IsConnected()
	{
		var retVal = false;

		if ( channel != null )
		{
			retVal = channel.IsConnected();
		}
		
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	public static ILog Logger
	{
		get => SocketChannel.Logger;
        set => SocketChannel.Logger = value;
    }

	/// <summary>
	/// 
	/// </summary>
	public virtual int ReceiveTimeout
	{
		get
		{
			var retVal = 0;
			if ( channel != null )
			{
				retVal = channel.ReceiveTimeout;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.ReceiveTimeout = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual int SendTimeout
	{
		get
		{
			var retVal = 0;
			if ( channel != null )
			{
				retVal = channel.SendTimeout;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.SendTimeout = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual int ReceiveBufferSize
	{
		get
		{
			var retVal = 0;
			if ( channel != null )
			{
				retVal = channel.ReceiveBufferSize;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.ReceiveBufferSize = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual int SendBufferSize
	{
		get
		{
			var retVal = 0;
			if ( channel != null )
			{
				retVal = channel.SendBufferSize;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.SendBufferSize = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual bool NoDelay
	{
		get
		{
			var retVal = false;
			if ( channel != null )
			{
				retVal = channel.NoDelay;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.NoDelay = value;
			}
		}
	}

	/// <summary>
	/// The host name on the "other" side of the connection
	/// </summary>
	public virtual string RemoteHost
	{
		get
		{
			string retVal = null;

			if ( channel != null )
			{
				retVal = channel.RemoteHost;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.RemoteHost = value;
			}
		}
	}

	/// <summary>
	/// The port on the "other" side of the connection
	/// </summary>
	public virtual int RemotePort
	{
		get
		{
			var retVal = 0;
			if ( channel != null )
			{
				retVal = channel.RemotePort;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.RemotePort = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual Socket Sock
	{
		get
		{
			Socket retVal = null;
			if ( channel != null )
			{
				retVal = channel.Sock;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.Sock = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual SslStream SSLStream
	{
		get
		{
			SslStream retVal = null;
			if ( channel != null )
			{
				retVal = channel.SSLStream;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.SSLStream = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual bool Encrypt
	{
		get
		{
			var retVal = false;
			if ( channel != null )
			{
				retVal = channel.Encrypt;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.Encrypt = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual int Available
	{
		get
		{
			var retVal = 0;
			if ( channel != null && channel.Sock != null )
			{
				retVal = channel.Sock.Available;
			}
			return retVal;
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual string ServerCN
	{
		get
		{
			string retVal = null;
			if ( channel != null )
			{
				retVal = channel.ServerCN;
			}
			return retVal;
		}
		set
		{
			if ( channel != null )
			{
				channel.ServerCN = value;
			}
		}
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual X509Certificate2 ServerCertificate
	{
		get => SocketChannel.ServerCertificate;
        set
		{
			if ( channel != null )
			{
				SocketChannel.ServerCertificate = value;
			}
		}
	}
	
	/// <summary>
	/// 
	/// </summary>
	public virtual void Open()
	{
		channel = SocketChannel.Open( null, false, null );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sock"></param>
	public virtual void Open( Socket sock )
	{
		channel = SocketChannel.Open( sock, false, null );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="sock"></param>
	/// <param name="encrypt_data"></param>
	/// <param name="CN"></param>
	public virtual void Open( Socket sock, bool encrypt_data, 
		string CN )
	{
		channel = SocketChannel.Open( sock, encrypt_data, CN );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="wrapper"></param>
	/// <param name="timeOutSecs"></param>
	public virtual void Accept( SocketChannelWrapper wrapper, int timeOutSecs )
	{
		if ( channel != null )
		{
			wrapper.channel = channel.Accept( timeOutSecs );
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="timeout"></param>
	/// <returns></returns>
	public virtual SocketChannelWrapper Accept( int timeout )
	{
		var retVal = new SocketChannelWrapper();
		retVal.channel = channel.Accept( timeout );
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public virtual SocketChannelWrapper Accept()
	{
		return Accept( 0 );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="host"></param>
	/// <param name="port"></param>
	/// <param name="timeOutSecs"></param>
	/// <param name="connectLoopWaitMSecs"></param>
	/// <param name="failRetries"></param>
	/// <param name="retryReasons"></param>
	/// <param name="doBlocking"></param>
	public virtual void Connect( string host, int port, int timeOutSecs,
		int connectLoopWaitMSecs, int failRetries, 
		string [] retryReasons, bool doBlocking )
	{
		if ( channel != null )
		{
			channel.Connect( host, port, timeOutSecs,
				connectLoopWaitMSecs, failRetries,
				retryReasons, doBlocking );
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="host"></param>
	/// <param name="port"></param>
	/// <param name="timeOutSecs"></param>
	/// <param name="connectLoopWaitMSecs"></param>
	public virtual void Connect( string host, int port, int timeOutSecs,
		int connectLoopWaitMSecs )
	{
		if ( channel != null )
		{
			channel.Connect( host, port, timeOutSecs, 
				connectLoopWaitMSecs );
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="host"></param>
	/// <param name="port"></param>
	/// <param name="timeOutSecs"></param>
	public virtual void BlockingConnect( string host, int port, int timeOutSecs )
	{
		if ( channel != null )
		{
			channel.BlockingConnect( host, port, timeOutSecs );
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <returns></returns>
	public virtual bool CloseConnection()
	{
		var retVal = false;

		if ( channel != null )
		{
			retVal = channel.CloseConnection();
			channel.Sock = null;
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="timeoutSecs"></param>
	/// <param name="lingerSecs"></param>
	/// <returns></returns>
	public virtual bool CloseConnection( int timeoutSecs, int lingerSecs )
	{
		var retVal = false;

		if ( channel != null )
		{
			retVal = channel.CloseConnection( timeoutSecs, lingerSecs );
			channel.Sock = null;
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="which"></param>
	/// <param name="timeoutSecs"></param>
	/// <param name="lingerSecs"></param>
	/// <returns></returns>
	public virtual bool CloseConnection( SocketChannelWrapper which, 
		int timeoutSecs, int lingerSecs )
	{
		var retVal = false;
		if ( channel != null )
		{
			retVal = channel.CloseConnection( which.channel, 
				timeoutSecs, lingerSecs );
			channel.Sock = null;
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="buff"></param>
	/// <returns></returns>
	public virtual int Read( ByteBuffer buff )
	{
		var retVal = 0;
		if ( channel != null )
		{
			retVal = channel.Read( buff );
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="buff"></param>
	/// <returns></returns>
	public virtual long Write( ByteBuffer buff )
	{
		long retVal = 0;
		if ( channel != null )
		{
			retVal = channel.Write( buff );
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="outbuf"></param>
	/// <returns></returns>
	public virtual int WriteStream( ByteBuffer outbuf )
	{
		var retVal = 0;
		if ( channel != null )
		{
			retVal = channel.WriteStream( outbuf );
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="buff"></param>
	/// <returns></returns>
	public virtual int ReadStream( ByteBuffer buff )
	{
		var retVal = 0;
		if ( channel != null )
		{
			retVal = channel.ReadStream( buff );
		}
		return retVal;
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual bool Blocking
	{
		get => channel.Blocking;
        set => channel.Blocking = value;
    }

	/// <summary>
	/// a generic IList<Socket> version for elegant solutions
	/// </summary>
	/// <param name="readers"></param>
	/// <param name="writers"></param>
	/// <param name="errors"></param>
	/// <param name="microSecToWait"></param>
	public virtual void Select( IList<Socket> readers, IList<Socket> writers, 
		IList<Socket> errors, int microSecToWait )
	{
		channel.Select( readers, writers, errors, microSecToWait );
	}

	/// <summary>
	/// Have a non-generice IList version for maximum speed
	/// </summary>
	/// <param name="readers"></param>
	/// <param name="writers"></param>
	/// <param name="errors"></param>
	/// <param name="microSecToWait"></param>
	public virtual void Select( IList readers, IList writers, IList errors,
		int microSecToWait )
	{
		channel.Select( readers, writers, errors, microSecToWait );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="where"></param>
	public virtual void Bind( EndPoint where )
	{
		channel.Bind( where );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="backlog"></param>
	public virtual void Listen( int backlog )
	{
		channel.Listen( backlog );
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="microSecs"></param>
	/// <param name="mode"></param>
	/// <returns></returns>
	public virtual bool Poll( int microSecs, SelectMode mode )
	{
		return channel.Poll( microSecs, mode );
	}

	/// <summary>
	/// 
	/// </summary>
	public virtual void Close()
	{
		if ( channel != null )
		{
			channel.Close();
		}
	}

	/// <summary>
	/// 
	/// </summary>
	/// <param name="level"></param>
	/// <param name="optname"></param>
	/// <param name="lingerOption"></param>
	public virtual void SetSocketOption( SocketOptionLevel level,
			SocketOptionName optname, LingerOption lingerOption )
	{
		channel.SetSocketOption( level, optname, lingerOption );
	}
}
}
