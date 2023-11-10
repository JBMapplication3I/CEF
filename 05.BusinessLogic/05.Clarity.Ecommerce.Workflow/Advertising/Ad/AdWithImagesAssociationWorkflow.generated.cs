// <autogenerated>
// <copyright file="AdWithImagesAssociationWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>An Ad Images association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{IAdModel, IAd, IAdImageModel, IAdImage, AdImage}"/>
    /// <seealso cref="IAdWithImagesAssociationWorkflow"/>
    public partial class AdWithImagesAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateImagesWorkflowBase<IAdModel, IAd, IAdImageModel, IAdImage, AdImage>
        , IAdWithImagesAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<AdImage>? GetObjectsCollection(IAd entity) { return entity.Images; }

        /// <inheritdoc/>
        protected override List<IAdImageModel>? GetModelObjectsList(IAdModel model) { return model.Images; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(IAd entity, IAdImage newEntity)
        {
            if (newEntity == null) { return; }
            entity.Images!.Add((AdImage)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(IAd entity)
        {
            if (entity.Images != null) { return; }
            entity.Images = new HashSet<AdImage>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(IAdImage entity, DateTime timestamp)
        {
            // Hook-in to deactivate more custom property assignments
            await DeactivateObjectAdditionalPropertiesAsync(entity, timestamp).ConfigureAwait(false);
            // Deactivate this entity
            entity.Active = false;
            entity.UpdatedDate = timestamp;
        }

        private static int UpsertThisEntityTypeAndGetIDInner(
            string? name,
            string? key,
            ITypableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var entity = string.IsNullOrWhiteSpace(key) ? null : context.AdImageTypes.FilterByActive(true).FilterByCustomKey(key, true).SingleOrDefault();
            if (entity == null) { entity = string.IsNullOrWhiteSpace(name) ? null : context.AdImageTypes.FilterByActive(true).FilterByName(name, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.CustomKey)) { entity = context.AdImageTypes.FilterByActive(true).FilterByName(model!.CustomKey, true).SingleOrDefault(); }
            if (entity == null && !string.IsNullOrWhiteSpace(model?.Name)) { entity = context.AdImageTypes.FilterByActive(true).FilterByName(model!.Name, true).SingleOrDefault(); }
            if (entity != null) { return entity.ID; } // Return existing
            // Using a separate context here so we don't save a partially modified Store
            var newEntity = RegistryLoaderWrapper.GetInstance<IAdImageType>(context.ContextProfileName);
            newEntity.Active = true;
            newEntity.CreatedDate = timestamp;
            if (model != null)
            {
                newEntity.CustomKey = model.CustomKey;
                newEntity.Name = model.Name;
                newEntity.DisplayName = model.DisplayName;
                newEntity.SortOrder = model.SortOrder;
            }
            else
            {
                newEntity.CustomKey = key;
                newEntity.Name = name;
            }
            context.AdImageTypes.Add((AdImageType)newEntity);
            if (!context.SaveUnitOfWork(true))
            {
                throw new InvalidOperationException("Saving the Type failed.");
            }
            return newEntity.ID;
        }

        private static int UpsertThisEntityTypeAndGetID(
            string? name,
            string? key,
            ITypableBaseModel? model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(name, key, model);
            return UpsertThisEntityTypeAndGetIDInner(name, key, model, timestamp, context);
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(IAdImageModel model)
        {
            return model.Active /* == true */
                // No additional default properties to check
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(IAdImageModel model, IAdImage entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IAdImage> ModelToNewObjectAsync(
            IAdImageModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IAdImage> ModelToNewObjectAsync(
            IAdImageModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate
            Contract.RequiresNotNull(model);
            var thisEntityTypeID = Contract.CheckValidID(model.TypeID)
                ? model.TypeID
                : model.Type != null && Contract.CheckValidID(model.Type.ID)
                    ? model.Type.ID
                    : UpsertThisEntityTypeAndGetID(
                        model.TypeName ?? "General",
                        model.TypeKey ?? "General",
                        model.Type,
                        timestamp,
                        context);
            if (thisEntityTypeID <= 0) { throw new ArgumentException("AdImage requires a valid Type."); }
            // Create a new entity and populate it with data
            var newEntity = model.CreateAdImageEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            newEntity.TypeID = thisEntityTypeID;
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
