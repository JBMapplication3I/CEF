#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace JPMC.MSDK.Filer
{
    /// <summary>
    /// Summary description for IFileConverter.
    /// </summary>
    public interface IFileConverter
    {
        /// <summary>
        /// Converts a file into a PGP encrypted file.
        /// </summary>
        /// <remarks>
        /// The resulting PGP file will not be compressed, as the
        /// SFTP servers will fail to decrypt a
        /// compressed PGP file.
        /// </remarks>
        /// <param name="reader"></param>
        /// <param name="pgpFile"></param>
        void ConvertTo(IFileReader reader, string pgpFile);
        /// <summary>
        /// Convert a PGP file to an AES file.
        /// </summary>
        /// <param name="pgpFile">The PGP file to convert.</param>
        /// <param name="aesFile">The AES file to write to.</param>
        /// <returns></returns>
        void ConvertFrom(string pgpFile, string aesFile);
//		/// <summary>
//		/// Convert a PGP file to an AES file.
//		/// </summary>
//		/// <param name="pgpFile">The PGP file to convert.</param>
//		/// <returns></returns>
//		string ConvertFrom(string pgpFile);
        /// <summary>
        /// Convert an error file to an AES file.
        /// </summary>
        /// <param name="pgpFile">The PGP file to convert.</param>
        /// <returns></returns>
        string ConvertErrorFrom(string pgpFile);

        string FileType { get; }
    }
}
