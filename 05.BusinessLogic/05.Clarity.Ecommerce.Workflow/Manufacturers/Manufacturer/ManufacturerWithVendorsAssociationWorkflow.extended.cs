// <copyright file="ManufacturerWithVendorsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate manufacturer vendors workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class ManufacturerWithVendorsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IVendorManufacturer newEntity,
            IVendorManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Vendors.ResolveWithAutoGenerateToIDAsync(
                    model.MasterID,
                    model.MasterKey,
                    model.MasterName,
                    null,
                    context)
                .ConfigureAwait(false);
        }
    }
}
