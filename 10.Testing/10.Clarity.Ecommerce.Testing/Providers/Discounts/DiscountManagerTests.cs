// <copyright file="DiscountManagerTests.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the discount manager tests class</summary>
// ReSharper disable AsyncConverter.AsyncMethodNamingHighlighting, ArgumentsStyleNamedExpression, ArgumentsStyleOther
// ReSharper disable ArgumentsStyleStringLiteral, RedundantAwait, UnusedMember.Global
namespace Clarity.Ecommerce.Providers.Discounts.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Mapper;
    using Models;
    using Moq;
    using Providers.Discounts;
    using Providers.Taxes.Basic;
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap.Pipeline;
#endif
    using Xunit;

    [Trait("Category", "Providers.Discounts")]
    public class DiscountManagerTests : XUnitLogHelper
    {
        public DiscountManagerTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            RegistryLoader.RootContainer.Configure(x => x.For<ILogger>().UseInstance(
                new ObjectInstance(new Logger
                {
                    ExtraLogger = s =>
                    {
                        try
                        {
                            TestOutputHelper.WriteLine(s);
                        }
                        catch
                        {
                            // Do nothing
                        }
                    },
                })));
            JSConfigs.CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            BasicTaxesProvider = new BasicTaxesProvider();
            DiscountManager = new DiscountManager();
        }

        private static IPricingFactoryContextModel DummyPricingFactoryContextModel => new PricingFactoryContextModel
        {
            AccountID = 1,
            CountryID = 1,
            AccountTypeID = 1,
            CurrencyID = 1,
            Quantity = 1,
            UserID = 1,
            StoreID = 1,
            SessionID = new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F47"),
            UserRoles = new List<string> { "CEF Global Administrator" },
        };

        private BasicTaxesProvider BasicTaxesProvider { get; }

        private DiscountManager DiscountManager { get; }

        private static MockingSetup GenMockingSetup() => new()
        {
            DoAccountTable = true,
            DoAddressTable = true,
            DoAppliedCartDiscountTable = true,
            DoAppliedCartItemDiscountTable = true,
            DoAppliedSalesOrderDiscountTable = true,
            DoAppliedSalesOrderItemDiscountTable = true,
            DoCartItemTable = true,
            DoCartItemTargetTable = true,
            DoCartStateTable = true,
            DoCartStatusTable = true,
            DoCartTable = true,
            DoCartTypeTable = true,
            DoContactTable = true,
            DoCountryTable = true,
            DoDiscountCodeTable = true,
            DoDiscountTable = true,
            DoDiscountUserTable = true,
            DoManufacturerProductTable = true,
            DoProductCategoryTable = true,
            DoProductImageTable = true,
            DoProductTable = true,
            DoProductTypeTable = true,
            DoRateQuoteTable = true,
            DoRegionTable = true,
            DoSalesItemTargetTypeTable = true,
            DoSalesOrderItemTable = true,
            DoSalesOrderTable = true,
            DoStoreProductTable = true,
            DoShipCarrierMethodTable = true,
            DoUserTable = true,
            DoVendorProductTable = true,
            DoFranchiseProductTable = true,
            DoBrandProductTable = true,
        };

        // TODO: CartItem discount that is already applied, but needs to update, like because the quantity of products changed
        // TODO: Shipping discount when no rate quote is selected
        // TODO: Shipping discount when selected rate quote is not applicable to the quote
        // TODO: Shipping discount when selected rate quote is already at zero
        // TODO: Buy X Get Y discount when the X and Y values are invalid
        // TODO: Exclusive but no other discounts, so addable (collection level)
        // TODO: Exclusive but no other discounts, so addable (item level)
        // TODO: Item level Usage limit check (positive and negative)

        [Fact]
        public Task Verify_AddDiscountByCode_WhichIsAlreadyAppliedToCart_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "ALREADY",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WhichIsAlreadyAppliedToCartItem_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "ALREADY-ITM",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithNoEffect_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "NO-EFFECT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithSingleMessage(result, "ERROR! Result would be no effect on final price.");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithAnExclusiveCodeButOtherDiscountsAlreadyAdded_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "EXCL-NOT-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! The requested discount can not be combined with other discounts on your cart.");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithAnExclusiveItemCodeButOtherDiscountsAlreadyAdded_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "EXCL-NOT-ADDABLE-ITM",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! The requested discount can not be combined with other discounts on your cart.");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithACodeThatDoesntExist_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                new MockingSetup { DoDiscountCodeTable = true },
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "DOESNT-EXIST",
                            cartID: 1,
                            pricingFactoryContext: new PricingFactoryContextModel(),
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithSingleMessage(result, "ERROR! No discount found for this code.");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithACartThatIsInvalid_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                new MockingSetup { DoDiscountCodeTable = true, DoCartTable = true, DoCartTypeTable = true, DoCartStatusTable = true, DoCartStateTable = true, },
                async (discountManager, _, contextProfileName) =>
                {
                    var pricingFactoryContext = DummyPricingFactoryContextModel;
                    pricingFactoryContext.SessionID = default;
                    pricingFactoryContext.UserID = null;
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "ABCD1234",
                            cartID: 1,
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithSingleMessage(result, "ERROR! Invalid Cart");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithACartThatIsEmpty_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    var pricingFactoryContext = DummyPricingFactoryContextModel;
                    pricingFactoryContext.SessionID = new Guid("771C8861-95BC-4437-9918-A779617DDBA2");
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "ABCD1234",
                            cartID: 1,
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithSingleMessage(result, "ERROR! Your cart is empty");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WhichHasExpired_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "DATE-NOT-ADDABLE-EXP",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        $"ERROR! This discount code expired on",
                        $"{DateTime.Today.AddDays(-4)}");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WhichHasntStarted_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "DATE-NOT-ADDABLE-FUT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        $"ERROR! This discount code doesn't become available until",
                        $"{DateTime.Today.AddDays(4)}");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WhichIsDeactivated_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "NOT-ADDABLE-DEACTIVATED",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithSingleMessage(result, "ERROR! No discount found for this code.");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithoutMeetingAuthed_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    var pricingFactoryContext = DummyPricingFactoryContextModel;
                    pricingFactoryContext.UserID = null;
                    pricingFactoryContext.AccountID = 0;
                    pricingFactoryContext.AccountTypeID = null;
                    pricingFactoryContext.CountryID = null;
                    pricingFactoryContext.CurrencyID = 1;
                    pricingFactoryContext.Quantity = 1;
                    pricingFactoryContext.StoreID = 1;
                    pricingFactoryContext.UserRoles = new List<string>();
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "NOT-ADDABLE-NO-AUTH",
                            cartID: 1,
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "InvalidByNotAuthed",
                        "InvalidByUser",
                        "InvalidByAccount",
                        "InvalidByAccountType",
                        "InvalidByUserRole",
                        "InvalidByCountry");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_WithoutMeetingUsageLimitByUsers_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "NOT-ADDABLE-USAGE-LIMIT-USER",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! This discount has been used the maximum number of times.");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithValidOrderData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-ORDER-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithValidShippingData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-SHIP-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithValidProductData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-PRODUCT-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithValidBuyXGetYData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-BXGY-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithValidOrderData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-ORDER-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithValidShippingData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-SHIP-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithValidProductData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-PRODUCT-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithValidBuyXGetYData_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-BXGY-ADDABLE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingOrderSubtotalGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-ORDER-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be greater than $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingOrderSubtotalGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-ORDER-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be greater than or equal to $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingOrderSubtotalLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-ORDER-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be less than $205.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingOrderSubtotalLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-ORDER-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be less than or equal to $204.99 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingShippingRateGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-SHIP-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be greater than $10,000.00 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingShippingRateGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-SHIP-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be greater than or equal to $10,000.00 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingShippingRateLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-SHIP-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be less than $8.00 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingShippingRateLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-SHIP-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be less than or equal to $7.99 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingProductSubtotalGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-PRODUCT-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be greater than $10,000.00 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingProductSubtotalGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-PRODUCT-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be greater than or equal to $10,000.00 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingProductSubtotalLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-PRODUCT-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be less than $8.00 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingProductSubtotalLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-PRODUCT-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be less than or equal to $7.99 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingBuyXGetYSubtotalGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-BXGY-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be greater than $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingBuyXGetYSubtotalGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-BXGY-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be greater than or equal to $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingBuyXGetYSubtotalLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-BXGY-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be less than $8.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Amt_WithoutMeetingBuyXGetYSubtotalLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "$-BXGY-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be less than or equal to $7.99 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingOrderSubtotalGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-ORDER-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be greater than $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingOrderSubtotalGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-ORDER-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be greater than or equal to $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingOrderSubtotalLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-ORDER-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be less than $205.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingOrderSubtotalLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-ORDER-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Order amount should be less than or equal to $204.99 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingShippingRateGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-SHIP-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be greater than $10,000.00 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingShippingRateGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-SHIP-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be greater than or equal to $10,000.00 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingShippingRateLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-SHIP-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be less than $8.00 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingShippingRateLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-SHIP-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Shipping amount should be less than or equal to $7.99 (actual: $15.95).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingProductSubtotalGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-PRODUCT-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be greater than $10,000.00 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingProductSubtotalGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-PRODUCT-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be greater than or equal to $10,000.00 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingProductSubtotalLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-PRODUCT-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be less than $8.00 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingProductSubtotalLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-PRODUCT-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! Product amount should be less than or equal to $7.99 (actual: $80.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingBuyXGetYSubtotalGreaterThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-BXGY-NOT-ADDABLE-GT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be greater than $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingBuyXGetYSubtotalGreaterThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-BXGY-NOT-ADDABLE-GTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be greater than or equal to $10,000.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingBuyXGetYSubtotalLessThanThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-BXGY-NOT-ADDABLE-LT",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be less than $8.00 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_AddDiscountByCode_Perc_WithoutMeetingBuyXGetYSubtotalLessThanOrEqualToThreshold_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.AddDiscountByCodeAsync(
                            code: "%-BXGY-NOT-ADDABLE-LTE",
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithMultipleMessages(
                        result,
                        "ERROR! You do not meet the requirements for this Discount.",
                        "ERROR! BuyXGetY amount should be less than or equal to $7.99 (actual: $205.00).");
                });
        }

        [Fact]
        public Task Verify_VerifyCurrentDiscounts_Passes()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                DiscountManager,
                GenMockingSetup(),
                async (discountManager, _, contextProfileName) =>
                {
                    // Act
                    var result = await discountManager.VerifyCurrentDiscountsAsync(
                            cartID: 1,
                            pricingFactoryContext: DummyPricingFactoryContextModel,
                            taxesProvider: BasicTaxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        [Fact]
        public Task Verify_LoadCartAndClearDiscountsIfEmpty_WithAnEmptyCart_Fails()
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                    DiscountManager,
                    GenMockingSetup(),
                    async (_, context, contextProfileName) =>
                    {
                        var cart = new Mock<ICartModel>();
                        cart.SetupAllProperties();
                        cart.Object.Discounts = context.AppliedCartDiscounts
                            .Select(x => ModelMapperForAppliedCartDiscount.MapAppliedCartDiscountModelFromEntityList(x, contextProfileName))
                            .ToList()!;
                        cart.Object.SalesItems = new List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
                        // Act
                        var result = await DiscountManager.LoadCartAndClearDiscountsIfEmptyAsync(
                                cart: cart.Object,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        // Assert
                        Verify_CEFAR_Failed_WithSingleMessage(
                            result,
                            "ERROR! Your cart is empty.");
                    });
        }

        /// <summary>Verify CEFAR failed with single message.</summary>
        /// <param name="result">       The result.</param>
        /// <param name="expectMessage">Message describing the expect.</param>
        private static void Verify_CEFAR_Failed_WithSingleMessage(CEFActionResponse result, string expectMessage)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Single(result.Messages);
            Assert.Equal(expectMessage, result.Messages[0]);
        }

        /// <summary>Verify CEFAR failed with multiple messages.</summary>
        /// <param name="result">        The result.</param>
        /// <param name="expectMessages">A variable-length parameters list containing expect messages.</param>
        private static void Verify_CEFAR_Failed_WithMultipleMessages(
            CEFActionResponse result,
            params string[] expectMessages)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Equal(expectMessages.Length, result.Messages.Count);
            var counter = 0;
            foreach (var expectMessage in expectMessages)
            {
                Assert.Equal(expectMessage, result.Messages[counter]);
                counter++;
            }
        }

        /// <summary>Verify CEFAR passed with no messages.</summary>
        /// <param name="result">The result.</param>
        private static void Verify_CEFAR_Passed_WithNoMessages(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(
                result.ActionSucceeded,
                result.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(result.Messages);
        }

        private static async Task RunWithSetupAndTearDownAsync(
            IDiscountManager discountManager,
            MockingSetup mockingSetup,
            Func<IDiscountManager, IClarityEcommerceEntities, string, Task> task,
            [CallerFilePath] string sourceFilePath = "",
            [CallerMemberName] string memberName = "")
        {
            var contextProfileName = $"{sourceFilePath}|{memberName}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await task(discountManager, mockingSetup.MockContext.Object, contextProfileName).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
