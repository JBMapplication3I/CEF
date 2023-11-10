﻿/*
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
    public class MultiTaxFilingReturnModel
    {
        /// <summary>
        /// The unique ID number of this filing return.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The unique ID number of the filing calendar associated with this return.
        /// </summary>
        public long? filingCalendarId { get; set; }

        /// <summary>
        /// The current status of the filing return.
        /// </summary>
        public FilingStatusId? status { get; set; }

        /// <summary>
        /// The filing frequency of the return.
        /// </summary>
        public FilingFrequencyId? filingFrequency { get; set; }

        /// <summary>
        /// The filing type of the return.
        /// </summary>
        public FilingTypeId? filingType { get; set; }

        /// <summary>
        /// The name of the form.
        /// </summary>
        public string? formName { get; set; }

        /// <summary>
        /// The unique code of the form.
        /// </summary>
        public string? formCode { get; set; }

        /// <summary>
        /// A description for the return.
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// Tax Authority ID of this return
        /// </summary>
        public int? taxAuthorityId { get; set; }

        /// <summary>
        /// The date the return was filed by Avalara.
        /// </summary>
        public DateTime? filedDate { get; set; }

        /// <summary>
        /// Accrual type of the return
        /// </summary>
        public AccrualType? accrualType { get; set; }

        /// <summary>
        /// The start date of this return
        /// </summary>
        public DateTime? startPeriod { get; set; }

        /// <summary>
        /// The end date of this return
        /// </summary>
        public DateTime? endPeriod { get; set; }

        /// <summary>
        /// A summary of all taxes compbined for this period
        /// </summary>
        public FilingsTaxSummaryModel? returnTaxSummary { get; set; }

        /// <summary>
        /// A detailed breakdown of the taxes in this filing
        /// </summary>
        public List<FilingsTaxDetailsModel>? returnTaxDetails { get; set; }

        /// <summary>
        /// The excluded carry over credit documents
        /// </summary>
        public FilingReturnCreditModel? excludedCarryOverCredits { get; set; }

        /// <summary>
        /// The applied carry over credit documents
        /// </summary>
        public FilingReturnCreditModel? appliedCarryOverCredits { get; set; }

        /// <summary>
        /// Total amount of adjustments on this return
        /// </summary>
        public decimal? totalAdjustments { get; set; }

        /// <summary>
        /// The Adjustments for this return.
        /// </summary>
        public List<FilingAdjustmentModel>? adjustments { get; set; }

        /// <summary>
        /// Total amount of payments on this return
        /// </summary>
        public decimal? totalPayments { get; set; }

        /// <summary>
        /// The payments for this return.
        /// </summary>
        public List<FilingPaymentModel>? payments { get; set; }

        /// <summary>
        /// The attachments for this return.
        /// </summary>
        public List<FilingAttachmentModel>? attachments { get; set; }

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
