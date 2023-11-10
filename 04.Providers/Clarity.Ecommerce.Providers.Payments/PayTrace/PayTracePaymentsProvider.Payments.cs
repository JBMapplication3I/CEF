// <copyright file="PayTracePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayTrace payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayTrace
{
    using System;
    using System.Threading.Tasks;
    using Ecommerce.Models;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Utilities;

    /// <summary>A PayTrace gateway.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class PayTracePaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PayTracePaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(payment.Amount);
            Contract.RequiresNotNull(billing);
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentResponse { Approved = false };
            }
            var request = new KeyedSaleRequest
            {
                Amount = (double)payment.Amount!.Value,
                CreditCard = new()
                {
                    Number = payment.CardNumber,
                    ExpirationMonth = payment.ExpirationMonth.ToString(),
                    ExpirationYear = payment.ExpirationYear.ToString(),
                },
                Csc = payment.CVV,
                BillingAddress = new()
                {
                    Name = billing!.FullName,
                    StreetAddress = billing.Address!.Street1,
                    City = billing.Address.City,
                    State = billing.Address.RegionCode,
                    Zip = billing.Address.PostalCode,
                },
            };
            if (!string.IsNullOrWhiteSpace(billing.Address.Street2))
            {
                request.BillingAddress.StreetAddress += " " + billing.Address.Street2;
            }
            if (!string.IsNullOrWhiteSpace(billing.Address.Street3))
            {
                request.BillingAddress.StreetAddress += " " + billing.Address.Street3;
            }
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlKeyedAuthorization,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<KeyedSaleResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentResponse
            {
                Amount = payment.Amount.Value,
                Approved = response.Success,
                AuthorizationCode = response.ApprovalCode,
                ResponseCode = response.StatusMessage,
                TransactionID = response.TransactionId.ToString(),
            };
        }

        /// <inheritdoc/>
        public override async Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(payment.Amount);
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentResponse { Approved = false };
            }
            var request = new CaptureTransactionRequest
            {
                Amount = (double)payment.Amount!.Value,
                TransactionId = long.Parse(paymentAuthorizationToken),
                IntegratorId = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlCapture,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<PayTraceExternalTransResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentResponse
            {
                Amount = payment.Amount.Value,
                Approved = response.Success,
                ResponseCode = response.ResponseCode.ToString(),
                TransactionID = response.TransactionId.ToString(),
            };
        }

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
            if (useWalletToken)
            {
                return await VaultSaleByCustomerID(payment.Token!, payment.Amount!.Value).ConfigureAwait(false);
            }
            var auth = await AuthorizeAsync(
                    payment,
                    billing,
                    shipping,
                    paymentAlreadyConverted,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!auth.Approved)
            {
                return auth;
            }
            return await CaptureAsync(auth.TransactionID!, payment, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Gets the token.</summary>
        /// <returns>The token.</returns>
        protected static OAuthToken? GetToken()
        {
            return OAuthTokenGenerator.GetToken(
                PayTracePaymentsProviderConfig.Url!,
                PayTracePaymentsProviderConfig.Username!,
                PayTracePaymentsProviderConfig.Password!);
        }

        /// <summary>Vault sale by customer identifier.</summary>
        /// <param name="customerID">Identifier for the customer.</param>
        /// <param name="amount">    The amount.</param>
        /// <returns>A Task{IPaymentResponse}</returns>
        private static async Task<IPaymentResponse> VaultSaleByCustomerID(string customerID, decimal amount)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentResponse { Approved = false };
            }
            var request = new
            {
                amount,
                customer_id = customerID,
                integrator_id = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlVaultSaleByCustomerId,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<VaultSaleByCustomerIDResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentResponse
            {
                Amount = amount,
                Approved = response.Success,
                AuthorizationCode = response.ApprovalCode,
                ResponseCode = response.ResponseCode.ToString(),
                TransactionID = response.TransactionId.ToString(),
            };
        }

        /// <summary>Keyed sale.</summary>
        /// <param name="payment">The payment.</param>
        /// <param name="billing">The billing.</param>
        /// <returns>A Task{IPaymentResponse}</returns>
        private static async Task<IPaymentResponse> KeyedSale(
            IProviderPayment payment,
            IContactModel billing)
        {
            var oAuthResult = GetToken();
            if (oAuthResult is null or { ErrorFlag: true })
            {
                return new PaymentResponse { Approved = false };
            }
            var request = new KeyedSaleRequest
            {
                Amount = (double)payment.Amount!.Value,
                CreditCard = new CreditCard
                {
                    Number = payment.CardNumber,
                    ExpirationMonth = payment.ExpirationMonth.ToString(),
                    ExpirationYear = payment.ExpirationYear.ToString(),
                },
                Csc = payment.CVV,
                BillingAddress = new Address
                {
                    Name = billing.FullName ?? billing.FirstName + " " + billing.LastName,
                    StreetAddress = billing.Address!.Street1,
                    City = billing.Address.City,
                    State = billing.Address.RegionCode,
                    Zip = billing.Address.PostalCode,
                },
                IntegratorId = PayTracePaymentsProviderConfig.IntegratorId,
            };
            var requestJson = JsonSerializer.GetSerializedString(request);
            var tempResponse = await PayTraceResponse.ProcessTransactionAsync(
                    PayTracePaymentsProviderConfig.Url + PayTracePaymentsProviderConfig.UrlKeyedSale,
                    oAuthResult.AccessToken!,
                    requestJson)
                .ConfigureAwait(false);
            var response = JsonSerializer.DeserializeResponse<KeyedSaleResponse>(tempResponse);
            JsonSerializer.AssignError(tempResponse, response);
            return new PaymentResponse
            {
                Amount = payment.Amount.Value,
                Approved = response.Success,
                AuthorizationCode = response.ApprovalCode,
                ResponseCode = response.ResponseCode.ToString(),
                TransactionID = response.TransactionId.ToString(),
            };
        }
    }
}
