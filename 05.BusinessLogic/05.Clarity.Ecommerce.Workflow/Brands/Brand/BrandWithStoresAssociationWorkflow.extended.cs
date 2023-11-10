// <copyright file="BrandWithStoresAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate Brand Stores workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class BrandWithStoresAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IBrandStore newEntity,
            IBrandStoreModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Stores.ResolveWithAutoGenerateToIDAsync(
                    model.StoreID,
                    model.StoreKey,
                    model.StoreName,
                    model.Store,
                    context)
                .ConfigureAwait(false);
        }
    }
}
