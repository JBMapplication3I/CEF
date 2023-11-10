#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using JPMC.MSDK.Common;

namespace JPMC.MSDK.Comm
{

    /// Title: Byte Buffer
    ///
    /// Description: abbreviated version of java's ByteBuffer
    ///
    /// Copyright (c)2018, Paymentech, LLC. All rights reserved
    ///
    /// Company: J. P. Morgan
    ///
    /// @author Frank McCanna
    /// @version 4.0
    ///

    /// <summary>
    /// A class that encapsulates access to a byte array with
    /// behavior similar to the Java ByteBuffer class
    /// </summary>
    public class ByteBuffer
{
        private byte[] bytes;
        private ulong position;
        private ulong limit;
        private ulong capacity;
        private const int numIntBytes = 4;

    //Properties
    /// <summary>
    /// byte array where the data resides
    /// </summary>
    public byte [] Bytes => bytes;

    /// <summary>
    /// index to next byte to either put or get
    /// </summary>
    public ulong Position
    {
        get => position;
        set => position = value;
    }

    /// <summary>
    /// maximum number of data bytes in ByteBuffer, different from capacity
    /// which is the actual number of bytes in the backing byte array
    /// </summary>
    public ulong Limit => limit;

    // Constructors
    /// <summary>
    /// Just create an empty byte buffer of so many bytes
    /// </summary>
    /// <param name="size"></param>
    public ByteBuffer( ulong size )
    {
        bytes = new byte[ size ];
        capacity = limit = size;
    }

    /// <summary>
    /// convenience with uint
    /// </summary>
    /// <param name="size"></param>
    public ByteBuffer( uint size ) : this( (ulong) size ) {}

    /// <summary>
    /// convenience with int
    /// </summary>
    /// <param name="size"></param>
    public ByteBuffer( int size ) : this( (ulong) size ) {}

    /// <summary>
    /// The java version has an Allocate method so lets do it also
    /// </summary>
    /// <param name="size"></param>
    /// <returns></returns>
    public static ByteBuffer Allocate( int size )
    {
        return new ByteBuffer( size );
    }

    //

    /// <summary>
    /// this is used for creating a ByteBuffer that is
    /// backed by the buffer specified ( eliminates need to
    /// copy bytes ).  We make a new buffer if the array given is different
    /// </summary>
    /// <param name="buff">actual byte array</param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    public ByteBuffer( byte[] buff, ulong offset, ulong length )
    {
        if ( ( ulong ) buff.Length < offset + length )
        {
            throw new CommException( Error.ArgumentMismatch,
                "Length of byte array " + buff.Length +
                " must not be smaller than offset + length: " + ( offset + length ) );
        }

        // when we get the special case that offset is not
        // zero, we copy the bytes into a new buffer.
        // This is different than java ByteBuffer but
        // is a rare situation and makes things a whole lot
        // simpler
        if ( offset != 0 || ( ulong ) buff.Length != length )
        {
            var newbuf = new byte[ length ];
            for ( ulong i=0,j=offset; i < length
                && j < (ulong) buff.Length ; i++, j++ )
            {
                newbuf[ i ] = buff[ j ];
            }
            bytes = newbuf;
        }
        else // just back the buffer with what we were given
        {
            bytes = buff;
        }

        capacity = limit = length;
    }

    /// <summary>
    /// convenience versions of the Wrap() method allowing for no offset or length
    /// and integer arguments
    /// </summary>
    /// <param name="buff"> source bytes </param>
    /// <param name="offset"> offset into the source byte array to start copying</param>
    /// <param name="length"> number of bytes to copy from source byte array</param>
    /// <returns></returns>
    public static ByteBuffer Wrap( byte[] buff, int offset, int length )
    {
        return new ByteBuffer( buff, (ulong) offset, (ulong) length );
    }

    /// <summary>
    /// Convenience when we just want to wrap the whole array
    /// </summary>
    /// <param name="buff"></param>
    /// <returns></returns>
    public static ByteBuffer Wrap( byte[] buff )
    {
        return new ByteBuffer( buff, (ulong) 0, (ulong) buff.Length );
    }

    // many of these could be properties but we will keep them as methods to keep things
    // closer to how java is.  This will make C# and java versions (and the code that uses
    // them more interchangeable

    /// <summary>
    /// java version has this method so do it also
    /// </summary>
    /// <returns></returns>
    public byte [] Array()
    {
        return bytes;
    }

    /// <summary>
    /// Puting a ByteBuffer is a little different because it changes the
    /// byte buffer you pass by "reading" from it.
    /// </summary>
    /// <param name="buff"></param>
    public void Put( ByteBuffer buff )
    {
        var guts = buff.Array();
        var remainder = Remaining() < buff.Remaining() ? Remaining() : buff.Remaining();
        Put( guts, buff.Position, buff.Remaining() );

        // the position of the argument buffer is incremented also
        buff.position += remainder;
    }

    /// <summary>
    /// Put just a single byte
    /// </summary>
    /// <param name="single"></param>
    public void Put( byte single )
    {
        var holder = new byte[ 1 ];
        holder[ 0 ] = single;
        Put( holder, 0, 1 );
    }

    /// <summary>
    /// Get just a single byte
    /// </summary>
    /// <returns></returns>
    public byte Get()
    {
        if ( position + 1 > limit )
        {
            throw new CommException( Error.ArgumentMismatch, "Adding 1 to current position: "
                + position + " is beyond limit" );
        }
        return bytes[ position++ ];
    }

    /// <summary>
    /// Main method for copying bytes into the ByteBuffer
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    public void Put( byte[] buff, ulong offset, ulong length )
    {
        if ( (ulong) buff.Length < offset + length )
        {
            throw new CommException( Error.ArgumentMismatch,
                "Length of byte array " + buff.Length +
                " must not be smaller than offset + length: " + ( offset + length ) );
        }
        for ( ulong j=0; position < limit && j + offset
            < ( ulong ) buff.Length; position++,j++ )
        {
            bytes[ position ] = buff[ offset + j ];
        }
    }

    // Convenience versions supporting different types of parameters
    /// <summary>
    ///
    /// </summary>
    /// <param name="buff"></param>
    /// <param name="offset"></param>
    /// <param name="length"></param>
    public void Put( byte[] buff, int offset, int length )
    {
        Put( buff, ( ulong ) offset, ( ulong ) length );
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="buff"></param>
    public void Put( byte[] buff )
    {
        Put( buff, ( ulong ) 0, ( ulong ) buff.Length );
    }
    /// <summary>
    ///
    /// </summary>
    /// <param name="str"></param>
    public void Put( string str )
    {
        Put( System.Text.ASCIIEncoding.ASCII.GetBytes( str ),
            ( ulong ) 0, ( ulong ) str.Length );
    }

    /// <summary>
    /// Always put in little endian byte order
    /// </summary>
    /// <param name="intVal"></param>
    public void Put( int intVal )
    {
        var avar = BitConverter.GetBytes( intVal );

        if ( ! BitConverter.IsLittleEndian )
        {
            Utils.ReverseBytes( avar );
        }

        Put( avar );
    }

    /// <summary>
    /// put in little endian order
    /// </summary>
    /// <param name="shortVal"></param>
    public void Put( short shortVal )
    {
        if ( BitConverter.IsLittleEndian )
        {
            Put( BitConverter.GetBytes( shortVal ) );
        }
        else
        {
            var highval = (byte) ( shortVal >> 8 );
            var lowval = (byte) ( shortVal & 0xff );
            Put( highval );
            Put( lowval );
        }
    }

    /// <summary>
    /// Sometime we just need to move the position without actually getting or putting
    /// </summary>
    /// <param name="inc"></param>
    public void AddPosition( ulong inc )
    {
        if ( position + inc > limit )
        {
            throw new CommException( Error.ArgumentMismatch, "Adding " + inc +
                " to current position: " + position + " is beyond limit" );
        }
        position += inc;
    }

    /// <summary>
    /// Calculated from position and limit
    /// </summary>
    /// <returns></returns>
    public ulong Remaining()
    {
        return limit - position;
    }

    /// <summary>
    /// Change from "used up" to "fresh"
    /// </summary>
    public void Flip()
    {
        limit = position;
        position = 0;
    }

    /// <summary>
    /// Gets properly sized byte array of data from a ByteBuffer
    /// </summary>
    /// <returns></returns>
    public byte [] ExtractBytes()
    {
        byte [] retVal = null;

        if ( Position > 0 )
        {
            var src = bytes;
            retVal = new byte[ Position ];

            for ( var i=0; i < ( int ) Position; i++ )
            {
                retVal[ i ] = src[ i ];
            }
        }
        return retVal;
    }

    /// <summary>
    /// Converts the byte buffer to a string.
    /// </summary>
    /// <returns></returns>
    override public string ToString()
    {
        string retVal = null;

        if ( bytes != null )
        {
            retVal = Utils.ByteArrayToString( bytes );
        }
        return retVal;
    }
}
}
