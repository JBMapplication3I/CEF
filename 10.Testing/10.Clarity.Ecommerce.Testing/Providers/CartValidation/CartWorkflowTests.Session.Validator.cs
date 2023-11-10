// <copyright file="CartValidatorTests.Session.Validator.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart workflow tests class</summary>
namespace Clarity.Ecommerce.Providers.CartValidation.Testing
{
    using System;
    using System.Collections.Concurrent;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
    using System.Threading.Tasks;
    using Ecommerce.Providers.Taxes.Basic;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Interfaces.Workflow;
    using Models;
    using Moq;
    using Providers.CartValidation;
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap;
#endif
    using Workflow;
    using Workflow.Testing;
    using Xunit;

    [Trait("Category", "Workflows.Shopping.Carts.Session.Validator")]
    public class CartValidatorTests : XUnitLogHelper
    {
        public CartValidatorTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private static async Task<ICartWorkflow> GenerateWorkflowAsync(string contextProfileName)
        {
            await Shopping_CartWorkflow_Session_Tests.SetupWorkflowsAsync(contextProfileName).ConfigureAwait(false);
            return new CartWorkflow();
        }

        private static async Task SetupContainerAsync(
            MockingSetup mockingSetup,
            ICartValidatorConfig config,
            IContainer childContainer,
            string contextProfileName)
        {
            await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.For<ICartValidatorConfig>().Use(config);
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
        }

        private enum CartValidatorFactorials
        {
            DoProductRestrictionsByAccountType = 0,
            DoProductRestrictionsByMinMax = 1,
            DoCategoryRestrictionsByMinMax = 2,
            DoStoreRestrictionsByMinMax = 3,
            DoVendorRestrictionsByMinMax = 4,
            DoManufacturerRestrictionsByMinMax = 5,
            DoProductRestrictionsByMustPurchaseMultiplesOfAmount = 6,
            UseShipToHomeFromAnyDCStockCheck = 7,
            UsePickupInStoreStockCheck = 8,
            UseShipToStoreFromStoreDCStockCheck = 9,
            OverrideAndForceShipToHomeOptionIfNoShipOptionSelected = 10,
            OverrideAndForceNoShipToOptionIfWhenShipOptionSelected = 11,
            ProductRestrictionsKeys = 12,
            AccountIsOnHold = 13,
            UsePILS = 14,
            NoItems = 15,
        }

        [Fact(Skip = "Only run on extended runs due to time length")]
        public async Task Verify_ValidateReadyForCheckout_Permutater_Returns_AppropriatelyAsync()
        {
            // Arrange
            const string contextProfileName = "CartValidatorTests|Verify_ValidateReadyForCheckout_Permutater_Returns_Appropriately";
            var restrictionsKeysFilled = new Dictionary<string, string> { ["Purchasable By Individuals"] = "Customer" };
            var restrictionsKeysEmpty = new Dictionary<string, string>();
            var binaryArray = new[] { '0', '1' };
            var combinations =
                from val00 in binaryArray // DoProductRestrictionsByAccountType
                from val01 in binaryArray // DoProductRestrictionsByMinMax
                from val02 in binaryArray // DoCategoryRestrictionsByMinMax
                from val03 in binaryArray // DoStoreRestrictionsByMinMax
                from val04 in binaryArray // DoVendorRestrictionsByMinMax
                from val05 in binaryArray // DoManufacturerRestrictionsByMinMax
                from val06 in binaryArray // DoProductRestrictionsByMustPurchaseMultiplesOfAmount
                from val07 in binaryArray // UseShipToHomeFromAnyDCStockCheck
                from val08 in binaryArray // UsePickupInStoreStockCheck
                from val09 in binaryArray // UseShipToStoreFromStoreDCStockCheck
                from val10 in binaryArray // OverrideAndForceShipToHomeOptionIfNoShipOptionSelected
                from val11 in binaryArray // OverrideAndForceNoShipToOptionIfWhenShipOptionSelected
                from val12 in binaryArray // ProductRestrictionsKeys
                from val13 in binaryArray // Account.IsOnHold
                from val14 in binaryArray // UsePILS
                from val15 in binaryArray // NoItems
                select $"{val00}{val01}{val02}{val03}{val04}{val05}{val06}{val07}{val08}{val09}{val10}{val11}{val12}{val13}{val14}{val15}";
            const int totalLines = 65536; // combinations.Count();  2^16=65,536
            var linesProcessed = 0;
            var startedAt = DateTime.Now;
            var capturedExceptions = new ConcurrentDictionary<string, Exception>();
            var cartWithNoItems = new CartModel { SalesItems = null };
            var taxesProvider = new BasicTaxesProvider();
            var sessionGuid = new Guid("344016cd-4149-49e4-b4c0-fce3c621701d");
            var sessionGuidAlt = new Guid("0819541c-b350-4f41-9ac3-b394e8d0d49e");
            var pricingFactoryContext = new PricingFactoryContextModel { PricePoint = "WEB" };
            await combinations
                ////.Where(x => x == "0000001000101000").Skip(512)
                /*
                .Where(x => new[]
                {
                    "0000001000100010",
                    "0000001000101010",
                    "0000001000110010",
                    "0000001000111010",
                    "0000001001100010",
                    "0000001001101010",
                    "0000001001110010",
                    "0000001001111010",
                    "0000001010100010",
                    "0000001010101010",
                    "0000001010110010",
                    "0000001010111010",
                    "0000001011100010",
                    "0000001011101010",
                    "0000001011110010",
                    "0000001011111010",
                    "0000001100100010",
                    "0000001100101010",
                    "0000001100110010",
                    "0000001100111010",
                    "0000001101100010",
                    "0000001101101010",
                    "0000001101110010",
                    "0000001101111010",
                    "0000001110100010",
                    "0000001110101010",
                    "0000001110110010",
                    "0000001110111010",
                    "0000001111100010",
                    "0000001111101010",
                    "0000001111110010",
                    "0000001111111010"
                }.Contains(x) || x[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '1')
                */
                .Take(32)
                .ForEachAsync(
                8,
                async combination =>
                {
                    // TestOutputHelper.WriteLine(combination);
                    if (++linesProcessed % 100 == 0)
                    {
                        var timeTaken = DateTime.Now - startedAt;
                        var linesLeft = totalLines - linesProcessed;
                        TestOutputHelper.WriteLine($"{linesProcessed:n0} of {totalLines:n0} ({linesProcessed / (double)totalLines:p}) [{timeTaken:g}|{TimeSpan.FromMilliseconds((timeTaken.TotalMilliseconds / linesProcessed) * linesLeft):g}]");
                    }
                    var thisContextProfileName = contextProfileName + "_" + combination;
                    using var childContainer = RegistryLoader.RootContainer.CreateChildContainer();
                    try
                    {
                        var config = new CartValidatorConfig
                        {
                            DoProductRestrictionsByAccountType = combination[(int)CartValidatorFactorials.DoProductRestrictionsByAccountType] == '1',
                            DoProductRestrictionsByMinMax = combination[(int)CartValidatorFactorials.DoProductRestrictionsByMinMax] == '1',
                            DoCategoryRestrictionsByMinMax = combination[(int)CartValidatorFactorials.DoCategoryRestrictionsByMinMax] == '1',
                            DoStoreRestrictionsByMinMax = combination[(int)CartValidatorFactorials.DoStoreRestrictionsByMinMax] == '1',
                            DoVendorRestrictionsByMinMax = combination[(int)CartValidatorFactorials.DoVendorRestrictionsByMinMax] == '1',
                            DoManufacturerRestrictionsByMinMax = combination[(int)CartValidatorFactorials.DoManufacturerRestrictionsByMinMax] == '1',
                            DoProductRestrictionsByMustPurchaseMultiplesOfAmount = combination[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '1',
                            UseShipToHomeFromAnyDCStockCheck = combination[(int)CartValidatorFactorials.UseShipToHomeFromAnyDCStockCheck] == '1',
                            UsePickupInStoreStockCheck = combination[(int)CartValidatorFactorials.UsePickupInStoreStockCheck] == '1',
                            UseShipToStoreFromStoreDCStockCheck = combination[(int)CartValidatorFactorials.UseShipToStoreFromStoreDCStockCheck] == '1',
                            OverrideAndForceShipToHomeOptionIfNoShipOptionSelected = combination[(int)CartValidatorFactorials.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected] == '1',
                            OverrideAndForceNoShipToOptionIfWhenShipOptionSelected = combination[(int)CartValidatorFactorials.OverrideAndForceNoShipToOptionIfWhenShipOptionSelected] == '1',
                            ProductRestrictionsKeysValue = combination[(int)CartValidatorFactorials.ProductRestrictionsKeys] == '1' ? restrictionsKeysFilled : restrictionsKeysEmpty,
                        };
                        var mockingSetup = new MockingSetup
                        {
                            SaveChangesResult = 1,
                            DoAccountTable = true,
                            DoAppliedCartDiscountTable = true,
                            DoAppliedCartItemDiscountTable = true,
                            DoAttributeTypeTable = true,
                            DoCartContactTable = true,
                            DoCartItemTable = true,
                            DoCartItemTargetTable = true,
                            DoCartTable = true,
                            DoCartTypeTable = true,
                            DoCategoryTable = true,
                            DoContactTable = true,
                            DoContactTypeTable = true,
                            DoDiscountTable = true,
                            DoGeneralAttributeTable = true,
                            DoInventoryLocationSectionTable = combination[(int)CartValidatorFactorials.UsePILS] == '1',
                            DoInventoryLocationTable = combination[(int)CartValidatorFactorials.UsePILS] == '1',
                            DoManufacturerProductTable = true,
                            DoManufacturerTable = true,
                            DoNoteTable = true,
                            DoProductCategoryTable = true,
                            DoProductImageTable = true,
                            DoProductInventoryLocationSectionTable = combination[(int)CartValidatorFactorials.UsePILS] == '1',
                            DoProductPricePointTable = true,
                            DoProductTable = true,
                            DoRateQuoteTable = true,
                            DoRoleUserTable = true,
                            DoSalesOrderItemTable = true,
                            DoSalesOrderTable = true,
                            DoStoreInventoryLocationTable = true,
                            DoStoreInventoryLocationTypeTable = true,
                            DoStoreProductTable = true,
                            DoStoreTable = true,
                            DoUserRoleTable = true,
                            DoUserTable = true,
                            DoVendorProductTable = true,
                            DoVendorTable = true,
                        };
                        await SetupContainerAsync(mockingSetup, config, childContainer, thisContextProfileName).ConfigureAwait(false);
                        ICartModel cart;
                        try
                        {
                            cart = combination[(int)CartValidatorFactorials.NoItems] == '1'
                                ? cartWithNoItems
                                : (await (await GenerateWorkflowAsync(thisContextProfileName).ConfigureAwait(false))
                                    .SessionGetAsync(
                                        new SessionCartBySessionAndTypeLookupKey(
                                            combination[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '1' ? sessionGuidAlt : sessionGuid,
                                            "Cart"),
                                        pricingFactoryContext,
                                        taxesProvider,
                                        thisContextProfileName)
                                    .ConfigureAwait(false))
                                  .cartResponse.Result!;
                        }
                        catch (ReflectionTypeLoadException ex)
                        {
                            foreach (var lex in ex.LoaderExceptions)
                            {
                                TestOutputHelper.WriteLine(lex!.ToString());
                            }
                            throw;
                        }
                        var account = new Mock<IAccountModel>();
                        account.Setup(m => m.Type).Returns(() => new TypeModel { Name = "Customer" });
                        if (combination[(int)CartValidatorFactorials.AccountIsOnHold] == '1')
                        {
                            account.Setup(m => m.IsOnHold).Returns(() => true);
                        }
                        // Act
                        var result = await new CartValidator().ValidateReadyForCheckoutAsync(
                                cart: cart,
                                currentAccount: account.Object,
                                taxesProvider: taxesProvider,
                                pricingFactoryContext: pricingFactoryContext,
                                currentUserID: null,
                                currentAccountID: null,
                                ////combination[(int)CartValidatorFactorials.UsePILS] == '1',
                                contextProfileName: thisContextProfileName)
                            .ConfigureAwait(false);
                        // Assert
                        var expectedCount = 0;
                        if (combination[(int)CartValidatorFactorials.AccountIsOnHold] == '1')
                        {
                            Assert.Contains("ERROR! Your Account is currently on hold. Please contact support for assistance.", result.Messages);
                            expectedCount += 1;
                            Assert.Equal(expectedCount, result.Messages.Count); // This check is a hard stop on the validator
                        }
                        else if (combination[(int)CartValidatorFactorials.NoItems] == '1')
                        {
                            Assert.Contains("ERROR! There are no active items in this cart.", result.Messages);
                            expectedCount += 1;
                            Assert.Equal(expectedCount, result.Messages.Count); // This check is a hard stop on the validator
                        }
                        else
                        {
                            if (combination[(int)CartValidatorFactorials.DoVendorRestrictionsByMinMax] == '1')
                            {
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Laars</b> missing <b>18</b> units of items. They suggest using more items from the <a ui-sref-plus uisrp-is-catalog=\"true\" uisrp-params=\"{ 'category': '{{bufferCategorySeoUrl}}' }\">{{bufferCategoryName}}</a> category or this specific item: <a ui-sref-plus uisrp-is-product=\"true\" uisrp-seo=\"{{bufferItemSeoUrl}}\">{{bufferItemName}}</a>.</p>", result.Messages);
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Laars</b>, missing <b>$1,959.00</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}>{{bufferItemName}}</a>\".</p>\r\n<p>An Override Fee of <b>$10.00</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of $10.00</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>$2,500.00</b> for <b>Laars</b>, you need an additional <b>$2,459.00</b>. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingDollarAmountIgnoredAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>25</b> units for <b>Laars</b>, you need an additional <b>23</b> units. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                expectedCount += 4;
                            }
                            if (combination[(int)CartValidatorFactorials.DoStoreRestrictionsByMinMax] == '1')
                            {
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Store 1</b> missing <b>18</b> units of items. They suggest using more items from the <a ui-sref-plus uisrp-is-catalog=\"true\" uisrp-params=\"{ 'category': '{{bufferCategorySeoUrl}}' }\">{{bufferCategoryName}}</a> category or this specific item: <a ui-sref-plus uisrp-is-product=\"true\" uisrp-seo=\"{{bufferItemSeoUrl}}\">{{bufferItemName}}</a>.</p>", result.Messages);
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Store 1</b>, missing <b>$1,959.00</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}>{{bufferItemName}}</a>\".</p>\r\n<p>An Override Fee of <b>$10.00</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of $10.00</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>$2,500.00</b> for <b>Store 1</b>, you need an additional <b>$2,459.00</b>. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingDollarAmountIgnoredAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>25</b> units for <b>Store 1</b>, you need an additional <b>23</b> units. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                expectedCount += 4;
                            }
                            if (combination[(int)CartValidatorFactorials.DoManufacturerRestrictionsByMinMax] == '1')
                            {
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Manufacturer 1</b> missing <b>19</b> units of items. They suggest using more items from the <a ui-sref-plus uisrp-is-catalog=\"true\" uisrp-params=\"{ 'category': '{{bufferCategorySeoUrl}}' }\">{{bufferCategoryName}}</a> category or this specific item: <a ui-sref-plus uisrp-is-product=\"true\" uisrp-seo=\"{{bufferItemSeoUrl}}\">{{bufferItemName}}</a>.</p>", result.Messages);
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Manufacturer 1</b>, missing <b>$1,970.00</b> worth of items. They suggest using more items from the <a href=\"/{{bufferCategorySeoUrl}}\">{{bufferCategoryName}}</a> category or this specific item: \"<a href=\"/Product/{{bufferItemSeoUrl}}>{{bufferItemName}}</a>\".</p>\r\n<p>An Override Fee of <b>$10.00</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of $10.00</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>$2,500.00</b> for <b>Manufacturer 1</b>, you need an additional <b>$2,470.00</b>. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingDollarAmountIgnoredAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>25</b> units for <b>Manufacturer 1</b>, you need an additional <b>24</b> units. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-1'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                expectedCount += 4;
                            }
                            if (combination[(int)CartValidatorFactorials.DoCategoryRestrictionsByMinMax] == '1')
                            {
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Wine</b> missing <b>20</b> units of items. They suggest using more items from the <a ui-sref-plus uisrp-is-catalog=\"true\" uisrp-params=\"{ 'category': 'Glass' }\">Glass</a> category or this specific item: <a ui-sref-plus uisrp-is-product=\"true\" uisrp-seo=\"200ml-BF-ANG-BT\">200ml BF ANG BT</a>.</p>", result.Messages);
                                Assert.Contains("ERROR! <p>Custom Warning Message for <b>Wine</b>, missing <b>$1,959.00</b> worth of items. They suggest using more items from the <a href=\"/Glass\">Glass</a> category or this specific item: \"<a href=\"/Product/200ml-BF-ANG-BT>200ml BF ANG BT</a>\".</p>\r\n<p>An Override Fee of <b>$10.00</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['DollarAmountOverrideFeeAcceptedFor-Store-2'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of $10.00</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>$2,500.00</b> for <b>Wine</b>, you need an additional <b>$2,459.00</b>. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingDollarAmountIgnoredAcceptedFor-Store-2'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                Assert.Contains("ERROR! <p>Your cart does not meet the minimum free shipping requirement of <b>25</b> units for <b>Wine</b>, you need an additional <b>23</b> units. Please add more items to your shopping cart to ensure your total order can get access to free shipping.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['FreeShippingQuantityAmountIgnoredAcceptedFor-Store-2'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the standard freight charges</label></div>", result.Messages);
                                Assert.Contains("200ml-BF-ANG-BT", result.Messages);
                                Assert.Contains("Glass", result.Messages);
                                expectedCount += 6;
                            }
                            if (combination[(int)CartValidatorFactorials.DoProductRestrictionsByAccountType] == '1')
                            {
                                Assert.Contains("WARNING! This account is not allowed to purchase \"{cartItem.ProductName}\". It has been removed from the current cart. Please contact support for assistance.", result.Messages);
                                expectedCount += 1;
                            }
                            if (combination[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '1')
                            {
                                Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLUE ZORK A\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                expectedCount += 1;
                                // if (combination[(int)CartValidatorFactorials.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected] == '0')
                                // {
                                Assert.Contains("ERROR! <p>Your cart does not meet the purchasing requirement for the Product <b>BLUE ZORK E</b> which must be purchased in multiples of <b>24</b>. You need an additional <b>23 units</b>. Please add more items to your shopping cart to ensure your total quantity meets the requirement.</p>\r\n<p>An Override Fee of <b>5%</b> is available to ignore this requirement and will be calculated and charged at the time of invoicing if accepted.</p><div class=\"checkbox mb-0\"><label><input type=\"checkbox\" ng-change=\"cartValidatorCtrl.cvCartService.updateCartAttributes(cartValidatorCtrl.type)\" ng-model=\"cartValidatorCtrl.cvCartService.carts[cartValidatorCtrl.type].SerializableAttributes['MustPurchaseProductInMultiplesOfAmountIgnoredAcceptedFor-Product-974'].Value\" ng-true-value=\"'true'\" ng-false-value=\"'false'\" />I accept the Override Fee of 5%</label></div>", result.Messages);
                                expectedCount += 1;
                                // }
                                // else
                                // {
                                //     Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLUE ZORK E\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                //     expectedCount += 1;
                                // }
                                if (combination[(int)CartValidatorFactorials.UsePILS] == '1')
                                {
                                    Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLUE ZORK B\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                    Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLUE ZORK C\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                    Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLUE ZORK D\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                    Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLUE ZORK F\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                    expectedCount += 4;
                                }
                            }
                            if (combination[(int)CartValidatorFactorials.DoProductRestrictionsByMinMax] == '1')
                            {
                                Assert.Contains("WARNING! The quantity of \"BLUE ZORK A\" did not meet the requirements set by the store administrators and has been adjusted to 2.", result.Messages);
                                Assert.Contains("WARNING! The quantity of \"BLUE ZORK D\" did not meet the requirements set by the store administrators and has been adjusted to 2.", result.Messages);
                                Assert.Contains("WARNING! The quantity of \"BLUE ZORK F\" did not meet the requirements set by the store administrators and has been adjusted to 2.", result.Messages);
                                Assert.Contains("WARNING! This account has purchased the maximum allotted quantity of \"BLUE ZORK C\". It has been removed from the current cart. If you believe this is in error, please contact support for assistance.", result.Messages);
                                expectedCount += 4;
                            }
                            if (combination[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '0'/*combination[(int)CartValidatorFactorials.UsePILS] == '0'
                                || combination[(int)CartValidatorFactorials.OverrideAndForceNoShipToOptionIfWhenShipOptionSelected] == '1'
                                || combination[(int)CartValidatorFactorials.UsePILS] == '1' && combination[(int)CartValidatorFactorials.ProductRestrictionsKeys] == '1'*/)
                            {
                                Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"BLACK ZORK\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"200ml BF ANG BT\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                expectedCount += 2;
                            }
                            if (combination[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '0'
                                && (combination[(int)CartValidatorFactorials.UsePILS] == '0'
                                    || combination[(int)CartValidatorFactorials.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected] == '1'))
                            {
                                Assert.Contains("WARNING! There is not enough stock to cover the requested quantity and this product \"200ml OPERA ANG\" is not allowed to be back-ordered. The quantity has been reduced to the amount in stock.", result.Messages);
                                expectedCount += 1;
                            }
                            if (combination[(int)CartValidatorFactorials.UsePILS] == '1'
                                && combination[(int)CartValidatorFactorials.OverrideAndForceShipToHomeOptionIfNoShipOptionSelected] == '1'
                                && combination[(int)CartValidatorFactorials.DoProductRestrictionsByMustPurchaseMultiplesOfAmount] == '0')
                            {
                                Assert.Contains("ERROR! After validation, the cart was empty.", result.Messages);
                                expectedCount += 1;
                            }
                            Assert.Equal(expectedCount, result.Messages.Count);
                        }
                    }
                    catch (Exception captured)
                    {
                        capturedExceptions[combination] = captured;
                        TestOutputHelper.WriteLine("{0}: {1}: {2}", combination, captured.GetType().Name, captured.Message);
                    }
                    finally
                    {
                        RegistryLoader.RemoveOverrideContainer(thisContextProfileName);
                    }
                })
                .ConfigureAwait(false);
            if (!capturedExceptions.IsEmpty)
            {
                throw new Exception($"{capturedExceptions.Count} captured exceptions");
            }
        }
    }
}
