// <copyright file="BraintreePaymentsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.Payments.BraintreePayments
{
    using System;
    using System.Threading.Tasks;
    using Braintree;
    using Models;

    /// <summary>Implements the Braintree payments provider class.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class BraintreePaymentsProvider : PaymentsProviderBase
    {
        private Braintree.Environment? environment;
        private string? merchantId;
        private string? publicKey;
        private string? privateKey;
        private BraintreeGateway gateway = null!;

        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => BraintreePaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> GetAuthenticationToken(string? contextProfileName)
        {
            var response = new CEFActionResponse<string>(false);
            try
            {
                response.Result = await gateway!.ClientToken.GenerateAsync().ConfigureAwait(false);
                response.ActionSucceeded = true;
            }
            catch (Exception ex)
            {
                response.Messages.Add(ex.Message);
            }
            return response;
        }

        /// <inheritdoc/>
        public override Task InitConfigurationAsync(string? contextProfileName)
        {
            environment = BraintreePaymentsProviderConfig.Environment;
            merchantId = BraintreePaymentsProviderConfig.MerchantId;
            publicKey = BraintreePaymentsProviderConfig.PublicKey;
            privateKey = BraintreePaymentsProviderConfig.PrivateKey;
            gateway = new()
            {
                Environment = environment,
                MerchantId = merchantId,
                PublicKey = publicKey,
                PrivateKey = privateKey,
            };
            return Task.CompletedTask;
        }
    }
}
