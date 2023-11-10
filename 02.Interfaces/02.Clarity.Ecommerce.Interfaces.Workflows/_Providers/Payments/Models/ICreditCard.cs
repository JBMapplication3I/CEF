// <copyright file="ICreditCard.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICreditCard interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    /// <summary>Interface for credit card model.</summary>
    public interface ICreditCard
    {
        /// <summary>Gets or sets the MaskedNumber.</summary>
        /// <value>The MaskedNumber.</value>
        string? MaskedNumber { get; set; }

        /// <summary>Gets or sets the expiration month.</summary>
        /// <value>The expiration month.</value>
        string? ExpirationMonth { get; set; }

        /// <summary>Gets or sets the expiration year.</summary>
        /// <value>The expiration year.</value>
        string? ExpirationYear { get; set; }
    }
}
