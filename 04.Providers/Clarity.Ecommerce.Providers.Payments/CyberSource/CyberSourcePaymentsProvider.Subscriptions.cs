// <copyright file="CyberSourcePaymentsProvider.Subscriptions.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cyber source gateway class</summary>
#if !NET5_0_OR_GREATER // Cybersource doesn't have .net 5.0+ builds
// ReSharper disable InheritdocInvalidUsage
#pragma warning disable SA1648 // inheritdoc should be used with inheriting class
namespace Clarity.Ecommerce.Providers.Payments.CyberSource
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using global::CyberSource.Clients;
    using global::CyberSource.Clients.SoapServiceReference;
    using Interfaces.Providers.Payments;
    using Renci.SshNet.Messages.Authentication;
    using Utilities;

    /// <content>A Cyber source gateway.</content>
    /// <seealso cref="ISubscriptionProviderBase"/>
    // removed implementing ISubscrptionProviderBase because
    // ImplementProductSubscriptionFromOrderItemAsync is part of the interface,
    // needed in PaymentsProviderBase, but ISubscriptionProviderBase is not
    // implemented in PaymentsProviderBase
    public partial class CyberSourcePaymentsProvider
    {
        /// <inheritdoc/>
        public async Task<IPaymentResponse> CreateSubscriptionAsync(
            ISubscriptionPaymentModel model,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresNotNull(model.Payment);
            Contract.RequiresNotNull(model.Payment!.BillingContact);
            Contract.RequiresNotNull(model.Payment.BillingContact!.Address);
            RequestMessage request = new()
            {
                merchantID = merchantID,
                merchantReferenceCode = merchantReferenceCode,
                ccAuthService = new() { run = "true" },
                paySubscriptionCreateService = new() { run = "true" },
                billTo = new()
                {
                    firstName = model.Payment.BillingContact.FirstName,
                    lastName = model.Payment.BillingContact.LastName,
                    street1 = model.Payment.BillingContact.Address!.Street1,
                    street2 = model.Payment.BillingContact.Address.Street2,
                    street3 = model.Payment.BillingContact.Address.Street3,
                    city = model.Payment.BillingContact.Address.City,
                    state = billingStateText,
                    postalCode = model.Payment.BillingContact.Address.PostalCode,
                    country = billingCountryText,
                    email = model.Payment.BillingContact.Email1,
                },
                card = new()
                {
                    accountNumber = model.Payment.CardNumber,
                    expirationMonth = model.Payment.ExpirationMonth.ToString(),
                    expirationYear = model.Payment.ExpirationYear.ToString(),
                },
                purchaseTotals = new() { currency = "USD" },
                recurringSubscriptionInfo = new()
                {
                    numberOfPayments = model.NumberOfPayments.ToString(),
                    startDate = (model.StartDate ?? DateExtensions.GenDateTime).ToString("O"),
                    frequency = model.Frequency,
                    amount = model.Amount.ToString(CultureInfo.InvariantCulture),
                },
            };
            try
            {
                var reply = SoapClient.RunTransaction(config, request);
                return CyberSourcePaymentsProviderExtensions.ToPaymentResponse(reply);
            }
            catch (Exception ex)
            {
                return await LogErrorAndReturnFailedPaymentResponseAsync(
                        nameof(CreateSubscriptionAsync),
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public async Task<IPaymentResponse> UpdateSubscriptionAsync(
            ISubscriptionPaymentModel model,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresNotNull(model.Payment);
            RequestMessage request = new()
            {
                merchantID = merchantID,
                merchantReferenceCode = merchantReferenceCode,
                ccAuthService = new() { run = "true" },
                ccCaptureService = new() { run = "true" },
                paySubscriptionCreateService = new() { run = "true" },
                card = new()
                {
                    accountNumber = model.Payment!.CardNumber,
                    expirationMonth = model.Payment.ExpirationMonth.ToString(),
                    expirationYear = model.Payment.ExpirationYear.ToString(),
                },
                recurringSubscriptionInfo = new()
                {
                    subscriptionID = model.SubscriptionReferenceNumber,
                },
            };
            try
            {
                var reply = SoapClient.RunTransaction(config, request);
                return CyberSourcePaymentsProviderExtensions.ToPaymentResponse(reply);
            }
            catch (Exception ex)
            {
                return await LogErrorAndReturnFailedPaymentResponseAsync(
                        nameof(UpdateSubscriptionAsync),
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <inheritdoc/>
        public Task<IPaymentResponse> GetSubscriptionAsync(
            ISubscriptionPaymentModel model,
#pragma warning disable IDE0060 // Remove unused parameter
            string? contextProfileName)
#pragma warning restore IDE0060 // Remove unused parameter
        {
            RequestMessage request = new()
            {
                merchantID = merchantID,
                merchantReferenceCode = merchantReferenceCode,
                paySubscriptionRetrieveService = new() { run = "true" },
                recurringSubscriptionInfo = new()
                {
                    subscriptionID = model.SubscriptionReferenceNumber,
                },
            };
            var reply = SoapClient.RunTransaction(config, request);
            SubscriptionFromReplyToModel(reply, model);
            return Task.FromResult(
                CyberSourcePaymentsProviderExtensions.ToSubscriptionResponse(
                    reply.paySubscriptionRetrieveReply.reasonCode,
                    reply.paySubscriptionRetrieveReply.subscriptionID));
        }

        /// <inheritdoc/>
        public async Task<IPaymentResponse> CancelSubscriptionAsync(
            ISubscriptionPaymentModel model,
            string? contextProfileName)
        {
            RequestMessage request = new()
            {
                merchantID = merchantID,
                merchantReferenceCode = merchantReferenceCode,
                paySubscriptionCreateService = new() { run = "true" },
                recurringSubscriptionInfo = new()
                {
                    subscriptionID = model.SubscriptionReferenceNumber,
                    status = "cancel",
                },
            };
            try
            {
                var reply = SoapClient.RunTransaction(config, request);
                return CyberSourcePaymentsProviderExtensions.ToPaymentResponse(reply);
            }
            catch (Exception ex)
            {
                return await LogErrorAndReturnFailedPaymentResponseAsync(
                        nameof(CancelSubscriptionAsync),
                        ex,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Subscription from reply to model.</summary>
        /// <param name="reply">The reply.</param>
        /// <param name="model">The model.</param>
        private static void SubscriptionFromReplyToModel(
            ReplyMessage reply,
            ISubscriptionPaymentModel model)
        {
            Contract.RequiresNotNull(model.Payment);
            Contract.RequiresNotNull(model.Payment!.BillingContact);
            Contract.RequiresNotNull(model.Payment.BillingContact!.Address);
            model.Payment.CardNumber = reply.paySubscriptionRetrieveReply.cardAccountNumber;
            model.Payment.ExpirationMonth = int.Parse(reply.paySubscriptionRetrieveReply.cardExpirationMonth);
            model.Payment.ExpirationYear = int.Parse(reply.paySubscriptionRetrieveReply.cardExpirationYear);
            model.Payment.CardType = reply.paySubscriptionRetrieveReply.cardType;
            model.Payment.BillingContact!.Email1 = reply.paySubscriptionRetrieveReply.email;
            model.Payment.BillingContact.FirstName = reply.paySubscriptionRetrieveReply.firstName;
            model.Payment.BillingContact.LastName = reply.paySubscriptionRetrieveReply.lastName;
            model.Payment.BillingContact.Address!.Street1 = reply.paySubscriptionRetrieveReply.street1;
            model.Payment.BillingContact.Address.Street2 = reply.paySubscriptionRetrieveReply.street2;
            model.Payment.BillingContact.Address.Street3 = reply.paySubscriptionRetrieveReply.street2;
            model.Payment.BillingContact.Address.PostalCode = reply.paySubscriptionRetrieveReply.postalCode;
            model.Payment.BillingContact.Address.City = reply.paySubscriptionRetrieveReply.city;
            model.Payment.BillingContact.Address.CountryCustom = reply.paySubscriptionRetrieveReply.country;
            model.Payment.BillingContact.Address.RegionCustom = reply.paySubscriptionRetrieveReply.state;
            model.SubscriptionReferenceNumber = reply.paySubscriptionRetrieveReply.subscriptionID;
            int.TryParse(reply.paySubscriptionRetrieveReply.paymentsRemaining, out var payRemaining);
            model.PaymentRemaining = payRemaining;
            bool.TryParse(reply.paySubscriptionRetrieveReply.automaticRenew, out var autoRenew);
            model.AutomaticRenewal = autoRenew;
        }
    }
}
#endif
