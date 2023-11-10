﻿// <copyright file="Range.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the range class</summary>
// https://github.com/dotnet/runtime/blob/419e949d258ecee4c40a460fb09c66d974229623/src/libraries/System.Private.CoreLib/src/System/Index.cs
// https://github.com/dotnet/runtime/blob/419e949d258ecee4c40a460fb09c66d974229623/src/libraries/System.Private.CoreLib/src/System/Range.cs
#if !NET5_0_OR_GREATER
namespace System
{
    using Runtime.CompilerServices;

    /// <summary>Represent a range has start and end indexes.</summary>
    /// <remarks>Range is used by the C# compiler to support the range syntax.
    /// <code>
    /// int[] someArray = new int[5] { 1, 2, 3, 4, 5 };
    /// int[] subArray1 = someArray[0..2]; // { 1, 2 }
    /// int[] subArray2 = someArray[1..^0]; // { 2, 3, 4, 5 }
    /// </code></remarks>
    public readonly struct Range : IEquatable<Range>
    {
        /// <summary>Initializes a new instance of the <see cref="Range"/> struct. Construct a Range object using the
        /// start and end indexes.</summary>
        /// <param name="start">Represent the inclusive start index of the range.</param>
        /// <param name="end">  Represent the exclusive end index of the range.</param>
        public Range(Index start, Index end)
        {
            Start = start;
            End = end;
        }

        /// <summary>Gets a Created Range object starting from first element to the end.</summary>
        /// <value>The full range.</value>
        public static Range All => new(Index.Start, Index.End);

        /// <summary>Gets an Index that represents the inclusive start index of the Range.</summary>
        /// <value>The start.</value>
        public Index Start { get; }

        /// <summary>Gets an Index that represents the exclusive end index of the Range.</summary>
        /// <value>The end.</value>
        public Index End { get; }

        /// <summary>Create a Range object starting from start index to the end of the collection.</summary>
        /// <param name="start">The start.</param>
        /// <returns>A Range.</returns>
        public static Range StartAt(Index start) => new(start, Index.End);

        /// <summary>Create a Range object starting from first element in the collection to the end Index.</summary>
        /// <param name="end">The end.</param>
        /// <returns>A Range.</returns>
        public static Range EndAt(Index end) => new(Index.Start, end);

        /// <summary>Indicates whether the current Range object is equal to another object of the same type.</summary>
        /// <param name="value">An object to compare with this object.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public override bool Equals(object? value)
            => value is Range r
            && r.Start.Equals(Start)
            && r.End.Equals(End);

        /// <summary>Indicates whether the current Range object is equal to another Range object.</summary>
        /// <param name="other">An object to compare with this object.</param>
        /// <returns>True if the objects are considered equal, false if they are not.</returns>
        public bool Equals(Range other) => other.Start.Equals(Start) && other.End.Equals(End);

        /// <summary>Returns the hash code for this instance.</summary>
        /// <returns>A hash code for this Range.</returns>
        public override int GetHashCode()
        {
            return Start.GetHashCode() * 31 + End.GetHashCode();
        }

        /// <summary>Converts the value of the current Range object to its equivalent string representation.</summary>
        /// <returns>A string that represents this Range.</returns>
        public override string ToString()
        {
            return Start + ".." + End;
        }

        /// <summary>Calculate the start offset and length of range object using a collection length.</summary>
        /// <remarks>For performance reason, we don't validate the input length parameter against negative values. It is
        /// expected Range will be used with collections which always have non negative length/count. We validate the
        /// range is inside the length scope though.</remarks>
        /// <param name="length">The length of the collection that the range will be used with. length has to be a
        ///                      positive value.</param>
        /// <returns>The offset and length.</returns>
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public (int Offset, int Length) GetOffsetAndLength(int length)
        {
            int start;
            var startIndex = Start;
            if (startIndex.IsFromEnd)
            {
                start = length - startIndex.Value;
            }
            else
            {
                start = startIndex.Value;
            }

            int end;
            var endIndex = End;
            if (endIndex.IsFromEnd)
            {
                end = length - endIndex.Value;
            }
            else
            {
                end = endIndex.Value;
            }
            if ((uint)end > (uint)length || (uint)start > (uint)end)
            {
                throw new ArgumentOutOfRangeException(nameof(length));
            }
            return (start, end - start);
        }
    }
}
#endif
