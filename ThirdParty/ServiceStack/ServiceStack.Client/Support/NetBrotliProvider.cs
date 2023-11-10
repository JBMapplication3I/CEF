namespace ServiceStack.Support
{
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using Caching;

    public class NetBrotliProvider : IBrotliProvider
    {
        public byte[] CompressStringToBytes(string text)
        {
            return CompressBytesToBytes(Encoding.UTF8.GetBytes(text));
        }

        public byte[] CompressBytesToBytes(byte[] buffer)
        {
            using var ms = new MemoryStream();
            using var zipStream = new BrotliStream(ms, CompressionMode.Compress);
            zipStream.Write(buffer, 0, buffer.Length);
            zipStream.Close();
            return ms.ToArray();
        }

        public string DecompressBytesToString(byte[] buffer)
        {
            var utf8Bytes = DecompressBytesToBytes(buffer);
            return Encoding.UTF8.GetString(utf8Bytes, 0, utf8Bytes.Length);
        }

        public byte[] DecompressBytesToBytes(byte[] buffer)
        {
            using var compressedStream = new MemoryStream(buffer);
            using var zipStream = new BrotliStream(compressedStream, CompressionMode.Decompress);
            return zipStream.ReadFully();
        }

        public Stream CompressStreamToStream(Stream stream)
        {
            return new BrotliStream(stream, CompressionMode.Compress);
        }

        public Stream DecompressStreamToStream(Stream stream)
        {
            return new BrotliStream(stream, CompressionMode.Decompress);
        }
    }
}
