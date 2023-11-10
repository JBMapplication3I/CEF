// <copyright file="MockPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the MockPaymentProvider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Mock
{
    using System;
    using System.Configuration;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;

    /// <summary>A mock payment provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class MockPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override bool HasValidConfiguration =>
            (ConfigurationManager.AppSettings["Clarity.Providers.EnabledProviders"]
                    ?.Split(';', ',')
                    .Any(x => x == nameof(MockPaymentsProvider))
                ?? false)
            || IsDefaultProvider && IsDefaultProviderActivated;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            // Do nothing
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
            return Task.FromResult<IPaymentResponse>(
                new PaymentResponse
                {
                    Amount = payment?.Amount ?? 0,
                    Approved = true,
                    AuthorizationCode = string.Empty,
                    ResponseCode = "200",
                    TransactionID = GenCode(payment!),
                });
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult<IPaymentResponse>(
                new PaymentResponse
                {
                    Amount = payment?.Amount ?? 0,
                    Approved = true,
                    AuthorizationCode = GenCode(payment!),
                    ResponseCode = "200",
                    TransactionID = string.Empty,
                });
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
            return Task.FromResult<IPaymentResponse>(
                new PaymentResponse
                {
                    Amount = payment?.Amount ?? 0,
                    Approved = true,
                    AuthorizationCode = GenCode(payment!),
                    ResponseCode = "200",
                    TransactionID = GenCode(payment!),
                });
        }

        private static string GenCode(IProviderPayment payment)
        {
            return $"MOCK:{(IsACH(payment) ? "ACH" : "CC")}:{Guid.NewGuid()}";
        }

        // ReSharper disable once UnusedMember.Local
        private static IPaymentResponse ToPaymentResponse(decimal amount)
        {
            return new PaymentResponse
            {
                Approved = true,
                Amount = amount,
                AuthorizationCode = "mock",
                ResponseCode = "mock: success",
                TransactionID = "test",
            };
        }
    }
}
