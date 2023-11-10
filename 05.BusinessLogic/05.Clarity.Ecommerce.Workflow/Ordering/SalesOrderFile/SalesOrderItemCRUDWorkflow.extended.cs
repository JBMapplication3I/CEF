// <copyright file="SalesOrderItemCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order item workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class SalesOrderItemWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesOrderItem>> FilterQueryByModelCustomAsync(
            IQueryable<SalesOrderItem> query,
            ISalesItemBaseSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterSalesOrderItemsBySalesOrderKey(search.MasterKey)
                .FilterSalesOrderItemsByUserExternalID(search.UserExternalID);
        }
    }
}
