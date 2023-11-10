/*
 * AvaTax API Client Library
 *
 * (c) 2004-2019 Avalara, Inc.
 *
 * For the full copyright and license information, please view the LICENSE
 * file that was distributed with this source code.
 *
 * @author Genevieve Conty
 * @author Greg Hester
 */

namespace Avalara.AvaTax.RestClient
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>
    /// Represents one file in a batch upload.
    /// </summary>
    public class BatchFileModel
    {
        /// <summary>
        /// The unique ID number assigned to this batch file.
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The unique ID number of the batch that this file belongs to.
        /// </summary>
        public int? batchId { get; set; }

        /// <summary>
        /// Logical Name of file (e.g. "Input" or "Error").
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// Content of the batch file.
        /// </summary>
        public byte[]? content { get; set; }

        /// <summary>
        /// Size of content, in bytes.
        /// </summary>
        public int? contentLength { get; set; }

        /// <summary>
        /// Content mime type (e.g. text/csv). This is used for HTTP downloading.
        /// </summary>
        public string? contentType { get; set; }

        /// <summary>
        /// File extension (e.g. CSV).
        /// </summary>
        public string? fileExtension { get; set; }

        /// <summary>
        /// Path to the file - name/S3 key
        /// </summary>
        public string? filePath { get; set; }

        /// <summary>
        /// Number of errors that occurred when processing this file.
        /// </summary>
        public int? errorCount { get; set; }

        /// <summary>
        /// Convert this object to a JSON string of itself
        /// </summary>
        /// <returns>A JSON string of this object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
