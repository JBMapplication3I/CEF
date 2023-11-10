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
    /// Represents a product classification system.
    /// </summary>
    public class ProductClassificationSystemModel
    {
        /// <summary>
        /// Its Integer SystemId value for System
        /// </summary>
        public int? systemId { get; set; }

        /// <summary>
        /// The System code for this System.
        /// </summary>
        public string? systemCode { get; set; }

        /// <summary>
        /// A friendly human-readable name representing this System.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// custom value set for the system
        /// </summary>
        public string? customsValue { get; set; }

        /// <summary>
        /// List of all countries that belong to the system including
        /// </summary>
        public List<ProductSystemCountryModel>? countries { get; set; }

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
