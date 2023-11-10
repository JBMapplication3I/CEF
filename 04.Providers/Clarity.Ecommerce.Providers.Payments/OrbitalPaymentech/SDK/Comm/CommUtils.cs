#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using JPMC.MSDK.Common;
using log4net;


namespace JPMC.MSDK.Comm
{

    ///
    ///<summary>
    /// Title: Communication Utilities
    ///</summary>
    ///<remarks>
    /// Description: Description: This class contains common communication
    /// utilities used by multiple communication modules
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 3.0
    ///</remarks>
    public class CommUtils
{
    /// <summary>
    /// Length of an integer
    /// </summary>
    public const int INTLEN = 4;

    /// <summary>
    ///
    /// </summary>
    public const int SHORTLEN = 2;
    /// <summary>
    ///
    /// </summary>
    public const int PNS_HEADER_LEN = 6;

    /// <summary>
    /// Use common SDK logger by default, normally this will be reset
    /// </summary>
    private static ILog logger;
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
    /// This class should only be used as a static object so the constructor
    /// is private
    /// </summary>
    private CommUtils()
    {
    }

    /// <summary>
    /// Send a message with the proper header in the front of it
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="buf"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="writeLoopWaitMSecs"></param>
    /// <param name="mtype"></param>
    public static void SendMessage( SocketChannelWrapper channel, byte[] buf, int timeoutSecs,
        int writeLoopWaitMSecs, MessageType mtype )
    {
        if ( mtype == MessageType.PNS )
        {
            SendPNSMessage( channel, buf, timeoutSecs, writeLoopWaitMSecs );
        }
        else if ( mtype == MessageType.SLM )
        {
            SendSLMMessage( channel, buf, timeoutSecs, writeLoopWaitMSecs );
        }
    }

    /// <summary>
    /// This is for Salem messages which have a 4 byte big-endian length at the front
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="buf"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="writeLoopWaitMSecs"></param>
    public static void SendSLMMessage( SocketChannelWrapper channel, byte[] buf, int timeoutSecs,
        int writeLoopWaitMSecs )
    {
        var newBufLen = buf.Length + INTLEN;

        // get the 4 bytes with the length filled in
        byte[] numBytes = null;

        // Since this is Windows, it should be little endian but
        // let's make sure
        if ( BitConverter.IsLittleEndian )
        {
            numBytes = Utils.IntToBigEndianBytes( (uint) buf.Length );
        }
        else
        {
            // must already be in big-endian order so just get the bytes as they are
            numBytes = BitConverter.GetBytes( buf.Length );
        }

        var newbuf = ByteBuffer.Allocate( newBufLen );

        newbuf.Put( numBytes );
        newbuf.Put( buf );
        newbuf.Flip();

        // send everything all at once for efficiency
        CommUtils.WriteMinimum( channel, newbuf,
            timeoutSecs, writeLoopWaitMSecs );
    }

    /// <summary>
    /// This is a PNS message so has a 2 byte big-endian length followed by 4 zero bytes
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="buf"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="writeLoopWaitMSecs"></param>
    public static void SendPNSMessage( SocketChannelWrapper channel, byte[] buf, int timeoutSecs,
    int writeLoopWaitMSecs )
    {
        var newBufLen = buf.Length + PNS_HEADER_LEN;
        var mlen = (ushort) buf.Length;

        // get the 4 bytes with the length filled in
        byte[] numBytes = null;

        numBytes = BitConverter.GetBytes( mlen );

        // Since this is Windows, it should be little endian but
        // let's make sure
        if ( BitConverter.IsLittleEndian )
        {
            Utils.ReverseBytes( numBytes );
        }

        var newbuf = ByteBuffer.Allocate( newBufLen );

        newbuf.Put( numBytes );
        var magic = 0;

        // if the magic ever becomes an integer with varying byte values, we will have
        // to reverse their order. Since here all the bytes are the same, don't bother
        newbuf.Put( magic );
        newbuf.Put( buf );
        newbuf.Flip();

        // send everything all at once for efficiency
        CommUtils.WriteMinimum( channel, newbuf,
            timeoutSecs, writeLoopWaitMSecs );
    }

    /// <summary>
    /// Receive a message as a byte buffer
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="readLoopWaitMSecs"></param>
    /// <param name="mtype"></param>
    /// <returns></returns>
    public static ByteBuffer RecvMessage( SocketChannelWrapper channel, int timeoutSecs,
        int readLoopWaitMSecs, MessageType mtype )
    {
        ByteBuffer inbuf = null;
        var bufLength = 0;

        try
        {
            if ( mtype.Equals( MessageType.SLM ) )
            {
                bufLength = GetSLMMessageSize( channel,
                    timeoutSecs, readLoopWaitMSecs );
            }
            else if ( mtype.Equals( MessageType.PNS ) )
            {
                bufLength = GetPNSMessageSize( channel,
                    timeoutSecs, readLoopWaitMSecs );
            }
            else
            {
                throw new Exception( "Unsupported Message Type" );
            }
        }
        catch ( CommException ex )
        {
            if ( ex.ErrorCode == Error.ReadTimeoutFailure )
            {
                throw;
            }
            else
            {
                var sslmsg = "SSL";

                if ( channel.Encrypt )
                {
                    sslmsg = "non-" + sslmsg;
                }

                Logger.Warn(
                "Either a non-JPMC peer or an "
                + sslmsg +
                " configured JPMC peer has tried to establish a connection" );

                throw new CommException( Error.ReadFailure,
                    "Failure to read message size",  ex );
            }
        }

        inbuf = ByteBuffer.Allocate( bufLength );

        // Now get only one message, converting it to a string
        try
        {
            ReadMinimum( channel, inbuf,
                timeoutSecs, readLoopWaitMSecs );
        }
        catch ( Exception ex )
        {
            throw new CommException( Error.ReadFailure,
                "Failure to read message",  ex );
        }

        // return the string
        return inbuf;
    }

    /// <summary>
    /// This is method will not return until it gets the length or times out
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="timeOutSecs"></param>
    /// <param name="readLoopWaitMSecs"></param>
    /// <returns></returns>
    public static int GetSLMMessageSize( SocketChannelWrapper channel,
        int timeOutSecs,
        int readLoopWaitMSecs )
    {
        var retVal = 0;

        var buff = ByteBuffer.Allocate( INTLEN );

        // Now read the bytes for the length.  If ReadMinimum returns
        // > 0, and "remaining" is greater than zero it means we only
        // read part of the length
        var numRead = 0;

        numRead = ReadMinimum( channel, buff, timeOutSecs, readLoopWaitMSecs );

        if ( numRead > 0 && buff.Remaining() > 0 )
        {
            Logger.Debug( "Partial read of message length" );
        }
        // we succeeded in reading the message length
        else if ( buff.Remaining() == 0 )
        {
            buff.Flip();

            var bytes = buff.Array();

            // the protocol does not have a "magic" number so we
            // make the length 2 bytes more than we need and make
            // the two high order bytes a zero magic number
            if ( bytes[ 0 ] != 0 || bytes[ 1 ] != 0 )
            {
                Logger.Error(
                    "Bad length at start of message. It looks like: "
                    + Utils.ByteArrayToString( buff.Array() ) );

                throw new Exception(
                    "bad length at start of message" );
            }

            retVal = ( int ) ReverseBytesToInt( bytes );

            if ( retVal == 0 )
            {
                throw new CommException( Error.ReadFailure,
                    "Message with zero length value received" );
            }
        }

        return retVal;
    }
    /// <summary>
    /// This is method will not return until it gets the length or times out
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="timeOutSecs"></param>
    /// <param name="readLoopWaitMSecs"></param>
    /// <returns></returns>
    public static int GetPNSMessageSize( SocketChannelWrapper channel,
        int timeOutSecs, int readLoopWaitMSecs )
    {
        var retVal = 0;

        var buff = ByteBuffer.Allocate( PNS_HEADER_LEN );

        ReadMinimum( channel, buff, timeOutSecs, readLoopWaitMSecs );

        var bytes = buff.Array();

        // make sure we got the full header
        if ( bytes.Length != PNS_HEADER_LEN )
        {
            channel.CloseConnection();
            throw new CommException( Error.ArgumentMismatch,
                "6 byte header at start of message is missing" );
        }

        // the protocol does not have a "magic" number so we make the
        // reserved header bytes as the magic number
        if ( bytes[ 2 ] != 0 || bytes[ 3 ] != 0 || bytes[ 4 ] != 0 ||
            bytes[ 5 ] != 0 )
        {
            channel.CloseConnection();
            throw new CommException( Error.ArgumentMismatch,
                "PNS message header does not have a zero reserved field" );
        }

        // The length is a big-endian short so the first byte is the higher order
        // bits
        retVal |= bytes[ 0 ];

        // believe it or not there is sign extension because the byte (above) is first cast to an int
        // so lets mask off any sign extension
        retVal &= 0xff;

        // so shift these into the higher order byte
        retVal <<= 8;

        // this byte has all the low order bits so just OR them in
        // but mask off sign extension again
        retVal |= 0xff & ( int ) bytes[ 1 ];

        return retVal;
    }

    /// <summary>
    /// Reverse the order of bytes and make an int of it
    /// </summary>
    /// <param name="bytes"></param>
    /// <returns></returns>
    public static uint ReverseBytesToInt( byte[] bytes )
    {
        uint retVal = 0;
        uint holder = 0;

        for ( int i = 0, j = INTLEN - 1; i < INTLEN; i++, j-- )
        {
            holder = bytes[ i ];
            if ( j != 0 )
            {
                holder = holder << ( 8 * j );
            }

            retVal |= holder;
        }
        return retVal;
    }

    /// <summary>
    /// Read a minimum amount of data off a socket
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="inbuf"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="loopWaitMSecs"></param>
    /// <returns></returns>
    public static int ReadMinimum( SocketChannelWrapper channel, ByteBuffer inbuf,
        int timeoutSecs, int loopWaitMSecs  )
    {
        var startTime = Utils.GetCurrentMilliseconds();
        var total = 0;
        var stat = 0;

        while( true )
        {
            stat = ReadBuffer( channel, inbuf );

            if ( stat > 0 )
            {
                total += stat;
            }

            if ( inbuf.Remaining() == 0 )
            {
                break;
            }

            CheckTimeout( startTime,
                ( long ) timeoutSecs * 1000, true );

            try
            {
                channel.Sock.Poll( loopWaitMSecs,
                        SelectMode.SelectRead );
            }
            catch{}
        }
        return total;
    }

    /// <summary>
    /// write a minimum amount of data to a socket
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="outbuf"></param>
    /// <param name="timeoutSecs"></param>
    /// <param name="loopWaitMSecs"></param>
    public static void WriteMinimum( SocketChannelWrapper channel,
        ByteBuffer outbuf, int timeoutSecs, int loopWaitMSecs  )
    {
        var startTime = Utils.GetCurrentMilliseconds();

        while( true )
        {
            WriteBuffer( channel, outbuf );

            if ( outbuf.Remaining() == 0 )
            {
                break;
            }

            CheckTimeout( startTime,
                ( long ) timeoutSecs * 1000, false );

            try
            {
                channel.Sock.Poll( loopWaitMSecs,
                        SelectMode.SelectRead );
            }
            catch{}
        }
    }

    /// <summary>
    /// Read a ByteBuffer full of data
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="inbuf"></param>
    /// <returns></returns>
    public static int ReadBuffer( SocketChannelWrapper channel, ByteBuffer inbuf )
    {
        var readStat = 0;

        // now get the data
        try
        {
            if ( channel.Encrypt )
            {
                readStat = channel.ReadStream( inbuf );
            }
            else
            {
                readStat = channel.Read( inbuf );
            }

            // this is just end-of-file
            if ( readStat < 0 )
            {
                channel.CloseConnection();
            }
        }
        catch ( CommException )
        {
            throw;
        }
        catch ( TimeoutException )
        {
            throw new CommException(
                Error.ReadTimeoutFailure, "read timeout" );
        }
        catch ( ThreadAbortException )
        {
            throw;
        }
        catch ( Exception ex )
        {
            CommUtils.PrintException( ex );

            // if some bytes were read then
            // report the error because this is
            // a "data lost" scenario
            if ( inbuf.Position > 0 )
            {
                Logger.Warn(
                    "Get exception during read(): "
                    + ex.GetType() +
                    " with message: " +
                    ex.Message );

                Logger.Warn( "Data length is: " +
                    inbuf.Position +
                    " which is only part of message "
                    + "read. " );

                throw new CommException( Error.ReadFailure,
                    "\"data lost\" read failure.",
                    ex );
            }

            if ( ex is CommException )
            {
                throw;
            }
            else
            {
                throw new CommException( Error.ReadFailure,
                    "read failure.", ex );
            }
        }

        return readStat;
    }

    /// <summary>
    /// Write a byte buffer full of data
    /// </summary>
    /// <param name="channel"></param>
    /// <param name="outbuf"></param>
    /// <returns></returns>
    public static int WriteBuffer( SocketChannelWrapper channel,
        ByteBuffer outbuf )
    {
        var retVal = 0;

        var writeGoal = ( int ) outbuf.Remaining();

        try
        {
            if ( channel.Encrypt )
            {
                retVal = channel.WriteStream(
                    outbuf );
            }
            else
            {
                retVal = ( int ) channel.Write( outbuf );
            }

            if ( outbuf.Remaining() != 0 && channel.Sock.Blocking )
            {
                Logger.Debug( " bytes written: " + retVal +
                    " write goal: " + writeGoal );

                //Console.WriteLine( " bytes written: " + retVal +
                        //" write goal: " + writeGoal );
                channel.CloseConnection();
                throw new CommException( Error.WriteFailure,
                    "Partial write to socket" );
            }
        }
        catch ( CommException )
        {
            throw;
        }
        catch ( TimeoutException )
        {
            throw new CommException(
                Error.WriteTimeoutFailure,
                "write timeout" );
        }
        catch ( SocketException ex )
        {
            channel.CloseConnection();

            if ( ex.ErrorCode == ( int ) SocketError.TimedOut )
            {
                throw new CommException(
                    Error.WriteTimeoutFailure,
                    "write to socket timed out.", ex );
            }
            else
            {
                throw new CommException(
                    Error.WriteFailure,
                    "write to socket failed.", ex );
            }
        }
        catch ( Exception ex )
        {
            PrintException( ex );
            channel.CloseConnection();
            throw new CommException(
                Error.WriteFailure,
                "write to socket failed.", ex );
        }
        return retVal;
    }

    /// <summary>
    /// See if the contents of two byte arrays are equal
    /// </summary>
    /// <param name="suspect"></param>
    /// <param name="testb"></param>
    /// <returns></returns>
        public static bool BytesEqual( byte [] suspect, byte [] testb )
        {
                return BytesEqual( suspect, 0, testb );
        }

        /// <summary>
    /// Compare (potentially) part of a byte array with another byte array
        /// </summary>
        /// <param name="suspect"></param>
        /// <param name="index"></param>
        /// <param name="testb"></param>
        /// <returns></returns>
        public static bool BytesEqual( byte [] suspect, int index, byte [] testb )
        {
                var retVal = false;

                if ( suspect.Length - index == testb.Length )
                {
                        retVal = true;

                        for ( int i=0,j=index; j < suspect.Length; i++,j++ )
                        {
                                if ( suspect[ j ] != testb[ i ] )
                                {
                                        retVal = false;
                                        break;
                                }
                        }
                }
                return retVal;
        }

    /// <summary>
    /// Do the timeout calculations
    /// </summary>
    /// <param name="startTime"></param>
    /// <param name="timeout"></param>
    /// <param name="isRead"></param>
    public static void CheckTimeout( long startTime, long timeout,
            bool isRead )
    {
        // if we have a timeout
        if ( timeout > 0 )
        {
            // check to see if we timed out
            var elapsedTime = Utils.GetCurrentMilliseconds() -
                startTime;

            var remainingTime = timeout - elapsedTime;

            if ( remainingTime <= 0 )

            {
                if ( isRead )
                {
                    throw new CommException(
                        Error.ReadTimeoutFailure,
                        "read timeout" );
                }
                else
                {
                    throw new CommException(
                        Error.WriteTimeoutFailure,
                        "write timeout" );
                }
            }
        }
    }

    ///
    /// <summary>
    /// Compare (potentially) part of a byte array with another byte array
    /// </summary>
    /// <param name="suspect"></param>
    /// <param name="index"></param>
    /// <param name="testb"></param>
    /// <returns></returns>
    public static bool BytesStartWith( byte [] suspect, int index, byte [] testb )
    {
        var retVal = false;

        if ( suspect != null && testb != null )
        {
            retVal = true;

            for ( int i=0,j=index; j < suspect.Length
                && i < testb.Length; i++,j++ )
            {
                if ( suspect[ j ] != testb[ i ] )
                {
                    retVal = false;
                    break;
                }
            }
        }
        return retVal;
    }

    /// <summary>
    /// In a byte array, replacd all instances of one byte with another
    /// </summary>
    /// <param name="victim"></param>
    /// <param name="replacee"></param>
    /// <param name="replacer"></param>
    public static byte [] ReplaceBytes( byte [] victim, char replacee, char replacer )
    {
        var replaceeStr = "" + replacee;
        var replacerStr = "" + replacer;

        var replaceeByteArray = Utils.StringToByteArray( replaceeStr );
        var replacerByteArray = Utils.StringToByteArray( replacerStr );

        var replaceeByte = replaceeByteArray[ 0 ];
        var replacerByte = replacerByteArray[ 0 ];

        for ( var i=0; i < victim.Length; i++ )
        {
            if ( victim[ i ] == replaceeByte )
            {
                victim[ i ] = replacerByte;
            }
        }
        return victim;
    }

    /// <summary>
    /// Check to see if string is in array of strings
    /// </summary>
    /// <param name="queryStr"></param>
    /// <param name="testArray"></param>
    /// <returns></returns>
    public static bool StringInArray( string queryStr, string [] testArray )
    {
        var retVal = false;

        if ( queryStr != null && testArray != null )
        {
            for ( var i=0; i < testArray.Length; i++ )
            {
                if ( queryStr == testArray[ i ] )
                {
                    retVal = true;
                    break;
                }
            }
        }
        return retVal;
    }

    /// <summary>
    /// check to see if a value is in a list
    /// </summary>
    /// <param name="list"></param>
    /// <param name="val"></param>
    /// <returns></returns>
    public static bool RemoveValueInList( IList list, object val )
    {
        var retVal = false;

        foreach ( var next in list )
        {
            if ( next == val )
            {
                list.Remove( next );
                retVal = true;
                break;
            }
        }

        return retVal;
    }

    /// <summary>
    /// Take a string with multiple strings separated by white space and
    /// return an array of strings
    /// </summary>
    /// <param name="source"></param>
    /// <returns></returns>
    public static string [] StringToStringArray( string source )
    {
        string [] retVal = null;
        string [] holder = null;
        char [] separators = { ' ', '\t', '\r', '\n' };

        if ( source != null )
        {
            var tmp = source.Split( separators );
            var count=0;

            for ( var i=0; i < tmp.Length; i++  )
            {
                var tmpstr = tmp[ i ].Trim();

                if ( tmpstr != null && tmpstr.Length > 0 )
                {
                    count++;
                }
            }

            if ( count > 0 )
            {
                holder = new string[ count ];

                for ( int i=0,j=0; i < tmp.Length; i++ )
                {
                    var tmpstr = tmp[ i ].Trim();

                    if ( tmpstr != null && tmpstr.Length > 0 )
                    {
                        holder[ j++ ] = tmpstr;
                    }
                }
                retVal = holder;
            }
        }
        return retVal;
    }

    /// <summary>
    ///  Parses a X.500 distinguished name for the value of the
    /// "Common Name" field.
    /// This is done a bit sloppy right now and should probably be done a bit
    /// more according to <code>RFC 2253</code>.
    /// </summary>
    /// <param name="dn"></param>
    /// <returns></returns>
    public static string GetCN( string dn )
    {
        var i = 0;
        i = dn.IndexOf( "CN=" );

        if ( i == -1 )
        {
            return null;
        }
        //get the remaining DN without CN=
        dn = dn.Substring( i + 3 );

        var dncs = dn.ToCharArray();

        for ( i = 0; i < dncs.Length; i++ )
        {
            if ( dncs[ i ] == ',' && i > 0 && dncs[ i - 1 ] != '\\' )
            {
                break;
            }
        }
        return dn.Substring( 0, i );
    }

    /// <summary>
    /// This method calls the IPAddress.Parse method to check the ipAddress
    /// input string. If the ipAddress argument represents a syntatically correct IPv4 or
    /// IPv6 address, the method displays the Parse output into quad-notation or
    /// colon-hexadecimal notation, respectively. Otherwise, it displays an
    /// error message.
    /// </summary>
    /// <param name="ipAddress"> IP address as a string </param>
    /// <returns> IP address as IPAdress object .</returns>
    public static IPAddress ParseIPAddress( string ipAddress )
    {
        IPAddress retVal = null;

        try
        {
            // Create an instance of IPAddress for the specified address string (in
            // dotted-quad, or colon-hexadecimal notation).
            var address = IPAddress.Parse(ipAddress);
            if ( address.AddressFamily == AddressFamily.InterNetwork )
            {
                retVal = address;
            }
        }
        catch( ArgumentNullException e )
        {
            Logger.Debug("ArgumentNullException caught!!!");
            Logger.Debug("Source : " + e.Source);
            Logger.Debug("Message : " + e.Message);
        }
        catch( FormatException )
        {
            // This is the normal exception if a host name is passed so we ignore it.
            //Logger.Debug("FormatException caught!!!");
            //Logger.Debug("Source : " + e.Source);
            //Logger.Debug("Message : " + e.Message);
        }
        catch( Exception e )
        {
            Logger.Debug("Exception caught!!!");
            Logger.Debug("Source : " + e.Source);
            Logger.Debug("Message : " + e.Message);
        }

        return retVal;
    }

    /// <summary>
    /// Convert a host name to an IP address
    /// </summary>
    /// <param name="hostname">the host name</param>
    /// <returns>The IP address for the specified host.</returns>
    public static IPAddress GetIPAddress( string hostname )
    {
        // check to see if hostname is already an IP address
        var retVal = ParseIPAddress( hostname );

        // if hostname was not an IP address (already)
        if ( retVal == null )
        {
            // convert host name string to an IP address
            try
            {
                // Get server related information.
                var heserver = Dns.GetHostEntry( hostname );

                // Loop on the AddressList
                foreach (var curAdd in heserver.AddressList)
                {
                    // Display the type of address family supported by the server. If the
                    // server is IPv6-enabled this value is: InternNetworkV6. If the server
                    // is also IPv4-enabled there will be an additional value of InterNetwork.

                    if ( curAdd.AddressFamily == AddressFamily.InterNetwork )
                    {
                        retVal = curAdd;
                        break;
                    }
                }
            }
            catch (Exception e)
            {
                Logger.Warn("[DoResolve] Exception: " + e);
            }
        }

        return retVal;
    }

    /// <summary>
    /// When logging level is set at DEBUG, this method will print all the
    /// details of an exception
    /// </summary>
    /// <param name="origEx">exception to print</param>
    public static string PrintException( Exception origEx )
    {
        // save some work if we can't print anyway
        if ( Logger != null && ! Logger.IsDebugEnabled )
        {
            return "";
        }

        var ex = origEx;
        var beginning = "\nSTART PrintException: " + ex.GetType().Name;
        var middle = new StringBuilder("");

        // label the top exception
        var start = "Exception: ";
        while ( ex != null )
        {
            middle.Append( PrintOneException( ex, start, "\n\tEnd of " ) );

            // label all the inner exceptions
            ex = ex.InnerException;
            start = "Inner Exception: ";
        }

        // do the base separately so it is clear
        ex = origEx.GetBaseException();

        if ( ex != null )
        {
            // the base should have also been one of the inner ones so
            // should already be printed.  So just display the name and id
            middle.Append( "\n\tBase Exception: " + ex.GetType().Name
                + "( " + ex.GetHashCode() + " )" );
        }

        var outStr = beginning + middle + "\nEND PrintException: " +
                origEx.GetType().Name;
        if ( Logger == null )
        {
            Console.WriteLine( outStr  );
        }
        else
        {
            Logger.Debug( outStr );
        }

        return outStr;
    }

    /// <summary>
    /// Use this when we don't want to print inner exceptions
    /// </summary>
    /// <param name="ex">exception to print</param>
    /// <param name="start">output this string first</param>
    /// <param name="end">output this string after</param>
    /// <returns></returns>
    public static string PrintOneException( Exception ex, string start, string end )
    {
        var name = ex.GetType().Name;
        var id = ex.GetHashCode();

        var outString = new StringBuilder( "\n\t" + start + name + "( " + id + " ): " +
            "\n\tMessage: " + ex.Message +
            "\n\tSource: " + ex.Source +
            "\n\tTarget: " + ex.TargetSite );

        if ( ex is WebException )
        {
            var wex = ( WebException ) ex;
            outString.Append( "\n\tStatus: " + wex.Status +
                "\n\tStatus Value: " + ( int ) wex.Status );

            outString.Append( PrintResponse( wex.Response ) );
        }

        if ( ex is SocketException )
        {
            var sex = ( SocketException ) ex;
            outString.Append( "\n\tErrorCode: " + sex.ErrorCode
                + "\n\tNativeErrorCode: " + sex.NativeErrorCode
                + "\n\tSocketErrorCode: " + sex.SocketErrorCode
                );
        }

        if ( ex.StackTrace != null )
        {
            outString.Append( "\n\tStack Trace:\n" + ex.StackTrace );
        }

        outString.Append( "\n\t" + end + name + "( " + id + " )" );

        return outString.ToString();
    }

    /// <summary>
    /// This method prints the status and header info in a WebResponse object
    /// </summary>
    /// <param name="resp"></param>
    /// <returns></returns>
    public static string PrintResponse( WebResponse resp )
    {
        var outString = new StringBuilder("");

        if ( resp != null && resp is HttpWebResponse )
        {
            var httpResponse = ( HttpWebResponse ) resp;

            outString.Append( "\n\tStart HttpWebResponse" );
            outString.Append( "\n\tStatusCode: " + httpResponse.StatusCode +
                "\n\tStatusDescription: " + httpResponse.StatusDescription );

            // Get the headers associated with the response.
            var hdr = httpResponse.Headers;

            if ( hdr != null )
            {
                outString.Append( "\n\tHeader values: " );
                var keys = hdr.GetEnumerator();

                while ( keys.MoveNext() )
                {
                    var key = ( string ) keys.Current;

                    outString.Append( "\n\t\t" + key + " = " + hdr.Get( key ) );
                }
            }
            outString.Append( "\n\tEnd HttpWebResponse" );
        }
        return outString.ToString();
    }

    /// <summary>
    /// Gets the IP address of the local machine
    /// </summary>
    /// <returns></returns>
    public static string GetIPAddress()
    {
        string retVal = null;

        var adapters = NetworkInterface.GetAllNetworkInterfaces();

        foreach ( var adapter in adapters )
        {
            if ( adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                adapter.OperationalStatus == OperationalStatus.Up )
            {
                var adapterProperties = adapter.GetIPProperties();
                var uniCast = adapterProperties.UnicastAddresses;
                if ( uniCast.Count > 0 )
                {
                    //Console.WriteLine( adapter.Description );
                    foreach ( IPAddressInformation uni in uniCast )
                    {
                        if ( uni.Address.AddressFamily == AddressFamily.InterNetwork )
                        {
                            retVal = uni.Address.ToString();
                            break;
                        }
                    }
                }
                if ( retVal != null )
                {
                    break;
                }
            }
        }
        return retVal;
    }

    /// <summary>
    /// For debugging, dump data to a file
    /// </summary>
    /// <param name="fname"></param>
    /// <param name="data"></param>
    /// <param name="append"></param>
    public static void DumpToFile(string fname, byte[] data, bool append )
    {
        FileStream ostream = null;

        if ( !append )
        {
            try
            {

            }
            catch ( IOException ex )
            {
                Logger.Warn( "Could not find file: " + fname );
                Logger.Error( ex.ToString() );
            }
        }

        try
        {
            ostream = new FileStream( fname, append  ? FileMode.Append : FileMode.Create,
                FileAccess.Write, FileShare.None );

            ostream.Write( data, 0, data.Length );
        }

        catch ( IOException ex )
        {
            Logger.Warn( "Error writing to file: " + fname );
            Logger.Error( ex.ToString() );
        }
        finally
        {
            if ( ostream != null )
            {
                try
                {
                    ostream.Close();
                }
                catch ( IOException e )
                {
                    // TODO Auto-generated catch block
                    Console.WriteLine( e.StackTrace );
                }
            }
        }
    }
}
}
