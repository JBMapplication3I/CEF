// <copyright file="CartWorkflowTests.Session.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Taxes.Basic;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.CartValidation;
    using Interfaces.Providers.Taxes;
    using Interfaces.Workflow;
    using Mapper;
    using Models;
    using Moq;
    using Providers.CartValidation;
    using StackExchange.Redis;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Shopping.Carts.Session")]
    public class Shopping_CartWorkflow_Session_Tests : XUnitLogHelper
    {
        public Shopping_CartWorkflow_Session_Tests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        public static async Task<ITaxesProviderBase> SetupWorkflowsAsync(
            string contextProfileName)
        {
            SetupProviderPluginPathsConfig();
            var taxesProvider = new BasicTaxesProvider();
            await taxesProvider.InitAsync(contextProfileName).ConfigureAwait(false);
            return taxesProvider;
        }

        [Fact]
        public async Task Verify_CheckExistsByTypeNameAndSessionID_WithValidData_Returns_AnInt()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_CheckExistsByTypeNameAndSessionID_WithValidData_Returns_AnInt";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoRateQuoteTable = true,
                    DoCartItemTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartID, _) = await workflow.CheckExistsByTypeNameAndSessionIDAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            new Guid("344016cd-4149-49e4-b4c0-fce3c621701d"),
                            "Cart",
                            1,
                            1),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cartID);
                Assert.True(cartID > 0);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Needs refactor after Account added")]
        public async Task Verify_AssignUserIDToCartIfNull_WithValidData_SavesTheChange()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_AssignUserIDToCartIfNull_WithValidData_SavesTheChange";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCategoryTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRateQuoteTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, taxesProvider, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F50"),
                            "Cart",
                            1, // Will assign this user id
                            1), // Will assign this account id
                        new PricingFactoryContextModel(),
                        taxesProvider,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cartResponse);
                Assert.NotNull(cartResponse.Result);
                Assert.Equal(1, cartResponse.Result!.UserID);
                Assert.Equal(1, cartResponse.Result.AccountID);
                mockingSetup.MockContext.Verify(x => x.SaveChangesAsync(), Times.AtLeastOnce);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SessionGet_WithInvalidData_Returns_AFailingCEFAR()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SessionGet_WithInvalidData_Returns_AFailingCEFAR";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoRateQuoteTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoProductTable = true,
                    DoProductCategoryTable = true,
                    DoCategoryTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, taxesProvider, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(default, "Cart", null, null),
                        new PricingFactoryContextModel(),
                        taxesProvider,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cartResponse);
                Assert.False(cartResponse.ActionSucceeded);
                // Assert.Equal("ERROR! No entity located", cartResponse.Messages[0]);
                // mockingSetup.MockContext.Verify(x => x.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SessionGet_WithNoSessionIDOrUserID_Returns_AFailingCEFAR()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SessionGet_WithNoSessionIDOrUserID_Returns_AFailingCEFAR";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTypeTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoRateQuoteTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoProductTable = true,
                    DoManufacturerProductTable = true,
                    DoVendorProductTable = true,
                    DoStoreProductTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(default, "Cart", null, null),
                        new PricingFactoryContextModel(),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cartResponse);
                Assert.False(cartResponse.ActionSucceeded);
                // Assert.Equal("ERROR! No entity located", cartResponse.Messages.Single());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SetShippingContact_WithValidData_Returns_APassingCEFActionResponse()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SetShippingContact_WithValidData_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoContactTable = true,
                    DoContactTypeTable = true,
                    DoAddressTable = true,
                    DoCountryTable = true,
                    DoRegionTable = true,
                    DoRegionImageTable = true,
                    DoRegionLanguageTable = true,
                    DoRegionCurrencyTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var timestamp = new DateTime(2023, 1, 1);
                var model = new ContactModel
                {
                    Active = true,
                    CreatedDate = timestamp,
                    Phone1 = "555-555-5555",
                    Email1 = "email@email.com",
                    FirstName = "John",
                    LastName = "Smith",
                    Address = new AddressModel
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        Company = "Some Address",
                        City = "Austin",
                        CountryID = 1,
                        RegionID = 43,
                        Street1 = "111 Fake Ln",
                    },
                };
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await workflow.SetShippingContactAsync(1, model, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SetQuantityBackOrderedForItem_WithValidData_SavesTheChanges()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SetQuantityBackOrderedForItem_WithValidData_SavesTheChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartItemTable = true,
                    DoRateQuoteTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                await workflow.SetQuantityBackOrderedForItemAsync(11, 5, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(x => x.SaveChangesAsync(), Times.AtLeastOnce);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Failing due to an unrelated issue. Will fix pending client funding.")]
        public async Task Verify_AddCartFee_WithValidData_SavesTheChanges()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_AddCartFee_WithValidData_SavesTheChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCategoryTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoDiscountTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSalesItemTargetTypeTable = true,
                    DoStoreProductTable = true,
                    DoStoreTable = true,
                    DoUserTable = true,
                    DoVendorProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result1 = await workflow.AddCartFeeAsync(
                        (await workflow.SessionGetAsync(
                                new SessionCartBySessionAndTypeLookupKey(new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F47"), "Cart", null, null),
                                new PricingFactoryContextModel(),
                                null,
                                contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse.Result!,
                        "$2.99",
                        contextProfileName)
                    .ConfigureAwait(false);
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F47"), "Cart", null, null),
                        new PricingFactoryContextModel(),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result1);
                Verify_CEFAR_Passed_WithNoMessages(cartResponse);
                Assert.True(cartResponse.ActionSucceeded);
                Assert.NotNull(cartResponse);
                Assert.NotNull(cartResponse.Result);
                Assert.NotNull(cartResponse.Result!.Totals);
                Assert.Equal(2.99m, cartResponse.Result.Totals.Fees);
                mockingSetup.MockContext.Verify(x => x.SaveChangesAsync(), Times.AtLeastOnce);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AddCartFee_WithNoCart_Returns_AFailingCEFAR()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_AddCartFee_WithNoCart_Returns_AFailingCEFAR";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartTypeTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoContactTable = true,
                    DoAddressTable = true,
                    DoRegionTable = true,
                    DoCountryTable = true,
                    DoRateQuoteTable = true,
                    DoNoteTable = true,
                    DoProductTable = true,
                    DoStoreTable = true,
                    DoUserTable = true,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoProductCategoryTable = true,
                    DoCategoryTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await workflow.AddCartFeeAsync(null!, "$2.99", contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.False(result.ActionSucceeded);
                Assert.Equal("ERROR! No cart provided", result.Messages.Single());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AddCartFee_WithNoFee_ThrowsAnInvalidOperationException()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_AddCartFee_WithNoFee_ThrowsAnInvalidOperationException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCategoryTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSalesItemTargetTypeTable = true,
                    DoStoreTable = true,
                    DoStoreProductTable = true,
                    DoUserTable = true,
                    DoBrandProductTable = true,
                    DoVendorProductTable = true,
                    DoFranchiseProductTable = true,
                    DoManufacturerProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(new Guid("344016cd-4149-49e4-b4c0-fce3c621701d"), "Cart", 1, 1),
                        new PricingFactoryContextModel(),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<InvalidOperationException>(
                        () => workflow.AddCartFeeAsync(cartResponse.Result!, null!, contextProfileName))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AddCartFee_WithAnUnparsableFee_Returns_APassingCEFAR()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_AddCartFee_WithAnUnparsableFee_Returns_APassingCEFAR";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCategoryTable = true,
                    DoContactTable = true,
                    DoContactImageTable = true,
                    DoContactImageTypeTable = true,
                    DoContactTypeTable = true,
                    DoCountryTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSalesItemTargetTypeTable = true,
                    DoShipCarrierMethodTable = true,
                    DoStoreTable = true,
                    DoStoreProductTable = true,
                    DoUserTable = true,
                    DoBrandProductTable = true,
                    DoVendorProductTable = true,
                    DoFranchiseProductTable = true,
                    DoManufacturerProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(new Guid("344016cd-4149-49e4-b4c0-fce3c621701d"), "Cart", 1, 1),
                        new PricingFactoryContextModel(),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Act
                var result = await RetryHelper.RetryOnExceptionAsync<RedisTimeoutException, CEFActionResponse<int>>(
                        () => workflow.AddCartFeeAsync(
                            cartResponse.Result!,
                            "abc",
                            contextProfileName))
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.True(result.ActionSucceeded);
                Assert.Empty(result.Messages);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SessionGetAsID_ReturnsAValidID()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SessionGetAsID_ReturnsAValidID";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartItemTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartID, _) = await workflow.SessionGetAsIDAsync(
                        new SessionCartBySessionAndTypeLookupKey(new Guid("055dca7c-ba8d-46fd-a92f-dac53e900056"), "Cart", 1, 1),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.Equal(3, cartID);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SessionGet_Returns_ACEFARWithPopulatedCartModel()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SessionGet_Returns_ACEFARWithPopulatedCartModel";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoDiscountTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSalesItemTargetTypeTable = true,
                    DoShipCarrierMethodTable = true,
                    DoStoreProductTable = true,
                    DoStoreTable = true,
                    DoUserTable = true,
                    DoBrandProductTable = true,
                    DoVendorProductTable = true,
                    DoFranchiseProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        lookupKey: new SessionCartBySessionAndTypeLookupKey(
                            sessionID: mockingSetup.MockContext.Object.Carts.First(x => x.SessionID.HasValue).SessionID!.Value,
                            typeKey: "Cart",
                            userID: 1,
                            accountID: 1),
                        pricingFactoryContext: new PricingFactoryContextModel(),
                        taxesProvider: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cartResponse);
                Assert.True(cartResponse.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SessionGet_With_AnUnmatchedSessionID_Returns_AFailingCEFAR()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_SessionGet_With_AnUnmatchedSessionID_Returns_AFailingCEFAR";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoDiscountTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoRateQuoteTable = true,
                    DoSalesItemTargetTypeTable = true,
                    DoShipCarrierMethodTable = true,
                    DoStoreProductTable = true,
                    DoStoreTable = true,
                    DoUserTable = true,
                    DoBrandProductTable = true,
                    DoVendorProductTable = true,
                    DoFranchiseProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cartResponse, _) = await workflow.SessionGetAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            new Guid("FFFFFFFF-FFFF-FFFF-FFFF-FFFFFFFFFFFF"),
                            "Cart",
                            1,
                            1),
                        new PricingFactoryContextModel(),
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cartResponse);
                // Assert.False(cartResponse.ActionSucceeded);
                // Assert.Equal("ERROR! No entity located", cartResponse.Messages[0]);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "FedEx doesn't return rates properly for this setup. To fix")]
        public async Task Verify_GetRateQuotes_ByCartID_Returns_ACEFARWithAListOfRateQuotes()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_GetRateQuotes_ByCartID_Returns_ACEFARWithAListOfRateQuotes";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSettingTable = true,
                    DoShipCarrierMethodTable = true,
                    DoShipCarrierTable = true,
                    DoStoreProductTable = true,
                    DoUserTable = true,
                    DoVendorProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var cart = mockingSetup.MockContext.Object.Carts.Single(x => x.ID == 1);
                cart.ShippingContactID = 1;
                cart.ShippingContact = mockingSetup.MockContext.Object.Contacts.Single(x => x.ID == 1);
                // Act
                var response = await workflow.GetRateQuotesAsync(
                        new CartByIDLookupKey(3),
                        new ContactModel
                        {
                            Active = true,
                            Address = new AddressModel
                            {
                                Active = true,
                                Street1 = "6805 N Capital of Texas Hwy",
                                Street2 = "Suite 312",
                                City = "Austin",
                                RegionID = mockingSetup.MockContext.Object.Regions.First(x => x.Name == "Texas").ID,
                                CountryID = mockingSetup.MockContext.Object.Regions.First(x => x.Name == "Texas").CountryID,
                                PostalCode = "78729"
                            },
                        },
                        false,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(response);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("Unknown Error").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.NotEmpty(response.Result!);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "FedEx doesn't return rates properly for this setup. To fix")]
        public async Task Verify_GetRateQuotes_BySessionIDAndType_Returns_ACEFARWithAListOfRateQuotes()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_GetRateQuotes_BySessionIDAndType_Returns_ACEFARWithAListOfRateQuotes";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAccountTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoPackageTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoSettingTable = true,
                    DoShipCarrierMethodTable = true,
                    DoShipCarrierTable = true,
                    DoStoreProductTable = true,
                    DoUserTable = true,
                    DoVendorProductTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var cart = mockingSetup.MockContext.Object.Carts.Single(x => x.ID == 1);
                cart.ShippingContactID = 1;
                cart.ShippingContact = mockingSetup.MockContext.Object.Contacts.Single(x => x.ID == 1);
                // Act
                var response = await workflow.GetRateQuotesAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            mockingSetup.MockContext.Object.Carts.First(x => x.ID == 3).SessionID!.Value,
                            "Cart"),
                        new ContactModel
                        {
                            Active = true,
                            Address = new AddressModel
                            {
                                Active = true,
                                Street1 = "6805 N Capital of Texas Hwy",
                                Street2 = "Suite 312",
                                City = "Austin",
                                RegionID = mockingSetup.MockContext.Object.Regions.First(x => x.Name == "Texas").ID,
                                CountryID = mockingSetup.MockContext.Object.Regions.First(x => x.Name == "Texas").CountryID,
                                PostalCode = "78729"
                            },
                        },
                        false,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(response);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("Unknown Error").Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.NotEmpty(response.Result!);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_ApplyRateQuoteToCart_ByCartID_Returns_ACEFARWithAListOfRateQuotes()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_ApplyRateQuoteToCart_ByCartID_Returns_ACEFARWithAListOfRateQuotes";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoPackageTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                    DoSettingTable = true,
                    DoShipCarrierTable = true,
                    DoShipCarrierMethodTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var cart = mockingSetup.MockContext.Object.Carts.Single(x => x.ID == 1);
                cart.ShippingContactID = 1;
                cart.ShippingContact = mockingSetup.MockContext.Object.Contacts.Single(x => x.ID == 1);
                // Act
                var response = await workflow.ApplyRateQuoteToCartAsync(
                        new CartByIDLookupKey(1),
                        1,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(response);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("Unknown Error").Aggregate((c, n) => $"{c}\r\n{n}"));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_ApplyRateQuoteToCart_ByCartID_WithANonExistingID_Returns_AFailingCEFAR()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_ApplyRateQuoteToCart_ByCartID_WithANonExistingID_Returns_AFailingCEFAR";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoPackageTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                    DoSettingTable = true,
                    DoShipCarrierTable = true,
                    DoShipCarrierMethodTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var cart = mockingSetup.MockContext.Object.Carts.Single(x => x.ID == 1);
                cart.ShippingContactID = 1;
                cart.ShippingContact = mockingSetup.MockContext.Object.Contacts.Single(x => x.ID == 1);
                // Act
                var response = await workflow.ApplyRateQuoteToCartAsync(
                        new CartByIDLookupKey(int.MaxValue - 1),
                        1,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(response);
                Assert.False(response.ActionSucceeded);
                Assert.Equal(
                    "WARNING! No Cart",
                    response.Messages.DefaultIfEmpty("Unknown Error").Aggregate((c, n) => $"{c}\r\n{n}"));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_ApplyRateQuoteToCart_BySessionIDAndType_Returns_ACEFARWithAListOfRateQuotes()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CartWorkflowTests|Verify_ApplyRateQuoteToCart_BySessionIDAndType_Returns_ACEFARWithAListOfRateQuotes";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoContactTable = true,
                    DoCountryTable = true,
                    DoManufacturerProductTable = true,
                    DoNoteTable = true,
                    DoPackageTable = true,
                    DoProductCategoryTable = true,
                    DoProductPricePointTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                    DoRegionTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                    DoSettingTable = true,
                    DoShipCarrierTable = true,
                    DoShipCarrierMethodTable = true,
                };
                await SetupContainerAsync(mockingSetup, childContainer, contextProfileName).ConfigureAwait(false);
                var (workflow, _, _) = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var cart = mockingSetup.MockContext.Object.Carts.Single(x => x.ID == 1);
                cart.ShippingContactID = 1;
                cart.ShippingContact = mockingSetup.MockContext.Object.Contacts.Single(x => x.ID == 1);
                // Act
                var response = await workflow.ApplyRateQuoteToCartAsync(
                        new SessionCartBySessionAndTypeLookupKey(
                            mockingSetup.MockContext.Object.Carts.First().SessionID!.Value,
                            "Cart",
                            null,
                            null),
                        1,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(response);
                Assert.True(
                    response.ActionSucceeded,
                    response.Messages.DefaultIfEmpty("Unknown Error").Aggregate((c, n) => $"{c}\r\n{n}"));
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        /// <summary>Verify CEFAR failed with single message.</summary>
        /// <param name="result">       The result.</param>
        /// <param name="expectMessage">Message describing the expect.</param>
        protected static void Verify_CEFAR_Failed_WithSingleMessage(CEFActionResponse result, string expectMessage)
        {
            Assert.NotNull(result);
            Assert.False(result.ActionSucceeded);
            Assert.Single(result.Messages);
            Assert.Equal(expectMessage, result.Messages[0]);
        }

        /// <summary>Verify CEFAR failed with multiple messages.</summary>
        /// <param name="result">        The result.</param>
        /// <param name="expectMessages">A variable-length parameters list containing expect messages.</param>
        protected static void Verify_CEFAR_Failed_WithMultipleMessages(
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
        protected static void Verify_CEFAR_Passed_WithNoMessages(CEFActionResponse result)
        {
            Assert.NotNull(result);
            Assert.True(
                result.ActionSucceeded,
                result.Messages.DefaultIfEmpty("No Messages").Aggregate((c, n) => c + "\r\n" + n));
            Assert.Empty(result.Messages);
        }

        private static void SetupProviderPluginPathsConfig()
        {
            ProviderTestHelper.SetupProviderPluginPathsConfig(
                new List<ProviderAssemblyRef> { new ProviderAssemblyRef("Pricing", "Flat") });
        }

        private static async Task<(ICartWorkflow, ITaxesProviderBase taxesProvider, ICartValidator cartValidator)> GenerateWorkflowAsync(string contextProfileName)
        {
            var taxesProvider = await SetupWorkflowsAsync(contextProfileName).ConfigureAwait(false);
            return (new CartWorkflow(), taxesProvider, new CartValidator());
        }

        private static async Task SetupContainerAsync(
            MockingSetup mockingSetup,
            StructureMap.IContainer childContainer,
            string contextProfileName)
        {
            await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
            childContainer.Configure(x =>
            {
                x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
            });
            RegistryLoader.OverrideContainer(childContainer, contextProfileName);
        }
    }
}
