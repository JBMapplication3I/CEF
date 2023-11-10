// <copyright file="Digest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the digest class</summary>
namespace Clarity.Ecommerce.Utilities
{
    using System;
    using System.IO;
    using System.Security.Cryptography;

    /// <summary>A digest.</summary>
    public static class Digest
    {
        /// <summary>CRC 32.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="model"> The model.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>An uint.</returns>
        public static int Crc32<T>(T model, Action<T, BinaryWriter> mapper)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            mapper(model, writer);
            return MapUintToInt(ms.ToArray().Crc32());
        }

        /// <summary>CRC 32.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>An uint.</returns>
        public static int Crc32(string source)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(source);
            return MapUintToInt(ms.ToArray().Crc32());
        }

        /// <summary>CRC 64.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="model"> The model.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>An ulong.</returns>
        public static long Crc64<T>(T model, Action<T, BinaryWriter> mapper)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            mapper(model, writer);
            return MapUlongToLong(ms.ToArray().Crc64());
        }

        /// <summary>CRC 64.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>An ulong.</returns>
        public static long Crc64(string source)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            writer.Write(source);
            return MapUlongToLong(ms.ToArray().Crc64());
        }

        /// <summary>SHA512 bytes.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="model"> The model.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A byte[].</returns>
        public static byte[] SHA512<T>(T model, Action<T, BinaryWriter> mapper)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            mapper(model, writer);
            return new SHA512Managed().ComputeHash(ms.ToArray());
        }

        /// <summary>SHA384 bytes.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="model"> The model.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A byte[].</returns>
        public static byte[] SHA384<T>(T model, Action<T, BinaryWriter> mapper)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            mapper(model, writer);
            return new SHA384Managed().ComputeHash(ms.ToArray());
        }

        /// <summary>SHA256 bytes.</summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="model"> The model.</param>
        /// <param name="mapper">The mapper.</param>
        /// <returns>A byte[].</returns>
        public static byte[] SHA256<T>(T model, Action<T, BinaryWriter> mapper)
        {
            using var ms = new MemoryStream();
            using var writer = new BinaryWriter(ms);
            mapper(model, writer);
            return new SHA256Managed().ComputeHash(ms.ToArray());
        }

        /// <summary>Map uint to int.</summary>
        /// <param name="uintValue">The uint value.</param>
        /// <returns>An int.</returns>
        public static int MapUintToInt(uint uintValue)
        {
            return unchecked((int)uintValue + int.MinValue);
        }

        /// <summary>Map int to uint.</summary>
        /// <param name="intValue">The int value.</param>
        /// <returns>An uint.</returns>
        public static uint MapIntToUint(int intValue)
        {
            return unchecked((uint)(intValue - int.MinValue));
        }

        /// <summary>Map ulong to long.</summary>
        /// <param name="ulongValue">The ulong value.</param>
        /// <returns>A long.</returns>
        public static long MapUlongToLong(ulong ulongValue)
        {
            return unchecked((long)ulongValue + long.MinValue);
        }

        /// <summary>Map long to ulong.</summary>
        /// <param name="longValue">The long value.</param>
        /// <returns>An ulong.</returns>
        public static ulong MapLongToUlong(long longValue)
        {
            return unchecked((ulong)(longValue - long.MinValue));
        }
    }
}
