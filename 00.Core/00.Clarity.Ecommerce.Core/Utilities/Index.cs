// <copyright file="Index.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the index class</summary>
// https://github.com/dotnet/runtime/blob/419e949d258ecee4c40a460fb09c66d974229623/src/libraries/System.Private.CoreLib/src/System/Index.cs
// https://github.com/dotnet/runtime/blob/419e949d258ecee4c40a460fb09c66d974229623/src/libraries/System.Private.CoreLib/src/System/Range.cs
#if !NET5_0_OR_GREATER
namespace System
{
    using Runtime.CompilerServices;

    /// <summary>Represent a type can be used to index a collection either from the start or the end.</summary>
    /// <remarks>Index is used by the C# compiler to support the new index syntax.
    /// <code>
    /// int[] someArray = new int[5] { 1, 2, 3, 4, 5 } ;
    /// int lastElement = someArray[^1]; // lastElement = 5
    /// </code></remarks>
    public readonly struct Index : IEquatable<Index>
    {
        private readonly int value;

        /// <summary>Initializes a new instance of the <see cref="Index"/> struct. Construct an Index using a value and
        /// indicating if the index is from the start or from the end.</summary>
        /// <remarks>If the Index constructed from the end, index value 1 means pointing at the last element and index
        /// value 0 means pointing at beyond last element.</remarks>
        /// <param name="value">  The index value. it has to be zero or positive number.</param>
        /// <param name="fromEnd">Indicating if the index is from the start or from the end.</param>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public Index(int value, bool fromEnd = false)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }
            if (fromEnd)
            {
                this.value = ~value;
            }
            else
            {
                this.value = value;
            }
        }

        // The following private constructors mainly created for perf reason to avoid the checks.
        private Index(int value)
        {
            this.value = value;
        }

        /// <summary>Gets a created Index pointing at first element.</summary>
        /// <value>The start.</value>
        public static Index Start => new(0);

        /// <summary>Gets a created Index pointing at beyond last element.</summary>
        /// <value>The end.</value>
        public static Index End => new(~0);

        /// <summary>Gets the index value.</summary>
        /// <value>The value.</value>
        public int Value
        {
            get
            {
                if (value < 0)
                {
                    return ~value;
                }
                else
                {
                    return value;
                }
            }
        }

        /// <summary>Gets a value indicating whether the index is from the start or the end.</summary>
        /// <value>True if this Index is from end, false if not.</value>
        public bool IsFromEnd => value < 0;

        /// <summary>Converts integer number to an Index.</summary>
        /// <param name="value">The value.</param>
        /// <returns>The result of the operation.</returns>
        public static implicit operator Index(int value) => FromStart(value);

        /// <summary>Create an Index from the start at the position indicated by the value.</summary>
        /// <param name="value">The index value from the start.</param>
        /// <returns>An Index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromStart(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }
            return new(value);
        }

        /// <summary>Create an Index from the end at the position indicated by the value.</summary>
        /// <param name="value">The index value from the end.</param>
        /// <returns>An Index.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Index FromEnd(int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(value), "value must be non-negative");
            }
            return new(~value);
        }

        /// <summary>Calculate the offset from the start using the giving collection length.</summary>
        /// <remarks>For performance reason, we don't validate the input length parameter and the returned offset value
        /// against negative values. we don't validate either the returned offset is greater than the input length. It
        /// is expected Index will be used with collections which always have non negative length/count. If the returned
        /// offset is negative and then used to index a collection will get out of range exception which will be same
        /// affect as the validation.</remarks>
        /// <param name="length">The length of the collection that the Index will be used with. length has to be a
        ///                      positive value.</param>
        /// <returns>The offset.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public int GetOffset(int length)
        {
            var offset = value;
            if (IsFromEnd)
            {
                // offset = length - (~value)
                // offset = length + (~(~value) + 1)
                // offset = length + value + 1
                offset += length + 1;
            }
            return offset;
        }

        /// <summary>Indicates whether the current Index object is equal to another object of the same type.</summary>
        /// <param name="v">An object to compare with this object.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public override bool Equals(object? v)
            => v is Index index
            && this.value == index.value;

        /// <summary>Indicates whether the current Index object is equal to another Index object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public bool Equals(Index other) => value == other.value;

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A hash code for this Index.</returns>
        public override int GetHashCode() => value;

        /// <summary>Converts the value of the current Index object to its equivalent string representation.</summary>
        /// <returns>A string that represents this Index.</returns>
        public override string ToString()
        {
            if (IsFromEnd)
            {
                return "^" + ((uint)Value).ToString();
            }
            return ((uint)Value).ToString();
        }
    }
}
#endif
