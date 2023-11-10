// <copyright file="ProductWithProductAssociationsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate products workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Utilities;

    public partial class ProductWithProductAssociationsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IProductAssociationModel model,
            IProductAssociation entity,
            IClarityEcommerceEntities context)
        {
            return entity.Slave!.CustomKey == (model.SlaveKey ?? model.Slave?.CustomKey)
                && entity.Quantity == model.Quantity
                && entity.UnitOfMeasure == model.UnitOfMeasure
                && entity.SortOrder == model.SortOrder
                && (entity.BrandID == model.BrandID || entity.Brand?.CustomKey == model.BrandKey)
                && (entity.StoreID == model.StoreID || entity.Store?.CustomKey == model.StoreKey)
                && (entity.TypeID == model.TypeID || entity.Type!.CustomKey == (model.TypeKey ?? model.Type?.CustomKey))
                && entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IProductAssociation newEntity,
            IProductAssociationModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // ProductAssociation Properties
            newEntity.Quantity = model.Quantity;
            newEntity.UnitOfMeasure = model.UnitOfMeasure?.Trim();
            newEntity.SortOrder = model.SortOrder;
            // Related Objects
            newEntity.TypeID = await Workflows.ProductAssociationTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: model.TypeID,
                    byKey: model.TypeKey,
                    byName: model.TypeName,
                    model: model.Type,
                    context: context)
                .ConfigureAwait(false);
            if (Contract.CheckValidIDOrAnyValidKey(
                    model.StoreID,
                    model.StoreKey,
                    model.StoreName,
                    model.Store?.CustomKey,
                    model.Store?.Name))
            {
                newEntity.StoreID = await Workflows.Stores.ResolveWithAutoGenerateToIDAsync(
                        byID: model.StoreID,
                        byKey: model.StoreKey,
                        byName: model.StoreName,
                        model: model.Store,
                        context: context)
                    .ConfigureAwait(false);
            }
            if (Contract.CheckValidIDOrAnyValidKey(
                    model.BrandID,
                    model.BrandKey,
                    model.BrandName,
                    model.Brand?.CustomKey,
                    model.Brand?.Name))
            {
                newEntity.BrandID = await Workflows.Brands.ResolveWithAutoGenerateToIDAsync(
                        byID: model.BrandID,
                        byKey: model.BrandKey,
                        byName: model.BrandName,
                        model: model.Brand,
                        context: context)
                    .ConfigureAwait(false);
            }
            newEntity.SlaveID = await Workflows.Products.ResolveWithAutoGenerateToIDAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    byName: model.SlaveName,
                    model: model.Slave,
                    context: context)
                .ConfigureAwait(false);
        }
    }
}
