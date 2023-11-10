using System;

namespace JPMC.MSDK.Common
{
/// <summary>
/// This class maintains an object that always maintains the
/// last X number of characters added, where X is the amount
/// specified by the Limit property.
/// </summary>
public class CircularBuffer
{
	private byte [] values;
	private int limit;
	private int count;
	private int next;

	/// <summary>
	/// Constructor, size of buffer cannot change
	/// </summary>
	/// <param name="limit"></param>
	public CircularBuffer( int limit )
	{
		if ( limit == 0 )
		{
			throw new ArgumentOutOfRangeException( 
				"CircularBuffer size must be greater than zero");
		}

		this.limit = limit;
		values = new byte [ limit ];
	}

	/// <summary>
	/// Number of characters allowed in the object. Once more 
	/// bytes are added to the buffer, bytes are removed from
	/// the start of the buffer to accommodate the new bytes
	/// without exceeding this limit.
	/// </summary>
	public int Limit => limit;

    /// <summary>
	/// Number of characters in the object, will always be "limit"
	/// once at least "limit" number of characters are add to
	/// the object
	/// </summary>
	public int Count => count;

    /// <summary>
	/// Add new bytes to the buffer
	/// </summary>
	/// <param name="newBytes"></param>
	public void Add( byte [] newBytes )
	{
		if ( newBytes != null )
		{
			Add( newBytes, 0, newBytes.Length );
		}
	}

	/// <summary>
	/// Add new bytes given an offset and a length
	/// </summary>
	/// <param name="newBytes"></param>
	/// <param name="offset"></param>
	/// <param name="length"></param>
	public void Add( byte [] newBytes, int offset, int length )
	{
		if ( newBytes != null )
		{
			for ( var i=offset; i < length; i++ )
			{
				Add( newBytes[ i ] );
			}
		}
	}

	/// <summary>
	/// Add just one byte to the end (latest) of the buffer
	/// </summary>
	/// <param name="latest"></param>
	public void Add( byte latest )
	{
		values[ next++ ] = latest;
		if ( next == limit )
		{
			next = 0;
		}
		if ( count < limit )
		{
			count++;
		}
	}

	/// <summary>
	/// Get the bytes in an array in the proper order
	/// </summary>
	/// <returns></returns>
	public byte [] Get()
	{
		byte [] retVal = null;

		if ( count > 0 )
		{
			retVal = new byte[ count ];
		}

		if ( retVal != null )
		{
			var index = next - 1;

			// if next is zero it means
			// we wrapped around
			if ( next == 0 )
			{
				index = limit - 1;
			}

			// we copy only "count" number of bytes
			for ( var i=count - 1; i >= 0; i-- )
			{
				// we wrap backwards if we hit zero
				if ( index < 0 )
				{
					index = limit - 1;
				}

				retVal[ i ] = values[ index-- ];
			}
		}
		
		return retVal;
	}

	/// <summary>
	/// Just get the latest byte that was added
	/// </summary>
	/// <returns></returns>
	public byte GetLatest()
	{
		byte retVal = 0;

		if ( count > 0 )
		{
			var index = next - 1;

			if ( next == 0 )
			{
				index = limit - 1;
			}
			retVal = values[ index ];
		}

		return retVal;
	}

	/// <summary>
	/// Returns the contents of the buffer as a string.
	/// </summary>
	/// <returns></returns>
	public new string ToString()
	{
		return Utils.ByteArrayToString(Get());
	}
}
}
