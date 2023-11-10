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
    /// Filing Returns Model
    /// </summary>
    public class FilingReturnModelBasic
    {
        /// <summary>
        /// The unique ID number of the company filing return.
        /// </summary>
        public long? companyId { get; set; }

        /// <summary>
        /// The unique ID number of this filing return.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The filing id that this return belongs too
        /// </summary>
        public long? filingId { get; set; }

        /// <summary>
        /// The resourceFileId of the return
        /// </summary>
        public long? resourceFileId { get; set; }

        /// <summary>
        /// The region id that this return belongs too
        /// </summary>
        public long? filingRegionId { get; set; }

        /// <summary>
        /// The unique ID number of the filing calendar associated with this return.
        /// </summary>
        public long? filingCalendarId { get; set; }

        /// <summary>
        /// The country of the form.
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// The region of the form.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// The month of the filing period for this tax filing.
        /// The filing period represents the year and month of the last day of taxes being reported on this filing.
        /// For example, an annual tax filing for Jan-Dec 2015 would have a filing period of Dec 2015.
        /// </summary>
        public int? endPeriodMonth { get; set; }

        /// <summary>
        /// The year of the filing period for this tax filing.
        /// The filing period represents the year and month of the last day of taxes being reported on this filing.
        /// For example, an annual tax filing for Jan-Dec 2015 would have a filing period of Dec 2015.
        /// </summary>
        public short? endPeriodYear { get; set; }

        /// <summary>
        /// The current status of the filing return.
        /// </summary>
        public FilingStatusId? status { get; set; }

        /// <summary>
        /// The filing frequency of the return.
        /// </summary>
        public FilingFrequencyId? filingFrequency { get; set; }

        /// <summary>
        /// The date the return was filed by Avalara.
        /// </summary>
        public DateTime? filedDate { get; set; }

        /// <summary>
        /// The sales amount.
        /// </summary>
        public decimal? salesAmount { get; set; }

        /// <summary>
        /// The filing type of the return.
        /// </summary>
        public FilingTypeId? filingType { get; set; }

        /// <summary>
        /// The name of the form.
        /// </summary>
        public string? formName { get; set; }

        /// <summary>
        /// The remittance amount of the return.
        /// </summary>
        public decimal? remitAmount { get; set; }

        /// <summary>
        /// The unique code of the form.
        /// </summary>
        public string? formCode { get; set; }

        /// <summary>
        /// A description for the return.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// The taxable amount.
        /// </summary>
        public decimal? taxableAmount { get; set; }

        /// <summary>
        /// The tax amount.
        /// </summary>
        public decimal? taxAmount { get; set; }

        /// <summary>
        /// The amount collected by avalara for this return
        /// </summary>
        public decimal? collectAmount { get; set; }

        /// <summary>
        /// The tax due amount.
        /// </summary>
        public decimal? taxDueAmount { get; set; }

        /// <summary>
        /// The non-taxable amount.
        /// </summary>
        public decimal? nonTaxableAmount { get; set; }

        /// <summary>
        /// The non-taxable due amount.
        /// </summary>
        public decimal? nonTaxableDueAmount { get; set; }

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
        /// The amount of sales excluded from the liability calculation
        /// </summary>
        public decimal? excludedSalesAmount { get; set; }

        /// <summary>
        /// The amount of non-taxable sales excluded from the liability calculation
        /// </summary>
        public decimal? excludedNonTaxableAmount { get; set; }

        /// <summary>
        /// The amount of tax excluded from the liability calculation
        /// </summary>
        public decimal? excludedTaxAmount { get; set; }

        /// <summary>
        /// Accrual type of the return
        /// </summary>
        public AccrualType? accrualType { get; set; }

        /// <summary>
        /// The attachments for this return.
        /// </summary>
        public List<FilingAttachmentModel>? attachments { get; set; }

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
        /// Convert this object to a JSON string of itself
        /// </summary>
        /// <returns>A JSON string of this object.</returns>
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, new JsonSerializerSettings() { Formatting = Formatting.Indented });
        }
    }
}
