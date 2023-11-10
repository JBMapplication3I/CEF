// <copyright file="PriceFactoryTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price factory tests class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Pricing;
    using Ecommerce.Providers.Pricing.Flat;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Providers.PricingFactory")]
    public class PriceFactoryTests : XUnitLogHelper
    {
        public PriceFactoryTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public Task Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithFlatPricingProvider()
        {
            return Task.WhenAll(
                Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithFlatPricingProviderInnerAsync(0969, /*.19d,*/ 1, 0.19d, 0.19d),
                Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithFlatPricingProviderInnerAsync(0969, /*0.19d,*/ 2, 0.19d, 0.19d)); // Quantity should not impact price in FlatPricingProvider
        }

        private static async Task Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithFlatPricingProviderInnerAsync(
            int productId, /*double unitPrice,*/ double quantity, double unitPriceExpected, double salePriceExpected)
        {
            // Arrange
            var contextProfileName = $"PriceFactoryTests|Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithFlatPricingProviderInner|{productId}|{quantity}|{unitPriceExpected}|{salePriceExpected}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true };
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var priceFactory = new PricingFactory();
                ////var priceFactoryProduct = new PricingFactoryProductModel
                ////{
                ////    ProductID = productId,
                ////    PriceBase = (decimal)unitPrice
                ////};
                var priceFactoryContext = new PricingFactoryContextModel { Quantity = (decimal)quantity };
                var pricingProvider = new FlatPricingProvider();
                // Act
                var calculatedPrice = await priceFactory.CalculatePriceAsync(productId, null, priceFactoryContext, contextProfileName, pricingProvider, true).ConfigureAwait(false);
                // Assert
                Assert.Equal("FlatPricingProvider", calculatedPrice.PricingProvider);
                Assert.Equal(unitPriceExpected, (double)calculatedPrice.UnitPrice);
                Assert.Equal(salePriceExpected, (double?)calculatedPrice.SalePrice);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Only run when doing performance testing")]
        public Task Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformant()
        {
            return Task.WhenAll(
                Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformantInnerAsync(001, 120),
                Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformantInnerAsync(010, 120),
                Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformantInnerAsync(100, 250),
                Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformantInnerAsync(500, 750));
        }

        private async Task Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformantInnerAsync(int loopCount, int expectedMilliseconds)
        {
            // Arrange
            var contextProfileName = $"PriceFactoryTests|Verify_CalculatePrice_WithASizeableProductBase_RemainsPerformantInner|{loopCount}|{expectedMilliseconds}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoPricing = true, DoProducts = true };
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = new ProductWorkflow();
                var priceFactoryContext = new PricingFactoryContextModel { Quantity = 1, PricePoint = "WEB" };
                // Act
                var (priceList, resultMilliseconds) = await PriceListAsync(loopCount, workflow, 1000004, priceFactoryContext).ConfigureAwait(false);
                // Assert
                Assert.Equal(loopCount, priceList.Count);
                Assert.True(resultMilliseconds < expectedMilliseconds);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        private async Task<(List<decimal>, double resultMilliseconds)> PriceListAsync(
            int loopCount,
            IProductWorkflow productWorkflow,
            int productID,
            IPricingFactoryContextModel priceFactoryContext)
        {
            var startPoint = DateExtensions.GenDateTime;
            TestOutputHelper.WriteLine(loopCount + $" iterations start at {startPoint:O}m");
            var priceList = new List<decimal>();
            for (var i = 0; i < loopCount; i++)
            {
                var price = (await productWorkflow.GetAsync(productID, priceFactoryContext, null).ConfigureAwait(false))?.PriceSale ?? 0;
                priceList.Add(price);
            }
            var endPoint = DateExtensions.GenDateTime;
            TestOutputHelper.WriteLine(loopCount + $" iterations ended at {endPoint:O}m");
            var resultMilliseconds = (endPoint - startPoint).TotalSeconds * 1000d;
            TestOutputHelper.WriteLine(loopCount + " iterations took " + resultMilliseconds + "ms");
            return (priceList, resultMilliseconds);
        }
    }
}
