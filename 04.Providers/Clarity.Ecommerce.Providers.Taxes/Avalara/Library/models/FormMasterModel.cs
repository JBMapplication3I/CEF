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
    /// Represents information about a tax form known to Avalara
    /// </summary>
    public class FormMasterModel
    {
        /// <summary>
        /// Unique ID number of this form master object
        /// </summary>
        public int? id { get; set; }

        /// <summary>
        /// The type of the form being submitted
        /// </summary>
        public int? formTypeId { get; set; }

        /// <summary>
        /// Unique tax form code representing this tax form
        /// </summary>
        public string? taxFormCode { get; set; }

        /// <summary>
        /// Legacy return name as known in the AvaFileForm table
        /// </summary>
        public string? legacyReturnName { get; set; }

        /// <summary>
        /// Human readable form summary name
        /// </summary>
        public string? taxFormName { get; set; }

        /// <summary>
        /// Description of this tax form
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// True if this form is available for use
        /// </summary>
        public bool? isEffective { get; set; }

        /// <summary>
        /// ISO 3166 code of the country that issued this tax form
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// The region within which this form was issued
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// Tax authority that issued the form
        /// </summary>
        public string? authorityName { get; set; }

        /// <summary>
        /// DEPRECATED
        /// </summary>
        public string? shortCode { get; set; }

        /// <summary>
        /// Day of the month when the form is due
        /// </summary>
        public int? dueDay { get; set; }

        /// <summary>
        /// Day of the month on which the form is considered delinquent. Almost always the same as DueDay
        /// </summary>
        public int? delinquentDay { get; set; }

        /// <summary>
        /// Month of the year the state considers as the first fiscal month
        /// </summary>
        public int? fiscalYearStartMonth { get; set; }

        /// <summary>
        /// Can form support multi frequencies
        /// </summary>
        public bool? hasMultiFrequencies { get; set; }

        /// <summary>
        /// Does this tax authority require a power of attorney in order to speak to Avalara
        /// </summary>
        public bool? isPOARequired { get; set; }

        /// <summary>
        /// True if this form requires that the customer register with the authority
        /// </summary>
        public bool? isRegistrationRequired { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasMultiRegistrationMethods { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasSchedules { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasMultiFilingMethods { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasMultiPayMethods { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? isEFTRequired { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? isFilePayMethodLinked { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public int? mailingReceivedRuleId { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public int? proofOfMailingId { get; set; }

        /// <summary>
        /// True if you can report a negative amount in a single jurisdiction on the form
        /// </summary>
        public bool? isNegAmountAllowed { get; set; }

        /// <summary>
        /// True if the form overall can go negative
        /// </summary>
        public bool? allowNegativeOverallTax { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? isNettingRequired { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public int? roundingMethodId { get; set; }

        /// <summary>
        /// Total amount of discounts that can be received by a vendor each year
        /// </summary>
        public decimal? vendorDiscountAnnualMax { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? versionsRequireAuthorityApproval { get; set; }

        /// <summary>
        /// Type of outlet reporting for this form
        /// </summary>
        public int? outletReportingMethodId { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasReportingCodes { get; set; }

        /// <summary>
        /// Not sure if used
        /// </summary>
        public bool? hasPrepayments { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? grossIncludesInterstateSales { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? grossIncludesTax { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasEfileFee { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasEpayFee { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? hasDependencies { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? requiredEfileTrigger { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? requiredEftTrigger { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? vendorDiscountEfile { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? vendorDiscountPaper { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? peerReviewed { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? peerReviewedId { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? peerReviewedDate { get; set; }

        /// <summary>
        /// ID of the Avalara user who created the form
        /// </summary>
        public int? createdUserId { get; set; }

        /// <summary>
        /// Date when form was created
        /// </summary>
        public DateTime? createdDate { get; set; }

        /// <summary>
        /// ID of the Avalara user who modified the form
        /// </summary>
        public int? modifiedUserId { get; set; }

        /// <summary>
        /// Date when form was modified
        /// </summary>
        public DateTime? modifiedDate { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddressMailTo { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddress1 { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddress2 { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddressCity { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddressRegion { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddressPostalCode { get; set; }

        /// <summary>
        /// Mailing address of the department of revenue
        /// </summary>
        public string? dorAddressCountry { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddressMailTo { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddress1 { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddress2 { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddressCity { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddressRegion { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddressPostalCode { get; set; }

        /// <summary>
        /// Mailing address to use when a zero dollar form is filed
        /// </summary>
        public string? zeroAddressCountry { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddressMailTo { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddress1 { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddress2 { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddressCity { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddressRegion { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddressPostalCode { get; set; }

        /// <summary>
        /// Mailing address to use when filing an amended return
        /// </summary>
        public string? amendedAddressCountry { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? onlineBackFiling { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? onlineAmendedReturns { get; set; }

        /// <summary>
        /// --Need Further Clarification
        /// </summary>
        public string? prepaymentFrequency { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? outletLocationIdentifiersRequired { get; set; }

        /// <summary>
        /// --Need Further Clarification
        /// </summary>
        public string? listingSortOrder { get; set; }

        /// <summary>
        /// Link to the state department of revenue website, if available
        /// </summary>
        public string? dorWebsite { get; set; }

        /// <summary>
        /// --Need Further Clarification
        /// </summary>
        public bool? fileForAllOutlets { get; set; }

        /// <summary>
        /// --Need Further Clarification
        /// </summary>
        public bool? paperFormsDoNotHaveDiscounts { get; set; }

        /// <summary>
        /// Internal behavior
        /// </summary>
        public bool? stackAggregation { get; set; }

        /// <summary>
        /// --Need Further Clarification
        /// </summary>
        public string? roundingPrecision { get; set; }

        /// <summary>
        /// --Need Further Clarification
        /// </summary>
        public string? inconsistencyTolerance { get; set; }

        /// <summary>
        /// Date when this form became effective
        /// </summary>
        public DateTime? effDate { get; set; }

        /// <summary>
        /// Date when this form expired
        /// </summary>
        public DateTime? endDate { get; set; }

        /// <summary>
        /// True if this form can be shown to customers
        /// </summary>
        public bool? visibleToCustomers { get; set; }

        /// <summary>
        /// True if this form requires that you set up outlets in the state
        /// </summary>
        public bool? requiresOutletSetup { get; set; }

        /// <summary>
        /// True if this state permits payment by ACH Credit
        /// </summary>
        public bool? achCreditAllowed { get; set; }

        /// <summary>
        /// Jurisdiction level of the state
        /// </summary>
        public string? reportLevel { get; set; }

        /// <summary>
        /// True if this form is verified filed via email
        /// </summary>
        public bool? postOfficeValidated { get; set; }

        /// <summary>
        /// Internal Avalara flag
        /// </summary>
        public string? stackAggregationOption { get; set; }

        /// <summary>
        /// Internal Avalara flag
        /// </summary>
        public string? sstBehavior { get; set; }

        /// <summary>
        /// Internal Avalara flag
        /// </summary>
        public string? nonSstBehavior { get; set; }

        /// <summary>
        /// Phone number of the department of revenue
        /// </summary>
        public string? dorPhoneNumber { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? averageCheckClearDays { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? filterZeroRatedLineDetails { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public bool? allowsBulkFilingAccounts { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? bulkAccountInstructionLink { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? registrationIdFormat { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? thresholdTrigger { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? transactionSortingOption { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public int? contentReviewFrequencyId { get; set; }

        /// <summary>
        /// Unused
        /// </summary>
        public string? aliasForFormMasterId { get; set; }

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
