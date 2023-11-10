// <copyright file="FusionAccountResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionInvoiceResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ServiceStack;

    public class FusionAccount
    {
        public long? PartyId { get; set; }

        public string? PartyNumber { get; set; }

        public string? SourceSystem { get; set; }

        public string? SourceSystemReferenceValue { get; set; }

        public long? OrganizationProfileId { get; set; }

        public string? OrganizationName { get; set; }

        public string? UniqueNameSuffix { get; set; }

        public string? PartyUniqueName { get; set; }

        public string? Type { get; set; }

        public string? SalesProfileNumber { get; set; }

        public int? OwnerPartyId { get; set; }

        public string? OwnerPartyNumber { get; set; }

        public string? OwnerEmailAddress { get; set; }

        public string? OwnerName { get; set; }

        public int? PrimaryContactPartyId { get; set; }

        public string? PrimaryContactPartyNumber { get; set; }

        public string? PrimaryContactSourceSystem { get; set; }

        public string? PrimaryContactSourceSystemReferenceValue { get; set; }

        public string? PrimaryContactName { get; set; }

        public string? PrimaryContactJobTitle { get; set; }

        public string? PrimaryContactEmail { get; set; }

        public string? PrimaryContactPhone { get; set; }

        public bool? ExistingCustomerFlag { get; set; }

        public DateTime? ExistingCustomerFlagLastUpdateDate { get; set; }

        public bool? NamedFlag { get; set; }

        public DateTime? LastAssignmentDateTime { get; set; }

        public bool? AssignmentExceptionFlag { get; set; }

        public string? SalesProfileStatus { get; set; }

        public string? IndustryCodeType { get; set; }

        public string? IndustryCode { get; set; }

        public int? ParentAccountPartyId { get; set; }

        public string? ParentAccountPartyNumber { get; set; }

        public string? ParentAccountSourceSystem { get; set; }

        public string? ParentAccountSourceSystemReferenceValue { get; set; }

        public string? ParentAccountName { get; set; }

        public int? YearEstablished { get; set; }

        public int? ControlYear { get; set; }

        public int? YearIncorporated { get; set; }

        public string? StockSymbol { get; set; }

        public string? FiscalYearendMonth { get; set; }

        public long? CurrentFiscalYearPotentialRevenueAmount { get; set; }

        public string? PreferredFunctionalCurrency { get; set; }

        public string? AnalysisFiscalYear { get; set; }

        public decimal? NextFiscalYearPotentialRevenueAmount { get; set; }

        public string? TaxpayerIdentificationNumber { get; set; }

        public string? CertificationLevel { get; set; }

        public string? CertificationReasonCode { get; set; }

        public string? DUNSNumber { get; set; }

        public string? DUNSCreditRating { get; set; }

        public string? ParentDUNSNumber { get; set; }

        public string? DomesticUltimateDUNSNumber { get; set; }

        public string? GlobalUltimateDUNSNumber { get; set; }

        public string? CEOName { get; set; }

        public string? CEOTitle { get; set; }

        public bool? PublicPrivateOwnershipFlag { get; set; }

        public string? ImportIndicator { get; set; }

        public string? ExportIndicator { get; set; }

        public string? SmallBusinessIndicator { get; set; }

        public string? WomanOwnedIndicator { get; set; }

        public bool? GeneralServicesAdministrationFlag { get; set; }

        public string? MinorityOwnedIndicator { get; set; }

        public string? MinorityOwnedType { get; set; }

        public string? RentOrOwnIndicator { get; set; }

        public string? LaborSurplusIndicator { get; set; }

        public string? OutOfBusinessIndicator { get; set; }

        public string? HQBranchIndicator { get; set; }

        public string? ParentOrSubsidiaryIndicator { get; set; }

        public string? DisadvantageIndicator { get; set; }

        public string? EmployeesAtPrimaryAddress { get; set; }

        public string? EmployeesAtPrimaryAddressEstimation { get; set; }

        public string? EmployeesAtPrimaryAddressMinimum { get; set; }

        public string? EmployeesAtPrimaryAddressText { get; set; }

        public string? EmployeesTotal { get; set; }

        public string? TotalEmployeesEstimatedIndicator { get; set; }

        public string? TotalEmployeesIndicator { get; set; }

        public string? TotalEmployeesMinimumIndicator { get; set; }

        public string? TotalEmployeesText { get; set; }

        public string? OrganizationSize { get; set; }

        public string? OrganizationType { get; set; }

        public string? PrincipalTitle { get; set; }

        public string? PrincipalName { get; set; }

        public string? LegalStatus { get; set; }

        public string? BusinessScope { get; set; }

        public string? RegistrationType { get; set; }

        public string? MissionStatement { get; set; }

        public string? CorporationClass { get; set; }

        public string? GrowthStrategyDescription { get; set; }

        public string? CongressionalDistrictCode { get; set; }

        public string? LineOfBusiness { get; set; }

        public string? HomeCountry { get; set; }

        public string? DoNotConfuseWith { get; set; }

        public string? LocalActivityCode { get; set; }

        public string? LocalActivityCodeType { get; set; }

        public string? LocalBusinessIdentifier { get; set; }

        public string? LocalBusinessIdentifierType { get; set; }

        public string? SiebelLocation { get; set; }

        public string? Comments { get; set; }

        public string? CurrencyCode { get; set; }

        public string? CorpCurrencyCode { get; set; }

        public string? DataCloudStatus { get; set; }

        public string? LastEnrichmentDate { get; set; }

        public string? PartyStatus { get; set; }

        public string? PartyType { get; set; }

        public string? CreatedByModule { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? LastUpdateLogin { get; set; }

        public string? LastUpdatedBy { get; set; }

        public string? PreferredContactMethod { get; set; }

        public string? PhoneContactPointType { get; set; }

        public string? PhoneCountryCode { get; set; }

        public string? PhoneAreaCode { get; set; }

        public string? PhoneNumber { get; set; }

        public string? PhoneExtension { get; set; }

        public string? FormattedPhoneNumber { get; set; }

        public string? RawPhoneNumber { get; set; }

        public string? FaxContactPointType { get; set; }

        public string? FaxCountryCode { get; set; }

        public string? FaxAreaCode { get; set; }

        public string? FaxNumber { get; set; }

        public string? FaxExtension { get; set; }

        public string? FormattedFaxNumber { get; set; }

        public string? RawFaxNumber { get; set; }

        public string? EmailContactPointType { get; set; }

        public string? EmailFormat { get; set; }

        public string? EmailAddress { get; set; }

        public string? WebContactPointType { get; set; }

        public string? URL { get; set; }

        public long? PrimaryAddressId { get; set; }

        public string? PartyNumberKey { get; set; }

        public string? RecordSet { get; set; }

        public string? SourceObjectType { get; set; }

        public bool? FavoriteAccountFlag { get; set; }

        public bool? UpdateFlag { get; set; }

        public bool? DeleteFlag { get; set; }

        public string? OrganizationTypeCode { get; set; }

        public string? AddressNumber { get; set; }

        public string? AddressElementAttribute1 { get; set; }

        public string? AddressElementAttribute2 { get; set; }

        public string? AddressElementAttribute3 { get; set; }

        public string? AddressElementAttribute4 { get; set; }

        public string? AddressElementAttribute5 { get; set; }

        public string? AddressLine1 { get; set; }

        public string? AddressLine2 { get; set; }

        public string? AddressLine3 { get; set; }

        public string? AddressLine4 { get; set; }

        public string? Building { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? County { get; set; }

        public string? FloorNumber { get; set; }

        public string? PostalCode { get; set; }

        public string? PostalPlus4Code { get; set; }

        public string? Province { get; set; }

        public string? State { get; set; }

        public string? Mailstop { get; set; }

        public string? AddressLinesPhonetic { get; set; }

        public string? AddressType { get; set; }

        public string? FormattedAddress { get; set; }

        public string? AccountScore { get; set; }

        public string? AccountScoringTier { get; set; }

        public string? Description { get; set; }

        public string? Keywords { get; set; }

        public int? ConflictId { get; set; }

        public string? DatafoxCompanyId { get; set; }

        public string? UltimateParentPartyId { get; set; }

        public string? UltimateParentName { get; set; }

        public string? UltimateParentPartyNumber { get; set; }

        public string? PhoneVerificationStatus { get; set; }

        public string? PhoneVerificationDate { get; set; }

        public string? FaxVerificationStatus { get; set; }

        public string? FaxVerificationDate { get; set; }

        public string? EmailVerificationStatus { get; set; }

        public string? EmailVerificationDate { get; set; }

        public string? PrimaryContactPhoneVerificationDate { get; set; }

        public string? PrimaryContactEmailVerificationDate { get; set; }

        public bool? PrimaryContactEmailDNC { get; set; }

        public bool? PrimaryContactPhoneDNC { get; set; }

        public string? PrimaryContactEmailVerificationStatus { get; set; }

        public string? PrimaryContactPhoneVerificationStatus { get; set; }

        public string? TotalAccountsInHierarchy { get; set; }

        public string? TotalChildAccounts { get; set; }

        public string? ParentAccountList { get; set; }

        public bool? PrimaryContactDoEmailFlag { get; set; }

        public bool? PrimaryContactDoCallFlag { get; set; }

        public string? TotalImmediateChildAccounts { get; set; }

        public bool? UltimateParentFlag { get; set; }

        public bool? GlobalUltimateFlag { get; set; }

        public bool? DomesticUltimateFlag { get; set; }

        public string? UltimateIdentifierSource { get; set; }

        public string? DomesticUltimateDatafoxId { get; set; }

        public string? GlobalUltimateDatafoxId { get; set; }

        public string? ProfileQualityScore { get; set; }

        public List<Link>? Links { get; set; }
    }

    public class FusionAccountResponse : FusionResponseBase
    {
        [ApiMember(Name = nameof(Items), DataType = "List<FusionAccount>?", ParameterType = "body", IsRequired = true)]
        [JsonProperty("items")]
        public List<FusionAccount>? Items { get; set; }
    }
}
