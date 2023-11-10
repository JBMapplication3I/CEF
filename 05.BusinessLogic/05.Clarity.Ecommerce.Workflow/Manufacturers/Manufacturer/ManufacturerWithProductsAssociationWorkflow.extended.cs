﻿// <copyright file="ManufacturerWithProductsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate manufacturer products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class ManufacturerWithProductsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IManufacturerProduct newEntity,
            IManufacturerProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.SlaveID = await Workflows.Products.ResolveToIDAsync(
                    model.SlaveID,
                    model.SlaveKey,
                    model.SlaveName,
                    model.Slave,
                    context)
                .ConfigureAwait(false);
        }
    }
}
