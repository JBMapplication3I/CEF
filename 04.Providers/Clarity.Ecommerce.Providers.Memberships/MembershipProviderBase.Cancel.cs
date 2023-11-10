// <copyright file="MembershipProviderBase.Cancel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership provider base class (Cancel part)</summary>
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public abstract partial class MembershipProviderBase
    {
        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> CancelMembershipAsync(int subscriptionID, string? contextProfileName)
        {
            if (Contract.CheckInvalidID(subscriptionID))
            {
                return CEFAR.FailingCEFAR("ERROR! Invalid subscription identifier.");
            }
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                var subscription = await context.Subscriptions
                    .Include(x => x.Type)
                    .FilterByID(subscriptionID)
                    .SingleOrDefaultAsync();
                if (subscription == null)
                {
                    return CEFAR.FailingCEFAR("ERROR! Could not find the subscription to cancel.");
                }
                var timestamp = DateExtensions.GenDateTime;
                if (Contract.CheckValidDate(subscription.EndsOn) && subscription.EndsOn < timestamp)
                {
                    return CEFAR.FailingCEFAR("ERROR! Cannot cancel a subscription that has already expired.");
                }
                // We are good to actually cancel, look for membership and ad zone access definitions and remove the
                // benefits they were providing
                if (Contract.CheckValidID(subscription.ProductMembershipLevelID) && subscription.ProductMembershipLevel != null)
                {
                    // TODO: Remove roles by setting end dates on them to the end of the pay period instead of Today,
                    // bind this to a setting for how the Client wants to handle it
                    // TODO: Review cancel subscription for all users in the account?
                    if (Contract.CheckValidID(subscription.UserID)
                        && Contract.CheckValidKey(subscription.ProductMembershipLevel.Slave!.RolesApplied))
                    {
                        var rolesForUser = await context.RoleUsers
                            .Where(x => x.UserId == subscription.UserID)
                            .Select(x => new RoleUser
                            {
                                RoleId = x.RoleId,
                                Role = new() { Name = x.Role!.Name },
                                UserId = x.UserId,
                                StartDate = x.StartDate,
                                EndDate = x.EndDate,
                            })
                            .ToArrayAsync()
                            .ConfigureAwait(false);
                        var roles = subscription.ProductMembershipLevel.Slave.RolesApplied!
                            .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
                        if (roles.Length > 0)
                        {
                            foreach (var role in roles)
                            {
                                foreach (var roleForUser in rolesForUser.Where(x => x.Role!.Name == role))
                                {
                                    if (roleForUser.EndDate != null && roleForUser.EndDate <= timestamp)
                                    {
                                        continue;
                                    }
                                    roleForUser.EndDate = timestamp;
                                }
                            }
                            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                        }
                    }
                    // TODO: Remove zones by setting end dates on them same as noted above for roles
                    foreach (var zoneID in subscription.ProductMembershipLevel.Slave!.MembershipAdZoneAccessByLevels!
                                                .Select(x => x.Master!.Slave!.ZoneID))
                    {
                        foreach (var access in context.AdZoneAccesses
                            .FilterAdZoneAccessesBySubscriptionID(subscriptionID)
                            .FilterAdZoneAccessesByZoneID(zoneID))
                        {
                            access.EndDate = timestamp;
                            access.UpdatedDate = timestamp;
                        }
                    }
                    await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
                }
                // Mark membership as canceled and update it
                var model = subscription.CreateSubscriptionModelFromEntityFull(contextProfileName)!;
                model.StatusID = 0;
                model.Status = null;
                model.StatusDisplayName = model.StatusName = model.StatusKey = "Cancelled";
                model.EndsOn = timestamp;
                model.AutoRenew = false;
                model.CanUpgrade = false;
                await Workflows.Subscriptions.UpdateAsync(model, contextProfileName).ConfigureAwait(false);
            }
            return CEFAR.PassingCEFAR();
        }
    }
}
