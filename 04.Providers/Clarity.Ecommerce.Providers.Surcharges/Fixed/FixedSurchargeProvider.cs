// <copyright file="FixedSurchargeProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fixed surcharge provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Surcharges.Fixed
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Providers.Surcharges;
    using Utilities;

    /// <summary>Surcharge provider that always returns $0.00 and for which completions/cancellations are no-ops.</summary>
    /// <seealso cref="SurchargeProviderBase"/>
    public class FixedSurchargeProvider : SurchargeProviderBase
    {
        /// <inheritdoc />
        public override bool HasValidConfiguration => true;

        /// <inheritdoc />
        public override async Task<(SurchargeDescriptor descriptor, decimal amount)> CalculateSurchargeAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName)
        {
            _ = Contract.RequiresNotNull(descriptor.TotalAmount, "Total amount was not provided when calculating surcharge.");
            Contract.Requires<ArgumentException>(descriptor.TotalAmount > 0, $"Total amount ({descriptor.TotalAmount}) may not be <= 0.");
            descriptor = await ResolveKeysOrThrowAsync(descriptor, contextProfileName).ConfigureAwait(false);
            descriptor = await ResolveKeyAsync(descriptor, contextProfileName).ConfigureAwait(false);
            descriptor.ProviderKey = descriptor.Key;
            return (descriptor, descriptor.TotalAmount!.Value * (FixedSurchargeProviderConfig.StandardPercent ?? 0.0m));
        }

        /// <inheritdoc />
        public override Task<SurchargeDescriptor> CancelAsync(SurchargeDescriptor descriptor, string? contextProfileName)
            => Task.FromResult(descriptor);

        /// <inheritdoc />
        public override Task<SurchargeDescriptor> MarkCompleteAsync(
                SurchargeDescriptor descriptor,
                bool mayThrow,
                string? contextProfileName)
            => Task.FromResult(descriptor);

        /// <inheritdoc />
        public override async Task<SurchargeDescriptor> TryResolveProviderKeyAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName)
        {
            descriptor = await ResolveKeyAsync(descriptor, contextProfileName).ConfigureAwait(false);
            descriptor.ProviderKey = descriptor.Key;
            return descriptor;
        }
    }
}
