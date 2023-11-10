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
    /// 
    /// </summary>
    public class WorksheetDocument
    {
        /// <summary>
        /// 
        /// </summary>
        public string? docCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public DateTime? docDate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? totalExempt { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? totalTaxable { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public decimal? totalTax { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<WorksheetDocumentLine>? lines { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<Message>? messages { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? resultCode { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string? transactionId { get; set; }

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
