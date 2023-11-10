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
    public class FilingReturnModel
    {
        /// <summary>
        /// The unique ID number of this filing return.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The region id that this return belongs too
        /// </summary>
        public long? filingRegionId { get; set; }

        /// <summary>
        /// The unique ID number of the filing calendar associated with this return.
        /// </summary>
        public long? filingCalendarId { get; set; }

        /// <summary>
        /// The resourceFileId of the return. Will be null if not available.
        /// </summary>
        public long? resourceFileId { get; set; }

        /// <summary>
        /// Tax Authority ID of this return
        /// </summary>
        public int? taxAuthorityId { get; set; }

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
        /// The start date of this return
        /// </summary>
        public DateTime? startPeriod { get; set; }

        /// <summary>
        /// The end date of this return
        /// </summary>
        public DateTime? endPeriod { get; set; }

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
        /// Consumer use tax liability during the period.
        /// </summary>
        public decimal? consumerUseTaxAmount { get; set; }

        /// <summary>
        /// Consumer use tax liability accrued during the period.
        /// </summary>
        public decimal? consumerUseTaxDueAmount { get; set; }

        /// <summary>
        /// Consumer use non-taxable amount.
        /// </summary>
        public decimal? consumerUseNonTaxableAmount { get; set; }

        /// <summary>
        /// Consumer use taxable amount.
        /// </summary>
        public decimal? consumerUseTaxableAmount { get; set; }

        /// <summary>
        /// Total amount of adjustments on this return
        /// </summary>
        public decimal? totalAdjustments { get; set; }

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
        /// The amount of carry over sales applied to the liability calculation
        /// </summary>
        public decimal? carryOverSalesAmount { get; set; }

        /// <summary>
        /// The amount of carry over non taxable sales applied to the liability calculation
        /// </summary>
        public decimal? carryOverNonTaxableAmount { get; set; }

        /// <summary>
        /// The amount of carry over sales tax applied to the liability calculation
        /// </summary>
        public decimal? carryOverTaxAmount { get; set; }

        /// <summary>
        /// The amount of carry over consumer use tax applied to the liability calculation
        /// </summary>
        public decimal? carryOverConsumerUseTaxAmount { get; set; }

        /// <summary>
        /// The total amount of total tax accrued in the current active period
        /// </summary>
        public decimal? taxAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of sales accrued in the current active period
        /// </summary>
        public decimal? salesAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of nontaxable sales accrued in the current active period
        /// </summary>
        public decimal? nonTaxableAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of taxable sales accrued in the current active period
        /// </summary>
        public decimal? taxableAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of sales tax accrued in the current active period
        /// </summary>
        public decimal? salesTaxAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of sellers use tax accrued in the current active period
        /// </summary>
        public decimal? sellersUseTaxAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of consumer use tax accrued in the current active period
        /// </summary>
        public decimal? consumerUseTaxAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of consumer use taxable sales accrued in the current active period
        /// </summary>
        public decimal? consumerUseTaxableAccrualAmount { get; set; }

        /// <summary>
        /// The total amount of consumer use non taxable sales accrued in the current active period
        /// </summary>
        public decimal? consumerUseNonTaxableAccrualAmount { get; set; }

        /// <summary>
        /// The Adjustments for this return.
        /// </summary>
        public List<FilingAdjustmentModel>? adjustments { get; set; }

        /// <summary>
        /// Total amount of augmentations on this return
        /// </summary>
        public decimal? totalAugmentations { get; set; }

        /// <summary>
        /// The Augmentations for this return.
        /// </summary>
        public List<FilingAugmentationModel>? augmentations { get; set; }

        /// <summary>
        /// Total amount of payments on this return
        /// </summary>
        public decimal? totalPayments { get; set; }

        /// <summary>
        /// The payments for this return.
        /// </summary>
        public List<FilingPaymentModel>? payments { get; set; }

        /// <summary>
        /// Accrual type of the return
        /// </summary>
        public AccrualType? accrualType { get; set; }

        /// <summary>
        /// The month of the filing period for this tax filing.
        /// The filing period represents the year and month of the last day of taxes being reported on this filing.
        /// For example, an annual tax filing for Jan-Dec 2015 would have a filing period of Dec 2015.
        /// </summary>
        public int? month { get; set; }

        /// <summary>
        /// The year of the filing period for this tax filing.
        /// The filing period represents the year and month of the last day of taxes being reported on this filing.
        /// For example, an annual tax filing for Jan-Dec 2015 would have a filing period of Dec 2015.
        /// </summary>
        public int? year { get; set; }

        /// <summary>
        /// The attachments for this return.
        /// </summary>
        public List<FilingAttachmentModel>? attachments { get; set; }

        /// <summary>
        /// The excluded carry over credit documents
        /// </summary>
        public FilingReturnCreditModel? excludedCarryOverCredits { get; set; }

        /// <summary>
        /// The applied carry over credit documents
        /// </summary>
        public FilingReturnCreditModel? appliedCarryOverCredits { get; set; }

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
