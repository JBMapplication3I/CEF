// <copyright file="TargetQuoteSubmitProvider.Create.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target quote submit provider class</summary>
// ReSharper disable MultipleSpaces
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.TargetQuote
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

    public partial class TargetQuoteSubmitProvider
    {
        /// <summary>Handles the quote status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="accountIsOnHold">   The account is on hold.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task HandleQuoteStatusAsync(
            int id,
            int? accountID,
            bool? accountIsOnHold,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var master = await context.SalesQuotes.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(accountID))
            {
                master.StatusID = QuoteStatusSubmittedID;
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return;
            }
            var useOnHold = accountIsOnHold ?? false;
            master.StatusID = useOnHold ? QuoteStatusOnHoldID : QuoteStatusSubmittedID;
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
        }

        /// <summary>Creates master sales items from original cart.</summary>
        /// <param name="originalCart">      The original cart.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new master sales items from original cart.</returns>
        // ReSharper disable once CyclomaticComplexity
        protected static async Task<List<SalesQuoteItem>> CreateMasterSalesItemsFromOriginalCartAsync(
            ICartModel originalCart,
            DateTime timestamp,
            string? contextProfileName)
        {
            var resultItems = new List<SalesQuoteItem>();
            foreach (var cartItem in originalCart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                var salesItem = new SalesQuoteItem
                {
                    Active = true,
                    CreatedDate = timestamp,
                    ProductID = cartItem.ProductID,
                    Name = cartItem.Name ?? cartItem.ProductName,
                    Description = cartItem.Description ?? cartItem.ProductShortDescription,
                    Sku = cartItem.Sku ?? cartItem.ProductKey,
                    ForceUniqueLineItemKey = cartItem.ForceUniqueLineItemKey,
                    UnitOfMeasure = cartItem.UnitOfMeasure,
                    Quantity = cartItem.Quantity,
                    QuantityBackOrdered = cartItem.QuantityBackOrdered ?? 0m,
                    QuantityPreSold = cartItem.QuantityPreSold ?? 0m,
                    UnitCorePrice = cartItem.UnitCorePrice,
                    UnitCorePriceInSellingCurrency = cartItem.UnitCorePriceInSellingCurrency,
                    UnitSoldPrice = cartItem.UnitSoldPrice,
                    UnitSoldPriceModifier = cartItem.UnitSoldPriceModifier,
                    UnitSoldPriceModifierMode = cartItem.UnitSoldPriceModifierMode,
                    UnitSoldPriceInSellingCurrency = cartItem.UnitSoldPriceInSellingCurrency,
                    OriginalCurrencyID = cartItem.OriginalCurrencyID,
                    SellingCurrencyID = cartItem.SellingCurrencyID,
                    UserID = cartItem.UserID,
                    JsonAttributes = cartItem.SerializableAttributes.SerializeAttributesDictionary(),
                };
                // Transfer Attributes
                await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                        entity: salesItem,
                        model: cartItem,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Notes
                await Workflows.SalesQuoteItemWithNotesAssociation.AssociateObjectsAsync(
                        entity: salesItem,
                        model: new SalesItemBaseModel<IAppliedSalesQuoteItemDiscountModel, AppliedSalesQuoteItemDiscountModel>
                        {
                            Notes = cartItem.Notes?.Cast<NoteModel>().ToList(),
                        },
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Discounts
                if (cartItem.Discounts != null && cartItem.Discounts.Any(x => x.Active))
                {
                    if (Contract.CheckEmpty(cartItem.Discounts))
                    {
                        // Try to load them if it looks like they aren't loaded
                        using var context3 = RegistryLoaderWrapper.GetContext(contextProfileName);
                        var discountItemSearchModel = RegistryLoaderWrapper.GetInstance<IAppliedCartItemDiscountSearchModel>(contextProfileName);
                        discountItemSearchModel.Active = true;
                        discountItemSearchModel.MasterID = cartItem.ID;
                        cartItem.Discounts = context3.AppliedCartItemDiscounts
                            .AsNoTracking()
                            .FilterAppliedCartItemDiscountsBySearchModel(discountItemSearchModel)
                            .SelectListAppliedCartItemDiscountAndMapToAppliedCartItemDiscountModel(contextProfileName)
                            .ToList();
                    }
                    salesItem.Discounts ??= new List<AppliedSalesQuoteItemDiscount>();
                    foreach (var cartItemDiscountModel in cartItem.Discounts.Where(x => x.Active))
                    {
                        salesItem.Discounts.Add(new()
                        {
                            // Base Properties
                            CustomKey = cartItemDiscountModel.CustomKey,
                            Active = cartItemDiscountModel.Active,
                            CreatedDate = cartItemDiscountModel.CreatedDate,
                            UpdatedDate = cartItemDiscountModel.UpdatedDate,
                            JsonAttributes = cartItemDiscountModel.SerializableAttributes.SerializeAttributesDictionary(),
                            // Applied Discount Properties
                            SlaveID = cartItemDiscountModel.DiscountID,
                            DiscountTotal = cartItemDiscountModel.DiscountTotal,
                            ApplicationsUsed = cartItemDiscountModel.ApplicationsUsed,
                            TargetApplicationsUsed = 0,
                        });
                    }
                }
                // Transfer Targets
                if (cartItem.Targets != null && cartItem.Targets.Any(x => x.Active))
                {
                    salesItem.Targets ??= new List<SalesQuoteItemTarget>();
                    foreach (var cartItemTarget in cartItem.Targets.Where(x => x.Active))
                    {
                        salesItem.Targets.Add(new()
                        {
                            // Base Properties
                            ID = cartItemTarget.ID,
                            CustomKey = cartItemTarget.CustomKey,
                            Active = cartItemTarget.Active,
                            CreatedDate = cartItemTarget.CreatedDate,
                            UpdatedDate = cartItemTarget.UpdatedDate,
                            JsonAttributes = cartItemTarget.SerializableAttributes.SerializeAttributesDictionary(),
                            Hash = cartItemTarget.Hash,
                            // Target Properties
                            NothingToShip = cartItemTarget.NothingToShip,
                            Quantity = cartItemTarget.Quantity,
                            // Related Objects
                            TypeID = cartItemTarget.TypeID,
                            BrandProductID = cartItemTarget.BrandProductID,
                            DestinationContactID = cartItemTarget.DestinationContactID,
                            SelectedRateQuoteID = cartItemTarget.SelectedRateQuoteID,
                        });
                    }
                }
                resultItems.Add(salesItem);
            }
            return resultItems;
        }

        /// <summary>Handles the attributes.</summary>
        /// <param name="checkout">           The checkout.</param>
        /// <param name="entity">             The entity.</param>
        /// <param name="originalCurrencyKey">The original currency key.</param>
        /// <param name="sellingCurrencyKey"> The selling currency key.</param>
        protected static void HandleAttributes(
            ICheckoutModel checkout,
            IHaveJsonAttributesBase entity,
            string originalCurrencyKey,
            string sellingCurrencyKey)
        {
            var attributes = entity.SerializableAttributes!;
            if (checkout.SerializableAttributes?.Any() == true)
            {
                foreach (var kvp in checkout.SerializableAttributes)
                {
                    attributes[kvp.Key] = kvp.Value;
                }
            }
            if (Contract.CheckValidKey(checkout.WithTaxes?.TaxExemptionNumber))
            {
                attributes[nameof(checkout.WithTaxes.TaxExemptionNumber)] = new()
                {
                    Key = nameof(checkout.WithTaxes.TaxExemptionNumber),
                    Value = checkout.WithTaxes!.TaxExemptionNumber!,
                };
            }
            if (Contract.CheckValidKey(originalCurrencyKey))
            {
                attributes["OriginalCurrencyKey"] = new()
                {
                    Key = "OriginalCurrencyKey",
                    Value = originalCurrencyKey,
                };
            }
            if (Contract.CheckValidKey(sellingCurrencyKey))
            {
                attributes["SellingCurrencyKey"] = new()
                {
                    Key = "SellingCurrencyKey",
                    Value = sellingCurrencyKey,
                };
            }
            // entity.SerializableAttributes = attributes; // Note: read-only
            entity.JsonAttributes = attributes.SerializeAttributesDictionary();
        }

        /// <summary>Generates a master quote.</summary>
        /// <param name="checkout">            The checkout.</param>
        /// <param name="originalCart">        The original cart.</param>
        /// <param name="user">                The user.</param>
        /// <param name="originalCurrencyKey"> The original currency key.</param>
        /// <param name="sellingCurrencyKey">  The selling currency key.</param>
        /// <param name="timestamp">           The timestamp Date/Time.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>The master quote.</returns>
        protected virtual async Task<CEFActionResponse<ISalesQuote>> GenerateMasterQuoteAsync(
            ICheckoutModel checkout,
            ICartModel originalCart,
            IUserModel? user,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            DateTime timestamp,
            string? contextProfileName)
        {
            var master = RegistryLoaderWrapper.GetInstance<ISalesQuote>(contextProfileName);
            master.Active = true;
            master.CreatedDate = timestamp;
            master.JsonAttributes = "{}";
            if (user != null)
            {
                master.UserID = user.ID;
                master.AccountID = user.AccountID;
            }
            master.SalesItems = await CreateMasterSalesItemsFromOriginalCartAsync(
                    originalCart: originalCart,
                    timestamp: timestamp,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            master.SubtotalItems = originalCart.Totals.SubTotal;
            master.SubtotalFees = originalCart.Totals.Fees;
            master.SubtotalFeesModifier = originalCart.SubtotalFeesModifier;
            master.SubtotalFeesModifierMode = originalCart.SubtotalFeesModifierMode;
            master.SubtotalShipping = originalCart.Totals.Shipping;
            master.SubtotalShippingModifier = originalCart.SubtotalShippingModifier;
            master.SubtotalShippingModifierMode = originalCart.SubtotalShippingModifierMode;
            master.SubtotalHandling = originalCart.Totals.Handling;
            master.SubtotalHandlingModifier = originalCart.SubtotalHandlingModifier;
            master.SubtotalHandlingModifierMode = originalCart.SubtotalHandlingModifierMode;
            master.SubtotalTaxes = originalCart.Totals.Tax;
            master.SubtotalTaxesModifier = originalCart.SubtotalTaxesModifier;
            master.SubtotalTaxesModifierMode = originalCart.SubtotalTaxesModifierMode;
            master.SubtotalDiscounts = originalCart.Totals.Discounts;
            master.SubtotalDiscountsModifier = originalCart.SubtotalDiscountsModifier;
            master.SubtotalDiscountsModifierMode = originalCart.SubtotalDiscountsModifierMode;
            master.Total = originalCart.Totals.Total;
            master.Total = master.SubtotalItems
                + master.SubtotalFees
                + master.SubtotalShipping
                + master.SubtotalHandling
                + master.SubtotalTaxes
                + Math.Abs(master.SubtotalDiscounts) * -1;
            TransferContactsList(originalCart, master); // Transfer contacts from storeCart to entity
            _ = await RelateStateStatusTypeAndAttributesUsingDummyAsync(
                    attributes: originalCart.SerializableAttributes!,
                    entity: master,
                    timestamp: timestamp,
                    statusKey: "Submitted",
                    typeKey: Contract.RequiresValidKey(CEFConfigDictionary.SubmitSalesQuoteDefaultTypeKey),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            HandleAttributes(
                checkout,
                master,
                originalCurrencyKey,
                sellingCurrencyKey);
            if (checkout.FileNames?.Count > 0)
            {
                var files = master.StoredFiles ?? new List<SalesQuoteFile>();
                foreach (var file in checkout.FileNames)
                {
                    files.Add(new()
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        CustomKey = file,
                        Name = file,
                        Slave = new()
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            CustomKey = file,
                            FileName = file,
                            Name = file,
                        },
                    });
                }
                master.StoredFiles = files;
            }
            // Transfer Discounts
            if (Contract.CheckEmpty(originalCart.Discounts))
            {
                // Try to load them if it looks like they aren't loaded
                using var context2 = RegistryLoaderWrapper.GetContext(contextProfileName);
                var discountSearchModel = RegistryLoaderWrapper.GetInstance<IAppliedCartDiscountSearchModel>(contextProfileName);
                discountSearchModel.Active = true;
                discountSearchModel.MasterID = originalCart.ID;
                originalCart.Discounts = context2.AppliedCartDiscounts
                    .AsNoTracking()
                    .FilterAppliedCartDiscountsBySearchModel(discountSearchModel)
                    .SelectListAppliedCartDiscountAndMapToAppliedCartDiscountModel(contextProfileName)
                    .ToList();
            }
            if (originalCart.Discounts != null && originalCart.Discounts.Any(x => x.Active))
            {
                master.Discounts ??= new List<AppliedSalesQuoteDiscount>();
                foreach (var cartItemDiscountModel in originalCart.Discounts.Where(x => x.Active))
                {
                    master.Discounts.Add(new()
                    {
                        // Base Properties
                        ID = cartItemDiscountModel.ID,
                        CustomKey = cartItemDiscountModel.CustomKey,
                        Active = cartItemDiscountModel.Active,
                        CreatedDate = cartItemDiscountModel.CreatedDate,
                        UpdatedDate = cartItemDiscountModel.UpdatedDate,
                        JsonAttributes = cartItemDiscountModel.SerializableAttributes.SerializeAttributesDictionary(),
                        // Applied Discount Properties
                        SlaveID = cartItemDiscountModel.DiscountID,
                        DiscountTotal = cartItemDiscountModel.DiscountTotal,
                        ApplicationsUsed = cartItemDiscountModel.ApplicationsUsed,
                        TargetApplicationsUsed = 0,
                    });
                }
            }
            /* TODO: Tie this to a setting
            if (Contract.CheckNotNull(user))
            {
                if (Contract.CheckNotNull(user.Account))
                {
                    master.CustomKey = $"{user.Account.CustomKey} {DateTime.Now}";
                }
                else
                {
                    master.CustomKey = $"{user.Email} {DateTime.Now}";
                }
            }
            */
            // Save the master
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.SalesQuotes.Add((SalesQuote)master);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesQuote>(
                        "ERROR! Something about creating and saving the master sales quote for the group failed.");
                }
            }
            return master.WrapInPassingCEFAR()!;
        }

        /// <summary>Copies the notes.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="entity">               The entity.</param>
        /// <param name="dummy">                The dummy.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected override async Task CopyNotesAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            ISalesQuote entity,
            ISalesQuoteModel dummy,
            DateTime timestamp,
            string? contextProfileName)
        {
            await Workflows.SalesQuoteWithNotesAssociation.AssociateObjectsAsync(
                    entity,
                    dummy,
                    timestamp,
                    contextProfileName)
                .ConfigureAwait(false);
            if (entity.Notes!.Count == 0)
            {
                entity.Notes = new HashSet<Note>();
                var (cartID, _) = Contract.CheckValidID(checkout.WithCartInfo!.CartID)
                    ? (checkout.WithCartInfo.CartID!.Value, null)
                    : await Workflows.Carts.SessionGetAsIDAsync(
                            lookupKey: new(
                                typeKey: checkout.WithCartInfo.CartTypeName!,
                                sessionID: checkout.WithCartInfo.CartSessionID!.Value,
                                userID: pricingFactoryContext.UserID,
                                accountID: pricingFactoryContext.AccountID),
                            pricingFactoryContext: pricingFactoryContext,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                if (Contract.CheckValidID(cartID))
                {
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    foreach (var note in context.Notes.AsNoTracking().Where(x => x.Active && x.CartID == cartID))
                    {
                        entity.Notes.Add(new()
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            TypeID = note.TypeID,
                            CreatedByUserID = note.CreatedByUserID,
                            Note1 = note.Note1,
                        });
                    }
                }
            }
            if (!Contract.CheckValidKey(checkout.SpecialInstructions))
            {
                return;
            }
            entity.Notes.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                TypeID = CustomerNoteTypeID,
                CreatedByUserID = pricingFactoryContext.UserID,
                Note1 = checkout.SpecialInstructions,
            });
        }

        /// <summary>Relate state status and type using dummy.</summary>
        /// <param name="cartAttributes">    The cart attributes.</param>
        /// <param name="checkoutAttributes">The checkout attributes.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="statusKey">         The status key.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task<ISalesQuoteModel> RelateStateStatusTypeAndAttributesUsingDummyForSubQuotesAsync(
            SerializableAttributesDictionary? cartAttributes,
            SerializableAttributesDictionary? checkoutAttributes,
            ISalesQuote entity,
            string? statusKey,
            DateTime timestamp,
            string? contextProfileName)
        {
            var dummy = RegistryLoaderWrapper.GetInstance<ISalesQuoteModel>(contextProfileName);
            var state = RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName);
            state.Name = state.DisplayName = "Work";
            state.CustomKey = "WORK";
            var status = RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName);
            status.CustomKey = status.Name = status.DisplayName = statusKey ?? "Submitted";
            var type = RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName);
            type.CustomKey = type.Name = type.DisplayName = "Sales Quote Child";
            dummy.State = state;
            dummy.Status = status;
            dummy.Type = type;
            await RelateRequiredStateAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            var attributes = cartAttributes ?? new SerializableAttributesDictionary();
            if (checkoutAttributes?.Any() == true)
            {
                foreach (var kvp in checkoutAttributes)
                {
                    attributes[kvp.Key] = kvp.Value;
                }
            }
            dummy.SerializableAttributes = attributes;
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                    entity: entity,
                    model: dummy,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return dummy;
        }

        /// <summary>Creates target quote sales items from target cart.</summary>
        /// <param name="targetCart">        Target cart.</param>
        /// <param name="masterQuote">       The master quote.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new target quote sales items from target cart.</returns>
        private static async Task<List<SalesQuoteItem>> CreateTargetQuoteSalesItemsFromTargetCartAsync(
            ICartModel targetCart,
            ISalesQuote masterQuote,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Things the target carts do that the master doesn't:
            // * Read in Targets info
            // * Update Stock
            // * Determine how much needs to be back ordered
            var resultItems = new List<SalesQuoteItem>();
            foreach (var cartItem in targetCart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                var salesItem = new SalesQuoteItem
                {
                    Active = true,
                    CreatedDate = timestamp,
                    ProductID = cartItem.ProductID,
                    Name = cartItem.Name ?? cartItem.ProductName,
                    Description = cartItem.Description ?? cartItem.ProductShortDescription,
                    Sku = cartItem.Sku ?? cartItem.ProductKey,
                    ForceUniqueLineItemKey = cartItem.ForceUniqueLineItemKey,
                    UnitOfMeasure = cartItem.UnitOfMeasure,
                    Quantity = cartItem.Quantity,
                    QuantityBackOrdered = cartItem.QuantityBackOrdered ?? 0m,
                    QuantityPreSold = cartItem.QuantityPreSold ?? 0m,
                    UnitCorePrice = cartItem.UnitCorePrice,
                    UnitCorePriceInSellingCurrency = cartItem.UnitCorePriceInSellingCurrency,
                    UnitSoldPrice = cartItem.UnitSoldPrice,
                    UnitSoldPriceModifier = cartItem.UnitSoldPriceModifier,
                    UnitSoldPriceModifierMode = cartItem.UnitSoldPriceModifierMode,
                    UnitSoldPriceInSellingCurrency = cartItem.UnitSoldPriceInSellingCurrency,
                    OriginalCurrencyID = cartItem.OriginalCurrencyID,
                    SellingCurrencyID = cartItem.SellingCurrencyID,
                    UserID = cartItem.UserID,
                };
                // Transfer Attributes
                await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                        entity: salesItem,
                        model: cartItem,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Notes
                await Workflows.SalesQuoteItemWithNotesAssociation.AssociateObjectsAsync(
                        entity: salesItem,
                        model: new SalesItemBaseModel<IAppliedSalesQuoteItemDiscountModel, AppliedSalesQuoteItemDiscountModel>
                        {
                            Notes = cartItem.Notes?.Cast<NoteModel>().ToList(),
                        },
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Targets
                await Workflows.SalesQuoteItemWithTargetsAssociation.AssociateObjectsAsync(
                        entity: salesItem,
                        model: new SalesItemBaseModel<IAppliedSalesQuoteItemDiscountModel, AppliedSalesQuoteItemDiscountModel>
                        {
                            Targets = cartItem.Targets?.Cast<SalesItemTargetBaseModel>().ToList(),
                        },
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Discounts
                var masterOrderItem = masterQuote.SalesItems!
                    .Single(x => x.Active
                        && x.ForceUniqueLineItemKey == salesItem.ForceUniqueLineItemKey
                        && x.ProductID == salesItem.ProductID);
                if (masterOrderItem.Discounts != null && masterOrderItem.Discounts.Any())
                {
                    salesItem.Discounts ??= new HashSet<AppliedSalesQuoteItemDiscount>();
                    foreach (var masterOrderItemDiscountModel in masterOrderItem.Discounts)
                    {
                        var salesItemDiscount = new AppliedSalesQuoteItemDiscount
                        {
                            CustomKey = masterOrderItemDiscountModel.CustomKey,
                            Active = masterOrderItemDiscountModel.Active,
                            CreatedDate = masterOrderItemDiscountModel.CreatedDate,
                            SlaveID = masterOrderItemDiscountModel.SlaveID,
                            DiscountTotal = masterOrderItemDiscountModel.DiscountTotal,
                            ApplicationsUsed = 0, // This is the target, usages are counted by the master
                            TargetApplicationsUsed = masterOrderItemDiscountModel.ApplicationsUsed,
                        };
                        if (Contract.CheckNotEmpty(masterOrderItem.Targets) && masterOrderItem.Targets!.Count(x => x.Active) > 1)
                        {
                            // There is more than one target for this item, so it needs to be split to a percentage of total
                            var percentage = salesItem.TotalQuantity / masterOrderItem.TotalQuantity;
                            salesItemDiscount.DiscountTotal *= percentage;
                            salesItemDiscount.TargetApplicationsUsed = (int?)(salesItemDiscount.TargetApplicationsUsed * percentage);
                        }
                        salesItem.Discounts.Add(salesItemDiscount);
                    }
                }
                resultItems.Add(salesItem);
            }
            return resultItems;
        }

        /// <summary>Handles the emails.</summary>
        /// <param name="splits">            The split quotes.</param>
        /// <param name="result">            The result.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task HandleEmailsAsync(
            IEnumerable<ISalesQuoteModel> splits,
            ICheckoutResult result,
            string? contextProfileName)
        {
            foreach (var split in splits)
            {
                if (split.SalesItems?.Any() != true)
                {
                    // Re-map this data onto the quote
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    var items = context.SalesQuoteItems
                        .FilterByActive(true)
                        .FilterSalesItemsByMasterID<SalesQuoteItem, AppliedSalesQuoteItemDiscount, SalesQuoteItemTarget>(split.ID)
                        .SelectFullSalesQuoteItemAndMapToSalesItemBaseModel(contextProfileName)
                        .ToList();
                    if (items.Count == 0)
                    {
                        result.WarningMessages.Add($"There was an error sending the emails for quote ID {split.ID}");
                        result.WarningMessages.Add("No items for this quote");
                        continue;
                    }
                    split.SalesItems = items;
                }
                try
                {
                    await new SalesQuotesSubmittedNormalToCustomerEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesQuote"] = split, })
                        .ConfigureAwait(false);
                }
                catch (Exception ex1)
                {
                    result.WarningMessages.Add(
                        $"There was an error sending the customer quote confirmation for quote ID {split.ID}");
                    result.WarningMessages.Add(ex1.Message);
                }
                try
                {
                    await new SalesQuotesSubmittedNormalToBackOfficeEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesQuote"] = split, })
                        .ConfigureAwait(false);
                }
                catch (Exception ex2)
                {
                    Logger.LogError(
                        "Targets Checkout Send Back-office Email", ex2.Message, ex2, null, contextProfileName);
                }
                try
                {
                    await new SalesQuotesSubmittedNormalToBackOfficeStoreEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesQuote"] = split, })
                        .ConfigureAwait(false);
                }
                catch (Exception ex3)
                {
                    Logger.LogError(
                        "Targets Checkout Send Store Back-office Email", ex3.Message, ex3, null, contextProfileName);
                }
            }
        }

        /// <summary>Builds sales group from target carts.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="originalCart">         The original cart.</param>
        /// <param name="targetCarts">          Target carts.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="pricingFactoryContext">The pricing factory context.</param>
        /// <param name="originalCurrencyKey">  The original currency key.</param>
        /// <param name="sellingCurrencyKey">   The selling currency key.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ISalesGroupModel}.</returns>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        private async Task<CEFActionResponse<ISalesGroupModel>> BuildSalesGroupFromTargetCartsAsync(
            ICheckoutModel checkout,
            ICartModel originalCart,
            IEnumerable<ICartModel?> targetCarts,
            IUserModel? user,
            IPricingFactoryContextModel pricingFactoryContext,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            var salesGroup = RegistryLoaderWrapper.GetInstance<ISalesGroup>(contextProfileName);
            salesGroup.Active = true;
            salesGroup.CreatedDate = timestamp;
            salesGroup.JsonAttributes = "{}";
            salesGroup.BrandID = checkout.ReferringBrandID;
            // Sales Group Account
            if (user != null)
            {
                salesGroup.AccountID = user.AccountID;
            }
            // Sales Group Master Sales Quote (no target data, just the items all together)
            var masterResult = await GenerateMasterQuoteAsync(
                    checkout: checkout,
                    originalCart: originalCart,
                    user: user,
                    originalCurrencyKey: originalCurrencyKey,
                    sellingCurrencyKey: sellingCurrencyKey,
                    timestamp: timestamp,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!masterResult.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR<ISalesGroupModel>(masterResult.Messages.ToArray());
            }
            var masterQuoteResultValue = Contract.RequiresNotNull(masterResult.Result);
            // Save the group
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.SalesGroups.Add((SalesGroup)salesGroup);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesGroupModel>(
                        "ERROR! Something about creating and saving the sales group failed.");
                }
                var master = await context.SalesQuotes.FilterByID(masterQuoteResultValue.ID).SingleAsync();
                master.SalesGroupAsRequestMasterID = salesGroup.ID;
                if (master.StatusID != masterQuoteResultValue.StatusID)
                {
                    // Save On Hold status if it was set
                    master.StatusID = masterQuoteResultValue.StatusID;
                }
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesGroupModel>(
                        "ERROR! Something about assigning the sales group to the master quote failed.");
                }
            }
            // Now create the sub-quotes
            // ReSharper disable once RedundantEnumerableCastCall
            foreach (var targetCart in targetCarts.Where(x => x != null).Cast<ICartModel>())
            {
                if (targetCart == null)
                {
                    continue;
                }
                var targetQuote = RegistryLoaderWrapper.GetInstance<SalesQuote>(contextProfileName);
                targetQuote.Active = true;
                targetQuote.CreatedDate = timestamp;
                if (user != null)
                {
                    targetQuote.UserID = user.ID;
                    targetQuote.AccountID = user.AccountID;
                }
                if (!targetCart.NothingToShip && targetCart.ShippingContact == null)
                {
                    throw new InvalidOperationException("Target carts that are shippable must have a shipping contact");
                }
                using (var contextCheck = RegistryLoaderWrapper.GetContext(contextProfileName))
                {
                    var master = await contextCheck.SalesQuotes
                        .AsNoTracking()
                        .FilterByID(masterQuoteResultValue.ID)
                        .SingleAsync()
                        .ConfigureAwait(false);
                    foreach (var targetItem in targetCart.SalesItems!)
                    {
                        var masterItemCheck = master.SalesItems!
                            .SingleOrDefault(x => x.ProductID == targetItem.ProductID
                                && x.ForceUniqueLineItemKey == targetItem.ForceUniqueLineItemKey);
                        if (masterItemCheck == null)
                        {
                            continue;
                        }
                        targetItem.SerializableAttributes = masterItemCheck.SerializableAttributes;
                    }
                }
                targetQuote.SalesItems = await CreateTargetQuoteSalesItemsFromTargetCartAsync(
                        targetCart: targetCart,
                        masterQuote: masterResult.Result!,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                targetQuote.SubtotalItems = targetQuote.SalesItems
                    .Where(x => x.Active)
                    .Sum(x => (x.UnitSoldPrice ?? x.UnitCorePrice) * x.TotalQuantity);
                if (Contract.CheckEmpty(targetCart.RateQuotes))
                {
                    targetCart.RateQuotes = (await Workflows.RateQuotes.SearchAsync(
                                search: new RateQuoteSearchModel { Active = true, CartID = targetCart.ID, Selected = true },
                                asListing: true,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .results;
                }
                var rate = targetCart.RateQuotes!
                    .Where(x => x.Active)
                    .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                    .FirstOrDefault(x => x.Active && x.Selected);
                targetQuote.SubtotalShipping = rate?.Rate ?? 0m;
                // TODO: Recalculate handling charges for products and their categories per sub-quote
                targetQuote.SubtotalHandling = 0m;
                // TODO: Recalculate fees per sub-quote
                targetQuote.SubtotalFees = 0m;
                // TODO@CG: Recalculate taxes per sub-quote
                targetQuote.SubtotalTaxes = 0m;
                var discountsTotal = targetQuote.Discounts!.Select(x => x.DiscountTotal).DefaultIfEmpty(0m).Sum();
                discountsTotal += targetQuote.SalesItems.SelectMany(x => x.Discounts!.Select(y => y.DiscountTotal)).DefaultIfEmpty(0m).Sum();
                targetQuote.SubtotalDiscounts = discountsTotal;
                targetQuote.Total = targetQuote.SubtotalItems
                    + targetQuote.SubtotalShipping
                    + targetQuote.SubtotalTaxes
                    + targetQuote.SubtotalFees
                    + targetQuote.SubtotalHandling
                    + Math.Abs(targetQuote.SubtotalDiscounts) * -1m;
                targetQuote.SalesGroupAsRequestSubID = salesGroup.ID;
                if (!targetCart.NothingToShip)
                {
                    var shippingContact = await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                            model: WipeIDsFromContact(targetCart.ShippingContact!)!,
                            timestamp: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    shippingContact.Result!.TypeID = ShippingTypeID;
                    targetQuote.ShippingContact = shippingContact.Result;
                }
                if (checkout.FileNames?.Count > 0)
                {
                    var salesFiles = targetQuote.StoredFiles ?? new List<SalesQuoteFile>();
                    foreach (var file in checkout.FileNames)
                    {
                        salesFiles.Add(new()
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            CustomKey = file,
                            Name = file,
                            Slave = new()
                            {
                                Active = true,
                                CreatedDate = timestamp,
                                CustomKey = file,
                                FileName = file,
                                Name = file,
                            },
                        });
                    }
                    targetQuote.StoredFiles = salesFiles;
                }
                targetQuote.BrandID = checkout.ReferringBrandID;
                var dummy = await RelateStateStatusTypeAndAttributesUsingDummyForSubQuotesAsync(
                        cartAttributes: originalCart.SerializableAttributes,
                        checkoutAttributes: checkout.SerializableAttributes,
                        entity: targetQuote,
                        statusKey: masterQuoteResultValue.StatusID == QuoteStatusOnHoldID ? "On Hold" : null,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Copy the notes from the Cart
                await CopyNotesAsync(
                        checkout: checkout,
                        pricingFactoryContext: pricingFactoryContext,
                        entity: targetQuote,
                        dummy: dummy,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
                {
                    context.SalesQuotes.Add(targetQuote);
                    if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                    {
                        return CEFAR.FailingCEFAR<ISalesGroupModel>(
                            "ERROR! Something about creating and saving the sub-sales quote for the sales group failed.");
                    }
                }
                if (rate == null)
                {
                    continue;
                }
                rate.SalesQuoteID = targetQuote.ID;
                rate.CartID = null;
                await Workflows.RateQuotes.UpdateAsync(rate, contextProfileName).ConfigureAwait(false);
            }
            // TODO: Move the original cart to History and Converted to Sales Quote
            // Return the group
            return (await Workflows.SalesGroups.GetAsync(salesGroup.ID, contextProfileName).ConfigureAwait(false))
                .WrapInPassingCEFAR()!;
        }
    }
}
