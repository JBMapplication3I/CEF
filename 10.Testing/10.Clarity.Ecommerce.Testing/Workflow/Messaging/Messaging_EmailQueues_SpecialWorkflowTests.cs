// <copyright file="EmailQueueWorkflowTests.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the EmailQueue workflows tests class</summary>
// ReSharper disable InconsistentNaming, MissingXmlDoc, ObjectCreationAsStatement, ReturnValueOfPureMethodIsNotUsed, StyleCop.SA1202
// ReSharper disable StyleCop.SA1201
#pragma warning disable AsyncFixer02 // Long-running or blocking operations inside an async method
namespace Clarity.Ecommerce.Workflow.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Testing;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Models;
    using Moq;
    using Providers.Emails;
    using Workflow;
    using Xunit;

    [Trait("Category", "Workflows.Messaging.EmailQueues.Special")]
    public class Messaging_EmailQueues_SpecialWorkflowTests : XUnitLogHelper
    {
        public Messaging_EmailQueues_SpecialWorkflowTests(Xunit.Abstractions.ITestOutputHelper testOutputHelper) : base(testOutputHelper) { }

        private class MockWebClientFactory : IWebClientFactory
        {
            public IWebClient Create()
            {
                var mockWebClient = new Mock<IWebClient>();
                mockWebClient.Setup(m => m.DownloadString(It.IsAny<string>())).Returns("The Template");
                return mockWebClient.Object;
            }
        }

        private static IEmailQueueWorkflow GenerateWorkflow()
        {
            return new EmailQueueWorkflow { WebClientFactory = new MockWebClientFactory() };
        }

        [Fact(Skip = "Manual testing only")]
        public Task Verify_SendingEmails_Should_SendAnEmail()
        {
            return Task.WhenAll(
                Verify_SendingEmails_Should_SendAnEmailInnerAsync("SendInvitationEmail", "james.gray@claritymis.com"),
                Verify_SendingEmails_Should_SendAnEmailInnerAsync("SendInviteCodeEmail", "james.gray@claritymis.com", "someInviteCode"));
        }

        private static async Task Verify_SendingEmails_Should_SendAnEmailInnerAsync(string type, params object[] parameters)
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            BaseModelMapper.Initialize();
            var contextProfileName = $"Messaging_EmailQueues_SpecialWorkflowTests|Verify_SendingEmails_Should_SendAnEmailInner|{type}|{parameters?.Aggregate((c, n) => $"{c}|{n}")}";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoEmailQueueTable = true,
                    DoEmailStatusTable = true,
                    DoEmailTemplateTable = true,
                    DoEmailTypeTable = true,
                    DoMessaging = true,
                    DoSalesOrderTable = true,
                    DoOrdering = true,
                    DoSalesOrderItemTable = true,
                    DoSalesOrderTypeTable = true,
                    DoProductTable = true,
                    DoContacts = true,
                    DoContactTable = true,
                    DoContactTypeTable = true,
                    DoUserTable = true,
                    DoUserTypeTable = true,
                    DoUserStatusTable = true,
                    DoAccounts = true,
                    DoAccountContactTable = true,
                    DoAccountPricePointTable = true,
                    DoAccountStatusTable = true,
                    DoAccountTable = true,
                    DoAccountTypeTable = true,
                    DoAddressTable = true,
                    DoRegionTable = true,
                };
                await mockingSetup.DoMockingSetupForContextAsync(contextProfileName).ConfigureAwait(false);
                childContainer.Configure(x =>
                {
                    x.For<IClarityEcommerceEntities>().Use(() => mockingSetup.MockContext.Object);
                    x.AddRegistry(new DataModelTestingRegistry(mockingSetup));
                });
                RegistryLoader.OverrideContainer(childContainer, contextProfileName);
                var workflow = GenerateWorkflow();
                // Act
                CEFActionResponse<int> result;
                switch (type)
                {
                    case "SendInvitationEmail":
                    {
                        result = await new AuthenticationInvitationWithStaticTokenToCustomerEmail().QueueAsync(
                                to: parameters![0] as string,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        break;
                    }
                    case "SendInviteCodeEmail":
                    {
                        result = await new AuthenticationInvitationWithGeneratedTokenToCustomerEmail().QueueAsync(
                                contextProfileName: contextProfileName,
                                to: parameters![0] as string,
                                parameters: new()
                                {
                                    ["fromUserEmail"] = mockingSetup.MockContext.Object.Users.First().Email,
                                    ["token"] = parameters[1] as string,
                                })
                            .ConfigureAwait(false);
                        break;
                    }
                    default: throw new ArgumentException("Invalid email type, check your InlineData Attribute");
                }
                // Assert
                // TODO: Check the C:\Temp folder for outgoing emails that were dumped there
                Assert.True(result.ActionSucceeded);
                Assert.True(result.Result > 0);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        [Fact(Skip = "Manual Testing only")]
        public async Task Verify_QueueSalesOrderBackOfficeNotification_Should_SendMeAnEmail()
        {
            // Arrange
            JSConfigs.CEFConfigDictionary.Load();
            const string contextProfileName = "Messaging_EmailQueues_SpecialWorkflowTests|Verify_QueueSalesOrderBackOfficeNotification_Should_SendMeAnEmail";
            using (var childContainer = RegistryLoader.RootContainer.CreateChildContainer())
            {
                var mockingSetup = new MockingSetup
                {
                    SaveChangesResult = 1,
                    DoEmailQueueTable = true,
                    DoEmailTemplateTable = true,
                    DoEmailTypeTable = true,
                    DoEmailStatusTable = true,
                    DoSettingGroupTable = true,
                    DoSettingTable = true,
                    DoSettingTypeTable = true,
                    DoSalesOrderTable = true,
                    DoSalesOrderItemTable = true,
                    DoContactTable = true,
                    DoRateQuoteTable = true,
                    DoAddressTable = true,
                    DoRegionTable = true,
                    DoCountryTable = true,
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
                var workflow = new EmailQueueWorkflow();
                var order = mockingSetup.SalesOrders!.Object
                    .FilterByActive(true)
                    .OrderByDescending(x => x.ID)
                    .Take(1)
                    .Select(x => ModelMapperForSalesOrder.CreateSalesOrderModelFromEntityFull(x, contextProfileName))
                    .First();
                await new SalesOrdersSubmittedNormalToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = order })
                    .ConfigureAwait(false);
            }
            RegistryLoader.RemoveOverrideContainer(contextProfileName);
        }

        #region Sales
#pragma warning disable IDE0051 // Remove unused private members
        private static async Task<ISalesInvoiceItem> TransferOrderItemsToInvoiceItemsAsync(
#pragma warning restore IDE0051 // Remove unused private members
            int masterID,
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> orderItem,
            string contextProfileName)
        {
            var item = new DataModel.SalesInvoiceItem
            {
                // Base Properties
                Active = true,
                CustomKey = orderItem.CustomKey,
                CreatedDate = DateExtensions.GenDateTime,
                UpdatedDate = null,
                // NameableBaseProperties
                Name = orderItem.Name ?? orderItem.ProductName,
                Description = orderItem.Description ?? orderItem.ProductShortDescription,
                // SalesItemBase Properties
                Sku = orderItem.Sku ?? orderItem.ProductKey,
                ForceUniqueLineItemKey = orderItem.ForceUniqueLineItemKey,
                UnitOfMeasure = orderItem.UnitOfMeasure ?? orderItem.ProductUnitOfMeasure,
                Quantity = orderItem.Quantity,
                QuantityBackOrdered = orderItem.QuantityBackOrdered ?? 0m,
                QuantityPreSold = orderItem.QuantityPreSold ?? 0m,
                UnitCorePrice = orderItem.UnitCorePrice,
                UnitSoldPrice = GetModifiedValue(
                    orderItem.UnitSoldPrice ?? orderItem.UnitCorePrice,
                    orderItem.UnitSoldPriceModifier,
                    orderItem.UnitSoldPriceModifierMode),
                UnitCorePriceInSellingCurrency = orderItem.UnitCorePriceInSellingCurrency,
                UnitSoldPriceInSellingCurrency = orderItem.UnitSoldPriceInSellingCurrency,
                OriginalCurrencyID = orderItem.OriginalCurrencyID,
                SellingCurrencyID = orderItem.SellingCurrencyID,
                // Related Objects
                MasterID = masterID,
                ProductID = orderItem.ProductID,
                UserID = orderItem.UserID,
            };
            // Transfer Attributes
            await new AssociateJsonAttributesWorkflow().AssociateObjectsAsync(item, orderItem, contextProfileName).ConfigureAwait(false);
            // Transfer Discounts
            if (orderItem.Discounts?.Any() == true)
            {
                item.Discounts ??= new List<DataModel.AppliedSalesInvoiceItemDiscount>();
                foreach (var salesOrderItemDiscount in orderItem.Discounts.Select(x => new DataModel.AppliedSalesInvoiceItemDiscount
                {
                    ID = x.ID,
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    SlaveID = x.DiscountID,
                    DiscountTotal = x.DiscountTotal,
                    ApplicationsUsed = x.ApplicationsUsed,
                }))
                {
                    item.Discounts.Add(salesOrderItemDiscount);
                }
            }
            return item;
        }

        protected static decimal GetModifiedValue(decimal? baseAmount, decimal? modifier, int? mode)
        {
            mode ??= 2;
            modifier ??= 0;
            baseAmount ??= 0;
            switch ((Enums.TotalsModifierModes)mode.Value)
            {
                case Enums.TotalsModifierModes.Unknown: { return baseAmount.Value; } // No Change
                case Enums.TotalsModifierModes.Override: { return modifier.Value; }
                case Enums.TotalsModifierModes.Add: { return baseAmount.Value + modifier.Value; }
                case Enums.TotalsModifierModes.PercentMarkup: { return baseAmount.Value * ((modifier.Value + 100) / 100); }
                default: { throw new ArgumentException($"Invalid modifier mode: {mode.Value}"); }
            }
        }
        #endregion
    }
}
