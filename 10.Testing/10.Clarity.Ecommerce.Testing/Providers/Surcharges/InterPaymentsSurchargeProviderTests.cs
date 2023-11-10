// <copyright file="InterPaymentsSurchargeProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inter payments surcharge provider tests class</summary>
namespace Clarity.Ecommerce.Providers.Surcharges.InterPayments.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Providers.Surcharges;
    using Surcharges.Testing;
    using Interfaces.Models;
    using Models;
    using Xunit;
    using JSConfigs;

    /// <summary>Test InterPayments' (https://api.interpayments.com) surcharge provider.</summary>
    /// <seealso cref="Clarity.Ecommerce.Providers.Surcharges.Testing.SurchargeProviderTestsBase"/>
    [Trait("Category", "Providers.Surcharges.Fixed")]
    public class InterPaymentsSurchargeProviderTests : SurchargeProviderTestsBase
    {
        /// <summary>Using InterPayments' standard testing data, test which cards can or cannot be surcharged.</summary>
        /// <remarks>Uses the logic from Wallet Workflows which is enabled by
        /// <see cref="CEFConfigDictionary.CaptureBINEnabled"/> to get BINs from test PANs.</remarks>
        /// <returns>A Task.</returns>
        [Fact(Skip = "Third party changed their end, test must be updated")]
        public Task Verify_Surchargable() => Run<InterpaymentsSurchargeProvider>(async context =>
        {
            var provider = RegistryLoaderWrapper.GetSurchargeProvider(context);
            Assert.NotNull(provider);
            // Testing data from InterPayments' site: https://api.interpayments.com/assets/transactionFeeApi.html#overview--testing
            var expecteds = new (string card, string zip, bool canSurcharge)[]
            {
                // TODO: Find out which jurisdictions don't allow surcharging and duplicate the below for those zips to test regionality better.
                ("4012000098765439", "55436", true),
                ("4012888818888", "55436", true),
                ("5499740000000057", "55436", true),
                ("2223000010309703", "55436", false),
                ("6011000993026909", "55436", true),
                ("371449635392376", "55436", true),
            };
            foreach (var (pan, zip, can) in expecteds)
            {
                var descriptor = StandardDescriptor;
                descriptor.BillingContact!.Address!.PostalCode = zip;
                // Same logic is implemented in the standard wallet workflows,
                // dependent on the
                descriptor.BIN = pan.Length == 16 ? pan[0..8] : pan[0..6];
                // Not being surchargable is not an error, and the provider should always return $0.
                var (newDescriptor, surcharge) = await provider!.CalculateSurchargeAsync(descriptor, context).ConfigureAwait(false);
                if (can)
                {
                    Assert.NotEqual(0.0m, surcharge);
                }
                else
                {
                    Assert.Equal(0.0m, surcharge);
                }
            }
        });

        /// <summary>Test the provider's keygen. InterPayments' provider uses the default behavior of the base provider.</summary>
        /// <returns>A Task.</returns>
        [Fact(Skip = "Third party changed their end, test must be updated")]
        public Task Verify_KeyGen() => Run<InterpaymentsSurchargeProvider>(async context =>
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
            BillingContact = new ContactModel { ID = 42, Address = new() { CountryCode = "USA", PostalCode = "55436", }, },
            BIN = null, // To be filled by the test case
            ApplicableInvoices = new SalesInvoiceModel[]
            {
                new() { ID = 1, BalanceDue = 25, },
                new() { ID = 5, BalanceDue = 25, },
                new() { ID = 2, BalanceDue = 50, },
            }.Cast<ISalesInvoiceModel>().ToHashSet(),
        };
    }
}
