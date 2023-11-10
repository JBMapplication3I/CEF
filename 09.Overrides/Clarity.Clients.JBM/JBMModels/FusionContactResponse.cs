// <copyright file="FusionContactResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FusionInvoiceResponse class</summary>

namespace Clarity.Clients.JBM
{
    using System;
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ServiceStack;

    public class FusionContact
    {
        public long? PartyId { get; set; }

        public string? PartyNumber { get; set; }

        public string? SourceSystem { get; set; }

        public string? SourceSystemReferenceValue { get; set; }

        public long? AccountPartyId { get; set; }

        public string? AccountPartyNumber { get; set; }

        public string? AccountSourceSystem { get; set; }

        public string? AccountSourceSystemReferenceValue { get; set; }

        public string? AccountName { get; set; }

        public string? AccountUniqueName { get; set; }

        public long? PersonProfileId { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public string? LastNamePrefix { get; set; }

        public string? MiddleName { get; set; }

        public string? UniqueNameSuffix { get; set; }

        public string? ContactName { get; set; }

        public string? PreviousLastName { get; set; }

        public string? SecondLastName { get; set; }

        public string? OwnerPartyId { get; set; }

        public string? OwnerPartyNumber { get; set; }

        public string? OwnerEmailAddress { get; set; }

        public string? OwnerName { get; set; }

        public string? SalesProfileNumber { get; set; }

        public string? Type { get; set; }

        public bool? ExistingCustomerFlag { get; set; }

        public string? ExistingCustomerFlagLastUpdateDate { get; set; }

        public bool? NamedFlag { get; set; }

        public string? LastAssignmentDate { get; set; }

        public bool? AssignmentExceptionFlag { get; set; }

        public string? SalesProfileStatus { get; set; }

        public string? TaxpayerIdentificationNumber { get; set; }

        public string? DateOfBirth { get; set; }

        public string? PlaceOfBirth { get; set; }

        public string? DateOfDeath { get; set; }

        public bool? DeceasedFlag { get; set; }

        public string? Gender { get; set; }

        public string? MaritalStatus { get; set; }

        public string? MaritalStatusEffectiveDate { get; set; }

        public string? DeclaredEthnicity { get; set; }

        public string? PreferredFunctionalCurrency { get; set; }

        public string? PersonalIncomeAmount { get; set; }

        public bool? HeadOfHouseholdFlag { get; set; }

        public string? HouseholdIncomeAmount { get; set; }

        public string? HouseholdSize { get; set; }

        public string? Salutation { get; set; }

        public string? SalutoryIntroduction { get; set; }

        public string? Title { get; set; }

        public string? AcademicTitle { get; set; }

        public string? Initials { get; set; }

        public string? RentOrOwnIndicator { get; set; }

        public string? CertificationLevel { get; set; }

        public string? CertificationReasonCode { get; set; }

        public string? Comments { get; set; }

        public string? PreferredContactMethod { get; set; }

        public string? FavoriteContactFlag { get; set; }

        public string? Department { get; set; }

        public string? DepartmentCode { get; set; }

        public bool? DoNotCallFlag { get; set; }

        public bool? DoNotContactFlag { get; set; }

        public bool? DoNotEmailFlag { get; set; }

        public bool? DoNotMailFlag { get; set; }

        public string? JobTitle { get; set; }

        public string? JobTitleCode { get; set; }

        public string? LastContactDate { get; set; }

        public string? LastKnownGPS { get; set; }

        public string? SalesAffinityCode { get; set; }

        public string? SalesBuyingRoleCode { get; set; }

        public string? CurrencyCode { get; set; }

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

        public string? WorkPhoneContactPointType { get; set; }

        public string? WorkPhoneCountryCode { get; set; }

        public string? WorkPhoneAreaCode { get; set; }

        public string? WorkPhoneNumber { get; set; }

        public string? WorkPhoneExtension { get; set; }

        public string? FormattedWorkPhoneNumber { get; set; }

        public string? RawWorkPhoneNumber { get; set; }

        public string? MobileContactPointType { get; set; }

        public string? MobileCountryCode { get; set; }

        public string? MobileAreaCode { get; set; }

        public string? MobileNumber { get; set; }

        public string? MobileExtension { get; set; }

        public string? FormattedMobileNumber { get; set; }

        public string? RawMobileNumber { get; set; }

        public string? FaxContactPointType { get; set; }

        public string? FaxCountryCode { get; set; }

        public string? FaxAreaCode { get; set; }

        public string? FaxNumber { get; set; }

        public string? FaxExtension { get; set; }

        public string? FormattedFaxNumber { get; set; }

        public string? RawFaxNumber { get; set; }

        public string? HomePhoneContactPointType { get; set; }

        public string? HomePhoneCountryCode { get; set; }

        public string? HomePhoneAreaCode { get; set; }

        public string? HomePhoneNumber { get; set; }

        public string? FormattedHomePhoneNumber { get; set; }

        public string? RawHomePhoneNumber { get; set; }

        public string? EmailContactPointType { get; set; }

        public string? EmailFormat { get; set; }

        public string? EmailAddress { get; set; }

        public long? PrimaryAddressId { get; set; }

        public string? PartyNumberKey { get; set; }

        public long? SellToPartySiteId { get; set; }

        public string? ClassificationCategory { get; set; }

        public string? ClassificationCode { get; set; }

        public string? ContactIsPrimaryForAccount { get; set; }

        public string? NameSuffix { get; set; }

        public string? ContactUniqueName { get; set; }

        public string? RecordSet { get; set; }

        public string? SourceObjectType { get; set; }

        public string? ContactRole { get; set; }

        public string? RegistrationStatus { get; set; }

        public bool? UpdateFlag { get; set; }

        public bool? DeleteFlag { get; set; }

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

        public string? OverallPrimaryFormattedPhoneNumber { get; set; }

        public string? OverallPrimaryPhoneContactPtId { get; set; }

        public string? TimezoneCode { get; set; }

        public int? ConflictId { get; set; }

        public bool? DoNotCallMobileFlag { get; set; }

        public bool? DoNotCallWorkPhoneFlag { get; set; }

        public bool? DoNotCallHomePhoneFlag { get; set; }

        public bool? DoNotContactEmailFlag { get; set; }

        public bool? DoNotContactFaxFlag { get; set; }

        public string? WorkPhoneVerificationStatus { get; set; }

        public string? WorkPhoneVerificationDate { get; set; }

        public string? MobileVerificationStatus { get; set; }

        public string? MobileVerificationDate { get; set; }

        public string? FaxVerificationStatus { get; set; }

        public string? FaxVerificationDate { get; set; }

        public string? HomePhoneVerificationStatus { get; set; }

        public string? HomePhoneVerificationDate { get; set; }

        public string? EmailVerificationStatus { get; set; }

        public string? EmailVerificationDate { get; set; }

        public bool? DoCallMobilePhoneFlag { get; set; }

        public bool? DoCallWorkPhoneFlag { get; set; }

        public bool? DoCallHomePhoneFlag { get; set; }

        public bool? DoContactFaxFlag { get; set; }

        public bool? DoContactEmailFlag { get; set; }

        public string? MobilePhoneContactPtId { get; set; }

        public string? WorkPhoneContactPtId { get; set; }

        public string? HomePhoneContactPtId { get; set; }

        public string? FaxContactPtId { get; set; }

        public string? EmailContactPtId { get; set; }

        public string? RawPhoneNumber { get; set; }

        public List<Link>? Links { get; set; }
    }

    public class FusionContactResponse : FusionResponseBase
    {
        [ApiMember(Name = nameof(Items), DataType = "List<FusionInvoiceHeader>?", ParameterType = "body", IsRequired = true)]
        [JsonProperty("items")]
        public List<FusionContact>? Items { get; set; }
    }
}