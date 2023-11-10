// <copyright file="ProductWithCategoriesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate product categories workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Utilities;

    public partial class ProductWithCategoriesAssociationWorkflow
    {
        /// <summary>Gets or sets the identifier of the general category type.</summary>
        /// <value>The identifier of the general category type.</value>
        private static int? GeneralCategoryTypeID { get; set; }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(IProductCategoryModel model)
        {
            // Must have something to recognize the category by
            return Contract.CheckAnyValidID(model.SlaveID, model.Slave?.ID)
                || Contract.CheckAnyValidKey(model.SlaveKey, model.Slave?.CustomKey)
                || CEFConfigDictionary.ImportProductsProductCategoriesAllowResolveByName
                    && Contract.CheckAnyValidKey(model.SlaveName, model.Slave?.Name)
                || CEFConfigDictionary.ImportProductsProductCategoriesAllowResolveBySeoUrl
                    && Contract.CheckValidKey(model.Slave?.SeoUrl);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            IProductCategoryModel model,
            IProductCategory entity,
            IClarityEcommerceEntities context)
        {
            if (entity.SortOrder != model.SortOrder)
            {
                return false; // else continue
            }
            if (Contract.CheckValidID(model.MasterID))
            {
                if (entity.MasterID != model.MasterID)
                {
                    return false;
                } // else continue
            }
            else if (Contract.CheckValidKey(model.MasterKey))
            {
                if (entity.Master!.CustomKey != model.MasterKey)
                {
                    return false;
                } // else continue
            }
            else
            {
                throw new ArgumentException(
                    "Must provide either a ProductID, Product.ID, ProductKey or Product.CustomKey to compare and select");
            }
            if (Contract.CheckValidID(model.SlaveID))
            {
                if (entity.SlaveID != model.SlaveID)
                {
                    return false;
                } // else continue
            }
            else if (Contract.CheckValidID(model.Slave?.ID))
            {
                if (entity.SlaveID != model.Slave!.ID)
                {
                    return false;
                } // else continue
            }
            else if (Contract.CheckValidKey(model.SlaveKey))
            {
                if (entity.Slave!.CustomKey != model.SlaveKey)
                {
                    return false;
                } // else continue
            }
            else if (Contract.CheckValidKey(model.Slave?.CustomKey))
            {
                if (entity.Slave!.CustomKey != model.Slave!.CustomKey)
                {
                    return false;
                } // else continue
            }
            else
            {
                throw new ArgumentException(
                    "Must provide either a CategoryID, Category.ID, CategoryKey or Category.CustomKey to compare and select");
            }
            return entity.JsonAttributes == model.SerializableAttributes.SerializeAttributesDictionary();
        }

        /// <inheritdoc/>
        protected override async Task ModelToNewObjectAdditionalPropertiesAsync(
            IProductCategory newEntity,
            IProductCategoryModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.Requires<InvalidOperationException>(
                ValidateObjectModelIsGoodForDatabaseAdditionalChecks(Contract.RequiresNotNull(model)),
                "Must pass either the Category ID, Key or Name to match against");
            GeneralCategoryTypeID ??= await Workflows.CategoryTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "GENERAL",
                    byName: "General",
                    byDisplayName: "General",
                    model: null,
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            if (model.Slave != null
                && !Contract.CheckValidIDOrAnyValidKey(
                    model.Slave.TypeID,
                    model.Slave.TypeKey,
                    model.Slave.TypeName,
                    model.Slave.Type?.CustomKey,
                    model.Slave.Type?.Name))
            {
                model.Slave.TypeID = GeneralCategoryTypeID.Value;
            }
            // Get the ID, can auto-generate if necessary
            var slaveID = await Workflows.Categories.ResolveWithAutoGenerateToIDOptionalAsync(
                    byID: model.SlaveID,
                    byKey: model.SlaveKey,
                    byName: model.SlaveName,
                    model: model.Slave,
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            if (Contract.CheckInvalidID(slaveID))
            {
                slaveID = await Workflows.Categories.ResolveWithAutoGenerateToIDAsync(
                        byID: model.SlaveID,
                        byKey: model.SlaveKey,
                        byName: model.SlaveName,
                        model: model.Slave,
                        contextProfileName: context.ContextProfileName)
                    .ConfigureAwait(false);
            }
            // Base Properties
            newEntity.Active = model.Active;
            newEntity.CustomKey = model.CustomKey;
            newEntity.CreatedDate = model.CreatedDate;
            newEntity.UpdatedDate = model.UpdatedDate;
            newEntity.Hash = model.Hash;
            newEntity.JsonAttributes = model.SerializableAttributes.SerializeAttributesDictionary();
            // Relationship Properties
            newEntity.SlaveID = slaveID!.Value;
            // ProductCategory Properties
            newEntity.SortOrder = model.SortOrder;
        }
    }
}
