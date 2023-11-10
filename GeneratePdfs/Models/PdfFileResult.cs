namespace GeneratePdfs.Models
{
    /// <summary>Encapsulates the result of a pdf file.</summary>
    public class PdfFileResult
    {
        /// <value>The response.</value>
        public byte[]? BinaryData { get; set; }

        /// <value>The filename.</value>
        public string? FileName { get; set; }
    }
}
