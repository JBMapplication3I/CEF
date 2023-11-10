// <copyright file="PayFabricWalletRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric wallet request class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using JetBrains.Annotations;

    /// <summary>A pay fabric wallet request.</summary>
    [PublicAPI]
    public class PayFabricWalletRequest
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public string? ID { get; set; }

        /// <summary>Gets or sets the name of the card.</summary>
        /// <value>The name of the card.</value>
        public string? CardName { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        public string? Account { get; set; }

        /// <summary>Gets or sets the bill to.</summary>
        /// <value>The bill to.</value>
        public PayFabricAddress? Billto { get; set; }

        /// <summary>Gets or sets the card holder.</summary>
        /// <value>The card holder.</value>
        public PayFabricCardHolder? CardHolder { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        public string? Customer { get; set; }

        /// <summary>Gets or sets the exponent date.</summary>
        /// <value>The exponent date.</value>
        public string? ExpDate { get; set; }

        /// <summary>Gets or sets the gp address code.</summary>
        /// <value>The gp address code.</value>
        public string? GPAddressCode { get; set; }

        /// <summary>Gets or sets the gateway token.</summary>
        /// <value>The gateway token.</value>
        public string? GatewayToken { get; set; }

        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public string? Identifier { get; set; }

        /// <summary>Gets or sets a value indicating whether this PayFabricWalletRequest is default card.</summary>
        /// <value>True if this PayFabricWalletRequest is default card, false if not.</value>
        public bool IsDefaultCard { get; set; }

        /// <summary>Gets or sets the issue number.</summary>
        /// <value>The issue number.</value>
        public string? IssueNumber { get; set; }

        /// <summary>Gets or sets the tender.</summary>
        /// <value>The tender.</value>
        public string? Tender { get; set; }

        /// <summary>Gets or sets the user define 1.</summary>
        /// <value>The user define 1.</value>
        public string? UserDefine1 { get; set; }

        /// <summary>Gets or sets the user define 2.</summary>
        /// <value>The user define 2.</value>
        public string? UserDefine2 { get; set; }

        /// <summary>Gets or sets the user define 3.</summary>
        /// <value>The user define 3.</value>
        public string? UserDefine3 { get; set; }

        /// <summary>Gets or sets the user define 4.</summary>
        /// <value>The user define 4.</value>
        public string? UserDefine4 { get; set; }
    }
}
