// <copyright file="StoreWithManufacturersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate store manufacturers workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class StoreWithManufacturersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IStoreManufacturer newEntity,
            IStoreManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Manufacturers.ResolveWithAutoGenerateToIDAsync(
                    model.SlaveID,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
        }
    }
}
