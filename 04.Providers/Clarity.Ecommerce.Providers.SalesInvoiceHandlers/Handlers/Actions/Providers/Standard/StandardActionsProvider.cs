// <copyright file="StandardActionsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard actions provider class</summary>
#nullable enable
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Actions.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using DataModel;
    using Emails;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Surcharges;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A standard sales invoice actions provider.</summary>
    /// <seealso cref="SalesInvoiceActionsProviderBase"/>
    public class StandardSalesInvoiceActionsProvider : SalesInvoiceActionsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSalesInvoiceActionsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <summary>Gets or sets the invoice state identifier of work.</summary>
        /// <value>The invoice state identifier of work.</value>
        protected static int InvoiceStateIDOfWork { get; set; }

        /// <summary>Gets or sets the invoice state identifier of history.</summary>
        /// <value>The invoice state identifier of history.</value>
        protected static int InvoiceStateIDOfHistory { get; set; }

        /// <summary>Gets or sets the invoice status identifier of unpaid.</summary>
        /// <value>The invoice status identifier of unpaid.</value>
        protected static int InvoiceStatusIDOfUnpaid { get; set; }

        /// <summary>Gets or sets the invoice status identifier of paid.</summary>
        /// <value>The invoice status identifier of paid.</value>
        protected static int InvoiceStatusIDOfPaid { get; set; }

        /// <summary>Gets or sets the invoice status identifier of partially paid.</summary>
        /// <value>The invoice status identifier of partially paid.</value>
        protected static int InvoiceStatusIDOfPartiallyPaid { get; set; }

        /// <summary>Gets or sets the invoice status identifier of void.</summary>
        /// <value>The invoice status identifier of void.</value>
        protected static int InvoiceStatusIDOfVoid { get; set; }

        /// <summary>Gets or sets the invoice type identifier of generated from order.</summary>
        /// <value>The invoice type identifier of generated from order.</value>
        protected static int InvoiceTypeIDOfGeneratedFromOrder { get; set; }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<ISalesInvoiceModel>> CreateSalesInvoiceFromSalesOrderAsync(
            ISalesOrderModel salesOrder,
            string? contextProfileName)
        {
            _ = Contract.RequiresValidID(salesOrder.ID);
            var salesInvoice = RegistryLoaderWrapper.GetInstance<ISalesInvoice>(contextProfileName);
            // Base
            var timestamp = DateExtensions.GenDateTime;
            salesInvoice.Active = true;
            salesInvoice.CreatedDate = timestamp;
            var subIterator = 0;
            var key = $"InvoiceForOrder-{salesOrder.ID}";
            while ((await Workflows.SalesInvoices.CheckExistsAsync(key, contextProfileName).ConfigureAwait(false)).HasValue)
            {
                subIterator++;
                key = $"InvoiceForOrder-{salesOrder.ID}.{subIterator}";
            }
            salesInvoice.CustomKey = key;
            if (Contract.CheckInvalidID(InvoiceStateIDOfWork))
            {
                InvoiceStateIDOfWork = await Workflows.SalesInvoiceStates.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "WORK",
                        byName: "Work",
                        byDisplayName: "Work",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(InvoiceStatusIDOfUnpaid))
            {
                InvoiceStatusIDOfUnpaid = await Workflows.SalesInvoiceStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Unpaid",
                        byName: "Unpaid",
                        byDisplayName: "Unpaid",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(InvoiceTypeIDOfGeneratedFromOrder))
            {
                InvoiceTypeIDOfGeneratedFromOrder = await Workflows.SalesInvoiceTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Generated from Order",
                        byName: "Generated from Order",
                        byDisplayName: "Generated from Order",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            salesInvoice.DueDate = CEFConfigDictionary.SalesInvoicesAllowDueDate
                ? DateTime.Now.AddDays(CEFConfigDictionary.SalesInvoicesDueDateDefault)
                : null;
            salesInvoice.TypeID = InvoiceTypeIDOfGeneratedFromOrder;
            salesInvoice.StatusID = InvoiceStatusIDOfUnpaid;
            salesInvoice.StateID = InvoiceStateIDOfWork;
            // IAmFilterableByNullableBrand
            salesInvoice.BrandID = salesOrder.BrandID;
            // IAmFilterableByNullableStore
            salesInvoice.StoreID = salesOrder.StoreID;
            // ISalesCollectionBase
            salesInvoice.Total = salesOrder.Totals!.Total;
            salesInvoice.SubtotalDiscounts = salesOrder.Totals.Discounts;
            salesInvoice.SubtotalFees = salesOrder.Totals.Fees;
            salesInvoice.SubtotalHandling = salesOrder.Totals.Handling;
            salesInvoice.SubtotalItems = salesOrder.Totals.SubTotal;
            salesInvoice.SubtotalShipping = salesOrder.Totals.Shipping;
            salesInvoice.SubtotalTaxes = salesOrder.Totals.Tax;
            salesInvoice.BalanceDue = salesOrder.Totals.Total;
            salesInvoice.ShippingSameAsBilling = false;
            salesInvoice.BillingContactID = salesOrder.BillingContactID;
            salesInvoice.ShippingContactID = salesOrder.ShippingSameAsBilling ?? false
                ? salesOrder.BillingContactID
                : salesOrder.ShippingContactID;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var itemIDs = salesOrder.SalesItems!.Select(x => x.ID).ToArray();
            var itemDiscounts = context.AppliedSalesOrderItemDiscounts
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => itemIDs.Contains(x.MasterID))
                .SelectListAppliedSalesOrderItemDiscountAndMapToAppliedSalesOrderItemDiscountModel(contextProfileName)
                .GroupBy(x => x.MasterID)
                .ToDictionary(x => x.Key, x => x.ToList());
            salesInvoice.SalesItems = salesOrder.SalesItems!
                .Where(x => x.Active && x.TotalQuantity > 0m)
                .Select(x => new SalesInvoiceItem
                {
                    // Base Properties
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = x.CustomKey,
                    JsonAttributes = x.SerializableAttributes.SerializeAttributesDictionary(),
                    // NameableBase Properties
                    Name = x.Name,
                    Description = x.Description,
                    // SalesItemBase Properties
                    ForceUniqueLineItemKey = x.ForceUniqueLineItemKey,
                    Quantity = x.Quantity,
                    QuantityBackOrdered = x.QuantityBackOrdered,
                    QuantityPreSold = x.QuantityPreSold,
                    Sku = x.Sku,
                    UnitOfMeasure = x.UnitOfMeasure,
                    UnitCorePrice = x.UnitCorePrice,
                    UnitSoldPrice = x.UnitSoldPrice,
                    UnitCorePriceInSellingCurrency = x.UnitCorePriceInSellingCurrency,
                    UnitSoldPriceInSellingCurrency = x.UnitSoldPriceInSellingCurrency,
                    // Related Objects
                    ProductID = x.ProductID,
                    OriginalCurrencyID = x.OriginalCurrencyID,
                    SellingCurrencyID = x.SellingCurrencyID,
                    UserID = x.UserID,
                    // Associated Objects
                    Discounts = (x.Discounts ?? (itemDiscounts.ContainsKey(x.ID) ? itemDiscounts[x.ID] : null))
                        ?.Select(y => new AppliedSalesInvoiceItemDiscount
                        {
                            // Base Properties
                            Active = true,
                            CreatedDate = timestamp,
                            CustomKey = y.CustomKey,
                            JsonAttributes = y.SerializableAttributes.SerializeAttributesDictionary(),
                            // AppliedDiscountBase Properties
                            ApplicationsUsed = y.ApplicationsUsed,
                            TargetApplicationsUsed = y.TargetApplicationsUsed,
                            DiscountTotal = y.DiscountTotal,
                            // Related Objects
                            SlaveID = y.SlaveID,
                        })
                        .ToList(),
                })
                .ToList();
            var invoiceDiscounts = context.AppliedSalesOrderDiscounts
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => salesOrder.ID == x.MasterID)
                .SelectListAppliedSalesOrderDiscountAndMapToAppliedSalesOrderDiscountModel(contextProfileName)
                .GroupBy(x => x.MasterID)
                .ToDictionary(x => x.Key, x => x.ToList());
            salesInvoice.Discounts = (invoiceDiscounts.ContainsKey(salesOrder.ID) ? invoiceDiscounts[salesOrder.ID] : null)
                ?.Select(x => new AppliedSalesInvoiceDiscount
                {
                    //Base Properties
                    CustomKey = x.CustomKey,
                    Active = x.Active,
                    CreatedDate = x.CreatedDate,
                    UpdatedDate = x.UpdatedDate,
                    JsonAttributes = x.SerializableAttributes.SerializeAttributesDictionary(),
                    //Applied Discount Properties
                    SlaveID = x.DiscountID,
                    DiscountTotal = x.DiscountTotal,
                    ApplicationsUsed = x.ApplicationsUsed,
                    TargetApplicationsUsed = 0,
                })
                .ToList();
            salesInvoice.AccountID = salesOrder.AccountID;
            salesInvoice.UserID = salesOrder.UserID;
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(
                    entity: salesInvoice,
                    model: salesOrder,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (CEFConfigDictionary.SplitShippingEnabled)
            {
                // Associate to the Group
                salesInvoice.SalesGroupID = salesOrder.SalesGroupAsMasterID ?? salesOrder.SalesGroupAsSubID;
            }
            else
            {
                // Associate to the Order
                salesInvoice.AssociatedSalesOrders!.Add(new()
                {
                    Active = true,
                    CreatedDate = timestamp,
                    CustomKey = salesOrder.CustomKey + "|" + salesInvoice.CustomKey,
                    MasterID = salesOrder.ID,
                    JsonAttributes = "{}",
                });
            }
            // Save
            context.SalesInvoices.Add((SalesInvoice)salesInvoice);
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesInvoiceModel>(
                    "ERROR! Unable to save sales invoice created from sales order");
            }
            // Apply the rate quotes to the invoice
            foreach (var rateQuote in salesOrder.RateQuotes!.Where(x => x.Selected))
            {
                var e = await context.RateQuotes.FilterByID(rateQuote.ID).SingleOrDefaultAsync().ConfigureAwait(false);
                if (e == null)
                {
                    continue;
                }
                e.SalesInvoiceID = salesInvoice.ID;
                e.UpdatedDate = timestamp;
            }
            if (!await context.SaveUnitOfWorkAsync().ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR<ISalesInvoiceModel>(
                    "ERROR! Unable to save sales invoice rate quote assignments from sales order");
            }
            return salesInvoice.CreateSalesInvoiceModelFromEntityFull(contextProfileName)
                .WrapInPassingCEFARIfNotNull();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> AutoPaySalesInvoiceAsync(
            int? userID,
            int? invoiceID,
            string? contextProfileName)
        {
            if (Contract.CheckInvalidID(invoiceID))
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Invalid invoice ID");
            }
            if (Contract.CheckInvalidID(userID))
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Invalid user ID");
            }
            var user = await Workflows.Users.GetAsync(userID!.Value, contextProfileName).ConfigureAwait(false);
            if (user == null)
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Cannot locate specified User");
            }
            if (!user.UseAutoPay)
            {
                return CEFAR.FailingCEFAR<string>("ERROR! User is not setup for auto-pay");
            }
            return await PaySingleByIDAsync(
                    id: invoiceID!.Value,
                    payment: new PaymentModel { },
                    billing: null,
                    contextProfileName,
                    useWalletToken: true,
                    userID: userID)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<ISalesInvoiceModel>> CreateInvoiceForOrderAsync(
            int id,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var model = context.SalesOrders
                .AsNoTracking()
                .FilterByID(id)
                .SelectSingleFullSalesOrderAndMapToSalesOrderModel(contextProfileName)!;
            var createSalesInvoiceResult = await CreateSalesInvoiceFromSalesOrderAsync(
                    salesOrder: model,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (!createSalesInvoiceResult.ActionSucceeded)
            {
                return createSalesInvoiceResult;
            }
            var emailResult = await new SalesOrderInvoiceCreatedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: context.ContextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["salesOrder"] = model,
                        ["salesInvoice"] = createSalesInvoiceResult.Result!,
                    })
                .ConfigureAwait(false);
            if (!emailResult.ActionSucceeded)
            {
                return emailResult.ChangeFailingCEFARType<ISalesInvoiceModel>();
            }
            if (emailResult.Messages.Count > 0)
            {
                createSalesInvoiceResult.Messages.AddRange(emailResult.Messages);
            }
            return createSalesInvoiceResult;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> AddPaymentAsync(
            ISalesInvoiceModel salesInvoice,
            IPaymentModel payment,
            decimal? originalPayment,
            string? contextProfileName)
        {
            _ = Contract.RequiresValidID(salesInvoice.ID); // We can only update invoices that have previously been saved
            var newPaymentLink = RegistryLoaderWrapper.GetInstance<ISalesInvoicePayment>(contextProfileName);
            newPaymentLink.Active = true;
            newPaymentLink.CreatedDate = DateExtensions.GenDateTime;
            newPaymentLink.MasterID = salesInvoice.ID;
            if (Contract.CheckInvalidID(payment.ID))
            {
                payment.ID = (await Workflows.Payments.CreateAsync(payment, contextProfileName).ConfigureAwait(false)).Result;
            }
            newPaymentLink.SlaveID = payment.ID;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.SalesInvoicePayments.Add((SalesInvoicePayment)newPaymentLink);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // We must add this to the model even though we already added it to the DB otherwise it will be marked as
            // inactive when the mapping layer hits it.
            // Property returns a copy of the list - must circumvent normal property access and create a temporary list
            var tmpList = salesInvoice.SalesInvoicePayments ?? new List<ISalesInvoicePaymentModel>();
            tmpList.Add(new SalesInvoicePaymentModel
            {
                Active = true,
                ID = newPaymentLink.ID,
                CustomKey = newPaymentLink.CustomKey,
                SlaveID = payment.ID,
            });
            salesInvoice.SalesInvoicePayments = tmpList;
            salesInvoice.BalanceDue -= originalPayment;
            if (salesInvoice.BalanceDue <= 0)
            {
                salesInvoice.StatusID = 0;
                salesInvoice.StatusKey = null;
                salesInvoice.StatusName = "Paid";
                salesInvoice.Status = null;
                salesInvoice.StateID = 0;
                salesInvoice.StateKey = "HISTORY";
                salesInvoice.StateName = null;
                salesInvoice.State = null;
                await UpdateSalesOrderStatusAsync(salesInvoice.ID, "Full Payment Received", contextProfileName).ConfigureAwait(false);
            }
            else
            {
                salesInvoice.StatusID = 0;
                salesInvoice.StatusKey = null;
                salesInvoice.StatusName = "Unpaid";
                salesInvoice.Status = null;
                await UpdateSalesOrderStatusAsync(salesInvoice.ID, "Partial Payment Received", contextProfileName).ConfigureAwait(false);
            }
            await UpdateSalesOrderBalanceDueAsync(salesInvoice.ID, salesInvoice.BalanceDue, contextProfileName).ConfigureAwait(false);
            // TODO: Replace this call with a more targeted update
            await Workflows.SalesInvoices.UpdateAsync(salesInvoice, contextProfileName).ConfigureAwait(false);
            return payment.TransactionNumber.WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<string>> PaySingleByIDAsync(
            int id,
            IPaymentModel payment,
            IContactModel? billing,
            string? contextProfileName,
            bool useWalletToken = false,
            int? userID = null)
        {
            var salesInvoice = await Workflows.SalesInvoices.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (salesInvoice == null)
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Cannot locate specified Invoice");
            }
            if (salesInvoice.StatusName == "Paid")
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Invoice is already marked as paid");
            }
            var paymentProvider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (paymentProvider == null)
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Unable to locate a payment provider to accept the payment");
            }
            await paymentProvider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
            if (useWalletToken)
            {
                if (salesInvoice.BalanceDue > CEFConfigDictionary.PaymentsByInvoiceCreditCardLimit)
                {
                    return CEFAR.FailingCEFAR<string>("ERROR! Invoice balance due is greater than the Credit Card Limit.");
                }
                if (paymentProvider is not IWalletProviderBase walletProvider)
                {
                    return CEFAR.FailingCEFAR<string>("ERROR! Current payment provider doesn't implement wallet functionalities");
                }
                if (Contract.CheckInvalidID(userID))
                {
                    return CEFAR.FailingCEFAR<string>("ERROR! Invalid user ID");
                }
                var walletList = await Workflows.Wallets
                    .GetWalletForUserAsync(userID!.Value, walletProvider, contextProfileName)
                    .ConfigureAwait(false);
                if (!walletList.ActionSucceeded || walletList.Result == null)
                {
                    return CEFAR.FailingCEFAR<string>(string.Join(".", walletList.Messages));
                }
                var wallet = walletList.Result
                    .OrderByDescending(x => x.UpdatedDate)
                    .FirstOrDefault();
                payment.Amount = salesInvoice.BalanceDue;
                payment.WalletID = wallet?.ID;
                payment.Token = wallet?.Token;
            }
            if (payment.Amount is null or <= 0m)
            {
                return CEFAR.FailingCEFAR<string>("ERROR! No amount was provided");
            }
            var paymentMethod = await DeterminePaymentMethodAsync(payment, contextProfileName).ConfigureAwait(false);
            var originalPayment = payment.Amount;
            if (!Contract.CheckValidKey(paymentMethod))
            {
                return CEFAR.FailingCEFAR<string>("ERROR! Could not determine payment method");
            }
            if (originalPayment > salesInvoice.BalanceDue)
            {
                return CEFAR.FailingCEFAR<string>(
                    $"ERROR! The amount ({payment.Amount:c}) cannot be higher than the remaining balance"
                    + $" on the Invoice ({salesInvoice.BalanceDue:c})");
            }
            billing ??= salesInvoice.BillingContact;
            /*
            if (billing == null)
            {
                var accountID = await Workflows.Accounts.GetIDByUserIDAsync(payment.ID, contextProfileName);
                if (Contract.CheckValidID(accountID))
                {
                    billing = (await Workflows.AddressBooks.GetAddressBookPrimaryBillingAsync(
                            accountID.Value,
                            contextProfileName)
                            .ConfigureAwait(false))
                        ?.Contact;
                }
            }
            */
            ////if (payment.PaymentMethodKey == WireTransferKey
            ////    || payment.PaymentMethodKey == OnlinePaymentRecordKey
            ////    || payment.PaymentMethodKey == CheckByMailKey)
            ////{
            ////    return await AddPaymentToSalesInvoiceAsync(
            ////            salesInvoice,
            ////            payment,
            ////            contextProfileName)
            ////        .ConfigureAwait(false);
            ////}
            if (billing == null)
            {
                return CEFAR.FailingCEFAR<string>(
                    "ERROR! Unable to locate a billing contact to send with the payment information");
            }
            // May add to the payment.Amount
            var (surchargeDescriptor, surchargeAmount) = await CommonSurchargeCalculationLogic(
                    payment,
                    billing,
                    new[] { salesInvoice },
                    contextProfileName)
                .ConfigureAwait(false);
            var (_, newTotal) = CalculateUpliftFee(payment.Amount.Value, paymentMethod!);
            payment.Amount = newTotal; // Assign the new amount
            var paymentResult = await paymentProvider.AuthorizeAndACaptureAsync(
                    payment: payment,
                    billing: billing,
                    shipping: billing,
                    paymentAlreadyConverted: false, // TODO: Adjust this based on multi-currency data
                    contextProfileName: contextProfileName,
                    useWalletToken: useWalletToken)
                .ConfigureAwait(false);
            if (paymentResult?.Approved != true)
            {
                return CEFAR.FailingCEFAR<string>(
                    $"ERROR! Transaction failed: {paymentResult?.ResponseCode ?? "Unknown Reason"}");
            }
            payment.Token = paymentResult.AuthorizationCode;
            payment.TransactionNumber = paymentResult.TransactionID;
            payment.PaymentMethodKey ??= string.IsNullOrEmpty(payment.BankName) ? "Credit Card" : "Wire Transfer";
            var paymentType = payment.TypeKey
                ?? (string.IsNullOrEmpty(payment.CardNumber) ? "General" : FindType(payment.CardNumber!));
            payment.TypeID = 0;
            payment.TypeKey = paymentType;
            payment.StatusID = 0;
            payment.StatusKey = "Captured";
            if (payment.CardNumber?.Length > 4)
            {
                payment.Last4CardDigits = payment.CardNumber[^4..];
            }
            else
            {
                if (payment.AccountNumber?.Length > 4)
                {
                    payment.AccountNumberLast4 = payment.AccountNumber[^4..];
                }
                if (payment.RoutingNumber?.Length > 4)
                {
                    payment.RoutingNumberLast4 = payment.RoutingNumber[^4..];
                }
            }
            if (surchargeDescriptor.HasValue)
            {
                await CommonSurchargeCompletionLogic(
                        surchargeDescriptor.Value,
                        surchargeAmount,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            return await AddPaymentAsync(salesInvoice, payment, originalPayment, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> AssignSalesGroupAsync(
            int salesInvoiceID,
            int salesGroupID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesInvoices.FilterByID(salesInvoiceID).SingleAsync().ConfigureAwait(false);
            if (entity.SalesGroupID == salesGroupID)
            {
                // Already set
                return CEFAR.PassingCEFAR();
            }
            // Set now
            entity.SalesGroupID = salesGroupID;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("Error: Something about saving this change failed");
        }

        /// <inheritdoc/>
        // ReSharper disable once CognitiveComplexity, CyclomaticComplexity
        public override async Task<CEFActionResponse<string>> PayMultipleByAmountsAsync(
            Dictionary<int, decimal> amounts,
            IPaymentModel payment,
            IContactModel? billing,
            int userID,
            string? contextProfileName)
        {
            // Validate
            _ = Contract.RequiresNotNull(amounts);
            _ = Contract.RequiresNotNull(payment);
            if (payment.Amount is <= 0 or decimal.MaxValue)
            {
                return CEFAR.FailingCEFAR<string>(
                    "ERROR! Cannot submit a payment without specifying the total amount");
            }
            var invoices = new Dictionary<int, ISalesInvoiceModel>();
            foreach (var kvp in amounts)
            {
                _ = Contract.RequiresValidID(kvp.Key);
                Contract.Requires<ArgumentOutOfRangeException>(kvp.Value > 0 && kvp.Value != decimal.MaxValue);
                invoices[kvp.Key] = Contract.RequiresNotNull(
                    await Workflows.SalesInvoices.GetAsync(
                            kvp.Key,
                            contextProfileName)
                        .ConfigureAwait(false));
                if (invoices[kvp.Key].BalanceDue == null || invoices[kvp.Key].BalanceDue < kvp.Value)
                {
                    return CEFAR.FailingCEFAR<string>(
                        "ERROR! Cannot submit a payment for more than the balance due on an Invoice");
                }
            }
            // Run the single master payment for the full amount
            var paymentProvider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (paymentProvider == null)
            {
                return CEFAR.FailingCEFAR<string>(
                    "ERROR! Unable to locate a payment provider to accept the payment");
            }
            await paymentProvider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
            billing ??= invoices.First().Value.BillingContact;
            if (billing == null)
            {
                var accountID = await Workflows.Accounts.GetIDByUserIDAsync(
                        userID,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (Contract.CheckValidID(accountID))
                {
                    billing = (await Workflows.AddressBooks.GetAddressBookPrimaryBillingAsync(
                                accountID!.Value,
                                contextProfileName)
                            .ConfigureAwait(false))
                        ?.Contact;
                }
            }
            if (billing == null)
            {
                return CEFAR.FailingCEFAR<string>(
                    "ERROR! Unable to locate a billing contact to send with the payment information");
            }
            var (surchargeDescriptor, surchargeAmount) = await CommonSurchargeCalculationLogic(
                    payment,
                    billing,
                    invoices.Values.ToList(),
                    contextProfileName)
                .ConfigureAwait(false);
            var timestamp = DateExtensions.GenDateTime;
            if (paymentProvider is IWalletProviderBase walletProvider)
            {
                // Store to wallet if new and they sent a reference name
                if (Contract.CheckInvalidID(payment.WalletID)
                    && Contract.CheckValidKey(payment.ReferenceName))
                {
                    var walletResponse = await walletProvider.CreateCustomerProfileAsync(
                            payment,
                            billing,
                            contextProfileName)
                        .ConfigureAwait(false);
                    if (!walletResponse.Approved)
                    {
                        return CEFAR.FailingCEFAR<string>(
                            "ERROR! Could not save new card to the wallet");
                    }
                    var walletModel = RegistryLoaderWrapper.GetInstance<IWalletModel>(contextProfileName);
                    walletModel.Active = true;
                    walletModel.CreatedDate = timestamp;
                    walletModel.CardHolderName = payment.CardHolderName;
                    walletModel.CreditCardNumber = payment.CardNumber;
                    walletModel.AccountNumber = payment.AccountNumber;
                    walletModel.RoutingNumber = payment.RoutingNumber;
                    walletModel.BankName = payment.BankName;
                    walletModel.ExpirationMonth = payment.ExpirationMonth;
                    walletModel.ExpirationYear = payment.ExpirationYear;
                    walletModel.Name = payment.ReferenceName;
                    walletModel.UserID = userID;
                    var walletResult = await Workflows.Wallets.CreateWalletEntryAsync(
                            walletModel,
                            walletProvider,
                            contextProfileName)
                        .ConfigureAwait(false);
                    if (!walletResult.ActionSucceeded)
                    {
                        return CEFAR.FailingCEFAR<string>("ERROR! Unable to use wallet");
                    }
                    payment.WalletID = walletResult.Result!.ID;
                }
                // Read the wallet data token from the wallet if valid
                if (Contract.CheckValidID(payment.WalletID))
                {
                    var wallet = await Workflows.Wallets.GetDecryptedWalletAsync(
                            userID,
                            payment.WalletID!.Value,
                            contextProfileName)
                        .ConfigureAwait(false);
                    if (!wallet.ActionSucceeded)
                    {
                        return CEFAR.FailingCEFAR<string>("ERROR! Unable to use wallet");
                    }
                    var card = wallet.Result!;
                    payment.ReferenceName = card.Name;
                    payment.CardHolderName = card.CardHolderName;
                    payment.CardNumber = card.CreditCardNumber;
                    payment.ExpirationMonth = card.ExpirationMonth;
                    payment.ExpirationYear = card.ExpirationYear;
                    payment.CardType = card.CardType;
                    payment.Token = card.Token;
                    payment.AccountNumber = card.AccountNumber;
                    payment.RoutingNumber = card.RoutingNumber;
                    payment.BankName = card.BankName;
                }
            }
            // Note: In the case of the surcharge provider wrapper, this will generate more invoices tied to the
            // invoices on payment.SalesInvoices (prorated based on amount paid).
            var paymentResult = await paymentProvider.AuthorizeAndACaptureAsync(
                    payment,
                    billing,
                    billing,
                    false, // TODO: Adjust this based on multi-currency data
                    contextProfileName)
                .ConfigureAwait(false);
            if (paymentResult?.Approved != true)
            {
                return CEFAR.FailingCEFAR<string>(
                    $"ERROR! Transaction failed: {paymentResult?.ResponseCode}");
            }
            payment.Token = paymentResult.AuthorizationCode;
            payment.TransactionNumber = paymentResult.TransactionID;
            var paymentMethodKey = payment.PaymentMethodKey ?? (string.IsNullOrEmpty(payment.BankName) ? "Credit Card" : "Wire Transfer");
            payment.PaymentMethodKey = paymentMethodKey;
            var paymentType = payment.TypeKey
                ?? (string.IsNullOrEmpty(payment.CardNumber) ? "General" : FindType(payment.CardNumber!));
            payment.TypeID = 0;
            payment.TypeKey = paymentType;
            payment.StatusID = 0;
            payment.StatusKey = "Captured";
            if (payment.CardNumber?.Length > 4)
            {
                payment.Last4CardDigits = payment.CardNumber[^4..];
            }
            else
            {
                if (payment.AccountNumber?.Length > 4)
                {
                    payment.AccountNumberLast4 = payment.AccountNumber[^4..];
                }
                if (payment.RoutingNumber?.Length > 4)
                {
                    payment.RoutingNumberLast4 = payment.RoutingNumber[^4..];
                }
            }
            if (surchargeDescriptor.HasValue)
            {
                await CommonSurchargeCompletionLogic(
                        surchargeDescriptor.Value,
                        surchargeAmount,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            // Store the master payment
            await Workflows.Payments.CreateAsync(payment, contextProfileName).ConfigureAwait(false);
            // Split the payment amount among the individual invoices and add payment records for each
            var result = await CEFAR.AggregateAsync(
                    amounts.Select(kvp =>
                    {
                        // Create the individual payment record by cloning the main payment model and just putting the
                        // specific amount on it
                        var innerPaymentModel = payment.CreatePaymentEntity(timestamp, contextProfileName).CreatePaymentModelFromEntityFull(contextProfileName);
                        innerPaymentModel!.Active = true;
                        innerPaymentModel.CreatedDate = timestamp;
                        innerPaymentModel.UpdatedDate = null;
                        innerPaymentModel.Amount = kvp.Value;
                        innerPaymentModel.PaymentMethodKey = paymentMethodKey;
                        innerPaymentModel.TypeKey = paymentType;
                        innerPaymentModel.StatusKey = "Captured";
                        // innerPaymentModel.ExternalPaymentID = mainPayment?.ID?.ToString();
                        return (id: kvp.Key, pmt: innerPaymentModel);
                    }),
                    x => AddPaymentAsync(invoices[x.id], x.pmt, x.pmt.Amount, contextProfileName)!)
                .ConfigureAwait(false);
            return new(result, payment.TransactionNumber);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsPaidAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesInvoices.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(InvoiceStateIDOfHistory))
            {
                InvoiceStateIDOfHistory = await Workflows.SalesInvoiceStates.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "HISTORY",
                        byName: "History",
                        byDisplayName: "History",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(InvoiceStatusIDOfVoid))
            {
                InvoiceStatusIDOfPaid = await Workflows.SalesInvoiceStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Paid",
                        byName: "Paid",
                        byDisplayName: "Paid",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            entity.StatusID = InvoiceStateIDOfHistory;
            entity.StateID = InvoiceStatusIDOfPaid;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR("ERROR! Something about updating this record failed");
            }
            await new SalesInvoicePaidNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["invoice"] = entity.CreateSalesInvoiceModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsUnpaidAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesInvoices.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(InvoiceStateIDOfHistory))
            {
                InvoiceStateIDOfWork = await Workflows.SalesInvoiceStates.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "WORK",
                        byName: "Work",
                        byDisplayName: "Work",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(InvoiceStatusIDOfVoid))
            {
                InvoiceStatusIDOfUnpaid = await Workflows.SalesInvoiceStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Unpaid",
                        byName: "Unpaid",
                        byDisplayName: "Unpaid",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            entity.StatusID = InvoiceStateIDOfWork;
            entity.StateID = InvoiceStatusIDOfUnpaid;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR("ERROR! Something about updating this record failed");
            }
            await new SalesInvoiceUnpaidNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["invoice"] = entity.CreateSalesInvoiceModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsPartiallyPaidAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesInvoices.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(InvoiceStateIDOfHistory))
            {
                InvoiceStateIDOfWork = await Workflows.SalesInvoiceStates.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "WORK",
                        byName: "Work",
                        byDisplayName: "Work",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(InvoiceStatusIDOfVoid))
            {
                InvoiceStatusIDOfPartiallyPaid = await Workflows.SalesInvoiceStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Partially Paid",
                        byName: "Partially Paid",
                        byDisplayName: "Partially Paid",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            entity.StatusID = InvoiceStateIDOfWork;
            entity.StateID = InvoiceStatusIDOfPartiallyPaid;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR("ERROR! Something about updating this record failed");
            }
            await new SalesInvoicePartiallyPaidNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["invoice"] = entity.CreateSalesInvoiceModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> SetRecordAsVoidedAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entity = await context.SalesInvoices.FilterByID(id).SingleAsync().ConfigureAwait(false);
            if (Contract.CheckInvalidID(InvoiceStateIDOfHistory))
            {
                InvoiceStateIDOfHistory = await Workflows.SalesInvoiceStates.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "HISTORY",
                        byName: "History",
                        byDisplayName: "History",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(InvoiceStatusIDOfVoid))
            {
                InvoiceStatusIDOfVoid = await Workflows.SalesInvoiceStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Void",
                        byName: "Void",
                        byDisplayName: "Void",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            entity.StatusID = InvoiceStatusIDOfVoid;
            entity.StateID = InvoiceStateIDOfHistory;
            entity.UpdatedDate = DateExtensions.GenDateTime;
            if (!await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR("ERROR! Something about updating this record failed");
            }
            await new SalesInvoiceVoidedNotificationToCustomerEmail().QueueAsync(
                    contextProfileName: contextProfileName,
                    to: null,
                    parameters: new()
                    {
                        ["invoice"] = entity.CreateSalesInvoiceModelFromEntityFull(contextProfileName),
                    })
                .ConfigureAwait(false);
            return CEFAR.PassingCEFAR();
        }

        /// <summary>Searches for the first type.</summary>
        /// <param name="cardNumber">The card number.</param>
        /// <returns>The found type.</returns>
        protected static string FindType(string cardNumber)
        {
            // https://www.regular-expressions.info/creditcard.html
            if (Regex.Match(cardNumber, "^4[0-9]{12}(?:[0-9]{3})?$").Success)
            {
                return "VISA";
            }
            if (Regex.Match(cardNumber, "^(?:5[1-5][0-9]{2}|222[1-9]|22[3-9][0-9]|2[3-6][0-9]{2}|27[01][0-9]|2720)[0-9]{12}$").Success)
            {
                return "Mastercard";
            }
            if (Regex.Match(cardNumber, "^3[47][0-9]{13}$").Success)
            {
                return "American Express";
            }
            if (Regex.Match(cardNumber, "^6(?:011|5[0-9]{2})[0-9]{12}$").Success)
            {
                return "Discover";
            }
            return "General";
        }

        /// <summary>Updates the sales order status.</summary>
        /// <param name="invoiceID">         Identifier for the invoice.</param>
        /// <param name="statusKey">         The status key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task UpdateSalesOrderStatusAsync(
            int invoiceID,
            string statusKey,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var orderIDs = await context.SalesOrderSalesInvoices
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.SlaveID == invoiceID)
                .Select(x => x.MasterID)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
            var orders = context.SalesOrders
                .FilterByActive(true)
                .FilterByIDs(orderIDs);
            var count = await orders.AsNoTracking().CountAsync().ConfigureAwait(false);
            if (count is > 1 or 0)
            {
                return;
            }
            var salesOrderStatusID = await context.SalesOrderStatuses
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByCustomKey(statusKey, true)
                .Select(x => x.ID)
                .SingleAsync()
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(salesOrderStatusID))
            {
                return;
            }
            var salesOrder = await orders.SingleAsync().ConfigureAwait(false);
            if (salesOrder.StatusID == salesOrderStatusID)
            {
                return;
            }
            salesOrder.StatusID = salesOrderStatusID;
            salesOrder.UpdatedDate = DateExtensions.GenDateTime;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        /// <summary>Updates the sales order balance due.</summary>
        /// <param name="invoiceID">         Identifier for the invoice.</param>
        /// <param name="balanceDue">        The balance due.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task UpdateSalesOrderBalanceDueAsync(
            int invoiceID,
            decimal? balanceDue,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var orderIDs = await context.SalesOrderSalesInvoices
                .AsNoTracking()
                .FilterByActive(true)
                .Where(x => x.SlaveID == invoiceID)
                .Select(x => x.MasterID)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
            var orders = context.SalesOrders
                .FilterByActive(true)
                .FilterByIDs(orderIDs);
            var count = await orders.AsNoTracking().CountAsync().ConfigureAwait(false);
            if (count is > 1 or 0)
            {
                return;
            }
            var salesOrder = await orders.SingleAsync().ConfigureAwait(false);
            if (salesOrder.BalanceDue == balanceDue)
            {
                return;
            }
            salesOrder.BalanceDue = balanceDue;
            salesOrder.UpdatedDate = DateExtensions.GenDateTime;
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
        }

        private static async Task<string?> DeterminePaymentMethodAsync(
            IPaymentModel payment,
            string? contextProfileName)
        {
            string? paymentMethod = null;
            if (Contract.CheckValidID(payment.WalletID))
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var entry = await context.Wallets
                    .AsNoTracking()
                    .FilterByID(payment.WalletID!.Value)
                    .Select(x => new
                    {
                        x.CreditCardNumber,
                        x.AccountNumber,
                    })
                    .SingleAsync()
                    .ConfigureAwait(false);
                if (Contract.CheckValidKey(entry.CreditCardNumber))
                {
                    paymentMethod = Enums.PaymentMethodsStrings.CreditCard;
                }
                else if (Contract.CheckValidKey(entry.AccountNumber))
                {
                    paymentMethod = Enums.PaymentMethodsStrings.Echeck;
                }
                // TODO: Other payment method types
            }
            else
            {
                if (Contract.CheckValidKey(payment.CardNumber))
                {
                    paymentMethod = Enums.PaymentMethodsStrings.CreditCard;
                }
                else if (Contract.CheckValidKey(payment.AccountNumber))
                {
                    paymentMethod = Enums.PaymentMethodsStrings.Echeck;
                }
                // TODO: Other payment method types
            }
            return paymentMethod;
        }

        /// <summary>Common logic for marking a surcharge as complete in
        /// <see cref="PaySingleByIDAsync(int, IPaymentModel, IContactModel?, string, bool, int?)"/>
        /// and <see cref="PayMultipleByAmountsAsync(Dictionary{int, decimal}, IPaymentModel, IContactModel?, int, string)"/>.</summary>
        /// <remarks>Should be called before the payment is deducted from the invoice.</remarks>
        /// <param name="surchargeDescriptor">Descriptor of what we're surcharging for.</param>
        /// <param name="surchargeAmount">The amount we previously surcharged for.</param>
        /// <param name="contextProfileName">For dependency injection.</param>
        private static async Task CommonSurchargeCompletionLogic(
            SurchargeDescriptor surchargeDescriptor,
            decimal surchargeAmount,
            string? contextProfileName)
        {
            var surchargeProvider = RegistryLoaderWrapper.GetSurchargeProvider(contextProfileName);
            if (surchargeProvider is not null)
            {
                await surchargeProvider.MarkCompleteAsync(
                        surchargeDescriptor,
                        mayThrow: false,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (!CEFConfigDictionary.CreateSurchargeInvoiceEnabled || surchargeAmount <= 0)
            {
                return;
            }
            // Originally, I'd return in these error checking conditionals. However, it's more important to record
            // those surcharges somewhere, even if they're wonky.
            if (surchargeDescriptor.ApplicableInvoices?.Any(i => i.BalanceDue is null) is true or null)
            {
                await Logger.LogErrorAsync(
                        "No balance due",
                        "Somehow we finished a payment on an invoice without a balance due.",
                        contextProfileName)
                    .ConfigureAwait(false);
                return;
            }
            // Surcharge amount is to be prorated across all invoices paid for, with one new invoice generated for each
            // paid invoice to contain the amount paid.
            var totalDueOnAll = surchargeDescriptor.ApplicableInvoices.Sum(i => i.BalanceDue!.Value);
            var invoiceProportions = surchargeDescriptor.ApplicableInvoices
                .ToDictionary(i => i, i => i.BalanceDue!.Value / totalDueOnAll);
            if (invoiceProportions.Sum(kvp => kvp.Value) > 1)
            {
                await Logger.LogErrorAsync(
                        "Bad proportions",
                        "Something's wrong with the balance logic; Proportion was > 1.",
                        contextProfileName)
                    .ConfigureAwait(false);
                return;
            }
            var surchargePerInvoice = invoiceProportions.ToDictionary(i => i.Key, i => surchargeAmount * i.Value);
            if (surchargePerInvoice.Sum(kvp => kvp.Value) != surchargeAmount)
            {
                await Logger.LogErrorAsync(
                        "Bad surcharge split",
                        "Something's wrong with the surcharge split logic; Sum != totalSurchargeAmount.",
                        contextProfileName)
                    .ConfigureAwait(false);
                return;
            }
            foreach (var kvp in surchargePerInvoice)
            {
                var invoice = kvp.Key;
                var surcharge = kvp.Value;
                ////var itemName = $"Surcharge for paying {surchargeDescriptor.TotalAmount * invoiceProportions[invoice]} on invoice {invoice.CustomKey} (#{invoice.ID})";
                string ReplaceTemplateVars(string target) => target
                    .Replace(
                        "{tgtSurchargeAmount}",
                        (surchargeDescriptor.TotalAmount * invoiceProportions![invoice!]).ToString())
                    .Replace("{tgtInvoiceKey}", invoice!.CustomKey)
                    .Replace("{tgtInvoiceID}", invoice.ID.ToString());
                ////var title = ReplaceTemplateVars(CEFConfigDictionary.CreateSurchargeInvoiceTitleTemplate);
                var itemSKU = ReplaceTemplateVars(CEFConfigDictionary.CreateSurchargeInvoiceSKUTemplate);
                var itemName = ReplaceTemplateVars(CEFConfigDictionary.CreateSurchargeInvoiceItemNameTemplate);
                var surchargeInvoice = new SalesInvoiceModel
                {
                    Active = true,
                    // NOTE: Use (Cast) style for the first two because they must always be there and "as" style for
                    // subsequent casts because they may be null
                    BillingContact = (ContactModel?)invoice.BillingContact,
                    User = (UserModel?)invoice.User,
                    Account = invoice.Account as AccountModel,
                    Brand = invoice.Brand as BrandModel,
                    Store = invoice.Store as StoreModel,
                    SalesGroup = invoice.SalesGroup as SalesGroupModel,
                    Shipment = invoice.Shipment as ShipmentModel,
                    BalanceDue = 0,
                    StatusKey = CEFConfigDictionary.CreateSurchargeInvoiceStatusKey,
                    StateKey = CEFConfigDictionary.CreateSurchargeInvoiceStateKey,
                    TypeKey = CEFConfigDictionary.CreateSurchargeInvoiceTypeKey,
                    Totals = new() { SubTotal = surcharge, },
                    SalesItems = new()
                    {
                        new()
                        {
                            Active = true,
                            UnitSoldPrice = surcharge,
                            Sku = itemSKU,
                            Name = itemName,
                            DateReceived = DateTime.UtcNow,
                            // TODO: OriginalCurrency?
                            Quantity = 1m,
                            User = (UserModel?)invoice.User,
                        },
                    },
                    SerializableAttributes = new()
                    {
                        [CEFConfigDictionary.CreateSurchargeInvoiceGeneratedByAttributeKey] = new()
                        {
                            Key = CEFConfigDictionary.CreateSurchargeInvoiceGeneratedByAttributeKey,
                            Value = invoice.ID.ToString(),
                        },
                    },
                };
                var newInvoice = await Workflows.SalesInvoices.CreateAsync(
                        surchargeInvoice,
                        contextProfileName)
                    .ConfigureAwait(false);
                await Logger.LogInformationAsync(
                        "Created surcharge invoice",
                        $"Created surcharge invoice #{newInvoice.Result}",
                        contextProfileName)
                    .ConfigureAwait(false);
            }
        }

        /// <summary>Common logic for calculating a surcharge in
        /// <see cref="PaySingleByIDAsync(int, IPaymentModel, IContactModel?, string?, bool, int?)"/> and
        /// <see cref="PayMultipleByAmountsAsync(Dictionary{int, decimal}, IPaymentModel, IContactModel?, int, string)"/>.</summary>
        /// <param name="payment">           The payment we're making.</param>
        /// <param name="billing">           The contact making the payment.</param>
        /// <param name="salesInvoices">     The invoices being paid.</param>
        /// <param name="contextProfileName">For dependency injection.</param>
        /// <returns>A descriptor of what we're surcharging for, to be later used in
        /// <see cref="CommonSurchargeCompletionLogic(SurchargeDescriptor, decimal, string)"/>.</returns>
        private static async Task<(SurchargeDescriptor? descriptor, decimal amount)> CommonSurchargeCalculationLogic(
            IPaymentModel payment,
            IContactModel? billing,
            IEnumerable<ISalesInvoiceModel> salesInvoices,
            string? contextProfileName)
        {
            var surchargeProvider = RegistryLoaderWrapper.GetSurchargeProvider(contextProfileName);
            if (surchargeProvider is null)
            {
                return (null, 0m);
            }
            var surchargeDescriptor = new SurchargeDescriptor
            {
                ApplicableInvoices = salesInvoices.ToHashSet(),
                BillingContact = billing,
                BIN = payment.SerializableAttributes?["BIN"]?.Value is { } bin && Contract.CheckValidKey(bin)
                    ? bin
                    : payment.WalletID is { } walletID
                        && await Workflows.Wallets.GetAsync(walletID, contextProfileName).ConfigureAwait(false) is { } usingWallet
                        && usingWallet.SerializableAttributes.TryGetValue("BIN", out var binVal)
                        ? binVal.Value
                        : throw new Exception("Unable to get BIN from either payment or wallet. Unable to surcharge."),
                TotalAmount = payment.Amount,
            };
            var (descriptor, amount) = await surchargeProvider.CalculateSurchargeAsync(
                    surchargeDescriptor,
                    contextProfileName)
                .ConfigureAwait(false);
            payment.Amount += amount;
            return (descriptor, amount);
        }

        // ReSharper disable once UnusedTupleComponentInReturnValue
        private static (decimal fee, decimal total) CalculateUpliftFee(decimal amount, string paymentMethod)
        {
            // TODO: Set up other payment types in this form
            return paymentMethod switch
            {
                Enums.PaymentMethodsStrings.Echeck => CalculateUpliftFeeInner(
                    amount, CEFConfigDictionary.PaymentsByECheckUpliftPercent,
                    CEFConfigDictionary.PaymentsByECheckUpliftPercent,
                    CEFConfigDictionary.PaymentsByECheckUpliftUseWhicheverIsGreater),
                Enums.PaymentMethodsStrings.CreditCard => CalculateUpliftFeeInner(
                    amount, CEFConfigDictionary.PaymentsByCreditCardUpliftPercent,
                    CEFConfigDictionary.PaymentsByCreditCardUpliftAmount,
                    CEFConfigDictionary.PaymentsByCreditCardUpliftUseWhicheverIsGreater),
                _ => (fee: 0m, total: amount),
            };
        }

        private static (decimal fee, decimal total) CalculateUpliftFeeInner(
            decimal amount,
            decimal upliftPercent,
            decimal upliftAmount,
            bool useGreater)
        {
            var amt = amount;
            var fee = 0m;
            var feeP = fee;
            var amtP = amt;
            var feeA = fee;
            var amtA = amt;
            if (upliftPercent > 0m)
            {
                var thisFee = amt * (0m + upliftPercent);
                feeP += thisFee;
                amtP += thisFee;
            }
            if (upliftAmount > 0m)
            {
                feeA += upliftAmount;
                amtA += upliftAmount;
            }
            if (useGreater)
            {
                if (feeP > feeA)
                {
                    fee = feeP;
                    amt += amtP;
                }
                else
                {
                    fee = feeA;
                    amt = amtA;
                }
            }
            else
            {
                fee = feeP + feeA;
                amt += fee;
            }
            return (fee, amt);
        }
    }
}
