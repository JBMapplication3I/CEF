// <copyright file="CardConnectAuthorizationResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the card connect authorization response class</summary>
// ReSharper disable CommentTypo, StringLiteralTypo
namespace Clarity.Ecommerce.Providers.Payments.CardConnect.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;

    /// <summary>The authorization response.</summary>
    public class CardConnectAuthorizationResponse
    {
        /// <summary>Indicates the status of the authorization request.</summary>
        /// <value>The response status.</value>
        [JsonProperty("respstat", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ApprovalStatus ResponseStatus { get; set; }

        /// <summary>CardConnect retrieval reference number from authorization response.</summary>
        /// <value>The retrieval reference.</value>
        [JsonProperty("retref", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? RetrievalReference { get; set; }

        /// <summary>Either the masked payment card or eCheck (ACH) account if "tokenize":"y" in the request OR the token
        /// generated for the account if "tokenize":"n" or was omitted in the request.</summary>
        /// <value>The account.</value>
        [JsonProperty("account", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Account { get; set; }

        /// <summary>The payment card expiration date, in MMYY format, if expiry was included in the request.</summary>
        /// <value>The expiry mmyy.</value>
        [JsonProperty("expiry", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ExpiryMMYY { get; set; }

        /// <summary>A token that replaces the card number in capture and settlement requests if requested.</summary>
        /// <value>The token.</value>
        [JsonProperty("token", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Token { get; set; }

        /// <summary>Authorized amount. Same as the request amount for most approvals.</summary>
        /// <value>The amount in cents.</value>
        [JsonProperty("amount", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AmountInCents { get; set; }

        /// <summary>Copied from the authorization request.</summary>
        /// <value>The identifier of the merchant.</value>
        [JsonProperty("merchid", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? MerchantId { get; set; }

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

        /// <summary>Alpha-numeric AVS (zip code) verification response code.</summary>
        /// <value>The avs response code.</value>
        [JsonProperty("avsresp", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AvsResponseCode { get; set; }

        /// <summary>Alpha-numeric CVV (card verification value) verification response code.</summary>
        /// <value>The cvv response code.</value>
        [JsonProperty("cvvresp", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public CvvResponseCode CvvResponseCode { get; set; }

        /// <summary>An array that includes the fields returned in the BIN response.</summary>
        /// <value>The bin service response.</value>
        [JsonProperty("bininfo", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public ICollection<string>? BinServiceResponse { get; set; }

        /// <summary>Type of BIN.</summary>
        /// <value>The type of the bin.</value>
        [JsonProperty("bintype", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? BinType { get; set; }

        /// <summary>Only returned for merchants using the First Data North and Chase Paymentech Tampa processors.</summary>
        /// <value>The entry mode.</value>
        [JsonProperty("entrymode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? EntryMode { get; set; }

        /// <summary>Authorization Code from the Issuer.</summary>
        /// <value>The authorization code.</value>
        [JsonProperty("authcode", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? AuthorizationCode { get; set; }

        /// <summary>JSON escaped, Base64 encoded, Gzipped, BMP of signature data (via Ingenico ISC250). Returned if the
        /// authorization used a token that had associated signature data or track data with embedded signature data.</summary>
        /// <value>The signature.</value>
        [JsonProperty("signature", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Signature { get; set; }

        /// <summary>Y if a Corporate or Purchase Card.</summary>
        /// <value>The commercial card flag.</value>
        [JsonProperty("commcard", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? CommercialCardFlag { get; set; }

        /// <summary>Authorization Response Cryptogram (ARPC). This is returned only when EMV data is present within the
        /// Track Parameter.</summary>
        /// <value>The emv.</value>
        [JsonProperty("emv", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? Emv { get; set; }

        /// <summary>A string of receipt and EMV tag data (when applicable) returned from the processor.</summary>
        /// <value>Information describing the emv tag.</value>
        [JsonProperty("emvtagdata", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? EmvTagData { get; set; }

        /// <summary>An object that includes additional fields to be printed on a receipt.</summary>
        /// <value>Information describing the receipt.</value>
        [JsonProperty("receipt", Required = Required.DisallowNull, NullValueHandling = NullValueHandling.Ignore)]
        public string? ReceiptData { get; set; }
    }
}
