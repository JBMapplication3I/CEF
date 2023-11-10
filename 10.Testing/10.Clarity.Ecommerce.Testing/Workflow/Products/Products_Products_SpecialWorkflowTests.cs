// <copyright file="ProductWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the products workflow tests class</summary>
// ReSharper disable InconsistentNaming, MemberCanBePrivate.Global, ObjectCreationAsStatement
// ReSharper disable ReturnValueOfPureMethodIsNotUsed, UnusedMember.Global
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Moq;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Products.Products.Special")]
    public class Products_Products_SpecialWorkflowTests : Products_Products_WorkflowTestsBase
    {
        public Products_Products_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private static readonly IPricingFactoryContextModel DefaultPriceFactoryContext = new PricingFactoryContextModel { PricePoint = "WEB", Quantity = 1 };

        #region Read
        protected override async Task Verify_Get_ByID_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_ByID_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await new ProductWorkflow().GetAsync(
                        id: 1151,
                        contextProfileName: contextProfileName,
                        isVendorAdmin: false,
                        vendorAdminID: null,
                        previewID: null)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductModel>(result);
                Assert.NotNull(result);
                Assert.Equal(1151, result!.ID);
                Assert.Equal("0200-ANGBT-BFUT-IA", result.CustomKey);
                Assert.Equal("200ml BF ANG BT", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        protected override async Task Verify_Get_ByKey_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_ByKey_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                var result = await new ProductWorkflow().GetAsync(
                        "0200-ANGBT-BFUT-IA",
                        contextProfileName,
                        false,
                        null)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductModel>(result);
                Assert.NotNull(result);
                Assert.Equal(1151, result!.ID);
                Assert.Equal("0200-ANGBT-BFUT-IA", result.CustomKey);
                Assert.Equal("200ml BF ANG BT", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_AVariantMaster_ByKey_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_AVariantMaster_ByKey_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetAsync(
                        "VariantMaster",
                        contextProfileName,
                        false,
                        null)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductModel>(result);
                Assert.NotNull(result);
                Assert.Equal(400000, result!.ID);
                Assert.Equal("VariantMaster", result.CustomKey);
                Assert.Equal("A Variant Master", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_AKitMaster_ByKey_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_AKitMaster_ByKey_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetAsync(
                        "KitMaster",
                        contextProfileName,
                        false,
                        null)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductModel>(result);
                Assert.NotNull(result);
                Assert.Equal(400003, result!.ID);
                Assert.Equal("KitMaster", result.CustomKey);
                Assert.Equal("A Kit Master", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_ByKey_WithAnIDNotInTheData_Should_ReturnNull()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_ByKey_WithAnIDNotInTheData_Should_ReturnNull";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetAsync(
                        key: "NOT THERE",
                        contextProfileName: contextProfileName,
                        isVendorAdmin: false,
                        vendorAdminID: null)
                    .ConfigureAwait(false);
                // Assert
                Assert.Null(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_BySEOURL_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_BySEOURL_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetProductBySeoUrlAsync(
                        seoUrl: "200ml-BF-ANG-BT",
                        contextProfileName: contextProfileName,
                        isVendorAdmin: false,
                        vendorAdminID: null)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductModel>(result);
                Assert.NotNull(result);
                Assert.Equal(1151, result!.ID);
                Assert.Equal("0200-ANGBT-BFUT-IA", result.CustomKey);
                Assert.Equal("200ml BF ANG BT", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_BySEOURL_WithAnIDNotInTheData_Should_ReturnNull()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_BySEOURL_WithAnIDNotInTheData_Should_ReturnNull";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true };
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
                var result = await new ProductWorkflow().GetProductBySeoUrlAsync(
                        "NOT-THERE",
                        contextProfileName,
                        false,
                        null)
                    .ConfigureAwait(false);
                // Assert
                Assert.Null(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetFull_ByID_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetFull_ByID_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetFullAsync(1151, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<ProductModel>(result);
                Assert.Equal(1151, result!.ID);
                Assert.Equal("0200-ANGBT-BFUT-IA", result.CustomKey);
                Assert.Equal("200ml BF ANG BT", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetFull_ByKey_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetFull_ByKey_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetFullAsync("0200-ANGBT-BFUT-IA", contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<ProductModel>(result);
                Assert.Equal(1151, result!.ID);
                Assert.Equal("0200-ANGBT-BFUT-IA", result.CustomKey);
                Assert.Equal("200ml BF ANG BT", result.Name);
                Assert.True(result.Active);
                Assert.Equal(new DateTime(2023, 1, 1), result.CreatedDate);
                Assert.Null(result.UpdatedDate);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Search_WithACategoryName_Should_ReturnAListOfModelsWithLiteMapping()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Search_WithACategoryName_Should_ReturnAListOfModelsWithLiteMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCategoryTable = true,
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductTable = true,
                    DoProductStatusTable = true,
                    DoProductTypeTable = true,
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
                var results = await new ProductWorkflow().SearchAsync(new ProductSearchModel { Active = true, CategoryName = "Wine", PricingFactoryContext = DefaultPriceFactoryContext }, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results.results);
                Assert.Equal(2, results.results.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetProductReviewInformation_Should_ReturnAModel()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetProductReviewInformation_Should_ReturnAModel";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoReviewTable = true };
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
                var result = await new ProductWorkflow().GetProductReviewInformationAsync(1151, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductReviewInformationModel>(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetProductReviewInformation_ForAProductWithNoReviews_Should_ReturnAModelWithNoResults()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetProductReviewInformation_ForAProductWithNoReviews_Should_ReturnAModelWithNoResults";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoReviewTable = true };
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
                var result = await new ProductWorkflow().GetProductReviewInformationAsync(0969, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<ProductReviewInformationModel>(result);
                Assert.Equal(0, result.Count);
                Assert.Equal(0, result.Value);
                Assert.Empty(result.Reviews!);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetAllActiveAsListing_Should_ReturnAListOfModelsWithListingMapping()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetAllActiveAsListing_Should_ReturnAListOfModelsWithListingMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true };
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
                var results = await new ProductWorkflow().GetAllActiveAsListingAsync(contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Equal(28, results.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetProductsByCategory_Should_ReturnAListOfModelsWithListingMapping()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetProductsByCategory_Should_ReturnAListOfModelsWithListingMapping";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoProductCategoryTable = true, DoCategoryTable = true };
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
                var result = await new ProductWorkflow().GetProductsByCategoryAsync(null, DefaultPriceFactoryContext, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<QuickOrderFormProductsModel>(result);
                Assert.Empty(result!.Headers!);
                Assert.NotNull(result.ProductsByCategory);
                Assert.Equal(2, result.ProductsByCategory!.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetLatestProducts_Should_ReturnAListOfModels()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetLatestProducts_Should_ReturnAListOfModels";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true };
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
                var results = await new ProductWorkflow().GetLatestProductsAsync(4, 180, DefaultPriceFactoryContext, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.InRange(results.Count, 0, 4);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetCustomersFavoriteProducts_Should_ReturnAListOfModels()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetCustomersFavoriteProducts_Should_ReturnAListOfModels";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoSalesOrderTable = true, DoSalesOrderItemTable = true };
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
                var results = await new ProductWorkflow().GetCustomersFavoriteProductsAsync(4, 180, DefaultPriceFactoryContext, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.InRange(results.Count, 0, 4);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetBestSellingProducts_Should_ReturnAListOfModels()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetBestSellingProducts_Should_ReturnAListOfModels";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoSalesOrderTable = true, DoSalesOrderItemTable = true };
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
                var results = await new ProductWorkflow().GetBestSellingProductsAsync(4, 180, DefaultPriceFactoryContext, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.InRange(results.Count, 0, 4);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetTrendingProducts_Should_ReturnAListOfModels()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetTrendingProducts_Should_ReturnAListOfModels";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoSalesOrderTable = true, DoSalesOrderItemTable = true };
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
                var results = await new ProductWorkflow().GetTrendingProductsAsync(4, 180, DefaultPriceFactoryContext, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.InRange(results.Count, 0, 4);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetKeyWords_Should_ReturnAListOfStrings()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetKeyWords_Should_ReturnAListOfStrings";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true };
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
                var results = await new ProductWorkflow().GetKeyWordsAsync(null, null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<string>>(results);
                Assert.InRange(results.Count, 0, int.MaxValue);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetKeyWords_WithTerm_Should_ReturnAListOfStrings()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetKeyWords_WithTerm_Should_ReturnAListOfStrings";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
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
                // Act
                var results = await new ProductWorkflow().GetKeyWordsAsync("200", null, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<string>>(results);
                Assert.InRange(results.Count, 0, int.MaxValue);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_GetKeyWords_WithTermAndTypes_Should_ReturnAListOfStrings()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetKeyWords_WithTermAndTypes_Should_ReturnAListOfStrings";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup { DoProductTable = true, DoCategoryTable = true };
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
                var results = await new ProductWorkflow().GetKeyWordsAsync("200", new List<string> { "product", "product_code", "something else" }, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<string>>(results);
                Assert.InRange(results.Count, 0, int.MaxValue);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_ProductWithSubscriptionType_ByKey_Should_ReturnAModelWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Get_ProductWithSubscriptionType_ByKey_Should_ReturnAModelWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoProductSubscriptionTypeTable = true,
                    DoSubscriptionTypeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                var result = await new ProductWorkflow().GetFullAsync("PRODUCT-A", contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.NotNull(result);
                Assert.IsType<ProductModel>(result);
                Assert.NotNull(result!.ProductSubscriptionTypes);
                Assert.Single(result.ProductSubscriptionTypes);
                var productSubscriptionType = result.ProductSubscriptionTypes!.First();
                Assert.Equal(1, productSubscriptionType.ID);
                Assert.Equal(000100, productSubscriptionType.MasterID);
                Assert.Equal(1, productSubscriptionType.SlaveID);
                Assert.Equal("BronzeMembershipMonthly", productSubscriptionType.SlaveKey);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        private static IProductModel GenerateProductModel()
        {
            var timestamp = DateExtensions.GenDateTime;
            return new ProductModel
            {
                // Base Properties
                Active = true,
                CustomKey = "AWESOME-1",
                CreatedDate = timestamp,
                UpdatedDate = null,
                // NameableBase Properties
                Name = "Awesome Product",
                // Product Properties
                AllowBackOrder = true,
                SortOrder = 5,
                Description = "product description",
                AvailableEndDate = timestamp.AddDays(90),
                AvailableStartDate = timestamp,
                HandlingCharge = 1.24m,
                Depth = null,
                Height = null,
                Weight = null,
                Width = null,
                IsDiscontinued = false,
                IsFreeShipping = false,
                IsTaxable = true,
                IsUnlimitedStock = false,
                IsVisible = true,
                KitBaseQuantityPriceMultiplier = null,
                ManufacturerPartNumber = "13245AB12",
                MaximumPurchaseQuantity = null,
                MaximumPurchaseQuantityIfPastPurchased = null,
                MinimumPurchaseQuantity = null,
                MinimumPurchaseQuantityIfPastPurchased = null,
                // PriceBase = 5.99m,
                // PriceMsrp = 8.99m,
                // PriceReduction = 7.99m,
                // PriceSale = 4.99m,
                SeoDescription = "This awesome product's seo description",
                SeoKeywords = "awesome,product",
                SeoMetaData = null,
                SeoPageTitle = "Awesome Product",
                SeoUrl = "Awesome-Product",
                ShortDescription = "A really awesome product",
                // StockQuantity = 508,
                // StockQuantityBroken = 0,
                UnitOfMeasure = "Each",
                // Related Objects
                TypeID = 0,
                TypeKey = null,
                TypeName = "Product Type Name",
                Type = new TypeModel { Active = true, CreatedDate = timestamp, Name = "Product Type Name" },
                StatusID = 0,
                StatusKey = null,
                StatusName = "Product Status Name",
                Status = new StatusModel { Active = true, CreatedDate = timestamp, Name = "Product Status Name" },
                Package = new PackageModel
                {
                    Active = true,
                    CreatedDate = timestamp,
                    Weight = 5.123m,
                    Width = 5,
                    Depth = 6,
                    Height = 7,
                    IsCustom = true,
                },
                PackageID = null,
                MasterPack = new PackageModel
                {
                    Active = true,
                    CreatedDate = timestamp,
                    Weight = 5.123m,
                    Width = 5,
                    Depth = 6,
                    Height = 7,
                    IsCustom = true,
                },
                MasterPackID = null,
                Pallet = new PackageModel
                {
                    Active = true,
                    CreatedDate = timestamp,
                    Weight = 5.123m,
                    Width = 5,
                    Depth = 6,
                    Height = 7,
                    IsCustom = true,
                },
                PalletID = null,
                // Associated Objects
                ProductAssociations = new List<ProductAssociationModel>(), // See Verify_Create_WithValidProductAssociationsData_Should_AddToTheDbSetAndSaveChanges
                ProductCategories = new List<ProductCategoryModel>(), // See Verify_Create_WithValidProductCategoriesData_Should_AddToTheDbSetAndSaveChanges
                Manufacturers = new List<ManufacturerProductModel>(), // See
                Images = new List<ProductImageModel>(),
                StoredFiles = new List<ProductFileModel>(),
                Vendors = new List<VendorProductModel>(), // See Verify_Create_WithValidVendorProductsData_Should_AddToTheDbSetAndSaveChanges
                // Not Settable Properties
                PrimaryImageFileName = null,
                Reviews = null,
                ProductsAssociatedWith = null,
            };
        }

        #region Create
        [Fact]
        public async Task Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Create_WithValidData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductTable = true,
                    DoProductStatusTable = true,
                    DoProductTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                    SaveChangesResult = 1,
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
                var model = GenerateProductModel();
                // Act
                await new ProductWorkflow().CreateAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.Products!.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Create_WithValidProductCategoriesData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Create_WithValidProductCategoriesData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCategoryTable = true,
                    DoCategoryTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductStatusTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
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
                var model = GenerateProductModel();
                model.ProductCategories = new List<IProductCategoryModel>
                {
                    new ProductCategoryModel
                    {
                        Active = true,
                        SlaveID = 1,
                        SortOrder = 1,
                    },
                    new ProductCategoryModel
                    {
                        Active = true,
                        Slave = new CategoryModel { Active = true, Name = "Some New Category" },
                        SortOrder = 1,
                    },
                    new ProductCategoryModel
                    {
                        Active = false,
                        Slave = new CategoryModel { Active = true, Name = "Some New Category 2" },
                        SortOrder = 1
                    },
                };
                // Act
                await new ProductWorkflow().CreateAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.Products!.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Create_WithValidProductAssociationsData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Create_WithValidProductAssociationsData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductTable = true,
                    DoProductStatusTable = true,
                    DoProductTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                    SaveChangesResult = 1,
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
                var model = GenerateProductModel();
                model.ProductAssociations = new List<IProductAssociationModel>
                {
                    new ProductAssociationModel
                    {
                        Active = true,
                        MasterID = 1151,
                        SlaveID = 1152,
                        TypeID = 2,
                        Quantity = 5,
                        UnitOfMeasure = "SET-100",
                    },
                    new ProductAssociationModel
                    {
                        Active = true,
                        MasterID = 1151,
                        SlaveID = 1152,
                        Type = new TypeModel { Active = true, Name = "Some New Association Type" },
                        Quantity = 5,
                        UnitOfMeasure = "SET-100",
                    },
                    new ProductAssociationModel
                    {
                        Active = true,
                        MasterKey = "0200-ANGBT-BFUT-IA",
                        SlaveKey = "0200-ANGRF-OPERA-VSI-IA",
                        TypeID = 2,
                        Quantity = 5,
                        UnitOfMeasure = "SET-100",
                    },
                };
                // Act
                await new ProductWorkflow().CreateAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.Products!.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Create_WithValidManufacturerProductsData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Create_WithValidManufacturerProductsData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoManufacturerTable = true,
                    DoManufacturerProductTable = true,
                    DoManufacturerTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                    DoProductStatusTable = true,
                    SaveChangesResult = 1,
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
                var model = GenerateProductModel();
                model.Manufacturers = new List<IManufacturerProductModel>
                {
                    new ManufacturerProductModel
                    {
                        Active = true,
                        MasterName = "Joe's Manufacturing",
                        MasterKey = "JOES-MNF",
                        SlaveKey = model.CustomKey,
                        Master = new ManufacturerModel
                        {
                            Name = "Joe's Manufacturing",
                            CustomKey = "JOES-MNF",
                            TypeKey = "GENERAL"
                        },
                    },
                    new ManufacturerProductModel
                    {
                        Active = true,
                        SlaveKey = model.CustomKey,
                        MasterKey = "MNF-1",
                        Master = new ManufacturerModel
                        {
                            CustomKey = "MNF-1",
                            TypeKey = "GENERAL",
                        },
                    },
                };
                // Act
                await new ProductWorkflow().CreateAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.Products!.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Create_WithValidVendorProductsData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Create_WithValidVendorProductsData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoPackageTypeTable = true,
                    DoProductTable = true,
                    DoProductStatusTable = true,
                    DoProductTypeTable = true,
                    DoVendorProductTable = true,
                    DoVendorTable = true,
                    DoPackageTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                    SaveChangesResult = 1,
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
                var model = GenerateProductModel();
                model.Vendors = new List<IVendorProductModel>
                {
                    new VendorProductModel
                    {
                        Active = true,
                        ActualCost = 5m,
                        InventoryCount = 1,
                        MaximumInventory = 100,
                        MinimumInventory = 0,
                        ListedPrice = 12.50m,
                        MasterID = 1,
                        MasterName = "Bob's Electronics",
                    },
                    new VendorProductModel
                    {
                        Active = true,
                        ActualCost = 5m,
                        InventoryCount = 1,
                        MaximumInventory = 100,
                        MinimumInventory = 0,
                        ListedPrice = 12.50m,
                        MasterName = "Laars",
                        MasterKey = "Laars",
                    },
                    new VendorProductModel
                    {
                        Active = false,
                        ActualCost = 5m,
                        InventoryCount = 1,
                        MaximumInventory = 100,
                        MinimumInventory = 0,
                        ListedPrice = 12.50m,
                        MasterName = "Lochinvar",
                        MasterKey = "Lochinvar"
                    },
                };
                // Act
                await new ProductWorkflow().CreateAsync(model, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.Products!.Verify(m => m.Add(It.IsAny<Product>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CreateLegacyProductWithKey_WithValidData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_CreateLegacyProductWithKey_WithValidData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoProductStatusTable = true,
                    DoPackageTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
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
                await new ProductWorkflow().CreateLegacyProductWithKeyAsync("SOME-LEGACY-KEY", null, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.Products!.Verify(m => m.Add(It.IsAny<Product>()), Times.AtLeastOnce);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_CreateProductReview_WithValidData_Should_AddToTheDbSetAndSaveChanges()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_CreateProductReview_WithValidData_Should_AddToTheDbSetAndSaveChanges";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoReviewTable = true,
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
                var model = new ReviewModel
                {
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    UpdatedDate = null,
                    Approved = true,
                    Title = "This product r0x",
                    Comment = "This is my comment",
                    Value = 5,
                    TypeID = 1,
                    ProductID = 1152,
                };
                // Act
                try
                {
                    await new ProductWorkflow().CreateProductReviewAsync(model, contextProfileName).ConfigureAwait(false);
                }
                catch
                {
                    // Ignored
                }
                // Assert
                mockingSetup.Reviews!.Verify(m => m.Add(It.IsAny<Review>()), Times.Once());
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region Update
        [Fact]
        public async Task Verify_Update_Should_UpdateUpdatedDateValue()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Update_Should_UpdateUpdatedDateValue";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoAttributeTypeTable = true,
                    DoCategoryTable = true,
                    DoCategoryTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoInventoryLocationSectionTable = true,
                    DoInventoryLocationTable = true,
                    DoPackageTable = true,
                    DoPackageTypeTable = true,
                    DoProductAssociationTable = true,
                    DoProductAssociationTypeTable = true,
                    DoProductCategoryTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoProductStatusTable = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoVendorProductTable = true,
                    DoVendorTable = true,
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
                var model = new ProductModel
                {
                    ID = 1151,
                    Active = true,
                    CustomKey = "Some key",
                    Name = "Some product",
                    StatusName = "Normal",
                    // ProductInventoryLocationSections = new List<ProductInventoryLocationSectionModel>
                    // {
                    //     new ProductInventoryLocationSectionModel
                    //     {
                    //         Active = true,
                    //         Quantity = 100,
                    //         SlaveKey = "Shelf 1-1B",
                    //         SlaveName = "Shelf 1-1B",
                    //         Slave = new InventoryLocationSectionModel
                    //         {
                    //             CustomKey = "Shelf 1-1B",
                    //             Active = true,
                    //             InventoryLocationKey = "UMCOM",
                    //         }
                    //     },
                    // },
                    Vendors = new List<VendorProductModel>
                    {
                        new VendorProductModel
                        {
                            Active = true,
                            ActualCost = 5m,
                            InventoryCount = 1,
                            MaximumInventory = 100,
                            MinimumInventory = 0,
                            ListedPrice = 12.50m,
                            MasterID = 1,
                            MasterName = "Bob's Electronics"
                        },
                    },
                    ProductAssociations = new List<ProductAssociationModel>
                    {
                        new ProductAssociationModel
                        {
                            Active = true,
                            MasterID = 1151,
                            SlaveID = 1152,
                            TypeID = 2,
                            Quantity = 5,
                            UnitOfMeasure = "SET-100",
                        },
                        new ProductAssociationModel
                        {
                            Active = true,
                            MasterID = 1151,
                            SlaveID = 1152,
                            Type = new TypeModel { Active = true, Name = "Some New Association Type" },
                            Quantity = 5,
                            UnitOfMeasure = "SET-100"
                        },
                    },
                    ProductCategories = new List<ProductCategoryModel>
                    {
                        new ProductCategoryModel
                        {
                            Active = true,
                            SlaveID = 1,
                            SortOrder = 1,
                        },
                        new ProductCategoryModel
                        {
                            Active = true,
                            Slave = new CategoryModel { Active = true, Name = "Some New Category" },
                            SortOrder = 1,
                        }
                    },
                };
                // Act
                var workflow = new ProductWorkflow();
                var result = await workflow.UpdateAsync(model, contextProfileName).ConfigureAwait(false);
                var resultModel = await workflow.GetAsync(result.Result, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce());
                Assert.NotNull(resultModel);
                Assert.NotNull(resultModel!.UpdatedDate);
                ////Assert.Equal("Plaza 1, Suite 925", result.Street2);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region Deactivate
        [Fact]
        public async Task Verify_Deactivate_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_Deactivate_ThatIsNotActive_Should_NotUpdateItemAndReturnTrue";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoInactives = true,
                    DoProductTable = true,
                    DoProductTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoProductInventoryLocationSectionTable = true,
                    DoPackageTypeTable = true,
                    DoPackageTable = true,
                    DoProductImageTable = true,
                    DoProductFileTable = true,
                    SaveChangesResult = 1,
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
                var workflow = new ProductWorkflow();
                // Act
                var result1 = await workflow.DeactivateAsync(0969, contextProfileName).ConfigureAwait(false);
                var result2 = await workflow.GetAsync(
                        id: 0969,
                        contextProfileName: contextProfileName,
                        isVendorAdmin: false,
                        vendorAdminID: null,
                        previewID: null)
                    .ConfigureAwait(false);
                // Assert
                ////mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never); // we didn't save any changes
                Verify_CEFAR_Passed_WithNoMessages(result1);
                Assert.NotNull(result2);
                Assert.Null(result2!.UpdatedDate); // We didn't update anything
                Assert.False(result2.Active); // It was inactive without edit
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion

        #region Special
        [Fact]
        public async Task Verify_GetBestSellingProducts_Should_ReturnAListOfProducts()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_GetBestSellingProducts_Should_ReturnAListOfProducts";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
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
                var results = await new ProductWorkflow().GetBestSellingProductsAsync(
                        count: 4,
                        days: 365,
                        pricingFactoryContext: DefaultPriceFactoryContext,
                        categorySeoUrl: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Equal(3, results.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Don't run automatically")]
        public async Task Verify_SearchPreviouslyOrdered_ReturnsProductModel()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_SearchPreviouslyOrdered_ReturnsProductModel";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoAttributes = true,
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
                var workflow = new ProductWorkflow();
                var search = new ProductSearchModel
                {
                    ProductIDs = new[] { 1154, 1153, 1155, 1156, 1151 },
                    PricingFactoryContext = DefaultPriceFactoryContext,
                };
                // Act
                var (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Equal(5, results.Count);
                // Filter From Name, description and attribute value
                search.Keywords = "luna random some";
                (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Equal(2, results.Count);
                // Filter From description
                search.Keywords = "random";
                (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Single(results);
                // Filter From Name
                search.Keywords = "Testing";
                (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Single(results);
                // Filter From AttributeValue
                search.Keywords = "some";
                (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Empty(results);
                // Filter From Name
                search.Keywords = "0300";
                (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Single(results);
                // Filter By Attribute Name
                search.Keywords = "";
                //search.AttributeName = "DigitalDownload";
                (results, _, _) = await workflow.SearchPreviouslyOrderedAsync(search, false, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Equal(5, results.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_SearchRecentlyViewedProductsAsync_ReturnsAListOfProducts()
        {
            // Arrange
            const string contextProfileName = "Products_Products_SpecialWorkflowTests|Verify_SearchRecentlyViewedProductsAsync_ReturnsAListOfProducts";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoProductTable = true,
                    DoProductAssociationTypeTable = true,
                    DoAttributes = true,
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
                var results = await new ProductWorkflow().SearchRecentlyViewedProductsAsync(
                        new List<int>() { 1154, 1153, 1155, 1156, 1151 },
                        contextProfileName)
                    .ConfigureAwait(false);
                // Assert
                Assert.IsType<List<IProductModel>>(results);
                Assert.Equal(5, results.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        #endregion
    }
}
