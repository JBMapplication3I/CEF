// <copyright file="LitlePaymentsProvider.Payments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the litle payments provider class</summary>
#if !NET5_0_OR_GREATER // Litle doesn't have .net 5.0+ builds (alternative available)
namespace Clarity.Ecommerce.Providers.Payments.LitleShip
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Litle.Sdk;
    using Models;
    using Utilities;

    /// <content>A litle provider.</content>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class LitlePaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => LitlePaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <summary>Gets or sets the configuration.</summary>
        /// <value>The configuration.</value>
        private Dictionary<string, string?>? Config { get; set; }

        /// <summary>Gets or sets the litle.</summary>
        /// <value>The litle.</value>
        private LitleOnline Litle { get; set; } = null!;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            Config = new()
            {
                { "url", LitlePaymentsProviderConfig.URL },
                { "reportGroup", LitlePaymentsProviderConfig.ReportGroup },
                { "username", LitlePaymentsProviderConfig.Username },
                { "version", LitlePaymentsProviderConfig.Version },
                { "timeout", LitlePaymentsProviderConfig.Timeout },
                { "merchantId", LitlePaymentsProviderConfig.MerchantId },
                { "password", LitlePaymentsProviderConfig.Password },
                { "printxml", LitlePaymentsProviderConfig.Version },
                { "logFile", null },
                { "neuterAccountNums", null },
                { "proxyHost", null },
                { "proxyPort", null },
            };
            Litle = new(Config);
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
            Contract.Requires<ArgumentNullException>(payment.Amount.HasValue);
            var authorization = new authorization
            {
                amount = (long)(payment.Amount!.Value * 100),
                orderSource = orderSourceType.ecommerce,
                billToAddress = new()
                {
                    name = "John Smith",
                    addressLine1 = "1 Main St.",
                    city = "Burlington",
                    state = "MA",
                    zip = "01803-3747",
                    country = countryTypeEnum.US,
                },
                card = new()
                {
                    number = payment.CardNumber,
                    expDate = payment.ExpirationMonth + (payment.ExpirationYear.ToString().Length > 2 ? payment.ExpirationYear.ToString().Substring(payment.ExpirationYear.ToString().Length - 3, 2) : payment.ExpirationYear.ToString()),
                    cardValidationNum = payment.CVV,
                    type = payment.CardNumber!.ToPaymentType(),
                },
            };
            var response = Litle.Authorize(authorization);
            return Task.FromResult(response.ToPaymentResponse());
        }

        /// <inheritdoc/>
        public override Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName)
        {
            return Task.FromResult(
                Litle.Capture(new() { litleTxnId = long.Parse(paymentAuthorizationToken) })
                    .ToPaymentResponse());
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
            Contract.Requires<ArgumentNullException>(payment.Amount.HasValue);
            var authorization = new authorization
            {
                amount = (long)(payment.Amount!.Value * 100),
                orderSource = orderSourceType.ecommerce,
                billToAddress = new()
                {
                    name = "John Smith",
                    addressLine1 = "1 Main St.",
                    city = "Burlington",
                    state = "MA",
                    zip = "01803-3747",
                    country = countryTypeEnum.US,
                },
                card = new()
                {
                    number = payment.CardNumber,
                    expDate = payment.ExpirationMonth + (payment.ExpirationYear.ToString().Length > 2 ? payment.ExpirationYear.ToString().Substring(payment.ExpirationYear.ToString().Length - 3, 2) : payment.ExpirationYear.ToString()),
                    cardValidationNum = payment.CVV,
                    type = payment.CardNumber!.ToPaymentType(),
                },
            };
            var response = Litle.Authorize(authorization);
            return Task.FromResult(Litle.Capture(new() { litleTxnId = response.litleTxnId }).ToPaymentResponse());
        }
    }
}
#endif
