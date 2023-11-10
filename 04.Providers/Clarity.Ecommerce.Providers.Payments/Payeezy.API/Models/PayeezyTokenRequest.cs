// <copyright file="PayeezyTokenRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Payeezy token request class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Runtime.Serialization;
    using JetBrains.Annotations;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>(Serializable)a payeezy token request.</summary>
    [PublicAPI]
    [Serializable]
    public class PayeezyTokenRequest
    {
        /// <summary>Initializes a new instance of the <see cref="PayeezyTokenRequest"/> class.</summary>
        /// <param name="cardNumber">    The card number.</param>
        /// <param name="cvv">           The cvv.</param>
        /// <param name="expDate">       The exponent date.</param>
        /// <param name="cardType">      Type of the card.</param>
        /// <param name="cardHolderName">Name of the card holder.</param>
        /// <param name="token">       The ta token.</param>
        public PayeezyTokenRequest(
            string cardNumber,
            string cvv,
            string expDate,
            string cardType,
            string cardHolderName,
            string token = "NOIW")
        {
            Token = token;
            CreditCard = new()
            {
                type = cardType,
                cardholder_name = cardHolderName,
                card_number = cardNumber,
                exp_date = expDate,
                cvv = cvv,
            };
        }

        /// <summary>Type of the token being created. Values = FDToken.</summary>
        /// <remarks>Note: Use this method to create FDTokens. Payeezy.JS implements the same.Note: US Merchants will
        /// be getting Transarmor multi-use tokens when this method is used.</remarks>
        /// <value>The type.</value>
        [JsonRequired]
        [DataMember(Name = "type"), JsonProperty("type"), ApiMember(Name = "type")]
        public string Type { get; } = "FDToken";

        /// <summary>Gets the authentication.</summary>
        /// <remarks>Possible values=true/false. False – gets you a FDToken without 0$ auth. The token generated is valid
        /// for "authorize", "purchase" and reversals ("capture", "void" or "refund/settled") transactions. ta_token
        /// parameter is mandatory for auth=false. Note: CVV check will not happen here and card information will not be
        /// validated. True-gets you a FDToken with 0$ auth. The token generated is valid for "authorize" payment
        /// transaction only.</remarks>
        /// <value>The authentication.</value>
        [JsonRequired]
        [DataMember(Name = "auth"), JsonProperty("auth"), ApiMember(Name = "auth")]
        public string Auth { get; } = "false";

        /// <summary>Gets the ta token.</summary>
        /// <remarks>Transarmor Token type.</remarks>
        /// <value>The ta token.</value>
        [JsonRequired]
        [DataMember(Name = "ta_token"), JsonProperty("ta_token"), ApiMember(Name = "ta_token")]
        public string Token { get; }

        /// <summary>Gets the credit card.</summary>
        /// <remarks>JSON object that holds the credit/debit card that is being tokenized.</remarks>
        /// <value>The credit card.</value>
        [JsonRequired]
        [DataMember(Name = "credit_card"), JsonProperty("credit_card"), ApiMember(Name = "credit_card")]
        public PayeezyCreditCard CreditCard { get; }
    }
}
