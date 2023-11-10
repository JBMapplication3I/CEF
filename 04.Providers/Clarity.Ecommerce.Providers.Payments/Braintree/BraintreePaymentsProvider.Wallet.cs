// <copyright file="BraintreePaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.Payments.BraintreePayments
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Threading.Tasks;
    using Braintree;
    using Braintree.Exceptions;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Utilities;

    /// <summary>Implements the BraintreePaymentsProvider wallet functionality.</summary>
    public partial class BraintreePaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel? billing,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(billing);
            var customerRequest = new CustomerRequest
            {
                Id = BillingToCustomerKey(billing),
                FirstName = billing!.FirstName,
                LastName = billing.LastName,
                Email = billing.Email1,
                Phone = billing.Phone1,
            };
            try
            {
                Customer customer;
                try
                {
                    customer = await gateway.Customer.FindAsync(customerRequest.Id).ConfigureAwait(false);
                }
                catch (NotFoundException)
                {
                    return BraintreePaymentsProviderExtensions.CustomerToPaymentWalletResponse(
                        await gateway.Customer.CreateAsync(customerRequest).ConfigureAwait(false));
                }
                return BraintreePaymentsProviderExtensions.ToPaymentWalletResponse(customer);
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error: {ex.Message}");
            }
            return BraintreePaymentsProviderExtensions.ToPaymentWalletResponse(
                (Result<Transaction>?)null);
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
            /*
            var token = payment.CardHolderName.Replace(" ", string.Empty)
                + payment.CardType
                + payment.CardNumber.Substring(payment.CardNumber.Length - 4);
            var result = gateway.PaymentMethod.Delete(token);
            return Task.FromResult(
                BraintreePaymentsProviderExtensions.DeletePaymentMethodToPaymentWalletResponse(result));
            */
        }

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
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

        /// <inheritdoc/>
        public Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        private static string BillingToCustomerKey(IContactModel? billing)
        {
            Contract.RequiresNotNull(billing);
            return $"{billing!.FirstName?[0]}{billing.LastName}{billing.ID}";
        }

        private static string BillingAndCardInfoToToken(IContactModel? billing, IProviderPayment payment)
        {
            Contract.RequiresNotNull(billing);
            return billing!.ID.ToString()
                + payment.CardHolderName!.Replace(" ", string.Empty)
                + payment.CardType
                + payment.CardNumber![^4..];
        }

        private static CreditCardAddressRequest BillingToCreditCardAddressRequest(IContactModel? billing)
        {
            Contract.RequiresNotNull(billing);
            return new()
            {
                CountryCodeAlpha3 = billing!.Address!.Country!.ISO3166Alpha3,
                StreetAddress = billing.Address.Street1,
                ExtendedAddress = billing.Address.Street2,
                FirstName = billing.FirstName,
                LastName = billing.LastName,
                Locality = billing.Address.City,
                PostalCode = billing.Address.PostalCode,
                Region = billing.Address.Region?.Name ?? string.Empty,
            };
        }

        private static AddressRequest BillingToAddressRequest(IContactModel? billing)
        {
            Contract.RequiresNotNull(billing);
            return new()
            {
                CountryCodeAlpha3 = billing!.Address!.Country!.ISO3166Alpha3,
                StreetAddress = billing.Address.Street1,
                ExtendedAddress = billing.Address.Street2,
                FirstName = billing.FirstName,
                LastName = billing.LastName,
                Locality = billing.Address.City,
                PostalCode = billing.Address.PostalCode,
                Region = billing.Address.Region?.Name ?? string.Empty,
            };
        }

        private async Task<Customer?> GetCustomerProfileAsync(IContactModel? billing)
        {
            try
            {
                return await gateway.Customer.FindAsync(BillingToCustomerKey(billing)).ConfigureAwait(false);
            }
            catch (NotFoundException)
            {
                // Do Nothing
            }
            return null;
        }

        private async Task<CreditCard?> GetCreditCardAsync(IProviderPayment payment, IContactModel billing)
        {
            try
            {
                return await gateway.CreditCard.FindAsync(BillingAndCardInfoToToken(billing, payment)).ConfigureAwait(false);
            }
            catch (NotFoundException)
            {
                // Do Nothing
            }
            return null;
        }

        private Task<IPaymentWalletResponse> CreateCustomerCreditCardAsync(
            IProviderPayment payment,
            IContactModel billing)
        {
            Contract.RequiresNotNull(billing);
            var token = BillingAndCardInfoToToken(billing, payment);
            var cardRequest = new CreditCardRequest
            {
                CustomerId = billing.FirstName?[0] + billing.LastName + billing.ID.ToString(),
                BillingAddress = BillingToCreditCardAddressRequest(billing),
                CardholderName = payment.CardHolderName,
                Number = payment.CardNumber,
                ExpirationMonth = payment.ExpirationMonth!.Value.ToString(),
                ExpirationYear = payment.ExpirationYear!.Value.ToString(),
                CVV = payment.CVV ?? string.Empty,
                Token = token,
            };
            return Task.FromResult(
                BraintreePaymentsProviderExtensions.CardToPaymentWalletResponse(
                    gateway.CreditCard.Create(cardRequest),
                    token));
        }

        private async Task<IPaymentResponse> CustomerProfilePaymentAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            var customer = await GetCustomerProfileAsync(billing).ConfigureAwait(false);
            if (customer == null)
            {
                await CreateCustomerProfileAsync(payment, billing, contextProfileName).ConfigureAwait(false);
                await CreateCustomerCreditCardAsync(payment, billing).ConfigureAwait(false);
                customer = await GetCustomerProfileAsync(billing).ConfigureAwait(false);
            }
            var card = await GetCreditCardAsync(payment, billing).ConfigureAwait(false);
            if (card == null)
            {
                await CreateCustomerCreditCardAsync(payment, billing).ConfigureAwait(false);
                card = await GetCreditCardAsync(payment, billing).ConfigureAwait(false);
            }
            var txn = new TransactionRequest
            {
                CustomerId = customer!.Id,
                Amount = payment.Amount ?? 0m,
                BillingAddress = BillingToAddressRequest(billing),
                MerchantAccountId = BraintreePaymentsProviderConfig.MerchantAccountID,
                CurrencyIsoCode = BraintreePaymentsProviderConfig.CurrencyIsoCode,
                PaymentMethodToken = card!.Token,
            };
            return BraintreePaymentsProviderExtensions.ToPaymentResponse(gateway.Transaction.Sale(txn));
        }
    }
}
