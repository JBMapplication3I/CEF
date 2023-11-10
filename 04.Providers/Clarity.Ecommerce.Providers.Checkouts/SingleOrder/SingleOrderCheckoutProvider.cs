// <copyright file="SingleOrderCheckoutProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the single order checkout provider class</summary>
namespace Clarity.Ecommerce.Providers.Checkouts.SingleOrder
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
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A single order checkout provider.</summary>
    /// <seealso cref="CheckoutProviderBase"/>
    public class SingleOrderCheckoutProvider : CheckoutProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => SingleOrderCheckoutProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

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
            throw new InvalidOperationException("ERROR! Single Order Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<List<ICartModel?>?>> AnalyzeAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new InvalidOperationException("ERROR! Single Order Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new InvalidOperationException("ERROR! Single Order Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> ClearAnalysisAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            CartByIDLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName)
        {
            throw new InvalidOperationException("ERROR! Single Order Checkout doesn't support this process");
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            IPricingFactoryContextModel pricingFactoryContext,
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
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
                    taxesProvider: taxesProvider,
                    gateway: gateway,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<ICheckoutResult> CheckoutAsync(
            ICheckoutModel checkout,
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            int? selectedAccountID,
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
            if (cart == null)
            {
                var retVal = new CheckoutResult();
                retVal.ErrorMessages.Add("ERROR! Cart was null");
                return retVal;
            }
            return await CheckoutInnerAsync(
                    checkout: checkout,
                    cart: cart,
                    nothingToShip: cart.SalesItems!.All(x => x.ProductNothingToShip),
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    gateway: gateway,
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
            ////if (Contract.CheckValidKey(checkout.PaymentStyle) && UsePreferredPaymentMethodForLaterPayments)
            ////{
            ////    attributes[PreferredPaymentMethodAttr.CustomKey] = new SerializableAttributeObject
            ////    {
            ////        // ReSharper disable once PossibleInvalidOperationException
            ////        ID = PreferredPaymentMethodAttr.ID.Value,
            ////        Key = PreferredPaymentMethodAttr.CustomKey,
            ////        Value = checkout.PaymentStyle
            ////    };
            ////}
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

        /// <summary>Process the emails for checkout.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="order">             The order.</param>
        /// <param name="result">            The result.</param>
        /// <param name="cart">              The cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task ProcessEmailsForCheckoutAsync(
            ICheckoutModel checkout,
            ISalesOrderModel order,
            ICheckoutResult result,
            ICartModel cart,
            string? contextProfileName)
        {
            if (order.SalesOrderPayments?.Count <= 0)
            {
                var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                order.SalesOrderPayments = context.SalesOrderPayments
                    .Where(x => x.MasterID == order.ID)
                    .SelectFullSalesOrderPaymentAndMapToSalesOrderPaymentModel(contextProfileName)
                    .ToList();
            }
            order.PaymentTransactionID = result.PaymentTransactionID;
            order.PurchaseOrderNumber = checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber;
            if (CEFConfigDictionary.GetClosestWarehouseWithStock)
            {
                try
                {
                    var sameAsBilling = cart.ShippingSameAsBilling ?? false;
                    var destination = await Workflows.Contacts.GetAsync(
                            Contract.RequiresValidID(sameAsBilling ? cart.BillingContactID : cart.ShippingContactID),
                            contextProfileName)
                        .ConfigureAwait(false);
                    var regionCode = destination?.Address?.RegionCode;
                    await new SalesOrdersWarehouseShippingInformationToBackOfficeEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new()
                            {
                                ["cart"] = cart,
                                ["orderID"] = Contract.RequiresValidID(order.ID),
                                ["regionCode"] = regionCode,
                            })
                        .ConfigureAwait(false);
                    foreach (var cartItem in cart.SalesItems!)
                    {
                        await Workflows.CartItems.DeactivateAsync(
                                Contract.RequiresValidID(cartItem.ID),
                                contextProfileName)
                            .ConfigureAwait(false);
                    }
                }
                catch (Exception ex2)
                {
                    Logger.LogError("Send Checkout Back-office Email", ex2.Message, ex2, null, contextProfileName);
                }
            }
            try
            {
                await new SalesOrdersSubmittedNormalToCustomerEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = order, })
                    .ConfigureAwait(false);
            }
            catch (Exception ex1)
            {
                result.WarningMessage ??= string.Empty;
                var message = $"There was an error sending the customer order confirmation for order id {result.OrderID}.";
                result.WarningMessage += message + "\r\n";
                result.WarningMessages.Add(message);
                result.WarningMessages.Add(ex1.Message);
            }
            try
            {
                await new SalesOrdersSubmittedNormalToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new() { ["salesOrder"] = order, })
                    .ConfigureAwait(false);
            }
            catch (Exception ex2)
            {
                Logger.LogError("Send Checkout Back-office Email", ex2.Message, ex2, null, contextProfileName);
            }
            if (CEFConfigDictionary.FranchisesEnabled && CEFConfigDictionary.StoresEnabled)
            {
                try
                {
                    await new SalesOrdersSubmittedNormalToBackOfficeFranchiseEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesOrder"] = order, },
                            customReplacements: new() { ["OrderNotes"] = order.Notes })
                        .ConfigureAwait(false);
                }
                catch (Exception ex3)
                {
                    Logger.LogError("Send Checkout Franchise Back-office Email", ex3.Message, ex3, null, contextProfileName);
                }
            }
        }

        /// <summary>Sets account identifier to selected affiliate key.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected static async Task SetAccountIDToSelectedAffiliateKeyAsync(
            ICheckoutModel checkout,
            ISalesOrder entity,
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

        /// <summary>Creates a sales order from a checkout and user object.</summary>
        /// <param name="context">              The context.</param>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="nothingToShip">        True to nothing to ship.</param>
        /// <param name="pricingFactoryContext">The pricing factory context.</param>
        /// <param name="processPaymentResult"> The process payment result.</param>
        /// <param name="paymentTransactionID"> The Payment Transaction ID.</param>
        /// <param name="originalCurrencyKey">  The original currency key.</param>
        /// <param name="sellingCurrencyKey">   The selling currency key.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ISalesOrderModel.</returns>
        // ReSharper disable once FunctionComplexityOverflow
        protected virtual async Task<ISalesOrderModel> CreateSingleAsync(
            IClarityEcommerceEntities context,
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel? user,
            bool nothingToShip,
            IPricingFactoryContextModel pricingFactoryContext,
            CEFActionResponse<IProcessPaymentResponse> processPaymentResult,
            string? paymentTransactionID,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            ITaxesProviderBase? taxesProvider,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Resolve the contacts
            WipeIDsFromMainContacts(cart);
            var billingContact = (await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                        model: WipeIDsFromContact(checkout.Billing),
                        timestamp: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .Result;
            var shippingContact = nothingToShip
                ? null
                : (await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                            model: WipeIDsFromContact(checkout.IsSameAsBilling ? checkout.Billing : checkout.Shipping),
                            timestamp: null,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false))
                    .Result;
            if (billingContact is not null)
            {
                billingContact.TypeID = BillingTypeID;
            }
            if (!nothingToShip && shippingContact is not null)
            {
                shippingContact.TypeID = ShippingTypeID;
            }
            // Create the new entity
            var entity = new SalesOrder
            {
                // Base Properties
                Active = true,
                CreatedDate = timestamp,
                // TODO@BE: New SalesOrder.CustomKey with a custom iteration pattern in Single Order Checkout
                // Sales order Properties
                PurchaseOrderNumber = string.IsNullOrWhiteSpace(checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber)
                    ? null
                    : checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber,
                PaymentTransactionID = paymentTransactionID,
                ShippingSameAsBilling = checkout.IsSameAsBilling,
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
                TypeID = OrderTypeID,
                StateID = OrderStateID,
                BillingContact = billingContact,
                ShippingContact = shippingContact,
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
                + (Math.Abs(entity.SubtotalDiscounts) * -1);
            entity.BalanceDue = processPaymentResult.Result!.BalanceDue;
            string statusKey;
            if (Contract.CheckValidID(pricingFactoryContext.AccountID))
            {
                var account = await Workflows.Accounts.GetAsync(
                        pricingFactoryContext.AccountID!.Value,
                        contextProfileName)
                    .ConfigureAwait(false);
                var useOnHold = account!.IsOnHold;
                if (!useOnHold && account.Credit > 0 && entity.BalanceDue > 0)
                {
                    useOnHold = account.Credit - entity.BalanceDue <= 0;
                }
                statusKey = entity.BalanceDue > 0
                    ? useOnHold
                        ? "On Hold"
                        : "Pending"
                    : "Full Payment Received";
            }
            else
            {
                statusKey = entity.BalanceDue > 0
                    ? "Pending"
                    : "Full Payment Received";
            }
            if (Contract.CheckValidKey(checkout.PaymentStyle))
            {
                var paymentAttributeObject = new SerializableAttributeObject
                {
                    Key = "PaymentMethod",
                    Value = checkout.PaymentStyle!,
                };
                cart.SerializableAttributes!.AddOrUpdate(
                    "PaymentMethod",
                    paymentAttributeObject,
                    (_, _) => paymentAttributeObject);
            }
            var payment = checkout.CreatePaymentModelFromCheckoutModel();
            if (Contract.CheckValidKey(payment.Last4CardDigits))
            {
                var cardLastFourAttributeObject = new SerializableAttributeObject
                {
                    Key = "CardLastFour",
                    Value = payment.Last4CardDigits!,
                };
                cart.SerializableAttributes!.AddOrUpdate(
                    "CardLastFour",
                    cardLastFourAttributeObject,
                    (_, _) => cardLastFourAttributeObject);
            }
            if (Contract.CheckValidKey(cart.RateQuotes?.Find(x => x.Active && x.Selected)?.ShipCarrierMethodName))
            {
                var shippingMethodAttributeObject = new SerializableAttributeObject
                {
                    Key = "ShippingMethod",
                    Value = cart.RateQuotes!.Find(x => x.Active && x.Selected)!.ShipCarrierMethodName!,
                };
                cart.SerializableAttributes!.AddOrUpdate(
                    "ShippingMethod",
                    shippingMethodAttributeObject,
                    (_, _) => shippingMethodAttributeObject);
            }
            var typeKey = CEFConfigDictionary.CheckoutSalesOrderDefaultTypeKey ?? "WEB";
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
            context.SalesOrders.Add(entity);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (taxesProvider != null)
            {
                // Commit Taxes
                var taxes = await taxesProvider.CommitCartAsync(
                        cart,
                        user?.ID,
                        contextProfileName,
                        checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber,
                        checkout.WithTaxes?.VatID)
                    .ConfigureAwait(false);
                entity.TaxTransactionID = taxes == null
                    ? string.Empty
                    : $"DOC-{DateTime.Today:yyMMdd}-{cart.ID}";
            }
            foreach (var cartItem in cart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                await TransferCartItemsToOrderItemsAsync(
                        context,
                        timestamp,
                        entity,
                        cartItem,
                        contextProfileName)
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
            // Process Payoneer for the order if set and data was properly provided
            await ProcessPayoneerForOrderAsync(
                    storeID: null, ////cart.SalesItems.First().StoreID,
                    buyer: user,
                    order: entity,
                    cart: cart,
                    amount: entity.Total,
                    paymentStyle: checkout.PaymentStyle,
                    overridePayoneerAccountID: checkout.PayByPayoneer?.PayoneerAccountID,
                    overridePayoneerCustomerID: checkout.PayByPayoneer?.PayoneerCustomerID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            // Copy the Attributes from the Cart
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                    entity: entity,
                    model: dummy,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (checkout.FileNames?.Count > 0)
            {
                var salesOrderFiles = entity.StoredFiles ?? new HashSet<SalesOrderFile>();
                foreach (var file in checkout.FileNames)
                {
                    salesOrderFiles.Add(new()
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
                entity.StoredFiles = salesOrderFiles;
            }
            // Save any additional content or changes that occurred
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return (await Workflows.SalesOrders.GetAsync(entity.ID, contextProfileName).ConfigureAwait(false))!;
        }

        /// <summary>Checkout inner.</summary>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="nothingToShip">        True to nothing to ship.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="gateway">              The gateway.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{ICheckoutResult}.</returns>
        protected virtual async Task<ICheckoutResult> CheckoutInnerAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            bool nothingToShip,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
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
            var (order, checkoutResult) = await GenerateOrderAndCheckoutResultAsync(
                    context: context,
                    checkout: checkout,
                    cart: cart,
                    user: user,
                    nothingToShip: nothingToShip,
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    gateway: gateway,
                    result: result,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            result = checkoutResult;
            if (result is not { Succeeded: true } || result.OrderID == null && result.OrderIDs?.Any() != true || order is null)
            {
                // There was an error and the error message was put into ErrorMessage/ErrorMessages
                return result ?? RegistryLoaderWrapper.GetInstance<ICheckoutResult>(contextProfileName);
            }
            await HandleAddressBookAsync(
                    checkout: checkout,
                    user: user,
                    nothingToShip: nothingToShip,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await HandleWalletAsync(
                    user: user,
                    checkout: checkout,
                    gateway: gateway,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await ProcessMembershipsForOrderAsync(
                    salesOrder: order,
                    pricingFactoryContext: pricingFactoryContext,
                    invoiceID: null,
                    timestamp: order.CreatedDate,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await ProcessSubscriptionsForOrderAsync(
                    salesOrder: order,
                    pricingFactoryContext: pricingFactoryContext,
                    salesGroupID: null,
                    invoiceID: null,
                    timestamp: order.CreatedDate,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await ProcessCalendarEventsForOrderAsync(
                    result: result,
                    contextUserID: pricingFactoryContext.UserID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await ProcessEmailsForCheckoutAsync(
                    checkout: checkout,
                    order: order,
                    result: result,
                    cart: cart,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return result;
        }

        /// <summary>Generates an order and checkout result.</summary>
        /// <param name="context">              The context.</param>
        /// <param name="checkout">             The checkout.</param>
        /// <param name="cart">                 The cart.</param>
        /// <param name="user">                 The user.</param>
        /// <param name="nothingToShip">        True to nothing to ship.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="gateway">              The gateway.</param>
        /// <param name="result">               The result.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The order and checkout result.</returns>
        protected async Task<(ISalesOrderModel? order, ICheckoutResult? result)> GenerateOrderAndCheckoutResultAsync(
            IClarityEcommerceEntities context,
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel? user,
            bool nothingToShip,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            IPaymentsProviderBase? gateway,
            ICheckoutResult result,
            string? contextProfileName)
        {
            (ISalesOrderModel? order, ICheckoutResult? result) retVal = (null, null);
            var useWalletResult = await TryToUseWalletIfSetAsync(
                    checkout,
                    pricingFactoryContext.UserID,
                    contextProfileName)
                .ConfigureAwait(false);
            if (!useWalletResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages = useWalletResult.Messages;
                retVal.result = result;
                return retVal;
            }
            // Gets the currency key from one of three places in order of priority: checkout/wallet -> user -> cookie
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
                    GetModifiedValue(cart.Totals.Discounts, cart.SubtotalDiscountsModifier, cart.SubtotalDiscountsModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Fees = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(cart.Totals.Fees, cart.SubtotalFeesModifier, cart.SubtotalFeesModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Handling = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(cart.Totals.Handling, cart.SubtotalHandlingModifier, cart.SubtotalHandlingModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            cart.Totals.Shipping = await Workflows.Currencies.ConvertAsync(
                    originalCurrencyKey,
                    sellingCurrencyKey,
                    GetModifiedValue(cart.Totals.Shipping, cart.SubtotalShippingModifier, cart.SubtotalShippingModifierMode),
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
                    GetModifiedValue(cart.Totals.Tax, cart.SubtotalTaxesModifier, cart.SubtotalTaxesModifierMode),
                    contextProfileName)
                .ConfigureAwait(false);
            var processPaymentResult = await ProcessPaymentAsync(
                    checkout: checkout,
                    cart: cart,
                    gateway: gateway,
                    originalCurrencyKey: originalCurrencyKey,
                    sellingCurrencyKey: sellingCurrencyKey,
                    paymentAlreadyConverted: true,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!processPaymentResult.ActionSucceeded)
            {
                result.Succeeded = false;
                result.ErrorMessages = processPaymentResult.Messages;
                retVal.result = result;
                return retVal;
            }
            var section = "Payment";
            try
            {
                section = "Payment Token Storage";
                result.Token = processPaymentResult.Result!.Token;
                result.PaymentTransactionID = processPaymentResult.Result.TransactionID;
                section = "Attributes Storage";
                section = "Creating Order Record";
                var order = await CreateSingleAsync(
                        context: context,
                        checkout: checkout,
                        cart: cart,
                        user: user,
                        nothingToShip: nothingToShip,
                        pricingFactoryContext: pricingFactoryContext,
                        processPaymentResult: processPaymentResult,
                        paymentTransactionID: processPaymentResult.Result.TransactionID ?? result.Token ?? string.Empty,
                        originalCurrencyKey: originalCurrencyKey,
                        sellingCurrencyKey: sellingCurrencyKey,
                        taxesProvider: taxesProvider,
                        timestamp: DateExtensions.GenDateTime,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                order.RateQuotes = cart.RateQuotes;
                result.OrderID = order.ID;
                retVal.order = order;
                var invoiceActionsProvider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName);
                if (processPaymentResult.Result.MakeInvoice)
                {
                    section = "Creating Invoice for Order";
                    order.BalanceDue = processPaymentResult.Result.BalanceDue;
                    if (invoiceActionsProvider is not null)
                    {
                        await invoiceActionsProvider.CreateSalesInvoiceFromSalesOrderAsync(
                                salesOrder: order,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                    }
                }
                else
                {
                    section = "Saving the payment made at Checkout to order";
                    var amount = cart.Totals.Total - processPaymentResult.Result.BalanceDue;
                    order.BalanceDue = processPaymentResult.Result.BalanceDue;
                    var paymentModel = checkout.CreatePaymentModelFromCheckoutModel();
                    paymentModel.CurrencyID = sellingCurrencyID ?? originalCurrencyID ?? DefaultCurrencyID;
                    paymentModel.Amount = amount;
                    paymentModel.Token = processPaymentResult.Result.Token;
                    paymentModel.TransactionNumber = processPaymentResult.Result.TransactionID;
                    paymentModel.TypeKey = checkout.PayByCreditCard?.CardType ?? "Other";
                    paymentModel.StatusKey = "Payment Received";
                    var method = MapToPaymentMethodKey(checkout.PaymentStyle!);
                    paymentModel.PaymentMethodID = await Workflows.PaymentMethods.ResolveWithAutoGenerateToIDAsync(
                            byID: null,
                            byKey: method,
                            model: new PaymentMethodModel
                            {
                                CustomKey = method,
                                Name = method,
                            },
                            contextProfileName: null)
                        .ConfigureAwait(false);
                    var payment = await Workflows.Payments.CreateEntityWithoutSavingAsync(
                            model: paymentModel,
                            timestamp: DateExtensions.GenDateTime,
                            contextProfileName: contextProfileName)
                        .ConfigureAwait(false);
                    context.Payments.Add(payment.Result!);
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                    paymentModel.ID = payment.Result!.ID;
                    if (order.BalanceDue > 0)
                    {
                        section = "Saving the payment made at Checkout to order / And creating an invoice";
                        // We have a balance due, so create a sales invoice for the full order balance
                        // then add the payment submitted to the invoice (which will reduce the balance due on it)
                        paymentModel.TransactionNumber = result.PaymentTransactionID;
                        if (invoiceActionsProvider is not null)
                        {
                            var createSalesInvoiceResult = await invoiceActionsProvider.CreateSalesInvoiceFromSalesOrderAsync(
                                    salesOrder: order,
                                    contextProfileName: contextProfileName)
                                .ConfigureAwait(false);
                            if (createSalesInvoiceResult.ActionSucceeded)
                            {
                                await invoiceActionsProvider.AddPaymentAsync(
                                        salesInvoice: createSalesInvoiceResult.Result!,
                                        payment: paymentModel,
                                        originalPayment: paymentModel.Amount,
                                        contextProfileName: contextProfileName)
                                    .ConfigureAwait(false);
                            }
                        }
                    }
                    section = "Saving the payment made at Checkout to order / adding to collection";
                    // Just apply the payment to the sales order and don't create an invoice
                    context.SalesOrderPayments.Add(new()
                    {
                        Active = true,
                        CreatedDate = order.CreatedDate,
                        SlaveID = payment.Result.ID,
                        MasterID = order.ID,
                    });
                    await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
                }
                section = "Returning values";
                result.Succeeded = true;
                retVal.result = result;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        name: $"{nameof(SingleOrderCheckoutProvider)}.{nameof(GenerateOrderAndCheckoutResultAsync)}.Exception",
                        message: ex.Message,
                        ex: ex,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                result.Succeeded = false;
                result.ErrorMessages.Add(ex.Message);
                result.ErrorMessages.Add($"Failed at: {section}");
                retVal.result = result;
                return retVal;
            }
            return retVal;
        }
    }
}
