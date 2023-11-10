// <copyright file="ContactCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the contact workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    public partial class ContactWorkflow
    {
        /// <inheritdoc/>
        protected override bool OverrideDuplicateCheck => true;

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(IContactModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            model.TypeID = 1;
            model.Type = null;
            model.TypeKey = null;
            model.TypeName = null;
            return await CreateAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            IContact entity,
            IContactModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateContactFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            if (!Contract.CheckValidIDOrAnyValidKey(
                    model.Type?.ID ?? model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.Type?.CustomKey,
                    model.Type?.Name))
            {
                model.TypeKey = "Shipping";
            }
            // Related Objects
            await model.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.AssignPostPropertiesToContactAndAddress(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<CEFActionResponse> DeleteAsync(
            Contact? entity,
            IClarityEcommerceEntities context)
        {
            if (entity == null)
            {
                return CEFAR.PassingCEFAR();
            }
            await DeleteAssociatedImagesAsync<ContactImage>(entity.ID, context).ConfigureAwait(false);
            return await base.DeleteAsync(entity, context).ConfigureAwait(false);
        }
    }
}
