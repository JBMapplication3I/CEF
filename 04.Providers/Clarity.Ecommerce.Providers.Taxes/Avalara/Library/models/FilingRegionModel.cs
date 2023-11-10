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
    /// Regions
    /// </summary>
    public class FilingRegionModel
    {
        /// <summary>
        /// The unique ID number of this filing region.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The filing id that this region belongs too
        /// </summary>
        public long? filingId { get; set; }

        /// <summary>
        /// The two-character ISO-3166 code for the country.
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// The two or three character region code for the region.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// The sales amount.
        /// </summary>
        public decimal? salesAmount { get; set; }

        /// <summary>
        /// The taxable amount.
        /// </summary>
        public decimal? taxableAmount { get; set; }

        /// <summary>
        /// The tax amount.
        /// </summary>
        public decimal? taxAmount { get; set; }

        /// <summary>
        /// The tax amount due.
        /// </summary>
        public decimal? taxDueAmount { get; set; }

        /// <summary>
        /// The amount collected by Avalara for this region
        /// </summary>
        public decimal? collectAmount { get; set; }

        /// <summary>
        /// Total remittance amount of all returns in region
        /// </summary>
        public decimal? totalRemittanceAmount { get; set; }

        /// <summary>
        /// The non-taxable amount.
        /// </summary>
        public decimal? nonTaxableAmount { get; set; }

        /// <summary>
        /// Consumer use tax liability.
        /// </summary>
        public decimal? consumerUseTaxAmount { get; set; }

        /// <summary>
        /// Consumer use non-taxable amount.
        /// </summary>
        public decimal? consumerUseNonTaxableAmount { get; set; }

        /// <summary>
        /// Consumer use taxable amount.
        /// </summary>
        public decimal? consumerUseTaxableAmount { get; set; }

        /// <summary>
        /// The date the filing region was approved.
        /// </summary>
        public DateTime? approveDate { get; set; }

        /// <summary>
        /// The start date for the filing cycle.
        /// </summary>
        public DateTime? startDate { get; set; }

        /// <summary>
        /// The end date for the filing cycle.
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// Whether or not you have nexus in this region.
        /// </summary>
        public bool? hasNexus { get; set; }

        /// <summary>
        /// The current status of the filing region.
        /// </summary>
        public FilingStatusId? status { get; set; }

        /// <summary>
        /// A list of tax returns in this region.
        /// </summary>
        public List<FilingReturnModel>? returns { get; set; }

        /// <summary>
        /// A list of tax returns in this region.
        /// </summary>
        public List<FilingsCheckupSuggestedFormModel>? suggestReturns { get; set; }

        /// <summary>
        /// The date when this record was created.
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// The User ID of the user who created this record.
        /// </summary>
        public int? createdUserId { get; set; }

        /// <summary>
        /// The date/time when this record was last modified.
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// The user ID of the user who last modified this record.
        /// </summary>
        public int? modifiedUserId { get; set; }

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
