// <copyright file="IPaymentWalletResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPaymentWalletResponse interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    /// <summary>Interface for payment wallet response.</summary>
    public interface IPaymentWalletResponse
    {
        /// <summary>Gets a value indicating whether the approved.</summary>
        /// <value>true if approved, false if not.</value>
        bool Approved { get; }

        /// <summary>Gets the token.</summary>
        /// <value>The token.</value>
        string? Token { get; }

        /// <summary>Gets or sets the response code.</summary>
        /// <value>The response code.</value>
        string? ResponseCode { get; set; }

        /// <summary>Gets or sets the type of the card.</summary>
        /// <value>The type of the card.</value>
        string? CardType { get; set; }

        /// <summary>Gets or sets the account.</summary>
        /// <value>The account.</value>
        string? Account { get; set; }

        /// <summary>Gets or sets the exponent date.</summary>
        /// <value>The exponent date.</value>
        string? ExpDate { get; set; }

        /// <summary>Gets or sets the name of the card.</summary>
        /// <value>The name of the card.</value>
        string? CardName { get; set; }

        /// <summary>Gets or sets the customer.</summary>
        /// <value>The customer.</value>
        string? Customer { get; set; }
    }
}
