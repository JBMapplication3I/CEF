// <copyright file="PriceRulesPricingProviderTests.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the price rules pricing provider tests class</summary>
// ReSharper disable ExpressionIsAlwaysNull, InconsistentNaming
namespace Clarity.Ecommerce.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Pricing;
    using Ecommerce.Providers.Pricing.PriceRule;
    using Interfaces.DataModel;
    using Xunit;

    [Trait("Category", "Providers.Pricing.PriceRules")]
    public class PriceRulesPricingProviderTests : XUnitLogHelper
    {
        public PriceRulesPricingProviderTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_StandardFlowOfRulesForAProductWhichIsAffected_ForAnonymousUsers_Returns_TheCorrectBaseAndSalePrices()
        {
            return RunnerAsync(
                "PriceRulesPricingProviderTests|Verify_StandardFlowOfRulesForAProductWhichIsAffected_ForAnonymousUsers_Returns_TheCorrectBaseAndSalePrices",
                1151,
                14.95872m,
                13.71216m,
                doAnon: true);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_StandardFlowOfRulesForAProductWhichIsAffected_Returns_TheCorrectBaseAndSalePrices()
        {
            return RunnerAsync(
                "PriceRulesPricingProviderTests|Verify_StandardFlowOfRulesForAProductWhichIsAffected_Returns_TheCorrectBaseAndSalePrices",
                1151,
                13.48956m,
                12.36543m);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_StandardFlowOfRulesForAProductWhichIsAffected_WithAnExclusiveRuleThatHasAPriceOverride_Returns_TheCorrectBaseAndSalePrices()
        {
            return RunnerAsync(
                "PriceRulesPricingProviderTests|Verify_StandardFlowOfRulesForAProductWhichIsAffected_WithAnExclusiveRuleThatHasAPriceOverride_Returns_TheCorrectBaseAndSalePrices",
                969,
                189.99m,
                null);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_StandardFlowOfRulesForAProductWhichIsAffected_WithAnExclusiveRule_Returns_TheCorrectBaseAndSalePrices()
        {
            return RunnerAsync(
                "PriceRulesPricingProviderTests|Verify_StandardFlowOfRulesForAProductWhichIsAffected_WithAnExclusiveRule_Returns_TheCorrectBaseAndSalePrices",
                1152,
                50m,
                null);
        }

        [Fact(Skip = "Don't run automatically")]
        public Task Verify_StandardFlowOfRulesForAProductWhichIsUnaffected_Returns_TheCorrectBaseAndSalePrices()
        {
            return RunnerAsync(
                "PriceRulesPricingProviderTests|Verify_StandardFlowOfRulesForAProductWhichIsUnaffected_Returns_TheCorrectBaseAndSalePrices",
                400000,
                0m,
                null);
        }

        private static async Task RunnerAsync(
            string contextProfileName,
            int productID,
            decimal expectedBase,
            decimal? expectedSale,
            bool doAnon = false)
        {
            // Arrange
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountAssociationTable = true,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoCurrencyTable = true,
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
                    DoProductTable = true,
                    DoRoleUserTable = true,
                    DoStoreTable = true,
                    DoUserRoleTable = true,
                    DoUserTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync().ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var provider = new PriceRulesPricingProvider();
                Assert.True(provider.HasValidConfiguration);
                var pricingFactory = new PricingFactory();
                var factoryProduct = await pricingFactory.GetPricingFactoryProductAsync(
                    productID: productID,
                    cartItemAttributes: null,
                    contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                var factoryContext = pricingFactory.GetPricingFactoryContext(
                    quantity: 1,
                    accountID: doAnon ? (int?)null : mockingSetup.RawAccounts.Select(x => x.ID).First(),
                    userID: doAnon ? (int?)null : mockingSetup.RawUsers.Select(x => x.ID).First(),
                    pricePoint: null,
                    storeID: mockingSetup.RawStores.Select(x => x.ID).First(),
                    currencyID: mockingSetup.RawCurrencies.Select(x => x.ID).First(),
                    sessionID: null,
                    contextProfileName: contextProfileName);
                // Act
                var calculatedPrice = await pricingFactory.CalculatePriceAsync(
                    factoryProduct,
                    factoryContext,
                    contextProfileName,
                    provider,
                    true)
                    .ConfigureAwait(false);
                // Assert
                Assert.Equal(nameof(PriceRulesPricingProvider), calculatedPrice.PricingProvider);
                Assert.Equal(expectedBase, calculatedPrice.UnitPrice);
                Assert.Equal(expectedSale, calculatedPrice.SalePrice);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
