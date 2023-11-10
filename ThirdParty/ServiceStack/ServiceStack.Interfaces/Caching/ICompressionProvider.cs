namespace ServiceStack.Caching
{
    using System.IO;

    public interface ICompressionProvider
    {
        byte[] CompressStringToBytes(string text);
        byte[] CompressBytesToBytes(byte[] bytes);
        string DecompressBytesToString(byte[] buffer);
        byte[] DecompressBytesToBytes(byte[] buffer);
        Stream CompressStreamToStream(Stream stream);
        Stream DecompressStreamToStream(Stream stream);
    }
}
