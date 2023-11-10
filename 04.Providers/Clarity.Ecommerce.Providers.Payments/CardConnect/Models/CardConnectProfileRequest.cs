// <copyright file="CardConnectProfileRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the card connect profile request class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    using Newtonsoft.Json;

    /// <summary>The profile request.</summary>
    public class CardConnectProfileRequest
    {
        /// <summary>20-digit profile ID and (optional) 3-digit account ID string in the format &lt;profile id&gt;/&lt;
        /// account id&gt;</summary>
        /// <value>The profile.</value>
        [JsonProperty("profile", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Profile { get; set; }

        /// <summary>"Y" to assign as default account.</summary>
        /// <value>The default account.</value>
        [JsonProperty("defaultacct", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? DefaultAccount { get; set; }

        /// <summary>"Y" to update profile data with non-empty request data only as opposed to full profile replacement
        /// including empty values.</summary>
        /// <value>The profile update.</value>
        [JsonProperty("profileupdate", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ProfileUpdate { get; set; }

        /// <summary>Specifies whether or not the profile is set to opt out of the CardPointe Account Updater service.</summary>
        /// <value>The card pointe account updater option out.</value>
        [JsonProperty("auoptout", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? CardPointeAccountUpdaterOptOut { get; set; }

        /// <summary>One of PPAL, PAID, GIFT, PDEBIT, otherwise not required.</summary>
        /// <value>The type of the account.</value>
        [JsonProperty("accttype", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountType { get; set; }

        /// <summary>Merchant ID, required for all requests.</summary>
        /// <value>The identifier of the merchant.</value>
        [JsonProperty("merchid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? MerchantId { get; set; }

        /// <summary>CardSecure Token, Clear text card number, or Bank Account Number.</summary>
        /// <value>The account.</value>
        [JsonProperty("account", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Account { get; set; }

        /// <summary>Bank routing (ABA) number. Required for eCheck (ACH) authorizations when a bank account number is
        /// provided in the account field.</summary>
        /// <value>The bank aba.</value>
        [JsonProperty("bankaba", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? BankAba { get; set; }

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
    }
}
