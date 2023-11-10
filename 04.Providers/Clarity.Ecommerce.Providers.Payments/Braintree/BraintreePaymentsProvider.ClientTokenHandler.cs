// <copyright file="BraintreePaymentsProvider.ClientTokenHandler.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
namespace Clarity.Ecommerce.Providers.Payments.BraintreePayments
{
    using Braintree;

    /// <summary>Implements the Braintree Client Token Handler class.</summary>
    public static class ClientTokenHandler
    {
        /// <summary>Generates a client token for Braintree payments.</summary>
        /// <returns>Returns the a string containing the client token.</returns>
        public static string GetClientToken()
        {
            return new BraintreeGateway
            {
                Environment = BraintreePaymentsProviderConfig.Environment,
                MerchantId = BraintreePaymentsProviderConfig.MerchantId,
                PublicKey = BraintreePaymentsProviderConfig.PublicKey,
                PrivateKey = BraintreePaymentsProviderConfig.PrivateKey,
            }.ClientToken.Generate();
        }
    }
}
