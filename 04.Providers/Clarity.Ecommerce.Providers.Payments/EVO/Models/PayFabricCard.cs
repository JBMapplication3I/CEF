// <copyright file="PayFabricCard.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pay fabric card class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    /// <summary>A pay fabric card.</summary>
    public class PayFabricCard
    {
        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        public string? Account { get; set; }

        /// <summary>Gets or sets the card holder.</summary>
        /// <value>The card holder.</value>
        public PayFabricCardHolder? CardHolder { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        public string? Customer { get; set; }

        /// <summary>Gets or sets the exponent date.</summary>
        /// <value>The exponent date.</value>
        public string? ExpDate { get; set; }
    }
}
