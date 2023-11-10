// <copyright file="HeartlandPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the heartland payments provider class</summary>
// https://developer.heartlandpaymentsystems.com/Documentation/credit-card-payments
namespace Clarity.Ecommerce.Providers.Payments.Heartland
{
    using System;
    using System.Threading.Tasks;
    using GlobalPayments.Api;
    using GlobalPayments.Api.Entities;
    using GlobalPayments.Api.PaymentMethods;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Newtonsoft.Json;
    using Utilities;

    /// <summary>A heartland payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class HeartlandPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => HeartlandPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            if (ProviderMode == Enums.PaymentProviderMode.Production)
            {
                ServicesContainer.Configure(new ServicesConfig
                {
                    GatewayConfig = new GatewayConfig
                    {
                        SecretApiKey = HeartlandPaymentsProviderConfig.SecretApiKey,
                        ServiceUrl = HeartlandPaymentsProviderConfig.ServiceUrl,
                        DeveloperId = HeartlandPaymentsProviderConfig.DeveloperID,
                        VersionNumber = HeartlandPaymentsProviderConfig.VersionNumber,
                    },
                });
                return Task.CompletedTask;
            }
            ServicesContainer.Configure(new ServicesConfig
            {
                GatewayConfig = new GatewayConfig
                {
                    SecretApiKey = HeartlandPaymentsProviderConfig.TestSecretApiKey,
                    ServiceUrl = HeartlandPaymentsProviderConfig.TestServiceUrl,
                    DeveloperId = HeartlandPaymentsProviderConfig.DeveloperID,
                    VersionNumber = HeartlandPaymentsProviderConfig.VersionNumber,
                },
            });
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel billing,
            IContactModel shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel cart = null)
        {
            return Task.FromResult(
                RunTransaction(
                    payment,
                    billing?.Address?.Street1,
                    billing?.Address?.PostalCode,
                    (card, address) => card.Charge(payment.Amount).WithCurrency("USD").WithAddress(address).Execute(),
                    contextProfileName));
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel billing,
            IContactModel shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            return Task.FromResult(
                RunTransaction(
                    payment,
                    billing?.Address?.Street1,
                    billing?.Address?.PostalCode,
                    (card, address) => card.Authorize(payment.Amount).WithCurrency("USD").WithAddress(address).Execute(),
                    contextProfileName));
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult(
                RunTransaction(
                    payment,
                    null,
                    null,
                    (_, __) => Transaction.FromId(paymentAuthorizationToken).Capture(payment.Amount).Execute(),
                    contextProfileName));
        }

        /// <summary>Executes the transaction operation.</summary>
        /// <param name="payment">           The payment.</param>
        /// <param name="street1">           The first street.</param>
        /// <param name="postalCode">        The postal code.</param>
        /// <param name="transactionFunc">   The transaction function.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An IPaymentResponse.</returns>
        private IPaymentResponse RunTransaction(
            IProviderPayment payment,
            string street1,
            string postalCode,
            Func<CreditCardData, Address, Transaction> transactionFunc,
            string? contextProfileName)
        {
            // Prepare to charge a card
            var card = Contract.CheckValidKey(payment.Token)
                ? HeartlandPaymentsProviderExtensions.TokenToCard(payment)
                : HeartlandPaymentsProviderExtensions.PaymentToCard(payment);
            var address = HeartlandPaymentsProviderExtensions.StreetAndZipToAddress(street1, postalCode);
            // Perform the requested transaction
            try
            {
                var response = transactionFunc(card, address);
                switch (response.ResponseCode)
                {
                    case "EC":
                    {
                        Logger.LogError(
                            "HeartlandPaymentsProvider.AuthorizeAndACapture",
                            response.ResponseMessage,
                            JsonConvert.SerializeObject(response));
                        throw new ArgumentException(response.ResponseMessage);
                    }
                    default:
                    {
                        Logger.LogInformation(
                            "HeartlandPaymentsProvider.AuthorizeAndACapture",
                            "Success!",
                            JsonConvert.SerializeObject(response));
                        break;
                    }
                }
                return new PaymentResponse
                {
                    Amount = response.AuthorizedAmount ?? payment.Amount ?? 0m,
                    TransactionID = response.TransactionId,
                    Approved = response.ResponseCode == "00", // "00" == Success
                    ResponseCode = response.ResponseCode,
                    AuthorizationCode = response.AuthorizationCode,
                };
            }
            catch (BuilderException ex1)
            {
                // handle builder errors
                Logger.LogError("HeartlandPaymentsProvider.AuthorizeAndACapture.Error", ex1.Message, contextProfileName);
                throw;
            }
            catch (ConfigurationException ex2)
            {
                // handle errors related to your services configuration
                Logger.LogError("HeartlandPaymentsProvider.AuthorizeAndACapture.Error", ex2.Message, contextProfileName);
                throw;
            }
            catch (GatewayException ex3)
            {
                // handle gateway errors/exceptions
                Logger.LogError("HeartlandPaymentsProvider.AuthorizeAndACapture.Error", ex3.Message, contextProfileName);
                throw;
            }
            catch (UnsupportedTransactionException ex4)
            {
                // handle errors when the configured gateway doesn't support desired transaction
                Logger.LogError("HeartlandPaymentsProvider.AuthorizeAndACapture.Error", ex4.Message, contextProfileName);
                throw;
            }
            catch (ApiException ex5)
            {
                Logger.LogError("HeartlandPaymentsProvider.AuthorizeAndACapture.Error", ex5.Message, contextProfileName);
                throw;
            }
        }
    }
}
