// <copyright file="PurchaseOrdersTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase orders tests class</summary>
// ReSharper disable InconsistentNaming, UnusedMember.Global
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Moq;
    using Workflow;
    using Xunit;
    using Xunit.Abstractions;

    [Trait("Category", "Workflows.Purchasing.PurchaseOrders.Special")]
    public class Purchasing_PurchaseOrders_SpecialWorkflowTests : Purchasing_PurchaseOrders_WorkflowTestsBase
    {
        public Purchasing_PurchaseOrders_SpecialWorkflowTests(ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact(Skip = "Disabling pending client funding to recreate this functionality")]
        public async Task Verify_Checkout_WithEmptyCart_Should_NotGeneratePurchaseOrder()
        {
            // Arrange
            const string contextProfileName = "Purchasing_PurchaseOrders_SpecialWorkflowTests|Verify_Checkout_WithEmptyCart_Should_NotGeneratePurchaseOrder";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartItemTable = true,
                    DoCartTypeTable = true,
                    DoPurchaseOrderTable = true,
                    DoRateQuoteTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                await new PurchaseOrderWorkflow().CheckoutAsync(5, contextProfileName).ConfigureAwait(false);
                // Assert
                mockingSetup.PurchaseOrders!.Verify(m => m.Add(It.IsAny<PurchaseOrder>()), Times.Never);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Disabling pending client funding to recreate this functionality")]
        public async Task Verify_Checkout_WithSingleVendor_Should_GenerateSinglePO()
        {
            // Arrange
            const string contextProfileName = "Purchasing_PurchaseOrders_SpecialWorkflowTests|Verify_Checkout_WithSingleVendor_Should_GenerateSinglePO";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoPurchaseOrderTable = true,
                    DoProductTable = true,
                    DoVendorTable = true,
                    DoProductPricePointTable = true,
                    DoProductCategoryTable = true,
                    DoCategoryTable = true,
                    DoRateQuoteTable = true,
                    DoCartItemTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                await new PurchaseOrderWorkflow().CheckoutAsync(6, contextProfileName).ConfigureAwait(false);
                // Assert
                //mockingSetup.PurchaseOrders.Verify( m => m.Add(It.IsAny<DataModel.PurchaseOrder>()), Times.Once);
                //mockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
                var po = mockingSetup.PurchaseOrders!.Object.LastOrDefault();
                Assert.NotNull(po);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Disabling pending client funding to recreate this functionality")]
        public async Task Verify_Checkout_WithSingleVendor_Should_RemoveVendorItemsFromCart()
        {
            // Arrange
            const string contextProfileName = "Purchasing_PurchaseOrders_SpecialWorkflowTests|Verify_Checkout_WithSingleVendor_Should_RemoveVendorItemsFromCart";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoPurchaseOrderTable = true,
                    DoProductTable = true,
                    DoVendors = true,
                    DoDiscounts = true,
                    DoProductPricePointTable = true,
                    DoProductCategoryTable = true,
                    DoCategoryTable = true,
                    DoRateQuoteTable = true,
                    DoCartItemTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                await new PurchaseOrderWorkflow().CheckoutAsync(6, contextProfileName).ConfigureAwait(false);
                // Assert
                //mockingSetup.CartItems.Verify(m => m.Remove(It.IsAny<DataModel.CartItem>()), Times.Exactly(4));
                //mockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Disabling pending client funding to recreate this functionality")]
        public async Task Verify_Checkout_WithMultipleVendors_Should_GenerateMultiplePOs()
        {
            // Arrange
            const string contextProfileName = "Purchasing_PurchaseOrders_SpecialWorkflowTests|Verify_Checkout_WithMultipleVendors_Should_GenerateMultiplePOs";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoProductTable = true,
                    DoVendorTable = true,
                    DoProductPricePointTable = true,
                    DoProductCategoryTable = true,
                    DoCategoryTable = true,
                    DoRateQuoteTable = true,
                    DoCartItemTable = true,
                    DoDiscountTable = true,
                    DoNoteTable = true,
                    DoCartContactTable = true,
                    DoAppliedCartDiscountTable = true,
                    DoAppliedCartItemDiscountTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act
                await new PurchaseOrderWorkflow().CheckoutAsync(7, contextProfileName).ConfigureAwait(false);
                // Assert
                //Assert.Equal(4, mockingSetup.CartItems.Object.Count(x => !x.Active));
                //mockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact]
        public async Task Verify_Get_Returns_Correct_PurchaseOrderItems()
        {
            // Arrange
            const string contextProfileName = "Purchasing_PurchaseOrders_SpecialWorkflowTests|Verify_Get_Returns_Correct_PurchaseOrderItems";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoCartTable = true,
                    DoCartTypeTable = true,
                    DoPurchaseOrderTable = true,
                    DoProductTable = true,
                    DoVendorTable = true,
                    SaveChangesResult = 1,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                // Act  (one element belongs to a different vendor)
                /*var actual =*/
                await new PurchaseOrderWorkflow().GetAsync(1, contextProfileName).ConfigureAwait(false);
                // Assert
                //Assert.Equal(4, actual.OrderItems.Count);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
