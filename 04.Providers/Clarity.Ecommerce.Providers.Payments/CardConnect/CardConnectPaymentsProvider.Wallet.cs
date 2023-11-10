// <copyright file="CardConnectPaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the CardConnect payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.CardConnect
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Newtonsoft.Json;

    /// <summary>A CardConnect Wallet provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class CardConnectPaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            try
            {
                CardConnectProfileRequest request = new()
                {
                    Profile = FixedLength(payment.CardNumber![Math.Max(0, payment.CardNumber!.Length - 4)..] + payment.CardHolderName, 20),
                    MerchantId = CardConnectPaymentsProviderConfig.MerchantId,
                    Account = payment.CardNumber,
                    ExpiryMMYY = payment.ExpirationMonth?.ToString("00")
                        + (payment.ExpirationYear.ToString()!.Length > 2
                            ? payment.ExpirationYear.ToString()!
                                .Substring(payment.ExpirationYear.ToString()!.Length - 2, 2)
                            : payment.ExpirationYear.ToString()),
                    AccountHolderName = payment.CardHolderName,
                    Address = billing.Address!.Street1,
                    City = billing.Address.City,
                    Region = billing.Address.RegionCode,
                    Country = billing.Address.CountryCode == "USA" ? "US" : billing.Address.CountryCode,
                    PostalCode = string.IsNullOrEmpty(payment.Zip)
                        ? billing.Address.PostalCode
                        : payment.Zip,
                    Phone = billing.Phone1,
                    Email = billing.Email1,
                };
                var result = await SendRequestAsync("POST", JsonConvert.SerializeObject(request), "profile").ConfigureAwait(false);
                var response = result is null ? null : JsonConvert.DeserializeObject<CardConnectProfileResponse>(result);
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
            catch (Exception ex)
            {
                var response = new CardConnectProfileResponse
                {
                    ResponseStatus = ApprovalStatus.Declined,
                    ResponseText = ex.Message,
                };
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            try
            {
                var request = new CardConnectProfileRequest
                {
                    Profile = FixedLength(payment.CardNumber![Math.Max(0, payment.CardNumber!.Length - 4)..] + payment.CardHolderName, 20),
                    MerchantId = CardConnectPaymentsProviderConfig.MerchantId,
                    Account = payment.CardNumber,
                    ExpiryMMYY = payment.ExpirationMonth?.ToString("00")
                        + (payment.ExpirationYear.ToString()!.Length > 2
                            ? payment.ExpirationYear.ToString()!
                                .Substring(payment.ExpirationYear.ToString()!.Length - 2, 2)
                            : payment.ExpirationYear.ToString()),
                    AccountHolderName = payment.CardHolderName,
                    Address = billing.Address!.Street1,
                    City = billing.Address.City,
                    Region = billing.Address.RegionCode,
                    Country = billing.Address.CountryCode == "USA" ? "US" : billing.Address.CountryCode,
                    PostalCode = string.IsNullOrEmpty(payment.Zip)
                        ? billing.Address.PostalCode
                        : payment.Zip,
                    Phone = billing.Phone1,
                    Email = billing.Email1,
                };
                var result = await SendRequestAsync("PUT", JsonConvert.SerializeObject(request), "profile").ConfigureAwait(false);
                var response = result is null ? null : JsonConvert.DeserializeObject<CardConnectProfileResponse>(result);
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
            catch (Exception ex)
            {
                var response = new CardConnectProfileResponse
                {
                    ResponseStatus = ApprovalStatus.Declined,
                    ResponseText = ex.Message,
                };
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            try
            {
                var request = new CardConnectProfileRequest
                {
                    Profile = FixedLength(payment.CardNumber![Math.Max(0, payment.CardNumber!.Length - 4)..] + payment.CardHolderName, 20),
                    MerchantId = CardConnectPaymentsProviderConfig.MerchantId,
                };
                var result = await SendRequestAsync("DELETE", JsonConvert.SerializeObject(request), "profile/" + request.Profile).ConfigureAwait(false);
                var response = result is null ? null : JsonConvert.DeserializeObject<CardConnectProfileResponse>(result);
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
            catch (Exception ex)
            {
                var response = new CardConnectProfileResponse
                {
                    ResponseStatus = ApprovalStatus.Declined,
                    ResponseText = ex.Message,
                };
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            try
            {
                var request = new CardConnectProfileRequest
                {
                    Profile = FixedLength(payment.CardNumber![Math.Max(0, payment.CardNumber!.Length - 4)..] + payment.CardHolderName, 20),
                    MerchantId = CardConnectPaymentsProviderConfig.MerchantId,
                };
                var result = await SendRequestAsync("GET", JsonConvert.SerializeObject(request), "profile/" + request.Profile).ConfigureAwait(false);
                var response = result is null ? null : JsonConvert.DeserializeObject<CardConnectProfileResponse>(result);
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
            catch (Exception ex)
            {
                var response = new CardConnectProfileResponse
                {
                    ResponseStatus = ApprovalStatus.Declined,
                    ResponseText = ex.Message,
                };
                return CardConnectPaymentsProviderExtensions.ToPaymentWalletResponse(response);
            }
        }

        /// <inheritdoc/>
        public Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Fixed length.</summary>
        /// <param name="input"> The input.</param>
        /// <param name="length">The length.</param>
        /// <returns>A string.</returns>
        private static string FixedLength(string input, int length)
        {
            return input.Length > length
                ? input[..length]
                : input.PadRight(length, ' ');
        }
    }
}
