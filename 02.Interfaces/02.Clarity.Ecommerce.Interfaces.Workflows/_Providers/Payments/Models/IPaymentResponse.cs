// <copyright file="IPaymentResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPaymentResponse interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    /// <summary>Interface for payment response.</summary>
    public interface IPaymentResponse
    {
        /// <summary>Gets or sets the amount.</summary>
        /// <value>The amount.</value>
        decimal Amount { get; set; }

        /// <summary>Gets or sets a value indicating whether the approved.</summary>
        /// <value>true if approved, false if not.</value>
        bool Approved { get; set; }

        /// <summary>Gets or sets the response code.</summary>
        /// <value>The response code.</value>
        string? ResponseCode { get; set; }

        /// <summary>Gets or sets the authorization code.</summary>
        /// <value>The authorization code.</value>
        string? AuthorizationCode { get; set; }

        /// <summary>Gets or sets the identifier of the transaction.</summary>
        /// <value>The identifier of the transaction.</value>
        string? TransactionID { get; set; }

        /// <summary>Gets or sets the reference code.</summary>
        /// <value>The reference code for the transaction.</value>
        string? ReferenceCode { get; set; }
    }
}
