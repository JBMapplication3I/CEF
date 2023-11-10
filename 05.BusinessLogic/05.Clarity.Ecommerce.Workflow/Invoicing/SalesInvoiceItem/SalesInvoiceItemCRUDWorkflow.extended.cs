// <copyright file="SalesInvoiceItemCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice item workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class SalesInvoiceItemWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<SalesInvoiceItem>> FilterQueryByModelCustomAsync(
            IQueryable<SalesInvoiceItem> query,
            ISalesItemBaseSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterSalesInvoiceItemsBySalesInvoiceKey(search.MasterKey)
                .FilterSalesInvoiceItemsByUserExternalID(search.UserExternalID);
        }
    }
}
