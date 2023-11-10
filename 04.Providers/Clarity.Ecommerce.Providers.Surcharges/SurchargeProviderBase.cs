// <copyright file="SurchargeProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the surcharge provider base class.</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Surcharges
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using Enums;
    using Interfaces.Providers.Surcharges;
    using Utilities;

    /// <summary>Base for all surcharge providers. Provides a common
    /// <see cref="ResolveKeyAsync"/> implementation which generates based on billing
    /// contact ID/invoice IDs/balance due.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISurchargeProviderBase"/>
    public abstract class SurchargeProviderBase : ProviderBase, ISurchargeProviderBase
    {
        /// <inheritdoc />
        public override ProviderType ProviderType => ProviderType.Surcharges;

        /// <inheritdoc />
        public override bool HasDefaultProvider => true; // NullSurchargeProvider

        /// <inheritdoc />
        public override bool IsDefaultProvider => false;

        /// <inheritdoc />
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc />
        public abstract Task<(SurchargeDescriptor descriptor, decimal amount)> CalculateSurchargeAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName);

        /// <inheritdoc />
        public abstract Task<SurchargeDescriptor> CancelAsync(SurchargeDescriptor descriptor, string? contextProfileName);

        /// <inheritdoc />
        public abstract Task<SurchargeDescriptor> MarkCompleteAsync(
            SurchargeDescriptor descriptor,
            bool mayThrow,
            string? contextProfileName);

        /// <inheritdoc />
        public virtual Task<SurchargeDescriptor> ResolveKeyAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName)
        {
            if (descriptor.Key is not null)
            {
                return Task.FromResult(descriptor);
            }
            if (descriptor.BillingContact is null)
            {
                throw new ArgumentException("Missing BillingContact when resolving a key/surcharge.");
            }
            if (descriptor.ApplicableInvoices?.Any() != true)
            {
                throw new ArgumentException("Missing BillingContact when resolving a key/surcharge.");
            }
            descriptor.Key = string.Join(
                ":",
                $"BillTo{descriptor.BillingContact.ID}",
                string.Join(
                    "|",
                    descriptor.ApplicableInvoices
                        .OrderBy(i => i.ID) // With this OrderBy, this should be stable.
                        .Select(i => $"Invoice{i.ID}")),
                $"PayingAgainst{descriptor.ApplicableInvoices.Sum(i => i.BalanceDue):.00}");
            return Task.FromResult(descriptor);
        }

        /// <inheritdoc />
        public abstract Task<SurchargeDescriptor> TryResolveProviderKeyAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName);

        /// <summary>Helper for calling ResolveKey and TryResolveProviderKey.</summary>
        /// <param name="descriptor">        A descriptor giving as much info as possible about the surcharge taking
        ///                                  place.</param>
        /// <param name="contextProfileName">For DI.</param>
        /// <returns>The updated descriptor.</returns>
        protected async Task<SurchargeDescriptor> ResolveKeysOrThrowAsync(
            SurchargeDescriptor descriptor,
            string? contextProfileName)
        {
            if (!Contract.CheckValidKey(descriptor.Key))
            {
                descriptor = await ResolveKeyAsync(descriptor, contextProfileName).ConfigureAwait(false);
            }
            if (!Contract.CheckValidKey(descriptor.ProviderKey))
            {
                descriptor = await TryResolveProviderKeyAsync(descriptor, contextProfileName).ConfigureAwait(false);
            }
            return descriptor;
        }
    }
}
