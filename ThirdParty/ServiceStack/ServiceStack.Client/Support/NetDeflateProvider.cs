namespace ServiceStack.Support
{
    using System.IO;
    using System.IO.Compression;
    using System.Text;
    using Caching;

    public class NetDeflateProvider : IDeflateProvider
    {
        public byte[] CompressStringToBytes(string text)
        {
            return CompressBytesToBytes(Encoding.UTF8.GetBytes(text));
        }

        public byte[] CompressBytesToBytes(byte[] bytes)
        {
            // In .NET FX incompat-ville, you can't access compressed bytes without closing DeflateStream
            // Which means we must use MemoryStream since you have to use ToArray() on a closed Stream
            using var ms = new MemoryStream();
            using var zipStream = new DeflateStream(ms, CompressionMode.Compress);
            zipStream.Write(bytes, 0, bytes.Length);
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
            using var zipStream = new DeflateStream(compressedStream, CompressionMode.Decompress);
            return zipStream.ReadFully();
        }

        public Stream CompressStreamToStream(Stream stream)
        {
            return new DeflateStream(stream, CompressionMode.Compress);
        }

        public Stream DecompressStreamToStream(Stream stream)
        {
            return new DeflateStream(stream, CompressionMode.Decompress);
        }
    }
}
