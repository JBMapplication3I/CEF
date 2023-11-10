// <autogenerated>
// <copyright file="ProductWithVendorsAssociationWorkflow.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Association Workflow classes for each table</summary>
// <remarks>This file was auto-generated by AssociationWorkflows.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#nullable enable
// ReSharper disable ConvertIfStatementToNullCoalescingExpression, InvalidXmlDocComment
#pragma warning disable CS0618,CS1998
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Utilities;

    /// <summary>An Product Vendors association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{IProductModel, IProduct, IVendorProductModel, IVendorProduct, VendorProduct}"/>
    /// <seealso cref="IProductWithVendorsAssociationWorkflow"/>
    public partial class ProductWithVendorsAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateObjectsWorkflowBase<IProductModel, IProduct, IVendorProductModel, IVendorProduct, VendorProduct>
        , IProductWithVendorsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<VendorProduct>? GetObjectsCollection(IProduct entity) { return entity.Vendors; }

        /// <inheritdoc/>
        protected override List<IVendorProductModel>? GetModelObjectsList(IProductModel model) { return model.Vendors; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(IProduct entity, IVendorProduct newEntity)
        {
            if (newEntity == null) { return; }
            entity.Vendors!.Add((VendorProduct)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(IProduct entity)
        {
            if (entity.Vendors != null) { return; }
            entity.Vendors = new HashSet<VendorProduct>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(IVendorProduct entity, DateTime timestamp)
        {
            // Hook-in to deactivate more custom property assignments
            await DeactivateObjectAdditionalPropertiesAsync(entity, timestamp).ConfigureAwait(false);
            // Deactivate this entity
            entity.Active = false;
            entity.UpdatedDate = timestamp;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(IVendorProductModel model)
        {
            return model.Active /* == true */
                // No additional default properties to check
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(IVendorProductModel model, IVendorProduct entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IVendorProduct> ModelToNewObjectAsync(
            IVendorProductModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IVendorProduct> ModelToNewObjectAsync(
            IVendorProductModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate
            Contract.RequiresNotNull(model);
            // Create a new entity and populate it with data
            var newEntity = model.CreateVendorProductEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
