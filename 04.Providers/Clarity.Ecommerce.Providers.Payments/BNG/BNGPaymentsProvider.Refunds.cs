// <copyright file="BNGPaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the BNG payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Transactions;
    using Utilities;

    /// <summary>A BNG payments provider.</summary>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class BNGPaymentsProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0);
            Contract.RequiresAllValid(payment.ExpirationMonth, payment.ExpirationYear, transactionID);
            return Task.FromResult(
                service.Request(new BNGRefundTransaction(
                    (amount ?? payment.Amount ?? 0m).ToString(CultureInfo.InvariantCulture),
                    transactionID!)));
        }
    }
}
