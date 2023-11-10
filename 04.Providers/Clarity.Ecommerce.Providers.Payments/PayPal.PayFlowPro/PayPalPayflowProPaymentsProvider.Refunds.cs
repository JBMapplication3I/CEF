// <copyright file="PayPalPayflowProPaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal Payflow Pro payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>A PayPal Payflow Pro payments provider.</summary>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class PayPalPayflowProPaymentsProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0);
            PayflowCreditCardOrACHParameters refund;
            if (Extensions.Value.IsACH(payment))
            {
                refund = Extensions.Value.InfoToRefundParameters(
                    payment,
                    amount,
                    null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                refund = Extensions.Value.InfoToRefundParameters(
                    payment,
                    amount,
                    CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            }
            refund.TransactionType = "C";
            refund.OriginalID = transactionID;
            var requestBody = Extensions.Value.CreditCardOrACHToRefundRequestBody(refund);
            var result = Extensions.Value.PaymentBodyToRequestAndGetResult(
                requestBody,
                contextProfileName,
                Logger);
            result.Amount = amount ?? payment.Amount ?? 0m;
            return Task.FromResult(result);
        }
    }
}
