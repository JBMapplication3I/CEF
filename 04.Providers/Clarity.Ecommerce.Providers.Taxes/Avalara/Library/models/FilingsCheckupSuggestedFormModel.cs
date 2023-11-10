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
    /// Worksheet Checkup Report Suggested Form Model
    /// </summary>
    public class FilingsCheckupSuggestedFormModel
    {
        /// <summary>
        /// Tax Authority ID of the suggested form returned
        /// </summary>
        public int? taxAuthorityId { get; set; }

        /// <summary>
        /// Country of the suggested form returned
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// Region of the suggested form returned
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// Name of the suggested form returned
        /// </summary>
        public string? taxFormCode { get; set; }

        /// <summary>
        /// Legacy Name of the suggested form returned
        /// </summary>
        public string? returnName { get; set; }

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
