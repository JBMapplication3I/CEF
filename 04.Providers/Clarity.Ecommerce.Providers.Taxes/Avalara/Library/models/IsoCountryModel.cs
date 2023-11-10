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
    /// Represents an ISO 3166 recognized country
    /// </summary>
    public class IsoCountryModel
    {
        /// <summary>
        /// The two character ISO 3166 country code
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// The three character ISO 3166 country code
        /// </summary>
        public string? alpha3Code { get; set; }

        /// <summary>
        /// The full name of this country in uppercase.
        ///  
        /// For names in proper or formal case, or for names in other languages, please examine the `localizedNames` element for an appropriate name.
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// True if this country is a member of the European Union
        /// </summary>
        public bool? isEuropeanUnion { get; set; }

        /// <summary>
        /// A list of localized names in a variety of languages.
        ///  
        /// This list is maintained by the International Standards Organization.
        /// </summary>
        public List<IsoLocalizedName>? localizedNames { get; set; }

        /// <summary>
        /// Whether or not this country requires a region in postal addresses.
        /// </summary>
        public bool? addressesRequireRegion { get; set; }

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
