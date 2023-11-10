// <copyright file="GroupCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the group workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class GroupWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<IQueryable<Group>> FilterQueryByModelCustomAsync(
            IQueryable<Group> query,
            IGroupSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterByGroupOwnerID(search.OwnerID)
                .FilterByMemberID(search.UserID);
        }
    }
}
