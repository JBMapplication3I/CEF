// <copyright file="MembershipProviderBase.Upgrade.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership provider base class (Upgrade part)</summary>
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Utilities;

    public abstract partial class MembershipProviderBase
    {
        /// <inheritdoc/>
        [Pure]
        public virtual async Task<CEFActionResponse> IsSubscriptionInUpgradePeriodAsync(
            int subscriptionID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var subscription = await context.Subscriptions
                .AsNoTracking()
                .FilterByActive(true)
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
                return CEFAR.PassingCEFAR(); // Allowed to upgrade whenever
            }
            var maxDate = DateExtensions.GenDateTime.AddDays(CEFConfigDictionary.MembershipsUpgradePeriodBlackout * -1);
            return (subscription.EndsOn <= maxDate).BoolToCEFAR(
                $"The subscription cannot be changed within {CEFConfigDictionary.MembershipsUpgradePeriodBlackout} days of the end.");
        }

        /// <inheritdoc/>
        public async Task<List<IProductModel>> GetAvailableUpgradesAsync(int subscriptionID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var typeSortOrder = Contract.RequiresValidID(
                await context.Subscriptions
                    .AsNoTracking()
                    .FilterByActive(true)
                    .FilterByID(subscriptionID)
                    .Select(x => x.Type!.SortOrder)
                    .SingleOrDefaultAsync());
            var higherSubscriptionTypeIDs = context.SubscriptionTypes
                .AsNoTracking()
                .FilterByActive(true)
                .FilterSubscriptionTypesByGreaterSortOrder(typeSortOrder)
                .Select(x => x.ID)
                .Distinct();
            var productList = new List<IProductModel>();
            foreach (var typeID in higherSubscriptionTypeIDs)
            {
                var productSubscriptions = await Workflows.ProductSubscriptionTypes.GetBySubscriptionTypeAsync(
                        typeID,
                        contextProfileName)
                    .ConfigureAwait(false);
                if (productSubscriptions.Count != 1)
                {
                    continue;
                }
                productList.Add(
                    (await Workflows.Products.GetAsync(
                        id: productSubscriptions.Single().MasterID,
                        contextProfileName: contextProfileName,
                        isVendorAdmin: false,
                        vendorAdminID: null,
                        previewID: null)
                    .ConfigureAwait(false))!);
            }
            return productList;
        }
    }
}
