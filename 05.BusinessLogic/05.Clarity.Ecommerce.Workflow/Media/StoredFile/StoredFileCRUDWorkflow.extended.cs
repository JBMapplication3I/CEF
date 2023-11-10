// <copyright file="StoredFileCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements additional query filters for Stored Files.</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using Clarity.Ecommerce.DataModel;
    using Clarity.Ecommerce.Interfaces.DataModel;
    using Clarity.Ecommerce.Interfaces.Models;

    public partial class StoredFileWorkflow
    {
        protected override async Task<IQueryable<StoredFile>> FilterQueryByModelCustomAsync(
            IQueryable<StoredFile> query,
            IStoredFileSearchModel search,
            IClarityEcommerceEntities context)
        {
            var baseQuery = await base.FilterQueryByModelCustomAsync(
                    query,
                    search,
                    context)
                .ConfigureAwait(false);
            return baseQuery
                .FilterStoredFilesByAccountID(search.AccountID)
                .FilterStoredFilesByCategoryID(search.CategoryID);
        }
    }
}
