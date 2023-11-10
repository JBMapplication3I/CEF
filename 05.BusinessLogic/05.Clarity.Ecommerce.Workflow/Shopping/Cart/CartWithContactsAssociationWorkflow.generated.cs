// <autogenerated>
// <copyright file="CartWithContactsAssociationWorkflow.generated.cs" company="clarity-ventures.com">
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

    /// <summary>An Cart Contacts association workflow.</summary>
    /// <seealso cref="AssociateObjectsWorkflowBase{ICartModel, ICart, ICartContactModel, ICartContact, CartContact}"/>
    /// <seealso cref="ICartWithContactsAssociationWorkflow"/>
    public partial class CartWithContactsAssociationWorkflow
        // ReSharper disable once RedundantExtendsListEntry
        : AssociateObjectsWorkflowBase<ICartModel, ICart, ICartContactModel, ICartContact, CartContact>
        , ICartWithContactsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override ICollection<CartContact>? GetObjectsCollection(ICart entity) { return entity.Contacts; }

        /// <inheritdoc/>
        protected override List<ICartContactModel>? GetModelObjectsList(ICartModel model) { return model.Contacts; }

        /// <inheritdoc/>
        protected override async Task AddObjectToObjectsListAsync(ICart entity, ICartContact newEntity)
        {
            if (newEntity == null) { return; }
            entity.Contacts!.Add((CartContact)newEntity);
        }

        /// <inheritdoc/>
        protected override void InitializeObjectListIfNull(ICart entity)
        {
            if (entity.Contacts != null) { return; }
            entity.Contacts = new HashSet<CartContact>();
        }

        /// <inheritdoc/>
        protected override async Task DeactivateObjectAsync(ICartContact entity, DateTime timestamp)
        {
            // Hook-in to deactivate more custom property assignments
            await DeactivateObjectAdditionalPropertiesAsync(entity, timestamp).ConfigureAwait(false);
            // Deactivate this entity
            entity.Active = false;
            entity.UpdatedDate = timestamp;
        }

        /// <inheritdoc/>
        protected override bool ValidateObjectModelIsGoodForDatabase(ICartContactModel model)
        {
            return model.Active /* == true */
                // No additional default properties to check
                // == Hook-in to make additional checks
                && ValidateObjectModelIsGoodForDatabaseAdditionalChecks(model);
        }

        /// <inheritdoc/>
        protected override async Task<bool> MatchObjectModelWithObjectEntityAsync(ICartContactModel model, ICartContact entity, IClarityEcommerceEntities context)
        {
            return model.CustomKey == entity.CustomKey
                && model.Hash == entity.Hash
                // == Hook-in to make additional checks
                && await MatchObjectModelWithObjectEntityAdditionalChecksAsync(model, entity, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<ICartContact> ModelToNewObjectAsync(
            ICartContactModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<ICartContact> ModelToNewObjectAsync(
            ICartContactModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate
            Contract.RequiresNotNull(model);
            // Create a new entity and populate it with data
            var newEntity = model.CreateCartContactEntity(timestamp, context.ContextProfileName);
            newEntity.UpdatedDate = null; // Clear the Updated Date
            // Hook-in to add more custom property assignments
            await ModelToNewObjectAdditionalPropertiesAsync(newEntity, model, timestamp, context).ConfigureAwait(false);
            // Return the new entity, ready for adding to the DB
            return newEntity;
        }
    }
}
