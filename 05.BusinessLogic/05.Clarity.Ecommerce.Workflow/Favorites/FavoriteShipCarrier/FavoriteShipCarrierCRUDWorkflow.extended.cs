// <copyright file="FavoriteShipCarrierCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite ship carrier workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FavoriteShipCarrierWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<FavoriteShipCarrier>> FilterQueryByModelCustomAsync(
            IQueryable<FavoriteShipCarrier> query,
            IFavoriteShipCarrierSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterFavoritesByFavoriteIDKeyOrName<FavoriteShipCarrier, ShipCarrier>(
                    search.ShipCarrierID,
                    search.ShipCarrierKey,
                    search.ShipCarrierName);
        }
    }
}
