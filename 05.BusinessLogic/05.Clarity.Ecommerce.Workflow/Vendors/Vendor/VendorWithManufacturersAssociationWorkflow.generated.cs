// <autogenerated>
// <copyright file="VendorWithManufacturersAssociationWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>An Vendor Manufacturers association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{IVendorModel, IVendor, IVendorManufacturerModel, IVendorManufacturer, VendorManufacturer}"/>
    /// <seealso cref="IVendorWithManufacturersAssociationWorkflow"/>
    public partial class VendorWithManufacturersAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateObjectsWorkflowBase<IVendorModel, IVendor, IVendorManufacturerModel, IVendorManufacturer, VendorManufacturer>
        , IVendorWithManufacturersAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<VendorManufacturer>? GetObjectsCollection(IVendor entity) { return entity.Manufacturers; }

        /// <inheritdoc/>
        protected override List<IVendorManufacturerModel>? GetModelObjectsList(IVendorModel model) { return model.Manufacturers; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(IVendor entity, IVendorManufacturer newEntity)
        {
            if (newEntity == null) { return; }
            entity.Manufacturers!.Add((VendorManufacturer)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(IVendor entity)
        {
            if (entity.Manufacturers != null) { return; }
            entity.Manufacturers = new HashSet<VendorManufacturer>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(IVendorManufacturer entity, DateTime timestamp)
        {
            // Hook-in to deactivate more custom property assignments
            await DeactivateObjectAdditionalPropertiesAsync(entity, timestamp).ConfigureAwait(false);
            // Deactivate this entity
            entity.Active = false;
            entity.UpdatedDate = timestamp;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(IVendorManufacturerModel model)
        {
            return model.Active /* == true */
                // No additional default properties to check
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(IVendorManufacturerModel model, IVendorManufacturer entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IVendorManufacturer> ModelToNewObjectAsync(
            IVendorManufacturerModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IVendorManufacturer> ModelToNewObjectAsync(
            IVendorManufacturerModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate
            Contract.RequiresNotNull(model);
            // Create a new entity and populate it with data
            var newEntity = model.CreateVendorManufacturerEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
