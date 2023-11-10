// Copyright (c) ServiceStack, Inc. All Rights Reserved.
// License: https://raw.github.com/ServiceStack/ServiceStack/master/license.txt

namespace ServiceStack
{
    using System;
    using System.IO;
    using System.Text;
    using Caching;

    public static class StreamExt
    {
        /// <summary>Compresses the specified text using the default compression method: Deflate.</summary>
        /// <param name="text">           The text.</param>
        /// <param name="compressionType">Type of the compression.</param>
        /// <returns>A byte[].</returns>
        public static byte[] Compress(this string text, string compressionType)
        {
            if (compressionType == CompressionTypes.Deflate)
            {
                return Deflate(text);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return GZip(text);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return BrotliCompress(text);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        public static Stream CompressStream(this Stream stream, string compressionType)
        {
            if (compressionType == CompressionTypes.Deflate)
            {
                return DeflateProvider.CompressStreamToStream(stream);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return GZipProvider.CompressStreamToStream(stream);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return BrotliProvider.CompressStreamToStream(stream);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        /// <summary>Compresses the specified text using the default compression method: Deflate.</summary>
        /// <param name="bytes">          The bytes to act on.</param>
        /// <param name="compressionType">Type of the compression.</param>
        /// <returns>A byte[].</returns>
        public static byte[] CompressBytes(this byte[] bytes, string compressionType)
        {
            if (compressionType == CompressionTypes.Deflate)
            {
                return DeflateProvider.CompressBytesToBytes(bytes);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return GZipProvider.CompressBytesToBytes(bytes);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return BrotliProvider.CompressBytesToBytes(bytes);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        public static IDeflateProvider DeflateProvider = new Support.NetDeflateProvider();

        public static IGZipProvider GZipProvider = new Support.NetGZipProvider();

#if NET60_OR_GREATER
        public static IBrotliProvider BrotliProvider = new Support.NetBrotliProvider();
#endif

        /// <summary>Decompresses the specified buffer using the default compression method: Inflate.</summary>
        /// <param name="buffer">         The buffer.</param>
        /// <param name="compressionType">Type of the compression.</param>
        /// <returns>A string.</returns>
        public static string Decompress(this byte[] buffer, string compressionType)
        {
            if (compressionType == CompressionTypes.Deflate)
            {
                return Inflate(buffer);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return GUnzip(buffer);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return BrotliDecompress(buffer);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        /// <summary>Decompresses the specified gz buffer using inflate or gzip method.</summary>
        /// <param name="stream">         Compressed stream.</param>
        /// <param name="compressionType">Type of the compression. Can be "gzip" or "deflate".</param>
        /// <returns>Decompressed stream.</returns>
        public static Stream Decompress(this Stream stream, string compressionType)
        {
            if (string.IsNullOrEmpty(compressionType))
            {
                return stream;
            }
            if (compressionType == CompressionTypes.Deflate)
            {
                return DeflateProvider.DecompressStreamToStream(stream);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return GZipProvider.DecompressStreamToStream(stream);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return BrotliProvider.DecompressStreamToStream(stream);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        /// <summary>Decompresses the specified buffer using the default compression method: Inflate.</summary>
        /// <param name="buffer">         The buffer to act on.</param>
        /// <param name="compressionType">Type of the compression.</param>
        /// <returns>A byte[].</returns>
        public static byte[] DecompressBytes(this byte[] buffer, string compressionType)
        {
            if (compressionType == CompressionTypes.Deflate)
            {
                return DeflateProvider.DecompressBytesToBytes(buffer);
            }
            if (compressionType == CompressionTypes.GZip)
            {
                return GZipProvider.DecompressBytesToBytes(buffer);
            }
#if NET60_OR_GREATER
            if (compressionType == CompressionTypes.Brotli)
            {
                return BrotliProvider.DecompressBytesToBytes(buffer);
            }
#endif
            throw new NotSupportedException(compressionType);
        }

        public static byte[] Deflate(this string text)
        {
            return DeflateProvider.CompressStringToBytes(text);
        }

        public static string Inflate(this byte[] gzBuffer)
        {
            return DeflateProvider.DecompressBytesToString(gzBuffer);
        }

        public static byte[] GZip(this string text)
        {
            return GZipProvider.CompressStringToBytes(text);
        }

        public static string GUnzip(this byte[] gzBuffer)
        {
            return GZipProvider.DecompressBytesToString(gzBuffer);
        }

#if NET60_OR_GREATER
        public static byte[] BrotliCompress(this string text)
        {
            return BrotliProvider.CompressStringToBytes(text);
        }

        public static string BrotliDecompress(this byte[] gzBuffer)
        {
            return BrotliProvider.DecompressBytesToString(gzBuffer);
        }
#endif

        public static string ToUtf8String(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            using var reader = new StreamReader(stream, Encoding.UTF8);
            return reader.ReadToEnd();
        }

        public static byte[] ToBytes(this Stream stream)
        {
            if (stream == null)
            {
                throw new ArgumentNullException(nameof(stream));
            }
            return stream.ReadFully();
        }

        public static void Write(this Stream stream, string text)
        {
            var bytes = Encoding.UTF8.GetBytes(text);
            stream.Write(bytes, 0, bytes.Length);
        }
    }
}
