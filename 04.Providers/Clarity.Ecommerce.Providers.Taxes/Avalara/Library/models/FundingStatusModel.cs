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
    /// Status of an Avalara Managed Returns funding configuration for a company
    /// </summary>
    public class FundingStatusModel
    {
        /// <summary>
        /// The unique ID number of this funding request
        /// </summary>
        public long? requestId { get; set; }

        /// <summary>
        /// SubledgerProfileID
        /// </summary>
        public int? subledgerProfileID { get; set; }

        /// <summary>
        /// CompanyID
        /// </summary>
        public string? companyID { get; set; }

        /// <summary>
        /// Domain
        /// </summary>
        public string? domain { get; set; }

        /// <summary>
        /// Recipient
        /// </summary>
        public string? recipient { get; set; }

        /// <summary>
        /// Sender
        /// </summary>
        public string? sender { get; set; }

        /// <summary>
        /// DocumentKey
        /// </summary>
        public string? documentKey { get; set; }

        /// <summary>
        /// DocumentType
        /// </summary>
        public string? documentType { get; set; }

        /// <summary>
        /// DocumentName
        /// </summary>
        public string? documentName { get; set; }

        /// <summary>
        /// MethodReturn
        /// </summary>
        public FundingESignMethodReturn? methodReturn { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string? status { get; set; }

        /// <summary>
        /// ErrorMessage
        /// </summary>
        public string? errorMessage { get; set; }

        /// <summary>
        /// LastPolled
        /// </summary>
        public DateTime? lastPolled { get; set; }

        /// <summary>
        /// LastSigned
        /// </summary>
        public DateTime? lastSigned { get; set; }

        /// <summary>
        /// LastActivated
        /// </summary>
        public DateTime? lastActivated { get; set; }

        /// <summary>
        /// TemplateRequestId
        /// </summary>
        public long? templateRequestId { get; set; }

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
