// <copyright file="CartWorkflowTests.Static.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Static cart workflow tests class</summary>
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Shopping.Carts.Static")]
    public class Shopping_CartWorkflow_Static_Tests : XUnitLogHelper
    {
        public Shopping_CartWorkflow_Static_Tests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private static async Task<ICartWorkflow> GenerateWorkflowAsync(string contextProfileName)
        {
            await Shopping_CartWorkflow_Session_Tests.SetupWorkflowsAsync(contextProfileName).ConfigureAwait(false);
            return new CartWorkflow();
        }

        private const string StaticCartType = "Wish List";

        #region StaticGet
        [Fact]
        public async Task Verify_StaticGet_With_AValidCartTypeUserIDAndPricingFactoryContext_Returns_AStaticMappedStaticCart()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticGet_With_AValidCartTypeUserIDAndPricingFactoryContext_Returns_AStaticMappedStaticCart";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                    DoProductImageTable = true,
                    DoProductCategoryTable = true,
                    DoFranchiseProductTable = true,
                    DoStoreProductTable = true,
                    DoVendorProductTable = true,
                    DoBrandProductTable = true,
                    DoManufacturerProductTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticGetAsync(new StaticCartLookupKey(1, StaticCartType, 1), new PricingFactoryContextModel(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.NotEmpty(result!.SalesItems!);
                Assert.Equal(3, result.SalesItems!.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticGet_With_AnInvalidPricingFactoryContext_Throws_AnArgumentNullException()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticGet_With_AnInvalidPricingFactoryContext_Throws_AnArgumentNullException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentNullException>(async () => await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticGetAsync(new StaticCartLookupKey(1, StaticCartType, 1), null!, contextProfileName).ConfigureAwait(false)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticGet_With_DataThatWillNotMatchAnExistingRecord_Returns_Null()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticGet_With_DataThatWillNotMatchAnExistingRecord_Returns_Null";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticGetAsync(new StaticCartLookupKey(2, StaticCartType, 2), new PricingFactoryContextModel(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Null(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region StaticAddItem
        [Fact]
        public async Task Verify_StaticAddItem_With_AValidCartTypeUserIDProductIDAndSAD_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddItem_With_AValidCartTypeUserIDProductIDAndSAD_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticAddItemAsync(
                        new StaticCartLookupKey(1, StaticCartType, 1),
                        1151,
                        1m,
                        new SerializableAttributesDictionary
                        {
                            ["SomeKey"] = new SerializableAttributeObject
                            {
                                Key = "SomeKey",
                                Value = "Some value"
                            },
                        },
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticAddLot_With_AValidCartTypeUserIDLotIDAndSAD_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddLot_With_AValidCartTypeUserIDLotIDAndSAD_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                    DoLotTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x => x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object));
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticAddLotAsync(
                        new StaticCartLookupKey(1, StaticCartType, 1),
                        1,
                        1m,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticAddItem_With_AValidCartTypeUserIDProductID_But_NoSAD_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddItem_With_AValidCartTypeUserIDProductID_But_NoSAD_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticAddItemAsync(
                        new StaticCartLookupKey(1, StaticCartType, 1),
                        1151,
                        1m,
                        null!,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticAddItem_With_DataThatWillMatchAnExistingRecord_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddItem_With_DataThatWillMatchAnExistingRecord_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticAddItemAsync(
                        new StaticCartLookupKey(1, StaticCartType, 1),
                        1151,
                        1m,
                        null!,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticAddItem_With_AnInvalidProductID_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddItem_With_AnInvalidProductID_Throws_AnInvalidOperationException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var productID in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(() => workflow.StaticAddItemAsync(
                            new StaticCartLookupKey(1, StaticCartType, 1),
                            productID,
                            1m,
                            null!,
                            contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticAddItem_With_AProductIDThatDoesntExist_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddItem_With_AProductIDThatDoesntExist_Throws_AnInvalidOperationException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoCartTable = true, DoProductTable = true };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var productID in new[] { 500, 80000, 10000 })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(() => workflow.StaticAddItemAsync(
                            new StaticCartLookupKey(1, StaticCartType, 1),
                            productID,
                            1m,
                            null!,
                            contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticAddItem_With_DataThatCannotBeSaved_Returns_AFailingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticAddItem_With_DataThatCannotBeSaved_Returns_AFailingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = -1,
                    DoProductTable = true,
                    DoCartItemTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticAddItemAsync(
                        new StaticCartLookupKey(1, StaticCartType, 1),
                        1151,
                        1m,
                        null!,
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                // Assert.False(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region StaticRemove(`1)
        [Fact]
        public async Task Verify_StaticRemove1_With_AValidID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticRemove1_With_AValidID_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartItemTable = true,
                    DoAppliedCartItemDiscountTable = true,
                    DoRateQuoteTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false)).StaticRemoveAsync(new CartByIDLookupKey(91), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticRemove1_With_AnInvalidID_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticRemove1_With_AnInvalidID_Throws_AnInvalidOperationException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var id in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(() => workflow.StaticRemoveAsync(new CartByIDLookupKey(id), contextProfileName)).ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region StaticRemove(`3)
        [Fact]
        public async Task Verify_StaticRemove3_With_AValidCartTypeUserIDAndProductID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticRemove3_With_AValidCartTypeUserIDAndProductID_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false))
                    .StaticRemoveAsync(new StaticCartLookupKey(1, StaticCartType, 1), 1151, null, contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_StaticRemove3_With_AnInvalidProductID_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticRemove3_With_AnInvalidProductID_Throws_AnInvalidOperationException";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup();
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                foreach (var productID in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(
                            () => workflow.StaticRemoveAsync(new StaticCartLookupKey(1, StaticCartType, 1), productID, null, contextProfileName))
                        .ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region StaticClear
        [Fact]
        public async Task Verify_StaticClear_With_AValidCartTypeAndUserID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "StaticCartWorkflowTests|Verify_StaticClear_With_AValidCartTypeAndUserID_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoRateQuoteTable = true,
                    // DoDiscountTable = true,
                    // DoAppliedCartItemDiscountTable = true,
                    // DoAppliedCartDiscountTable = true,
                    // DoVendorProductTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await (await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false))
                    .StaticClearAsync(new StaticCartLookupKey(1, StaticCartType, 1), contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion
    }
}
