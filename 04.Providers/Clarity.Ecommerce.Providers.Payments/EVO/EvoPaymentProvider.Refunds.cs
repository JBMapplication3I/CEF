// <copyright file="EvoPaymentProvider.Refunds.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo payment provider. refunds class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <content>An EVO payment provider.</content>
    public partial class EvoPaymentProvider : IRefundsProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentResponse> RefundAsync(
            IProviderPayment payment,
            string? transactionID,
            decimal? amount,
            string? contextProfileName)
        {
            Contract.Requires<ArgumentNullException>((amount ?? payment.Amount) > 0);
            EvoPaymentProviderParameters refund;
            if (Extensions.IsACH(payment))
            {
                refund = Extensions.InfoToRefundParameters(
                    payment: payment,
                    amount: amount,
                    expDate: null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                refund = Extensions.InfoToRefundParameters(
                    payment: payment,
                    amount: amount,
                    expDate: CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            }
            refund.TransactionType = "C";
            refund.OriginalID = transactionID;
            EvoPaymentProviderParameters creditCardOrACH;
            if (Extensions.IsACH(payment))
            {
                creditCardOrACH = Extensions.InfoToWalletParameters(
                    payment: payment,
                    billing: null,
                    expDate: null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                creditCardOrACH = Extensions.InfoToWalletParameters(
                    payment: payment,
                    billing: null,
                    expDate: CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            }
            var authCode = $"{creditCardOrACH.DeviceID}|{creditCardOrACH.DevicePassword}";
            var setupID = await Extensions.GetAccountProfileGatewayAsync(
                    authKey: authCode,
                    profileType: creditCardOrACH.AccountType == "C" ? "ECheck" : "Credit",
                    contextProfileName: contextProfileName,
                    logger: Logger)
                .ConfigureAwait(false);
            PayFabricTransactionRequest model = new()
            {
                Amount = payment.Amount.ToString(),
                Card = new()
                {
                    Account = creditCardOrACH.Account,
                    CardHolder = new()
                    {
                        FirstName = payment.CardHolderName!.Split(' ')[0],
                        LastName = payment.CardHolderName.Split(' ')[1],
                    },
                    Customer = payment.AccountNumber,
                    ExpDate = creditCardOrACH.ExpirationDate,
                },
                Currency = "USD",
                Customer = payment.AccountNumber,
                SetupId = setupID,
            };
            var result = await Extensions.RefundCustomerAsync(
                    authCode,
                    model,
                    contextProfileName,
                    Logger)
                .ConfigureAwait(false);
            if (result is null)
            {
                return new PaymentResponse();
            }
            result.Amount = amount ?? payment.Amount ?? 0m;
            return result;
        }
    }
}
