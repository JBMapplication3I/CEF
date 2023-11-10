// <copyright file="StreamExtensions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2022 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stream extensions class</summary>
namespace Microsoft.AspNet.Identity.Owin
{
    using System;
    using System.IO;
    using System.Text;

    /// <summary>A stream extensions.</summary>
    internal static class StreamExtensions
    {
        /// <summary>The default encoding.</summary>
        internal static readonly Encoding DefaultEncoding;

        /// <summary>Initializes static members of the Microsoft.AspNet.Identity.Owin.StreamExtensions class.</summary>
        static StreamExtensions()
        {
            DefaultEncoding = new UTF8Encoding(false, true);
        }

        /// <summary>A Stream extension method that creates a reader.</summary>
        /// <param name="stream">The stream to act on.</param>
        /// <returns>The new reader.</returns>
        public static BinaryReader CreateReader(this Stream stream)
        {
            return new BinaryReader(stream, DefaultEncoding, true);
        }

        /// <summary>A Stream extension method that creates a writer.</summary>
        /// <param name="stream">The stream to act on.</param>
        /// <returns>The new writer.</returns>
        public static BinaryWriter CreateWriter(this Stream stream)
        {
            return new BinaryWriter(stream, DefaultEncoding, true);
        }

        /// <summary>A BinaryReader extension method that reads date time offset.</summary>
        /// <param name="reader">The reader to act on.</param>
        /// <returns>The date time offset.</returns>
        public static DateTimeOffset ReadDateTimeOffset(this BinaryReader reader)
        {
            return new DateTimeOffset(reader.ReadInt64(), TimeSpan.Zero);
        }

        /// <summary>A BinaryWriter extension method that writes.</summary>
        /// <param name="writer">The writer to act on.</param>
        /// <param name="value"> The value.</param>
        public static void Write(this BinaryWriter writer, DateTimeOffset value)
        {
            writer.Write(value.UtcTicks);
        }
    }
}
