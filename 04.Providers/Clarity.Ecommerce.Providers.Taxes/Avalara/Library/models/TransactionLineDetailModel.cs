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
    /// An individual tax detail element. Represents the amount of tax calculated for a particular jurisdiction, for a particular line in an invoice.
    /// </summary>
    public class TransactionLineDetailModel
    {
        /// <summary>
        /// The unique ID number of this tax detail.
        /// </summary>
        public long? id { get; set; }

        /// <summary>
        /// The unique ID number of the line within this transaction.
        /// </summary>
        public long? transactionLineId { get; set; }

        /// <summary>
        /// The unique ID number of this transaction.
        /// </summary>
        public long? transactionId { get; set; }

        /// <summary>
        /// The unique ID number of the address used for this tax detail.
        /// </summary>
        public long? addressId { get; set; }

        /// <summary>
        /// The two character ISO 3166 country code of the country where this tax detail is assigned.
        /// </summary>
        public string? country { get; set; }

        /// <summary>
        /// The two-or-three character ISO region code for the region where this tax detail is assigned.
        /// </summary>
        public string? region { get; set; }

        /// <summary>
        /// For U.S. transactions, the Federal Information Processing Standard (FIPS) code for the county where this tax detail is assigned.
        /// </summary>
        public string? countyFIPS { get; set; }

        /// <summary>
        /// For U.S. transactions, the Federal Information Processing Standard (FIPS) code for the state where this tax detail is assigned.
        /// </summary>
        public string? stateFIPS { get; set; }

        /// <summary>
        /// The amount of this line that was considered exempt in this tax detail.
        /// </summary>
        public decimal? exemptAmount { get; set; }

        /// <summary>
        /// The unique ID number of the exemption reason for this tax detail.
        /// </summary>
        public int? exemptReasonId { get; set; }

        /// <summary>
        /// True if this detail element represented an in-state transaction.
        /// </summary>
        public bool? inState { get; set; }

        /// <summary>
        /// The code of the jurisdiction to which this tax detail applies.
        /// </summary>
        public string? jurisCode { get; set; }

        /// <summary>
        /// The name of the jurisdiction to which this tax detail applies.
        /// </summary>
        public string? jurisName { get; set; }

        /// <summary>
        /// The unique ID number of the jurisdiction to which this tax detail applies.
        /// </summary>
        public int? jurisdictionId { get; set; }

        /// <summary>
        /// The Avalara-specified signature code of the jurisdiction to which this tax detail applies.
        /// </summary>
        public string? signatureCode { get; set; }

        /// <summary>
        /// The state assigned number of the jurisdiction to which this tax detail applies.
        /// </summary>
        public string? stateAssignedNo { get; set; }

        /// <summary>
        /// DEPRECATED - Date: 12/20/2017, Version: 18.1, Message: Use jurisdictionTypeId instead.
        /// The type of the jurisdiction to which this tax detail applies.
        /// </summary>
        public JurisTypeId? jurisType { get; set; }

        /// <summary>
        /// The type of the jurisdiction in which this tax detail applies.
        /// </summary>
        public JurisdictionType? jurisdictionType { get; set; }

        /// <summary>
        /// The amount of this line item that was considered nontaxable in this tax detail.
        /// </summary>
        public decimal? nonTaxableAmount { get; set; }

        /// <summary>
        /// The rule according to which portion of this detail was considered nontaxable.
        /// </summary>
        public int? nonTaxableRuleId { get; set; }

        /// <summary>
        /// The type of nontaxability that was applied to this tax detail.
        /// </summary>
        public TaxRuleTypeId? nonTaxableType { get; set; }

        /// <summary>
        /// The rate at which this tax detail was calculated.
        /// </summary>
        public decimal? rate { get; set; }

        /// <summary>
        /// The unique ID number of the rule according to which this tax detail was calculated.
        /// </summary>
        public int? rateRuleId { get; set; }

        /// <summary>
        /// The unique ID number of the source of the rate according to which this tax detail was calculated.
        /// </summary>
        public int? rateSourceId { get; set; }

        /// <summary>
        /// For Streamlined Sales Tax customers, the SST Electronic Return code under which this tax detail should be applied.
        /// </summary>
        public string? serCode { get; set; }

        /// <summary>
        /// Indicates whether this tax detail applies to the origin or destination of the transaction.
        /// </summary>
        public Sourcing? sourcing { get; set; }

        /// <summary>
        /// The amount of tax for this tax detail.
        /// </summary>
        public decimal? tax { get; set; }

        /// <summary>
        /// The taxable amount of this tax detail.
        /// </summary>
        public decimal? taxableAmount { get; set; }

        /// <summary>
        /// The type of tax that was calculated. Depends on the company's nexus settings as well as the jurisdiction's tax laws.
        /// </summary>
        public TaxType? taxType { get; set; }

        /// <summary>
        /// The id of the tax subtype.
        /// </summary>
        public string? taxSubTypeId { get; set; }

        /// <summary>
        /// The id of the tax type group.
        /// </summary>
        public string? taxTypeGroupId { get; set; }

        /// <summary>
        /// The name of the tax against which this tax amount was calculated.
        /// </summary>
        public string? taxName { get; set; }

        /// <summary>
        /// The type of the tax authority to which this tax will be remitted.
        /// </summary>
        public int? taxAuthorityTypeId { get; set; }

        /// <summary>
        /// The unique ID number of the tax region.
        /// </summary>
        public int? taxRegionId { get; set; }

        /// <summary>
        /// The amount of tax that AvaTax calculated.
        /// If an override for tax amount is used, there may be a difference between the tax
        /// field which applies your override, and the this amount that is calculated without override.
        /// </summary>
        public decimal? taxCalculated { get; set; }

        /// <summary>
        /// The amount of tax override that was specified for this tax line.
        /// </summary>
        public decimal? taxOverride { get; set; }

        /// <summary>
        /// DEPRECATED - Date: 12/20/2017, Version: 18.1, Message: Please use rateTypeCode instead.
        /// The rate type for this tax detail.
        /// </summary>
        public RateType? rateType { get; set; }

        /// <summary>
        /// Indicates the code of the rate type that was used to calculate this tax detail. Use [ListRateTypesByCountry](https://developer.avalara.com/api-reference/avatax/rest/v2/methods/Definitions/ListRateTypesByCountry/) API for a full list of rate type codes.
        /// </summary>
        public string? rateTypeCode { get; set; }

        /// <summary>
        /// Number of units in this line item that were calculated to be taxable according to this rate detail.
        /// </summary>
        public decimal? taxableUnits { get; set; }

        /// <summary>
        /// Number of units in this line item that were calculated to be nontaxable according to this rate detail.
        /// </summary>
        public decimal? nonTaxableUnits { get; set; }

        /// <summary>
        /// Number of units in this line item that were calculated to be exempt according to this rate detail.
        /// </summary>
        public decimal? exemptUnits { get; set; }

        /// <summary>
        /// When calculating units, what basis of measurement did we use for calculating the units?
        /// </summary>
        public string? unitOfBasis { get; set; }

        /// <summary>
        /// True if this value is a non-passthrough tax.
        ///  
        /// A non-passthrough tax is a tax that may not be charged to a customer; it must be paid directly by the company.
        /// </summary>
        public bool? isNonPassThru { get; set; }

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
