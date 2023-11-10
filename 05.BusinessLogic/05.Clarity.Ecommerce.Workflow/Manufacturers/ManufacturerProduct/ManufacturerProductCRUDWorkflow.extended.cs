// <copyright file="ManufacturerProductCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer product workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class ManufacturerProductWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<ManufacturerProduct>> FilterQueryByModelCustomAsync(
            IQueryable<ManufacturerProduct> query,
            IManufacturerProductSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterManufacturerProductsByProductKey(search.ProductKey)
                .FilterManufacturerProductsByProductName(search.ProductName)
                .FilterManufacturerProductsByManufacturerKey(search.ManufacturerKey)
                .FilterManufacturerProductsByManufacturerName(search.ManufacturerName);
        }
    }
}
