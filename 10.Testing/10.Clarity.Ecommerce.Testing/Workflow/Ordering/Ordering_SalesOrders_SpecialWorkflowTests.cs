// <copyright file="SalesOrderWorkflowsTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order workflows tests class</summary>
// ReSharper disable InconsistentNaming, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed
// ReSharper disable UnusedMember.Global, UnusedVariable
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Models;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Ordering.SalesOrders.Special")]
    public class Ordering_SalesOrders_SpecialWorkflowTests : Ordering_SalesOrders_WorkflowTestsBase
    {
        public Ordering_SalesOrders_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        [Fact]
        public async Task Verify_Get_ByID_Should_ReturnAnObjectWithFullMap()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_Get_ByID_Should_ReturnAnObjectWithFullMap";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderStatusTable = true,
                    DoContactTypeTable = true,
                    DoNoteTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
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
                // Act
                var result = await new SalesOrderWorkflow().GetAsync(30000, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<SalesOrderModel>(result);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_Should_AddTwoOrdersToTheDatabase()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_Should_AddTwoOrdersToTheDatabase";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoSalesOrderTable = true,
                    DoSalesOrderStatusTable = true,
                    DoSalesOrderStateTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true
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
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(30003, contextProfileName, false).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(30003, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<CEFActionResponse<ISalesOrderModel[]>>(result);
                Assert.True(result.ActionSucceeded, result.Messages.DefaultIfEmpty(string.Empty).Aggregate((c, n) => $"{c}\r\n{n}"));
                Assert.False(result.Messages.Any());
                Assert.NotNull(entityResult.UpdatedDate);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.AtLeastOnce);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithNoBackOrderItems_Should_NotSaveAndReturnAFailureMessage()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithNoBackOrderItems_Should_NotSaveAndReturnAFailureMessage";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderItemStatusTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true
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
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(30000, contextProfileName).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(30000, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.False(result.ActionSucceeded);
                Assert.Single(result.Messages);
                Assert.Equal("ERROR! There were no items to be backordered, cannot split", result.Messages[0]);
                Assert.Null(entityResult.UpdatedDate);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithAnInactiveOrder_Should_NotSaveAndReturnAFailureMessage()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithAnInactiveOrder_Should_NotSaveAndReturnAFailureMessage";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderItemStatusTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoInactives = true
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
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(30000, contextProfileName).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(30000, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<CEFActionResponse<ISalesOrderModel[]>>(result);
                Assert.False(result.ActionSucceeded);
                Assert.Equal(2, result.Messages.Count);
                Assert.Equal("ERROR! The order was not Active, cannot split", result.Messages[0]);
                Assert.False(entityResult.Active);
                Assert.Null(entityResult.UpdatedDate);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithAnOrderWithNoActiveItems_Should_NotSaveAndReturnAFailureMessage()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithAnOrderWithNoActiveItems_Should_NotSaveAndReturnAFailureMessage";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderItemStatusTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoInactiveSalesItems = true,
                    DoSalesOrderStatusTable = true,
                    DoContactTypeTable = true,
                    DoNoteTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
                    SaveChangesResult = 1,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(30000, contextProfileName).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(30000, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.False(result.ActionSucceeded);
                Assert.Equal(2, result.Messages.Count);
                Assert.Equal("ERROR! The order did not have any active items, cannot split", result.Messages[0]);
                Assert.True(entityResult.Active);
                Assert.Null(entityResult.UpdatedDate);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithAnIDNotInTheData_Should_NotSaveAndReturnAFailureMessage()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithAnIDNotInTheData_Should_NotSaveAndReturnAFailureMessage";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderItemStatusTable = true,
                    DoSalesOrderTypeTable = true,
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
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(2, contextProfileName, false).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(2, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.IsType<CEFActionResponse<ISalesOrderModel[]>>(result);
                Assert.False(result.ActionSucceeded);
                Assert.Single(result.Messages);
                Assert.Equal("ERROR! Could not find the Entity", result.Messages[0]);
                Assert.Null(entityResult);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithOnlyOneActiveLineItem_Should_NotSaveAndReturnAFailureMessage()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithOnlyOneActiveLineItem_Should_NotSaveAndReturnAFailureMessage";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderItemStatusTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true
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
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(30002, contextProfileName).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(30002, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.False(result.ActionSucceeded);
                Assert.Equal(2, result.Messages.Count);
                Assert.Equal("ERROR! The order only had one active line item, cannot split", result.Messages[0]);
                Assert.Null(entityResult.UpdatedDate);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        /*
        [Fact]
        public async Task Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithCompletedItems_Should_NotSaveAndReturnAFailureMessage()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_SplitSalesOrderIntoSubOrdersBasedOnItemStatuses_WithCompletedItems_Should_NotSaveAndReturnAFailureMessage";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoProductFileTable = true,
                    DoProductImageTable = true
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
                var workflow = new SalesOrderWorkflow();
                // Act
                var result = await workflow.SplitSalesOrderIntoSubOrdersBasedOnItemStatusesAsync(30001, contextProfileName).ConfigureAwait(false);
                var entityResult = await workflow.GetAsync(30001, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.False(result.ActionSucceeded);
                Assert.Equal(2, result.Messages.Count);
                Assert.Equal("ERROR! There are order items that are already completed on this order", result.Messages[0]);
                Assert.Null(entityResult.UpdatedDate);
                mockingSetup.MockContext.Verify(m => m.SaveChangesAsync(), Times.Never);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
        */

        [Fact]
        public async Task Verify_GetSalesOrdersDistinctProductsForAccount_Returns_Enumerable_Of_Int()
        {
            // Arrange
            const string contextProfileName = "Ordering_SalesOrders_SpecialWorkflowTests|Verify_GetSalesOrdersDistinctProductsForAccount_Returns_Enumerable_Of_Int";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderTypeTable = true,
                    DoShipmentTable = true,
                    DoProductTable = true,
                    DoAccounts = true,
                    DoSalesOrderStatusTable = true,
                    DoContactTable = true,
                    DoContactTypeTable = true,
                    DoNoteTypeTable = true,
                    DoGeneralAttributeTable = true,
                    DoAttributeTypeTable = true,
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
                // Act
                var result = await new SalesOrderWorkflow().GetSalesOrdersDistinctProductsForAccountAsync(1, contextProfileName).ConfigureAwait(false);
                // Assert
                Assert.Equal(4, result.Count());
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
