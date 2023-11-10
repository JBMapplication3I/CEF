// <copyright file="CyberSourcePaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CyberSource Payments Provider class</summary>
#if !NET5_0_OR_GREATER // Cybersource doesn't have .net 5.0+ builds
namespace Clarity.Ecommerce.Providers.Payments.CyberSource
{
    using System;
    using System.Threading.Tasks;
    using global::CyberSource.Clients;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <content>A Cyber source gateway.</content>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class CyberSourcePaymentsProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            try
            {
                return CyberSourcePaymentsProviderExtensions.ToPaymentResponse(
                    SoapClient.RunTransaction(new()
                    {
                        ccAuthReversalService = new()
                        {
                            run = "true",
                            authRequestID = transactionID,
                        },
                        purchaseTotals = new()
                        {
                            currency = "USD",
                            grandTotalAmount = (amount ?? payment.Amount ?? 0).ToString("0.00"),
                        },
                    }));
            }
            catch (Exception ex)
            {
                return await LogErrorAndReturnFailedPaymentResponseAsync(
                        nameof(RefundAsync),
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        private static async Task<IPaymentResponse> LogErrorAndReturnFailedPaymentResponseAsync(
            string functionName,
            Exception ex,
            string? contextProfileName)
        {
            var guid = await Logger.LogErrorAsync(
                    name: $"{nameof(CyberSourcePaymentsProvider)}.{functionName}",
                    message: ex.Message,
                    ex: ex,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return ex.ConvertRawExceptionToErrorCode(guid) switch
            {
                Exception exe => CyberSourcePaymentsProviderExtensions.ToPaymentResponse(
                    false,
                    "ERROR: " + exe.Message),
                ErrorCodeWithMessage isCode => CyberSourcePaymentsProviderExtensions.ToPaymentResponse(
                    false,
                    isCode.Message + " Code: " + isCode.Code + " Log ID: " + isCode.LogID),
                _ => CyberSourcePaymentsProviderExtensions.ToPaymentResponse(
                    false,
                    "An unknown error has occurred"),
            };
        }
    }
}
#endif
