// <copyright file="IRefundsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRefundsProviderBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System.Threading.Tasks;

    /// <summary>Interface for refunds provider base.</summary>
    /// <seealso cref="IProviderBase"/>
    public interface IRefundsProviderBase : IProviderBase
    {
        /// <summary>Refunds the transaction by the specified amount (or full amount if null).</summary>
        /// <param name="payment">           The payment.</param>
        /// <param name="transactionID">     Identifier for the transaction.</param>
        /// <param name="amount">            The amount.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentResponse.</returns>
        Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName);
    }
}
