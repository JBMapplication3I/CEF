// <copyright file="PayeezyAPIPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payeezy API payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayeezyAPI
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;

    /// <content>A Payeezy API payments provider.</content>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class PayeezyAPIPaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment, IContactModel billing, string? contextProfileName)
        {
            var request = CreateTokenRequestJson(payment);
            authorization = CreateHMAC(apiSecret, token, request, nonce, timestamp);
            var result = SendPostRequest(request, "/tokens");
            if (result is null)
            {
                return new PaymentWalletResponse();
            }
            var response = JsonConvert.DeserializeObject<PayeezyTokenRequestResponse>(result);
            if (response is null)
            {
                return new PaymentWalletResponse();
            }
            if (response.Status is null or "failed")
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.Error",
                        message: $"{{request:{request},response:{result}}}",
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return new PaymentWalletResponse
                {
                    Approved = false,
                    ResponseCode = response.Status,
                    Token = response.Error!.messages!
                        .Select(x => $"{x.code}: {x.description}")
                        .DefaultIfEmpty(string.Empty)
                        .Aggregate((c, n) => c + "\r\n" + n),
                    CardType = payment.CardType,
                };
            }
            await Logger.LogInformationAsync(
                    name: $"{nameof(PayeezyAPIPaymentsProvider)}.{nameof(CreateCustomerProfileAsync)}.Success",
                    message: $"{{request:{request},response:{result}}}",
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return new PaymentWalletResponse
            {
                Approved = true,
                ResponseCode = response.Status,
                Token = response.Token!.Value,
                CardType = payment.CardType,
            };
        }

        // Payeezy does not support the RUD in CRUD when it comes to creating a wallet as of 5/16/2018

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment, IContactModel billing, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Creates token request JSON.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>The new token request JSON.</returns>
        private static string CreateTokenRequestJson(IProviderPayment payment)
        {
            var request = new PayeezyTokenRequest(
                cardType: payment.CardType!,
                cardHolderName: payment.CardHolderName!,
                cardNumber: payment.CardNumber!,
                cvv: payment.CVV!,
                expDate: CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value));
            return JsonConvert.SerializeObject(request);
        }
    }
}
