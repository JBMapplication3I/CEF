// <copyright file="StoreWithBrandsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store brands workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class StoreWithBrandsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IBrandStore newEntity,
            IBrandStoreModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Brands.ResolveWithAutoGenerateToIDAsync(
                    model.MasterID,
                    model.MasterKey,
                    model.MasterName,
                    null,
                    context)
                .ConfigureAwait(false);
        }
    }
}
