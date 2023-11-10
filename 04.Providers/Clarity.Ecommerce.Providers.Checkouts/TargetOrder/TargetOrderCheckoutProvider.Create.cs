// <copyright file="TargetOrderCheckoutProvider.Create.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the target order checkout provider class</summary>
// ReSharper disable MultipleSpaces
#pragma warning disable IDE1006 // Naming Styles
namespace Clarity.Ecommerce.Providers.Checkouts.TargetOrder
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
    using Interfaces.Providers.Taxes;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class TargetOrderCheckoutProvider
    {
        /// <summary>Creates master sales items from original cart.</summary>
        /// <param name="originalCart">      The original cart.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new master sales items from original cart.</returns>
        // ReSharper disable once CyclomaticComplexity
        protected static async Task<List<SalesOrderItem>> CreateMasterSalesItemsFromOriginalCartAsync(
            ICartModel originalCart,
            DateTime timestamp,
            string? contextProfileName)
        {
            var resultItems = new List<SalesOrderItem>();
            foreach (var cartItem in originalCart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                var salesItem = new SalesOrderItem
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
                    UnitSoldPrice = GetModifiedValue(
                        cartItem.UnitSoldPrice,
                        cartItem.UnitSoldPriceModifier,
                        cartItem.UnitSoldPriceModifierMode),
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
                await Workflows.SalesOrderItemWithNotesAssociation.AssociateObjectsAsync(
                        entity: salesItem,
                        model: new SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>
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
                    salesItem.Discounts ??= new List<AppliedSalesOrderItemDiscount>();
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
                    salesItem.Targets ??= new List<SalesOrderItemTarget>();
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
        protected virtual void HandleAttributes(
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

        /// <summary>Handles the payments.</summary>
        /// <param name="checkout">              The checkout.</param>
        /// <param name="user">                  The user.</param>
        /// <param name="processPaymentResponse">The process payment response.</param>
        /// <param name="originalCart">          The original cart.</param>
        /// <param name="masterSalesOrder">      The master sales order.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int} with the create Sales Invoice's identifier.</returns>
        protected virtual async Task<CEFActionResponse<int>> HandlePaymentStorageAsync(
            ICheckoutModel checkout,
            IUserModel? user,
            CEFActionResponse<IProcessPaymentResponse> processPaymentResponse,
            ISalesCollectionBaseModel originalCart,
            ISalesOrder masterSalesOrder,
            string? contextProfileName)
        {
            // Process Payoneer for the order if set and data was properly provided (Payoneer process trumps
            // invoice workflow as it would largely be duplicating efforts for customers)
            var account = Contract.CheckValidID(user?.AccountID)
                ? await Workflows.Accounts.GetAsync(user!.AccountID!.Value, contextProfileName).ConfigureAwait(false)
                : null;
            if (checkout.PaymentStyle == Enums.PaymentMethodsStrings.Payoneer)
            {
                var processPayoneerResult = await ProcessPayoneerForOrderAsync(
                        storeID: null,
                        buyer: user,
                        order: masterSalesOrder,
                        cart: originalCart,
                        amount: masterSalesOrder.Total,
                        paymentStyle: checkout.PaymentStyle,
                        overridePayoneerAccountID: checkout.PayByPayoneer?.PayoneerAccountID,
                        overridePayoneerCustomerID: checkout.PayByPayoneer?.PayoneerCustomerID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!processPayoneerResult.ActionSucceeded)
                {
                    processPayoneerResult.Messages.Add("ERROR! Unable to complete Payoneer Escrow order creation");
                    return processPayoneerResult.ChangeFailingCEFARType<int>();
                }
                masterSalesOrder.BalanceDue = masterSalesOrder.Total;
                masterSalesOrder.BrandID = checkout.ReferringBrandID;
                await HandleOrderStatusAsync(
                        id: masterSalesOrder.ID,
                        balanceDue: masterSalesOrder.Total,
                        accountID: account?.ID,
                        accountIsOnHold: account?.IsOnHold,
                        accountCredit: account?.Credit,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return 0.WrapInPassingCEFAR();
            }
            masterSalesOrder.BrandID = checkout.ReferringBrandID;
            var invoiceActionsProvider = RegistryLoaderWrapper.GetSalesInvoiceActionsProvider(contextProfileName);
            if (invoiceActionsProvider is null)
            {
                return 0.WrapInPassingCEFAR();
            }
            var createSalesInvoiceResult = await invoiceActionsProvider.CreateSalesInvoiceFromSalesOrderAsync(
                salesOrder: masterSalesOrder.CreateSalesOrderModelFromEntityFull(contextProfileName)!,
                contextProfileName: contextProfileName)
            .ConfigureAwait(false);
            if (!createSalesInvoiceResult.ActionSucceeded
                || !Contract.CheckValidID(createSalesInvoiceResult.Result!.ID))
            {
                return createSalesInvoiceResult.ChangeFailingCEFARType<int>();
            }
            var amount = 0m;
            if (checkout.PaymentStyle == Enums.PaymentMethodsStrings.Invoice)
            {
                await invoiceActionsProvider.AutoPaySalesInvoiceAsync(
                        user?.ID,
                        createSalesInvoiceResult.Result?.ID,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            else
            {
                // Add a Payment
                amount = checkout.IsPartialPayment ? checkout.Amount ?? 0m : originalCart.Totals!.Total;
                var paymentModel = checkout.CreatePaymentModelFromCheckoutModel();
                paymentModel.Amount = amount;
                paymentModel.Token = processPaymentResponse.Result!.Token;
                paymentModel.TransactionNumber = processPaymentResponse.Result.TransactionID;
                paymentModel.AuthCode = processPaymentResponse.Result.PaymentResponse.AuthorizationCode;
                paymentModel.Response = processPaymentResponse.Result.PaymentResponse.ResponseCode;
                paymentModel.TypeKey = checkout.PayByCreditCard?.CardType ?? "Other";
                paymentModel.StatusKey = "Payment Received";
                var addPaymentResult = await invoiceActionsProvider.AddPaymentAsync(
                    salesInvoice: createSalesInvoiceResult.Result,
                    payment: paymentModel,
                    originalPayment: amount,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
                if (!addPaymentResult.ActionSucceeded)
                {
                    return addPaymentResult.ChangeFailingCEFARType<int>();
                }
            }
            masterSalesOrder.BalanceDue = masterSalesOrder.Total - amount;
            await UpdateBalanceDueAsync(
                    id: masterSalesOrder.ID,
                    balanceDue: masterSalesOrder.BalanceDue.Value,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await HandleOrderStatusAsync(
                    id: masterSalesOrder.ID,
                    balanceDue: masterSalesOrder.BalanceDue.Value,
                    accountID: account?.ID,
                    accountIsOnHold: account?.IsOnHold,
                    accountCredit: account?.Credit,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return createSalesInvoiceResult.Result!.ID.WrapInPassingCEFAR();
        }

        /// <summary>Handles the payments.</summary>
        /// <param name="checkout">              The checkout.</param>
        /// <param name="user">                  The user.</param>
        /// <param name="processPaymentResponse">The process payment response.</param>
        /// <param name="originalCart">          The original cart.</param>
        /// <param name="masterSalesOrder">      The master sales order.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int} with the create Sales Invoice's identifier.</returns>
        protected virtual async Task<CEFActionResponse<int>?> HandlePaymentStorageNoInvoiceAsync(
            ICheckoutModel checkout,
            IUserModel? user,
            CEFActionResponse<IProcessPaymentResponse> processPaymentResponse,
            ISalesCollectionBaseModel originalCart,
            ISalesOrder masterSalesOrder,
            string? contextProfileName)
        {
            // Process Payoneer for the order if set and data was properly provided (Payoneer process trumps
            // invoice workflow as it would largely be duplicating efforts for customers)
            var account = Contract.CheckValidID(user?.AccountID)
                ? await Workflows.Accounts.GetAsync(user!.AccountID!.Value, contextProfileName).ConfigureAwait(false)
                : null;
            if (checkout.PaymentStyle == Enums.PaymentMethodsStrings.Payoneer)
            {
                var processPayoneerResult = await ProcessPayoneerForOrderAsync(
                        storeID: null,
                        buyer: user,
                        order: masterSalesOrder,
                        cart: originalCart,
                        amount: masterSalesOrder.Total,
                        paymentStyle: checkout.PaymentStyle,
                        overridePayoneerAccountID: checkout.PayByPayoneer?.PayoneerAccountID,
                        overridePayoneerCustomerID: checkout.PayByPayoneer?.PayoneerCustomerID,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                if (!processPayoneerResult.ActionSucceeded)
                {
                    processPayoneerResult.Messages.Add("ERROR! Unable to complete Payoneer Escrow order creation");
                    return processPayoneerResult.ChangeFailingCEFARType<int>();
                }
                masterSalesOrder.BalanceDue = masterSalesOrder.Total;
                masterSalesOrder.BrandID = checkout.ReferringBrandID;
                await HandleOrderStatusAsync(
                        id: masterSalesOrder.ID,
                        balanceDue: masterSalesOrder.Total,
                        accountID: account?.ID,
                        accountIsOnHold: account?.IsOnHold,
                        accountCredit: account?.Credit,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                return 0.WrapInPassingCEFAR();
            }
            masterSalesOrder.BrandID = checkout.ReferringBrandID;
            var amount = 0m;
            if (checkout.PaymentStyle != Enums.PaymentMethodsStrings.Invoice)
            {
                // Add a Payment
                amount = checkout.IsPartialPayment ? checkout.Amount ?? 0m : originalCart.Totals!.Total;
                var paymentModel = checkout.CreatePaymentModelFromCheckoutModel();
                paymentModel.Amount = amount;
                paymentModel.Token = processPaymentResponse.Result!.Token;
                paymentModel.TransactionNumber = processPaymentResponse.Result.TransactionID;
                paymentModel.AuthCode = processPaymentResponse.Result.PaymentResponse.AuthorizationCode;
                paymentModel.Response = processPaymentResponse.Result.PaymentResponse.ResponseCode;
                paymentModel.TypeKey = checkout.PayByCreditCard?.CardType ?? "Other";
                paymentModel.StatusKey = "Payment Received";
                var addPaymentResult = await AddPaymentAsync(
                    payment: paymentModel,
                    originalPayment: amount,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
                if (!addPaymentResult.ActionSucceeded)
                {
                    return addPaymentResult.ChangeFailingCEFARType<int>();
                }
            }
            masterSalesOrder.BalanceDue = masterSalesOrder.Total - amount;
            await UpdateBalanceDueAsync(
                    id: masterSalesOrder.ID,
                    balanceDue: masterSalesOrder.BalanceDue.Value,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            await HandleOrderStatusAsync(
                    id: masterSalesOrder.ID,
                    balanceDue: masterSalesOrder.BalanceDue.Value,
                    accountID: account?.ID,
                    accountIsOnHold: account?.IsOnHold,
                    accountCredit: account?.Credit,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR<int>(0, new string[] { });
        }

        private async Task<CEFActionResponse<string>> AddPaymentAsync(
            IPaymentModel payment,
            decimal? originalPayment,
            string? contextProfileName)
        {
            if (Contract.CheckInvalidID(payment.ID))
            {
                payment.ID = (await Workflows.Payments.CreateAsync(payment, contextProfileName).ConfigureAwait(false)).Result;
            }
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            return payment.TransactionNumber.WrapInPassingCEFAR()!;
        }

        /// <summary>Handles the order status.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="balanceDue">        The balance due.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="accountIsOnHold">   The account is on hold.</param>
        /// <param name="accountCredit">     The account credit.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected async Task HandleOrderStatusAsync(
            int id,
            decimal balanceDue,
            int? accountID,
            bool? accountIsOnHold,
            decimal? accountCredit,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var master = await context.SalesOrders.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(accountID))
            {
                master.StatusID = OrderStatusPendingID;
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                return;
            }
            var useOnHold = accountIsOnHold ?? false;
            if (!useOnHold && accountCredit > 0 && master.BalanceDue > 0)
            {
                useOnHold = accountCredit.Value - master.BalanceDue.Value <= 0;
            }
            master.StatusID = balanceDue > 0
                ? useOnHold ? OrderStatusOnHoldID : OrderStatusPendingID
                : OrderStatusPaidID;
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
        }

        /// <summary>Generates a master order.</summary>
        /// <param name="checkout">            The checkout.</param>
        /// <param name="originalCart">        The original cart.</param>
        /// <param name="user">                The user.</param>
        /// <param name="paymentTransactionID">Identifier for the payment transaction.</param>
        /// <param name="originalCurrencyKey"> The original currency key.</param>
        /// <param name="sellingCurrencyKey">  The selling currency key.</param>
        /// <param name="timestamp">           The timestamp Date/Time.</param>
        /// <param name="selectedAccountID">   The identifier of the selected account.</param>
        /// <param name="contextProfileName">  Name of the context profile.</param>
        /// <returns>The master order.</returns>
        protected virtual async Task<CEFActionResponse<ISalesOrder?>> GenerateMasterOrderAsync(
            ICheckoutModel checkout,
            ICartModel originalCart,
            IUserModel? user,
            string? paymentTransactionID,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            DateTime timestamp,
            int? selectedAccountID,
            string? contextProfileName)
        {
            var master = RegistryLoaderWrapper.GetInstance<ISalesOrder>(contextProfileName);
            master.Active = true;
            master.CreatedDate = timestamp;
            master.JsonAttributes = "{}";
            if (user != null)
            {
                master.UserID = user.ID;
                master.AccountID = selectedAccountID ?? user.AccountID;
            }
            master.SalesItems = await CreateMasterSalesItemsFromOriginalCartAsync(
                    originalCart: originalCart,
                    timestamp: timestamp,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            master.SubtotalItems = originalCart.Totals!.SubTotal;
            master.SubtotalFees = GetModifiedValue(
                originalCart.Totals.Fees,
                originalCart.SubtotalFeesModifier,
                originalCart.SubtotalFeesModifierMode);
            master.SubtotalShipping = GetModifiedValue(
                originalCart.Totals.Shipping,
                originalCart.SubtotalShippingModifier,
                originalCart.SubtotalShippingModifierMode);
            master.SubtotalHandling = GetModifiedValue(
                originalCart.Totals.Handling,
                originalCart.SubtotalHandlingModifier,
                originalCart.SubtotalHandlingModifierMode);
            master.SubtotalTaxes = GetModifiedValue(
                originalCart.Totals.Tax,
                originalCart.SubtotalTaxesModifier,
                originalCart.SubtotalTaxesModifierMode);
            master.SubtotalDiscounts = GetModifiedValue(
                originalCart.Totals.Discounts,
                originalCart.SubtotalDiscountsModifier,
                originalCart.SubtotalDiscountsModifierMode);
            master.Total = originalCart.Totals.Total;
            master.Total = master.SubtotalItems
                + master.SubtotalFees
                + master.SubtotalShipping
                + master.SubtotalHandling
                + master.SubtotalTaxes
                + Math.Abs(master.SubtotalDiscounts) * -1;
            master.PurchaseOrderNumber = checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber;
            master.PaymentTransactionID = paymentTransactionID;
            TransferContactsList(originalCart, master); // Transfer contacts from storeCart to entity
            _ = await RelateStateStatusTypeAndAttributesUsingDummyAsync(
                    attributes: originalCart.SerializableAttributes!,
                    entity: master,
                    timestamp: timestamp,
                    statusKey: "Pending",
                    typeKey: Contract.RequiresValidKey(CEFConfigDictionary.CheckoutSalesOrderDefaultTypeKey),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            HandleAttributes(
                checkout,
                master,
                originalCurrencyKey,
                sellingCurrencyKey);
            if (checkout.FileNames?.Count > 0)
            {
                var files = master.StoredFiles ?? new List<SalesOrderFile>();
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
                master.Discounts ??= new List<AppliedSalesOrderDiscount>();
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
                context.SalesOrders.Add((SalesOrder)master);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesOrder?>(
                        "ERROR! Something about creating and saving the master sales order for the group failed.");
                }
            }
            return master.WrapInPassingCEFAR()!;
        }

        /// <summary>Handles the emails.</summary>
        /// <param name="checkout">          The checkout.</param>
        /// <param name="splits">            The split orders.</param>
        /// <param name="result">            The result.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task HandleEmailsAsync(
            ICheckoutModel checkout,
            IEnumerable<ISalesOrderModel> splits,
            ICheckoutResult result,
            string? contextProfileName)
        {
            result.PaymentTransactionIDs ??= new[] { result.PaymentTransactionID };
            foreach (var split in splits)
            {
                if (result.PaymentTransactionIDs != null)
                {
                    split.PaymentTransactionID = result.PaymentTransactionIDs
                        .DefaultIfEmpty(string.Empty)
                        .Aggregate((c, n) => c + ", " + n);
                }
                split.PurchaseOrderNumber = checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber;
                if (split.SalesItems?.Any() != true)
                {
                    // Re-map this data onto the order
                    using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                    var items = context.SalesOrderItems
                        .FilterByActive(true)
                        .FilterSalesItemsByMasterID<SalesOrderItem, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(split.ID)
                        .SelectFullSalesOrderItemAndMapToSalesItemBaseModel(contextProfileName)
                        .ToList();
                    if (items.Count == 0)
                    {
                        result.WarningMessages.Add($"There was an error sending the emails for order ID {split.ID}");
                        result.WarningMessages.Add("No items for this order");
                        continue;
                    }
                    split.SalesItems = items;
                }
                try
                {
                    await new SalesOrdersSubmittedNormalToCustomerEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesOrder"] = split, })
                        .ConfigureAwait(false);
                }
                catch (Exception ex1)
                {
                    result.WarningMessages.Add(
                        $"There was an error sending the customer order confirmation for order ID {split.ID}");
                    result.WarningMessages.Add(ex1.Message);
                }
                try
                {
                    await new SalesOrdersSubmittedNormalToBackOfficeEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesOrder"] = split, })
                        .ConfigureAwait(false);
                }
                catch (Exception ex2)
                {
                    Logger.LogError(
                        "Targets Checkout Send Back-office Email", ex2.Message, ex2, null, contextProfileName);
                }
                try
                {
                    await new SalesOrdersSubmittedNormalToBackOfficeStoreEmail().QueueAsync(
                            contextProfileName: contextProfileName,
                            to: null,
                            parameters: new() { ["salesOrder"] = split, })
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
        /// <param name="checkout">              The checkout.</param>
        /// <param name="originalCart">          The original cart.</param>
        /// <param name="targetCarts">           Target carts.</param>
        /// <param name="user">                  The user.</param>
        /// <param name="pricingFactoryContext"> The pricing factory context.</param>
        /// <param name="taxesProvider">         The tax provider.</param>
        /// <param name="processPaymentResponse">The process payment response.</param>
        /// <param name="originalCurrencyKey">   The original currency key.</param>
        /// <param name="sellingCurrencyKey">    The selling currency key.</param>
        /// <param name="selectedAccountID">     The selected account ID.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ISalesGroupModel}.</returns>
        protected async Task<CEFActionResponse<ISalesGroupModel>> BuildSalesGroupFromTargetCartsAsync(
            ICheckoutModel checkout,
            ICartModel originalCart,
            IEnumerable<ICartModel?> targetCarts,
            IUserModel? user,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            CEFActionResponse<IProcessPaymentResponse> processPaymentResponse,
            string originalCurrencyKey,
            string sellingCurrencyKey,
            int? selectedAccountID,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            var salesGroup = RegistryLoaderWrapper.GetInstance<ISalesGroup>(contextProfileName);
            salesGroup.Active = true;
            salesGroup.CreatedDate = timestamp;
            salesGroup.JsonAttributes = "{}";
            salesGroup.BrandID = originalCart.BrandID;
            // Sales Group Account
            if (user != null)
            {
                salesGroup.AccountID = selectedAccountID ?? user.AccountID;
            }
            // Sales Group main Billing Contact
            //if (originalCart.BillingContact != null)
            //{
            //    WipeIDsFromContact(originalCart.BillingContact);
            //    salesGroup.BillingContact = (await Workflows.Contacts.CreateEntityWithoutSavingAsync(
            //                model: originalCart.BillingContact,
            //                timestamp: timestamp,
            //                contextProfileName: contextProfileName)
            //            .ConfigureAwait(false))
            //        .Result;
            //    salesGroup.BillingContact = (Contact)originalCart.BillingContact!
            //        .CreateContactEntity(originalCart.BillingContact.CreatedDate, contextProfileName);
            //}
            // Sales Group Master Sales Order (no target data, just the items all together)
            var masterResult = await GenerateMasterOrderAsync(
                    checkout: checkout,
                    originalCart: originalCart,
                    user: user,
                    paymentTransactionID: processPaymentResponse.Result?.TransactionID,
                    originalCurrencyKey: originalCurrencyKey,
                    sellingCurrencyKey: sellingCurrencyKey,
                    timestamp: timestamp,
                    selectedAccountID: selectedAccountID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!masterResult.ActionSucceeded)
            {
                return masterResult.ChangeFailingCEFARType<ISalesGroupModel>();
            }
            if (Contract.CheckAnyValidKey(checkout.SpecialInstructions))
            {
                salesGroup.Notes = new List<Note>()
                {
                    new Note
                    {
                        CustomKey = $"Shipping Instructions",
                        CreatedDate = DateExtensions.GenDateTime,
                        Note1 = checkout.SpecialInstructions,
                        Active = true,
                        CreatedByUserID = user?.ID,
                        TypeID = 1,
                    },
                };
            }
            if (taxesProvider != null)
            {
                // Commit Taxes
                var taxes = await taxesProvider.CommitCartAsync(
                        cart: originalCart,
                        userID: user?.ID,
                        contextProfileName: contextProfileName,
                        purchaseOrderNumber: checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber,
                        vatId: checkout.WithTaxes?.VatID)
                    .ConfigureAwait(false);
                masterResult.Result!.TaxTransactionID = taxes == null
                    ? string.Empty
                    : $"DOC-{DateTime.Today:yyMMdd}-{originalCart.ID}";
            }
            // Save the Payment that was completed, may create an invoice
            CEFActionResponse<int>? handlePaymentStorageResult = null;
            handlePaymentStorageResult = await HandlePaymentStorageNoInvoiceAsync(
                checkout: checkout,
                user: user,
                processPaymentResponse: processPaymentResponse,
                originalCart: originalCart,
                masterSalesOrder: masterResult.Result!,
                contextProfileName: contextProfileName)
            .ConfigureAwait(false);
            if (!handlePaymentStorageResult!.ActionSucceeded)
            {
                return handlePaymentStorageResult.ChangeFailingCEFARType<ISalesGroupModel>();
            }
            // Save the group
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            {
                context.SalesGroups.Add((SalesGroup)salesGroup);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesGroupModel>(
                        "ERROR! Something about creating and saving the sales group failed.");
                }
                var master = await context.SalesOrders.FilterByID(masterResult.Result!.ID).SingleAsync().ConfigureAwait(false);
                master.SalesGroupAsMasterID = salesGroup.ID;
                if (master.StatusID != masterResult.Result.StatusID)
                {
                    // Save On Hold status if it was set
                    master.StatusID = masterResult.Result.StatusID;
                }
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesGroupModel>(
                        "ERROR! Something about assigning the sales group to the master order failed.");
                }
            }
            // Now create the sub-orders
            foreach (var targetCart in targetCarts)
            {
                if (targetCart == null)
                {
                    continue;
                }
                var targetOrder = RegistryLoaderWrapper.GetInstance<SalesOrder>(contextProfileName);
                targetOrder.Active = true;
                targetOrder.CreatedDate = timestamp;
                if (user != null)
                {
                    targetOrder.UserID = user.ID;
                    targetOrder.AccountID = selectedAccountID ?? user.AccountID;
                }
                if (!targetCart.NothingToShip && targetCart.ShippingContact == null)
                {
                    throw new InvalidOperationException("Target carts that are shippable must have a shipping contact");
                }
                using (var contextCheck = RegistryLoaderWrapper.GetContext(contextProfileName))
                {
                    var master = await contextCheck.SalesOrders
                        .AsNoTracking()
                        .FilterByID(masterResult.Result.ID)
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
                targetOrder.SalesItems = await CreateTargetOrderSalesItemsFromTargetCartAsync(
                        targetCart: targetCart,
                        masterOrder: masterResult.Result,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                targetOrder.SubtotalItems = targetOrder.SalesItems
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
                targetOrder.SubtotalShipping = rate?.Rate ?? 0m;
                // TODO: Recalculate handling charges for products and their categories per sub-order
                targetOrder.SubtotalHandling = 0m;
                // TODO: Recalculate fees per sub-order
                targetOrder.SubtotalFees = 0m;
                // TODO@CG: Recalculate taxes per sub-order
                targetOrder.SubtotalTaxes = 0m;
                var discountsTotal = targetOrder.Discounts!.Select(x => x.DiscountTotal).DefaultIfEmpty(0m).Sum();
                discountsTotal += targetOrder.SalesItems.SelectMany(x => x.Discounts!.Select(y => y.DiscountTotal)).DefaultIfEmpty(0m).Sum();
                targetOrder.SubtotalDiscounts = discountsTotal;
                targetOrder.Total = targetOrder.SubtotalItems
                    + targetOrder.SubtotalShipping
                    + targetOrder.SubtotalTaxes
                    + targetOrder.SubtotalFees
                    + targetOrder.SubtotalHandling
                    + Math.Abs(targetOrder.SubtotalDiscounts) * -1m;
                targetOrder.PurchaseOrderNumber = checkout.PayByBillMeLater?.CustomerPurchaseOrderNumber;
                targetOrder.SalesGroupAsSubID = salesGroup.ID;
                //if (!targetCart.NothingToShip)
                //{
                //    var shippingContact = await Workflows.Contacts.CreateEntityWithoutSavingAsync(
                //            model: WipeIDsFromContact(targetCart.ShippingContact),
                //            timestamp: null,
                //            contextProfileName: contextProfileName)
                //        .ConfigureAwait(false);
                //    if (shippingContact.ActionSucceeded)
                //    {
                //        shippingContact.Result!.TypeID = ShippingTypeID;
                //        targetOrder.ShippingContact = shippingContact.Result;
                //    }
                //    targetOrder.ShippingContact = (Contact)targetCart.ShippingContact!
                //        .CreateContactEntity(targetOrder.ShippingContact!.CreatedDate, contextProfileName);
                //}
                if (checkout.FileNames?.Count > 0)
                {
                    var salesFiles = targetOrder.StoredFiles ?? new List<SalesOrderFile>();
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
                    targetOrder.StoredFiles = salesFiles;
                }
                targetOrder.BrandID = checkout.ReferringBrandID;
                var dummy = await RelateStateStatusTypeAndAttributesUsingDummyForSubOrdersAsync(
                        cartAttributes: originalCart.SerializableAttributes,
                        checkoutAttributes: checkout.SerializableAttributes,
                        entity: targetOrder,
                        statusKey: masterResult.Result.StatusID == OrderStatusOnHoldID ? "On Hold" : null,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Copy the notes from the Cart
                await CopyNotesAsync(
                        checkout: checkout,
                        pricingFactoryContext: pricingFactoryContext,
                        entity: targetOrder,
                        dummy: dummy,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                context.SalesOrders.Add(targetOrder);
                if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR<ISalesGroupModel>(
                        "ERROR! Something about creating and saving the sub-sales order for the sales group failed.");
                }
                if (!targetCart.NothingToShip)
                {
                    targetOrder = await context.SalesOrders
                        .FilterByID(targetOrder.ID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false);
                    targetOrder.ShippingContactID = targetCart.ShippingContactID;
                    context.SalesOrders.Add(targetOrder);
                    context.Entry(targetOrder).State = EntityState.Modified;
                    context.Entry(targetOrder).Property(x => x.ShippingContactID).IsModified = true;
                    if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
                    {
                        return CEFAR.FailingCEFAR<ISalesGroupModel>(
                            "ERROR! Something about assigning the shipping contact to the sub-sales order for the sales group failed.");
                    }
                }
                if (rate == null)
                {
                    continue;
                }
                rate.SalesOrderID = targetOrder.ID;
                rate.CartID = null;
                await Workflows.RateQuotes.UpdateAsync(rate, contextProfileName).ConfigureAwait(false);
            }
            // TODO: Move the original cart to History and Converted to Sales Order
            // Return the group
            var group = await Workflows.SalesGroups.GetAsync(salesGroup.ID, contextProfileName).ConfigureAwait(false);
            using (var rateContext = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var subOrders = (await context.SalesOrders
                        .Include(rq => rq.RateQuotes)
                        .Include(n => n.Notes)
                        .Include(a => a.Account)
                        .FilterByIDs(group!.SubSalesOrders.Select(x => x.ID))
                        .ToListAsync()
                    .ConfigureAwait(false))
                    .Select(y => y.CreateSalesOrderModelFromEntityFull(contextProfileName)!)
                    .ToList();
                group.SubSalesOrders = subOrders;
                foreach (var sub in group!.SubSalesOrders!)
                {
                    var selectedRate = (await context.RateQuotes
                            .FilterRateQuotesBySalesOrderID(sub.ID, false)
                            .FilterByActive(true)
                            .FilterRateQuotesBySelected(true)
                            .ToListAsync()
                        .ConfigureAwait(false))
                        .Select(y => y.CreateRateQuoteModelFromEntityFull(contextProfileName)!)
                        .ToList();
                    sub.RateQuotes = selectedRate;
                }
            }
            return group.WrapInPassingCEFAR()!;
        }

        /// <summary>Updates the balance due.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="balanceDue">        The balance due.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task UpdateBalanceDueAsync(
            int id,
            decimal balanceDue,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var order = await context.SalesOrders.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (order.BalanceDue == balanceDue)
            {
                return;
            }
            order.BalanceDue = balanceDue;
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
        }

        /// <summary>Relate state status and type using dummy.</summary>
        /// <param name="cartAttributes">    The cart attributes.</param>
        /// <param name="checkoutAttributes">The checkout attributes.</param>
        /// <param name="entity">            The entity.</param>
        /// <param name="statusKey">         The status key.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task<ISalesOrderModel> RelateStateStatusTypeAndAttributesUsingDummyForSubOrdersAsync(
            SerializableAttributesDictionary? cartAttributes,
            SerializableAttributesDictionary? checkoutAttributes,
            ISalesOrder entity,
            string? statusKey,
            DateTime timestamp,
            string? contextProfileName)
        {
            var dummy = RegistryLoaderWrapper.GetInstance<ISalesOrderModel>(contextProfileName);
            var state = RegistryLoaderWrapper.GetInstance<IStateModel>(contextProfileName);
            state.Name = state.DisplayName = "Work";
            state.CustomKey = "WORK";
            var status = RegistryLoaderWrapper.GetInstance<IStatusModel>(contextProfileName);
            status.CustomKey = status.Name = status.DisplayName = statusKey ?? "Pending";
            var type = RegistryLoaderWrapper.GetInstance<ITypeModel>(contextProfileName);
            type.CustomKey = type.Name = type.DisplayName = "Sales Order Child";
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

        /// <summary>Creates target order sales items from target cart.</summary>
        /// <param name="targetCart">        Target cart.</param>
        /// <param name="masterOrder">       The master order.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new target order sales items from target cart.</returns>
        private static async Task<List<SalesOrderItem>> CreateTargetOrderSalesItemsFromTargetCartAsync(
            ICartModel targetCart,
            ISalesOrder masterOrder,
            DateTime timestamp,
            string? contextProfileName)
        {
            // Things the target carts do that the master doesn't:
            // * Read in Targets info
            // * Update Stock
            // * Determine how much needs to be back ordered
            var resultItems = new List<SalesOrderItem>();
            foreach (var cartItem in targetCart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                var salesItem = new SalesOrderItem
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
                    UnitSoldPrice = GetModifiedValue(
                        cartItem.SerializableAttributes.TryGetValue("SoldPrice", out var raw) && decimal.TryParse(raw.Value, out var soldPrice)
                            ? soldPrice
                            : cartItem.UnitSoldPrice ?? cartItem.UnitCorePrice,
                        cartItem.UnitSoldPriceModifier,
                        cartItem.UnitSoldPriceModifierMode),
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
                await Workflows.SalesOrderItemWithNotesAssociation.AssociateObjectsAsync(
                        entity: salesItem,
                        model: new SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>
                        {
                            Notes = cartItem.Notes?.Cast<NoteModel>().ToList(),
                        },
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Targets
                await Workflows.SalesOrderItemWithTargetsAssociation.AssociateObjectsAsync(
                        entity: salesItem,
                        model: new SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>
                        {
                            Targets = cartItem.Targets?.Cast<SalesItemTargetBaseModel>().ToList(),
                        },
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                // Transfer Discounts
                var masterOrderItem = masterOrder.SalesItems!
                    .SingleOrDefault(x => x.Active
                        && x.ForceUniqueLineItemKey == salesItem.ForceUniqueLineItemKey
                        && x.ProductID == salesItem.ProductID);
                if (masterOrderItem != null && masterOrderItem.Discounts != null && masterOrderItem.Discounts.Any())
                {
                    salesItem.Discounts ??= new HashSet<AppliedSalesOrderItemDiscount>();
                    foreach (var masterOrderItemDiscountModel in masterOrderItem.Discounts)
                    {
                        var salesItemDiscount = new AppliedSalesOrderItemDiscount
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
                // Update Stock allocations TODO@JTG: Shouldn't this happen at the master and not the targets?
                var regionID = Contract.RequiresValidID(targetCart?.ShippingContact?.Address?.RegionID);
                var region = await Workflows.Regions.GetAsync(regionID, contextProfileName).ConfigureAwait(false);
                await UpdateStockInfoForCartItemAsync(
                        cartItem: cartItem,
                        shippingRegionCode: region?.Code,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            return resultItems;
        }

        /// <summary>Verify subscription doesnt exist.</summary>
        /// <param name="item">              The item.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{Subscription?}}.</returns>
        private static async Task<CEFActionResponse<Subscription?>> VerifySubscriptionDoesntExist(
            // ReSharper disable once SuggestBaseTypeForParameter
            ISalesItemBaseModel item,
            int? userID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var existingSubscription = await context.Subscriptions
                .Include(x => x.ProductSubscriptionType)
                .FilterByActive(true)
                .FilterSubscriptionsByUserID(userID)
                .Where(s => s.ProductSubscriptionType!.MasterID == item.ProductID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            var currentTime = DateExtensions.GenDateTime;
            if (existingSubscription == null)
            {
                return CEFAR.PassingCEFAR(existingSubscription);
            }
            var daysAgo = (currentTime - existingSubscription.StartsOn).Days;
            // ReSharper disable once InvertIf
            if (existingSubscription.EndsOn != null)
            {
                var daysUntil = ((DateTime)existingSubscription.EndsOn - currentTime).Days;
                return CEFAR.FailingCEFAR<Subscription?>(
                    $"ERROR! You just purchased this prescription {daysAgo} days ago."
                    + $" You will need to wait another {daysUntil} days before you can request a refill.");
            }
            return CEFAR.FailingCEFAR<Subscription?>(
                $"ERROR! You just purchased this prescription {daysAgo} days ago."
                + " You will need to wait before you can request a refill.");
        }
    }
}
