// <copyright file="MembershipProviderBase.Create.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership provider base class (Create part)</summary>
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Providers.Memberships;
    using Interfaces.Providers.Pricing;
    using Models;
    using Utilities;

    /// <summary>A membership provider base.</summary>
    /// <seealso cref="ProviderBase"/>
    /// <seealso cref="IMembershipsProviderBase"/>
    public abstract partial class MembershipProviderBase
    {
        /// <summary>Gets or sets the subscription type identifier of membership.</summary>
        /// <value>The subscription type identifier of membership.</value>
        protected static int SubscriptionTypeIDOfMembership { get; set; }

        /// <summary>Gets or sets the subscription status identifier of current.</summary>
        /// <value>The subscription status identifier of current.</value>
        protected static int SubscriptionStatusIDOfCurrent { get; set; }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ImplementProductMembershipFromOrderItemAsync(
            int? salesOrderUserID,
            int? salesOrderAccountID,
            ISalesItemBaseModel salesOrderItem,
            IPricingFactoryContextModel pricingFactoryContext,
            int? invoiceID,
            DateTime timestamp,
            string? contextProfileName)
        {
            if (!Contract.CheckValidID(salesOrderItem?.ProductID))
            {
                return CEFAR.FailingCEFAR("No Product to read an ID from");
            }
            // Check if upgrade
            if (salesOrderItem!.SerializableAttributes?.ContainsKey("upgrade_membership") == true)
            {
                if (!int.TryParse(salesOrderItem.SerializableAttributes["upgrade_membership"].Value, out var oldID))
                {
                    return CEFAR.FailingCEFAR("Could not read old subscription ID to mark for upgrade.");
                }
                var previousSubscription = await Workflows.Subscriptions.GetAsync(oldID, contextProfileName).ConfigureAwait(false);
                if (previousSubscription == null)
                {
                    return CEFAR.FailingCEFAR("Must supply the identifier of an existing subscription to upgrade");
                }
                // Mark previous membership as upgraded and update it
                previousSubscription.StatusID = 0;
                previousSubscription.Status = null;
                previousSubscription.StatusKey = "Upgraded";
                previousSubscription.StatusName = previousSubscription.StatusDisplayName = null;
                previousSubscription.EndsOn = timestamp;
                await Workflows.Subscriptions.UpdateAsync(previousSubscription, contextProfileName).ConfigureAwait(false);
                // Create new membership
                return await OnMembershipProductSalesOrderItemCreatedAsync(
                        productID: salesOrderItem.ProductID!.Value,
                        userID: salesOrderItem.UserID ?? salesOrderUserID,
                        accountID: salesOrderAccountID,
                        invoiceID: invoiceID,
                        timestamp: timestamp,
                        pricingFactoryContext: pricingFactoryContext,
                        fee: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(ProductTypeIDOfMembership))
            {
                ProductTypeIDOfMembership = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "MEMBERSHIP",
                        byName: "Membership",
                        byDisplayName: "Membership",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(ProductTypeIDOfKit))
            {
                ProductTypeIDOfKit = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "KIT",
                        byName: "Kit",
                        byDisplayName: "Kit",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckInvalidID(ProductTypeIDOfVariantKit))
            {
                ProductTypeIDOfVariantKit = await Workflows.ProductTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "VARIANT-KIT",
                        byName: "Variant Kit",
                        byDisplayName: "Variant Kit",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (salesOrderItem.ProductTypeID == ProductTypeIDOfMembership)
            {
                return await OnMembershipProductSalesOrderItemCreatedAsync(
                        productID: salesOrderItem.ProductID!.Value,
                        userID: salesOrderItem.UserID ?? salesOrderUserID,
                        accountID: salesOrderAccountID,
                        invoiceID: invoiceID,
                        timestamp: timestamp,
                        pricingFactoryContext: pricingFactoryContext,
                        fee: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            if (salesOrderItem.ProductTypeID == ProductTypeIDOfKit
                || salesOrderItem.ProductTypeID == ProductTypeIDOfVariantKit)
            {
                // NOTE: BOM Down is recursive, so we're getting all unique product IDs all the way down the
                // chain, not just one level
                return await CEFAR.AggregateAsync(
                    (await Workflows.ProductKits.KitComponentBOMDownAsync(salesOrderItem.ProductID!.Value, contextProfileName).ConfigureAwait(false))
                        .Where(x => x.TypeID == ProductTypeIDOfMembership && Contract.CheckValidID(x.ID)),
                    x => OnMembershipProductSalesOrderItemCreatedAsync(
                        productID: x.ID,
                        userID: salesOrderItem.UserID ?? salesOrderUserID,
                        accountID: salesOrderAccountID,
                        invoiceID: invoiceID,
                        timestamp: timestamp,
                        pricingFactoryContext: pricingFactoryContext,
                        fee: null,
                        contextProfileName: contextProfileName))
                    .ConfigureAwait(false);
            }
            return CEFAR.FailingCEFAR(
                "No action taken as product is not a membership or kit that contains a membership");
        }

        /// <summary>Definitions to ad zone access model.</summary>
        /// <param name="subscription">          The subscription.</param>
        /// <param name="membershipAdZoneAccess">The membership ad zone access.</param>
        /// <returns>An AdZoneAccessModel.</returns>
        private static IAdZoneAccessModel DefinitionsToAdZoneAccessModel(
            ISubscriptionModel subscription,
            IMembershipAdZoneAccess? membershipAdZoneAccess,
            string? contextProfileName)
        {
            var retVal = RegistryLoaderWrapper.GetInstance<IAdZoneAccessModel>(contextProfileName);
            retVal.Active = true;
            retVal.Name = Contract.RequiresNotNull(membershipAdZoneAccess?.Slave?.Zone)!.Name;
            retVal.Description = membershipAdZoneAccess!.Slave!.Zone!.Description;
            retVal.SubscriptionID = Contract.RequiresNotNull(subscription).ID;
            retVal.ZoneID = membershipAdZoneAccess.Slave.ZoneID;
            retVal.StartDate = subscription.StartsOn;
            retVal.EndDate = subscription.EndsOn ?? DateTime.MaxValue;
            retVal.UniqueAdLimit = membershipAdZoneAccess.Slave.UniqueAdLimit;
            retVal.ImpressionLimit = membershipAdZoneAccess.Slave.ImpressionLimit;
            retVal.ClickLimit = membershipAdZoneAccess.Slave.ClickLimit;
            return retVal;
        }

        /// <summary>Creates ad zone access.</summary>
        /// <param name="subscription">          The subscription.</param>
        /// <param name="membershipAdZoneAccess">The ad zone access definition.</param>
        /// <param name="levelKey">              The level key.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>The new ad zone access identifier.</returns>
        private static Task<CEFActionResponse<int>> CreateAdZoneAccessForUserAsync(
            ISubscriptionModel subscription,
            IMembershipAdZoneAccess membershipAdZoneAccess,
            string levelKey,
            string? contextProfileName)
        {
            // Set UniqueAdLimit based on current subscriber count
            var subscriptionTypeID = Contract.RequiresNotNull(subscription).TypeID;
            var uniqueAdLimitThresholds = Contract.RequiresNotNull(membershipAdZoneAccess).MembershipAdZoneAccessByLevels!
                .Where(x => x.Active && x.Slave!.CustomKey == levelKey)
                .OrderBy(x => x.SubscriberCountThreshold)
                .ToList();
            // If no thresholds have been defined for the membership level just return the default AdZoneAccessDefinition
            if (uniqueAdLimitThresholds.Count == 0)
            {
                return Workflows.AdZoneAccesses.CreateAsync(
                    DefinitionsToAdZoneAccessModel(subscription, membershipAdZoneAccess, contextProfileName),
                    contextProfileName);
            }
            var count = GetCountBySubscriptionTypeID(subscriptionTypeID, contextProfileName);
            var uniqueAdLimitThreshold = uniqueAdLimitThresholds.Find(t => count <= t.SubscriberCountThreshold);
            // If count is greater than all thresholds return null so no AdZoneAccess is given
            // We are still calling Create AdZoneAccess in case the base still needs to do something and so tests
            // can verify that it was called with a null value
            if (uniqueAdLimitThreshold == null)
            {
                return Workflows.AdZoneAccesses.CreateAsync(
                    DefinitionsToAdZoneAccessModel(subscription, null, contextProfileName),
                    contextProfileName);
            }
            membershipAdZoneAccess.Slave!.UniqueAdLimit = uniqueAdLimitThreshold.UniqueAdLimit;
            return Workflows.AdZoneAccesses.CreateAsync(
                DefinitionsToAdZoneAccessModel(subscription, membershipAdZoneAccess, contextProfileName),
                contextProfileName);
        }

        /// <summary>Creates a membership instance for the user.</summary>
        /// <param name="productMembershipLevel">The product membership level.</param>
        /// <param name="fee">                   The fee.</param>
        /// <param name="userID">                Identifier for the user.</param>
        /// <param name="accountID">             Identifier for the account.</param>
        /// <param name="invoiceID">             Identifier for the invoice.</param>
        /// <param name="timestamp">             The timestamp Date/Time.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>The new membership.</returns>
        private static async Task<CEFActionResponse<ISubscriptionModel>> CreateMembershipLevelInstanceForUserAsync(
            IProductMembershipLevel productMembershipLevel,
            decimal? fee,
            int? userID,
            int? accountID,
            int? invoiceID,
            DateTime timestamp,
            string? contextProfileName)
        {
            var repeatType = productMembershipLevel.MembershipRepeatType!.Slave;
            var billingPeriodsTotal = productMembershipLevel.MembershipRepeatType.Slave!.RepeatableBillingPeriods
                + repeatType!.InitialBonusBillingPeriods;
            var subscription = RegistryLoaderWrapper.GetInstance<ISubscriptionModel>(contextProfileName);
            subscription.Active = true;
            subscription.LastPaidDate = subscription.StartsOn = subscription.MemberSince = subscription.CreatedDate = timestamp;
            subscription.EndsOn = MembershipCalculateDate(timestamp, repeatType.CustomKey!, repeatType.RepeatableBillingPeriods);
            subscription.BillingPeriodsPaid = 1;
            subscription.BillingPeriodsTotal = billingPeriodsTotal ?? 0;
            subscription.Fee = fee ?? 0m;
            subscription.Memo = $"Renewing {repeatType.DisplayName ?? repeatType.Name} Subscription for {billingPeriodsTotal} period(s)";
            // TODO: Make this a site-wide setting to either never auto-repeat, always auto-repeat, or allow it be
            // based on user's selection
            subscription.AutoRenew = repeatType.RepeatableBillingPeriods > 0;
            // TODO: Read sort order of all levels to see if this is the highest or not
            subscription.CanUpgrade = true;
            subscription.RepeatTypeID = repeatType.ID;
            if (Contract.CheckInvalidID(SubscriptionStatusIDOfCurrent))
            {
                SubscriptionStatusIDOfCurrent = await Workflows.SubscriptionStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Current",
                        byName: "Current",
                        byDisplayName: "Current",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            subscription.StatusID = SubscriptionStatusIDOfCurrent;
            if (Contract.CheckInvalidID(SubscriptionTypeIDOfMembership))
            {
                SubscriptionTypeIDOfMembership = await Workflows.SubscriptionTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "Membership",
                        byName: "Membership",
                        byDisplayName: "Membership",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            subscription.TypeID = SubscriptionTypeIDOfMembership;
            subscription.UserID = userID;
            subscription.AccountID = accountID;
            subscription.ID = (await Workflows.Subscriptions.CreateAsync(subscription, contextProfileName).ConfigureAwait(false)).Result;
            var result = subscription.WrapInPassingCEFARIfNotNull();
            if (Contract.CheckValidID(userID)
                && Contract.CheckValidKey(productMembershipLevel.Slave?.RolesApplied))
            {
                var response = await CEFAR.AggregateAsync(
                        productMembershipLevel.Slave!.RolesApplied!
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                            .Select(role => new RoleForUserModel
                            {
                                Name = role,
                                UserId = userID!.Value,
                                StartDate = timestamp,
                                EndDate = subscription.EndsOn,
                            }),
                        x => Workflows.Authentication.AssignRoleToUserAsync(x, contextProfileName))
                    .ConfigureAwait(false);
                if (!response.ActionSucceeded)
                {
                    // TODO
                }
            }
            // See if this subscription type is supposed to generate access to Advertizing space(s), if so, set them up
            if (!productMembershipLevel.Slave!.MembershipAdZoneAccessByLevels!.Any(x => x.Active))
            {
                return result;
            }
            foreach (var membershipAdZoneAccessByLevel in productMembershipLevel.Slave.MembershipAdZoneAccessByLevels!.Where(x => x.Active))
            {
                var createResponse = await CreateAdZoneAccessForUserAsync(
                        subscription: subscription,
                        membershipAdZoneAccess: membershipAdZoneAccessByLevel.Master!,
                        levelKey: membershipAdZoneAccessByLevel.Slave!.CustomKey!,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
                result.Messages.Add($"Set up ad zone: {createResponse.Result}");
            }
            return result;
        }

        /// <summary>Executes the membership product sales order item created action.</summary>
        /// <param name="productID">            Identifier for the product.</param>
        /// <param name="userID">               The identifier for the user.</param>
        /// <param name="accountID">            The identifier for the account.</param>
        /// <param name="invoiceID">            Identifier for the invoice.</param>
        /// <param name="timestamp">            The timestamp Date/Time.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="fee">                  The fee.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        private static async Task<CEFActionResponse> OnMembershipProductSalesOrderItemCreatedAsync(
            int productID,
            int? userID,
            int? accountID,
            int? invoiceID,
            DateTime timestamp,
            IPricingFactoryContextModel? pricingFactoryContext,
            decimal? fee,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // ReSharper disable once StyleCop.SA1008
            var list = await context.Products
                .AsNoTracking()
                .Include(x => x.ProductMembershipLevels)
                .Include(x => x.ProductMembershipLevels!.Select(y => y.Slave))
                .Include(x => x.ProductMembershipLevels!.Select(y => y.Slave).Select(y => y!.MembershipAdZoneAccessByLevels))
                .Include(x => x.ProductMembershipLevels!.Select(y => y.Slave).Select(y => y!.MembershipAdZoneAccessByLevels!.Select(z => z.Master)))
                .Include(x => x.ProductMembershipLevels!.Select(y => y.Slave).Select(y => y!.MembershipAdZoneAccessByLevels!.Select(z => z.Master!.Slave)))
                .Include(x => x.ProductMembershipLevels!.Select(y => y.Slave).Select(y => y!.MembershipAdZoneAccessByLevels!.Select(z => z.Master!.Slave!.Zone)))
                .Include(x => x.ProductMembershipLevels!.Select(y => y.MembershipRepeatType))
                .Include(x => x.ProductMembershipLevels!.Select(y => y.MembershipRepeatType).Select(y => y!.Slave))
                .FilterByActive(true)
                .FilterByID(productID)
                .Select(x => new Product
                {
                    ID = x.ID,
                    Name = x.Name,
                    Description = x.Description,
                    ProductMembershipLevels = x.ProductMembershipLevels,
                })
                .Take(1)
                .ToListAsync()
                .ConfigureAwait(false);
            if (list.Count == 0)
            {
                return CEFAR.FailingCEFAR<IEnumerable<ISubscriptionModel>>("Could not locate product");
            }
            var first = list.Single();
            var membershipProduct = new MembershipProduct
            {
                ProductName = first.Name,
                ProductDescription = first.Description,
                Price = fee
                    ?? (await RegistryLoaderWrapper.GetInstance<IPricingFactory>(contextProfileName).CalculatePriceAsync(
                            first.ID,
                            null,
                            pricingFactoryContext,
                            contextProfileName)
                        .ConfigureAwait(false))
                        .BasePrice,
                ProductMembershipLevels = first.ProductMembershipLevels!
                    .Where(y => y.Active && y.Slave!.Active && y.MembershipRepeatType!.Active)
                    .ToList<IProductMembershipLevel>(),
            };
            if (Contract.CheckEmpty(membershipProduct.ProductMembershipLevels))
            {
                return CEFAR.FailingCEFAR<IEnumerable<ISubscriptionModel>>(
                    "Product does not contain membership definitions");
            }
            return await CEFAR.AggregateAsync(
                membershipProduct.ProductMembershipLevels,
                // NOTE: Typically only one
                x => CreateMembershipLevelInstanceForUserAsync(
                        productMembershipLevel: x!,
                        fee: membershipProduct.Price,
                        userID: userID,
                        accountID: accountID,
                        invoiceID: invoiceID,
                        timestamp: timestamp,
                        contextProfileName: contextProfileName)!)
                    .ConfigureAwait(false);
        }

        /// <summary>Gets count by subscription type identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <param name="includeInactive">   True to include, false to exclude the inactive.</param>
        /// <returns>The count by subscription type identifier.</returns>
        private static int GetCountBySubscriptionTypeID(int? id, string? contextProfileName, bool includeInactive = false)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return context.Subscriptions
                .AsNoTracking()
                .Count(s => s.TypeID == id && (s.Active || includeInactive));
        }
    }
}
