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
    /// Encapsulates the result of uploading a file to the resource system
    /// </summary>
    public class ResourceFileUploadResultModel
    {
        /// <summary>
        /// If the request failed, this contains a description of the error.
        /// </summary>
        public string? message { get; set; }

        /// <summary>
        /// If the request succeeded, this is the ID number of the file.
        /// </summary>
        public long? resourceFileId { get; set; }

        /// <summary>
        /// True if the upload request succeeded.
        /// </summary>
        public bool? aaa_success { get; set; }

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
