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
    /// Represents information about location types stored in a line
    /// </summary>
    public class TransactionLineLocationTypeModel
    {
        /// <summary>
        /// The unique ID number of this line location address model
        /// </summary>
        public long? documentLineLocationTypeId { get; set; }

        /// <summary>
        /// The unique ID number of the document line associated with this line location address model
        /// </summary>
        public long? documentLineId { get; set; }

        /// <summary>
        /// The address ID corresponding to this model
        /// </summary>
        public long? documentAddressId { get; set; }

        /// <summary>
        /// The location type code corresponding to this model
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
