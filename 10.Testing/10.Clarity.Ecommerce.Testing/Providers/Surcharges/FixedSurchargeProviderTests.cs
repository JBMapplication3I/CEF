// <copyright file="FixedSurchargeProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the fixed surcharge provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Surcharges.Fixed.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Surcharges;
    using Surcharges.Testing;
    using Interfaces.Models;
    using Models;
    using Xunit;

    /// <summary>Test the fixed surcharge provider.</summary>
    /// <remarks>We don't test completion or cancellation as they are defined to be no-ops for this provider.</remarks>
    /// <seealso cref="SurchargeProviderTestsBase"/>
    [Trait("Category", "Providers.Surcharges.Fixed")]
    public class FixedSurchargeProviderTests : SurchargeProviderTestsBase
    {
        /// <summary>Test various fixed percentage surcharges, including having no surcharge (default).</summary>
        /// <returns>A Task.</returns>
        [Fact]
        public Task Verify_FixedPercentage() => Run<FixedSurchargeProvider>(async context =>
        {
            var provider = RegistryLoaderWrapper.GetSurchargeProvider(context);
            Assert.NotNull(provider);
            // IDs are arbitrary, Fixed doesn't load them at all.
            var descriptor = StandardDescriptor;
            var expecteds = new (decimal?, decimal)[]
            {
                (null, 0),
                (0.03m, 3.0m),
                (0.42m, 42.0m),
            };
            foreach (var (pct, amount) in expecteds)
            {
                FixedSurchargeProviderConfig.StandardPercent = pct;
                var (newDescriptor, surcharge) = await provider!.CalculateSurchargeAsync(descriptor, context).ConfigureAwait(false);
                Assert.Equal(amount, surcharge);
            }
        });

        /// <summary>Test the provider's keygen. FixedSurchargeProvider uses the default behavior of the base provider.</summary>
        /// <returns>A Task.</returns>
        [Fact]
        public Task Verify_KeyGen() => Run<FixedSurchargeProvider>(async context =>
        {
            var provider = RegistryLoaderWrapper.GetSurchargeProvider(context);
            Assert.NotNull(provider);
            var descriptor = await provider!.ResolveKeyAsync(StandardDescriptor, context);
            // Note that the key has the invoice IDs in ascending order but the descriptor has them starting out of order.
            Assert.Equal(StandardDescriptorKey, descriptor.Key);
        });

        /// <summary>(Immutable)
        /// The key which InterPayments' provider should generate for <see cref="StandardDescriptor"/>.</summary>
        private const string StandardDescriptorKey = "BillTo42:Invoice1|Invoice2|Invoice5:PayingAgainst100.00";

        /// <summary>An arbitrary descriptor, suitable for typical testing.</summary>
        /// <value>The standard descriptor.</value>
        private static SurchargeDescriptor StandardDescriptor => new SurchargeDescriptor
        {
            TotalAmount = 100.0m,
            BillingContact = new ContactModel { ID = 42, },
            BIN = null, // Fixed does not use BIN
            ApplicableInvoices = new SalesInvoiceModel[]
            {
                new() { ID = 1, BalanceDue = 25, },
                new() { ID = 5, BalanceDue = 25, },
                new() { ID = 2, BalanceDue = 50, },
            }.Cast<ISalesInvoiceModel>().ToHashSet(),
        };
    }
}
