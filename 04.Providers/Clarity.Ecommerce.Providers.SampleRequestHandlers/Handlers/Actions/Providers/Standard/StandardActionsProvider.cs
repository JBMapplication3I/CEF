// <copyright file="StandardActionsProvider.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the standard actions provider class</summary>
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Standard
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Mapper;
    using Models;
    using Utilities;

    /// <summary>A standard sample request actions provider.</summary>
    /// <seealso cref="SampleRequestActionsProviderBase"/>
    public class StandardSampleRequestActionsProvider : SampleRequestActionsProviderBase
    {
        /// <inheritdoc/>
        public override bool HasValidConfiguration
            => StandardSampleRequestActionsProviderConfig.IsValid(IsDefaultProvider && IsDefaultProviderActivated);

        /// <inheritdoc/>
        public override bool IsDefaultProvider => true;

        /// <inheritdoc/>
        public override async Task<ISampleRequestModel> CreateViaCheckoutProcessAsync(
            ICheckoutModel checkout,
            ICartModel cart,
            IUserModel user,
            string? contextProfileName)
        {
            Contract.RequiresNotNull(checkout.Billing);
            var timestamp = DateExtensions.GenDateTime;
            var billingContact = (Contact)checkout.Billing!.CreateContactEntity(timestamp, contextProfileName);
            // ReSharper disable once PossibleInvalidOperationException
            billingContact.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Billing",
                    byName: "Billing",
                    byDisplayName: "Billing",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var shippingContact = checkout.IsSameAsBilling
                ? billingContact
                : (Contact)checkout.Shipping!.CreateContactEntity(timestamp, contextProfileName);
            // ReSharper disable once PossibleInvalidOperationException
            shippingContact.TypeID = await Workflows.ContactTypes.ResolveWithAutoGenerateToIDAsync(
                    byID: null,
                    byKey: "Shipping",
                    byName: "Shipping",
                    byDisplayName: "Shipping",
                    model: null,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            var newSampleRequest = new SampleRequest
            {
                StatusID = 1, // TODO@BE: No Magic Numbers! Resolve to the "Processing" request status ID
                BillingContact = billingContact,
                ShippingContact = shippingContact,
                CreatedDate = timestamp,
                Active = true,
                UserID = user?.ID,
                AccountID = user?.AccountID,
            };
            await RelateRequiredTypeAsync(newSampleRequest, new SampleRequestModel { Type = new() { CustomKey = "Web", Name = "Web", DisplayName = "Web" } }, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStatusAsync(newSampleRequest, new SampleRequestModel { Status = new() { CustomKey = "Pending", Name = "Pending", DisplayName = "Pending" } }, timestamp, contextProfileName).ConfigureAwait(false);
            await RelateRequiredStateAsync(newSampleRequest, new SampleRequestModel { State = new() { CustomKey = "WORK", Name = "Work", DisplayName = "Work" } }, timestamp, contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            context.SampleRequests.Add(newSampleRequest);
            await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            // Remove Items from cart
            foreach (var cartItem in cart.SalesItems!.Where(x => x.ItemType == Enums.ItemType.Item))
            {
                await TransferCartItemsToSampleRequestItemsAsync(timestamp, newSampleRequest, cartItem, contextProfileName).ConfigureAwait(false);
            }
            // Transfer contacts from cart to quote
            foreach (var contact in cart.Contacts!)
            {
                newSampleRequest.Contacts ??= new List<SampleRequestContact>();
                newSampleRequest.Contacts.Add(new()
                {
                    SlaveID = contact.ContactID,
                    Active = contact.Active,
                    CreatedDate = contact.CreatedDate,
                });
            }
            // Copy the notes from the Cart
            var dummyOrderModel = new SampleRequestModel();
            if (dummyOrderModel.Notes != null)
            {
                if (Contract.CheckValidID(newSampleRequest.ID))
                {
                    foreach (var note in dummyOrderModel.Notes)
                    {
                        note.SampleRequestID = newSampleRequest.ID;
                    }
                }
                await Workflows.SampleRequestWithNotesAssociation.AssociateObjectsAsync(newSampleRequest, dummyOrderModel, timestamp, contextProfileName).ConfigureAwait(false);
            }
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(newSampleRequest, dummyOrderModel, contextProfileName).ConfigureAwait(false);
            if (string.IsNullOrWhiteSpace(newSampleRequest.JsonAttributes))
            {
                newSampleRequest.JsonAttributes = "{}";
            }
            if (!string.IsNullOrWhiteSpace(checkout.SpecialInstructions))
            {
                newSampleRequest.Notes!.Add(new()
                {
                    Active = true,
                    CreatedDate = timestamp,
                    TypeID = 3,
                    CreatedByUserID = user?.ID,
                    Note1 = checkout.SpecialInstructions,
                });
            }
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            return (await Workflows.SampleRequests.GetAsync(newSampleRequest.ID, contextProfileName).ConfigureAwait(false))!;
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<ISalesInvoiceModel>> AddPaymentAsync(
            int id,
            IPaymentModel payment,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse<ISalesInvoiceModel>> CreateInvoiceForAsync(
            int id,
            string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> CreatePickTicketForAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsBackorderedAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsCompletedAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsConfirmedAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsDropShippedAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsShippedAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc/>
        public override Task<CEFActionResponse> SetRecordAsVoidedAsync(int id, string? contextProfileName)
        {
            throw new NotImplementedException();
        }

        /// <summary>Transfer cart items to sample request items.</summary>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="newSampleRequest">  The new sample request.</param>
        /// <param name="cartItem">          The cart item.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private static async Task TransferCartItemsToSampleRequestItemsAsync(
            DateTime timestamp,
            IBase newSampleRequest,
            ISalesItemBaseModel cartItem,
            string? contextProfileName)
        {
            var sampleRequestItem = new SampleRequestItem
            {
                Active = true,
                CreatedDate = timestamp,
                UpdatedDate = timestamp,
                MasterID = newSampleRequest.ID,
                ProductID = cartItem.ProductID,
                Name = cartItem.ProductName,
                Sku = cartItem.ProductKey,
                ForceUniqueLineItemKey = cartItem.ForceUniqueLineItemKey,
                Quantity = cartItem.Quantity,
                QuantityBackOrdered = cartItem.QuantityBackOrdered ?? 0m,
                QuantityPreSold = cartItem.QuantityPreSold ?? 0m,
                UnitOfMeasure = cartItem.UnitOfMeasure,
            };
            // Transfer shipment from cartItem to salesItem
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(sampleRequestItem, cartItem, contextProfileName).ConfigureAwait(false);
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                context.SampleRequestItems.Add(sampleRequestItem);
                await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            }
            await Workflows.CartItems.DeactivateAsync(cartItem.ID, contextProfileName).ConfigureAwait(false);
        }

        /// <summary>Relate Required Status.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Status.</param>
        /// <param name="model">             The model that has a Required Status.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredStatusAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredStatusAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Status.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Status.</param>
        /// <param name="model">    The model that has a Required Status.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredStatusAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SampleRequestStatuses.ResolveWithAutoGenerateAsync(
                    byID: model.StatusID, // By Other ID
                    byKey: model.StatusKey, // By Flattened Other Key
                    byName: model.StatusName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Status,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StatusID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Status == null;
            if (resolved.Result == null && model.Status != null)
            {
                resolved.Result = model.Status;
            }
            var modelObjectIsNull = resolved == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable StatusID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StatusID == resolved!.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Status!.ID == resolved!.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Status!.UpdateSampleRequestStatusFromModel(resolved!.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StatusID = resolved!.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved!.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved!.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive StatusID to the SampleRequest entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.StatusID = resolved!.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StatusID = 0;
                entity.Status = (SampleRequestStatus)resolved!.Result!.CreateSampleRequestStatusEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Status to the SampleRequest entity");
        }

        /// <summary>Relate Required State.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required State.</param>
        /// <param name="model">             The model that has a Required State.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredStateAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredStateAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required State.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required State.</param>
        /// <param name="model">    The model that has a Required State.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredStateAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SampleRequestStates.ResolveWithAutoGenerateAsync(
                    byID: model.StateID, // By Other ID
                    byKey: model.StateKey, // By Flattened Other Key
                    byName: model.StateName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.State,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.StateID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.State == null;
            if (resolved.Result == null && model.State != null)
            {
                resolved.Result = model.State;
            }
            var modelObjectIsNull = resolved == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable StateID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.StateID == resolved!.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.State!.ID == resolved!.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.State!.UpdateSampleRequestStateFromModel(resolved!.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.StateID = resolved!.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved!.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved!.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive StateID to the SampleRequest entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.StateID = resolved!.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.StateID = 0;
                entity.State = (SampleRequestState)resolved!.Result!.CreateSampleRequestStateEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given State to the SampleRequest entity");
        }

        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Type.</param>
        /// <param name="model">             The model that has a Required Type.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        private static async Task RelateRequiredTypeAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredTypeAsync(
                    entity: entity,
                    model: model,
                    timestamp: timestamp,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Type.</summary>
        /// <exception cref="ArgumentNullException">    Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Type.</param>
        /// <param name="model">    The model that has a Required Type.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        private static async Task RelateRequiredTypeAsync(
            ISampleRequest entity,
            ISampleRequestModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.SampleRequestTypes.ResolveWithAutoGenerateAsync(
                    byID: model.TypeID, // By Other ID
                    byKey: model.TypeKey, // By Flattened Other Key
                    byName: model.TypeName, // By Flattened Other Name
                    byDisplayName: null, // Skip DisplayName as it's not normally part of the interface
                    model: model.Type,
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.TypeID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Type == null;
            if (resolved.Result == null && model.Type != null)
            {
                resolved.Result = model.Type;
            }
            var modelObjectIsNull = resolved == null;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable TypeID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.TypeID == resolved!.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.Type!.ID == resolved!.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Type!.UpdateSampleRequestTypeFromModel(resolved!.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.TypeID = resolved!.Result!.ID;
                if (!modelObjectIsNull)
                {
                    if (!entityObjectIsNull)
                    {
                        // [Optional/Required] Scenario 3a: We can't update the existing object because it's the wrong one
                    }
                    else
                    {
                        // [Optional/Required] Scenario 3b: We can't assign a new object in on this entity because it would duplicate the record
                    }
                }
                return;
            }
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved!.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved!.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive TypeID to the SampleRequest entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.TypeID = resolved!.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.TypeID = 0;
                entity.Type = (SampleRequestType)resolved!.Result!.CreateSampleRequestTypeEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Type to the SampleRequest entity");
        }
    }
}
