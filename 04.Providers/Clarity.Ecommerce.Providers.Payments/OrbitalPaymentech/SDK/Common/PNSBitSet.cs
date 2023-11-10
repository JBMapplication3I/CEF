#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

// Disables warnings for XML doc comments.
#pragma warning disable 1591
#pragma warning disable 1573
#pragma warning disable 1572
#pragma warning disable 1571
#pragma warning disable 1587
#pragma warning disable 1570


namespace JPMC.MSDK.Common
{

    /// <summary>
    /// Create a BitSet class that provides additional functionality and
    /// uses a bit index that starts at 1 (instead of zero) for the first bit
    /// The string methods also display the nibbles from left to right (little endian)
    /// but each nibble (4 bits) is displayed from right to left (big endian)
    /// </summary>
    public class PNSBitSet
{
    /// <summary>
    /// For PNS, it is always 64 bits
    /// </summary>
    private static readonly int numBits = 64;

    /// <summary>
    /// Internal data structure that holds the bits
    /// </summary>
    private BitArray ba = new BitArray( numBits );

    private PNSBitSet secondaryBitmap;
    private bool allowSecondaryBitmap = true;

    private PNSBitSet tertiaryBitmap;

    /// <summary>
    /// Create an empty Bit Set
    /// </summary>
    public PNSBitSet(){}

    /// <summary>
    /// Alternate constructor that takes bytes with bits set that should be set
    /// in the BitSet
    /// </summary>
    /// <param name="values">an array of bytes with bit values set</param>
    /// <param name="offset">where to start looking in the array</param>
    /// <param name="length">how many bytes in the array to use</param>
    public PNSBitSet( byte [] values, int offset,
        int length )
    {
        if ( length * 8 > numBits )
        {
            throw new Exception( " length is too long" );
        }

        var holder = new byte[ length ];

        for ( var i=0; i < length; i++ )
        {
            holder[ i ] = values[ i + offset ];
        }

        IsoBitMapToBitSet( holder );
    }

    public void AddSecondaryBitmap( byte[] values, int offset, int length )
    {
        secondaryBitmap = new PNSBitSet( values, offset, length );
    }

    /// <summary>
    ///
    /// </summary>
    public static int NumberOfBits => numBits;

    public bool AllowSecondaryBitmap
    {
        get => allowSecondaryBitmap;
        set => allowSecondaryBitmap = value;
    }

    /// <summary>
    /// The following methods provide the same functionality as all the
    /// base class's (BitSet) methods that have a bit index as a parameter.
    /// These methods allow the bit index to be "one" based (first bit
    /// is referenced with a 1 instead of a 0 ).  All methods have the
    /// same name as the base class equivalent except these method names
    /// are all prefixed with the letter 's'
    /// </summary>
    /// <param name="bitIndex">which bit, first one is 1 not 0</param>
    /// <returns>returns the value of the bit.</returns>
    public bool Get( int bitIndex )
    {
        if ( secondaryBitmap != null && bitIndex > numBits && bitIndex <= numBits * 2 )
        {
            return secondaryBitmap.Get( bitIndex - numBits );
        }
        else if ( tertiaryBitmap != null && bitIndex > numBits * 2 )
        {
            return tertiaryBitmap.Get( bitIndex - numBits * 2 );
        }
        return ba.Get( bitIndex - 1 );
    }

    /// <summary>
    /// Set bit to true
    /// </summary>
    /// <param name="bitIndex">which bit, first one is 1 not 0</param>
    public void Set( int bitIndex )
    {
        if ( AllowSecondaryBitmap && bitIndex > numBits && bitIndex < numBits * 2 )
        {
            if ( secondaryBitmap == null )
            {
                secondaryBitmap = new PNSBitSet();
                secondaryBitmap.AllowSecondaryBitmap = false;
            }
            secondaryBitmap.Set( bitIndex - numBits );
            ba.Set( 0, true );
        }
        else if ( AllowSecondaryBitmap && bitIndex > numBits * 2 )
        {
            if ( tertiaryBitmap == null )
            {
                tertiaryBitmap = new PNSBitSet();
                tertiaryBitmap.AllowSecondaryBitmap = false;
            }
            tertiaryBitmap.Set( bitIndex - numBits * 2 );
            secondaryBitmap.Set( 1 );
        }
        else
        {
            ba.Set( bitIndex - 1, true );
        }
    }

    /// <summary>
    /// Just like the base class's toString() but specify bit numbers as
    /// one based (first bit is reference with a 1 instead of a 0 as in
    /// BitSet class
    /// </summary>
    /// <returns>string representation of the Bit Set.</returns>
    override public string ToString()
    {
        var retVal = new StringBuilder();
        var firstOne = true;

        retVal.Append( '{' );

        for ( var i=1; i <= ba.Length; i++ )
        {
            if ( Get( i ) )
            {
                if ( ! firstOne )
                {
                    retVal.Append(',');
                    retVal.Append(' ');
                }
                else
                {
                    firstOne = false;
                }

                retVal.Append( i );
            }
        }
        retVal.Append( '}' );
        return retVal.ToString();
    }

    /// <summary>
    /// Specifically create an ISO hex string from this BitSet
    /// </summary>
    /// <returns>a hex string in PNS iso format.</returns>
    public string BitSetToIsoHex()
    {
        var retVal = new StringBuilder();

        for ( var i=0; i < ba.Length; i+=4 )
        {
            // get each hex character bits
            var holder = MakeNibble( i );

            // reverse the bits
            var reverse = ReverseNibble( holder );

            // tack on the new hex digit in opposite order
            retVal.Append( ValueToHexChar( reverse ) );
        }

        var result = retVal.ToString().ToUpper();
        if ( secondaryBitmap != null )
        {
            result += secondaryBitmap.BitSetToIsoHex();
        }
        return result;
    }

    /// <summary>
    /// Get the set of bytes with the bits set
    /// </summary>
    /// <returns>a byte array representation of the bits.</returns>
    public byte [] BitSetToBytes()
    {
        var retVal = new byte[ ba.Length / 8 ];

        for ( int i=0,j=0; i < ba.Length; i+=8, j++ )
        {
            // get each hex character bits
            var holder = MakeReverseByte( i );

            retVal[ j ] = holder;
        }

        if ( secondaryBitmap == null )
        {
            return retVal;
        }
        else
        {
            var list = new List<byte>( retVal );
            list.AddRange( secondaryBitmap.BitSetToBytes() );
            if ( tertiaryBitmap != null )
            {
                list.AddRange( tertiaryBitmap.BitSetToBytes() );
            }
            return list.ToArray();
        }
    }


    /// <summary>
    /// Returns the bytes with the bits in reverse order
    /// </summary>
    /// <param name="index">index into internal byte array, first bit is zero</param>
    /// <returns>byte with 8 bits in reverse order.</returns>
    private byte MakeReverseByte( int index )
    {
        byte retVal = 0;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 128;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 64;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 32;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 16;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 8;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 4;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 2;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 1;
        return retVal;
    }


    /// <summary>
    /// Create a nibble value (4 bits) from the index position in
    /// this BitSet
    /// </summary>
    /// <param name="index">bit array index, first is zero</param>
    /// <returns>byte with 4 bits set to values from the bit array.</returns>
    public byte MakeNibble( int index )
    {
        byte retVal = 0;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 1;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 2;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 4;
        if ( index < ba.Length && ba.Get( index++ ) ) retVal |= 8;
        return retVal;
    }

    /// <summary>
    /// Load an ISO bit map into this BitSet
    /// </summary>
    /// <param name="values">values from an ISO bit map</param>
    public void IsoBitMapToBitSet( byte [] values )
    {
        for ( var i=0; i < values.Length; i++ )
        {
            var holder = values[ i ];

            if ( ( holder & 1 ) == 1 ) ba.Set( i * 8 + 7, true );
            if ( ( holder & 2 ) == 2 ) ba.Set( i * 8 + 6, true );
            if ( ( holder & 4 ) == 4 ) ba.Set( i * 8 + 5, true );
            if ( ( holder & 8 ) == 8 ) ba.Set( i * 8 + 4, true );
            if ( ( holder & 16 ) == 16 ) ba.Set( i * 8 + 3, true );
            if ( ( holder & 32 ) == 32 ) ba.Set( i * 8 + 2, true );
            if ( ( holder & 64 ) == 64 ) ba.Set( i * 8 + 1, true );
            if ( ( holder & 128 ) == 128 ) ba.Set( i * 8, true );
        }
        return;
    }

    /// <summary>
    /// put nibble's bits in opposite order
    /// </summary>
    /// <param name="val">byte with low order 4 bits set/clear</param>
    /// <returns>low order 4 bits in reverse order in byte.</returns>
    public static byte ReverseNibble( byte val )
    {
        byte retVal = 0;

        // brute force but clear
        if ( ( val & 1 ) == 1 )
        {
            retVal |= 8;
        }
        if ( ( val & 2 ) == 2 )
        {
            retVal |= 4;
        }
        if ( ( val & 4 ) == 4 )
        {
            retVal |= 2;
        }
        if ( ( val & 8 ) == 8 )
        {
            retVal |= 1;
        }
        return retVal;
    }

    /// <summary>
    /// Convert a 4 bit value (nibble) into a hex character
    /// </summary>
    /// <param name="val">byte value</param>
    /// <returns>a single hex character representing low order 4 bits in byte.</returns>
    public static char ValueToHexChar( byte val )
    {
        var retVal = '0';

        // brute force but clear
        switch ( val )
        {
        case 0: retVal = '0'; break;
        case 1: retVal = '1'; break;
        case 2: retVal = '2'; break;
        case 3: retVal = '3'; break;
        case 4: retVal = '4'; break;
        case 5: retVal = '5'; break;
        case 6: retVal = '6'; break;
        case 7: retVal = '7'; break;
        case 8: retVal = '8'; break;
        case 9: retVal = '9'; break;
        case 10: retVal = 'a'; break;
        case 11: retVal = 'b'; break;
        case 12: retVal = 'c'; break;
        case 13: retVal = 'd'; break;
        case 14: retVal = 'e'; break;
        case 15: retVal = 'f'; break;
        default: throw new Exception( "Value: " + val + " not a hex digit" );
        }
        return retVal;
    }
}
}
