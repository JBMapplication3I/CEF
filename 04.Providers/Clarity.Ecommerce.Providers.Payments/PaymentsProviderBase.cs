// <copyright file="PaymentsProviderBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the payments provider base class</summary>
namespace Clarity.Ecommerce.Providers.Payments
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Pricing;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    /// <summary>The payments provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IPaymentsProviderBase"/>
    public abstract class PaymentsProviderBase : ProviderBase, IPaymentsProviderBase
    {
        /// <summary>The months.</summary>
        private static readonly ReadOnlyDictionary<string, string> Months = new(
            new Dictionary<string, string>
            {
                { "January", "01" }, { "Jan.", "01" }, { "Jan", "01" }, { "01", "01" }, { "1", "01" },
                { "February", "02" }, { "Feb.", "02" }, { "Feb", "02" }, { "02", "02" }, { "2", "02" },
                { "March", "03" }, { "Mar.", "03" }, { "Mar", "03" }, { "03", "03" }, { "3", "03" },
                { "April", "04" }, { "Apr.", "04" }, { "Apr", "04" }, { "04", "04" }, { "4", "04" },
                { "May", "05" }, { "05", "05" }, { "5", "05" },
                { "June", "06" }, { "Jun.", "06" }, { "Jun", "06" }, { "06", "06" }, { "6", "06" },
                { "July", "07" }, { "Jul.", "07" }, { "Jul", "07" }, { "07", "07" }, { "7", "07" },
                { "August", "08" }, { "Aug.", "08" }, { "Aug", "08" }, { "08", "08" }, { "8", "08" },
                { "September", "09" }, { "Sept.", "09" }, { "Sep", "09" }, { "09", "09" }, { "9", "09" },
                { "October", "10" }, { "Oct.", "10" }, { "Oct", "10" }, { "10", "10" },
                { "November", "11" }, { "Nov.", "11" }, { "Nov", "11" }, { "11", "11" },
                { "December", "12" }, { "Dec.", "12" }, { "Dec", "12" }, { "12", "12" },
            });

        private static readonly Dictionary<string, int> WordToNumberLookup = new()
        {
            { "zero", 0 },
            { "one", 1 },
            { "two", 2 },
            { "three", 3 },
            { "four", 4 },
            { "five", 5 },
            { "six", 6 },
            { "seven", 7 },
            { "eight", 8 },
            { "nine", 9 },
            { "ten", 10 },
            { "eleven", 11 },
            { "twelve", 12 },
            { "thirteen", 13 },
            { "fourteen", 14 },
            { "fifteen", 15 },
            { "sixteen", 16 },
            { "seventeen", 17 },
            { "eighteen", 18 },
            { "nineteen", 19 },
            { "twenty", 20 },
            { "thirty", 30 },
            { "forty", 40 },
            { "fifty", 50 },
            { "sixty", 60 },
            { "seventy", 70 },
            { "eighty", 80 },
            { "ninety", 90 },
            { "hundred", 100 },
        };

        /// <summary>Gets the provider mode.</summary>
        /// <value>The provider mode.</value>
        public static Enums.PaymentProviderMode ProviderMode => CEFConfigDictionary.PaymentsProviderMode;

        /// <inheritdoc/>
        public override Enums.ProviderType ProviderType => Enums.ProviderType.Payments;

        /// <inheritdoc/>
        public override bool HasDefaultProvider => true;

        /// <inheritdoc/>
        public override bool IsDefaultProvider => false;

        /// <inheritdoc/>
        public override bool IsDefaultProviderActivated { get; set; }

        /// <summary>Gets or sets the identifier of the subscription product type.</summary>
        /// <value>The identifier of the subscription product type.</value>
        private static int SubscriptionProductTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the subscription variant product type.</summary>
        /// <value>The identifier of the subscription variant product type.</value>
        private static int SubscriptionVariantProductTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the kit product type.</summary>
        /// <value>The identifier of the kit product type.</value>
        private static int KitProductTypeID { get; set; }

        /// <summary>Gets or sets the identifier of the variant kit product type.</summary>
        /// <value>The identifier of the variant kit product type.</value>
        private static int VariantKitProductTypeID { get; set; }

        /// <inheritdoc/>
        public abstract Task InitConfigurationAsync(string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<IPaymentResponse> AuthorizeAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<IPaymentResponse> CaptureAsync(
            string paymentAuthorizationToken,
            IProviderPayment payment,
            string? contextProfileName);

        /// <inheritdoc/>
        public abstract Task<IPaymentResponse> AuthorizeAndACaptureAsync(
            IProviderPayment payment,
            IContactModel? billing,
            IContactModel? shipping,
            bool paymentAlreadyConverted,
            string? contextProfileName,
            ICartModel? cart = null,
            bool useWalletToken = false);

        /// <inheritdoc/>
        public abstract Task<CEFActionResponse<string>> GetAuthenticationToken(
            string? contextProfileName);

        /// <summary>Implement product subscription from order item .</summary>
        /// <param name="salesOrderUserID">     Identifier for the sales order user.</param>
        /// <param name="salesOrderAccountID">  Identifier for the sales order account.</param>
        /// <param name="salesGroupID">         Identifier for the sales group.</param>
        /// <param name="salesOrderItem">       The sales order item.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="invoiceID">            Identifier for the invoice.</param>
        /// <param name="salesOrderID">         Identifier for the sales order.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        public virtual async Task<CEFActionResponse> ImplementProductSubscriptionFromOrderItemAsync(
            int? salesOrderUserID,
            int? salesOrderAccountID,
            int? salesGroupID,
            ISalesItemBaseModel<IAppliedSalesOrderItemDiscountModel> salesOrderItem,
            IPricingFactoryContextModel pricingFactoryContext,
            int? invoiceID,
            int? salesOrderID,
            DateTime timestamp,
            string? contextProfileName)
        {
            if (!Contract.CheckValidID(salesOrderItem?.ProductID))
            {
                return CEFAR.FailingCEFAR("No Product to read an ID from");
            }
            if (Contract.CheckInvalidID(SubscriptionProductTypeID))
            {
                SubscriptionProductTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                    null, "SUBSCRIPTION", "Subscription", "Subscription", null, contextProfileName).ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(SubscriptionVariantProductTypeID))
            {
                SubscriptionVariantProductTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                    null, "VARIANT-SUBSCRIPTION", "Variant Subscription", "Variant Subscription", null, contextProfileName).ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(KitProductTypeID))
            {
                KitProductTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                    null, "KIT", "Kit", "Kit", null, contextProfileName).ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(VariantKitProductTypeID))
            {
                VariantKitProductTypeID = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                    null, "VARIANT-KIT", "Variant Kit", "Variant Kit", null, contextProfileName).ConfigureAwait(false);
            }
            if (salesOrderItem!.ProductTypeID == SubscriptionProductTypeID
                || salesOrderItem.ProductTypeID == SubscriptionVariantProductTypeID)
            {
                return await OnSubscriptionProductSalesOrderItemCreatedAsync(
                        salesOrderItem.ProductID!.Value,
                        salesOrderItem.UserID ?? salesOrderUserID,
                        salesOrderAccountID,
                        invoiceID,
                        salesOrderID,
                        salesGroupID,
                        timestamp,
                        pricingFactoryContext,
                        salesOrderItem.UnitSoldPrice ?? salesOrderItem.UnitCorePrice,
                        contextProfileName)
                    .ConfigureAwait(false);
            }
            if (salesOrderItem.ProductTypeID != KitProductTypeID
                && salesOrderItem.ProductTypeID != VariantKitProductTypeID)
            {
                return CEFAR.FailingCEFAR(
                    "No action taken as product is not a subscription or kit that contains a subscription");
            }
            // NOTE: BOM Down is recursive, so we're getting all unique product IDs all the way down the
            // chain, not just one level
            var components1 = await Workflows.ProductKits.KitComponentBOMDownAsync(
                    salesOrderItem.ProductID!.Value,
                    contextProfileName)
                .ConfigureAwait(false);
            var components2 = components1
                .Where(x => Contract.CheckValidID(x.ID)
                    && (x.TypeID == SubscriptionProductTypeID
                        || x.TypeID == SubscriptionVariantProductTypeID));
            return await CEFAR.AggregateAsync(
                    components2,
                    x => OnSubscriptionProductSalesOrderItemCreatedAsync(
                        x.ID,
                        salesOrderItem.UserID ?? salesOrderUserID,
                        salesOrderAccountID,
                        invoiceID,
                        salesOrderID,
                        salesGroupID,
                        timestamp,
                        pricingFactoryContext,
                        salesOrderItem.UnitSoldPrice ?? salesOrderItem.UnitCorePrice,
                        contextProfileName))
                .ConfigureAwait(false);
        }

        /// <summary>Subscription calculate date.</summary>
        /// <param name="fromDate">     from date.</param>
        /// <param name="repeatTypeKey">The repeat type key.</param>
        /// <param name="repeatCount">  Number of repeats.</param>
        /// <returns>A DateTime.</returns>
        [Pure]
        internal static DateTime SubscriptionCalculateDate(DateTime fromDate, string repeatTypeKey, int? repeatCount)
        {
            if (repeatCount < 1)
            {
                repeatCount = 1;
            }
            // Eventually boil everything down to a count of months to add, but we need to figure it out from the
            // english language representation
            // Examples of what we think we need to process:
            // * 13 Months
            // * 13Months
            // * 13
            // * 13-months
            // * 13-month
            // * Yearly
            // * 1 year
            // * 2 years
            // * Monthly
            // * quarterly
            // * semi-annual
            // * semiannual
            // * semiyearly
            // * annual
            // * annually
            var matches = Regex.Matches(
                    repeatTypeKey.ToLowerInvariant(),
                    @"(?<numbers>\d+)|(?<words>[a-z]+)",
                    RegexOptions.IgnoreCase)
                .Cast<Match>();
            var count = 0;
            var byYear = new Regex(@"(?:year|annual)(?:ly|s)?");
            var byQuarter = new Regex(@"quarter(?:ly|s)?");
            var byHalf = new Regex(@"semi|half");
            var byDouble = new Regex(@"bi");
            var thinkInYears = false;
            var thinkInQuarters = false;
            var thinkInHalves = false;
            var thinkInDoubles = false;
            foreach (var match in matches)
            {
                if (Contract.CheckValidKey(match.Groups["numbers"]?.Value))
                {
                    count = int.Parse(match.Groups["numbers"].Value);
                    continue;
                }
                if (WordToNumberLookup.ContainsKey(match.Groups["words"].Value))
                {
                    count = WordToNumberLookup[match.Groups["words"].Value];
                    continue;
                }
                if (byYear.IsMatch(match.Groups["words"].Value))
                {
                    thinkInYears = true;
                }
                else if (byQuarter.IsMatch(match.Groups["words"].Value))
                {
                    thinkInQuarters = true;
                }
                if (byHalf.IsMatch(match.Groups["words"].Value))
                {
                    thinkInHalves = true;
                }
                if (byDouble.IsMatch(match.Groups["words"].Value))
                {
                    thinkInDoubles = true;
                }
            }
            if (count <= 0)
            {
                count = 1;
            }
            if (thinkInYears)
            {
                count *= 12;
            }
            else if (thinkInQuarters)
            {
                count *= 3;
            }
            else if (thinkInHalves)
            {
                // thinkInMonths
                // Force doubling
                thinkInDoubles = true;
            }
            if (thinkInHalves)
            {
                count /= 2;
            }
            if (thinkInDoubles)
            {
                count *= 2;
            }
            return fromDate.AddMonths(count * repeatCount.GetValueOrDefault());
        }

        /// <summary>Clean expiration date.</summary>
        /// <param name="month">            The month.</param>
        /// <param name="year">             The year.</param>
        /// <param name="needFourDigitYear">True to need four digit year.</param>
        /// <returns>A string.</returns>
        protected static string CleanExpirationDate(int month, int year, bool needFourDigitYear = false)
        {
            return $"{CleanExpirationMonth(month.ToString())}{CleanExpirationYear(year.ToString(), needFourDigitYear)}";
        }

        /// <summary>Query if 'payment' is ACH.</summary>
        /// <param name="payment">The payment.</param>
        /// <returns>True if ACH, false if not.</returns>
        protected static bool IsACH(IProviderPayment payment)
        {
            return Contract.CheckAllValidKeys(
                payment.CardHolderName,
                payment.CardType,
                payment.AccountNumber,
                payment.RoutingNumber,
                payment.BankName);
        }

        /// <summary>Generate a code that can be used to reference a payment to a CEF Sales Order</summary>
        /// <param name="toAppend">Value to append to the guid</param>
        /// <returns>The generated code.</returns>
        protected static string GenerateReferenceCode(string toAppend)
        {
            return $"{Guid.NewGuid()} | {toAppend}";
        }

        /// <summary>Executes the subscription product sales order item created action.</summary>
        /// <param name="productID">            Identifier for the product.</param>
        /// <param name="userID">               The identifier for the user.</param>
        /// <param name="accountID">            The identifier for the account.</param>
        /// <param name="invoiceID">            Identifier for the invoice.</param>
        /// <param name="salesOrderID">         Identifier for the sales order.</param>
        /// <param name="salesGroupID">         Identifier for the sales group.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="fee">                  The fee.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        protected virtual async Task<CEFActionResponse> OnSubscriptionProductSalesOrderItemCreatedAsync(
            int productID,
            int? userID,
            int? accountID,
            int? invoiceID,
            int? salesOrderID,
            int? salesGroupID,
            DateTime timestamp,
            IPricingFactoryContextModel pricingFactoryContext,
            decimal? fee,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var subscriptionProduct = await (await context.Products
                    .AsNoTracking()
                    .Include(x => x.ProductSubscriptionTypes)
                    .Include(x => x.ProductSubscriptionTypes!.Select(y => y.Slave))
                    .Include(x => x.ProductSubscriptionTypes!.Select(y => y.SubscriptionTypeRepeatType))
                    .Include(x => x.ProductSubscriptionTypes!.Select(y => y.SubscriptionTypeRepeatType).Select(y => y!.Slave))
                    .FilterByActive(true)
                    .FilterByID(productID)
                    .Select(x => new
                    {
                        x.ID,
                        x.Name,
                        x.Description,
                        x.ProductSubscriptionTypes,
                    })
                    .Take(1)
                    .ToListAsync())
                .Select(async x => new
                {
                    ProductName = x.Name,
                    ProductDescription = x.Description,
                    Price = fee
                        ?? (await RegistryLoaderWrapper.GetInstance<IPricingFactory>(contextProfileName).CalculatePriceAsync(
                                productID: x.ID,
                                salesItemAttributes: null,
                                pricingFactoryContext: pricingFactoryContext,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false))
                        .BasePrice,
                    ProductSubscriptionTypes = x.ProductSubscriptionTypes!
                        .Where(y => y.Active && y.Slave!.Active && y.SubscriptionTypeRepeatType!.Active)
                        ////.Select(BaseModelMapper.CreateProductSubscriptionTypeModelFromEntityFull)
                        .ToList<IProductSubscriptionType>(),
                })
                .Single()
                .ConfigureAwait(false);
            if (subscriptionProduct == null)
            {
                return CEFAR.FailingCEFAR<IEnumerable<ISubscriptionModel>>("Could not locate product");
            }
            if (Contract.CheckEmpty(subscriptionProduct.ProductSubscriptionTypes))
            {
                return CEFAR.FailingCEFAR<IEnumerable<ISubscriptionModel>>(
                    "Product does not contain subscription definitions");
            }
            var invoice = await context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .FilterByID(salesOrderID)
                .FirstOrDefaultAsync();
            var quantity = invoice?.SalesItems!.FirstOrDefault(p => p.ProductID == productID)?.Quantity;
            return await CEFAR.AggregateAsync(
                    subscriptionProduct.ProductSubscriptionTypes,
                    x => CreateSubscriptionInstanceForUserAsync(
                        x!,
                        subscriptionProduct.Price,
                        userID,
                        accountID,
                        invoiceID,
                        salesOrderID,
                        salesGroupID,
                        quantity ?? 0,
                        timestamp,
                        contextProfileName)!)
                .ConfigureAwait(false);
            ////return CEFAR.Aggregate(
            ////    subscriptionProduct.ProductSubscriptionTypes,
            ////    // NOTE: Typically only one
            ////    async x => await CreateSubscriptionInstanceForUserAsync(
            ////        x,
            ////        subscriptionProduct.Price,
            ////        userID,
            ////        accountID,
            ////        invoiceID,
            ////        salesOrderID,
            ////        salesGroupID,
            ////        quantity ?? 0,
            ////        timestamp,
            ////        contextProfileName));
        }

        /// <summary>Creates subscription instance for user.</summary>
        /// <param name="productSubscriptionType">The product subscription type.</param>
        /// <param name="fee">                    The fee.</param>
        /// <param name="userID">                 Identifier for the user.</param>
        /// <param name="accountID">              Identifier for the account.</param>
        /// <param name="invoiceID">              Identifier for the invoice.</param>
        /// <param name="salesOrderID">           Identifier for the sales order.</param>
        /// <param name="salesGroupID">           Identifier for the sales group.</param>
        /// <param name="quantity">               The quantity.</param>
        /// <param name="timestamp">              The timestamp Date/Time.</param>
        /// <param name="contextProfileName">     Name of the context profile.</param>
        /// <returns>The new subscription.</returns>
        protected virtual async Task<CEFActionResponse<ISubscriptionModel>> CreateSubscriptionInstanceForUserAsync(
            IProductSubscriptionType productSubscriptionType,
            decimal? fee,
            int? userID,
            int? accountID,
            int? invoiceID,
            int? salesOrderID,
            int? salesGroupID,
            decimal quantity,
            DateTime timestamp,
            string? contextProfileName)
        {
            var repeatType = productSubscriptionType.SubscriptionTypeRepeatType!.Slave;
            var billingPeriodsTotal = repeatType!.InitialBonusBillingPeriods
                + productSubscriptionType.SubscriptionTypeRepeatType.Slave!.RepeatableBillingPeriods ?? 0;
            var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>(contextProfileName);
            subscription.IsAutoRefill = repeatType.CustomKey != "OnDemand";
            subscription.Active = true;
            subscription.LastPaidDate
                = subscription.StartsOn
                = subscription.MemberSince
                = subscription.CreatedDate
                = timestamp;
            subscription.EndsOn = SubscriptionCalculateDate(
                timestamp,
                repeatType.CustomKey!,
                repeatType.RepeatableBillingPeriods);
            subscription.BillingPeriodsPaid = 1;
            subscription.BillingPeriodsTotal = billingPeriodsTotal;
            subscription.Fee = fee ?? 0m;
            subscription.Memo = $"Renewing {repeatType.DisplayName ?? repeatType.Name} Subscription for {billingPeriodsTotal} period(s)";
            // TODO: Make this a site-wide setting to either never auto-repeat, always auto-repeat, or allow it be
            // based on user's selection
            subscription.AutoRenew = repeatType.RepeatableBillingPeriods > 0;
            // TODO: Read sort order of all levels to see if this is the highest or not
            subscription.CanUpgrade = true;
            subscription.RepeatTypeID = repeatType.ID;
            subscription.SalesOrderID = salesOrderID;
            subscription.SalesGroupID = salesGroupID;
            subscription.StatusID = await Workflows.SubscriptionStatuses.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Current",
                    byName: "Current",
                    byDisplayName: "Current",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            subscription.TypeID = await Workflows.SubscriptionTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Subscription",
                    byName: "Subscription",
                    byDisplayName: "Subscription",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            subscription.UserID = userID;
            subscription.AccountID = accountID;
            // subscription.Quantity = (int)quantity;
            subscription.ProductSubscriptionTypeID = productSubscriptionType.ID;
            subscription.ID = (await Workflows.Subscriptions.CreateAsync(subscription, contextProfileName).ConfigureAwait(false)).Result;
            return subscription.WrapInPassingCEFARIfNotNull();
        }

        /// <summary>Clean expiration month.</summary>
        /// <param name="month">The month.</param>
        /// <returns>A string.</returns>
        private static string CleanExpirationMonth(string month)
        {
            return Months[month];
        }

        /// <summary>Clean expiration year.</summary>
        /// <param name="year">    The year.</param>
        /// <param name="needFour">True to need four.</param>
        /// <returns>A string.</returns>
        private static string CleanExpirationYear(string year, bool needFour = false)
        {
            return needFour
                ? year.Length < 4
                    ? $"20{year.Substring(year.Length - 2, 2)}"
                    : year.Substring(year.Length - 4, 4)
                : year.Substring(year.Length - 2, 2);
        }
    }
}
