// <copyright file="TieredPricingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing factory tests class</summary>
// ReSharper disable MultipleSpaces
namespace Clarity.Ecommerce.Testing
{
    using System.Threading.Tasks;
    using Ecommerce.Providers.Pricing;
    using Ecommerce.Providers.Pricing.Tiered;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Moq;
    using Workflow;
    using Xunit;

    [Trait("Category", "Providers.Pricing.Tiered")]
    public class TieredPricingProviderTests : XUnitLogHelper
    {
        public TieredPricingProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private static IProductWorkflow GenerateWorkflow(IMock<IClarityEcommerceEntities> mockContext)
        {
            RegistryLoader.ContainerInstance.Inject(mockContext.Object);
            return new ProductWorkflow();
        }

        [Fact(Skip = "Will resolve later")]
        public async Task Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithTieredPricingProvider()
        {
            var setups = new[]
            {
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 319.98m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0003, basePrice = 00.19000, expected = 284.99m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0006, basePrice = 00.19000, expected = 259.00m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0020, basePrice = 00.19000, expected = 250.00m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0050, basePrice = 00.19000, expected = 244.85m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "ZORK1-2M",   storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 319.98m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "ZORK3-5M",   storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 284.99m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "ZORK6-19M",  storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 259.00m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "ZORK20-49M", storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 250.00m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "ZORK50M+",   storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 244.85m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 00.19000, expected = 319.98m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0003, basePrice = 00.19000, expected = 284.99m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0006, basePrice = 00.19000, expected = 259.00m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0020, basePrice = 00.19000, expected = 250.00m },
                new { productID = 0969, productKey = "ZORK-BLACK",              productName = "BLACK ZORK",      pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0050, basePrice = 00.19000, expected = 244.85m },
                // 0200-ANGBT-BFUT-IA
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "FT",         storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 12.00000, expected = 013.45m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "HT",         storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 12.00000, expected = 013.93m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 12.00000, expected = 016.79m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0120, basePrice = 12.00000, expected = 016.21m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0240, basePrice = 12.00000, expected = 014.59m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 1440, basePrice = 12.00000, expected = 013.93m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 2880, basePrice = 12.00000, expected = 013.45m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "RETAIL",     storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 12.00000, expected = 017.84m },
                new { productID = 1151, productKey = "0200-ANGBT-BFUT-IA",      productName = "200ml BF ANG BT", pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 12.00000, expected = 017.84m },
                // 0200-ANGRF-OPERA-VSI-IA
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "FT",         storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 30.00000, expected = 033.63m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "HT",         storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 30.00000, expected = 034.81m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 30.00000, expected = 044.59m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0060, basePrice = 30.00000, expected = 040.53m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0120, basePrice = 30.00000, expected = 036.48m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 0720, basePrice = 30.00000, expected = 034.81m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "LTL",        storeKey = "store-1", currencyKey = "USD", quantity = 1440, basePrice = 30.00000, expected = 033.63m },
                new { productID = 1152, productKey = "0200-ANGRF-OPERA-VSI-IA", productName = "200ml OPERA ANG", pricePointKey = "WEB",        storeKey = "store-1", currencyKey = "USD", quantity = 0001, basePrice = 30.00000, expected = 046.16m }
            };
            const string ContextProfileName = "TieredPricingProviderTests|Verify_CalculatePrice_Should_ReturnTheCorrectValue_WithTieredPricingProvider";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountPricePointTable = true,
                    DoCurrencyTable = true,
                    DoGeneralAttributeTable = true,
                    DoPackageTypeTable = true,
                    DoPricePointTable = true,
                    DoPriceRoundingTable = true,
                    DoPriceRuleAccountTable = true,
                    DoPriceRuleAccountTypeTable = true,
                    DoPriceRuleCategoryTable = true,
                    DoPriceRuleManufacturerTable = true,
                    DoPriceRuleProductTable = true,
                    DoPriceRuleProductTypeTable = true,
                    DoPriceRuleStoreTable = true,
                    DoPriceRuleTable = true,
                    DoPriceRuleUserRoleTable = true,
                    DoPriceRuleVendorTable = true,
                    DoProductAssociationTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true,
                    DoProductImageTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoStoreTable = true,
                    DoStoreTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, ContextProfileName);
                var mockContext = mockingSetup.MockContext;
                var pricingFactory = new PricingFactory { PricingProvider = new TieredPricingProvider() };
                var productWorkflow = GenerateWorkflow(mockContext);
                foreach (var setup in setups)
                {
                    // Arrange
                    var priceFactoryContext = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>();
                    priceFactoryContext.Quantity = setup.quantity;
                    priceFactoryContext.PricePoint = setup.pricePointKey;
                    priceFactoryContext.StoreKey = setup.storeKey;
                    priceFactoryContext.CurrencyKey = setup.currencyKey;
                    // Act
                    var price = (await productWorkflow.GetAsync(setup.productID, priceFactoryContext, ContextProfileName).ConfigureAwait(false)).PriceBase;
                    // Assert
                    Assert.Equal(nameof(TieredPricingProvider), pricingFactory.PricingProvider?.Name);
                    TestOutputHelper.WriteLine($"{setup.productID:0000}, {setup.pricePointKey}, {setup.storeKey}, {setup.currencyKey}, {setup.quantity:00000}, {setup.expected:$000.00} = {price:$000.00}");
                    Assert.NotNull(price);
                    Assert.Equal(setup.expected, price.Value);
                }
            }
            RegistryLoader.RemoveOverrideContainer(ContextProfileName);
        }
    }
}
