// <copyright file="RuntimeHelpers2.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the runtime helpers class</summary>
// https://github.com/dotnet/runtime/blob/419e949d258ecee4c40a460fb09c66d974229623/src/libraries/System.Private.CoreLib/src/System/Index.cs
// https://github.com/dotnet/runtime/blob/419e949d258ecee4c40a460fb09c66d974229623/src/libraries/System.Private.CoreLib/src/System/Range.cs
#if !NET5_0_OR_GREATER
namespace System.Runtime.CompilerServices
{
    /// <summary>A runtime helpers.</summary>
    public static class RuntimeHelpers2
    {
        /// <summary>Slices the specified array using the specified range.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="array">The array.</param>
        /// <param name="range">The range.</param>
        /// <returns>An array of t.</returns>
        public static T[] GetSubArray<T>(T[] array, Range range)
        {
            if (array == null)
            {
                throw new ArgumentNullException(nameof(array));
            }
            var (offset, length) = range.GetOffsetAndLength(array.Length);
            if (default(T) != null || typeof(T[]) == array.GetType())
            {
                // We know the type of the array to be exactly T[].
                if (length == 0)
                {
                    return Array.Empty<T>();
                }
                var dest = new T[length];
                Array.Copy(array, offset, dest, 0, length);
                return dest;
            }
            else
            {
                // The array is actually a U[] where U:T.
                var dest = (T[])Array.CreateInstance(array.GetType().GetElementType()!, length);
                Array.Copy(array, offset, dest, 0, length);
                return dest;
            }
        }
    }
}
#endif
