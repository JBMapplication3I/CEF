// <copyright file="StaticRandom.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the static random class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System;

    /// <summary>A static random.</summary>
    public class StaticRandom
    {
        /// <summary>The random.</summary>
        private static readonly Random Random = new();

        /// <summary>The synchronize lock.</summary>
        private static readonly object PadLock = new();

        /// <summary>Gets the next.</summary>
        /// <returns>An int.</returns>
        public static int Next()
        {
            lock (PadLock)
            {
                // synchronize
                return Random.Next();
            }
        }
    }
}
