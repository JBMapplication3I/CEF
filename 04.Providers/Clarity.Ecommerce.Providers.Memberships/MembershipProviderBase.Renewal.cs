// <copyright file="MembershipProviderBase.Renewal.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership provider base class</summary>
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using JetBrains.Annotations;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public abstract partial class MembershipProviderBase
    {
        /// <inheritdoc/>
        [Pure]
        public virtual async Task<CEFActionResponse> IsSubscriptionInRenewalPeriodAsync(
            int subscriptionID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var subscription = await context.Subscriptions
                .AsNoTracking()
                .FilterByID(subscriptionID)
                .Select(x => new { x.ID, x.EndsOn })
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidID(subscription?.ID))
            {
                return CEFAR.FailingCEFAR("ERROR! Unable to locate subscription");
            }
            if (!Contract.CheckValidDate(subscription!.EndsOn))
            {
                return CEFAR.FailingCEFAR("No End Date specified for subscription, cannot renew.");
            }
            var timestamp = DateExtensions.GenDateTime;
            var minDate = timestamp.AddDays(CEFConfigDictionary.MembershipsRenewalPeriodBefore * -1);
            var maxDate = timestamp.AddDays(CEFConfigDictionary.MembershipsRenewalPeriodAfter);
            return (subscription.EndsOn >= minDate && subscription.EndsOn <= maxDate)
                .BoolToCEFAR("This subscription is outside the renewal period and cannot be modified.");
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> RenewMembershipAsync(
            int previousSubscriptionID,
            int productID,
            int? invoiceID,
            DateTime timestamp,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var previousSubscription = context.Subscriptions
                .AsNoTracking()
                .FilterByID(previousSubscriptionID)
                .SelectSingleFullSubscriptionAndMapToSubscriptionModel(contextProfileName);
            if (previousSubscription == null)
            {
                return CEFAR.FailingCEFAR<ISubscriptionModel>("Could not locate previous subscription.");
            }
            // Mark previous membership as renewed and update it
            previousSubscription.StatusID = 0;
            previousSubscription.Status = null;
            previousSubscription.StatusKey = "Renewed";
            previousSubscription.EndsOn = DateExtensions.GenDateTime;
            await Workflows.Subscriptions.UpdateAsync(previousSubscription, contextProfileName).ConfigureAwait(false);
            return await OnMembershipProductSalesOrderItemCreatedAsync(
                    productID: productID,
                    userID: previousSubscription.UserID,
                    accountID: previousSubscription.AccountID,
                    invoiceID: invoiceID,
                    timestamp: timestamp,
                    pricingFactoryContext: pricingFactoryContext,
                    fee: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> RenewMembershipAsync(
            int previousSubscriptionID,
            int productID,
            int? invoiceID,
            DateTime timestamp,
            decimal fee,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var previousSubscription = context.Subscriptions
                .AsNoTracking()
                .FilterByID(previousSubscriptionID)
                .SelectSingleFullSubscriptionAndMapToSubscriptionModel(contextProfileName);
            if (previousSubscription == null)
            {
                return CEFAR.FailingCEFAR<ISubscriptionModel>("Could not locate previous subscription.");
            }
            // Mark previous membership as renewed and update it
            previousSubscription.StatusID = 0;
            previousSubscription.Status = null;
            previousSubscription.StatusKey = "Renewed";
            previousSubscription.EndsOn = DateExtensions.GenDateTime;
            await Workflows.Subscriptions.UpdateAsync(previousSubscription, contextProfileName).ConfigureAwait(false);
            return await OnMembershipProductSalesOrderItemCreatedAsync(
                    productID: productID,
                    userID: previousSubscription.UserID,
                    accountID: previousSubscription.AccountID,
                    invoiceID: invoiceID,
                    timestamp: timestamp,
                    pricingFactoryContext: null,
                    fee: fee,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ModifySubscriptionForUserAsync(
            int subscriptionID,
            int? billingContactID,
            int? shippingContactID,
            int userID,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var subscription = Contract.RequiresNotNull(
                    await context.Subscriptions
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(subscriptionID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false));
                var accountID = Contract.RequiresValidID(
                    await context.Users
                        .AsNoTracking()
                        .FilterByActive(true)
                        .FilterByID(userID)
                        .Select(x => x.AccountID)
                        .SingleOrDefaultAsync()
                        .ConfigureAwait(false));
                if (subscription.AccountID != accountID)
                {
                    return CEFAR.FailingCEFAR("ERROR! Subscription does not match to the user's account.");
                }
                if (subscription.EndsOn.HasValue && subscription.EndsOn < timestamp)
                {
                    return CEFAR.FailingCEFAR(
                        "ERROR! Subscriptions which have already ended cannot be modified, submit a new order instead.");
                }
                if (subscription.SubscriptionHistories!.Count == 0)
                {
                    return CEFAR.FailingCEFAR(
                        "ERROR! Can only modify a Subscription which has previously had a payment on it, after the "
                        + "initial order.");
                }
                // Update the billing info with the payment provider
                var refID = subscription.SubscriptionHistories
                    .OrderByDescending(x => x.CreatedDate)
                    .First().Slave!.ExternalPaymentID;
                var paymentProvider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
                if (paymentProvider == null)
                {
                    return CEFAR.FailingCEFAR("ERROR! No Payment Provider has been set up");
                }
                if (paymentProvider is not ISubscriptionProviderBase subscriptionPaymentsProvider)
                {
                    return CEFAR.FailingCEFAR(
                        "ERROR! The current Payment Provider does not support subscription services.");
                }
                var modificationModel = RegistryLoaderWrapper.GetInstance<ISubscriptionPaymentModel>(contextProfileName);
                modificationModel.Subscription = subscription.CreateSubscriptionModelFromEntityFull(contextProfileName);
                modificationModel.SubscriptionReferenceNumber = refID;
                modificationModel.Amount = 0m;
                modificationModel.NumberOfPayments = 0;
                modificationModel.Payment = RegistryLoaderWrapper.GetInstance<IPaymentModel>(contextProfileName);
                if (Contract.CheckValidID(billingContactID))
                {
                    modificationModel.Payment.BillingContactID = billingContactID;
                    modificationModel.Payment.BillingContactKey = null;
                    modificationModel.Payment.BillingContact = null;
                }
                var updateResult = await subscriptionPaymentsProvider.UpdateSubscriptionAsync(modificationModel, contextProfileName).ConfigureAwait(false);
                if (!updateResult.Approved)
                {
                    return CEFAR.FailingCEFAR(
                        $"ERROR! Updating the subscription in the payment provider failed: {updateResult.ResponseCode}");
                }
                // Set the new default billing and shipping addresses in the address book if set
                var addressBook = await Workflows.AddressBooks.GetAddressBookAsync(accountID, contextProfileName).ConfigureAwait(false);
                if (Contract.CheckValidID(billingContactID))
                {
                    var currentBilling = addressBook
                        .Where(x => x.IsBilling)
                        .OrderByDescending(x => x.CreatedDate)
                        .FirstOrDefault();
                    if (currentBilling == null)
                    {
                        var billingToSetAsDefault = addressBook
                            .Where(x => x.SlaveID == billingContactID)
                            .OrderByDescending(x => x.CreatedDate)
                            .FirstOrDefault();
                        if (billingToSetAsDefault == null)
                        {
                            return CEFAR.FailingCEFAR(
                                $"ERROR! Cannot find the contact with ID '{billingContactID}' to set as default billing.");
                        }
                        if (billingToSetAsDefault.IsPrimary)
                        {
                            return CEFAR.FailingCEFAR(
                                "ERROR! Cannot set the primary shipping address as the primary billing address");
                        }
                        billingToSetAsDefault.IsBilling = true;
                        foreach (var entry in addressBook.Where(x => x.SlaveID != billingContactID && x.IsBilling))
                        {
                            entry.IsBilling = false;
                            await Workflows.AddressBooks.UpdateAddressInBookAsync(entry, contextProfileName).ConfigureAwait(false);
                        }
                        await Workflows.AddressBooks.UpdateAddressInBookAsync(billingToSetAsDefault, contextProfileName).ConfigureAwait(false);
                    }
                    else if (currentBilling.ID == billingContactID)
                    {
                        foreach (var entry in addressBook.Where(x => x.SlaveID != billingContactID && x.IsBilling))
                        {
                            entry.IsBilling = false;
                            await Workflows.AddressBooks.UpdateAddressInBookAsync(entry, contextProfileName).ConfigureAwait(false);
                        }
                    }
                }
                if (Contract.CheckValidID(shippingContactID))
                {
                    var currentShipping = addressBook
                        .Where(x => x.IsPrimary)
                        .OrderByDescending(x => x.CreatedDate)
                        .FirstOrDefault();
                    if (currentShipping == null)
                    {
                        var shippingToSetAsDefault = addressBook
                            .Where(x => x.SlaveID == shippingContactID)
                            .OrderByDescending(x => x.CreatedDate)
                            .FirstOrDefault();
                        if (shippingToSetAsDefault == null)
                        {
                            return CEFAR.FailingCEFAR(
                                $"ERROR! Cannot find the contact with ID '{shippingContactID}' to set as default shipping.");
                        }
                        if (shippingToSetAsDefault.IsBilling)
                        {
                            return CEFAR.FailingCEFAR(
                                "ERROR! Cannot set the primary billing address as the primary shipping address");
                        }
                        shippingToSetAsDefault.IsPrimary = true;
                        foreach (var entry in addressBook.Where(x => x.SlaveID != shippingContactID && x.IsPrimary))
                        {
                            entry.IsPrimary = false;
                            await Workflows.AddressBooks.UpdateAddressInBookAsync(entry, contextProfileName).ConfigureAwait(false);
                        }
                        await Workflows.AddressBooks.UpdateAddressInBookAsync(shippingToSetAsDefault, contextProfileName).ConfigureAwait(false);
                    }
                    else if (currentShipping.ID == shippingContactID)
                    {
                        foreach (var entry in addressBook.Where(x => x.SlaveID != shippingContactID && x.IsPrimary))
                        {
                            entry.IsPrimary = false;
                            await Workflows.AddressBooks.UpdateAddressInBookAsync(entry, contextProfileName).ConfigureAwait(false);
                        }
                    }
                }
            }
            return CEFAR.PassingCEFAR();
        }

        /////// <inheritdoc/>
        ////public CEFActionResponse RenewSubscription(
        ////    int subscriptionID,
        ////    int walletID,
        ////    IPricingFactoryContextModel pricingFactoryContext,
        ////    ITaxesProviderBase? taxesProvider,
        ////    string? contextProfileName)
        ////{
        ////    // TODO: make sure the card belongs to the logged in user
        ////    var membershipProvider = RegistryLoaderWrapper.GetMembershipProvider(contextProfileName);
        ////    if (membershipProvider == null)
        ////    {
        ////        throw new ArgumentException("Could not load the membership provider");
        ////    }
        ////    var previousMembership = Workflows.Subscriptions.Get(subscriptionID, contextProfileName);
        ////    if (previousMembership == null)
        ////    {
        ////        throw new ArgumentException("Could not find the specified membership");
        ////    }
        ////    // Check if in renewal period
        ////    if (!membershipProvider.IsSubscriptionInRenewalPeriod(subscriptionID, contextProfileName).ActionSucceeded)
        ////    {
        ////        throw new InvalidOperationException("Subscription is outside of the renewal period");
        ////    }
        ////    // Take Payment
        ////    var wallet = Workflows.Wallets.Get(walletID, contextProfileName);
        ////    if (wallet == null)
        ////    {
        ////        throw new ArgumentException("Wallet could not be retrieve with the provided ID");
        ////    }
        ////    var paymentProvider = RegistryLoaderWrapper.GetPaymentProvider();
        ////    if (paymentProvider == null)
        ////    {
        ////        throw new ArgumentException("Could not load the payment provider");
        ////    }
        ////    var renewalProductIDResponse = membershipProvider.GetMembershipRenewalProductID(
        ////        previousMembership.TypeID, contextProfileName);
        ////    if (!renewalProductIDResponse.ActionSucceeded)
        ////    {
        ////        return renewalProductIDResponse.ChangeFailingCEFARType<ISubscriptionModel>();
        ////    }
        ////    var (productID, overridePrice) = renewalProductIDResponse.Result;
        ////    var productModel = Workflows.Products.Get(productID, contextProfileName);
        ////    var amount = overridePrice
        ////        ?? CalculateTotalCostForRenewal(
        ////            productID,
        ////            productModel.IsTaxable,
        ////            wallet.UserID,
        ////            pricingFactoryContext,
        ////            taxesProvider,
        ////            contextProfileName);
        ////    var paymentModel = new PaymentModel
        ////    {
        ////        Active = true,
        ////        Amount = amount,
        ////        Token = wallet.Token
        ////    };
        ////    var transaction = ProcessTransactionForApproval(
        ////        paymentProvider,
        ////        paymentModel,
        ////        wallet.User.Contact,
        ////        contextProfileName);
        ////    if (!transaction.ActionSucceeded)
        ////    {
        ////        throw new InvalidOperationException(
        ////            "ERROR! Your transaction was not approved by the payment provider!");
        ////    }
        ////    // Create payment
        ////    var timestamp = DateExtensions.GenDateTime;
        ////    var payment = paymentModel.CreatePaymentEntity(timestamp);
        ////    payment.TransactionNumber = transaction.Result;
        ////    using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
        ////    {
        ////        context.Payments.Add(payment);
        ////        context.SaveUnitOfWork(true);
        ////    }
        ////    paymentModel.ID = payment.ID;
        ////    // Create SO/Invoice
        ////    var salesOrderModel = new SalesOrderModel
        ////    {
        ////        TypeKey = "Web",
        ////        StatusKey = "Completed",
        ////        Totals = new CartTotals { SubTotal = amount },
        ////        Active = true,
        ////        CreatedDate = timestamp,
        ////        UserID = wallet.UserID,
        ////        PaymentTransactionID = transaction.Result,
        ////        SalesItems = new List<SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>>
        ////        {
        ////            new SalesItemBaseModel<IAppliedSalesOrderItemDiscountModel, AppliedSalesOrderItemDiscountModel>
        ////            {
        ////                Active = true,
        ////                CreatedDate = timestamp,
        ////                ProductID = productModel.ID,
        ////                ProductKey = productModel.CustomKey,
        ////                UnitCorePrice = amount,
        ////                Name = productModel.Name,
        ////                Quantity = 1,
        ////                Sku = productModel.ManufacturerPartNumber,
        ////                StatusKey = "Completed"
        ////            }
        ////        }
        ////    };
        ////    salesOrderModel = (SalesOrderModel)Workflows.SalesOrders.Create(salesOrderModel, contextProfileName);
        ////    // Create invoice
        ////    var createSalesInvoiceResult = Workflows.SalesInvoices.CreateSalesInvoiceFromSalesOrder(
        ////        salesOrderModel, contextProfileName);
        ////    if (!createSalesInvoiceResult.ActionSucceeded)
        ////    {
        ////        return createSalesInvoiceResult.ChangeFailingCEFARType<ISubscriptionModel>();
        ////    }
        ////    // link payment
        ////    var addPaymentResult = Workflows.SalesInvoices.AddPaymentToSalesInvoice(
        ////        createSalesInvoiceResult.Result, paymentModel, contextProfileName);
        ////    if (!addPaymentResult.ActionSucceeded)
        ////    {
        ////        return addPaymentResult.ChangeFailingCEFARType<ISubscriptionModel>();
        ////    }
        ////    // Renew membership
        ////    return membershipProvider.RenewMembership(
        ////        subscriptionID,
        ////        productID,
        ////        createSalesInvoiceResult.Result?.ID,
        ////        createSalesInvoiceResult.Result?.CreatedDate ?? timestamp,
        ////        amount,
        ////        contextProfileName);
        ////}

        /////// <summary>Calculates the total cost for renewal of the subscription including tax.</summary>
        /////// <param name="productID">            Identifier for the product.</param>
        /////// <param name="isTaxable">            True if this SubscriptionWorkflow is taxable.</param>
        /////// <param name="userID">               Identifier for the user.</param>
        /////// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /////// <param name="taxesProvider">          The tax provider.</param>
        /////// <param name="contextProfileName">   Name of the context profile.</param>
        /////// <returns>The calculated total cost for renewal.</returns>
        ////private decimal CalculateTotalCostForRenewal(
        ////    int productID,
        ////    bool isTaxable,
        ////    int userID,
        ////    IPricingFactoryContextModel pricingFactoryContext,
        ////    ITaxesProviderBase? taxesProvider,
        ////    string? contextProfileName)
        ////{
        ////    var calculatedPrice = Workflows.PricingFactory.CalculatePrice(
        ////        productID,
        ////        null,
        ////        pricingFactoryContext,
        ////        contextProfileName);
        ////    var amount = calculatedPrice.UnitPrice;
        ////    if (!isTaxable || taxesProvider == null)
        ////    {
        ////        return amount;
        ////    }
        ////    var userContact = Workflows.Users.Get(userID, contextProfileName).Contact;
        ////    var cart = new CartModel
        ////    {
        ////        BillingContact = (ContactModel)userContact,
        ////        ShippingContact = (ContactModel)userContact,
        ////        SalesItems = new List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>
        ////        {
        ////            new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
        ////            {
        ////                Active = true,
        ////                UnitCorePrice = amount
        ////            }
        ////        }
        ////    };
        ////    amount += Task.Run(async () => await taxesProvider.CalculateCartAsync(
        ////            cart, userID, contextProfileName))
        ////        .Result
        ////        .TotalTaxes;
        ////    return amount;
        ////}
    }
}
