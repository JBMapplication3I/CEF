// <copyright file="EvoPaymentProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the evo payment provider class</summary>
// https://www.evopayments.us/developers/
// https://docs.evosnap.com/
// https://docs.evosnap.com/commerce-web-services/overview/
// https://github.com/PayFabric/APIs/tree/v2/Sections
// https://sandbox.payfabric.com/Portal/Account/?wforce=1
namespace Clarity.Ecommerce.Providers.Payments.EVO
{
    using System;
    using System.Threading.Tasks;
    using Models;

    /// <summary>An EVO payments provider.</summary>
    /// <seealso cref="PaymentsProviderBase"/>
    public partial class EvoPaymentProvider : PaymentsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => EvoPaymentProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
