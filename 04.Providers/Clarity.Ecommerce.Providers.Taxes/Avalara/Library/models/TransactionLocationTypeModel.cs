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
    /// Information about a location type
    /// </summary>
    public class TransactionLocationTypeModel
    {
        /// <summary>
        /// Location type ID for this location type in transaction
        /// </summary>
        public long? documentLocationTypeId { get; set; }

        /// <summary>
        /// Transaction ID
        /// </summary>
        public long? documentId { get; set; }

        /// <summary>
        /// Address ID for the transaction
        /// </summary>
        public long? documentAddressId { get; set; }

        /// <summary>
        /// Location type code
        /// </summary>
        public string? locationTypeCode { get; set; }

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
