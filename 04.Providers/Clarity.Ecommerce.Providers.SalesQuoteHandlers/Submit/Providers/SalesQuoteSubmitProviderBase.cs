// <copyright file="SalesQuoteSubmitProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout provider base class</summary>
// ReSharper disable ConditionIsAlwaysTrueOrFalse
#nullable enable
namespace Clarity.Ecommerce.Providers.Checkouts
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Discounts;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.SalesQuoteHandlers.Checkouts;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A quote checkout provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="ISalesQuoteSubmitProviderBase"/>
    public abstract class SalesQuoteSubmitProviderBase : ProviderBase, ISalesQuoteSubmitProviderBase
    {
        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.SalesQuoteCheckout;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <inheritdoc/>
        public bool IsInitialized { get; protected set; }

        /// <summary>Gets the identifier of the shipping type.</summary>
        /// <value>The identifier of the shipping type.</value>
        protected static int ShippingTypeID { get; private set; }

        /// <summary>Gets the identifier of the quote status submitted.</summary>
        /// <value>The identifier of the quote status submitted.</value>
        protected static int QuoteStatusSubmittedID { get; private set; }

        /// <summary>Gets the identifier of the quote status on hold.</summary>
        /// <value>The identifier of the quote status on hold.</value>
        protected static int QuoteStatusOnHoldID { get; private set; }

        /// <summary>Gets the identifier of the quote type.</summary>
        /// <value>The identifier of the quote type.</value>
        protected static int QuoteTypeID { get; private set; }

        /// <summary>Gets the identifier of the quote state.</summary>
        /// <value>The identifier of the quote state.</value>
        protected static int QuoteStateID { get; private set; }

        /// <summary>Gets the identifier of the customer note type.</summary>
        /// <value>The identifier of the customer note type.</value>
        protected static int CustomerNoteTypeID { get; private set; }

        /// <summary>Gets the default currency identifier.</summary>
        /// <value>The default currency identifier.</value>
        protected static int DefaultCurrencyID { get; private set; }

        /// <summary>Gets or sets the user type identifier of customer.</summary>
        /// <value>The user type identifier of customer.</value>
        protected static int UserTypeIDOfCustomer { get; set; }

        /// <summary>Gets or sets the user status identifier of registered.</summary>
        /// <value>The user status identifier of registered.</value>
        protected static int UserStatusIDOfRegistered { get; set; }

        // TODO: Admin calls for Quote Checkout
        // /// <inheritdoc/>
        // public abstract CEFActionResponse<List<ICartModel>> Analyze(
        //     ICheckoutModel checkout,
        //     int cartID,
        //     int userID,
        //     IPricingFactoryContextModel pricingFactoryContext,
        //     ITaxesProviderBase? taxesProvider,
        //     string? contextProfileName);

        // TODO: Admin calls for Quote Checkout
        // /// <inheritdoc/>
        // public abstract ICheckoutResult Checkout(
        //     ICheckoutModel checkout,
        //     int cartID,
        //     int userID,
        //     IPricingFactoryContextModel pricingFactoryContext,
        //     ITaxesProviderBase? taxesProvider,
        //     string? contextProfileName);

        // TODO: Analyze Call for Quote Checkout
        // /// <inheritdoc/>
        // public abstract CEFActionResponse<List<ICartModel>> Analyze(
        //     ICheckoutModel checkout,
        //     IPricingFactoryContextModel pricingFactoryContext,
        //     string cartType,
        //     ITaxesProviderBase? taxesProvider,
        //     int? userID,
        //     string? contextProfileName);

        /// <inheritdoc/>
        public Task InitAsync(
            int quoteStatusSubmittedID,
            int quoteStatusOnHoldID,
            int quoteTypeID,
            int quoteStateID,
            int shippingTypeID,
            int customerNoteTypeID,
            int defaultCurrencyID,
            string? contextProfileName)
        {
            QuoteStatusSubmittedID = quoteStatusSubmittedID;
            QuoteStatusOnHoldID = quoteStatusOnHoldID;
            QuoteTypeID = quoteTypeID;
            QuoteStateID = quoteStateID;
            ShippingTypeID = shippingTypeID;
            CustomerNoteTypeID = customerNoteTypeID;
            DefaultCurrencyID = defaultCurrencyID;
            ////PreferredPaymentMethodAttr = preferredPaymentMethodAttr;
            return Task.CompletedTask;
        }

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public virtual async Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            _ = Contract.RequiresNotNull(checkout);
            IUserModel? user = null;
            // No username means Guest Checkout
            if (Contract.CheckValidKey(checkout.WithUserInfo?.ExternalUserID)
                || Contract.CheckValidID(checkout.WithUserInfo?.UserID)
                || Contract.CheckValidKey(checkout.WithUserInfo?.UserName))
            {
                // Get User
                var currentUserName = checkout.WithUserInfo!.ExternalUserID ?? checkout.WithUserInfo.UserName;
                user = Contract.CheckValidID(checkout.WithUserInfo.UserID)
                    ? await Workflows.Users.GetAsync(checkout.WithUserInfo!.UserID!.Value, contextProfileName).ConfigureAwait(false)
                    : await Workflows.Users.GetAsync(currentUserName!, contextProfileName).ConfigureAwait(false);
                RelateContactInfoIfMissing(checkout.Shipping, user);
            }
            var cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookupKey: lookupKey.ToIDLookupKey(checkout.WithCartInfo!.CartID!.Value),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : (checkout.WithCartInfo?.CartSessionID).HasValue
                    ? (await Workflows.Carts.SessionGetAsync(
                            lookupKey: lookupKey,
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: taxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .cartResponse.Result
                    : null;
            if (cart == null)
            {
                return new CheckoutResult { Succeeded = false, ErrorMessage = "ERROR! Could not look up your cart." };
            }
            if (cart.SalesItems!.Count == 0)
            {
                return new CheckoutResult { Succeeded = false, ErrorMessage = "ERROR! Your cart was empty." };
            }
            var result = new CheckoutResult();
            if (user != null)
            {
                pricingFactoryContext.UserID = user.ID;
                if (Contract.CheckValidID(user.AccountID))
                {
                    pricingFactoryContext.AccountID = user.AccountID;
                }
            }
            var quoteResponse = await CreateSingleViaCheckoutProcessAsync(
                    checkout: checkout,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            result.OrderID = quoteResponse.Result;
            result.Succeeded = true;
            return result;
        }

        /// <inheritdoc/>
        public abstract Task<ICheckoutResult> SubmitAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int?>> CreateSingleViaCheckoutProcessAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            var cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookupKey: new CartByIDLookupKey(
                            cartID: checkout.WithCartInfo!.CartID!.Value,
                            userID: pricingFactoryContext.UserID,
                            accountID: pricingFactoryContext.AccountID,
                            brandID: pricingFactoryContext.BrandID,
                            franchiseID: pricingFactoryContext.FranchiseID,
                            storeID: pricingFactoryContext.StoreID),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : (checkout.WithCartInfo?.CartSessionID).HasValue
                    ? (await Workflows.Carts.SessionGetAsync(
                            lookupKey: new SessionCartBySessionAndTypeLookupKey(
                                sessionID: checkout.WithCartInfo!.CartSessionID!.Value,
                                typeKey: "Quote Cart",
                                userID: pricingFactoryContext.UserID,
                                accountID: pricingFactoryContext.AccountID,
                                brandID: pricingFactoryContext.BrandID,
                                franchiseID: pricingFactoryContext.FranchiseID,
                                storeID: pricingFactoryContext.StoreID),
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: taxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .cartResponse.Result
                    : null;
            if (cart == null)
            {
                throw new ArgumentException("Could not locate the quote cart to begin Checkout process");
            }
            // Resolve the contacts
            Contact? shippingContact = null;
            if (checkout.Shipping != null)
            {
                checkout.Shipping.ID = 0;
                var shippingContactCreateResponse = await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                        checkout.Shipping,
                        null,
                        contextProfileName)
                    .ConfigureAwait(false);
                shippingContactCreateResponse.Result!.TypeID = ShippingTypeID;
                shippingContact = shippingContactCreateResponse.Result;
            }
            // Create the new entity
            var entity = new SalesQuote
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                JsonAttributes = checkout.SerializableAttributes.SerializeAttributesDictionary(),
                // Sales Quote Properties
                ////PurchaseOrderNumber = !Contract.CheckValidKey(checkout.PurchaseOrder) ? null : checkout.PurchaseOrder,
                SubtotalItems = cart.Totals.SubTotal,
                SubtotalFees = cart.Totals.Fees,
                SubtotalFeesModifier = cart.SubtotalFeesModifier,
                SubtotalFeesModifierMode = cart.SubtotalFeesModifierMode,
                SubtotalShipping = cart.Totals.Shipping,
                SubtotalShippingModifier = cart.SubtotalShippingModifier,
                SubtotalShippingModifierMode = cart.SubtotalShippingModifierMode,
                SubtotalHandling = cart.Totals.Handling,
                SubtotalHandlingModifier = cart.SubtotalHandlingModifier,
                SubtotalHandlingModifierMode = cart.SubtotalHandlingModifierMode,
                SubtotalTaxes = cart.Totals.Tax,
                SubtotalTaxesModifier = cart.SubtotalTaxesModifier,
                SubtotalTaxesModifierMode = cart.SubtotalTaxesModifierMode,
                SubtotalDiscounts = cart.Totals.Discounts,
                SubtotalDiscountsModifier = cart.SubtotalDiscountsModifier,
                SubtotalDiscountsModifierMode = cart.SubtotalDiscountsModifierMode,
                // Related Objects
                ShippingContact = shippingContact,
                StatusID = QuoteStatusSubmittedID,
                UserID = pricingFactoryContext.UserID,
                AccountID = pricingFactoryContext.AccountID,
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
            // Totals
            entity.Total = entity.SubtotalItems
                + entity.SubtotalFees
                + entity.SubtotalShipping
                + entity.SubtotalHandling
                + entity.SubtotalTaxes
                + Math.Abs(entity.SubtotalDiscounts) * -1m;
            // State, Status and Type
            var dummy = new SalesQuoteModel
            {
                State = new() { Active = true, Name = "Work", CustomKey = "WORK", DisplayName = "Work" },
                Status = new() { Active = true, Name = "Submitted", CustomKey = "Submitted", DisplayName = "Submitted" },
                Type = new() { Active = true, Name = "Web", CustomKey = "Web", DisplayName = "Web" },
                SerializableAttributes = cart.SerializableAttributes,
            };
            await RelateRequiredStateAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            // Discounts
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                await RegistryLoaderWrapper.GetInstance<IDiscountManager>(contextProfileName)
                    .VerifyCurrentDiscountsAsync(cart.ID, pricingFactoryContext, taxesProvider, contextProfileName)
                    .ConfigureAwait(false);
                entity.Discounts = cart.Discounts!
                    .Select(x => new AppliedSalesQuoteDiscount
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        CustomKey = x.CustomKey,
                        SlaveID = x.ID,
                        DiscountTotal = x.DiscountTotal,
                        ApplicationsUsed = x.ApplicationsUsed,
                    })
                    .ToList();
                // Add and save what we have done so far
                context.SalesQuotes.Add(entity);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                // Commit Taxes
                ////var response = taxesProvider?.Commit(TaxEntityType.SalesOrder, cart, newSalesQuote.PurchaseOrderNumber);
                ////newSalesQuote.TaxTransactionID = response?.ActionSucceeded == null ? string.Empty : response.Result;
                // Remove Items from cart
                foreach (var cartItem in cart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
                {
                    await TransferCartItemsToQuoteItemsAsync(
                            timestamp: timestamp,
                            entity: entity,
                            cartItem: cartItem,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                }
                // Transfer contacts from cart to quote
                foreach (var contact in cart.Contacts!)
                {
                    (entity.Contacts ??= new List<SalesQuoteContact>()).Add(new()
                    {
                        Active = contact.Active,
                        CreatedDate = contact.CreatedDate,
                        SlaveID = contact.ContactID,
                    });
                }
                // Copy the notes from the Cart
                await Workflows.SalesQuoteWithNotesAssociation.AssociateObjectsAsync(
                        entity: entity,
                        model: dummy,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (Contract.CheckValidKey(checkout.SpecialInstructions))
                {
                    entity.Notes!.Add(new()
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        // ReSharper disable once PossibleInvalidOperationException
                        TypeID = CustomerNoteTypeID,
                        CreatedByUserID = pricingFactoryContext.UserID,
                        Note1 = checkout.SpecialInstructions,
                    });
                }
                // Copy the Attributes from the Cart
                await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                        entity: entity,
                        model: dummy,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Import and attach the files submitted with the quote
                var storedFiles = checkout.FileNames
                    ?.Select(fileName => new SalesQuoteFileModel
                    {
                        Active = true,
                        CreatedDate = timestamp,
                        Name = fileName,
                        Slave = new()
                        {
                            Active = true,
                            CreatedDate = timestamp,
                            FileName = fileName,
                            IsStoredInDB = false,
                        },
                    })
                    .ToList();
                var dummyOrderModel = new SalesQuoteModel
                {
                    StoredFiles = storedFiles,
                    SalesQuoteCategories = checkout.CategoryIDs
                        ?.Select(c => new SalesQuoteCategoryModel { Active = true, SlaveID = c })
                        .ToList(),
                };
                await RunDefaultAssociateWorkflowsAsync(entity, dummyOrderModel, timestamp, contextProfileName).ConfigureAwait(false);
                await Workflows.SalesQuoteWithStoredFilesAssociation.AssociateObjectsAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
                await Workflows.SalesQuoteWithSalesQuoteCategoriesAssociation.AssociateObjectsAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            return ((int?)entity.ID).WrapInPassingCEFARIfNotNull();
        }

        /// <summary>Gets modified value.</summary>
        /// <param name="baseAmount">The base amount.</param>
        /// <param name="modifier">  The modifier.</param>
        /// <param name="mode">      The mode.</param>
        /// <returns>The modified value.</returns>
        protected static decimal GetModifiedValue(decimal? baseAmount, decimal? modifier, int? mode)
        {
            mode ??= 2;
            modifier ??= 0;
            baseAmount ??= 0;
            switch ((Enums.TotalsModifierModes)mode.Value)
            {
                case Enums.TotalsModifierModes.Override:
                {
                    return modifier.Value;
                }
                case Enums.TotalsModifierModes.Add:
                {
                    return baseAmount.Value + modifier.Value;
                }
                case Enums.TotalsModifierModes.PercentMarkup:
                {
                    return baseAmount.Value * ((modifier.Value + 100) / 100);
                }
                default:
                {
                    throw new ArgumentException($"Invalid modifier mode: {mode.Value}");
                }
            }
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

        /// <summary>Executes the cart is null check operation.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        protected static CEFActionResponse DoCartIsNullCheck(ICartModel cart)
        {
            return cart.WrapInPassingCEFARIfNotNull("ERROR! Could not look up your quote cart.");
        }

        /// <summary>Executes the cart empty check operation.</summary>
        /// <param name="cart">The cart.</param>
        /// <returns>A <see cref="CEFActionResponse"/>.</returns>
        protected static CEFActionResponse DoCartEmptyCheck(ICartModel cart)
        {
            return cart.SalesItems.WrapInPassingCEFARIfNotNullOrEmpty<List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>, ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(
                "ERROR! Your quote cart was empty.");
        }

        /// <summary>Enforce user in pricing factory context.</summary>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="user">                 The user.</param>
        protected static void EnforceUserInPricingFactoryContextIfSet(
            IPricingFactoryContextModel pricingFactoryContext,
            IUserModel? user)
        {
            if (user == null)
            {
                return;
            }
            pricingFactoryContext.UserID = user.ID;
            if (Contract.CheckValidID(user.AccountID))
            {
                pricingFactoryContext.AccountID = user.AccountID;
            }
        }

        /// <summary>Gets order level discounts.</summary>
        /// <param name="cart">     The cart.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A List{AppliedSalesOrderDiscount}.</returns>
        protected static List<AppliedSalesQuoteDiscount> GetOrderLevelDiscounts(
            ICartModel cart,
            DateTime timestamp)
        {
            return cart.Discounts!
                .Select(x => new AppliedSalesQuoteDiscount
                {
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = x.CustomKey,
                    SlaveID = x.DiscountID,
                    DiscountTotal = x.DiscountTotal,
                    ApplicationsUsed = x.ApplicationsUsed,
                })
                .ToList();
        }

        /// <summary>Transfer contacts list.</summary>
        /// <param name="cart">  The cart.</param>
        /// <param name="entity">The entity.</param>
        protected static void TransferContactsList(ICartModel cart, ISalesQuote entity)
        {
            if (Contract.CheckEmpty(cart.Contacts))
            {
                return;
            }
            foreach (var contact in cart.Contacts!)
            {
                (entity.Contacts ??= new HashSet<SalesQuoteContact>()).Add(new()
                {
                    Active = contact.Active,
                    CreatedDate = contact.CreatedDate,
                    SlaveID = contact.ContactID,
                });
            }
        }

        /// <summary>Gets the selected rate quote from the cart.</summary>
        /// <param name="cart">     The cart.</param>
        /// <returns>The selected rate quote(s).</returns>
        protected static List<RateQuote> GetSelectedRateQuote(ICartModel cart)
        {
            return cart.RateQuotes!
                .Where(x => x.Selected)
                .Select(x => new RateQuote
                {
                    // Base Properties
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    JsonAttributes = x.SerializableAttributes.SerializeAttributesDictionary(),
                    // NameableBase Properties
                    Name = x.Name,
                    Description = x.Description,
                    // RateQuote Properties
                    CartHash = x.CartHash,
                    Rate = x.Rate,
                    RateTimestamp = x.RateTimestamp,
                    Selected = true,
                    TargetShippingDate = x.TargetShippingDate,
                    EstimatedDeliveryDate = x.EstimatedDeliveryDate,
                    // Related objects
                    ShipCarrierMethodID = x.ShipCarrierMethodID,
                })
                .ToList();
        }

        /// <summary>Try resolve cart.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="validate">             A flag indicating to validate the cart or not.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        protected static async Task<CEFActionResponse<ICartModel>> TryResolveCartAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            bool validate,
            string? contextProfileName)
        {
            var cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookupKey: lookupKey.ToIDLookupKey(checkout.WithCartInfo!.CartID!.Value),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : Contract.CheckNotNull(checkout.WithCartInfo?.CartSessionID)
                    ? (await Workflows.Carts.SessionGetAsync(
                                lookupKey: lookupKey,
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse.Result
                    : null;
            if (cart == null)
            {
                return CEFAR.FailingCEFAR<ICartModel>("ERROR! Cart was null, no other messages provided.");
            }
            var validateResponse = validate
                ? await Workflows.CartValidator.ValidateReadyForCheckoutAsync(
                        cart: cart,
                        currentAccount: Contract.CheckValidID(pricingFactoryContext.AccountID)
                            ? await Workflows.Accounts.GetForCartValidatorAsync(
                                    accountID: pricingFactoryContext.AccountID!.Value,
                                    contextProfileName: contextProfileName)
                                .ConfigureAwait(false)
                            : null,
                        taxesProvider: taxesProvider,
                        pricingFactoryContext: pricingFactoryContext,
                        currentUserID: lookupKey.UserID ?? pricingFactoryContext.UserID,
                        currentAccountID: lookupKey.AccountID ?? pricingFactoryContext.AccountID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.PassingCEFAR();
            var response = CEFAR.FailingCEFAR<ICartModel>();
            if (!validateResponse.ActionSucceeded)
            {
                // Cart has been modified, get the cart again with changes
                cart = Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                    ? await Workflows.Carts.AdminGetAsync(
                            lookupKey: lookupKey.ToIDLookupKey(checkout.WithCartInfo!.CartID!.Value),
                            pricingFactoryContext: pricingFactoryContext,
                            taxesProvider: taxesProvider,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false)
                    : Contract.CheckNotNull(checkout.WithCartInfo?.CartSessionID)
                        ? (await Workflows.Carts.SessionGetAsync(
                                lookupKey: lookupKey,
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse.Result
                        : null;
            }
            response.ActionSucceeded = cart != null;
            response.Result = cart;
            response.Messages.AddRange(validateResponse.Messages);
            if (!response.ActionSucceeded && Contract.CheckEmpty(response.Messages))
            {
                response.Messages.Add("ERROR! Cart was null, no other messages provided.");
            }
            return response;
        }

        /// <summary>Transfer cart items to quote items.</summary>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="cartItem">          The cart item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CyclomaticComplexity, FunctionComplexityOverflow
        protected static async Task TransferCartItemsToQuoteItemsAsync(
            DateTime timestamp,
            IBase entity,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> cartItem,
            string? contextProfileName)
        {
            var item = new SalesQuoteItem
            {
                Active = true,
                CreatedDate = timestamp,
                UpdatedDate = timestamp,
                MasterID = entity.ID,
                ProductID = cartItem.ProductID,
                Name = cartItem.ProductName,
                Sku = cartItem.ProductKey,
                ForceUniqueLineItemKey = cartItem.ForceUniqueLineItemKey,
                Quantity = cartItem.Quantity,
                QuantityBackOrdered = cartItem.QuantityBackOrdered ?? 0m,
                QuantityPreSold = cartItem.QuantityPreSold ?? 0m,
                UnitCorePrice = cartItem.UnitCorePrice,
                UnitSoldPrice = cartItem.UnitCorePrice,
                ////ShippingCarrierMethodName = cart.ShipOption?.Name,
                UnitOfMeasure = cartItem.UnitOfMeasure,
            };
            // Transfer Attributes
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(item, cartItem, contextProfileName).ConfigureAwait(false);
            // Transfer Discounts
            if (cartItem.Discounts?.Any() == true)
            {
                item.Discounts ??= new List<AppliedSalesQuoteItemDiscount>();
                foreach (var cartItemDiscountModel in cartItem.Discounts)
                {
                    item.Discounts.Add(new()
                    {
                        ID = cartItemDiscountModel.ID,
                        CustomKey = cartItemDiscountModel.CustomKey,
                        Active = cartItemDiscountModel.Active,
                        CreatedDate = cartItemDiscountModel.CreatedDate,
                        UpdatedDate = cartItemDiscountModel.UpdatedDate,
                        SlaveID = cartItemDiscountModel.DiscountID,
                        DiscountTotal = cartItemDiscountModel.DiscountTotal,
                        ApplicationsUsed = cartItemDiscountModel.ApplicationsUsed,
                    });
                }
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.SalesQuoteItems.Add(item);
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
            await Workflows.CartItems.DeactivateAsync(cartItem.ID, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Relate state status and type using dummy.</summary>
        /// <param name="attributes">        The attributes.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="statusKey">         The key of the status.</param>
        /// <param name="typeKey">           The key of the type.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An ISalesOrderModel.</returns>
        protected static async Task<ISalesQuoteModel> RelateStateStatusTypeAndAttributesUsingDummyAsync(
            SerializableAttributesDictionary attributes,
            ISalesQuote entity,
            DateTime timestamp,
            string statusKey,
            string typeKey,
            string? contextProfileName)
        {
            var dummy = RegistryLoaderWrapper.GetInstance<ISalesQuoteModel>(contextProfileName);
            var state = RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName);
            state.Name = state.DisplayName = "Work";
            state.CustomKey = "WORK";
            var status = RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName);
            status.CustomKey = status.Name = status.DisplayName = statusKey;
            var type = RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName);
            type.CustomKey = type.Name = type.DisplayName = typeKey;
            dummy.State = state;
            dummy.Status = status;
            dummy.Type = type;
            await RelateRequiredStateAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredTypeAsync(entity, dummy, timestamp, contextProfileName).ConfigureAwait(false);
            dummy.SerializableAttributes = attributes;
            return dummy;
        }

        /// <summary>Relate Required Status.</summary>
        /// <param name="entity">            The entity that has a Required Status.</param>
        /// <param name="model">             The model that has a Required Status.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CyclomaticComplexity
        protected static async Task RelateRequiredStatusAsync(ISalesQuote entity, ISalesQuoteModel model, DateTime timestamp, string? contextProfileName)
        {
            // Must have the core objects on both sides
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesQuoteStatuses.ResolveWithAutoGenerateAsync(
                    byID: model.StatusID, // By Other ID
                    byKey: model.StatusKey, // By Flattened Other Key
                    byName: model.StatusName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Status,
                    contextProfileName: contextProfileName)
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
                entity.Status = (SalesQuoteStatus)resolved.Result!.CreateSalesQuoteStatusEntity(timestamp, contextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Status to the SalesQuote entity");
        }

        /// <summary>Relate Required State.</summary>
        /// <param name="entity">            The entity that has a Required State.</param>
        /// <param name="model">             The model that has a Required State.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CyclomaticComplexity
        protected static async Task RelateRequiredStateAsync(ISalesQuote entity, ISalesQuoteModel model, DateTime timestamp, string? contextProfileName)
        {
            // Must have the core objects on both sides
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesQuoteStates.ResolveWithAutoGenerateAsync(
                    byID: model.StateID, // By Other ID
                    byKey: model.StateKey, // By Flattened Other Key
                    byName: model.StateName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.State,
                    contextProfileName: contextProfileName)
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
                entity.State = (SalesQuoteState)resolved.Result!.CreateSalesQuoteStateEntity(timestamp, contextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given State to the SalesQuote entity");
        }

        /// <summary>Relate Required Type.</summary>
        /// <param name="entity">            The entity that has a Required Type.</param>
        /// <param name="model">             The model that has a Required Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        // ReSharper disable once CyclomaticComplexity
        protected static async Task RelateRequiredTypeAsync(ISalesQuote entity, ISalesQuoteModel model, DateTime timestamp, string? contextProfileName)
        {
            // Must have the core objects on both sides
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SalesQuoteTypes.ResolveWithAutoGenerateAsync(
                    byID: model.TypeID, // By Other ID
                    byKey: model.TypeKey, // By Flattened Other Key
                    byName: model.TypeName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Type,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.TypeID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Type == null;
            if (resolved.Result == null && model.Type != null)
            {
                resolved.Result = model.Type;
            }
            var modelObjectIsNull = resolved.Result == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable TypeID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.TypeID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Type!.ID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Type!.UpdateSalesQuoteTypeFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.TypeID = resolved.Result!.ID;
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
                throw new InvalidOperationException("Cannot assign an inactive TypeID to the SalesQuote entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.TypeID = resolved.Result!.ID;
                return;
            }
            // ReSharper disable once InvertIf
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.TypeID = 0;
                entity.Type = (SalesQuoteType)resolved.Result!.CreateSalesQuoteTypeEntity(timestamp, contextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Type to the SalesQuote entity");
        }

        /// <summary>Executes all associate workflows operations with the default information.</summary>
        /// <remarks>Perform any extra resolvers before running this function.<br/>
        /// The calls are nullable so you could null out specific workflow if you don't want to it to run.</remarks>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task RunDefaultAssociateWorkflowsAsync(
            ISalesQuote entity, ISalesQuoteModel model, DateTime timestamp, string? contextProfileName)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, contextProfileName).ConfigureAwait(false);
            if (!Contract.CheckValidKey(entity.JsonAttributes))
            {
                entity.JsonAttributes = "{}";
            }
            if (model.SalesItems != null)
            {
                await Workflows.SalesQuoteWithSalesItemsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            /*
            if (model.Discounts != null)
            {
                await Workflows.AssociateSalesQuoteDiscounts.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            */
            if (model.StoredFiles != null)
            {
                await Workflows.SalesQuoteWithStoredFilesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (model.Contacts != null)
            {
                await Workflows.SalesQuoteWithContactsAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (model.RateQuotes != null)
            {
                await Workflows.SalesQuoteWithRateQuotesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (model.Notes != null)
            {
                await Workflows.SalesQuoteWithNotesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            // Skipped: Not supposed to make it or fully deprecated: Children
            if (model.AssociatedSalesOrders != null)
            {
                await Workflows.SalesQuoteWithAssociatedSalesOrdersAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
            if (model.SalesQuoteCategories != null)
            {
                await Workflows.SalesQuoteWithSalesQuoteCategoriesAssociation.AssociateObjectsAsync(entity, model, timestamp, contextProfileName).ConfigureAwait(false);
            }
        }

        /// <summary>Gets cart.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cartType">             Type of the cart.</param>
        /// <param name="currentUserID">        Identifier for the current user.</param>
        /// <param name="currentAccountID">     Identifier for the current account.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The cart.</returns>
        protected virtual async Task<ICartModel?> GetCartAsync(
            ICheckoutModel checkout,
            string cartType,
            int? currentUserID,
            int? currentAccountID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            return Contract.CheckValidID(checkout.WithCartInfo?.CartID)
                ? await Workflows.Carts.AdminGetAsync(
                        lookupKey: new CartByIDLookupKey(
                            cartID: checkout.WithCartInfo!.CartID!.Value,
                            userID: currentUserID,
                            accountID: currentAccountID,
                            brandID: pricingFactoryContext.BrandID,
                            franchiseID: pricingFactoryContext.FranchiseID,
                            storeID: pricingFactoryContext.StoreID),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false)
                : (checkout.WithCartInfo?.CartSessionID).HasValue
                    ? (await Workflows.Carts.SessionGetAsync(
                                lookupKey: new SessionCartBySessionAndTypeLookupKey(
                                    sessionID: checkout.WithCartInfo!.CartSessionID!.Value,
                                    typeKey: cartType,
                                    userID: currentUserID,
                                    accountID: currentAccountID,
                                    brandID: pricingFactoryContext.BrandID,
                                    franchiseID: pricingFactoryContext.FranchiseID,
                                    storeID: pricingFactoryContext.StoreID),
                                pricingFactoryContext: pricingFactoryContext,
                                taxesProvider: taxesProvider,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .cartResponse.Result
                    : null;
        }

        /// <summary>Copies the notes.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="entity">               The entity.</param>
        /// <param name="dummy">                The dummy.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task CopyNotesAsync(
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
            if (Contract.CheckEmpty(entity.Notes))
            {
                entity.Notes = new HashSet<Note>();
                var (cartID, _) = Contract.CheckValidID(checkout.WithCartInfo!.CartID)
                    ? (checkout.WithCartInfo.CartID!.Value, null)
                    : await Workflows.Carts.SessionGetAsIDAsync(
                            new(
                                sessionID: checkout.WithCartInfo.CartSessionID!.Value,
                                typeKey: checkout.WithCartInfo.CartTypeName!,
                                userID: pricingFactoryContext.UserID,
                                accountID: pricingFactoryContext.AccountID,
                                brandID: pricingFactoryContext.BrandID,
                                franchiseID: pricingFactoryContext.FranchiseID,
                                storeID: pricingFactoryContext.StoreID),
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
            entity.Notes ??= new HashSet<Note>();
            entity.Notes!.Add(new()
            {
                Active = true,
                CreatedDate = timestamp,
                TypeID = CustomerNoteTypeID,
                CreatedByUserID = pricingFactoryContext.UserID,
                Note1 = checkout.SpecialInstructions,
            });
        }

        /// <summary>Try resolve user.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{IUserModel}.</returns>
        protected async Task<CEFActionResponse<IUserModel?>> TryResolveUserAsync(
            ICheckoutModel checkout,
            string? contextProfileName)
        {
            // No username means Guest Checkout
            if (!Contract.CheckValidID(checkout.WithUserInfo?.UserID)
                && !Contract.CheckValidKey(checkout.WithUserInfo?.UserName)
                && !Contract.CheckValidKey(checkout.WithUserInfo?.ExternalUserID))
            {
                return CEFAR.FailingCEFAR<IUserModel?>(
                    "WARNING! No user data to resolve a user with, assuming Guest Checkout.");
            }
            // Get User, Create if doesn't exist
            var currentUserName = checkout.WithUserInfo!.ExternalUserID ?? checkout.WithUserInfo.UserName;
            var user = Contract.CheckValidID(checkout.WithUserInfo.UserID)
                ? await Workflows.Users.GetAsync(checkout.WithUserInfo.UserID!.Value, contextProfileName).ConfigureAwait(false)
                : await Workflows.Users.GetAsync(currentUserName!, contextProfileName).ConfigureAwait(false);
            if (user == null)
            {
                user = await CreateUserFromCheckoutAsync(checkout, contextProfileName).ConfigureAwait(false);
            }
            else
            {
                if (user.Account is { Active: false })
                {
                    return CEFAR.FailingCEFAR<IUserModel?>(
                        "ERROR! Your Account has been deactivated. Please contact support for assistance.");
                }
                BackFillContactInfoFromUserIfMissing(checkout.Billing, checkout.Shipping, user);
            }
            return user.WrapInPassingCEFAR();
        }

        /// <summary>Gets user.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The user.</returns>
        protected virtual async Task<IUserModel?> GetUserAsync(ICheckoutModel checkout, string? contextProfileName)
        {
            // No username means Guest Checkout
            if (string.IsNullOrWhiteSpace(checkout.WithUserInfo?.ExternalUserID)
                && !(checkout.WithUserInfo?.UserID).HasValue
                && string.IsNullOrWhiteSpace(checkout.WithUserInfo?.UserName))
            {
                return null;
            }
            // Get User, Create if doesn't exist
            // ReSharper disable once PossibleNullReferenceException (Verified above)
            var currentUserName = checkout.WithUserInfo!.ExternalUserID ?? checkout.WithUserInfo.UserName;
            var user = checkout.WithUserInfo.UserID.HasValue
                ? await Workflows.Users.GetAsync(checkout.WithUserInfo.UserID.Value, contextProfileName).ConfigureAwait(false)
                : await Workflows.Users.GetAsync(currentUserName!, contextProfileName).ConfigureAwait(false);
            if (user == null)
            {
                user = await CreateUserFromCheckoutAsync(
                        checkout,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            else
            {
                RelateContactInfoIfMissing(checkout.Shipping, user);
            }
            return user;
        }

        /// <summary>Creates user from checkout.</summary>
        /// <param name="checkout">            The checkout.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>The new user from checkout.</returns>
        protected virtual async Task<IUserModel> CreateUserFromCheckoutAsync(
            ICheckoutModel checkout,
            string? contextProfileName)
        {
            var user = RegistryLoaderWrapper.GetInstance<IUserModel>(contextProfileName);
            if (CEFConfigDictionary.PurchaseCreateAccountEnabled)
            {
                var account = RegistryLoaderWrapper.GetInstance<IAccountModel>(contextProfileName);
                account.Active = true;
                account.CreatedDate = DateExtensions.GenDateTime;
                account.Name = checkout.Shipping?.FirstName + checkout.Shipping?.LastName;
                account.StatusID = 1;
                account.TypeID = 1;
                account.IsTaxable = true;
                var createAccountResponse = await Workflows.Accounts.CreateAsync(account, contextProfileName)
                    .AwaitAndThrowIfFailedAsync()
                    .ConfigureAwait(false);
                user.AccountID = createAccountResponse.Result;
            }
            user.UserName = checkout.WithUserInfo?.ExternalUserID ?? checkout.WithUserInfo?.UserName;
            if (Contract.CheckInvalidID(UserStatusIDOfRegistered))
            {
                UserStatusIDOfRegistered = await Workflows.UserStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Registered",
                        byName: "Registered",
                        byDisplayName: "Registered",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(UserTypeIDOfCustomer))
            {
                UserTypeIDOfCustomer = await Workflows.UserTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Customer",
                        byName: "Customer",
                        byDisplayName: "Customer",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            user.StatusID = UserStatusIDOfRegistered;
            user.TypeID = UserTypeIDOfCustomer;
            user.Contact = (ContactModel?)checkout.Shipping;
            user.BillingAddress = (AddressModel?)checkout.Shipping?.Address;
            user.OverridePassword = checkout.WithUserInfo?.Password;
            var createResponse = await Workflows.Users.CreateAsync(user, contextProfileName)
                .AwaitAndThrowIfFailedAsync()
                .ConfigureAwait(false);
            return (await Workflows.Users.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false))!;
        }

        /// <summary>Adds the addresses to book to 'user'.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="user">              The user.</param>
        /// <param name="nothingToShip">     A flag indicating there was nothing to ship.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected async Task HandleAddressBookAsync(
            ICheckoutModel checkout,
            IUserModel? user,
            bool nothingToShip,
            string? contextProfileName)
        {
            /*
            if (!CommonCheckoutProviderConfig.AddAddressesToBook)
            {
                return;
            }
            */
            if (!Contract.CheckValidID(user?.AccountID))
            {
                return;
            }
            // ReSharper disable once PossibleInvalidOperationException
            if (!Contract.CheckValidID(await Workflows.Accounts.CheckExistsAsync(
                        user!.AccountID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false)))
            {
                return;
            }
            var addressBook = await Workflows.AddressBooks.GetAddressBookAsync(
                    user.AccountID.Value,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!nothingToShip
                && checkout.Shipping != null // Targets checkout will have this be null
                && addressBook.All(x => x.ContactKey != checkout.Shipping.CustomKey))
            {
                await AddAddressToBookAsync(
                        contact: checkout.Shipping,
                        accountID: user.AccountID.Value,
                        currentUserID: user.ID,
                        isBilling: false,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Back fill contact information from user if missing.</summary>
        /// <param name="billingContact"> The billing contact.</param>
        /// <param name="shippingContact">The shipping contact.</param>
        /// <param name="user">           The user.</param>
        private static void BackFillContactInfoFromUserIfMissing(
            IContactModel? billingContact,
            IContactModel? shippingContact,
            IHaveAContactBaseModel? user)
        {
            if (user?.Contact == null)
            {
                return;
            }
            if (billingContact != null)
            {
                if (string.IsNullOrWhiteSpace(billingContact.FirstName)
                    || string.IsNullOrWhiteSpace(billingContact.LastName))
                {
                    billingContact.FirstName = user.Contact.FirstName;
                    billingContact.LastName = user.Contact.LastName;
                }
                if (string.IsNullOrWhiteSpace(billingContact.Address?.Street1))
                {
                    billingContact.Address = user.Contact.Address;
                }
            }
            // ReSharper disable once InvertIf
            if (shippingContact != null)
            {
                if (string.IsNullOrWhiteSpace(shippingContact.FirstName)
                    || string.IsNullOrWhiteSpace(shippingContact.LastName))
                {
                    shippingContact.FirstName = user.Contact.FirstName;
                    shippingContact.LastName = user.Contact.LastName;
                }
                if (string.IsNullOrWhiteSpace(shippingContact.Address?.Street1))
                {
                    shippingContact.Address = billingContact?.Address;
                }
            }
        }

        /// <summary>Relate contact information if missing.</summary>
        /// <param name="shippingContact">The shipping contact.</param>
        /// <param name="user">           The user.</param>
        private static void RelateContactInfoIfMissing(
            IContactModel? shippingContact,
            IHaveAContactBaseModel? user)
        {
            if (user == null
                || Contract.CheckValidKey(shippingContact?.FirstName)
                && Contract.CheckValidKey(shippingContact?.LastName))
            {
                return;
            }
            shippingContact!.FirstName = user.Contact!.FirstName;
            shippingContact.LastName = user.Contact.LastName;
        }

        /// <summary>Adds the address to book.</summary>
        /// <param name="contact">           The contact.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="isBilling">         True if this CheckoutProviderBase is billing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AddAddressToBookAsync(
            // ReSharper disable once SuggestBaseTypeForParameter
            IContactModel contact,
            int accountID,
            int currentUserID,
            bool isBilling,
            string? contextProfileName)
        {
            try
            {
                var model = RegistryLoaderWrapper.GetInstance<IAccountContactModel>(contextProfileName);
                model.Active = true;
                model.CustomKey = contact.CustomKey;
                // TODO: Make these set by a value chosen by the user instead of always doing it
                model.IsBilling = isBilling;
                model.IsPrimary = !isBilling;
                model.TransmittedToERP = false;
                model.MasterID = model.MasterID = accountID;
                var slave = (IContactModel)contact.DeepCopy();
                WipeIDsFromContact(slave);
                model.Slave = slave;
                await Workflows.AddressBooks.CreateAddressInBookAsync(model, currentUserID, contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(SalesQuoteSubmitProviderBase)}.{nameof(AddAddressToBookAsync)}",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
        }
    }
}
