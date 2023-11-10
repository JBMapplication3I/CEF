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
    /// A request to upload a file to Resource Files
    /// </summary>
    public class ResourceFileUploadRequestModel
    {
        /// <summary>
        /// This stream contains the bytes of the file being uploaded.
        /// </summary>
        public byte content { get; set; }

        /// <summary>
        /// The username adding the file
        /// </summary>
        public string? username { get; set; }

        /// <summary>
        /// The account ID to which this file will be attached.
        /// </summary>
        public int? accountId { get; set; }

        /// <summary>
        /// The company ID to which this file will be attached.
        /// </summary>
        public int? companyId { get; set; }

        /// <summary>
        /// The original name of this file.
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// The resource type ID of this file.
        /// </summary>
        public int? resourceFileTypeId { get; set; }

        /// <summary>
        /// Length of the file in bytes.
        /// </summary>
        public long? length { get; set; }

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
