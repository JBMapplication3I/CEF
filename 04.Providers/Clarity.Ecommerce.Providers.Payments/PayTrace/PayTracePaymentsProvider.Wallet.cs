// <copyright file="PayTracePaymentsProvider.Wallet.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;

    /// <summary>A PayPal Payflow Pro payments provider.</summary>
    /// <seealso cref="IWalletProviderBase"/>
    public partial class PayTracePaymentsProvider : IWalletProviderBase
    {
        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> CreateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentWalletResponse { Approved = false };
            }
            var request = new CustomerProfileRequest
            {
                CustomerID = payment.CardHolderName + payment.CardNumber![^4..],
                CreditCard = new()
                {
                    Number = payment.CardNumber,
                    ExpirationMonth = payment.ExpirationMonth.ToString(),
                    ExpirationYear = payment.ExpirationYear.ToString(),
                },
                IntegratorID = PayTracePaymentsProviderConfig.IntegratorId,
                BillingAddress = new()
                {
                    StreetAddress = billing.Address!.Street1,
                    City = billing.Address.City,
                    Name = $"{billing.FirstName} {billing.LastName}",
                    State = billing.Address.RegionCode,
                    Zip = billing.Address.PostalCode,
                },
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlCreateCustomer,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<CustomerProfileResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentWalletResponse
            {
                Account = response.CustomerID,
                Approved = response.Success,
                CardName = response.MaskedCardNumber,
                CardType = string.Empty,
                Customer = response.CustomerID,
                ExpDate = request.CreditCard.ExpirationMonth + request.CreditCard.ExpirationYear,
                ResponseCode = response.ResponseCode.ToString(),
                Token = response.CustomerID,
            };
        }

        /// <inheritdoc/>
        /// <remarks>This functions the same way that Create does, essentially it overwrites if already there.</remarks>
        public async Task<IPaymentWalletResponse> UpdateCustomerProfileAsync(
            IProviderPayment payment,
            IContactModel billing,
            string? contextProfileName)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentWalletResponse { Approved = false };
            }
            var request = new CustomerProfileRequest
            {
                CustomerID = payment.CardHolderName + payment.CardNumber![^4..],
                CreditCard = new()
                {
                    Number = payment.CardNumber,
                    ExpirationMonth = payment.ExpirationMonth.ToString(),
                    ExpirationYear = payment.ExpirationYear.ToString(),
                },
                BillingAddress = new()
                {
                    StreetAddress = billing.Address!.Street1,
                    City = billing.Address.City,
                    Name = $"{billing.FirstName} {billing.LastName}",
                    State = billing.Address.RegionCode,
                    Zip = billing.Address.PostalCode,
                },
                IntegratorID = PayTracePaymentsProviderConfig.IntegratorId,
                NewID = payment.CardHolderName + payment.CardNumber[^4..],
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlUpdateCustomer,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<CustomerProfileResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentWalletResponse
            {
                Account = response.CustomerID,
                Approved = response.Success,
                CardName = response.MaskedCardNumber,
                CardType = string.Empty,
                Customer = response.CustomerID,
                ExpDate = request.CreditCard.ExpirationMonth + request.CreditCard.ExpirationYear,
                ResponseCode = response.ResponseCode.ToString(),
                Token = response.TransactionId.ToString(),
            };
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> DeleteCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentWalletResponse { Approved = false };
            }
            var request = new CustomerProfileRequest
            {
                CustomerID = payment.Token,
                IntegratorID = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlDeleteCustomer,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<CustomerProfileResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentWalletResponse
            {
                Approved = response.Success,
                Customer = response.CustomerID,
                ResponseCode = response.ResponseCode.ToString(),
                Token = response.CustomerID,
            };
        }

        /// <inheritdoc/>
        public async Task<IPaymentWalletResponse> GetCustomerProfileAsync(
            IProviderPayment payment,
            string? contextProfileName)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentWalletResponse { Approved = false };
            }
            var request = new CustomerProfileRequest
            {
                IntegratorID = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlExportCustomer,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<CustomerProfileExportResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            var customer = response.Customers?.FirstOrDefault(x => x.CustomerID == payment.Token);
            return new PaymentWalletResponse
            {
                Account = customer?.CreditCard?.MaskedNumber,
                Approved = response.Success,
                CardName = customer?.CreditCard?.MaskedNumber,
                Customer = customer?.CustomerID,
                ExpDate = customer?.CreditCard?.ExpirationMonth + customer?.CreditCard?.ExpirationYear,
                ResponseCode = response.ResponseCode.ToString(),
                Token = customer?.CustomerID,
            };
        }

        /// <inheritdoc/>
        public async Task<List<IPaymentWalletResponse>> GetCustomerProfilesAsync(
            string walletAccountNumber,
            string? contextProfileName)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new List<IPaymentWalletResponse> { new PaymentWalletResponse { Approved = false } };
            }
            var request = new CustomerProfileRequest
            {
                IntegratorID = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlExportCustomer,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<CustomerProfileExportResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            var results = new List<IPaymentWalletResponse>();
            foreach (var customer in response.Customers!.Where(x => x.CustomerID == walletAccountNumber))
            {
                results.Add(new PaymentWalletResponse
                {
                    Account = customer.CreditCard!.MaskedNumber,
                    Approved = response.Success,
                    CardName = customer.CreditCard.MaskedNumber,
                    Customer = customer.CustomerID,
                    ExpDate = customer.CreditCard.ExpirationMonth + customer.CreditCard.ExpirationYear,
                    ResponseCode = response.ResponseCode.ToString(),
                    Token = customer.CustomerID,
                });
            }
            if (results.Count == 0)
            {
                results.Add(new PaymentWalletResponse
                {
                    Approved = response.Success,
                    ResponseCode = response.ResponseCode.ToString(),
                });
            }
            return results;
        }
    }
}
