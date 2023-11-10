// <copyright file="PayeezyAPIPaymentsProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy API payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;
    using Utilities;

    /// <content>A Payeezy API payments provider.</content>
    /// <seealso cref="IRefundsProviderBase"/>
    public partial class PayeezyAPIPaymentsProvider : IRefundsProviderBase
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
            Contract.RequiresValidKey(transactionID);
            var transValues = transactionID!.Split('|');
            // payeezy does not accept amounts with a decimal in them
            var amt = Convert.ToInt32((amount ?? payment.Amount ?? 0m) * 100).ToString();
            var request = CreateRefundRequestJson(transValues, amt);
            authorization = CreateHMAC(apiSecret, token, request, nonce, timestamp);
            var result = SendPostRequest(request, "/" + transValues.FirstOrDefault());
            if (result is null)
            {
                return new PaymentResponse();
            }
            var response = JsonConvert.DeserializeObject<PayeezyRefundRequestResponse>(result);
            if (response is null)
            {
                return new PaymentResponse();
            }
            if (response.transaction_status != "approved")
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(RefundAsync)}.Error",
                        message: $"{{request:{request},response:{result}}}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentResponse
                {
                    Approved = false,
                    AuthorizationCode = response.transaction_tag,
                    ResponseCode = response.transaction_status,
                    TransactionID = response.transaction_id
                        + response.Error!.messages!
                            .Select(x => $"{x.code}: {x.description}")
                            .DefaultIfEmpty(string.Empty)
                            .Aggregate((c, n) => c + "\r\n" + n),
                };
            }
            await Logger.LogInformationAsync(
                    name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(RefundAsync)}.Success",
                    message: $"{{request:{request},response:{result}}}",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return new PaymentResponse
            {
                Approved = response.transaction_status == "approved",
                AuthorizationCode = response.transaction_tag,
                ResponseCode = response.bank_resp_code,
                TransactionID = response.transaction_id,
                Amount = amount ?? payment.Amount ?? 0m,
            };
        }

        /// <summary>Creates refund request JSON.</summary>
        /// <param name="transValues">The transaction values.</param>
        /// <param name="strAmount">  The amount.</param>
        /// <returns>The new refund request JSON.</returns>
        private static string CreateRefundRequestJson(IReadOnlyList<string> transValues, string strAmount)
        {
            return JsonConvert.SerializeObject(
                new PayeezyRefundRequest()
                {
                    merchant_ref = transValues[6],
                    transaction_type = "refund",
                    method = "token",
                    amount = strAmount,
                    currency_code = "USD",
                    token = new()
                    {
                        token_type = "FDToken",
                        token_data = new()
                        {
                            type = transValues[2],
                            cardholder_name = transValues[3],
                            value = transValues[4],
                            exp_date = transValues[5],
                        },
                    },
                });
        }
    }
}
