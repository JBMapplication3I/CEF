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
    /// Company Initialization Model
    /// </summary>
    public class CompanyInitializationModel
    {
        /// <summary>
        /// Company Name
        /// </summary>
        public string? name { get; set; }

        /// <summary>
        /// Company Code - used to distinguish between companies within your accounting system
        /// </summary>
        public string? companyCode { get; set; }

        /// <summary>
        /// Vat Registration Id - leave blank if not known.
        /// </summary>
        public string? vatRegistrationId { get; set; }

        /// <summary>
        /// United States Taxpayer ID number, usually your Employer Identification Number if you are a business or your
        /// Social Security Number if you are an individual.
        /// This value is required if the address provided is inside the US and if you subscribed to the Avalara Managed Returns or SST Certified Service Provider service. Otherwise it is optional.
        /// </summary>
        public string? taxpayerIdNumber { get; set; }

        /// <summary>
        /// Address Line1
        /// </summary>
        public string? line1 { get; set; }

        /// <summary>
        /// Line2
        /// </summary>
        public string? line2 { get; set; }

        /// <summary>
        /// Line3
        /// </summary>
        public string? line3 { get; set; }

        /// <summary>
        /// City
        /// </summary>
        public string? city { get; set; }

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
        /// Postal Code
        /// </summary>
        public string? postalCode { get; set; }

        /// <summary>
        /// Name or ISO 3166 code identifying the country.
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
        /// First Name
        /// </summary>
        public string? firstName { get; set; }

        /// <summary>
        /// Last Name
        /// </summary>
        public string? lastName { get; set; }

        /// <summary>
        /// Title
        /// </summary>
        public string? title { get; set; }

        /// <summary>
        /// Email
        /// </summary>
        public string? email { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string? phoneNumber { get; set; }

        /// <summary>
        /// Mobile Number
        /// </summary>
        public string? mobileNumber { get; set; }

        /// <summary>
        /// Fax Number
        /// </summary>
        public string? faxNumber { get; set; }

        /// <summary>
        /// Parent Company ID
        /// </summary>
        public int? parentCompanyId { get; set; }

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
