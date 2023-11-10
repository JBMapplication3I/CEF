// <copyright file="EvoPaymentProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo payment provider. payments class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <content>An EVO payment provider.</content>
    public partial class EvoPaymentProvider
    {
        /// <summary>Gets the extensions.</summary>
        /// <value>The extensions.</value>
        private EvoPaymentProviderExtensions Extensions { get; } = new();

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            Contract.Requires<ArgumentException>(payment.Amount > 0);
            if (ProviderMode != Enums.PaymentProviderMode.Production && payment.Amount >= 1000)
            {
                payment.Amount = 999m;
            }
            EvoPaymentProviderParameters creditCardOrACH;
            if (Extensions.IsACH(payment))
            {
                creditCardOrACH = Extensions.InfoToCardParameters(
                    payment: payment,
                    billing: billing,
                    shipping: shipping,
                    expDate: null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                var exp = payment.ExpirationMonth > 0 && payment.ExpirationYear > 0
                    ? CleanExpirationDate(payment.ExpirationMonth.Value, payment.ExpirationYear.Value)
                    : null;
                creditCardOrACH = Extensions.InfoToCardParameters(
                    payment: payment,
                    billing: billing,
                    shipping: shipping,
                    expDate: exp);
            }
            var authCode = $"{creditCardOrACH.DeviceID}|{creditCardOrACH.DevicePassword}";
            creditCardOrACH.TransactionType = "Sale";
            creditCardOrACH.OrderID = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}";
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
                        FirstName = billing?.FirstName ?? string.Empty,
                        LastName = billing?.LastName ?? string.Empty,
                    },
                    Customer = payment.AccountNumber,
                    ExpDate = creditCardOrACH.ExpirationDate,
                },
                Currency = "USD",
                Customer = payment.AccountNumber,
                SetupId = setupID,
                Type = "Sale",
            };
            return await Extensions.CreateAndProcessPaymentTransactionAsync(
                    model: model,
                    authKey: authCode,
                    contextProfileName: contextProfileName,
                    logger: Logger)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            if (Extensions.IsACH(payment))
            {
                throw new InvalidOperationException(
                    "The eCheck/ACH process cannot perform authorize with later capture actions");
            }
            Contract.Requires<ArgumentException>(payment.Amount > 0);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var creditCard = Extensions.InfoToCardParameters(
                payment: payment,
                billing: billing,
                shipping: shipping,
                expDate: payment.ExpirationMonth > 0 && payment.ExpirationYear > 0
                    ? CleanExpirationDate(payment.ExpirationMonth.Value, payment.ExpirationYear.Value)
                    : null);
            creditCard.TransactionType = "A";
            creditCard.OrderID = $"Web Order {DateExtensions.GenDateTime.ToFileTimeUtc()}";
            return Extensions.GetAuthorizationTokenAsync(
                authKey: $"{creditCard.DeviceID}|{creditCard.DevicePassword}",
                contextProfileName: contextProfileName,
                logger: Logger);
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            if (Extensions.IsACH(payment))
            {
                throw new InvalidOperationException(
                    "The eCheck/ACH process cannot perform authorize with later capture actions");
            }
            Contract.Requires<ArgumentException>(payment.Amount > 0m);
            Contract.RequiresValidID(payment.ExpirationMonth);
            Contract.RequiresValidID(payment.ExpirationYear);
            var creditCardOrACH = Extensions.InfoToCardParameters(
                payment: payment,
                billing: null,
                shipping: null,
                expDate: CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            creditCardOrACH.TransactionType = "D";
            creditCardOrACH.OriginalID = paymentAuthorizationToken;
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
                Currency = "USD",
                Customer = payment.AccountNumber,
                SetupId = setupID,
                Type = "Sale",
            };
            var result = await Extensions.CreatePaymentTransactionAsync(
                    model: model,
                    authKey: authCode,
                    contextProfileName: contextProfileName,
                    logger: Logger)
                .ConfigureAwait(false);
            result.Amount = payment.Amount!.Value;
            return result;
        }
    }
}
