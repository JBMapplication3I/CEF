// <copyright file="IProcessPaymentResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProcessPaymentResponse interface</summary>
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using Interfaces.Providers.Payments;

    /// <summary> Interface for a Process Payment Response</summary>
    public interface IProcessPaymentResponse
    {
        /// <summary>Gets or sets the token.</summary>
        /// <value>The token.</value>
        string? Token { get; set; }

        /// <summary>Gets or sets the Transaction ID.</summary>
        /// <value>The Transaction ID.</value>
        string? TransactionID { get; set; }

        /// <summary>Gets or sets a value indicating whether an invoice needs to be created or not.</summary>
        /// <value>True if an invoice needs to be created, false if not.</value>
        bool MakeInvoice { get; set; }

        /// <summary>Gets or sets the Balance Due.</summary>
        /// <value>The Balance Due.</value>
        decimal BalanceDue { get; set; }

        /// <summary>Gets or sets the Payment Response.</summary>
        /// <value>The Payment Response.</value>
        IPaymentResponse PaymentResponse { get; set; }
    }
}
