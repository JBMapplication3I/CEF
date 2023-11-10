// <copyright file="CreateRepeatOrdersFromActiveSubscriptionsTask.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create repeat orders from active subscriptions task class</summary>
// ReSharper disable PossibleInvalidOperationException
// ReSharper disable UnusedParameter.Local
namespace Clarity.Ecommerce.Providers.ScheduledTasks.Tasks.Subscriptions
{
    using System;
    using System.Collections.Generic;
    using System.Configuration;
    using System.Data.Entity;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Checkouts;
    using DataModel;
    using Ecommerce.Tasks;
    using Hangfire;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A create repeat orders from active subscriptions task.</summary>
    /// <seealso cref="TaskBase"/>
    public class CreateRepeatOrdersFromActiveSubscriptionsTask : TaskBase
    {
        private readonly DateTime oneMonth = DateTime.Now.AddMonths(-1);
        private readonly DateTime oneQuarter = DateTime.Now.AddMonths(-3);
        private readonly DateTime oneYear = DateTime.Now.AddYears(-1);
        private readonly DateTime filterBefore = DateTime.Now.AddMonths(-4);
        private readonly DateTime fourtyFiveDaysAgo = DateTime.Now.AddDays(-45);
        private readonly DateTime oneHundredTenDaysAgo = DateTime.Now.AddDays(-110);
        private readonly StringBuilder notes = new();

        /// <inheritdoc/>
        public override async Task ProcessAsync(IJobCancellationToken? cancellationToken)
        {
            // TODO: context profile name?
            string? contextProfileName = null;
            // ReSharper disable once ExpressionIsAlwaysNull
            if (GetActiveTaskJobsCount(contextProfileName) > 1)
            {
                return;
            }
            cancellationToken?.ThrowIfCancellationRequested();
            // ReSharper disable once ExpressionIsAlwaysNull
            await CreateOrdersFromSubscriptionsAsync(cancellationToken, contextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override List<IScheduledJobConfigurationSettingModel> DefaultSettings(DateTime timestamp, out string description)
        {
            description = "Read active subscriptions for repeatable orders which should be submitted on this day";
            return new()
            {
                CreateSetting(timestamp, $"Process {ConfigurationKey}: Cron Schedule", "0 0 * * *", null),
            };
        }

        /// <inheritdoc/>
        protected override Task LoadSettingsAsync(string? contextProfileName)
        {
            return LoadSettingsAsync(contextProfileName, "0 0 * * *");
        }

        private static SalesOrder? GetOriginalSalesOrder(ISubscription sub)
        {
            return sub.SalesGroup!.SalesOrderMasters!.FirstOrDefault();
        }

        private static ICartModel SetupCart(ISubscription sub, ISalesItemBaseModel<IAppliedCartItemDiscountModel> salesItem)
        {
            // Create Cart with item for subscription
            var cart = RegistryLoaderWrapper.GetInstance<ICartModel>();
            cart.Active = true;
            cart.SalesItems = new() { salesItem };
            cart.AccountID = sub.AccountID;
            cart.UserID = sub.UserID;
            cart.StatusID = 1;
            cart.StateID = 1;
            cart.TypeID = 1;
            cart.ShippingContactID = sub.User!.ContactID;
            cart.BillingContactID = sub.User.ContactID;

            return cart;
        }

        private static IAccountModel CreateAccountModel(ISubscription sub, string? contextProfileName)
        {
            return sub.Account.CreateAccountModelFromEntityFull(contextProfileName)!;
        }

        private static IUserModel CreateUserModel(ISubscription sub, string? contextProfileName)
        {
            return sub.User.CreateUserModelFromEntityFull(contextProfileName)!;
        }

        private static bool CartHasValidTaxAmount(ISalesCollectionBaseModel cart)
        {
            return cart.Totals.Tax >= 0;
        }

        private static async Task<IWalletProviderBase> VerifyPaymentProviderSupportsWalletAsync(string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetPaymentProvider(contextProfileName);
            if (provider is not IWalletProviderBase wallet)
            {
                throw new ArgumentException("Current payment provider doesn't implement wallet functionalities");
            }
            await provider.InitConfigurationAsync(contextProfileName).ConfigureAwait(false);
            return wallet;
        }

        private static async Task<CheckoutResult> ProcessPaymentAsync(ProcessSpecificCartToSingleOrder checkoutModel)
        {
            var checkoutService = new CheckoutService();
            return (CheckoutResult)await checkoutService.Post(checkoutModel).ConfigureAwait(false);
        }

        /// <summary>Creates orders from active subscriptions.</summary>
        /// <param name="cancellationToken"> The cancellation token.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new orders from subscriptions asynchronous.</returns>
        private async Task CreateOrdersFromSubscriptionsAsync(
            IJobCancellationToken? cancellationToken,
            string? contextProfileName)
        {
            cancellationToken?.ThrowIfCancellationRequested();
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var activeSubs = await GetActiveSubscriptionsAsync(context).ConfigureAwait(false);
                if (!activeSubs.Any())
                {
                    // If no active subs, there's nothing to process
                    return;
                }
                foreach (var sub in activeSubs)
                {
                    try
                    {
                        if (IsSubscriptionLastPaidDateBeforeRenewDate(sub))
                        {
                            continue;
                        }
                        if (!IsProcessable(sub, contextProfileName))
                        {
                            continue;
                        }
                        if (SubscriptionHasRecentPayment(context, sub, contextProfileName))
                        {
                            continue;
                        }
                        // ReSharper disable once UnusedVariable
                        var salesGroup = sub.SalesGroup;
                        var salesOrder = sub.SalesOrder ?? GetOriginalSalesOrder(sub);
                        var salesItemFromOrder = salesOrder?.SalesItems!.FirstOrDefault();
                        if (salesItemFromOrder?.ProductID == null)
                        {
                            continue;
                        }
                        var productId = salesItemFromOrder.ProductID ?? -1;
                        var product = await GetSubscriptionProductAsync(productId, contextProfileName).ConfigureAwait(false);
                        if (CEFConfigDictionary.MembershipLevelsEnabled)
                        {
                            // <add key="Clarity.Tasks.Subscription.ProductMembershipLevels" value="false" /> added to appsettings for RX1 - delete if we use Membership Levels
                            ////var productAssociation = GetSubscriptionProductAssociation(product);
                            ////var baseProductID = productAssociation?.MasterID ?? sub.ProductMembershipLevel.MasterID;
                        }
                        var productBase = await GetSubscriptionBaseProductAsync(productId, contextProfileName).ConfigureAwait(false);
                        var productQuantity = salesItemFromOrder.Quantity;
                        var salesItem = await SetupSalesItemAsync(sub, product, productBase, productQuantity, contextProfileName).ConfigureAwait(false);
                        var cartModel = SetupCart(sub, salesItem);
                        var cart = await GetCartAsync(sub, cartModel, contextProfileName).ConfigureAwait(false);
                        await SetShippingRatesAsync(sub, salesOrder!, cart, contextProfileName).ConfigureAwait(false);
                        if (!CartHasValidTaxAmount(cart))
                        {
                            continue;
                        }
                        var wallet = await GetWalletAsync(sub, contextProfileName).ConfigureAwait(false);
                        var checkoutModel = await CreateCheckoutModelAsync(cart, wallet, sub, salesOrder!, salesItem.UnitCorePrice, contextProfileName).ConfigureAwait(false);
                        var checkoutResult = await ProcessPaymentAsync(checkoutModel).ConfigureAwait(false);
                        if (checkoutResult.Succeeded)
                        {
                            await SubscriptionSuccessProcessAsync(context, sub, checkoutResult.OrderID!.Value, contextProfileName).ConfigureAwait(false);
                        }
                        else
                        {
                            if (await RetryWithRecentBillingAndSubscriptionWalletAsync(context, cart, sub, salesOrder!, salesItem.UnitCorePrice, contextProfileName).ConfigureAwait(false))
                            {
                                continue;
                            }
                            if (await RetryWithSubscriptionBillingAndRecentWalletAsync(context, cart, sub, salesOrder!, salesItem.UnitCorePrice, contextProfileName).ConfigureAwait(false))
                            {
                                continue;
                            }
                            if (await RetryWithRecentBillingAndRecentSubscriptionAsync(context, cart, sub, salesOrder!, salesItem.UnitCorePrice, contextProfileName).ConfigureAwait(false))
                            {
                                continue;
                            }
                            var subscriptionHistory = await CreateSubscriptionHistoryAsync(sub, 0, contextProfileName).ConfigureAwait(false);
                            await SendFailedEmailAsync(subscriptionHistory, contextProfileName).ConfigureAwait(false);
                            notes.Clear();
                        }
                    }
                    catch (Exception ex)
                    {
                        var ex2 = new JobFailedException($"Process {ConfigurationKey}: Unable to Create Order: {sub.ID}", ex);
                        await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex2.Message, ex2, contextProfileName).ConfigureAwait(false);
                    }
                }
            }
            catch (Exception ex)
            {
                var ex2 = new JobFailedException($"Process {ConfigurationKey}: Unable to Create Orders", ex);
                await Logger.LogErrorAsync("Scheduler." + ConfigurationKey, ex2.Message, ex2, contextProfileName).ConfigureAwait(false);
            }
        }

        private Task<List<Subscription>> GetActiveSubscriptionsAsync(IClarityEcommerceEntities context)
        {
            var now = DateExtensions.GenDateTime;
            return context.Subscriptions
                .FilterByActive(true)
                .Where(x => (x.EndsOn == null || x.EndsOn > now)
                         && x.LastPaidDate.HasValue
                         && x.LastPaidDate >= filterBefore
                         && x.LastPaidDate <= oneMonth)
                .ToListAsync();
        }

        private bool IsSubscriptionLastPaidDateBeforeRenewDate(ISubscription sub)
        {
            if (sub.LastPaidDate < filterBefore)
            {
                return true;
            }
            switch (sub.RepeatType!.Name!.ToLower())
            {
                case "monthly" when sub.LastPaidDate < fourtyFiveDaysAgo:
                case "quarterly" when sub.LastPaidDate < oneHundredTenDaysAgo:
                {
                    return true;
                }
                default:
                {
                    return false;
                }
            }
        }

        private DateTime? GetSubsHistoryLastPaymentDate(ISubscription sub, string? contextProfileName)
        {
            var subHistoryLastPayment = sub.SubscriptionHistories!.Any()
                ? sub.SubscriptionHistories!.OrderByDescending(x => x.PaymentDate).First().PaymentDate
                : sub.LastPaidDate;
            Logger.LogInformation(
                "Scheduler." + ConfigurationKey,
                $"Process {ConfigurationKey}: subsHistoryLastPayment: {subHistoryLastPayment}",
                contextProfileName);
            return subHistoryLastPayment;
        }

        private DateTime GetRepeatTimeFrame(ISubscription sub, string? contextProfileName)
        {
            var repeatTimeFrame = oneYear;
            var subHistoryLastPayment = GetSubsHistoryLastPaymentDate(sub, contextProfileName);
            // Check for subscription type and kick out if not time to process
            switch (sub.RepeatType!.Name!.ToLower())
            {
                case "monthly":
                {
                    if (sub.LastPaidDate <= oneMonth && subHistoryLastPayment <= oneMonth)
                    {
                        repeatTimeFrame = oneMonth;
                    }
                    break;
                }
                case "quarterly":
                {
                    if (sub.LastPaidDate <= oneQuarter
                        && subHistoryLastPayment <= oneQuarter
                        && sub.LastPaidDate >= oneHundredTenDaysAgo)
                    {
                        repeatTimeFrame = oneQuarter;
                    }
                    break;
                }
                case "annually":
                {
                    if (sub.LastPaidDate <= oneYear && subHistoryLastPayment <= oneYear)
                    {
                        repeatTimeFrame = oneYear;
                    }
                    break;
                }
                default:
                {
                    // Subscription type not found so do not process
                    Logger.LogInformation(
                        name: "Scheduler." + ConfigurationKey,
                        message: $"Process {ConfigurationKey}: Quitting because there are no subscriptions types that match this subscription",
                        contextProfileName: contextProfileName);
                    repeatTimeFrame = oneYear;
                    break;
                }
            }
            Logger.LogInformation(
                name: "Scheduler." + ConfigurationKey,
                message: $"Process {ConfigurationKey}: sub.RepeatType: {sub.RepeatType.Name.ToLower()}",
                contextProfileName: contextProfileName);
            return repeatTimeFrame;
        }

        private bool IsProcessable(ISubscription sub, string? contextProfileName)
        {
            var subHistoryLastPayment = GetSubsHistoryLastPaymentDate(sub, contextProfileName);
            if (!sub.IsAutoRefill)
            {
                return false;
            }
            // Check for subscription type and kick out if not time to process
            switch (sub.RepeatType!.Name!.ToLower())
            {
                case "monthly":
                {
                    if (sub.LastPaidDate <= oneMonth && subHistoryLastPayment <= oneMonth)
                    {
                        return true;
                    }
                    break;
                }
                case "quarterly":
                {
                    if (sub.LastPaidDate <= oneQuarter
                        && subHistoryLastPayment <= oneQuarter
                        && sub.LastPaidDate >= oneHundredTenDaysAgo)
                    {
                        return true;
                    }
                    break;
                }
                case "annually":
                {
                    if (sub.LastPaidDate <= oneYear && subHistoryLastPayment <= oneYear)
                    {
                        return true;
                    }
                    break;
                }
                default:
                {
                    // Subscription type not found so do not process
                    return false;
                }
            }
            return false;
        }

        private bool SubscriptionHasRecentPayment(IClarityEcommerceEntities context, ISubscription sub, string? contextProfileName)
        {
            var repeatTimeFrame = GetRepeatTimeFrame(sub, contextProfileName);
            var payments = context.Payments
                .FilterByActive(true)
                .Where(x => x.BillingContact!.Email1 == sub.User!.Contact!.Email1)
                .Where(x => x.CreatedDate <= repeatTimeFrame);
            if (payments.Any()
                && payments.Any(x => x.CreatedDate > sub.LastPaidDate
                    && x.Amount < sub.Fee || (x.CreatedDate - sub.LastPaidDate!).Value.TotalMinutes < 2))
            {
                return true;
            }
            return false;
        }

        private Task<IProductModel> GetSubscriptionProductAsync(int subscriptionMasterID, string? contextProfileName)
        {
            return Workflows.Products.GetAsync(subscriptionMasterID, contextProfileName)!;
        }

        ////private IProductAssociationModel GetSubscriptionProductAssociation(IProductModel product)
        ////{
        ////    return product.ProductAssociations.FirstOrDefault(x => x.TypeKey == "VARIANT-OF-MASTER" && x.Active);
        ////}

        private Task<IProductModel> GetSubscriptionBaseProductAsync(int baseProductID, string? contextProfileName)
        {
            return Workflows.Products.GetAsync(baseProductID, contextProfileName)!;
        }

        /*
        private decimal GetSubscriptionSalesOrderQuantity(IClarityEcommerceEntities context, Subscription sub, int? productID, int baseProductID, string? contextProfileName)
        {
            // Get underlying sales order item for original subscription order if possible
            var productQuantity = 1M;
            var originalSalesOrder = GetOriginalSalesOrder(context, sub);
            var salesOrderQuantity = context.SalesOrders
                                .FilterByActive(true)
                                .Where(x => x.UserID == sub.UserID && ((DbFunctions.DiffSeconds(sub.CreatedDate, x.CreatedDate) < 2)));
            if (originalSalesOrder != null)
            {
                if (Math.Abs((sub.CreatedDate - originalSalesOrder.CreatedDate).TotalSeconds) < 2)
                {
                    var salesOrderQuantityID = originalSalesOrder.ID;
                    originalSalesOrderID = originalSalesOrder.ID;
                    Logger.LogInformation("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Override quantity process found sales order with ID: {salesOrderQuantityID} - ProductID: {productID} - baseProductID: {baseProductID}", contextProfileName);
                    var salesOrderItemQuantity = context.SalesOrderItems
                                    .FilterByActive(true)
                                    .Where(x => x.MasterID == salesOrderQuantityID)
                                    .Where(x => x.ProductID == productID || x.ProductID == baseProductID);
                    if (salesOrderItemQuantity.Any())
                    {
                        Logger.LogInformation("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Override quantity process found sales order item", contextProfileName);
                        productQuantity = salesOrderItemQuantity.FirstOrDefault().Quantity;
                        Logger.LogInformation("Scheduler." + ConfigurationKey, $"Process {ConfigurationKey}: Override quantity for item with quantity of {productQuantity}", contextProfileName);
                    }
                }
            }
            return productQuantity;
        }
        */

        private async Task<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> SetupSalesItemAsync(
            ISubscription sub,
            IProductModel product,
            INameableBaseModel productBase,
            decimal productQuantity,
            string? contextProfileName)
        {
            var salesItem = RegistryLoaderWrapper.GetInstance<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            salesItem.Active = true;
            salesItem.ProductID = productBase.ID;
            salesItem.Name = productBase.Name;
            salesItem.ProductDescription = productBase.Description;
            salesItem.CustomKey = productBase.CustomKey;
            salesItem.CreatedDate = productBase.CreatedDate;
            salesItem.Quantity = productQuantity;
            salesItem.UnitOfMeasure = product.UnitOfMeasure;
            var prices = await Workflows.PricingFactory.CalculatePriceAsync(
                    productID: product.ID,
                    salesItemAttributes: null,
                    pricingFactoryContext: new PricingFactoryContextModel(),
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            salesItem.UnitCorePrice = prices.BasePrice;
            salesItem.UnitSoldPrice = prices.SalePrice;
            salesItem.UserID = sub.UserID;
            return salesItem;
        }

        private async Task<IPricingFactoryContextModel> GetPricingAsync(
            ISubscription sub,
            IUserModel userModel,
            IAccountModel accountModel)
        {
            var pricingFactoryContext = RegistryLoaderWrapper.GetInstance<IPricingFactoryContextModel>();
            ////pricingFactoryContext.StoreID = ParseRequestUrlReferrerToStoreID();
            var currentUserID = sub.UserID;
            ////if (currentUserID != null)
            ////{
            var currentUser = userModel;
            pricingFactoryContext.UserID = currentUserID;
            pricingFactoryContext.UserKey = userModel.UserName;
            pricingFactoryContext.UserRoles = (await Workflows.Authentication.GetRolesForUserAsync(currentUserID!.Value, null).ConfigureAwait(false))
                .Select(x => x.Name!)
                .ToList();
            pricingFactoryContext.CountryID = currentUser.Contact?.Address?.CountryID;
            var account = accountModel;
            ////if (account != null)
            ////{
            pricingFactoryContext.AccountID = account.ID;
            pricingFactoryContext.AccountKey = account.CustomKey;
            pricingFactoryContext.AccountTypeID = account.TypeID;
            pricingFactoryContext.PricePoint = account.AccountPricePoints?.SingleOrDefault(x => x.Active)?.SlaveKey
                ?? ConfigurationManager.AppSettings["SystemValues.Modes.TieredPricingMode.DefaultPricePointKey"]
                ?? "WEB";
            ////}
            pricingFactoryContext.CurrencyID = currentUser.CurrencyID;
            if (!Contract.CheckValidID(pricingFactoryContext.StoreID)
                && Contract.CheckValidID(currentUser.PreferredStoreID))
            {
                pricingFactoryContext.StoreID = currentUser.PreferredStoreID;
            }
            return pricingFactoryContext;
        }

        private async Task<ICartModel> GetCartAsync(ISubscription sub, ICartModel cartModel, string? contextProfileName)
        {
            var accountModel = CreateAccountModel(sub, contextProfileName);
            var userModel = CreateUserModel(sub, contextProfileName);
            var pricingFactoryContext = await GetPricingAsync(sub, userModel, accountModel).ConfigureAwait(false);
            var taxesProvider = await RegistryLoaderWrapper.GetTaxProviderAsync(contextProfileName).ConfigureAwait(false);
            var cart = await Workflows.Carts.AdminGetAsync(
                    lookupKey: new SessionCartBySessionAndTypeLookupKey(default, "Cart", sub.UserID, sub.AccountID),
                    pricingFactoryContext: pricingFactoryContext,
                    taxesProvider: taxesProvider,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            if (cart == null)
            {
                var cartResponse = await Workflows.Carts.AdminCreateAsync(cartModel, contextProfileName).ConfigureAwait(false);
                cart = await Workflows.Carts.AdminGetAsync(
                        lookupKey: new CartByIDLookupKey(
                            cartID: cartResponse.Result,
                            userID: sub.UserID,
                            accountID: sub.AccountID),
                        pricingFactoryContext: pricingFactoryContext,
                        taxesProvider: taxesProvider,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            else
            {
                // Empty cart
                cart.SalesItems!.RemoveAll(x => x.Active = true);
            }
            return cart!;
        }

        private async Task<IContactModel> GetBillingAddressAsync(
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            string? contextProfileName,
            bool getMostRecent = false)
        {
            if (getMostRecent)
            {
                var recentbillingContactID = sub.User!.Account!.AccountContacts!.OrderByDescending(x => x.UpdatedDate).FirstOrDefault(x => x.IsBilling)?.SlaveID;
                return (await Workflows.Contacts.GetAsync((int)recentbillingContactID!, contextProfileName).ConfigureAwait(false))!;
            }
            var billingContactID = salesOrder?.BillingContactID
                ?? sub.User!.Account!.AccountContacts!.OrderByDescending(x => x.UpdatedDate).FirstOrDefault(x => x.IsBilling)?.SlaveID;
            return (await Workflows.Contacts.GetAsync((int)billingContactID!, contextProfileName).ConfigureAwait(false))!;
        }

        private async Task<IContactModel> GetShippingAddressAsync(
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            string? contextProfileName)
        {
            var shippingContactID = salesOrder?.ShippingContactID
                ?? sub.User!.Account!.AccountContacts!.OrderByDescending(x => x.UpdatedDate).FirstOrDefault(x => x.IsPrimary)?.SlaveID;
            return (await Workflows.Contacts.GetAsync((int)shippingContactID!, contextProfileName).ConfigureAwait(false))!;
        }

        private async Task SetShippingRatesAsync(
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            ICartModel cart,
            string? contextProfileName)
        {
            var billingContact = await GetBillingAddressAsync(sub, salesOrder, contextProfileName).ConfigureAwait(false);
            var shippingContact = await GetShippingAddressAsync(sub, salesOrder, contextProfileName).ConfigureAwait(false);
            var shippingProviders = RegistryLoaderWrapper.GetShippingProviders(contextProfileName);
            if (shippingProviders.Any())
            {
                var shippingProvider = shippingProviders.First();
                var rates = await shippingProvider.GetRateQuotesAsync(
                        cart.ID,
                        cart.SalesItems!.Select(x => x.ID).ToList(),
                        billingContact,
                        shippingContact,
                        false,
                        contextProfileName)
                    .ConfigureAwait(false);
                await Workflows.Carts.ApplyRateQuoteToCartAsync(
                        CartByIDLookupKey.FromCart(cart),
                        rates.Result!.First().ID,
                        contextProfileName)
                    .ConfigureAwait(false);
                cart.Totals.Shipping = rates.Result!.First().Rate!.Value; // 7.99m;
            }
        }

        private async Task<IWalletModel?> GetWalletAsync(ISubscription sub, string? contextProfileName, bool getMostRecent = false)
        {
            var walletProvider = await VerifyPaymentProviderSupportsWalletAsync(contextProfileName).ConfigureAwait(false);
            var originalSalesOrder = GetOriginalSalesOrder(sub);
            if (getMostRecent || originalSalesOrder == null)
            {
                return (await Workflows.Wallets.GetWalletForUserAsync(sub.UserID!.Value, walletProvider, contextProfileName).ConfigureAwait(false))
                    .Result!
                    .OrderByDescending(x => x.UpdatedDate)
                    .FirstOrDefault();
            }
            originalSalesOrder.SerializableAttributes.TryGetValue("ProfileID", out var profileId);
            if (profileId == null)
            {
                return (await Workflows.Wallets.GetWalletForUserAsync(sub.UserID!.Value, walletProvider, contextProfileName).ConfigureAwait(false))
                    .Result!
                    .OrderByDescending(x => x.UpdatedDate)
                    .FirstOrDefault();
            }
            return (await Workflows.Wallets.GetWalletForUserAsync(sub.UserID!.Value, walletProvider, contextProfileName).ConfigureAwait(false))
                .Result!
                .FirstOrDefault(x => x.ID.ToString() == profileId.Value);
        }

        private async Task<ProcessSpecificCartToSingleOrder> CreateCheckoutModelAsync(
            ISalesCollectionBaseModel cart,
            IWalletModel? wallet,
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            decimal? productPriceBase,
            string? contextProfileName)
        {
            var billingContact = await GetBillingAddressAsync(sub, salesOrder, contextProfileName).ConfigureAwait(false);
            var shippingContact = await GetShippingAddressAsync(sub, salesOrder, contextProfileName).ConfigureAwait(false);
            return new()
            {
                WithCartInfo = new()
                {
                    CartID = cart.ID,
                    CartSessionID = cart.SessionID,
                    CartTypeName = "Cart",
                },
                WithUserInfo = new()
                {
                    UserID = sub.UserID,
                    UserName = sub.User!.UserName,
                    IsNewAccount = false,
                },
                PayByWalletEntry = new()
                {
                    WalletID = wallet?.ID,
                    WalletToken = wallet?.Token,
                },
                Billing = (ContactModel)billingContact,
                Shipping = (ContactModel)shippingContact,
                Amount = productPriceBase,
                SpecialInstructions = $"Subscription:{sub.ID}",
                PaymentStyle = "SingleOrderCheckoutProvider",
            };
        }

        private async Task<ISalesOrderModel> GetSalesOrderAsync(int orderID, string? contextProfileName)
        {
            return (await Workflows.SalesOrders.GetAsync(orderID, contextProfileName).ConfigureAwait(false))!;
        }

        private async Task<ISubscriptionHistoryModel> CreateSubscriptionHistoryAsync(
            ISubscription sub,
            int salesOrderPaymentID,
            string? contextProfileName)
        {
            var subscriptionHistoryModel = new SubscriptionHistoryModel
            {
                Active = true,
                CreatedDate = DateTime.Now,
                UpdatedDate = DateTime.Now,
                MasterID = sub.ID,
                SlaveID = salesOrderPaymentID,
                PaymentSuccess = true,
                Memo = sub.Memo,
                SerializableAttributes = new()
                {
                    ["Notes"] = new() { Key = "Notes", Value = notes.ToString() },
                },
            };
            // ReSharper disable once InvertIf
            if (salesOrderPaymentID == 0)
            {
                subscriptionHistoryModel.Slave = new()
                {
                    StatusKey = "No Payment Received",
                    TypeKey = "Other",
                };
                subscriptionHistoryModel.PaymentSuccess = false;
            }
            var createResponse = await Workflows.SubscriptionHistories.CreateAsync(subscriptionHistoryModel, contextProfileName).ConfigureAwait(false);
            return (await Workflows.SubscriptionHistories.GetAsync(createResponse.Result, contextProfileName).ConfigureAwait(false))!;
        }

        private async Task UpdateSubscriptionAsync(IBase sub, string? contextProfileName)
        {
            var subUpdated = (await Workflows.Subscriptions.GetAsync(sub.ID, contextProfileName).ConfigureAwait(false))!;
            if (string.IsNullOrEmpty(subUpdated.CustomKey) || !subUpdated.CustomKey!.Contains("SubUpdate"))
            {
                subUpdated.CustomKey = subUpdated.CustomKey + "|SubUpdate:" + DateTime.Now.ToString("O") + "|";
            }
            subUpdated.BillingPeriodsPaid++;
            // End subscription if billing periods paid are equal to billing periods total - billing periods are refills
            if (subUpdated.BillingPeriodsPaid >= subUpdated.BillingPeriodsTotal)
            {
                subUpdated.EndsOn = DateExtensions.GenDateTime;
            }
            subUpdated.LastPaidDate = DateTime.Now;
            subUpdated.UpdatedDate = DateTime.Now;
            await Workflows.Subscriptions.UpdateAsync(subUpdated, contextProfileName).ConfigureAwait(false);
        }

        // Deactivate subscription created during checkout (if one exists)
        private async Task DeactivateSubscriptionAsync(IClarityEcommerceEntities context, ISubscription sub, string? contextProfileName)
        {
            var subNewID = await context.Subscriptions
                .FilterByActive(true)
                .Where(x => x.UserID!.Value == sub.UserID!.Value
                    && x.CreatedDate > sub.CreatedDate
                    && DbFunctions.DiffMinutes(DateTime.Now, x.CreatedDate) < 2)
                .OrderByDescending(x => x.CreatedDate)
                .FirstOrDefaultAsync();
            if (subNewID is { ID: > 0 })
            {
                var subNew = await Workflows.Subscriptions.GetAsync(subNewID.ID, contextProfileName).ConfigureAwait(false);
                subNew!.Active = false;
                await Workflows.Subscriptions.UpdateAsync(subNew, contextProfileName).ConfigureAwait(false);
            }
        }

#pragma warning disable CA1822,IDE0060
        private Task SendSuccessEmailAsync(ISubscriptionHistoryModel subscriptionHistory, string? contextProfileName)
#pragma warning restore CA1822,IDE0060
        {
            return Task.CompletedTask;
            ////return Workflows.EmailQueues.Queue_Notifications_SalesOrders_Subscription_Success_Async(subscriptionHistory, notes.ToString(), contextProfileName);
        }

#pragma warning disable CA1822,IDE0060
        private Task SendFailedEmailAsync(ISubscriptionHistoryModel subscriptionHistory, string? contextProfileName)
#pragma warning restore CA1822,IDE0060
        {
            return Task.CompletedTask;
            ////return Workflows.EmailQueues.Queue_Notifications_SalesOrders_Subscription_Failed_Async(subscriptionHistory, notes.ToString(), contextProfileName);
        }

        private async Task SubscriptionSuccessProcessAsync(
            IClarityEcommerceEntities context,
            ISubscription sub,
            int orderID,
            string? contextProfileName)
        {
            var salesOrder = await GetSalesOrderAsync(orderID, contextProfileName).ConfigureAwait(false);
            var subscriptionHistory = await CreateSubscriptionHistoryAsync(sub, salesOrder.SalesOrderPayments![0].SlaveID, contextProfileName).ConfigureAwait(false);
            await UpdateSubscriptionAsync(sub, contextProfileName).ConfigureAwait(false);
            await SendSuccessEmailAsync(subscriptionHistory, contextProfileName).ConfigureAwait(false);
            await DeactivateSubscriptionAsync(context, sub, contextProfileName).ConfigureAwait(false);
        }

        private async Task<bool> RetryWithRecentBillingAndRecentSubscriptionAsync(
            IClarityEcommerceEntities context,
            ISalesCollectionBaseModel cart,
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            decimal? productPriceBase,
            string? contextProfileName)
        {
            ////var billingContact = await GetBillingAddressAsync(sub, salesOrder, contextProfileName, getMostRecent: true).ConfigureAwait(false);
            var wallet = await GetWalletAsync(sub, contextProfileName, getMostRecent: true).ConfigureAwait(false);
            var checkoutModel = await CreateCheckoutModelAsync(cart, wallet, sub, salesOrder, productPriceBase, contextProfileName).ConfigureAwait(false);
            var checkoutResult = await ProcessPaymentAsync(checkoutModel).ConfigureAwait(false);
            if (checkoutResult.Succeeded)
            {
                notes.Append($"{DateTime.Now} - Payment was Successful. Subscription {sub.ID} Retried with subscription billing and most recent wallet. ");
                await SubscriptionSuccessProcessAsync(context, sub, checkoutResult.OrderID!.Value, contextProfileName).ConfigureAwait(false);
                return true;
            }
            notes.Append($"{DateTime.Now} - Payment was Unsuccessful. Error: ");
            foreach (var message in checkoutResult.ErrorMessages)
            {
                notes.Append($"{message}.");
            }
            notes.Append($"Subscription {sub.ID} retried with subscription billing and most recent wallet.");
            return false;
        }

        private async Task<bool> RetryWithSubscriptionBillingAndRecentWalletAsync(
            IClarityEcommerceEntities context,
            ISalesCollectionBaseModel cart,
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            decimal? productPriceBase,
            string? contextProfileName)
        {
            ////var billingContact = await GetBillingAddressAsync(sub, salesOrder, contextProfileName).ConfigureAwait(false);
            var wallet = await GetWalletAsync(sub, contextProfileName, getMostRecent: true).ConfigureAwait(false);
            var checkoutModel = await CreateCheckoutModelAsync(cart, wallet, sub, salesOrder, productPriceBase, contextProfileName).ConfigureAwait(false);
            var checkoutResult = await ProcessPaymentAsync(checkoutModel).ConfigureAwait(false);
            if (checkoutResult.Succeeded)
            {
                notes.Append($"{DateTime.Now} - Payment was Successful. Subscription {sub.ID} retried with subscription billing and most recent wallet. ");
                await SubscriptionSuccessProcessAsync(context, sub, checkoutResult.OrderID!.Value, contextProfileName).ConfigureAwait(false);
                return true;
            }
            notes.Append($"{DateTime.Now} - Payment was Unsuccessful. Error: ");
            foreach (var message in checkoutResult.ErrorMessages)
            {
                notes.Append($"{message}.");
            }
            notes.Append($"Subscription {sub.ID} retried with subscription billing and most recent wallet. ");
            return false;
        }

        private async Task<bool> RetryWithRecentBillingAndSubscriptionWalletAsync(
            IClarityEcommerceEntities context,
            ISalesCollectionBaseModel cart,
            ISubscription sub,
            ISalesCollectionBase salesOrder,
            decimal? productPriceBase,
            string? contextProfileName)
        {
            ////var billingContact = await GetBillingAddressAsync(sub, salesOrder, contextProfileName, getMostRecent: true).ConfigureAwait(false);
            var wallet = await GetWalletAsync(sub, contextProfileName).ConfigureAwait(false);
            var checkoutModel = await CreateCheckoutModelAsync(cart, wallet, sub, salesOrder, productPriceBase, contextProfileName).ConfigureAwait(false);
            var checkoutResult = await ProcessPaymentAsync(checkoutModel).ConfigureAwait(false);
            if (checkoutResult.Succeeded)
            {
                notes.Append($"{DateTime.Now} - Payment was Successful. Subscription {sub.ID} retried with most recent billing and subscription wallet. ");
                await SubscriptionSuccessProcessAsync(context, sub, checkoutResult.OrderID!.Value, contextProfileName).ConfigureAwait(false);
                return true;
            }
            notes.Append($"{DateTime.Now} - Payment was Unsuccessful. Error: ");
            foreach (var message in checkoutResult.ErrorMessages)
            {
                notes.Append($"{message}.");
            }
            notes.Append($"Subscription {sub.ID} retried with most recent billing and subscription wallet.");
            return false;
        }
    }
}
