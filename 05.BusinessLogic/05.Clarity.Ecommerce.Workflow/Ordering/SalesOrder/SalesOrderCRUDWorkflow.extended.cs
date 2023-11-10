// <copyright file="SalesOrderCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2015-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Providers.Emails;
    using ServiceStack;
    using Utilities;

    public partial class SalesOrderWorkflow
    {
        /// <summary>Initializes static members of the <see cref="SalesOrderWorkflow"/> class.</summary>
        static SalesOrderWorkflow()
        {
            // Ensure Hooks as needed
            try
            {
                if (CEFConfigDictionary.EmailNotificationsSalesOrderToCustomerByEmailOnSave)
                {
                    OnRecordUpdatedAsyncHook = (model, context)
                        => new SalesOrderUpdatedNotificationToCustomerEmail().QueueAsync(
                            contextProfileName: context.ContextProfileName,
                            to: null,
                            parameters: new() { ["salesOrder"] = model, });
                }
                ModelMapperForSalesOrderItem.CreateSalesOrderItemModelFromEntityHooksList = (entity, model, contextProfileName) =>
                {
                    if (Contract.CheckValidKey(contextProfileName))
                    {
                        return model;
                    }
                    model.Status = entity.Status;
                    return model;
                };
            }
            catch
            {
                // Do Nothing
            }
        }

        /// <inheritdoc/>
        public override Task<ISalesOrderModel?> GetAsync(int id, string? contextProfileName)
        {
            return GetAsync(id, null, contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<ISalesOrderModel?> GetAsync(string key, string? contextProfileName)
        {
            return GetAsync(null, key, contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<ISalesOrderModel> SecureSalesOrderAsync(
            int id,
            List<int> accountIDs,
            string? contextProfileName)
        {
            var model = await GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (Contract.CheckNotNull(model)
                && accountIDs.Exists(x => x == model!.AccountID)
                && model!.Active)
            {
                if (Contract.CheckNotNull(model.SalesGroupAsMasterID))
                {
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    var shipping = (await context.SalesOrders
                        .FilterSalesOrdersBySalesGroupAsSubID(model.SalesGroupAsMasterID, false)
                        .Select(x => new { Shipping = x.ShippingContact, ShippingID = x.ShippingContactID, ShippingKey = x.ShippingContact!.CustomKey })
                        .ToListAsync()
                        .ConfigureAwait(false))
                        .Select(x => new { Shipping = (ContactModel?)x.Shipping.CreateContactModelFromEntityFull(context.ContextProfileName), x.ShippingID, x.ShippingKey, })
                        .ToList();
                    model.ShippingContact = shipping.FirstOrDefault()?.Shipping;
                    model.ShippingContactID = shipping.FirstOrDefault()?.ShippingID;
                    model.ShippingContactKey = shipping.FirstOrDefault()?.ShippingKey;
                }
                return model;
            }
            throw HttpError.Unauthorized("Unauthorized to view this SalesOrder");
        }

        /// <inheritdoc/>
        public override Task<IEnumerable<ISalesOrderModel>> SearchForConnectAsync(
            ISalesOrderSearchModel model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var results = context.SalesOrders
                .AsNoTracking()
                .FilterByActive(model.Active)
                .FilterSalesOrderBySearchModel(model)
                .OrderByDescending(so => so.CreatedDate)
                .SelectListSalesOrderAndMapToSalesOrderModel(contextProfileName)
                .ToList();
            return Task.FromResult<IEnumerable<ISalesOrderModel>>(results);
        }

        /// <inheritdoc/>
        public async Task<IEnumerable<int?>> GetSalesOrdersDistinctProductsForAccountAsync(
            int accountID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.SalesOrderItems
                .AsNoTracking()
                .FilterByActive(true)
                .Include(x => x.Master)
                .Where(x => x.Master!.Active && x.Master.AccountID == accountID)
                .Select(x => x.ProductID)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<ISalesOrderModel?> GetByPayoneerOrderIDAsync(
            long payoneerOrderID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var orderID = await context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .FilterObjectsWithJsonAttributesByValues(
                    new() { ["Payoneer-Order-ID"] = new[] { payoneerOrderID.ToString(), }, })
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync();
            return Contract.CheckValidID(orderID)
                ? await GetAsync(orderID, null, contextProfileName).ConfigureAwait(false)
                : null;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<ISalesOrderModel?>> GetSalesOrderByUserAndEventAsync(
            int userID,
            int calendarEventID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var possibleProductIDs = await context.CalendarEvents
                .FilterByID(calendarEventID)
                .SelectMany(x => x.Products!
                    .Where(y => y.Active && y.Slave!.Active)
                    .Select(y => y.SlaveID))
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
            if (possibleProductIDs.Count == 0)
            {
                return CEFAR.FailingCEFAR<ISalesOrderModel?>("No Possible Products");
            }
            var entity = await context.SalesOrders
                .FilterByActive(true)
                .FilterSalesCollectionsByUserID<SalesOrder, SalesOrderStatus, SalesOrderType, SalesOrderItem, AppliedSalesOrderDiscount, SalesOrderState, SalesOrderFile, SalesOrderContact, SalesOrderEvent, SalesOrderEventType>(userID)
                .FilterSalesCollectionsBySalesItemProductIDs<SalesOrder, SalesOrderStatus, SalesOrderType, SalesOrderItem, AppliedSalesOrderDiscount, SalesOrderState, SalesOrderFile, SalesOrderContact, SalesOrderEvent, SalesOrderEventType, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(possibleProductIDs)
                .OrderByDescending(x => x.ID)
                .Take(1)
                .Select(x => ModelMapperForSalesOrder.MapSalesOrderModelFromEntityFull(x, contextProfileName))
                .FirstOrDefaultAsync();
            return entity == null
                ? CEFAR.FailingCEFAR<ISalesOrderModel?>()
                : entity.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> UpdateStatusOfSalesOrderAsync(
            string statusUpdate,
            int productID,
            int quantity,
            int salesOrderID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var confirmID = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Confirmed",
                    byName: "Confirmed",
                    byDisplayName: "Confirmed",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var approvedID = await Workflows.SalesQuoteStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Approved",
                    byName: "Approved",
                    byDisplayName: "Approved",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var voidID = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Void",
                    byName: "Void",
                    byDisplayName: "Void",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var futureAdjudicateID = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Future Adjudicate",
                    byName: "Future Adjudicate",
                    byDisplayName: "Future Adjudicate",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var updatedProduct = await context.Products
                .FilterByID(productID)
                .Select(x => new
                {
                    x.Name,
                    x.CustomKey,
                    x.PriceBase,
                    x.PriceSale,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var salesItem = await context.SalesOrderItems
                .SingleOrDefaultAsync(x => x.MasterID == salesOrderID)
                .ConfigureAwait(false);
            salesItem.Name = updatedProduct.Name;
            salesItem.Quantity = quantity;
            salesItem.Sku = updatedProduct.CustomKey;
            salesItem.UnitCorePrice = updatedProduct.PriceBase ?? 0m;
            salesItem.UnitSoldPrice = updatedProduct.PriceSale;
            salesItem.ProductID = productID;
            salesItem.MasterID = salesItem.MasterID;
            var salesOrderToUpdate = context.SalesOrders.SingleOrDefault(so => so.ID == salesOrderID);
            if (salesOrderToUpdate != null)
            {
                if (string.Equals(statusUpdate, "confirmed", StringComparison.OrdinalIgnoreCase))
                {
                    salesOrderToUpdate.StatusID = confirmID;
                    // get subscription and doctor update repeat type and billing periods total (number of refills - quantity?)
                    await ConfirmOrderAsync(salesOrderID, contextProfileName).ConfigureAwait(false);
                }
                if (statusUpdate.ToLower() == "void")
                {
                    salesOrderToUpdate.StatusID = voidID;
                    await VoidOrderAsync(salesOrderID, null, contextProfileName).ConfigureAwait(false);
                }
                if (statusUpdate.ToLower() == "future adjudicate")
                {
                    salesOrderToUpdate.StatusID = voidID;
                    var quote = new SalesQuote
                    {
                        Active = true,
                        SubtotalItems = salesOrderToUpdate.SubtotalItems,
                        SubtotalShipping = salesOrderToUpdate.SubtotalShipping,
                        SubtotalTaxes = salesOrderToUpdate.SubtotalTaxes,
                        SubtotalFees = salesOrderToUpdate.SubtotalFees,
                        SubtotalHandling = salesOrderToUpdate.SubtotalHandling,
                        SubtotalDiscounts = salesOrderToUpdate.SubtotalDiscounts,
                        ShippingSameAsBilling = salesOrderToUpdate.ShippingSameAsBilling,
                        BillingContactID = salesOrderToUpdate.BillingContactID,
                        ShippingContactID = salesOrderToUpdate.ShippingContactID,
                        StatusID = futureAdjudicateID,
                        StateID = salesOrderToUpdate.StateID, // Work = 1; History = 2 (SalesOrderState is the same)
                        TypeID = 1, // General = 1
                        UserID = salesOrderToUpdate.UserID,
                        AccountID = salesOrderToUpdate.AccountID,
                        JsonAttributes = salesOrderToUpdate.JsonAttributes,
                        StoreID = salesOrderToUpdate.StoreID,
                        BrandID = salesOrderToUpdate.BrandID,
                    };
                    await VoidOrderAsync(salesOrderID, null, contextProfileName).ConfigureAwait(false);
                    context.SalesQuotes.Add(quote);
                }
                if (statusUpdate.ToLower() == "approved")
                {
                    salesOrderToUpdate.StatusID = approvedID;
                    await ConfirmOrderAsync(salesOrderID, contextProfileName).ConfigureAwait(false);
                }
            }
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesOrderModel>(
                    "ERROR! Something about editing the sales order status, creating a quote or editing the sales order item went wrong.");
            }
            return salesOrderToUpdate.WrapInPassingCEFAR("The order has been updated");
        }

        /// <inheritdoc/>
        public async Task<(List<ISubscriptionModel> results, int totalPages, int totalCount)> GetOnDemandSubscriptionsByUserAsync(
            int userID,
            ISubscriptionSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (context.Subscriptions
                    .Where(s => !s.IsAutoRefill && s.UserID == userID)
                    .FilterByPaging(search.Paging, out var totalPages, out var totalCount)
                    .SelectListSubscriptionAndMapToSubscriptionModel(contextProfileName)
                    .ToList(),
                totalPages,
                totalCount);
        }

        /// <inheritdoc/>
        public async Task<ISubscriptionModel?> GetSubscriptionBySalesOrder(
            int salesOrderID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Subscriptions
                .FilterSubscriptionsBySalesOrderID(salesOrderID, false)
                .SelectFirstFullSubscriptionAndMapToSubscriptionModel(contextProfileName);
        }

        /// <inheritdoc/>
        public async Task<(List<ISubscriptionHistoryModel> results, int totalPages, int totalCount)> GetSubscriptionHistoryBySubID(
            int subscriptionID,
            ISubscriptionHistorySearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (context.SubscriptionHistories
                    .Where(sh => sh.MasterID == subscriptionID)
                    .FilterByPaging(search.Paging, out var totalPages, out var totalCount)
                    .SelectListSubscriptionHistoryAndMapToSubscriptionHistoryModel(contextProfileName)
                    .ToList(),
                totalPages,
                totalCount);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> RefillOnDemandSubscriptionAsync(
            int userID,
            int subscriptionID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var subscription = await context.Subscriptions
                .FilterByID(subscriptionID)
                .FilterSubscriptionsByUserID(userID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (subscription.BillingPeriodsTotal - subscription.BillingPeriodsPaid <= 0)
            {
                return CEFAR.FailingCEFAR<ISubscription>(
                    "ERROR! You have no refills remaining - please contact your doctor.");
            }
            // var productID = await context.ProductSubscriptionTypes
            //     .Where(pst => pst.ID == subscription.ProductSubscriptionTypeID)
            //     .Select(pst => pst.MasterID)
            //     .FirstOrDefaultAsync();
            // var product = context.Products
            //     .FirstOrDefault(p => p.ID == productID);
            // add product to cart - front end?
            subscription.BillingPeriodsPaid++;
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISubscription>(
                    "ERROR! Subscription could not be updated.");
            }
            return subscription.WrapInPassingCEFAR("The subscription has been updated");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> EditSalesOrderAsync(
            int salesOrderId,
            int productId,
            IPricingFactoryContextModel factoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var order = await context.SalesOrders
                .FilterByActive(true)
                .FilterByID(salesOrderId)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var item = await context.SalesOrderItems
                .FilterByActive(true)
                .Where(x => x.MasterID == salesOrderId)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            // this will work because we will eventually be modifying shipping so that each sales order only has one item
            var product = await context.Products
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(productId)
                .Select(x => new
                {
                    x.ID,
                    x.CustomKey,
                    x.Name,
                })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            var prices = await Workflows.PricingFactory.CalculatePriceAsync(
                    productID: product.ID,
                    pricingFactoryContext: factoryContext,
                    salesItemAttributes: null,
                    contextProfileName: null)
                .ConfigureAwait(false);
            item.ProductID = product.ID;
            item.UpdatedDate = DateExtensions.GenDateTime;
            item.Name = product.Name;
            item.Sku = product.CustomKey;
            item.UnitCorePrice = prices.BasePrice;
            item.UnitSoldPrice = prices.SalePrice;
            order.StatusID = await Workflows.SalesOrderStatuses.ResolveWithAutoGenerateToIDAsync(
                    null,
                    "Substitution Processing",
                    "Substitution Processing",
                    "Substitution Processing",
                    null,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesOrderItem>(
                    "ERROR! Sales order could not be updated.");
            }
            return item.WrapInPassingCEFAR("The sales order has been updated");
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CancelSubscriptionAsync(
            int salesOrderID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var subscription = await context.Subscriptions
                .FilterByActive(true)
                .Where(x => x.SalesOrderID == salesOrderID)
                .SingleOrDefaultAsync();
            if (subscription == null)
            {
                return CEFAR.FailingCEFAR<ISubscriptionModel>(
                    "ERROR! Something about cancelling the subscription went wrong.");
            }
            subscription.Active = false;
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISubscriptionModel>(
                    "ERROR! Something about cancelling the subscription went wrong.");
            }
            return subscription.WrapInPassingCEFAR("The subscription has been cancelled");
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateSalesOrderFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            // Associated Objects
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context.ContextProfileName).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            await AssociateSalesItemsAsync(model, entity, timestamp, context.ContextProfileName).ConfigureAwait(false);
            if (model.Notes != null)
            {
                if (Contract.CheckValidID(entity.ID))
                {
                    foreach (var note in model.Notes)
                    {
                        note.SalesOrderID = entity.ID;
                    }
                }
                await Workflows.SalesOrderWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            }
#pragma warning disable SA1501,format // Statement should not be on a single line
            if (model.SalesOrderPayments != null) { await Workflows.SalesOrderWithSalesOrderPaymentsAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false); }
            if (model.Contacts != null) { await Workflows.SalesOrderWithContactsAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false); }
            if (model.StoredFiles != null) { await Workflows.SalesOrderWithStoredFilesAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false); }
            if (model.RateQuotes != null) { await Workflows.SalesOrderWithRateQuotesAssociation.AssociateObjectsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false); }
#pragma warning restore SA1501,format // Statement should not be on a single line
        }

        /// <summary>Executes the limited relate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected async Task RunLimitedRelateWorkflowsAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunLimitedRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <summary>Runs limited relate workflows.</summary>
        /// <param name="entity">   The entity to map into.</param>
        /// <param name="model">    The model to map from.</param>
        /// <param name="timestamp">Update timestamp.</param>
        /// <param name="context">  The context to use.</param>
        /// <returns>A task to await.</returns>
        protected async Task RunLimitedRelateWorkflowsAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateOptionalInventoryLocationAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalStoreAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalBrandAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalAccountAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateOptionalUserAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredStateAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateBillingContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await RelateShippingContactAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesOrder>> FilterQueryByModelCustomAsync(
            IQueryable<SalesOrder> query,
            ISalesOrderSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterSalesOrdersByHasSalesGroupAsMaster(search.HasSalesGroupAsMaster)
                .FilterSalesOrdersByHasSalesGroupAsSub(search.HasSalesGroupAsSub);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            SalesOrder? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            // ReSharper disable once InvertIf
            if (context.Notes != null)
            {
                // Must wrap in null check for unit tests
                for (var i = 0; i < context.Notes.Count(x => x.SalesOrderID == entity.ID);)
                {
                    context.Notes.Remove(context.Notes.First(x => x.SalesOrderID == entity.ID));
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
            }
            // Delete the order itself
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesOrder>> FilterQueryByModelExtensionAsync(
            IQueryable<SalesOrder> query,
            ISalesOrderSearchModel search,
            IClarityEcommerceEntities context)
        {
            search.HasSalesGroupAsMaster = true;
            search.HasSalesGroupAsSub = null;
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterSalesOrdersByMasterOrders(search);
        }

        /// <summary>Gets a sales order by its identifier.</summary>
        /// <param name="id">                The identifier to get.</param>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ISalesOrderModel.</returns>
        private static async Task<ISalesOrderModel?> GetAsync(int? id, string? key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var salesOrder = context.SalesOrders
                ////.AsNoTracking()
                .FilterByActive(Contract.CheckValidID(id) ? null : true)
                .FilterByID(id)
                .FilterByCustomKey(key, true)
                .SelectFirstFullSalesOrderAndMapToSalesOrderModel(contextProfileName);
            // ReSharper disable once InvertIf
            if (salesOrder?.SalesItems != null && context.Products != null)
            {
                foreach (var soi in salesOrder.SalesItems.Where(x => Contract.CheckValidID(x.ProductID)))
                {
                    soi.ProductDownloadsNew = await context.Products
                        .AsNoTracking()
                        .FilterByID(soi.ProductID)
                        .SelectMany(x => x.StoredFiles!)
                        .Where(pf => pf.Slave != null && pf.Slave.FileName != null)
                        .Select(pf => pf.Slave!.FileName!)
                        .ToListAsync()
                        .ConfigureAwait(false);
                }
            }
            return salesOrder;
        }

        /// <summary>Relate Optional BillingContact.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional BillingContact.</param>
        /// <param name="model">    The model that has a Optional BillingContact.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private async Task RelateBillingContactAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Contacts.ResolveWithAutoGenerateOptionalAsync(
                    model.BillingContactID, // By Other ID
                    model.BillingContactKey, // By Flattened Other Key
                    model.BillingContact,
                    context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.BillingContactID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.BillingContact == null;
            if (resolved.Result == null && model.BillingContact != null)
            {
                resolved.Result = model.BillingContact;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.BillingContactID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.BillingContact!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.BillingContact!.UpdateContactFromModel(model.BillingContact!, timestamp, timestamp);
                    // Update the Address Properties as well if available
                    if (model.BillingContact?.Address != null)
                    {
                        if (entity.BillingContact!.Address == null)
                        {
                            entity.BillingContact.Address = (Address)model.BillingContact.Address.CreateAddressEntity(timestamp, context.ContextProfileName);
                        }
                        else
                        {
                            entity.BillingContact.Address.UpdateAddressFromModel(model.BillingContact.Address, timestamp, timestamp);
                        }
                    }
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.BillingContactID = resolved.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                    entity.BillingContactID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.BillingContactID = null;
                entity.BillingContact = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.BillingContactID = null;
                entity.BillingContact = (Contact)resolved.Result!.CreateContactEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
                entity.BillingContact = (Contact)resolved.Result!.CreateContactEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.BillingContactID = null;
                entity.BillingContact = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.BillingContact.UpdateContactFromModel(resolved, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given BillingContact to the SalesOrder entity");
        }

        /// <summary>Relate Optional ShippingContact.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Optional ShippingContact.</param>
        /// <param name="model">    The model that has a Optional ShippingContact.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private async Task RelateShippingContactAsync(
            ISalesOrder entity,
            ISalesOrderModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.Contacts.ResolveWithAutoGenerateOptionalAsync(
                    model.ShippingContactID, // By Other ID
                    model.ShippingContactKey, // By Flattened Other Key
                    model.ShippingContact,
                    context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.ShippingContactID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.ShippingContact == null;
            if (resolved.Result == null && model.ShippingContact != null)
            {
                resolved.Result = model.ShippingContact;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (entityIDIsNull && modelIDIsNull && entityObjectIsNull && modelObjectIsNull)
            {
                // [Optional] Scenario 1: Nothing we can do with/to either end, so do nothing
                return;
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.ShippingContactID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.ShippingContact!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.ShippingContact!.UpdateContactFromModel(model.ShippingContact!, timestamp, timestamp);
                    // Update the Address Properties as well if available
                    if (model.ShippingContact?.Address != null)
                    {
                        var resolvedAddress = await Workflows.Addresses.ResolveAddressAsync(
                                model.ShippingContact.Address,
                                context.ContextProfileName)
                            .ConfigureAwait(false);
                        if (entity.ShippingContact!.Address == null)
                        {
                            entity.ShippingContact.Address = (Address)resolvedAddress.CreateAddressEntity(timestamp, context.ContextProfileName);
                        }
                        else
                        {
                            entity.ShippingContact.Address.UpdateAddressFromModel(resolvedAddress, timestamp, timestamp);
                        }
                        // Above does not handle region or country, so do those manually
                        entity.ShippingContact.Address.Region = null;
                        entity.ShippingContact.Address.RegionID = resolvedAddress.RegionID;
                        entity.ShippingContact.Address.Country = null;
                        entity.ShippingContact.Address.CountryID = resolvedAddress.CountryID;
                    }
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.ShippingContactID = resolved.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIsNull && !modelObjectIDIsNull)
            {
                if (modelObjectIsActive)
                {
                    // [Optional] Scenario 4: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                    entity.ShippingContactID = resolved.Result!.ID;
                    return;
                }
                // [Optional] Scenario 5: We have IDs but they don't match and the model has been deactivated, remove the entity from it's master
                entity.ShippingContactID = null;
                entity.ShippingContact = null;
                return;
            }
            if (!entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 6: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.ShippingContactID = null;
                entity.ShippingContact = (Contact)resolved.Result!.CreateContactEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (entityIDIsNull && modelObjectIsActive)
            {
                // [Optional] Scenario 7: We don't have an entity id, and we have a new model, assign the new model
                entity.ShippingContact = (Contact)resolved.Result!.CreateContactEntity(timestamp, context.ContextProfileName);
                return;
            }
            if (!entityIDIsNull && modelIDIsNull && !modelObjectIsActive)
            {
                // [Optional] Scenario 8: We were removing or deactivating the object, clear it from the entity
                entity.ShippingContactID = null;
                entity.ShippingContact = null;
                return;
            }
            // Note: We would do this if it wasn't just a base typable entity (like ReportType, which has extra values to adjust on the model)
            // if (!entityObjectIsNull && modelObjectIsActive && entityObjectAndModelObjectHaveSameID)
            // {
            //     // TODO: Determine 'Equals' object between the objects so we only update if different
            //     // [Optional] Scenario 9: We have data on both sides, update the object, assign the values using the Update action
            //     entity.ShippingContact.UpdateContactFromModel(resolved.Result, updateTimestamp);
            //     return;
            // }
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given ShippingContact to the SalesOrder entity");
        }
    }
}
