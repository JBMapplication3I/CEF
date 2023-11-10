// <copyright file="ByteArrayExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the byte array extensions class</summary>
namespace Clarity.Ecommerce
{
    using System.Linq;

    /// <summary>A byte array extensions.</summary>
    public static class ByteArrayExtensions
    {
        /// <summary>The ISO 3309 polynomial.</summary>
        public const ulong Iso3309Polynomial = 0xD800000000000000;

        /// <summary>The default polynomial 32.</summary>
        public const uint DefaultPolynomial32 = 0xedb88320u;

        /// <summary>The default seed 32.</summary>
        public const uint DefaultSeed32 = 0xffffffffu;

        /// <summary>The default seed 64.</summary>
        public const uint DefaultSeed64 = 0x0u;

        /// <summary>The default table 32.</summary>
        private static uint[]? defaultTable32;

        /// <summary>The default table 64.</summary>
        private static ulong[]? defaultTable64;

        /// <summary>Gets the default table 32.</summary>
        /// <value>The default table 32.</value>
        private static uint[] DefaultTable32 => defaultTable32 ??= InitializeTable32();

        /// <summary>Gets the default table 64.</summary>
        /// <value>The default table 64.</value>
        private static ulong[] DefaultTable64 => defaultTable64 ??= InitializeTable64();

        /// <summary>A byte[] extension method that CRC 32.</summary>
        /// <param name="buffer">The buffer to act on.</param>
        /// <param name="seed">  The seed.</param>
        /// <returns>An uint.</returns>
        public static uint Crc32(this byte[] buffer, uint seed = DefaultSeed32)
        {
            return ~buffer.Aggregate(seed, (current, elem) => (current >> 8) ^ DefaultTable32[elem ^ current & 0xff]);
        }

        /// <summary>A byte[] extension method that CRC 64.</summary>
        /// <param name="buffer">The buffer to act on.</param>
        /// <param name="seed">  The seed.</param>
        /// <returns>An ulong.</returns>
        public static ulong Crc64(this byte[] buffer, ulong seed = DefaultSeed64)
        {
            return ~buffer.Aggregate(seed, (current, elem) => (current >> 8) ^ DefaultTable64[elem ^ current & 0xff]);
        }

        /// <summary>Initializes the table 32.</summary>
        /// <returns>An uint[].</returns>
        private static uint[] InitializeTable32()
        {
            var createTable = new uint[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (uint)i;
                for (var j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ DefaultPolynomial32;
                    }
                    else
                    {
                        entry >>= 1;
                    }
                }
                createTable[i] = entry;
            }
            return createTable;
        }

        /// <summary>Initializes the table 64.</summary>
        /// <returns>An ulong[].</returns>
        private static ulong[] InitializeTable64()
        {
            var createTable = new ulong[256];
            for (var i = 0; i < 256; i++)
            {
                var entry = (ulong)i;
                for (var j = 0; j < 8; j++)
                {
                    if ((entry & 1) == 1)
                    {
                        entry = (entry >> 1) ^ Iso3309Polynomial;
                    }
                    else
                    {
                        entry >>= 1;
                    }
                }
                createTable[i] = entry;
            }
            return createTable;
        }
    }
}
