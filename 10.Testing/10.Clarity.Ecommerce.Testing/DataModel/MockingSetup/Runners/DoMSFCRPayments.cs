// <copyright file="DoMockingSetupForContextRunnerPayments.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the do mocking setup for context runner payments class</summary>
namespace Clarity.Ecommerce.Testing
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Moq;

    public partial class MockingSetup
    {
        private async Task DoMockingSetupForContextRunnerPaymentsAsync(Mock<IClarityEcommerceEntities> mockContext)
        {
            #region Apply Data and set up IQueryable: Memberships
            if (DoAll || DoPayments || DoMembershipTable)
            {
                RawMemberships = new()
                {
                    await CreateADummyMembershipAsync(id: 1, key: "SiteMembership", name: "Site Membership", displayName: "Site Membership", desc: "desc", sortOrder: 0).ConfigureAwait(false),
                    await CreateADummyMembershipAsync(id: 2, key: "SiteMembership2", name: "Site Membership 2", displayName: "Site Membership 2", desc: "desc", sortOrder: 1).ConfigureAwait(false),
                };
                await InitializeMockSetMembershipsAsync(mockContext, RawMemberships).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Membership Ad Zone Accesses
            if (DoAll || DoPayments || DoMembershipAdZoneAccessTable)
            {
                RawMembershipAdZoneAccesses = new()
                {
                    await CreateADummyMembershipAdZoneAccessAsync(id: 1, key: "SiteMembership|AdZoneAccess").ConfigureAwait(false),
                    await CreateADummyMembershipAdZoneAccessAsync(id: 2, key: "SiteMembership2|AdZoneAccess", masterID: 2).ConfigureAwait(false),
                };
                await InitializeMockSetMembershipAdZoneAccessesAsync(mockContext, RawMembershipAdZoneAccesses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Membership Ad Zone Access By Levels
            if (DoAll || DoPayments || DoMembershipAdZoneAccessByLevelTable)
            {
                RawMembershipAdZoneAccessByLevels = new()
                {
                    await CreateADummyMembershipAdZoneAccessByLevelAsync(id: 1, key: "KEY-1",              masterID: 1, subscriberCountThreshold: 000, uniqueAdLimit: 000).ConfigureAwait(false),
                    await CreateADummyMembershipAdZoneAccessByLevelAsync(id: 2, key: "BronzeZoneAccess",   masterID: 1, subscriberCountThreshold: 010, uniqueAdLimit: 010).ConfigureAwait(false),
                    await CreateADummyMembershipAdZoneAccessByLevelAsync(id: 3, key: "GoldZoneAccess",     masterID: 1, slaveID: 2, subscriberCountThreshold: 100, uniqueAdLimit: 100).ConfigureAwait(false),
                    await CreateADummyMembershipAdZoneAccessByLevelAsync(id: 4, key: "PlatinumZoneAccess", slaveID: 3, subscriberCountThreshold: 200, uniqueAdLimit: 200).ConfigureAwait(false),
                    await CreateADummyMembershipAdZoneAccessByLevelAsync(id: 5, key: "BronzeZoneAccess2",  masterID: 2, slaveID: 5, subscriberCountThreshold: 000, uniqueAdLimit: 000).ConfigureAwait(false),
                };
                await InitializeMockSetMembershipAdZoneAccessByLevelsAsync(mockContext, RawMembershipAdZoneAccessByLevels).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Membership Levels
            if (DoAll || DoPayments || DoMembershipLevelTable)
            {
                RawMembershipLevels = new()
                {
                    await CreateADummyMembershipLevelAsync(id: 1, key: "SiteMembership|Bronze",    membershipID: 1, name: "Bronze",      displayName: "Bronze",      desc: "desc", sortOrder: 10, rolesApplied: "Bronze Site Membership").ConfigureAwait(false),
                    await CreateADummyMembershipLevelAsync(id: 2, key: "SiteMembership|Gold",      membershipID: 1, name: "Gold",        displayName: "Gold",        desc: "desc", sortOrder: 20, rolesApplied: "Gold Site Membership").ConfigureAwait(false),
                    await CreateADummyMembershipLevelAsync(id: 3, key: "SiteMembership|Platinum",  membershipID: 1, name: "Platinum",    displayName: "Platinum",    desc: "desc", sortOrder: 30, rolesApplied: "Platinum Site Membership").ConfigureAwait(false),
                    await CreateADummyMembershipLevelAsync(id: 4, key: "SiteMembership|GoldNoAds", name: "Gold No Ads", displayName: "Gold No Ads", desc: "desc", sortOrder: 40, rolesApplied: "Gold Site Membership").ConfigureAwait(false),
                    await CreateADummyMembershipLevelAsync(id: 5, key: "SiteMembership2|Bronze",   membershipID: 2, name: "Bronze 2",     displayName: "Bronze 2",   desc: "desc", sortOrder: 10, rolesApplied: "Bronze Site Membership").ConfigureAwait(false),
                };
                await InitializeMockSetMembershipLevelsAsync(mockContext, RawMembershipLevels).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Membership Repeat Types
            if (DoAll || DoPayments || DoMembershipRepeatTypeTable)
            {
                RawMembershipRepeatTypes = new()
                {
                    await CreateADummyMembershipRepeatTypeAsync(id: 1, key: "SiteMembership|Monthly",       masterID: 1).ConfigureAwait(false),
                    await CreateADummyMembershipRepeatTypeAsync(id: 2, key: "SiteMembership|Quarterly",     masterID: 1, slaveID: 2).ConfigureAwait(false),
                    await CreateADummyMembershipRepeatTypeAsync(id: 3, key: "SiteMembership|Semi-Annually", slaveID: 3).ConfigureAwait(false),
                    await CreateADummyMembershipRepeatTypeAsync(id: 4, key: "SiteMembership|Annually",      masterID: 1, slaveID: 4).ConfigureAwait(false),
                };
                await InitializeMockSetMembershipRepeatTypesAsync(mockContext, RawMembershipRepeatTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Payments
            if (DoAll || DoPayments || DoPaymentTable)
            {
                RawPayments = new()
                {
                    await CreateADummyPaymentAsync(id: 1, key: "PAYMENT-1", amount: 367.09m, authDate: CreatedDate, authCode: "authCode", last4CardDigits: "1111", authorized: true, cVV: "123", cardMask: "X", expirationYear: DateTime.Now.AddYears(1).Year, paymentData: "pmtData", externalPaymentID: "extPmtID", referenceNo: "refNo", response: "success!", transactionNumber: "transNo", expirationMonth: 1, externalCustomerID: "1234", receivedDate: CreatedDate, statusDate: CreatedDate).ConfigureAwait(false),
                };
                await InitializeMockSetPaymentsAsync(mockContext, RawPayments).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Payment Methods
            if (DoAll || DoPayments || DoPaymentMethodTable)
            {
                var index = 0;
                RawPaymentMethods = new()
                {
                    await CreateADummyPaymentMethodAsync(id: ++index, key: "Credit Card", name: "Credit Card", desc: "desc").ConfigureAwait(false),
                    await CreateADummyPaymentMethodAsync(id: ++index, key: "Check by Mail", name: "Check by Mail", desc: "desc").ConfigureAwait(false),
                    await CreateADummyPaymentMethodAsync(id: ++index, key: "Purchase Order Number", name: "Purchase Order Number", desc: "desc").ConfigureAwait(false),
                    await CreateADummyPaymentMethodAsync(id: ++index, key: "Wire Transfer", name: "Wire Transfer", desc: "desc").ConfigureAwait(false),
                    await CreateADummyPaymentMethodAsync(id: ++index, key: "Online Payment Record", name: "Online Payment Record", desc: "desc").ConfigureAwait(false),
                    await CreateADummyPaymentMethodAsync(id: ++index, key: "Store Credit / Gift Certificate", name: "Store Credit / Gift Certificate", desc: "desc").ConfigureAwait(false),
                };
                await InitializeMockSetPaymentMethodsAsync(mockContext, RawPaymentMethods).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Payment Statuses
            if (DoAll || DoPayments || DoPaymentStatusTable)
            {
                var index = 0;
                RawPaymentStatuses = new()
                {
                    await CreateADummyPaymentStatusAsync(id: ++index, key: "No Payment Received", name: "No Payment Received", desc: "desc", displayName: "No Payment Received").ConfigureAwait(false),
                    await CreateADummyPaymentStatusAsync(id: ++index, key: "Payment Received", name: "Payment Received", desc: "desc", displayName: "Payment Received").ConfigureAwait(false),
                };
                await InitializeMockSetPaymentStatusesAsync(mockContext, RawPaymentStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Payment Types
            if (DoAll || DoPayments || DoPaymentTypeTable)
            {
                var index = 0;
                RawPaymentTypes = new()
                {
                    await CreateADummyPaymentTypeAsync(id: ++index, key: "Mastercard", name: "Mastercard", desc: "desc", displayName: "Mastercard").ConfigureAwait(false),
                    await CreateADummyPaymentTypeAsync(id: ++index, key: "VISA", name: "VISA", desc: "desc", displayName: "VISA").ConfigureAwait(false),
                    await CreateADummyPaymentTypeAsync(id: ++index, key: "American Express", name: "American Express", desc: "desc", displayName: "American Express").ConfigureAwait(false),
                    await CreateADummyPaymentTypeAsync(id: ++index, key: "Discover", name: "Discover", desc: "desc", displayName: "Discover").ConfigureAwait(false),
                    await CreateADummyPaymentTypeAsync(id: ++index, key: "Other", name: "Other", desc: "desc", displayName: "Other").ConfigureAwait(false),
                };
                await InitializeMockSetPaymentTypesAsync(mockContext, RawPaymentTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Invoice Payments
            if (DoAll || DoPayments || DoSalesInvoicePaymentTable)
            {
                var index = 0;
                RawSalesInvoicePayments = new()
                {
                    await CreateADummySalesInvoicePaymentAsync(id: ++index, key: "SI-PMT-A", masterID: 30000).ConfigureAwait(false),
                };
                await InitializeMockSetSalesInvoicePaymentsAsync(mockContext, RawSalesInvoicePayments).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Order Payments
            if (DoAll || DoPayments || DoSalesOrderPaymentTable)
            {
                var index = 0;
                RawSalesOrderPayments = new()
                {
                    await CreateADummySalesOrderPaymentAsync(id: ++index, key: "SO-PMT-A", masterID: 30000).ConfigureAwait(false),
                };
                await InitializeMockSetSalesOrderPaymentsAsync(mockContext, RawSalesOrderPayments).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Sales Return Payments
            if (DoAll || DoPayments || DoSalesReturnPaymentTable)
            {
                var index = 0;
                RawSalesReturnPayments = new()
                {
                    await CreateADummySalesReturnPaymentAsync(id: ++index, key: "SR-PMT-A", masterID: 30000).ConfigureAwait(false),
                };
                await InitializeMockSetSalesReturnPaymentsAsync(mockContext, RawSalesReturnPayments).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Subscriptions
            if (DoAll || DoPayments || DoSubscriptionTable)
            {
                RawSubscriptions = new()
                {
                    await CreateADummySubscriptionAsync(
                        id: 01, key: "SUB-A", name: "Gold Membership Monthly", desc: "desc",
                        accountID: 1, typeID: 3,
                        autoRenew: true, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate, memberSince: CreatedDate, startsOn: CreatedDate, endsOn: CreatedDate.AddYears(1)).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 02, key: "SUB-B", name: "Bronze Membership Monthly", desc: "desc",
                        accountID: 1, typeID: 3,
                        autoRenew: true, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate, memberSince: CreatedDate, startsOn: CreatedDate, endsOn: CreatedDate.AddYears(1)).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 03, key: "SUB-C", name: "Bronze Membership Monthly (and already ended)", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3,
                        autoRenew: true, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2), endsOn: CreatedDate.AddYears(-1)).ConfigureAwait(false),
                    //
                    await CreateADummySubscriptionAsync(
                        id: 04, key: "Ended", name: "Ended Membership", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2), endsOn: CreatedDate.AddYears(-1)).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 05, key: "EndsToday", name: "Ends Today", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2), endsOn: DateTime.Today).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 06, key: "NullEndDate", name: "Null End Date", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2)).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 07, key: "MaxEndDate", name: "Max End Date", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2), endsOn: DateTime.MaxValue).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 08, key: "MinEndDate", name: "Max End Date", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2), endsOn: DateTime.MinValue).ConfigureAwait(false),
                    await CreateADummySubscriptionAsync(
                        id: 09, key: "EndDateInBlackoutPeriod", name: "Max End Date", desc: "desc",
                        accountID: 1, statusID: 2, typeID: 3, billingPeriodsPaid: 12, billingPeriodsTotal: 12, canUpgrade: true, fee: 257.00m, memo: "memo",
                        lastPaidDate: CreatedDate.AddYears(-2), memberSince: CreatedDate.AddYears(-2), startsOn: CreatedDate.AddYears(-2), endsOn: DateTime.Today.AddDays(/*UpgradePeriodBlackout*/ 30 * -1 - 1)).ConfigureAwait(false),
                };
                await InitializeMockSetSubscriptionsAsync(mockContext, RawSubscriptions).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Subscription Histories
            if (DoAll || DoPayments || DoSubscriptionHistoryTable)
            {
                RawSubscriptionHistories = new()
                {
                    await CreateADummySubscriptionHistoryAsync(id: 1, key: "SUBSCRIPTION-HISTORY-1", paymentDate: CreatedDate, paymentSuccess: true).ConfigureAwait(false),
                };
                await InitializeMockSetSubscriptionHistoriesAsync(mockContext, RawSubscriptionHistories).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Subscription Statuses
            if (DoAll || DoPayments || DoSubscriptionStatusTable)
            {
                var index = 0;
                RawSubscriptionStatuses = new()
                {
                    await CreateADummySubscriptionStatusAsync(id: ++index, key: "Current", name: "Current", desc: "desc", displayName: "Current").ConfigureAwait(false),
                    await CreateADummySubscriptionStatusAsync(id: ++index, key: "Expired", name: "Expired", desc: "desc", displayName: "Expired").ConfigureAwait(false),
                    await CreateADummySubscriptionStatusAsync(id: ++index, key: "Suspended", name: "Suspended", desc: "desc", displayName: "Suspended").ConfigureAwait(false),
                    await CreateADummySubscriptionStatusAsync(id: ++index, key: "Cancelled", name: "Cancelled", desc: "desc", displayName: "Cancelled").ConfigureAwait(false),
                };
                await InitializeMockSetSubscriptionStatusesAsync(mockContext, RawSubscriptionStatuses).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Subscription Type Repeat Types
            if (DoAll || DoPayments || DoSubscriptionTypeRepeatTypeTable)
            {
                RawSubscriptionTypeRepeatTypes = new()
                {
                    await CreateADummySubscriptionTypeRepeatTypeAsync(id: 1, key: "MembershipMonthly", masterID: 1, slaveID: 2).ConfigureAwait(false),
                    await CreateADummySubscriptionTypeRepeatTypeAsync(id: 2, key: "SubscriptionQuarterly", masterID: 2, slaveID: 1).ConfigureAwait(false),
                    await CreateADummySubscriptionTypeRepeatTypeAsync(id: 3, key: "MembershipSemi-Annually", masterID: 2, slaveID: 2).ConfigureAwait(false),
                    await CreateADummySubscriptionTypeRepeatTypeAsync(id: 4, key: "Annually", masterID: 1, slaveID: 1).ConfigureAwait(false),
                };
                await InitializeMockSetSubscriptionTypeRepeatTypesAsync(mockContext, RawSubscriptionTypeRepeatTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Subscription Types
            if (DoAll || DoPayments || DoSubscriptionTypeTable)
            {
                RawSubscriptionTypes = new()
                {
                    await CreateADummySubscriptionTypeAsync(id: 1, key: "BronzeMembershipMonthly",   name: "BronzeMembershipMonthly",   desc: "desc", displayName: "Bronze Membership Monthly").ConfigureAwait(false),
                    await CreateADummySubscriptionTypeAsync(id: 2, key: "BronzeMembershipYearly",    name: "BronzeMembershipYearly",    desc: "desc", displayName: "Bronze Membership Yearly").ConfigureAwait(false),
                    await CreateADummySubscriptionTypeAsync(id: 3, key: "GoldMembershipMonthly",     name: "GoldMembershipMonthly",     desc: "desc", displayName: "Gold Membership Monthly").ConfigureAwait(false),
                    await CreateADummySubscriptionTypeAsync(id: 4, key: "GoldMembershipYearly",      name: "GoldMembershipYearly",      desc: "desc", displayName: "Gold Membership Yearly").ConfigureAwait(false),
                    await CreateADummySubscriptionTypeAsync(id: 5, key: "PlatinumMembershipMonthly", name: "PlatinumMembershipMonthly", desc: "desc", displayName: "Platinum Membership Monthly").ConfigureAwait(false),
                    await CreateADummySubscriptionTypeAsync(id: 6, key: "PlatinumMembershipYearly",  name: "PlatinumMembershipYearly",  desc: "desc", displayName: "Platinum Membership Yearly").ConfigureAwait(false),
                };
                await InitializeMockSetSubscriptionTypesAsync(mockContext, RawSubscriptionTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Repeat Types
            if (DoAll || DoPayments || DoRepeatTypeTable)
            {
                RawRepeatTypes = new()
                {
                    await CreateADummyRepeatTypeAsync(id: 1, key: "Monthly", name: "Monthly", desc: "Monthly", displayName: "Monthly", sortOrder: 100, initialBonusBillingPeriods: 0, repeatableBillingPeriods: 1).ConfigureAwait(false),
                    await CreateADummyRepeatTypeAsync(id: 2, key: "Quarterly", name: "Quarterly", desc: "Quarterly", displayName: "Quarterly", sortOrder: 200, initialBonusBillingPeriods: 0, repeatableBillingPeriods: 3).ConfigureAwait(false),
                    await CreateADummyRepeatTypeAsync(id: 3, key: "Semi-Annually", name: "Semi-Annually", desc: "Semi-Annually", displayName: "Semi-Annually", sortOrder: 300, initialBonusBillingPeriods: 0, repeatableBillingPeriods: 6).ConfigureAwait(false),
                    await CreateADummyRepeatTypeAsync(id: 4, key: "Annually", name: "Annually", desc: "Annually", displayName: "Annually", sortOrder: 400, initialBonusBillingPeriods: 0, repeatableBillingPeriods: 12).ConfigureAwait(false),
                };
                await InitializeMockSetRepeatTypesAsync(mockContext, RawRepeatTypes).ConfigureAwait(false);
            }
            #endregion
            #region Apply Data and set up IQueryable: Wallets
            if (DoAll || DoPayments || DoWalletTable)
            {
                var index = 0;
                RawWallets = new()
                {
                    await CreateADummyWalletAsync(++index, key: "WALLET-1-CC", name: "My VISA", desc: "desc", cardHolderName: "My Name", cardType: "VISA", creditCardNumber: "4111111111111111", expirationMonth: 1, expirationYear: DateTime.Now.AddYears(1).Year, token: "some-token").ConfigureAwait(false),
                    await CreateADummyWalletAsync(++index, key: "WALLET-2-EC", name: "My Checking", desc: "desc", cardHolderName: "My Name", cardType: "Checking", token: "some-token", routingNumber: "123456789", accountNumber: "01234578", bankName: "My Bank").ConfigureAwait(false),
                };
                await InitializeMockSetWalletsAsync(mockContext, RawWallets).ConfigureAwait(false);
            }
            #endregion
        }
    }
}
