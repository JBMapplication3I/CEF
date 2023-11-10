// <copyright file="CartWorkflowTests.Static.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Static cart workflow tests class</summary>
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Taxes.Basic;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JSConfigs;
    using Mapper;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Shopping.Carts.Admin")]
    public class Shopping_CartWorkflow_Admin_Tests : XUnitLogHelper
    {
        public Shopping_CartWorkflow_Admin_Tests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private static async Task<ICartWorkflow> GenerateWorkflowAsync(string contextProfileName)
        {
            await Shopping_CartWorkflow_Session_Tests.SetupWorkflowsAsync(contextProfileName).ConfigureAwait(false);
            return new CartWorkflow();
        }

        #region AdminGet
        [Fact]
        public async Task Verify_AdminGet_WithAValidIDPricingFactoryContextTaxProviderAndUserID_Returns_ASessionMappedCartModel()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminGet_WithAValidIDPricingFactoryContextTaxProviderAndUserID_Returns_ASessionMappedCartModel";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCategoryTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductTable = true,
                    DoProductImageTable = true,
                    DoRateQuoteTable = true,
                    DoBrandProductTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                    DoFranchiseProductTable = true,
                    DoManufacturerProductTable = true,
                };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false))
                    .AdminGetAsync(
                        lookupKey: new CartByIDLookupKey(1, 1, 1),
                        pricingFactoryContext: new PricingFactoryContextModel(),
                        taxesProvider: new BasicTaxesProvider(),
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminGet_WithAValidIDPricingFactoryContextTaxProvider_But_NoUserID_Should_ThrowAnArgumentNullException()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminGet_WithAValidIDPricingFactoryContextTaxProvider_But_NoUserID_Returns_ASessionMappedCartModel";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoCartContactTable = true,
                    DoCategoryTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductTable = true,
                    DoRateQuoteTable = true,
                };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentNullException>(
                        async () => await workflow.AdminGetAsync(
                                new CartByIDLookupKey(1),
                                new PricingFactoryContextModel(),
                                new BasicTaxesProvider(),
                                contextProfileName)
                            .ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminGet_WithAnInvalidID_Throws_AnInvalidOperationException()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminGet_WithAnInvalidID_Throws_AnInvalidOperationException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var id in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                        () => workflow.AdminGetAsync(
                            lookupKey: new CartByIDLookupKey(id, null),
                            pricingFactoryContext: new PricingFactoryContextModel(),
                            taxesProvider: new BasicTaxesProvider(),
                            contextProfileName: contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminGet_WithAnInvalidPricingFactoryContext_Throws_AnArgumentNullException()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminGet_WithAnInvalidPricingFactoryContext_Throws_AnArgumentNullException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).AdminGetAsync(new CartByIDLookupKey(1, null), null!, new BasicTaxesProvider(), contextProfileName).ConfigureAwait(false)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminGet_WithAnInvalidTaxProvider_Throws_AnArgumentNullException()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminGet_WithAnInvalidTaxProvider_Throws_AnArgumentNullException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).AdminGetAsync(new CartByIDLookupKey(1, null), new PricingFactoryContextModel(), null, contextProfileName).ConfigureAwait(false)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region AdminUpdate
        [Fact]
        public async Task Verify_AdminUpdate_WithAValidCartModel_Returns_APassingCEFActionResponse()
        {
            // Arrange
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminUpdate_WithAValidCartModel_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAccountStatusTable = true,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoAttributeTypeTable = true,
                    DoCartContactTable = true,
                    DoCartItemTable = true,
                    DoCartItemTargetTable = true,
                    DoCartStateTable = true,
                    DoCartStatusTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCategoryTable = true,
                    DoContactTable = true,
                    DoContactTypeTable = true,
                    DoCountryTable = true,
                    DoDiscountTable = true,
                    DoFavoriteCategoryTable = true,
                    DoFavoriteManufacturerTable = true,
                    DoFavoriteStoreTable = true,
                    DoFavoriteVendorTable = true,
                    DoGeneralAttributeTable = true,
                    DoNoteTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoProductTable = true,
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
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var model = await workflow.AdminGetAsync(
                        new CartByIDLookupKey(1, 1, 1),
                        new PricingFactoryContextModel(),
                        new BasicTaxesProvider(),
                        contextProfileName)
                    .ConfigureAwait(false);
                model!.SubtotalDiscountsModifier = 0.10m;
                model.SubtotalDiscountsModifierMode = (int)Enums.TotalsModifierModes.PercentMarkup;
                // Act
                var result = await workflow.AdminUpdateAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminUpdate_WithACartIDNotInTheData_Returns_AFailingCEFActionResponse()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminUpdate_WithACartIDNotInTheData_Returns_AFailingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoCartTable = true };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var id in new[] { 500, 1000, 10000, 80000 })
                {
                    // Act/Assert
                    Assert.False((await workflow.AdminUpdateAsync(new CartModel { ID = id }, contextProfileName).ConfigureAwait(false)).ActionSucceeded);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminUpdate_WithDataThatCannotBeSaved_Returns_AFailingCEFActionResponse()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminUpdate_WithDataThatCannotBeSaved_Returns_AFailingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { SaveChangesResult = -1, DoCartTable = true, };
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var id in new[] { 500, 1000, 10000, 80000 })
                {
                    // Act/Assert
                    Assert.False((await workflow.AdminUpdateAsync(new CartModel { ID = id }, contextProfileName).ConfigureAwait(false)).ActionSucceeded);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminUpdate_WithAnInvalidCartModel_Throws_AnArgumentNullException()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminUpdate_WithAnInvalidCartModel_Throws_AnArgumentNullException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentNullException>(
                        async () => await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false))
                            .AdminUpdateAsync(null!, contextProfileName)
                            .ConfigureAwait(false))
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_AdminUpdate_WithAnInvalidCartID_Throws_AnArgumentNullException()
        {
            // Arrange
            CEFConfigDictionary.Load();
            const string contextProfileName = "AdminCartWorkflowTests|Verify_AdminUpdate_WithAnInvalidCartID_Throws_AnArgumentNullException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>()
                        // ReSharper disable once AsyncConverter.AsyncWait
                        .Use(
                            () => mockingSetup.MockContext == null
                                || mockingSetup.MockContext.Object == null
                                || !mockingSetup.SetupComplete
                                    ? mockingSetup.DoMockingSetupForContextAsync(contextProfileName).Result.Object
                                    : mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act/Assert
                foreach (var id in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    await Assert.ThrowsAsync<ArgumentNullException>(() => workflow.AdminUpdateAsync(new CartModel { ID = id }, contextProfileName)).ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion
    }
}
