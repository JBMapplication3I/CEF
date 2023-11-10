// <copyright file="ITransactionResponse.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ITransactionResponse interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    /// <summary>Interface for a transaction model.</summary>
    public interface ITransactionResponse
    {
        /// <summary>Gets or sets a value indicating whether the success.</summary>
        /// <value>True if success, false if not.</value>
        bool Success { get; set; }

        /// <summary>Gets or sets the response code.</summary>
        /// <value>The response code.</value>
        int ResponseCode { get; set; }

        /// <summary>Gets or sets the status message.</summary>
        /// <value>A message describing the status.</value>
        string? StatusMessage { get; set; }

        /// <summary>Gets or sets the Transactions.</summary>
        /// <value>The Transactions.</value>
        ITransaction[]? Transactions { get; set; }
    }
}
