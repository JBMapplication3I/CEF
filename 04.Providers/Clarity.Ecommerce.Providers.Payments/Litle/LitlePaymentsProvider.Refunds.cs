// <copyright file="LitlePaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the litle payments provider class</summary>
#if !NET5_0_OR_GREATER // Litle doesn't have .net 5.0+ builds (alternative available)
namespace Clarity.Ecommerce.Providers.Payments.LitleShip
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A litle provider.</summary>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class LitlePaymentsProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0m);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            return Task.FromResult(Litle.RefundReversal(
                new() { litleTxnId = transactionID }).ToPaymentResponse());
        }
    }
}
#endif
