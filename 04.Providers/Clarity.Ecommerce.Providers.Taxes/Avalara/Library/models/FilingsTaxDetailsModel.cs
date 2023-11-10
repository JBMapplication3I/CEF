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
    /// Represents a listing of all tax calculation data for filings and for accruing to future filings.
    /// </summary>
    public class FilingsTaxDetailsModel
    {
        /// <summary>
        /// The tax type associated with the summary
        /// </summary>
        public string? taxType { get; set; }

        /// <summary>
        /// The total sales amount
        /// </summary>
        public decimal? salesAmount { get; set; }

        /// <summary>
        /// The nontaxable amount
        /// </summary>
        public decimal? nonTaxableAmount { get; set; }

        /// <summary>
        /// The tax amount
        /// </summary>
        public decimal? taxAmount { get; set; }

        /// <summary>
        /// The number of nights
        /// </summary>
        public long? numberOfNights { get; set; }

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
