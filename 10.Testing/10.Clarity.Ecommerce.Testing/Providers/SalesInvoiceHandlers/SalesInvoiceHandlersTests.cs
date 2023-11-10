// <copyright file="SalesInvoiceHandlersTests.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice handlers tests class</summary>
#nullable enable
namespace Clarity.Ecommerce.Testing.Providers.SalesInvoiceHandlers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Providers.Payments.Mock;
    using Interfaces.DataModel;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.SalesInvoiceHandlers.Actions;
    using Interfaces.Providers.SalesInvoiceHandlers.Queries;
    using JetBrains.Annotations;
    using JSConfigs;
    using Mapper;
    using Models;
#if NET5_0_OR_GREATER
    using Lamar;
#else
    using StructureMap.Pipeline;
#endif
    using Xunit;
    using Actions = Ecommerce.Providers.SalesInvoiceHandlers.Actions;
    using Queries = Ecommerce.Providers.SalesInvoiceHandlers.Queries;

    [Trait("Category", "Providers.SalesInvoiceHandlers")]
    public class SalesInvoiceHandlersTests : XUnitLogHelper
    {
        public SalesInvoiceHandlersTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper)
            : base(testOutputHelper)
        {
            CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
        }

        private static MockingSetup GenActionsMockingSetup() => new()
        {
            SaveChangesResult = 1,
            DoAccountTable = true,
            ////DoAddressTable = true,
            ////DoAppliedCartDiscountTable = true,
            ////DoAppliedCartItemDiscountTable = true,
            ////DoAppliedSalesOrderDiscountTable = true,
            ////DoAppliedSalesOrderItemDiscountTable = true,
            ////DoCartItemTable = true,
            ////DoCartItemTargetTable = true,
            ////DoCartStateTable = true,
            ////DoCartStatusTable = true,
            ////DoCartTable = true,
            ////DoCartTypeTable = true,
            DoContactTable = true,
            ////DoCountryTable = true,
            ////DoDiscountCodeTable = true,
            ////DoDiscountTable = true,
            ////DoDiscountUserTable = true,
            DoEmailTypeTable = true,
            DoEmailStatusTable = true,
            DoEmailQueueTable = true,
            ////DoManufacturerProductTable = true,
            ////DoProductCategoryTable = true,
            ////DoProductImageTable = true,
            ////DoProductTable = true,
            ////DoProductTypeTable = true,
            ////DoRateQuoteTable = true,
            ////DoRegionTable = true,
            DoPaymentMethodTable = true,
            DoPaymentTable = true,
            DoPaymentStatusTable = true,
            DoPaymentTypeTable = true,
            DoSalesInvoiceItemTable = true,
            DoSalesInvoiceTable = true,
            DoSalesInvoiceTypeTable = true,
            DoSalesInvoiceStateTable = true,
            DoSalesInvoiceStatusTable = true,
            DoSalesOrderSalesInvoiceTable = true,
            DoSalesInvoicePaymentTable = true,
            ////DoSalesItemTargetTypeTable = true,
            ////DoSalesOrderItemTable = true,
            DoSalesOrderTable = true,
            DoSalesOrderStatusTable = true,
            ////DoStoreProductTable = true,
            ////DoShipCarrierMethodTable = true,
            DoUserTable = true,
            ////DoVendorProductTable = true,
            DoWalletTable = true,
        };

        private static MockingSetup GenQueriesMockingSetup() => new()
        {
            ////SaveChangesResult = 1,
            ////DoAccountTable = true,
            ////DoAddressTable = true,
            ////DoAppliedCartDiscountTable = true,
            ////DoAppliedCartItemDiscountTable = true,
            ////DoAppliedSalesOrderDiscountTable = true,
            ////DoAppliedSalesOrderItemDiscountTable = true,
            ////DoCartItemTable = true,
            ////DoCartItemTargetTable = true,
            ////DoCartStateTable = true,
            ////DoCartStatusTable = true,
            ////DoCartTable = true,
            ////DoCartTypeTable = true,
            DoCalendarEventTable = true,
            DoCalendarEventProductTable = true,
            DoContactTable = true,
            ////DoCountryTable = true,
            ////DoDiscountCodeTable = true,
            ////DoDiscountTable = true,
            ////DoDiscountUserTable = true,
            ////DoEmailTypeTable = true,
            ////DoEmailStatusTable = true,
            ////DoEmailQueueTable = true,
            ////DoManufacturerProductTable = true,
            ////DoProductCategoryTable = true,
            ////DoProductImageTable = true,
            DoProductTable = true,
            ////DoProductTypeTable = true,
            ////DoRateQuoteTable = true,
            ////DoRegionTable = true,
            ////DoPaymentMethodTable = true,
            ////DoPaymentTable = true,
            ////DoPaymentStatusTable = true,
            ////DoPaymentTypeTable = true,
            DoSalesInvoiceItemTable = true,
            DoSalesInvoiceTable = true,
            DoSalesInvoiceTypeTable = true,
            DoSalesInvoiceStateTable = true,
            DoSalesInvoiceStatusTable = true,
            ////DoSalesOrderSalesInvoiceTable = true,
            ////DoSalesItemTargetTypeTable = true,
            ////DoSalesOrderItemTable = true,
            ////DoSalesOrderTable = true,
            ////DoSalesOrderStatusTable = true,
            ////DoStoreProductTable = true,
            ////DoShipCarrierMethodTable = true,
            ////DoUserTable = true,
            ////DoVendorProductTable = true,
            ////DoWalletTable = true,
        };

        [Fact(Skip = "In Progress")]
        public Task Verify_SalesInvoiceActionsHandlers()
        {
            var actionsProvider = new Actions.Standard.StandardSalesInvoiceActionsProvider();
            Assert.Equal(Enums.ProviderType.SalesInvoiceActionsHandler, actionsProvider.ProviderType);
            Assert.True(actionsProvider.IsDefaultProvider);
            Assert.True(actionsProvider.HasDefaultProvider);
            var queriesProvider = new Queries.Standard.StandardSalesInvoiceQueriesProvider();
            Assert.Equal(Enums.ProviderType.SalesInvoiceQueriesHandler, queriesProvider.ProviderType);
            Assert.True(queriesProvider.IsDefaultProvider);
            Assert.True(queriesProvider.HasDefaultProvider);
            return Task.WhenAll(
                Verify_AutoPaySalesInvoiceAsync_Passes(actionsProvider, queriesProvider),
                Verify_SetRecordAsPaid_Passes(actionsProvider, queriesProvider),
                Verify_SetRecordAsPartiallyPaid_Passes(actionsProvider, queriesProvider),
                Verify_SetRecordAsUnpaid_Passes(actionsProvider, queriesProvider),
                Verify_SetRecordAsVoided_Passes(actionsProvider, queriesProvider),
                Verify_AssignSalesGroup_Passes(actionsProvider, queriesProvider),
                Verify_CreateSalesInvoiceFromSalesOrder_Passes(actionsProvider, queriesProvider),
                Verify_CreateInvoiceForOrder_Passes(actionsProvider, queriesProvider),
                Verify_PaySingleByID_Passes(actionsProvider, queriesProvider),
                Verify_PaySingleByID_ThatIsAlreadyPaid_FailsWithSingleMessage(actionsProvider, queriesProvider),
                Verify_AddPayment_Passes(actionsProvider, queriesProvider),
                Verify_PayMultipleByAmounts_Passes(actionsProvider, queriesProvider));
        }

        [Fact(Skip = "In Progress")]
        public Task Verify_SalesInvoiceQueriesHandlers()
        {
            var actionsProvider = new Actions.Standard.StandardSalesInvoiceActionsProvider();
            Assert.Equal(Enums.ProviderType.SalesInvoiceActionsHandler, actionsProvider.ProviderType);
            Assert.True(actionsProvider.IsDefaultProvider);
            Assert.True(actionsProvider.HasDefaultProvider);
            var queriesProvider = new Queries.Standard.StandardSalesInvoiceQueriesProvider();
            Assert.Equal(Enums.ProviderType.SalesInvoiceQueriesHandler, queriesProvider.ProviderType);
            Assert.True(queriesProvider.IsDefaultProvider);
            Assert.True(queriesProvider.HasDefaultProvider);
            return Task.WhenAll(
                Verify_GetRecordsPerEventAndNotStatus_Passes(actionsProvider, queriesProvider),
                Verify_GetRecordsPastDueForPaymentReminder_Passes(actionsProvider, queriesProvider),
                // Includes a function which can only be run inside LINQ
                // Verify_GetRecordsForFinalPaymentReminder_Passes(actionsProvider, queriesProvider),
                Verify_GetRecordByUserAndEvent_Passes(actionsProvider, queriesProvider),
                Verify_GetRecordSecurely_Passes(actionsProvider, queriesProvider));
        }

        private Task Verify_AutoPaySalesInvoiceAsync_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.AutoPaySalesInvoiceAsync(
                            userID: 1,
                            invoiceID: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private Task Verify_SetRecordAsPaid_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.SetRecordAsPaidAsync(
                            id: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_SetRecordAsPartiallyPaid_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.SetRecordAsPartiallyPaidAsync(
                            id: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_SetRecordAsUnpaid_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.SetRecordAsUnpaidAsync(
                            id: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_SetRecordAsVoided_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.SetRecordAsVoidedAsync(
                            id: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_AssignSalesGroup_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.AssignSalesGroupAsync(
                            salesInvoiceID: 1,
                            salesGroupID: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_CreateInvoiceForOrder_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await actions.CreateInvoiceForOrderAsync(
                            id: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_CreateSalesInvoiceFromSalesOrder_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var model = context.SalesOrders.First().CreateSalesOrderModelFromEntityFull(contextProfileName);
                    // Act
                    var result = await actions.CreateSalesInvoiceFromSalesOrderAsync(
                            salesOrder: model!,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_AddPayment_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var model = context.SalesInvoices.First().CreateSalesInvoiceModelFromEntityFull(contextProfileName);
                    var payment = new PaymentModel
                    {
                        Active = true,
                        CreatedDate = new DateTime(2023, 1, 1),
                        WalletID = 1,
                        CVV = "123",
                        Amount = model!.BalanceDue,
                        TypeID = 1,
                        StatusID = 1,
                    };
                    // Act
                    var result = await actions.AddPaymentAsync(
                            salesInvoice: model,
                            payment: payment,
                            originalPayment: payment.Amount,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_PaySingleByID_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var billing = context.Contacts.First().CreateContactModelFromEntityFull(contextProfileName);
                    var payment = new PaymentModel
                    {
                        Active = true,
                        CreatedDate = new DateTime(2023, 1, 1),
                        WalletID = 1,
                        CVV = "123",
                        Amount = 100m,
                    };
                    // Act
                    var result = await actions.PaySingleByIDAsync(
                            id: 2,
                            payment: payment,
                            billing: billing,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_PaySingleByID_ThatIsAlreadyPaid_FailsWithSingleMessage(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var billing = context.Contacts.First().CreateContactModelFromEntityFull(contextProfileName);
                    var payment = new PaymentModel
                    {
                        Active = true,
                        CreatedDate = new DateTime(2023, 1, 1),
                        WalletID = 1,
                        CVV = "123",
                        Amount = 100m,
                    };
                    // Act
                    var result = await actions.PaySingleByIDAsync(
                            id: 1,
                            payment: payment,
                            billing: billing,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Failed_WithSingleMessage(result, "ERROR! Invoice is already marked as paid");
                });
        }

        private static Task Verify_PayMultipleByAmounts_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenActionsMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var billing = context.Contacts.First().CreateContactModelFromEntityFull(contextProfileName);
                    var amounts = new Dictionary<int, decimal>()
                    {
                        [2] = 100m,
                        [3] = 50m,
                    };
                    var payment = new PaymentModel
                    {
                        Active = true,
                        CreatedDate = new DateTime(2023, 1, 1),
                        WalletID = 1,
                        CVV = "123",
                        Amount = amounts.Values.Sum(),
                    };
                    // Act
                    var result = await actions.PayMultipleByAmountsAsync(
                            amounts: amounts,
                            payment: payment,
                            billing: billing,
                            userID: 1,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_GetRecordsPerEventAndNotStatus_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenQueriesMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var eventKey = "0200-ANGBT-BFUT-IA";
                    var statusKey = "Void";
                    // Act
                    var result = await queries.GetRecordsPerEventAndNotStatusAsync(
                            eventKey: eventKey,
                            statusKey: statusKey,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.NotEmpty(result);
                });
        }

        private static Task Verify_GetRecordsPastDueForPaymentReminder_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenQueriesMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await queries.GetRecordsPastDueForPaymentReminderAsync(
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.NotEmpty(result);
                });
        }

#pragma warning disable IDE0051 // Remove unused private members
        private static Task Verify_GetRecordsForFinalPaymentReminder_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
#pragma warning restore IDE0051 // Remove unused private members
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenQueriesMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    // Act
                    var result = await queries.GetRecordsForFinalPaymentReminderAsync(
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.NotEmpty(result);
                });
        }

        private static Task Verify_GetRecordByUserAndEvent_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenQueriesMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var userID = 1;
                    var calendarEventID = 1;
                    // Act
                    var result = await queries.GetRecordByUserAndEventAsync(
                            userID: userID,
                            calendarEventID: calendarEventID,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Verify_CEFAR_Passed_WithNoMessages(result);
                });
        }

        private static Task Verify_GetRecordSecurely_Passes(
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries)
        {
            // Arrange
            return RunWithSetupAndTearDownAsync(
                actions,
                queries,
                GenQueriesMockingSetup(),
                async (actions, queries, context, contextProfileName) =>
                {
                    var id = 1;
                    var accountIDs = new List<int> { 1 };
                    // Act
                    var result = await queries.GetRecordSecurelyAsync(
                            id: id,
                            accountIDs: accountIDs,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    // Assert
                    Assert.NotNull(result);
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
#pragma warning disable IDE0051 // Remove unused private members
        private static void Verify_CEFAR_Failed_WithMultipleMessages(
            CEFActionResponse result,
            params string[] expectMessages)
#pragma warning restore IDE0051 // Remove unused private members
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
            ISalesInvoiceActionsProviderBase actions,
            ISalesInvoiceQueriesProviderBase queries,
            MockingSetup mockingSetup,
            Func<ISalesInvoiceActionsProviderBase, ISalesInvoiceQueriesProviderBase, IClarityEcommerceEntities, string, Task> task,
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
                    x.For<ISalesInvoiceActionsProviderBase>().Use(() => actions);
                    x.For<ISalesInvoiceQueriesProviderBase>().Use(() => queries);
                    x.For<IPaymentsProviderBase>().UseInstance(new ObjectInstance(new MockPaymentsProvider()));
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                await task(actions, queries, mockingSetup.MockContext.Object, contextProfileName).ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }
    }
}
