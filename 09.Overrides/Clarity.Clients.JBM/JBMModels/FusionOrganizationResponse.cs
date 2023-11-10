// <copyright file="FusionOrganizationResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionInvoiceResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ServiceStack;

    public class FusionOrganization
    {
        public long? PartyId { get; set; }

        public string? PartyNumber { get; set; }

        public string? SourceSystem { get; set; }

        public string? SourceSystemReferenceValue { get; set; }

        public string? OrganizationName { get; set; }

        public string? UniqueNameSuffix { get; set; }

        public string? Status { get; set; }

        public string? CreatedByModule { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? LastUpdatedBy { get; set; }

        public string? PartyUsageCode { get; set; }

        public string? RawPhoneNumber { get; set; }

        public string? PhoneVerificationStatus { get; set; }

        public string? PhoneVerificationDate { get; set; }

        public string? EmailAddress { get; set; }

        public string? EmailVerificationStatus { get; set; }

        public string? EmailVerificationDate { get; set; }

        public string? URL { get; set; }

        public string? AddressNumber { get; set; }

        public string? AddressElementAttribute1 { get; set; }

        public string? AddressElementAttribute2 { get; set; }

        public string? AddressElementAttribute3 { get; set; }

        public string? AddressElementAttribute4 { get; set; }

        public string? AddressElementAttribute5 { get; set; }

        public string? Address1 { get; set; }

        public string? Address2 { get; set; }

        public string? Address3 { get; set; }

        public string? Address4 { get; set; }

        public string? AddressLinesPhonetic { get; set; }

        public string? AddressType { get; set; }

        public string? Building { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? County { get; set; }

        public string? FloorNumber { get; set; }

        public string? Mailstop { get; set; }

        public string? PostalCode { get; set; }

        public string? PostalPlus4Code { get; set; }

        public string? Province { get; set; }

        public string? State { get; set; }

        public string? ThirdPartyFlag { get; set; }

        public bool? ValidatedFlag { get; set; }

        public string? AccountScore { get; set; }

        public string? AccountScoringTier { get; set; }

        public string? AnalysisFiscalYear { get; set; }

        public string? BankCode { get; set; }

        public string? BankOrBranchNumber { get; set; }

        public string? BranchCode { get; set; }

        public string? BranchFlag { get; set; }

        public string? BusinessScope { get; set; }

        public string? CEOName { get; set; }

        public string? CEOTitle { get; set; }

        public string? CertificationLevel { get; set; }

        public string? CertificationReasonCode { get; set; }

        public string? CleanlinessScore { get; set; }

        public string? Comments { get; set; }

        public string? CompletenessScore { get; set; }

        public string? CongressionalDistrictCode { get; set; }

        public string? ControlYear { get; set; }

        public string? CorpCurrencyCode { get; set; }

        public string? CorporationClass { get; set; }

        public string? CurcyConvRateType { get; set; }

        public string? CurrencyCode { get; set; }

        public string? CurrentFiscalYearPotentialRevenueAmount { get; set; }

        public string? DataCloudStatus { get; set; }

        public string? DataConfidenceScore { get; set; }

        public string? DatafoxCompanyId { get; set; }

        public string? Description { get; set; }

        public string? DisadvantageIndicator { get; set; }

        public string? DisplayedDUNSPartyId { get; set; }

        public string? DomesticUltimateDUNSNumber { get; set; }

        public string? DoNotConfuseWith { get; set; }

        public string? DUNSNumber { get; set; }

        public string? DUNSCreditRating { get; set; }

        public string? DuplicateIndicator { get; set; }

        public string? DuplicateScore { get; set; }

        public string? EmployeesAtPrimaryAddress { get; set; }

        public string? EmployeesAtPrimaryAddressEstimation { get; set; }

        public string? EmployeesAtPrimaryAddressMinimum { get; set; }

        public string? EmployeesAtPrimaryAddressText { get; set; }

        public string? EmployeesTotal { get; set; }

        public string? EnquiryDUNS { get; set; }

        public string? EnrichmentScore { get; set; }

        public string? ExportIndicator { get; set; }

        public string? FiscalYearendMonth { get; set; }

        public string? GlobalUltimateDUNSNumber { get; set; }

        public string? GrowthStrategyDescription { get; set; }

        public string? GeneralServicesAdministrationFlag { get; set; }

        public string? HomeCountry { get; set; }

        public string? HQBranchIndicator { get; set; }

        public string? ImportIndicator { get; set; }

        public string? Keywords { get; set; }

        public string? LaborSurplusIndicator { get; set; }

        public string? LastEnrichmentDate { get; set; }

        public string? LastScoreUpdateDate { get; set; }

        public string? LegalStatus { get; set; }

        public string? LineOfBusiness { get; set; }

        public string? LocalActivityCode { get; set; }

        public string? LocalActivityCodeType { get; set; }

        public string? LocalBusinessIdentifier { get; set; }

        public string? LocalBusinessIdentifierType { get; set; }

        public string? MinorityOwnedIndicator { get; set; }

        public string? MinorityOwnedType { get; set; }

        public string? MissionStatement { get; set; }

        public string? NextFiscalYearPotentialRevenueAmount { get; set; }

        public string? OutOfBusinessIndicator { get; set; }

        public string? OrganizationSize { get; set; }

        public string? OrganizationType { get; set; }

        public string? ParentDUNSNumber { get; set; }

        public string? ParentOrSubsidiaryIndicator { get; set; }

        public string? PreferredFunctionalCurrency { get; set; }

        public string? PreferredContactMethod { get; set; }

        public string? PrincipalName { get; set; }

        public string? PrincipalTitle { get; set; }

        public string? PublicPrivateOwnershipFlag { get; set; }

        public string? RecencyScore { get; set; }

        public string? RegistrationType { get; set; }

        public string? RentOrOwnIndicator { get; set; }

        public string? SmallBusinessIndicator { get; set; }

        public string? StockSymbol { get; set; }

        public string? TaxpayerIdentificationNumber { get; set; }

        public string? TotalEmployeesEstimatedIndicator { get; set; }

        public string? TotalEmployeesIndicator { get; set; }

        public string? TotalEmployeesMinimumIndicator { get; set; }

        public string? TotalEmployeesText { get; set; }

        public string? TotalPaymentAmount { get; set; }

        public string? TradingPartnerIdentifier { get; set; }

        public string? UniqueNameAlias { get; set; }

        public string? ValidityScore { get; set; }

        public string? WomanOwnedIndicator { get; set; }

        public string? YearEstablished { get; set; }

        public string? YearIncorporated { get; set; }

        public List<Link>? Links { get; set; }
    }

    public class FusionOrganizationResponse : FusionResponseBase
    {
        [ApiMember(Name = nameof(Items), DataType = "List<FusionInvoiceHeader>?", ParameterType = "body", IsRequired = true)]
        [JsonProperty("items")]
        public List<FusionOrganization>? Items { get; set; }
    }
}