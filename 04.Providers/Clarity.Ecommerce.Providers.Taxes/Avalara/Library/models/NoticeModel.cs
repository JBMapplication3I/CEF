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
    /// Represents a letter received from a tax authority regarding tax filing.
    /// These letters often have the warning "Notice" printed at the top, which is why
    /// they are called "Notices".
    /// </summary>
    public class NoticeModel
    {
        /// <summary>
        /// The unique ID number of this notice.
        /// </summary>
        public int id { get; set; }

        /// <summary>
        /// The unique ID number of the company to which this notice belongs.
        /// </summary>
        public int companyId { get; set; }

        /// <summary>
        /// The status id of the notice
        /// </summary>
        public int statusId { get; set; }

        /// <summary>
        /// The status of the notice
        /// </summary>
        public string? status { get; set; }

        /// <summary>
        /// The received date of the notice
        /// </summary>
        public DateTime receivedDate { get; set; }

        /// <summary>
        /// The closed date of the notice
        /// </summary>
        public DateTime? closedDate { get; set; }

        /// <summary>
        /// The total remmitance amount for the notice
        /// </summary>
        public decimal? totalRemit { get; set; }

        /// <summary>
        /// NoticeCustomerTypeID can be retrieved from the definitions API
        /// </summary>
        public NoticeCustomerType customerTypeId { get; set; }

        /// <summary>
        /// Name or ISO 3166 code identifying the country that sent this notice.
        ///  
        /// This field supports many different country identifiers:
        ///  * Two character ISO 3166 codes
        ///  * Three character ISO 3166 codes
        ///  * Fully spelled out names of the country in ISO supported languages
        ///  * Common alternative spellings for many countries
        ///  
        /// For a full list of all supported codes and names, please see the Definitions API `ListCountries`.
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// Name or ISO 3166 code identifying the region within the country that sent this notice.
        ///  
        /// This field supports many different region identifiers:
        ///  * Two and three character ISO 3166 region codes
        ///  * Fully spelled out names of the region in ISO supported languages
        ///  * Common alternative spellings for many regions
        ///  
        /// For a full list of all supported codes and names, please see the Definitions API `ListRegions`.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// The tax authority id of the notice
        /// </summary>
        public int? taxAuthorityId { get; set; }

        /// <summary>
        /// The filing frequency of the notice
        /// </summary>
        public FilingFrequencyId? filingFrequency { get; set; }

        /// <summary>
        /// The filing type of the notice
        /// </summary>
        public TaxNoticeFilingTypeId? filingTypeId { get; set; }

        /// <summary>
        /// The ticket reference number of the notice
        /// </summary>
        public string? ticketReferenceNo { get; set; }

        /// <summary>
        /// The ticket reference url of the notice
        /// </summary>
        public string? ticketReferenceUrl { get; set; }

        /// <summary>
        /// The sales force case of the notice
        /// </summary>
        public string? salesForceCase { get; set; }

        /// <summary>
        /// The URL to the sales force case
        /// </summary>
        public string? salesForceCaseUrl { get; set; }

        /// <summary>
        /// The tax period of the notice
        /// </summary>
        public string? taxPeriod { get; set; }

        /// <summary>
        /// The notice reason id
        /// </summary>
        public int reasonId { get; set; }

        /// <summary>
        /// The notice reason
        /// </summary>
        public string? reason { get; set; }

        /// <summary>
        /// The tax notice type id
        /// </summary>
        public int? typeId { get; set; }

        /// <summary>
        /// The tax notice type description
        /// </summary>
        public string? type { get; set; }

        /// <summary>
        /// The notice customer funding options
        /// </summary>
        public FundingOption? customerFundingOptionId { get; set; }

        /// <summary>
        /// The priority of the notice
        /// </summary>
        public NoticePriorityId priorityId { get; set; }

        /// <summary>
        /// Comments from the customer on this notice
        /// </summary>
        public string? customerComment { get; set; }

        /// <summary>
        /// Indicator to hide from customer
        /// </summary>
        public bool hideFromCustomer { get; set; }

        /// <summary>
        /// Expected resolution date of the notice
        /// </summary>
        public DateTime? expectedResolutionDate { get; set; }

        /// <summary>
        /// Indicator to show customer this resolution date
        /// </summary>
        public bool showResolutionDateToCustomer { get; set; }

        /// <summary>
        /// The unique ID number of the user that closed the notice
        /// </summary>
        public int? closedByUserId { get; set; }

        /// <summary>
        /// The user who created the notice
        /// </summary>
        public string? createdByUserName { get; set; }

        /// <summary>
        /// The unique ID number of the user that owns the notice
        /// </summary>
        public int? ownedByUserId { get; set; }

        /// <summary>
        /// The description of the notice
        /// </summary>
        public string? description { get; set; }

        /// <summary>
        /// The ava file form id of the notice
        /// </summary>
        public int? avaFileFormId { get; set; }

        /// <summary>
        /// The id of the revenue contact
        /// </summary>
        public int? revenueContactId { get; set; }

        /// <summary>
        /// The id of the compliance contact
        /// </summary>
        public int? complianceContactId { get; set; }

        /// <summary>
        /// The tax form code of the notice
        /// </summary>
        public string? taxFormCode { get; set; }

        /// <summary>
        /// The document reference of the notice
        /// </summary>
        public string? documentReference { get; set; }

        /// <summary>
        /// The jurisdiction name of the notice
        /// </summary>
        public string? jurisdictionName { get; set; }

        /// <summary>
        /// The jurisdiction type of the notice
        /// </summary>
        public string? jurisdictionType { get; set; }

        /// <summary>
        /// Additional comments on the notice
        /// </summary>
        public List<NoticeCommentModel>? comments { get; set; }

        /// <summary>
        /// Finance details of the notice
        /// </summary>
        public List<NoticeFinanceModel>? finances { get; set; }

        /// <summary>
        /// Notice Responsibility Details
        /// </summary>
        public List<NoticeResponsibilityDetailModel>? responsibility { get; set; }

        /// <summary>
        /// Notice Root Cause Details
        /// </summary>
        public List<NoticeRootCauseDetailModel>? rootCause { get; set; }

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
