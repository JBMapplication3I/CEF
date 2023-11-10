// <copyright file="EvoPaymentProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo payment provider. wallet class</summary>
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    public partial class EvoPaymentProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            EvoPaymentProviderParameters creditCardOrACH;
            if (Extensions.IsACH(payment))
            {
                creditCardOrACH = Extensions.InfoToWalletParameters(
                    payment: payment,
                    billing: billing,
                    expDate: null);
            }
            else
            {
                Contract.RequiresValidID(payment.ExpirationMonth);
                Contract.RequiresValidID(payment.ExpirationYear);
                creditCardOrACH = Extensions.InfoToWalletParameters(
                    payment: payment,
                    billing: billing,
                    expDate: CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            }
            creditCardOrACH.TransactionType = "L";
            PayFabricWalletRequest model = new()
            {
                Account = creditCardOrACH.Account,
                CardHolder = new()
                {
                    FirstName = billing.FirstName,
                    LastName = billing.LastName,
                },
                Customer = payment.AccountNumber ?? billing.CustomKey,
                ExpDate = creditCardOrACH.ExpirationDate,
                Tender = creditCardOrACH.AccountType is "C"
                    ? "ECheck"
                    : "Credit",
            };
            return Extensions.CreateWalletAsync(
                model: model,
                createCreditCard: model.Tender == "Credit",
                authKey: $"{creditCardOrACH.DeviceID}|{creditCardOrACH.DevicePassword}",
                contextProfileName: contextProfileName,
                logger: Logger);
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
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
            return Extensions.RemoveWalletAsync(
                token: payment.Token!,
                authKey: authCode,
                contextProfileName: contextProfileName,
                logger: Logger);
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            // Nothing to update - we only update card nickname in cef
            return Task.FromResult(
                Extensions.ToPaymentWalletResponse(
                    approved: false,
                    customerProfileId: string.Empty,
                    responseCode: string.Empty));
        }

        /// <inheritdoc/>
        public async Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            var creditCardOrACH = new EvoPaymentProviderParameters();
            var authCode = $"{creditCardOrACH.DeviceID}|{creditCardOrACH.DevicePassword}";
            var wallets = await Extensions.GetAccountWalletsAsync(
                    authKey: authCode,
                    account: walletAccountNumber,
                    getCreditCard: true,
                    contextProfileName: contextProfileName,
                    logger: Logger)
                .ConfigureAwait(false);
            var eChecks = await Extensions.GetAccountWalletsAsync(
                    authKey: authCode,
                    account: walletAccountNumber,
                    getCreditCard: false,
                    contextProfileName: contextProfileName,
                    logger: Logger)
                .ConfigureAwait(false);
            wallets.AddRange(eChecks);
            return wallets;
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }
    }
}
