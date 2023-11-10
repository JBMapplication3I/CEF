// <copyright file="PayPalPayflowProPaymentsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the PayPal Payflow Pro payments provider class</summary>
namespace Clarity.Ecommerce.Providers.Payments.PayPalPayflowPro
{
    using System;
    using System.Threading.Tasks;
    using Models;

    /// <summary>A PayPal Payflow Pro payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class PayPalPayflowProPaymentsProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => PayPalPayflowProPaymentsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
    }
}
