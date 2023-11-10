// <copyright file="FavoriteStoreCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite store workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FavoriteStoreWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<FavoriteStore>> FilterQueryByModelCustomAsync(
            IQueryable<FavoriteStore> query,
            IFavoriteStoreSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterFavoritesByFavoriteIDKeyOrName<FavoriteStore, Store>(
                    search.StoreID,
                    search.StoreKey,
                    search.StoreName);
        }
    }
}
