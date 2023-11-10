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
    /// Represents information about a single legal taxing jurisdiction
    /// </summary>
    public class JurisdictionModel
    {
        /// <summary>
        /// The code that is used to identify this jurisdiction
        /// </summary>
        public string? code { get; set; }

        /// <summary>
        /// The name of this jurisdiction
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// The type of the jurisdiction, indicating whether it is a country, state/region, city, for example.
        /// </summary>
        public JurisdictionType type { get; set; }

        /// <summary>
        /// The base rate of tax specific to this jurisdiction.
        /// </summary>
        public decimal? rate { get; set; }

        /// <summary>
        /// The "Sales" tax rate specific to this jurisdiction.
        /// </summary>
        public decimal? salesRate { get; set; }

        /// <summary>
        /// The Avalara-supplied signature code for this jurisdiction.
        /// </summary>
        public string? signatureCode { get; set; }

        /// <summary>
        /// Name or ISO 3166 code identifying the region within the country.
        ///  
        /// This field supports many different region identifiers:
        ///  * Two and three character ISO 3166 region codes
        ///  * Fully spelled out names of the region in ISO supported languages
        ///  * Common alternative spellings for many regions
        ///  
        /// For a full list of all supported codes and names, please see the Definitions API `ListRegions`.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// The "Seller's Use" tax rate specific to this jurisdiction.
        /// </summary>
        public decimal? useRate { get; set; }

        /// <summary>
        /// The city name of this jurisdiction
        /// </summary>
        public string? city { get; set; }

        /// <summary>
        /// The county name of this jurisdiction
        /// </summary>
        public string? county { get; set; }

        /// <summary>
        /// Name or ISO 3166 code identifying the country of this jurisdiction.
        ///  
        /// This field supports many different country identifiers:
        ///  * Two character ISO 3166 codes
        ///  * Three character ISO 3166 codes
        ///  * Fully spelled out names of the country in ISO supported languages
        ///  * Common alternative spellings for many countries
        ///  
        /// For a full list of all supported codes and names, please see the Definitions API `ListCountries`.
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// A short name of the jurisidiction
        /// </summary>
        public string? shortName { get; set; }

        /// <summary>
        /// State FIPS code
        /// </summary>
        public string? stateFips { get; set; }

        /// <summary>
        /// County FIPS code
        /// </summary>
        public string? countyFips { get; set; }

        /// <summary>
        /// City FIPS code
        /// </summary>
        public string? placeFips { get; set; }

        /// <summary>
        /// Unique AvaTax Id of this Jurisdiction
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The date this jurisdiction starts to take effect on tax calculations
        /// </summary>
        public DateTime? effectiveDate { get; set; }

        /// <summary>
        /// The date this jurisdiction stops to take effect on tax calculations
        /// </summary>
        public DateTime? endDate { get; set; }

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
