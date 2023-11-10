// <copyright file="StandardActionsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard actions provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Emails;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A standard sales quote actions provider.</summary>
    /// <seealso cref="SalesQuoteActionsProviderBase"/>
    public class StandardSalesQuoteActionsProvider : SalesQuoteActionsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSalesQuoteActionsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <summary>Gets the identifier of the shipping type.</summary>
        /// <value>The identifier of the shipping type.</value>
        protected static int ShippingTypeID { get; private set; }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SubmitRFQForSingleProductAsync(
            ISalesQuoteModel model,
            bool doRecommend,
            bool doShare,
            string? contextProfileName)
        {
            try
            {
                await ValidateRfqForSingleProductAsync(model).ConfigureAwait(false);
            }
            catch (InvalidOperationException e)
            {
                return CEFAR.FailingCEFAR(e.Message);
            }
            // Clean up the incoming data as it is very lite
            model.TypeID = model.StateID = model.StatusID = 0;
            model.StatusKey = "Submitted";
            model.TypeKey = "Request for Quote (Single Item)";
            model.StateKey = "WORK";
            if (model.Notes?.Any() == true)
            {
                foreach (var note in model.Notes)
                {
                    note.CreatedByUserID = model.UserID;
                }
            }
            if (doRecommend)
            {
                model.SerializableAttributes ??= new();
                model.SerializableAttributes["Recommend-Other-Suppliers-After-24-Hours"] = new()
                {
                    Key = "Recommend-Other-Suppliers-After-24-Hours",
                    Value = true.ToString(),
                };
            }
            if (doShare)
            {
                var account = Contract.RequiresNotNull(
                    await Workflows.Accounts.GetAsync(
                        model.AccountID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false));
                var accountContact = account.AccountContacts!.Find(x => x.IsBilling)
                    ?? account.AccountContacts.Find(x => !x.IsPrimary)
                    ?? account.AccountContacts.FirstOrDefault();
                if (accountContact != null)
                {
                    model.BillingContactID = accountContact.ContactID;
                }
            }
            model.Totals.SubTotal = model.SalesItems!.Sum(x => x.ExtendedPrice);
            // Generate a sales group for the request
            var salesGroupResponse = await BuildSalesGroupAsync(
                    model,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!salesGroupResponse.ActionSucceeded || salesGroupResponse.Result == null)
            {
                return CEFAR.FailingCEFAR("ERROR! Something about generating and saving the sales group failed.");
            }
            model.SalesGroupAsRequestMaster = salesGroupResponse.Result;
            // Convert the model to an entity and save it
            var entity = (await Workflows.SalesQuotes.CreateEntityWithoutSavingAsync(
                        model: model,
                        timestamp: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .Result;
            foreach (var salesItem in entity!.SalesItems!.Where(x => x.UserID == 0))
            {
                salesItem.UserID = null;
            }
            ////RunDefaultRelateWorkflows(entity, model, entity.CreatedDate);
            ////RunDefaultAssociateWorkflows(entity, model, entity.CreatedDate);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.SalesQuotes.Add(entity);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR("ERROR! Failed to save the data");
                }
            }
            var updatedModel = Contract.RequiresNotNull(
                await Workflows.SalesQuotes.GetAsync(
                    entity.ID,
                    contextProfileName)
                .ConfigureAwait(false));
            var result = CEFAR.PassingCEFAR();
            CEFActionResponse<int>? emailResult1 = null;
            try
            {
                emailResult1 = await new SalesQuoteRFQSubmittedNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = updatedModel, })
                    .ConfigureAwait(false);
            }
            catch
            {
                result.Messages.Add("WARNING! There was an issue sending the email to the user");
                if (emailResult1 != null)
                {
                    result.Messages.AddRange(emailResult1.Messages);
                }
            }
            try
            {
                await new SalesQuoteRFQSubmittedNotificationToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = updatedModel, })
                    .ConfigureAwait(false);
            }
            catch
            {
                // Do Nothing
            }
            try
            {
                await new SalesQuoteRFQSubmittedNotificationToBackOfficeStoreEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = updatedModel, })
                    .ConfigureAwait(false);
            }
            catch
            {
                // Do Nothing
            }
            return result;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SubmitRFQForGenericProductsAsync(
            ISalesQuoteModel model,
            bool doShare,
            string? contextProfileName)
        {
            Contract.RequiresInvalidID(model.ID);
            Contract.RequiresNotNull(model.SalesItems);
            Contract.RequiresValidID(model.UserID);
            Contract.RequiresValidID(model.AccountID);
            // Clean up the incoming data as it is very lite
            model.TypeID = model.StateID = model.StatusID = 0;
            model.StatusKey = "Submitted";
            model.TypeKey = "Request for Quote (Generic Item)";
            model.StateKey = "WORK";
            if (model.Notes?.Any() == true)
            {
                foreach (var note in model.Notes)
                {
                    note.CreatedByUserID = model.UserID;
                }
            }
            if (doShare)
            {
                var account = Contract.RequiresNotNull(
                    await Workflows.Accounts.GetAsync(
                            model.AccountID!.Value,
                            contextProfileName)
                        .ConfigureAwait(false));
                var accountContact = account.AccountContacts!.Find(x => x.IsBilling)
                    ?? account.AccountContacts.Find(x => !x.IsPrimary)
                    ?? account.AccountContacts.FirstOrDefault();
                if (accountContact != null)
                {
                    model.BillingContactID = accountContact.ContactID;
                }
            }
            // Convert the model to an entity and save it
            var entity = (await Workflows.SalesQuotes.CreateEntityWithoutSavingAsync(
                        model: model,
                        timestamp: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .Result;
            ////RunDefaultRelateWorkflows(entity, model, entity.CreatedDate);
            ////RunDefaultAssociateWorkflows(entity, model, entity.CreatedDate);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.SalesQuotes.Add(entity!);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR("Failed to save the data");
                }
            }
            var updatedModel = Contract.RequiresNotNull(
                await Workflows.SalesQuotes.GetAsync(
                    entity!.ID,
                    contextProfileName)
                .ConfigureAwait(false));
            var result = CEFAR.PassingCEFAR();
            CEFActionResponse<int>? emailResult1 = null;
            try
            {
                emailResult1 = await new SalesQuoteRFQSubmittedNotificationToCustomerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = updatedModel, })
                    .ConfigureAwait(false);
                if (!emailResult1.ActionSucceeded)
                {
                    throw new("There was an issue sending the email to the user");
                }
            }
            catch
            {
                result.Messages.Add("There was an issue sending the email to the user");
                if (emailResult1 != null)
                {
                    result.Messages.AddRange(emailResult1.Messages);
                }
            }
            try
            {
                var emailResult2 = await new SalesQuoteRFQSubmittedNotificationToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = updatedModel, })
                    .ConfigureAwait(false);
                if (!emailResult2.ActionSucceeded)
                {
                    throw new("There was an issue sending the email to the user");
                }
            }
            catch
            {
                // Do Nothing
            }
            return result;
        }

        /// <inheritdoc/>
        public override async Task<bool> SetRecordsToExpiredAsync(string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                if (!int.TryParse(
                    context.Settings.FirstOrDefault(x => x.CustomKey == "SalesQuoteDaysToExpire")?.Value,
                    out var daysToExpire))
                {
                    return true;
                }
                var expirationDate = DateTime.UtcNow.AddDays(-daysToExpire);
                var statusID = await Workflows.SalesQuoteStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Expired",
                        byName: "Expired",
                        byDisplayName: "Expired",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                foreach (var expiredQuote in context.SalesQuotes
                    .FilterByActive(true)
                    .Where(q => q.CreatedDate < expirationDate && q.Status!.Name != "Expired"))
                {
                    expiredQuote.StatusID = statusID;
                }
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsInProcessAsync(int id, string? contextProfileName)
        {
            return ChangeStateAsync(id, "In Process", "WORK", contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsProcessedAsync(int id, string? contextProfileName)
        {
            return ChangeStateAsync(id, "Processed", "WORK", contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsApprovedAsync(int id, string? contextProfileName)
        {
            return ChangeStateAsync(id, "Approved", "HISTORY", contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsRejectedAsync(int id, string? contextProfileName)
        {
            return ChangeStateAsync(id, "Rejected", "HISTORY", contextProfileName);
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsVoidAsync(int id, string? contextProfileName)
        {
            return ChangeStateAsync(id, "Void", "HISTORY", contextProfileName);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> AwardLineItemAsync(
            int originalItemID,
            int responseItemID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(originalItemID);
            Contract.RequiresValidID(responseItemID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            ISalesQuoteItem original = await context.SalesQuoteItems.FilterByID(originalItemID).SingleAsync().ConfigureAwait(false),
                response = await context.SalesQuoteItems.FilterByID(responseItemID).SingleAsync().ConfigureAwait(false);
            Contract.RequiresNotNull(original);
            Contract.RequiresNotNull(response);
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false)).BoolToCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<(int orderID, int? invoiceID)>> ConvertQuoteToOrderAsync(
            int id,
            string? contextProfileName)
        {
            var quote = await Workflows.SalesQuotes.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (quote == null)
            {
                return CEFAR.FailingCEFAR<(int orderID, int? invoiceID)>($"ERROR! Could not get quote by ID {id}");
            }
            var quoteItems = quote.SalesItems!.Where(x => x.Active).ToList();
            quoteItems.ForEach(x => x.ProductKey = x.CustomKey);
            WipeIDsFromMainContacts(quote);
            var nothingToShip = quote.SalesItems!.All(x => x.ProductNothingToShip);
            IContact? shippingContact = null;
            if (quote.ShippingContact != null)
            {
                shippingContact = nothingToShip
                    ? null
                    : (await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                                model: WipeIDsFromContact(quote.ShippingContact!)!,
                                timestamp: null,
                                contextProfileName)
                            .ConfigureAwait(false))
                        .Result;
                if (!nothingToShip && shippingContact != null)
                {
                    if (Contract.CheckInvalidID(ShippingTypeID))
                    {
                        ShippingTypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                                byID: null,
                                byKey: "Shipping",
                                byName: "Shipping",
                                model: null,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
                    shippingContact.TypeID = ShippingTypeID;
                }
            }
            var orderItems = quoteItems
                .Select(x => new SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>
                {
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    Name = x.Name,
                    CustomKey = x.CustomKey,
                    DateReceived = x.DateReceived,
                    Description = x.Description,
                    ExtendedPriceInSellingCurrency = x.ExtendedPriceInSellingCurrency,
                    ExtendedShippingAmount = x.ExtendedShippingAmount,
                    ExtendedTaxAmount = x.ExtendedTaxAmount,
                    Hash = x.Hash,
                    ItemType = x.ItemType,
                    MasterID = x.MasterID,
                    MaxAllowedInCart = x.MaxAllowedInCart,
                    OriginalCurrencyID = x.OriginalCurrencyID,
                    ProductID = x.ProductID,
                    Quantity = x.Quantity,
                    QuantityBackOrdered = x.QuantityBackOrdered,
                    QuantityPreSold = x.QuantityPreSold,
                    RestockingFeeAmount = x.RestockingFeeAmount,
                    SellingCurrencyID = x.SellingCurrencyID,
                    SerializableAttributes = x.SerializableAttributes,
                    Sku = x.Sku,
                    ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                    UnitCorePrice = x.UnitCorePrice,
                    UnitSoldPrice = x.UnitSoldPrice,
                    UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                    UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                    UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode,
                    UnitSoldPriceModifier = x.UnitSoldPriceModifier,
                    UnitOfMeasure = x.UnitOfMeasure,
                    UserID = x.UserID,
                    UpdatedDate = x.UpdatedDate,
                    // Targets = x.Targets,
                    // Notes = x.Notes,
                    // Discounts = x.Discounts,
                })
                .ToList();
            var tempOrder = new SalesOrderModel
            {
                Active = true,
                CreatedDate = DateExtensions.GenDateTime,
                Totals = (CartTotals)quote.Totals!,
                StatusKey = "Pending",
                StateKey = "WORK",
                TypeKey = "From Quote",
                StoreID = quote.StoreID,
                AccountID = quote.AccountID,
                UserID = quote.UserID,
                ShippingContact =
                    nothingToShip ? null : (ContactModel?)shippingContact?.CreateContactModelFromEntityFull(contextProfileName),
                BalanceDue = quote.Totals!.Total,
                OriginalDate = quote.OriginalDate ?? quote.CreatedDate,
                SerializableAttributes = quote.SerializableAttributes,
                SalesItems = orderItems,
            };
            // Transfer Discounts
            if (Contract.CheckEmpty(quote.Discounts))
            {
                // Try to load them if it looks like they aren't loaded
                using var context2 = RegistryLoaderWrapper.GetContext(contextProfileName);
                var discountSearchModel = RegistryLoaderWrapper.GetInstance<IAppliedSalesQuoteDiscountSearchModel>(contextProfileName);
                discountSearchModel.Active = true;
                discountSearchModel.MasterID = quote.ID;
                quote.Discounts = context2.AppliedSalesQuoteDiscounts
                    .AsNoTracking()
                    .FilterAppliedSalesQuoteDiscountsBySearchModel(discountSearchModel)
                    .SelectListAppliedSalesQuoteDiscountAndMapToAppliedSalesQuoteDiscountModel(contextProfileName)
                    .ToList();
            }
            if (quote.Discounts != null && quote.Discounts.Any(x => x.Active))
            {
                var orderDiscounts = tempOrder.Discounts ?? new List<AppliedSalesOrderDiscountModel>();
                foreach (var quoteItemDiscountModel in quote.Discounts.Where(x => x.Active))
                {
                    orderDiscounts.Add(new()
                    {
                        // Base Properties
                        ID = quoteItemDiscountModel.ID,
                        CustomKey = quoteItemDiscountModel.CustomKey,
                        Active = quoteItemDiscountModel.Active,
                        CreatedDate = quoteItemDiscountModel.CreatedDate,
                        UpdatedDate = quoteItemDiscountModel.UpdatedDate,
                        SerializableAttributes = quoteItemDiscountModel.SerializableAttributes,
                        // Applied Discount Properties
                        SlaveID = quoteItemDiscountModel.DiscountID,
                        DiscountTotal = quoteItemDiscountModel.DiscountTotal,
                        ApplicationsUsed = quoteItemDiscountModel.ApplicationsUsed,
                    });
                }
                tempOrder.Discounts = orderDiscounts;
            }
            var orderCreateResponse = await Workflows.SalesOrders.CreateAsync(tempOrder, contextProfileName)
                .AwaitAndThrowIfFailedAsync()
                .ConfigureAwait(false);
            var order = await Workflows.SalesOrders.GetAsync(orderCreateResponse.Result, contextProfileName).ConfigureAwait(false);
            // Associate order and quote or order and group depending on settings
            if (CEFConfigDictionary.SplitShippingEnabled)
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var orderEntity = await context.SalesOrders
                    .FilterByID(order!.ID)
                    .SingleAsync()
                    .ConfigureAwait(false);
                order.SalesGroupAsMasterID = orderEntity.SalesGroupAsMasterID = quote.SalesGroupAsRequestMasterID
                    ?? quote.SalesGroupAsResponseMasterID;
                order.SalesGroupAsSubID = orderEntity.SalesGroupAsSubID = quote.SalesGroupAsRequestSubID
                    ?? quote.SalesGroupAsResponseSubID;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                if (Contract.CheckValidID(order.SalesGroupAsMasterID))
                {
                    // Since this was the master, look up and convert the subs too
                    var subQuotes = context.SalesQuotes
                        .FilterByActive(true)
                        .Where(x => x.SalesGroupAsRequestSubID == order.SalesGroupAsMasterID);
                    foreach (var subQuote in subQuotes)
                    {
                        var subQuoteNothingToShip = subQuote.SalesItems!.All(x => x.Product is { NothingToShip: true });
                        IContact? subQuoteShippingContact = null;
                        if (subQuote.ShippingContact != null)
                        {
                            subQuoteShippingContact = subQuoteNothingToShip
                                ? null
                                : (await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                                            model: WipeIDsFromContact(subQuote.ShippingContact?.CreateContactModelFromEntityFull(contextProfileName)!)!,
                                            timestamp: null,
                                            contextProfileName)
                                        .ConfigureAwait(false))
                                    .Result;
                            if (!subQuoteNothingToShip && subQuoteShippingContact != null)
                            {
                                if (Contract.CheckInvalidID(ShippingTypeID))
                                {
                                    ShippingTypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                                            byID: null,
                                            byKey: "Shipping",
                                            byName: "Shipping",
                                            model: null,
                                            contextProfileName: contextProfileName)
                                        .ConfigureAwait(false);
                                }
                                subQuoteShippingContact.TypeID = ShippingTypeID;
                            }
                        }
                        var subOrderItems = subQuote.SalesItems!
                            .Where(x => x.Active)
                            .Select(x => new SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>
                            {
                                Active = true,
                                CreatedDate = x.CreatedDate,
                                Name = x.Name,
                                CustomKey = x.CustomKey,
                                Description = x.Description,
                                Hash = x.Hash,
                                MasterID = x.MasterID,
                                OriginalCurrencyID = x.OriginalCurrencyID,
                                ProductID = x.ProductID,
                                Quantity = x.Quantity,
                                QuantityBackOrdered = x.QuantityBackOrdered,
                                QuantityPreSold = x.QuantityPreSold,
                                SellingCurrencyID = x.SellingCurrencyID,
                                SerializableAttributes = x.SerializableAttributes,
                                Sku = x.Sku,
                                ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                                UnitCorePrice = x.UnitCorePrice,
                                UnitSoldPrice = x.UnitSoldPrice,
                                UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                                UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                                UnitSoldPriceModifierMode = x.UnitSoldPriceModifierMode,
                                UnitSoldPriceModifier = x.UnitSoldPriceModifier,
                                UnitOfMeasure = x.UnitOfMeasure,
                                UserID = x.UserID,
                                UpdatedDate = x.UpdatedDate,
                                // Targets = x.Targets,
                                // Notes = x.Notes,
                                // Discounts = x.Discounts,
                            })
                            .ToList();
                        var subOrderCreateResponse = await Workflows.SalesOrders.CreateAsync(
                                new SalesOrderModel
                                {
                                    Active = true,
                                    CreatedDate = DateExtensions.GenDateTime,
                                    Totals = new()
                                    {
                                        // TODO: Split the discounts applied to the order per sub-order
                                        Discounts = subQuote.SubtotalDiscounts,
                                        Fees = subQuote.SubtotalFees,
                                        Handling = subQuote.SubtotalHandling,
                                        Shipping = subQuote.SubtotalShipping,
                                        SubTotal = subQuote.SubtotalItems,
                                        Tax = subQuote.SubtotalTaxes,
                                    },
                                    StatusKey = "Pending",
                                    StateKey = "WORK",
                                    TypeKey = "From Sub-Quote",
                                    StoreID = subQuote.StoreID,
                                    AccountID = subQuote.AccountID,
                                    UserID = subQuote.UserID,
                                    ShippingContact = subQuoteNothingToShip
                                        ? null
                                        : (ContactModel?)subQuoteShippingContact?.CreateContactModelFromEntityFull(contextProfileName),
                                    ShippingContactID = subQuoteNothingToShip
                                        ? null
                                        : subQuote.ShippingContactID ?? subQuote.ShippingContact?.ID,
                                    BalanceDue = subQuote.Total,
                                    OriginalDate = subQuote.CreatedDate,
                                    SerializableAttributes = subQuote.SerializableAttributes,
                                    SalesItems = subOrderItems,
                                },
                                contextProfileName)
                            .ConfigureAwait(false);
                        using var context2 = RegistryLoaderWrapper.GetContext(contextProfileName);
                        var subOrderEntity = await context2.SalesOrders
                            .FilterByID(subOrderCreateResponse.Result)
                            .SingleAsync()
                            .ConfigureAwait(false);
                        subOrderEntity.SalesGroupAsSubID = order.SalesGroupAsMasterID;
                        await context2.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                    }
                }
            }
            else
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                context.SalesQuoteSalesOrders.Add(new()
                {
                    Active = true,
                    MasterID = quote.ID,
                    SlaveID = order!.ID,
                    CustomKey = Contract.CheckAllValidKeys(order.CustomKey, quote.CustomKey)
                        ? $"Q:{quote.CustomKey}|O:{order.CustomKey}"
                        : $"Q:{quote.ID}|O:{order.ID}",
                });
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            // Mark the quote as approved and history now that it's been converted to an order
            await SetRecordAsApprovedAsync(quote.ID, contextProfileName).ConfigureAwait(false);
            // If invoices are enabled, generate one (they should be if you are using this process)
            var invoiceProvider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName);
            if (invoiceProvider is not null)
            {
                var invoiceResponse = await invoiceProvider.CreateSalesInvoiceFromSalesOrderAsync(
                        salesOrder: order,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return (order.ID, invoiceResponse.Result?.ID).WrapInPassingCEFAR();
            }
            return (order.ID, (int?)null).WrapInPassingCEFAR();
        }

        /// <summary>Wipe IDs from main contacts.</summary>
        /// <param name="coll">The sales collection.</param>
        protected static void WipeIDsFromMainContacts(ISalesCollectionBaseModel coll)
        {
            // Wipe the IDs from the contacts so they are forcefully regenerated
            WipeIDsFromContact(coll.ShippingContact);
        }

        /// <summary>Wipe IDs from contact.</summary>
        /// <param name="contact">The contact.</param>
        /// <returns>An IContactModel.</returns>
        protected static IContactModel? WipeIDsFromContact(IContactModel? contact)
        {
            if (!Contract.CheckValidID(contact?.ID))
            {
                return contact;
            }
            contact!.ID = 0;
            if (!Contract.CheckValidID(contact.Address?.ID))
            {
                return contact;
            }
            contact.AddressID = contact.Address!.ID = 0;
            return contact;
        }

        /// <summary>Validates the RFQ for single product described by model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>A Task.</returns>
        private static Task ValidateRfqForSingleProductAsync(ISalesQuoteModel model)
        {
            Contract.RequiresInvalidID(model.ID);
            Contract.RequiresNotNull(model.SalesItems);
            if (CEFConfigDictionary.SalesQuoteRequiresStoreID)
            {
                Contract.RequiresValidID(model.StoreID);
            }
            else if (CEFConfigDictionary.SalesQuoteRequiresStoreIDOrKey)
            {
                Contract.RequiresValidIDOrKey(model.StoreID, model.StoreKey);
            }
            Contract.Requires<InvalidOperationException>(model.SalesItems!.Count > 0);
            Contract.RequiresAllValidIDs(
                model.SalesItems[0].ProductID,
                model.UserID,
                model.AccountID);
            return Task.CompletedTask;
        }

        /// <summary>Builds sales group.</summary>
        /// <param name="quoteModel">        The quote model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{ISalesGroupModel}}.</returns>
        private static async Task<CEFActionResponse<ISalesGroupModel>> BuildSalesGroupAsync(
            ISalesQuoteModel quoteModel,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            var salesGroup = RegistryLoaderWrapper.GetInstance<ISalesGroup>(contextProfileName);
            salesGroup.Active = true;
            salesGroup.CreatedDate = timestamp;
            salesGroup.JsonAttributes = "{}";
            salesGroup.BrandID = quoteModel.BrandID;
            salesGroup.AccountID = quoteModel.AccountID;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.SalesGroups.Add((SalesGroup)salesGroup);
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesGroupModel>(
                    "ERROR! Something about creating and saving the sales group failed.");
            }
            return (await Workflows.SalesGroups.GetAsync(salesGroup.ID, contextProfileName).ConfigureAwait(false))
                .WrapInPassingCEFAR()!;
        }

        /// <summary>Change state.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="statusKey">         The status key.</param>
        /// <param name="stateKey">          The state key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static async Task<CEFActionResponse> ChangeStateAsync(
            int id,
            string statusKey,
            string stateKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesQuotes
                .FilterByID(Contract.RequiresValidID(id))
                .SingleAsync()
                .ConfigureAwait(false);
            var dummy = new SalesQuoteModel { StatusKey = statusKey, StateKey = stateKey };
            var timestamp = DateExtensions.GenDateTime;
            await Task.WhenAll(
                    RelateRequiredStatusAsync(entity, dummy, timestamp, contextProfileName),
                    RelateRequiredStateAsync(entity, dummy, timestamp, contextProfileName))
                .ConfigureAwait(false);
            entity.UpdatedDate = timestamp;
            return (await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save the data.");
        }

        /// <summary>Relate Required Status.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Status.</param>
        /// <param name="model">             The model that has a Required Status.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredStatusAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredStatusAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Status.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Status.</param>
        /// <param name="model">    The model that has a Required Status.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredStatusAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesQuoteStatuses.ResolveWithAutoGenerateAsync(
                    byID: model.StatusID, // By Other ID
                    byKey: model.StatusKey, // By Flattened Other Key
                    byName: model.StatusName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Status,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StatusID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Status == null;
            if (resolved.Result == null && model.Status != null)
            {
                resolved.Result = model.Status;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable StatusID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StatusID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Status!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Status!.UpdateSalesQuoteStatusFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StatusID = resolved.Result!.ID;
                // ReSharper disable once InvertIf
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
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive StatusID to the SalesQuote entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.StatusID = resolved.Result!.ID;
                return;
            }
            // ReSharper disable once InvertIf
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StatusID = 0;
                entity.Status = (SalesQuoteStatus)resolved.Result!.CreateSalesQuoteStatusEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Status to the SalesQuote entity");
        }

        /// <summary>Relate Required State.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required State.</param>
        /// <param name="model">             The model that has a Required State.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredStateAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredStateAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required State.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required State.</param>
        /// <param name="model">    The model that has a Required State.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredStateAsync(
            ISalesQuote entity,
            ISalesQuoteModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesQuoteStates.ResolveWithAutoGenerateAsync(
                    byID: model.StateID, // By Other ID
                    byKey: model.StateKey, // By Flattened Other Key
                    byName: model.StateName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.State,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StateID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.State == null;
            if (resolved.Result == null && model.State != null)
            {
                resolved.Result = model.State;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable StateID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StateID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.State!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.State!.UpdateSalesQuoteStateFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StateID = resolved.Result!.ID;
                // ReSharper disable once InvertIf
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
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive StateID to the SalesQuote entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.StateID = resolved.Result!.ID;
                return;
            }
            // ReSharper disable once InvertIf
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StateID = 0;
                entity.State = (SalesQuoteState)resolved.Result!.CreateSalesQuoteStateEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given State to the SalesQuote entity");
        }
    }
}
