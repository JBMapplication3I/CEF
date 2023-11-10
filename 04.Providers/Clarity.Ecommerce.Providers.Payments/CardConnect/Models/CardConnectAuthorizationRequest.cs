// <copyright file="CardConnectAuthorizationRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the card connect authorization request class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    using Newtonsoft.Json;

    /// <summary>The authorization request.</summary>
    public class CardConnectAuthorizationRequest
    {
        /// <summary>CardConnect merchant ID, required for all requests.</summary>
        /// <value>The identifier of the merchant.</value>
        [JsonProperty("merchid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? MerchantId { get; set; }

        /// <summary>Amount with decimal or without decimal in currency minor units (for example, USD Pennies or EUR
        /// Cents).</summary>
        /// <value>The amount in cents.</value>
        [JsonProperty("amount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AmountInCents { get; set; }

        /// <summary>Card Expiration in either MMYY or YYYYMMDD format. Not required for eCheck (ACH) requests.</summary>
        /// <value>The expiry mmyy.</value>
        [JsonProperty("expiry", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ExpiryMMYY { get; set; }

        /// <summary>CardSecure Token, Clear text card number, or Bank Account Number.</summary>
        /// <value>The account.</value>
        [JsonProperty("account", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Account { get; set; }

        /// <summary>Currency of the authorization (for example, USD for US dollars or CAD for Canadian Dollars).</summary>
        /// <value>The currency.</value>
        [JsonProperty("currency", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Currency { get; set; }

        /// <summary>Bank routing (ABA) number. Required for eCheck (ACH) authorizations when a bank account number is
        /// provided in the account field.</summary>
        /// <value>The bank aba.</value>
        [JsonProperty("bankaba", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? BankAba { get; set; }

        /// <summary>Payment card track data. Can be unencrypted Track 1 or Track 2 data, or encrypted swipe data
        /// (containing Track 1 and/or Track 2) data.</summary>
        /// <value>The track.</value>
        [JsonProperty("track", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Track { get; set; }

        /// <summary>Optional, to return receipt data fields in the authorization response.</summary>
        /// <value>The receipt.</value>
        [JsonProperty("receipt", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Receipt { get; set; }

        /// <summary>Optional, to return BIN lookup fields in the authorization response. Specify Y to retrieve the BIN
        /// data for the card.</summary>
        /// <value>The bin.</value>
        [JsonProperty("bin", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Bin { get; set; }

        /// <summary>Optional, to create an account profile or to use an existing profile.</summary>
        /// <value>The profile.</value>
        [JsonProperty("profile", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Profile { get; set; }

        /// <summary>Optional, specify Y to capture the transaction for settlement if approved.</summary>
        /// <value>The capture.</value>
        [JsonProperty("capture", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Capture { get; set; }

        /// <summary>Optional, if tokenize is Y the masked card or ACH account number is returned in the response.</summary>
        /// <value>The tokenize.</value>
        [JsonProperty("tokenize", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Tokenize { get; set; }

        /// <summary>JSON escaped, Base64 encoded, Gzipped, BMP of signature data. If the authorization is using a token
        /// with associated signature data, then the signature from the token is used.</summary>
        /// <value>The signature.</value>
        [JsonProperty("signature", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Signature { get; set; }

        /// <summary>CVV2/CVC/CID value.</summary>
        /// <value>The cvv 2.</value>
        [JsonProperty("cvv2", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Cvv2 { get; set; }

        /// <summary>Account name, optional for credit cards and electronic checks (ACH)</summary>
        /// <value>The name of the account holder.</value>
        [JsonProperty("name", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountHolderName { get; set; }

        /// <summary>Account street address (required for AVS)</summary>
        /// <value>The address.</value>
        [JsonProperty("address", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Address { get; set; }

        /// <summary>Second address line (for example, apartment or suite number) if applicable.</summary>
        /// <value>The address 2.</value>
        [JsonProperty("address2", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Address2 { get; set; }

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

        /// <summary>Email address of the account holder.</summary>
        /// <value>The email.</value>
        [JsonProperty("email", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Email { get; set; }

        /// <summary>Source system order number.</summary>
        /// <value>The identifier of the order.</value>
        [JsonProperty("orderid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? OrderId { get; set; }

        /// <summary>Authorization code from original authorization (VoiceAuth).</summary>
        /// <value>The authentication code.</value>
        [JsonProperty("authcode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AuthCode { get; set; }

        /// <summary>If taxexempt is set to 'N' for the transaction and a tax is not passed, the default configuration
        /// data is used. If taxexempt is set to 'Y', the taxamnt is $0.00.</summary>
        /// <value>The tax exempt.</value>
        [JsonProperty("taxexempt", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? TaxExempt { get; set; }

        /// <summary>Terminal Device ID.</summary>
        /// <value>The identifier of the terminal.</value>
        [JsonProperty("termid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? TerminalId { get; set; }

        /// <summary>Not required unless PPAL, PAID, GIFT, or PDEBIT.</summary>
        /// <value>The type of the account.</value>
        [JsonProperty("accttype", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AccountType { get; set; }

        /// <summary>Identifies the payment origin.</summary>
        /// <value>The payment origin.</value>
        [JsonProperty("ecomind", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public PaymentOrigin PaymentOrigin { get; set; }
    }
}
