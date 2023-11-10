// <copyright file="FavoriteManufacturerCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite manufacturer workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FavoriteManufacturerWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<FavoriteManufacturer>> FilterQueryByModelCustomAsync(
            IQueryable<FavoriteManufacturer> query,
            IFavoriteManufacturerSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterFavoritesByFavoriteIDKeyOrName<FavoriteManufacturer, Manufacturer>(
                    search.ManufacturerID,
                    search.ManufacturerKey,
                    search.ManufacturerName);
        }
    }
}
