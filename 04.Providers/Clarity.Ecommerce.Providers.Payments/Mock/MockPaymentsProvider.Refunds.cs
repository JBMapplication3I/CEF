// <copyright file="MockPaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the MockPaymentProvider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Mock
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A mock payment provider.</summary>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class MockPaymentsProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0);
            if (IsACH(payment))
            {
                return Task.FromResult<IPaymentResponse>(
                    new PaymentResponse
                    {
                        Amount = payment.Amount * -1 ?? 0,
                        Approved = true,
                        AuthorizationCode = string.Empty,
                        ResponseCode = "200",
                        TransactionID = GenCode(payment),
                    });
            }
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            return Task.FromResult<IPaymentResponse>(
                new PaymentResponse
                {
                    Amount = payment.Amount * -1 ?? 0,
                    Approved = true,
                    AuthorizationCode = string.Empty,
                    ResponseCode = "200",
                    TransactionID = GenCode(payment),
                });
        }
    }
}
