// <copyright file="SubscriptionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Utilities;

    public partial class SubscriptionWorkflow
    {
        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ISubscription entity,
            ISubscriptionModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateSubscriptionFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            entity.StartsOn = model.StartsOn.ConstrainDateTime();
            entity.EndsOn = model.EndsOn.ConstrainDateTime();
            entity.MemberSince = model.MemberSince.ConstrainDateTime();
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<Subscription>> FilterQueryByModelCustomAsync(
            IQueryable<Subscription> query,
            ISubscriptionSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterSubscriptionsByAccountKey(search.AccountKey)
                .FilterSubscriptionsByUserKey(search.UserKey);
        }
    }
}
