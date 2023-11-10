// <copyright file="MockPaymentsProvider.Subscriptions.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the mock payment provider. subscriptions class</summary>
namespace Clarity.Ecommerce.Providers.Payments.Mock
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Text.RegularExpressions;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Payments;
    using Interfaces.Providers.Pricing;
    using JetBrains.Annotations;
    using Models;
    using Utilities;

    public partial class MockPaymentsProvider : ISubscriptionProviderBase
    {
        private static readonly Dictionary<string, int> WordToNumberLookup = new Dictionary<string, int>
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
        // down to here
        /// <inheritdoc/>
        public async Task<IPaymentResponse> CreateSubscriptionAsync(
            ISubscriptionPaymentModel model,
            bool paymentAlreadyConverted,
            string? contextProfileName)
        {
            var profileResponse = await CreateCustomerProfileAsync(model.Payment, model.Payment.BillingContact, contextProfileName).ConfigureAwait(false);
            if (!profileResponse.Approved)
            {
                return new PaymentResponse
                {
                    Amount = model.Amount,
                    Approved = false,
                    ResponseCode = profileResponse.ResponseCode,
                };
            }
            if (!TryParseToken(profileResponse.Token, out var customerRefNum, out var mitReceivedTransactionID))
            {
                return new PaymentResponse
                {
                    Amount = model.Amount,
                    Approved = false,
                    ResponseCode = "ERROR: Unable to utilize customerRefNum from wallet create transaction",
                };
            }
            var result = ToPaymentResponse(model.Amount);
            /*e.accountUpdaterEligibility = "";
e.amexTranAdvAddn1 = "";
e.amexTranAdvAddn2 = "";
e.amexTranAdvAddn3 = "";
e.amexTranAdvAddn4 = "";
e.applicationId = "";
e.applicationLabel = "";
e.attendedTerminal = "";
e.authenticationECIInd = "";
e.avsAddress1 = "";
e.avsAddress2 = "";
e.avsCity = "";
e.avsCountryCode = "";
e.avsDestAddress1 = "";
e.avsDestAddress2 = "";
e.avsDestCity = "";
e.avsDestCountryCode = "";
e.avsDestName = "";
e.avsDestPhoneNum = "";
e.avsDestPhoneType = "";
e.avsDestState = "";
e.avsDestZip = "";
e.avsName = "";
e.avsPhone = "";
e.avsPhoneType = "";
e.avsState = "";
e.avsZip = "";
e.axAEVV = "";
e.bmlCustomerAnnualIncome = "";
e.bmlCustomerBirthDate = "";
e.bmlCustomerCheckingAccount = "";
e.bmlCustomerEmail = "";
e.bmlCustomerIP = "";
e.bmlCustomerRegistrationDate = "";
e.bmlCustomerResidenceStatus = "";
e.bmlCustomerSavingsAccount = "";
e.bmlCustomerSSN = "";
e.bmlCustomerTypeFlag = "";
e.bmlItemCategory = "";
e.bmlMerchantPromotionalCode = "";
e.bmlPreapprovalInvitationNum = "";
e.bmlProductDeliveryType = "";
e.bmlShippingCost = "";
e.bmlTNCVersion = "";
e.cardBrand = "";
e.cardholderActivatedTerminal = "";
e.cardholderAuthenticateMethod = "";
e.cardholderPresent = "";
e.cardIndicators = "";
e.cardPresentInd = "";
e.ccAccountNum = "";
e.ccCardVerifyNum = "";
e.ccCardVerifyPresenceInd = "";
e.ccExp = "";
e.chipCardData = "";
e.comments = "";
e.communicationSupport = "";
e.complianceClass = "";
e.customerAni = "";
e.customerBrowserName = "";
e.customerEmail = "";
e.customerIpAddress = "";
e.customerName = "";
e.customerPhone = "";
e.dataEntrySource = "";
e.debitBillerReferenceNumber = "";
e.debitPinCashBack = "";
e.debitPinNumber = "";
e.debitPinSecurityControl = "";
e.deviceID = "";
e.deviceSecuritySupport = "";
e.digitalTokenCryptogram = "";
e.digitalWalletType = "";
e.discountAmt = "";
e.dpanInd = "";
e.dwIncentiveInd = "";
e.dwSLI = "";
e.dwWalletID = "";
e.ecpActionCode = "";
e.ecpAuthMethod = "";
e.ecpBankAcctType = "";
e.ecpCheckDDA = "";
e.ecpCheckRT = "";
e.ecpCheckSerialNumber = "";
e.ecpDelvMethod = "";
e.ecpImageReferenceNumber = "";
e.ecpReDepositFreq = "";
e.ecpReDepositInd = "";
e.ecpSameDayInd = "";
e.ecpTerminalCity = "";
e.ecpTerminalState = "";
e.emailAddressSubtype = "";
e.emvSupport = "";
e.encryptedMagStripeTrack2 = "";
e.encryptedPan = "";
e.encryptedPanHash = "";
e.encryptedPanKey = "";
e.encryptedPanMethod = "";
e.encryptedPanPublicKeyFingerPrint = "";
e.encryptionId = "";
e.encryptionInd = "";
e.encryptionMethod = "";
e.euddBankBranchCode = "";
e.euddBankSortCode = "";
e.euddBIC = "";
e.euddCountryCode = "";
e.euddIBAN = "";
e.euddMandateID = "";
e.euddMandateSignatureDate = "";
e.euddMandateType = "";
e.euddRibCode = "";
e.ewsAddressLine1 = "";
e.ewsAddressLine2 = "";
e.ewsBusinessName = "";
e.ewsCheckSerialNumber = "";
e.ewsCity = "";
e.ewsDOB = "";
e.ewsFirstName = "";
e.ewsIDNumber = "";
e.ewsIDState = "";
e.ewsIDType = "";
e.ewsLastName = "";
e.ewsMiddleName = "";
e.ewsPhoneNumber = "";
e.ewsPhoneType = "";
e.ewsSSNTIN = "";
e.ewsState = "";
e.ewsZip = "";
e.fxExchangeRate = "";
e.fxOptOutInd = "";
e.fxPresentmentCurrency = "";
e.fxRateHandlingInd = "";
e.fxRateID = "";
e.fxSettlementCurrency = "";
e.iacDefault = "";
e.iacDenial = "";
e.iacOnline = "";
e.industrySupport = "";
e.keySerialNumber = "";
e.latitudeLongitude = "";
e.localDateTime = "";
e.magStripeTrack1 = "";
e.magStripeTrack2 = "";
e.mbDeferredBillDate = "";
e.mbMicroPaymentMaxBillingDays = "";
e.mbMicroPaymentMaxDollarValue = "";
e.mbMicroPaymentMaxTransactions = "";
e.mbOrderIdGenerationMethod = "";
e.mbRecurringEndDate = "";
e.mbRecurringFrequency = "";
e.mbRecurringMaxBillings = "";
e.mbRecurringNoEndDateFlag = "";
e.mbRecurringStartDate = "";
e.mbType = "";
e.mcSecureCodeAAV = "";
e.mobileDeviceType = "";
e.orderID = "";
e.panSequenceNumber = "";
e.partialAuthInd = "";
e.paymentActionInd = "";
e.paymentInd = "";
e.pCard3AltTaxAmt = "";
e.pCard3AltTaxInd = "";
e.pCard3DestCountryCd = "";
e.pCard3DiscAmt = "";
e.pCard3DutyAmt = "";
e.pCard3FreightAmt = "";
e.pCard3LineItemCount = "";
e.pCard3ShipFromZip = "";
e.pCard3VATtaxAmt = "";
e.pCard3VATtaxRate = "";
e.pCardCustomerVatRegNumber = "";
e.pCardDestAddress = "";
e.pCardDestAddress2 = "";
e.pCardDestCity = "";
e.pCardDestName = "";
e.pCardDestStateCd = "";
e.pCardDestZip = "";
e.pCardDtlTaxAmount1 = "";
e.pCardDtlTaxAmount1Ind = "";
e.pCardDtlTaxAmount2 = "";
e.pCardDtlTaxAmount2Ind = "";
e.pCardLocalTaxAmount = "";
e.pCardLocalTaxRate = "";
e.pCardMerchantVatRegNumber = "";
e.pCardNationalTax = "";
e.pCardNationalTaxRate = "";
e.pCardOrderID = "";
e.pCardPstTaxRegNumber = "";
e.pCardRequestorName = "";
e.pCardTotalTaxAmount = "";
e.peripheralSupport = "";
e.pin = "";
e.pinKeySerialNumber = "";
e.pinlessDebitMerchantUrl = "";
e.pinlessDebitTxnType = "";
e.politicalTimeZone = "";
e.posEntryMode = "";
e.prBirthDate = "";
e.priorAuthCd = "";
e.prLastName = "";
e.prMaskedAccountNumber = "";
e.prPartialPostalCode = "";
e.pymtBrandProgramCode = "";
e.readerSerialNumber = "";
e.recurringInd = "";
e.retailTransInfo = "";
e.retryTrace = "";
e.rtauOptOutInd = "";
e.shippingMethod = "";
e.shippingRef = "";
e.softDescMercCity = "";
e.softDescMercEmail = "";
e.softDescMercName = "";
e.softDescMercPhone = "";
e.softDescMercURL = "";
e.softDescProdDesc = "";
e.softwareID = "";
e.switchSoloCardStartDate = "";
e.switchSoloIssueNum = "";
e.tacDefault = "";
e.tacDenial = "";
e.tacOnline = "";
e.taxAmount = "";
e.taxInd = "";
e.terminalEntry = "";
e.terminalLaneId = "";
e.terminalLocation = "";
e.tipAmt = "";
e.tokenRequestorID = "";
e.tsi = "";
e.tvr = "";
e.txnSurchargeAmt = "";
e.txRefNum = "";
e.useCustomerRefNum = "";
e.useStoredAAVInd = "";
e.vendorID = "";
e.verifyByVisaCAVV = "";
e.verifyByVisaXID = "";*/
            return result;
        }

        /// <inheritdoc/>
        public Task<IPaymentResponse> UpdateSubscriptionAsync(ISubscriptionPaymentModel model, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentResponse> GetSubscriptionAsync(ISubscriptionPaymentModel model, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public Task<IPaymentResponse> CancelSubscriptionAsync(ISubscriptionPaymentModel model, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse> ImplementProductSubscriptionFromOrderItemAsync(
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
            int? productTypeId = null;
            if (salesOrderItem.ProductID != null)
            {
                var product = await Workflows.Products.GetAsync(salesOrderItem.ProductID ?? 0, contextProfileName).ConfigureAwait(false);
                productTypeId = product.TypeID;
            }
            if ((productTypeId != null && productTypeId == SubscriptionProductTypeID)
                || (productTypeId != null && productTypeId == SubscriptionVariantProductTypeID))
            {
                return await OnSubscriptionProductSalesOrderItemCreated(
                    salesOrderItem.ProductID.Value,
                    salesOrderItem.UserID ?? salesOrderUserID,
                    salesOrderAccountID,
                    invoiceID,
                    salesOrderID,
                    salesGroupID,
                    timestamp,
                    pricingFactoryContext,
                    salesOrderItem.UnitSoldPrice ?? salesOrderItem.UnitCorePrice,
                    contextProfileName).ConfigureAwait(false);
            }
            if (salesOrderItem.ProductTypeID == KitProductTypeID
                || salesOrderItem.ProductTypeID == VariantKitProductTypeID)
            {
                // NOTE: BOM Down is recursive, so we're getting all unique product IDs all the way down the
                // chain, not just one level
                var components1 = await Workflows.ProductKits.KitComponentBOMDownAsync(
                    salesOrderItem.ProductID.Value, contextProfileName).ConfigureAwait(false);
                var components2 = components1
                    .Where(x => Contract.CheckValidID(x.ID)
                             && (x.TypeID == SubscriptionProductTypeID
                                 || x.TypeID == SubscriptionVariantProductTypeID));
                return await CEFAR.AggregateAsync(
                    components2,
                    async x => await OnSubscriptionProductSalesOrderItemCreated(
                        // ReSharper disable once PossibleInvalidOperationException (This is verified in the Where statement)
                        x.ID,
                        salesOrderItem.UserID ?? salesOrderUserID,
                        salesOrderAccountID,
                        invoiceID,
                        salesOrderID,
                        salesGroupID,
                        timestamp,
                        pricingFactoryContext,
                        salesOrderItem.UnitSoldPrice ?? salesOrderItem.UnitCorePrice,
                        contextProfileName).ConfigureAwait(false)).ConfigureAwait(false);
            }
            return CEFAR.FailingCEFAR(
                "No action taken as product is not a subscription or kit that contains a subscription");
        }

        /// <summary>Subscription calculate date.</summary>
        /// <param name="fromDate">     from date.</param>
        /// <param name="repeatTypeKey">The repeat type key.</param>
        /// <param name="repeatCount">  Number of repeats.</param>
        /// <returns>A DateTime.</returns>
        [Pure]
        internal static new DateTime SubscriptionCalculateDate(DateTime fromDate, string repeatTypeKey, int? repeatCount)
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
        protected override async Task<CEFActionResponse> OnSubscriptionProductSalesOrderItemCreated(
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
            var subscriptionProduct = await context.Products
                .AsNoTracking()
                .Include(x => x.ProductSubscriptionTypes)
                .FilterByActive(true)
                .FilterByID(productID)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            // filter subscription types after getting from DB since there will never be that many
            var activeSubscriptionTypes = subscriptionProduct.ProductSubscriptionTypes.Where(y => y.Active && y.Slave.Active && y.SubscriptionTypeRepeatType.Active);
            var price = fee
                    ?? (await RegistryLoaderWrapper.GetInstance<IPricingFactory>().CalculatePriceAsync(
                                subscriptionProduct.ID,
                                null,
                                pricingFactoryContext,
                                contextProfileName)
                            .ConfigureAwait(false))
                        .BasePrice;
            if (subscriptionProduct == null)
            {
                return CEFAR.FailingCEFAR<IEnumerable<ISubscriptionModel>>("Could not locate product");
            }
            if (Contract.CheckEmpty(subscriptionProduct.ProductSubscriptionTypes))
            {
                return CEFAR.FailingCEFAR<IEnumerable<ISubscriptionModel>>(
                    "Product does not contain subscription definitions");
            }
            var invoice = context.SalesOrders.AsNoTracking().FilterByActive(true).FilterByID(salesOrderID).FirstOrDefault();
            var quantity = invoice?.SalesItems.FirstOrDefault(p => p.ProductID == productID)?.Quantity;
            return await CEFAR.AggregateAsync(
                    activeSubscriptionTypes,
                    x => CreateSubscriptionInstanceForUserAsync(
                        x,
                        price,
                        userID,
                        accountID,
                        invoiceID,
                        salesOrderID,
                        salesGroupID,
                        quantity ?? 0,
                        timestamp,
                        contextProfileName))
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
            ////        contextProfileName))
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
        protected override async Task<CEFActionResponse<ISubscriptionModel>> CreateSubscriptionInstanceForUserAsync(
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
            var repeatType = productSubscriptionType.SubscriptionTypeRepeatType.Slave;
            var billingPeriodsTotal = repeatType.InitialBonusBillingPeriods
                + productSubscriptionType.SubscriptionTypeRepeatType.Slave.RepeatableBillingPeriods ?? 0;
            var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>();
            if (repeatType.CustomKey == "OnDemand")
            {
                subscription.IsAutoRefill = false;
            }
            else
            {
                subscription.IsAutoRefill = true;
            }
            subscription.Active = true;
            subscription.LastPaidDate
                = subscription.StartsOn
                = subscription.MemberSince
                = subscription.CreatedDate
                = timestamp;
            subscription.EndsOn = SubscriptionCalculateDate(
                timestamp, repeatType.CustomKey, repeatType.RepeatableBillingPeriods);
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
            subscription.SalesInvoiceID = invoiceID;
            // subscription.SalesOrderID = salesOrderID; - from AIR (no SalesOrderID property for Subscription in RX1)
            subscription.SalesGroupID = salesGroupID;
            subscription.StatusID = await Workflows.SubscriptionStatuses.ResolveWithAutoGenerateToIDAsync(
                null, "Current", "Current", "Current", null, contextProfileName).ConfigureAwait(false);
            subscription.TypeID = await Workflows.SubscriptionTypes.ResolveWithAutoGenerateToIDAsync(
                null, "Subscription", "Subscription", "Subscription", null, contextProfileName).ConfigureAwait(false);
            subscription.UserID = userID;
            subscription.AccountID = accountID;
            // subscription.Quantity = (int)quantity;
            subscription.ProductSubscriptionTypeID = productSubscriptionType.ID;
            subscription = (ISubscriptionModel)Workflows.Subscriptions.CreateAsync(subscription, contextProfileName);
            return subscription.WrapInPassingCEFARIfNotNull();
        }

        /// <summary>Cron expression parsing.</summary>
        /// <returns>A string.</returns>
        // ReSharper disable once UnusedMember.Local
        private static string CronExpressionParsing()
        {
            // Paymentech doesn't use the first 2 of 5 points in their cron expr values, only send the last 3 of 5
            const string CronPrefix = "0 0 ";
            var cronInfo = CommonUtils.Cron.CronParser.Instance.Parse(CronPrefix + "1 * ?");
            CommonUtils.Cron.CronFluentBuilder.Build().SetDaysOfMonth("1", "15");
            // ReSharper disable once UnusedVariable
            var months = cronInfo.Months;
            return string.Empty;
        }
    }
}
