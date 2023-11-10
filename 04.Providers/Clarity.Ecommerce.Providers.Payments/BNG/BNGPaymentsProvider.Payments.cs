// <copyright file="BNGPaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the BNG payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.BNG
{
    using System;
    using System.Globalization;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Models;
    using Transactions;
    using Utilities;

    /// <content>A BNG payments provider.</content>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class BNGPaymentsProvider : PaymentsProviderBase
    {
        /// <summary>URL of the live.</summary>
        private const string LiveURL = "https://secure.bngpaymentgateway.com/api/transact.php";

        /// <summary>The test login.</summary>
        private const string StrTestLogin = "demo";

        /// <summary>The test password.</summary>
        private const string StrTestPassword = "password";

        /// <summary>The login.</summary>
        private string? login;

        /// <summary>The password.</summary>
        private string? password;

        /// <summary>The service.</summary>
        private BNGService service = null!;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BNGPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            login = ProviderMode == Enums.PaymentProviderMode.Testing ? StrTestLogin : BNGPaymentsProviderConfig.Login;
            password = ProviderMode == Enums.PaymentProviderMode.Testing ? StrTestPassword : BNGPaymentsProviderConfig.Password;
            service = new(LiveURL, login!, password!);
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
            Contract.RequiresNotNull(payment.Amount);
            Contract.RequiresAllValidIDs(payment.ExpirationMonth, payment.ExpirationYear);
            return Task.FromResult(
                service.Request(new BNGAuthorizationTransaction(
                    payment.Amount!.Value.ToString(CultureInfo.InvariantCulture),
                    payment.CardNumber!,
                    CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value),
                    payment.CVV!)));
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(payment.Amount);
            return Task.FromResult(
                service.Request(new BNGCaptureTransaction(
                    payment.Amount!.Value.ToString(CultureInfo.InvariantCulture),
                    paymentAuthorizationToken)));
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
            Contract.RequiresNotNull(payment.Amount);
            return Task.FromResult(
                service.Request(new BNGSaleTransaction(
                    payment.Amount!.Value.ToString(CultureInfo.InvariantCulture),
                    payment.CardNumber!,
                    CleanExpirationDate(payment.ExpirationMonth!.Value, payment.ExpirationYear!.Value),
                    payment.CVV!)));
        }
    }
}
