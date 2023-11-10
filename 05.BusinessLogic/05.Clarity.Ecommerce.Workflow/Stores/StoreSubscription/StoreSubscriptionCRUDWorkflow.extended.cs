// <copyright file="StoreSubscriptionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store vendor workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class StoreSubscriptionWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<StoreSubscription>> FilterQueryByModelCustomAsync(
            IQueryable<StoreSubscription> query,
            IStoreSubscriptionSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelCustomAsync(query, search, context).ConfigureAwait(false))
                .FilterRelationshipsByMasterStoreID<StoreSubscription, Subscription>(search.StoreID);
        }
    }
}
