// <copyright file="SingleQuoteSubmitProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the single quote checkout provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts.SingleQuote
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Discounts;
    using Ecommerce;
    using Emails;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>A single quote checkout provider.</summary>
    /// <seealso cref="SalesQuoteSubmitProviderBase"/>
    public class SingleQuoteSubmitProvider : SalesQuoteSubmitProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => SingleQuoteSubmitProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new InvalidOperationException("ERROR! Single Quote Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new InvalidOperationException("ERROR! Single Quote Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new InvalidOperationException("ERROR! Single Quote Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var resolveUserResult = await TryResolveUserAsync(checkout, contextProfileName).ConfigureAwait(false);
            var user = resolveUserResult.ActionSucceeded ? resolveUserResult.Result : null;
            EnforceUserInPricingFactoryContextIfSet(pricingFactoryContext, user);
            var cartResponse = await TryResolveCartAsync(
                    checkout: Contract.RequiresNotNull(checkout),
                    pricingFactoryContext: pricingFactoryContext,
                    lookupKey: lookupKey,
                    taxesProvider: taxesProvider,
                    validate: true,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // ReSharper disable once InvertIf
            if (!cartResponse.ActionSucceeded || cartResponse.Messages.Any(x => x.StartsWith("ERROR")))
            {
                var errors = cartResponse.Messages.Where(x => x.StartsWith("ERROR")).ToList();
                if (Contract.CheckEmpty(errors))
                {
                    errors.Add($"ERROR! Could not locate your cart with session id '{checkout.WithCartInfo?.CartSessionID}'");
                }
                return new CheckoutResult
                {
                    Succeeded = false,
                    ErrorMessages = cartResponse.Messages.Where(x => x.StartsWith("ERROR")).ToList(),
                    WarningMessages = cartResponse.Messages.Where(x => x.StartsWith("WARNING")).ToList(),
                };
            }
            return await CheckoutInnerAsync(
                    checkout: checkout,
                    cart: cartResponse.Result!,
                    nothingToShip: cartResponse.Result!.SalesItems!.All(x => x.ProductNothingToShip),
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var resolveUserResult = await TryResolveUserAsync(
                    Contract.RequiresNotNull(checkout),
                    contextProfileName)
                .ConfigureAwait(false);
            var user = resolveUserResult.ActionSucceeded ? resolveUserResult.Result : null;
            EnforceUserInPricingFactoryContextIfSet(pricingFactoryContext, user);
            var cart = await Workflows.Carts.AdminGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (cart is null)
            {
                var result = new CheckoutResult();
                result.ErrorMessages.Add("ERROR! Cart was null");
                return result;
            }
            return await CheckoutInnerAsync(
                    checkout: checkout,
                    cart: cart,
                    nothingToShip: cart.SalesItems!.All(x => x.ProductNothingToShip),
                    pricingFactoryContext: pricingFactoryContext,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
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
            var attributes = checkout.SerializableAttributes ?? new SerializableAttributesDictionary();
            /* Not applicable in Single Checkout, placed here for mirroring with Split
            if (!string.IsNullOrWhiteSpace(shipOption))
            {
                attributes[ShipOptionAttr.CustomKey] = new SerializableAttributeObject
                {
                    // ReSharper disable once PossibleInvalidOperationException
                    ID = ShipOptionAttr.ID.Value,
                    Key = ShipOptionAttr.CustomKey,
                    Value = shipOption
                };
            }*/
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

        /// <summary>Creates a sales quote from a checkout and user object.</summary>
        /// <param name="context">              The context.</param>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="nothingToShip">        True to nothing to ship.</param>
        /// <param name="pricingFactoryContext">The pricing factory context.</param>
        /// <param name="originalCurrencyKey">  The original currency key.</param>
        /// <param name="sellingCurrencyKey">   The selling currency key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ISalesQuoteModel.</returns>
        // ReSharper disable once FunctionComplexityOverflow
        protected virtual async Task<ISalesQuoteModel> CreateSingleAsync(
            IClarityEcommerceEntities context,
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel? user,
            bool nothingToShip,
            IPricingFactoryContextModel pricingFactoryContext,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            ITaxesProviderBase? taxesProvider,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Resolve the contacts
            WipeIDsFromMainContacts(cart);
            var shippingContact = nothingToShip
                ? null
                : await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                        WipeIDsFromContact(checkout.Shipping!)!,
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
            if (!nothingToShip && shippingContact != null)
            {
                shippingContact.Result!.TypeID = ShippingTypeID;
            }
            // Create the new entity
            var entity = new SalesQuote
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                // TODO@BE: New SalesQuote.CustomKey with a custom iteration pattern in Single Quote Checkout
                // Sales Quote Properties
                // TaxTransactionID = // TODO@CG: Avalara Transaction ID
                // NOTE: GetModifiedValue is no longer needed here, it's handled above with the cart and currency
                // conversion. These values are all converted to selling currency at this point as well
                SubtotalItems = cart.SalesItems!.Sum(x => x.ExtendedPriceInSellingCurrency ?? x.ExtendedPrice),
                SubtotalFees = cart.Totals!.Fees,
                SubtotalShipping = cart.Totals.Shipping,
                SubtotalHandling = cart.Totals.Handling,
                SubtotalTaxes = cart.Totals.Tax,
                SubtotalDiscounts = cart.Totals.Discounts,
                // Related Objects
                TypeID = QuoteTypeID,
                StateID = QuoteStateID,
                ShippingContact = shippingContact?.Result,
                UserID = pricingFactoryContext.UserID,
                AccountID = pricingFactoryContext.AccountID,
                // Associated Objects
            };
            if (Contract.CheckValidID(pricingFactoryContext.StoreID))
            {
                entity.StoreID = pricingFactoryContext.StoreID;
            }
            if (Contract.CheckValidID(pricingFactoryContext.FranchiseID))
            {
                entity.FranchiseID = pricingFactoryContext.FranchiseID;
            }
            if (Contract.CheckValidID(pricingFactoryContext.BrandID))
            {
                entity.BrandID = pricingFactoryContext.BrandID;
            }
            if (CEFConfigDictionary.AffiliatesEnabled && Contract.CheckValidKey(checkout.AffiliateAccountKey))
            {
                await SetAccountIDToSelectedAffiliateKeyAsync(checkout, entity, contextProfileName).ConfigureAwait(false);
            }
            // Totals (Already converted to the selling currency)
            entity.Total = entity.SubtotalItems
                + entity.SubtotalFees
                + entity.SubtotalShipping
                + entity.SubtotalHandling
                + entity.SubtotalTaxes
                + Math.Abs(entity.SubtotalDiscounts) * -1;
            string statusKey;
            if (Contract.CheckValidID(pricingFactoryContext.AccountID))
            {
                var account = await Workflows.Accounts.GetAsync(
                        pricingFactoryContext.AccountID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                var useOnHold = account!.IsOnHold;
                statusKey = useOnHold ? "On Hold" : "Pending";
            }
            else
            {
                statusKey = "Pending";
            }
            var scmn = cart.RateQuotes?.Find(x => x.Active && x.Selected)?.ShipCarrierMethodName;
            if (Contract.CheckValidKey(scmn))
            {
                var shippingMethodAttributeObject = new SerializableAttributeObject
                {
                    Key = "ShippingMethod",
                    Value = scmn!,
                };
                cart.SerializableAttributes!.AddOrUpdate(
                    "ShippingMethod",
                    shippingMethodAttributeObject,
                    (_, _) => shippingMethodAttributeObject);
            }
            var typeKey = Contract.RequiresValidKey(CEFConfigDictionary.SubmitSalesQuoteDefaultTypeKey);
            // State, Status and Type
            var dummy = await RelateStateStatusTypeAndAttributesUsingDummyAsync(
                    attributes: cart.SerializableAttributes!,
                    entity: entity,
                    timestamp: timestamp,
                    statusKey: statusKey,
                    typeKey: typeKey,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // Attributes
            HandleAttributes(
                checkout,
                entity,
                originalCurrencyKey,
                sellingCurrencyKey);
            // Discounts
            await RegistryLoaderWrapper.GetInstance<IDiscountManager>(contextProfileName)
                .VerifyCurrentDiscountsAsync(cart.ID, pricingFactoryContext, taxesProvider, contextProfileName)
                .ConfigureAwait(false);
            entity.Discounts = GetOrderLevelDiscounts(cart, timestamp);
            entity.RateQuotes = GetSelectedRateQuote(cart);
            // Add and save what we have done so far
            context.SalesQuotes.Add(entity);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (taxesProvider != null)
            {
                // Commit Taxes
                await taxesProvider.CommitCartAsync(
                        cart: cart,
                        userID: user?.ID,
                        contextProfileName: contextProfileName,
                        purchaseOrderNumber: null,
                        vatId: checkout.WithTaxes?.VatID)
                    .ConfigureAwait(false);
                // TODO
                // entity.TaxTransactionID = taxes == null
                //     ? string.Empty
                //     : $"DOC-{DateTime.Today:yyMMdd}-{cart.ID}";
            }
            foreach (var cartItem in cart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                await TransferCartItemsToQuoteItemsAsync(
                        timestamp: timestamp,
                        entity: entity,
                        cartItem: cartItem,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            // Transfer contacts from cart to entity
            TransferContactsList(cart, entity);
            // Copy the notes from the Cart
            await CopyNotesAsync(
                    checkout: checkout,
                    pricingFactoryContext: pricingFactoryContext,
                    entity: entity,
                    dummy: dummy,
                    timestamp: timestamp,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // Copy the Attributes from the Cart
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                    entity,
                    dummy,
                    contextProfileName)
                .ConfigureAwait(false);
            if (checkout.FileNames?.Count > 0)
            {
                var salesQuoteFiles = entity.StoredFiles ?? new List<SalesQuoteFile>();
                foreach (var file in checkout.FileNames)
                {
                    salesQuoteFiles.Add(new()
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
                entity.StoredFiles = salesQuoteFiles;
            }
            // Save any additional content or changes that occurred
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return (await Workflows.SalesQuotes.GetAsync(entity.ID, contextProfileName).ConfigureAwait(false))!;
        }

        /// <summary>Sets account identifier to selected affiliate key.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task SetAccountIDToSelectedAffiliateKeyAsync(
            ICheckoutModel checkout,
            // ReSharper disable once SuggestBaseTypeForParameter
            ISalesQuote entity,
            string? contextProfileName)
        {
            if (!Contract.CheckValidKey(checkout.AffiliateAccountKey))
            {
                return;
            }
            // Override the account assignment if we get a valid id back (leave the user assignment as-is)
            var altAccountID = await Workflows.Accounts.CheckExistsByKeyAsync(
                    checkout.AffiliateAccountKey!,
                    contextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(altAccountID))
            {
                entity.AccountID = altAccountID!.Value;
            }
        }

        /// <summary>Process the emails for checkout.</summary>
        /// <param name="quote">             The quote.</param>
        /// <param name="result">            The result.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task ProcessEmailsForCheckoutAsync(
            // ReSharper disable once SuggestBaseTypeForParameter
            ISalesQuoteModel quote,
            ICheckoutResult result,
            string? contextProfileName)
        {
            try
            {
                await new SalesQuotesSubmittedNormalToCustomerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = quote, })
                    .ConfigureAwait(false);
            }
            catch (Exception ex1)
            {
                result.WarningMessage ??= string.Empty;
                var message = $"There was an error sending the customer quote confirmation for quote id {result.QuoteID}.";
                result.WarningMessage += message + "\r\n";
                result.WarningMessages.Add(message);
                result.WarningMessages.Add(ex1.Message);
            }
            try
            {
                await new SalesQuotesSubmittedNormalToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = quote, })
                    .ConfigureAwait(false);
            }
            catch (Exception ex2)
            {
                Logger.LogError("Send Checkout Back-office Email", ex2.Message, ex2, null, contextProfileName);
            }
            try
            {
                await new SalesQuotesSubmittedNormalToBackOfficeStoreEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesQuote"] = quote, })
                    .ConfigureAwait(false);
            }
            catch (Exception ex3)
            {
                Logger.LogError("Send Checkout Store Back-office Email", ex3.Message, ex3, null, contextProfileName);
            }
        }

        /// <summary>Generates an quote and checkout result.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="result">               The result.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The quote and checkout result.</returns>
        private static async Task<(ISalesQuoteModel? quote, ICheckoutResult result)> GenerateQuoteAndCheckoutResultAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel? user,
            ICheckoutResult result,
            string? contextProfileName)
        {
            // Gets the currency key from one of three places in quote of
            // priority: checkout/wallet -> user -> cookie
            var sellingCurrencyKey = checkout.CurrencyKey
                ?? user?.CurrencyKey
                ?? CEFConfigDictionary.DefaultCurrency;
            var sellingCurrencyID = await Workflows.Currencies.CheckExistsAsync(
                    sellingCurrencyKey,
                    contextProfileName)
                .ConfigureAwait(false);
            checkout.CurrencyKey = sellingCurrencyKey;
            var originalCurrencyKey = CEFConfigDictionary.DefaultCurrency;
            var originalCurrencyID = await Workflows.Currencies.CheckExistsAsync(
                    originalCurrencyKey,
                    contextProfileName)
                .ConfigureAwait(false);
            foreach (var item in cart.SalesItems!)
            {
                item.OriginalCurrencyID = originalCurrencyID;
                item.SellingCurrencyID = sellingCurrencyID;
                item.UnitSoldPrice = GetModifiedValue(
                    item.UnitSoldPrice ?? item.UnitCorePrice,
                    item.UnitSoldPriceModifier,
                    item.UnitSoldPriceModifierMode);
                if (originalCurrencyID == sellingCurrencyID)
                {
                    item.UnitCorePriceInSellingCurrency = null;
                    item.UnitSoldPriceInSellingCurrency = null;
                    item.ExtendedPriceInSellingCurrency = null;
                    continue;
                }
                item.UnitCorePriceInSellingCurrency = await Workflows.Currencies.ConvertAsync(
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        item.UnitCorePrice,
                        contextProfileName)
                    .ConfigureAwait(false);
                item.UnitSoldPriceInSellingCurrency = await Workflows.Currencies.ConvertAsync(
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        item.UnitSoldPrice.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                item.ExtendedPriceInSellingCurrency = await Workflows.Currencies.ConvertAsync(
                        originalCurrencyKey,
                        sellingCurrencyKey,
                        item.ExtendedPrice,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            cart.Totals!.Discounts = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(
                        cart.Totals.Discounts,
                        cart.SubtotalDiscountsModifier,
                        cart.SubtotalDiscountsModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Fees = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(
                        cart.Totals.Fees,
                        cart.SubtotalFeesModifier,
                        cart.SubtotalFeesModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Handling = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(
                        cart.Totals.Handling,
                        cart.SubtotalHandlingModifier,
                        cart.SubtotalHandlingModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Shipping = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(
                        cart.Totals.Shipping,
                        cart.SubtotalShippingModifier,
                        cart.SubtotalShippingModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.SubTotal = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    cart.Totals.SubTotal,
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Tax = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(
                        cart.Totals.Tax,
                        cart.SubtotalTaxesModifier,
                        cart.SubtotalTaxesModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            result.Succeeded = true;
            (ISalesQuoteModel? quote, ICheckoutResult result) retVal = (null, result);
            return retVal;
        }

        /// <summary>Checkout inner.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="nothingToShip">        True to nothing to ship.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{ICheckoutResult}.</returns>
        private async Task<ICheckoutResult> CheckoutInnerAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            bool nothingToShip,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(checkout);
            var result = RegistryLoaderWrapper.GetInstance<ICheckoutResult>(contextProfileName);
            var nullCheckResult = DoCartIsNullCheck(cart);
            if (!nullCheckResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages.AddRange(nullCheckResult.Messages);
                return result;
            }
            var emptyCartCheckResult = DoCartIsNullCheck(cart);
            if (!emptyCartCheckResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages.AddRange(emptyCartCheckResult.Messages);
                return result;
            }
            // Resolve the Store ID for each cart item
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var resolveUserResult = await TryResolveUserAsync(checkout, contextProfileName).ConfigureAwait(false);
            var user = resolveUserResult.ActionSucceeded ? resolveUserResult.Result : null;
            EnforceUserInPricingFactoryContextIfSet(pricingFactoryContext, user);
            var (quote, checkoutResult) = await GenerateQuoteAndCheckoutResultAsync(
                    checkout: checkout,
                    cart: cart,
                    user: user,
                    result: result,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            result = checkoutResult;
            if (!result.Succeeded
                || result.QuoteID == null && result.QuoteIDs?.Any() != true)
            {
                // There was an error and the error message was put into ErrorMessage/ErrorMessages
                result.Succeeded = false;
                return result;
            }
            await HandleAddressBookAsync(checkout, user, nothingToShip, contextProfileName).ConfigureAwait(false);
            await ProcessEmailsForCheckoutAsync(quote!, checkoutResult, contextProfileName).ConfigureAwait(false);
            return result;
        }
    }
}
