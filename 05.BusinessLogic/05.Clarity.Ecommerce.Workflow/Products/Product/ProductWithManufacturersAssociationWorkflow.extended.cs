// <copyright file="ProductWithManufacturersAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate product manufacturers workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Utilities;

    public partial class ProductWithManufacturersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IManufacturerProductModel model)
        {
            // Must have something to recognize the manufacturer by
            return Contract.CheckAnyValidID(model.MasterID)
                || Contract.CheckAnyValidKey(model.MasterKey, model.MasterName);
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IManufacturerProduct newEntity,
            IManufacturerProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            newEntity.MasterID = await Workflows.Manufacturers.ResolveWithAutoGenerateToIDAsync(
                    byID: model.MasterID,
                    byKey: model.MasterKey,
                    byName: model.MasterName,
                    model: new ManufacturerModel
                    {
                        Active = true,
                        ID = model.MasterID,
                        CustomKey = model.MasterKey,
                        TypeKey = "GENERAL",
                    },
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
