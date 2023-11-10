// <copyright file="FavoriteVendorCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the favorite vendor workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class FavoriteVendorWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<FavoriteVendor>> FilterQueryByModelCustomAsync(
            IQueryable<FavoriteVendor> query,
            IFavoriteVendorSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterFavoritesByFavoriteIDKeyOrName<FavoriteVendor, Vendor>(
                    search.VendorID,
                    search.VendorKey,
                    search.VendorName);
        }
    }
}
