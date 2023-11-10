// <copyright file="InventoryLocationSectionCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the inventory location section workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class InventoryLocationSectionWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<InventoryLocationSection>> FilterQueryByModelCustomAsync(
            IQueryable<InventoryLocationSection> query,
            IInventoryLocationSectionSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterILSByInventoryLocationName(search.Name);
        }
    }
}
