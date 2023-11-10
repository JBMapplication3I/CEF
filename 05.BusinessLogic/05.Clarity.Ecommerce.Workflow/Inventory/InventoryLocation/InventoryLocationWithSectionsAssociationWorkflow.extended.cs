// <copyright file="InventoryLocationWithSectionsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate inventory location sections workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class InventoryLocationWithSectionsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IInventoryLocationSectionModel model,
            IInventoryLocationSection entity,
            IClarityEcommerceEntities context)
        {
            return entity.Name == model.Name
                && entity.Description == model.Description;
        }
    }
}
