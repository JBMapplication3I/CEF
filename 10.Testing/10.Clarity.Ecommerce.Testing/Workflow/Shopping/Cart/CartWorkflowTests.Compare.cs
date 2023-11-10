// <copyright file="CartWorkflowTests.Compare.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the compare cart workflow tests class</summary>
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Mapper;
    using Models;
    using StackExchange.Redis;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Shopping.Carts.Compare")]
    public class Shopping_CartWorkflow_Compare_Tests : XUnitLogHelper
    {
        public Shopping_CartWorkflow_Compare_Tests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [NotNull]
        private static Task GenerateWorkflowAsync(string contextProfileName)
        {
            return Shopping_CartWorkflow_Session_Tests.SetupWorkflowsAsync(contextProfileName);
        }

        /*
         * It was discussed that the following workflow will be built to resolve this issue:
         * 1. An anon user will add to compare cart, they will have a session id to tie the cart together within their
         *    local session
         * 2. The anon user logs into a specific account (or registers, same effect) and the cart will be tied to the
         *    user
         *    a. If the local compare cart has items and is newer than any other compare cart add associated from
         *       another device/session (e.g.- Phone vs Desktop or Firefox vs Chrome) then all older compare carts will
         *       be deactivated and this one will override and become the current compare cart
         *    b. If the local compare cart is empty, then any existing non-empty compare cart will override
         *       i. If there are none to override, just keep the existing empty one
         * The endpoints for Static Carts will be modified to allow this compare cart type through when anon (but will
         * still require auth/throw 401 for other static cart types such as "Wish List").
         */

        private static Guid CompareCartSessionID { get; } = new Guid("AF22524E-9F70-48BF-9A5E-5A2449BA9F48");

        #region CompareGet
        [Fact]
        public async Task Verify_CompareGet_With_AValidSessionIDUserIDAndPricingFactoryContext_Returns_AStaticMappedCompareCart()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareGet_With_AValidSessionIDUserIDAndPricingFactoryContext_Returns_AStaticMappedCompareCart";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                    DoProductCategoryTable = true,
                    DoProductImageTable = true,
                    DoStoreProductTable = true,
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cart, _) = await new CartWorkflow().CompareGetAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1), new PricingFactoryContextModel(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(cart);
                Assert.NotEmpty(cart!.SalesItems!);
                Assert.Equal(3, cart.SalesItems!.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareGet_With_AValidSessionIDAndPricingFactoryContext_But_NoUserID_Returns_AStaticMappedCompareCart()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareGet_With_AValidSessionIDAndPricingFactoryContext_But_NoUserID_Returns_AStaticMappedCompareCart";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoCartItemTable = true,
                    DoProductTable = true,
                    DoProductCategoryTable = true,
                    DoStoreProductTable = true,
                    DoProductImageTable = true,
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
                var cartWorkflow = new CartWorkflow();
                // Act
                var (cart, _) = await RetryHelper.RetryOnExceptionAsync<RedisTimeoutException, (ICartModel? cart, Guid? updatedSessionID)>(
                        () => cartWorkflow.CompareGetAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, null, null), new PricingFactoryContextModel(), contextProfileName))
                    .ConfigureAwait(false);
                // Assert
                Assert.NotNull(cart);
                Assert.NotEmpty(cart!.SalesItems!);
                Assert.Equal(3, cart.SalesItems!.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareGet_With_AnInvalidPricingFactoryContext_Throws_AnArgumentNullException()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareGet_With_AnInvalidPricingFactoryContext_Throws_AnArgumentNullException";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act/Assert
                await Assert.ThrowsAsync<ArgumentNullException>(() => new CartWorkflow().CompareGetAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, null, null), null, contextProfileName)).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareGet_With_DataThatWillNotMatchAnExistingRecord_Returns_Null()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareGet_With_DataThatWillNotMatchAnExistingRecord_Returns_Null";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var (cart, _) = await new CartWorkflow().CompareGetAsync(new CompareCartBySessionLookupKey(Guid.NewGuid(), 2, 2), new PricingFactoryContextModel(), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Null(cart);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region CompareAddItem
        [Fact]
        public async Task Verify_CompareAddItem_With_AValidSessionIDUserIDProductIDAndSAD_Returns_APassingCEFActionResponse()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_AValidSessionIDUserIDProductIDAndSAD_Returns_APassingCEFActionResponse";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareAddItemAsync(
                        new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1),
                        969,
                        new SerializableAttributesDictionary
                        {
                            ["Size"] = new SerializableAttributeObject
                            {
                                Key = "Size",
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
        public async Task Verify_CompareAddItem_With_AValidSessionIDUserIDProductIDAndSAD_But_NoUserID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            BaseModelMapper.Initialize();
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_AValidSessionIDUserIDProductIDAndSAD_But_NoUserID_Returns_APassingCEFActionResponse";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareAddItemAsync(
                        new CompareCartBySessionLookupKey(CompareCartSessionID, null, null),
                        969,
                        new SerializableAttributesDictionary
                        {
                            ["Size"] = new SerializableAttributeObject
                            {
                                Key = "Size",
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
        public async Task Verify_CompareAddItem_With_AValidSessionIDUserIDProductID_But_NoSAD_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_AValidSessionIDUserIDProductID_But_NoSAD_Returns_APassingCEFActionResponse";
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
                    DoGeneralAttributeTable = true,
                    DoProductTable = true,
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareAddItemAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1), 969, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareAddItem_With_AValidSessionIDProductID_But_NoSADNoUserID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_AValidSessionIDProductID_But_NoSADNoUserID_Returns_APassingCEFActionResponse";
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
                    DoGeneralAttributeTable = true,
                    DoProductTable = true,
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareAddItemAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, null, null), 969, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareAddItem_With_DataThatWillMatchAnExistingRecord_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_DataThatWillMatchAnExistingRecord_Returns_APassingCEFActionResponse";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareAddItemAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1), 1151, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareAddItem_With_AnInvalidProductID_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_AnInvalidProductID_Throws_AnInvalidOperationException";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new CartWorkflow();
                foreach (var productID in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(() => workflow.CompareAddItemAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, null, null), productID, null, contextProfileName)).ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareAddItem_With_AProductIDThatDoesntExist_Throws_AnArgumentNullException()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_AProductIDThatDoesntExist_Throws_AnArgumentNullException";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new CartWorkflow();
                // Act
                foreach (var productID in new[] { 500, 80000, 10000 })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<ArgumentNullException>(() => workflow.CompareAddItemAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, null, null), productID, null, contextProfileName)).ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareAddItem_With_DataThatCannotBeSaved_Returns_AFailingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareAddItem_With_DataThatCannotBeSaved_Returns_AFailingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = -1,
                    DoCartItemTable = true,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoCartStatusTable = true,
                    DoCartStateTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareAddItemAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1), 969, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.False(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region CompareRemove(`1)
        [Fact]
        public async Task Verify_CompareRemove1_With_AValidID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareRemove1_With_AValidID_Returns_APassingCEFActionResponse";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartItemTable = true,
                    DoCartTable = true,
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareRemoveAsync(91, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareRemove1_With_AnInvalidID_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareRemove1_With_AnInvalidID_Throws_AnInvalidOperationException";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new CartWorkflow();
                foreach (var id in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(() => workflow.CompareRemoveAsync(id, contextProfileName)).ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region CompareRemove(`3)
        [Fact]
        public async Task Verify_CompareRemove3_With_AValidSessionIDUserIDAndProductID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareRemove3_With_AValidSessionIDUserIDAndProductID_Returns_APassingCEFActionResponse";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareRemoveAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1), 1151, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CompareRemove3_With_AnInvalidProductID_Throws_AnInvalidOperationException()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareRemove3_With_AnInvalidProductID_Throws_AnInvalidOperationException";
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
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                var workflow = new CartWorkflow();
                foreach (var productID in new[] { -10, -1, 0, int.MinValue, int.MaxValue })
                {
                    // Act/Assert
                    await Assert.ThrowsAsync<InvalidOperationException>(() => workflow.CompareRemoveAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, null, null), productID, contextProfileName)).ConfigureAwait(false);
                }
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region CompareClear
        [Fact]
        public async Task Verify_CompareClear_With_AValidSessionIDAndUserID_Returns_APassingCEFActionResponse()
        {
            // Arrange
            const string contextProfileName = "CompareCartWorkflowTests|Verify_CompareClear_With_AValidSessionIDAndUserID_Returns_APassingCEFActionResponse";
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
                    DoAppliedCartItemDiscountTable = true,
                    DoRateQuoteTable = true,
                    DoAppliedCartDiscountTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await GenerateWorkflowAsync(contextProfileName).ConfigureAwait(false);
                // Act
                var result = await new CartWorkflow().CompareClearAsync(new CompareCartBySessionLookupKey(CompareCartSessionID, 1, 1), contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.True(result.ActionSucceeded);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion
    }
}
