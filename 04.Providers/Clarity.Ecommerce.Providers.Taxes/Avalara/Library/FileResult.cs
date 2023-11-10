namespace Avalara.AvaTax.RestClient
{
    /// <summary>Represents a file downloaded from AvaTax.</summary>
    public class FileResult
    {
        /// <summary>Raw bytes of the file.</summary>
        /// <value>The data.</value>
        public byte[]? Data { get; set; }

        /// <summary>Name of the file when downloaded as an attachment.</summary>
        /// <value>The filename.</value>
        public string? Filename { get; set; }

        /// <summary>MIME type of the file.</summary>
        /// <value>The type of the content.</value>
        public string? ContentType { get; set; }
    }
}
