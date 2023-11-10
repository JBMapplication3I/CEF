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
    /// Cycle Safe Expiration results.
    /// </summary>
    public class FilingsCheckupAuthorityModel
    {
        /// <summary>
        /// Unique ID of the tax authority
        /// </summary>
        public int? taxAuthorityId { get; set; }

        /// <summary>
        /// Location Code of the tax authority
        /// </summary>
        public string? locationCode { get; set; }

        /// <summary>
        /// Name of the tax authority
        /// </summary>
        public string? taxAuthorityName { get; set; }

        /// <summary>
        /// Type Id of the tax authority
        /// </summary>
        public int? taxAuthorityTypeId { get; set; }

        /// <summary>
        /// Jurisdiction Id of the tax authority
        /// </summary>
        public int? jurisdictionId { get; set; }

        /// <summary>
        /// Amount of tax collected in this tax authority
        /// </summary>
        public decimal? tax { get; set; }

        /// <summary>
        /// Tax Type collected in the tax authority
        /// </summary>
        public string? taxTypeId { get; set; }

        /// <summary>
        /// Suggested forms to file due to tax collected
        /// </summary>
        public List<FilingsCheckupSuggestedFormModel>? suggestedForms { get; set; }

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
