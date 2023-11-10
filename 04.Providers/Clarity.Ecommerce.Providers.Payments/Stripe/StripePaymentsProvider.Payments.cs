// <copyright file="StripePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stripe payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.StripeInt
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Stripe;

    /// <summary>A stripe payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class StripePaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StripePaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
        public override Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string authorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false)
        {
            try
            {
                var request = new ChargeCreateOptions();
                var amount = payment.Amount * 100;
                request.Amount = decimal.ToInt32(amount ?? 0m); // Everything must be in cents
                request.Currency = "usd";
                request.Description = string.Empty;
                if (!string.IsNullOrEmpty(payment.Token))
                {
                    request.Customer = payment.Token;
                }
                else
                {
                    request.Source = new CardCreateNestedOptions
                    {
                        Number = payment.CardNumber,
                        Name = billing?.FullName ?? $"{billing?.FirstName} {billing?.LastName}".Trim(),
                        ExpYear = payment.ExpirationYear ?? 0,
                        ExpMonth = payment.ExpirationMonth ?? 0,
                        AddressCountry = billing?.Address?.CountryCode,
                        AddressLine1 = billing?.Address?.Street1,
                        AddressLine2 = billing?.Address?.Street2,
                        AddressCity = billing?.Address?.City,
                        AddressState = billing?.Address?.RegionCode,
                        AddressZip = billing?.Address?.PostalCode,
                        Cvc = payment.CVV,
                    };
                    request.Capture = true;
                }
                var chargeService = new ChargeService();
                var stripeCharge = chargeService.Create(request);
                return Task.FromResult(StripePaymentsProviderExtensions.ToPaymentResponse(stripeCharge));
            }
            catch (StripeException ex)
            {
                var failedCharge = new Charge
                {
                    Paid = false,
                    FailureCode = ex.StripeError.Code,
                    FailureMessage = ex.Message,
                };
                return Task.FromResult(StripePaymentsProviderExtensions.ToPaymentResponse(failedCharge));
            }
        }
    }
}
