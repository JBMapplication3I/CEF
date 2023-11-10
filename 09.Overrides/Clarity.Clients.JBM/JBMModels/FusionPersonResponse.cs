// <copyright file="FusionPersonResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionInvoiceResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ServiceStack;

    public class FusionPerson
    {
        public long? PartyId { get; set; }

        public string? PartyNumber { get; set; }

        public string? PersonName { get; set; }

        public string? UniqueNameSuffix { get; set; }

        public string? Status { get; set; }

        public string? CreatedByModule { get; set; }

        public string? CreatedBy { get; set; }

        public DateTime? CreationDate { get; set; }

        public DateTime? LastUpdateDate { get; set; }

        public string? LastUpdatedBy { get; set; }

        public string? ThirdPartyFlag { get; set; }

        public string? ValidatedFlag { get; set; }

        public string? AcademicTitle { get; set; }

        public string? CertificationLevel { get; set; }

        public string? CertificationReasonCode { get; set; }

        public string? Comments { get; set; }

        public string? ContactRole { get; set; }

        public string? CorpCurrencyCode { get; set; }

        public string? CurcyConvRateType { get; set; }

        public string? CurrencyCode { get; set; }

        public string? DataCloudStatus { get; set; }

        public string? DateOfBirth { get; set; }

        public string? DateOfDeath { get; set; }

        public bool? DeceasedFlag { get; set; }

        public string? DeclaredEthnicity { get; set; }

        public string? Department { get; set; }

        public string? DepartmentCode { get; set; }

        public string? DoNotCallFlag { get; set; }

        public string? DoNotContactFlag { get; set; }

        public string? DoNotEmailFlag { get; set; }

        public string? DoNotMailFlag { get; set; }

        public string? FirstName { get; set; }

        public string? Gender { get; set; }

        public string? HeadOfHouseholdFlag { get; set; }

        public string? HouseholdIncomeAmount { get; set; }

        public string? HouseholdSize { get; set; }

        public string? Initials { get; set; }

        public string? TaxpayerIdentificationNumber { get; set; }

        public string? JobTitle { get; set; }

        public string? JobTitleCode { get; set; }

        public string? LastContactDate { get; set; }

        public string? LastEnrichmentDate { get; set; }

        public string? LastName { get; set; }

        public string? LastNamePrefix { get; set; }

        public string? MaritalStatus { get; set; }

        public string? MaritalStatusEffectiveDate { get; set; }

        public string? MiddleName { get; set; }

        public string? NameSuffix { get; set; }

        public string? PersonalIncomeAmount { get; set; }

        public string? Title { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? PreviousLastName { get; set; }

        public string? PreferredContactMethod { get; set; }

        public string? PreferredFunctionalCurrency { get; set; }

        public string? RegistrationStatus { get; set; }

        public string? RentOrOwnIndicator { get; set; }

        public string? SalesAffinityCode { get; set; }

        public string? SalesBuyingRoleCode { get; set; }

        public string? Salutation { get; set; }

        public string? SalutoryIntroduction { get; set; }

        public string? SecondLastName { get; set; }

        public string? DataConfidenceScore { get; set; }

        public string? LastScoreUpdateDate { get; set; }

        public string? DuplicateIndicator { get; set; }

        public string? CompletenessScore { get; set; }

        public string? ValidityScore { get; set; }

        public string? EnrichmentScore { get; set; }

        public string? RecencyScore { get; set; }

        public string? CleanlinessScore { get; set; }

        public string? DuplicateScore { get; set; }

        public string? TimezoneCode { get; set; }

        public string? OrganizationPartyId { get; set; }

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

        public string? Building { get; set; }

        public string? City { get; set; }

        public string? Country { get; set; }

        public string? County { get; set; }

        public string? FloorNumber { get; set; }

        public string? PostalCode { get; set; }

        public string? PostalPlus4Code { get; set; }

        public string? Province { get; set; }

        public string? State { get; set; }

        public string? AddressNumber { get; set; }

        public string? Mailstop { get; set; }

        public string? SourceSystem { get; set; }

        public string? SourceSystemReferenceValue { get; set; }

        public string? AddressType { get; set; }

        public string? PartyUsageCode { get; set; }

        public string? RawHomePhoneNumber { get; set; }

        public string? HomePhoneVerificationStatus { get; set; }

        public string? HomePhoneVerificationDate { get; set; }

        public string? RawMobilePhoneNumber { get; set; }

        public string? MobilePhoneVerificationStatus { get; set; }

        public string? MobilePhoneVerificationDate { get; set; }

        public string? RawWorkPhoneNumber { get; set; }

        public string? WorkPhoneVerificationStatus { get; set; }

        public string? WorkPhoneVerificationDate { get; set; }

        public string? EmailAddress { get; set; }

        public string? EmailVerificationStatus { get; set; }

        public string? EmailVerificationDate { get; set; }

        public string? OrganizationPartyNumber { get; set; }

        public string? OrganizationPartyName { get; set; }

        public string? OrganizationSourceSystem { get; set; }

        public string? OrganizationSourceSystemReferenceValue { get; set; }
    }

    public class FusionPersonResponse : FusionResponseBase
    {
        [ApiMember(Name = nameof(Items), DataType = "List<FusionInvoiceHeader>?", ParameterType = "body", IsRequired = true)]
        [JsonProperty("items")]
        public List<FusionPerson>? Items { get; set; }
    }
}