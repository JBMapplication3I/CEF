// <autogenerated>
// <copyright file="CartItemWithDiscountsAssociationWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>An Cart Item Discounts association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{ISalesItemBaseModel, ICartItem, IAppliedCartItemDiscountModel, IAppliedCartItemDiscount, AppliedCartItemDiscount}"/>
    /// <seealso cref="ICartItemWithDiscountsAssociationWorkflow"/>
    public partial class CartItemWithDiscountsAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateObjectsWorkflowBase<ISalesItemBaseModel<IAppliedCartItemDiscountModel>, ICartItem, IAppliedCartItemDiscountModel, IAppliedCartItemDiscount, AppliedCartItemDiscount>
        , ICartItemWithDiscountsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<AppliedCartItemDiscount>? GetObjectsCollection(ICartItem entity) { return entity.Discounts; }

        /// <inheritdoc/>
        protected override List<IAppliedCartItemDiscountModel>? GetModelObjectsList(ISalesItemBaseModel<IAppliedCartItemDiscountModel> model) { return model.Discounts; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(ICartItem entity, IAppliedCartItemDiscount newEntity)
        {
            if (newEntity == null) { return; }
            entity.Discounts!.Add((AppliedCartItemDiscount)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(ICartItem entity)
        {
            if (entity.Discounts != null) { return; }
            entity.Discounts = new HashSet<AppliedCartItemDiscount>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(IAppliedCartItemDiscount entity, DateTime timestamp)
        {
            // Hook-in to deactivate more custom property assignments
            await DeactivateObjectAdditionalPropertiesAsync(entity, timestamp).ConfigureAwait(false);
            // Deactivate this entity
            entity.Active = false;
            entity.UpdatedDate = timestamp;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(IAppliedCartItemDiscountModel model)
        {
            return model.Active /* == true */
                // No additional default properties to check
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(IAppliedCartItemDiscountModel model, IAppliedCartItemDiscount entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IAppliedCartItemDiscount> ModelToNewObjectAsync(
            IAppliedCartItemDiscountModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IAppliedCartItemDiscount> ModelToNewObjectAsync(
            IAppliedCartItemDiscountModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate
            Contract.RequiresNotNull(model);
            // Create a new entity and populate it with data
            var newEntity = model.CreateAppliedCartItemDiscountEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
