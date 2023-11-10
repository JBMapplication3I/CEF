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
    /// An address used within this transaction.
    /// </summary>
    public class TransactionAddressModel
    {
        /// <summary>
        /// The unique ID number of this address.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The unique ID number of the document to which this address belongs.
        /// </summary>
        public long? transactionId { get; set; }

        /// <summary>
        /// The boundary level at which this address was validated.
        /// </summary>
        public BoundaryLevel? boundaryLevel { get; set; }

        /// <summary>
        /// The first line of the address.
        /// </summary>
        public string? line1 { get; set; }

        /// <summary>
        /// The second line of the address.
        /// </summary>
        public string? line2 { get; set; }

        /// <summary>
        /// The third line of the address.
        /// </summary>
        public string? line3 { get; set; }

        /// <summary>
        /// The city for the address.
        /// </summary>
        public string? city { get; set; }

        /// <summary>
        /// The ISO 3166 region code. E.g., the second part of ISO 3166-2.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// The postal code or zip code for the address.
        /// </summary>
        public string? postalCode { get; set; }

        /// <summary>
        /// The ISO 3166 country code
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// The unique ID number of the tax region for this address.
        /// </summary>
        public int? taxRegionId { get; set; }

        /// <summary>
        /// Latitude for this address
        /// </summary>
        public string? latitude { get; set; }

        /// <summary>
        /// Longitude for this address
        /// </summary>
        public string? longitude { get; set; }

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
