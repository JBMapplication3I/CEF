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
    /// Model to specify lines to be removed
    /// </summary>
    public class RemoveTransactionLineModel
    {
        /// <summary>
        /// company code
        /// </summary>
        public string? companyCode { get; set; }

        /// <summary>
        /// document code for the transaction to add lines
        /// </summary>
        public string? transactionCode { get; set; }

        /// <summary>
        /// document type
        /// </summary>
        public DocumentType? documentType { get; set; }

        /// <summary>
        /// List of lines to be added
        /// </summary>
        public List<string>? lines { get; set; }

        /// <summary>
        /// ption to renumber lines after removal. After renumber, the line number becomes: "1", "2", "3", ...
        /// </summary>
        public bool? renumber { get; set; }

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
