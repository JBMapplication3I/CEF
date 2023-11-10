// <copyright file="CardConnectProfileResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the card connect profile response class</summary>
// ReSharper disable StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    using Newtonsoft.Json;

    /// <summary>The profile response.</summary>
    public class CardConnectProfileResponse
    {
        /// <summary>Gets or sets the identifier of the profile.</summary>
        /// <value>The identifier of the profile.</value>
        [JsonProperty("profileid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ProfileId { get; set; }

        /// <summary>Gets or sets the identifier of the account.</summary>
        /// <value>The identifier of the account.</value>
        [JsonProperty("acctid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountId { get; set; }

        /// <summary>Indicates the status of the profile request.</summary>
        /// <value>The response status.</value>
        [JsonProperty("respstat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ApprovalStatus ResponseStatus { get; set; }

        /// <summary>Indicates the status of the profile request.</summary>
        /// <value>The token.</value>
        [JsonProperty("token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Token { get; set; }

        /// <summary>Alpha-numeric response code that represents the description of the response.</summary>
        /// <value>The response code.</value>
        [JsonProperty("respcode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ResponseCode { get; set; }

        /// <summary>Text description of response.</summary>
        /// <value>The response text.</value>
        [JsonProperty("resptext", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ResponseText { get; set; }

        /// <summary>Abbreviation that represents the platform and the processor for the transaction.</summary>
        /// <value>The response processor.</value>
        [JsonProperty("respproc", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ResponseProcessor { get; set; }

        /// <summary>Abbreviation that represents the platform and the processor for the transaction.</summary>
        /// <value>The type of the account.</value>
        [JsonProperty("accttype", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountType { get; set; }

        /// <summary>Card Expiration in either MMYY or YYYYMMDD format. Not required for eCheck (ACH) requests.</summary>
        /// <value>The expiry mmyy.</value>
        [JsonProperty("expiry", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ExpiryMMYY { get; set; }

        /// <summary>Account name, optional for credit cards and electronic checks (ACH)</summary>
        /// <value>The name of the account holder.</value>
        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountHolderName { get; set; }

        /// <summary>Account street address (required for AVS)</summary>
        /// <value>The address.</value>
        [JsonProperty("address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Address { get; set; }

        /// <summary>Account city.</summary>
        /// <value>The city.</value>
        [JsonProperty("city", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? City { get; set; }

        /// <summary>Account postal code, defaults to "55555". If country is "US", must be 5 or 9 digits. Otherwise any
        /// alphanumeric string is accepted.</summary>
        /// <value>The postal code.</value>
        [JsonProperty("postal", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? PostalCode { get; set; }

        /// <summary>US State, Mexican State, Canadian Province.</summary>
        /// <value>The region.</value>
        [JsonProperty("region", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Region { get; set; }

        /// <summary>Account country (2 char country code), defaults to "US". Required for all non-US addresses.</summary>
        /// <value>The country.</value>
        [JsonProperty("country", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Country { get; set; }

        /// <summary>Account phone.</summary>
        /// <value>The phone.</value>
        [JsonProperty("phone", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Phone { get; set; }

        /// <summary>Company name associated with the account.</summary>
        /// <value>The company.</value>
        [JsonProperty("company", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Company { get; set; }

        /// <summary>Email address of the account holder.</summary>
        /// <value>The email.</value>
        [JsonProperty("email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Email { get; set; }

        /// <summary>"Y" to assign as default account.</summary>
        /// <value>The default account.</value>
        [JsonProperty("defaultacct", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? DefaultAccount { get; set; }

        /// <summary>Identifies if the card is a government issued card.</summary>
        /// <value>The gsa card.</value>
        [JsonProperty("gsacard", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? GsaCard { get; set; }

        /// <summary>Specifies whether or not the profile is set to opt out of the CardPointe Account Updater service.</summary>
        /// <value>The card pointe account updater option out.</value>
        [JsonProperty("auoptout", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? CardPointeAccountUpdaterOptOut { get; set; }
    }
}
