// <copyright file="FavoriteCategoryCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite category workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FavoriteCategoryWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<FavoriteCategory>> FilterQueryByModelCustomAsync(
            IQueryable<FavoriteCategory> query,
            IFavoriteCategorySearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterFavoritesByFavoriteIDKeyOrName<FavoriteCategory, Category>(
                    search.CategoryID,
                    search.CategoryKey,
                    search.CategoryName);
        }
    }
}
