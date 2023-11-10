// <autogenerated>
// <copyright file="AccountFileWorkflow.generated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Workflow generated to provide base setups</summary>
// <remarks>This file was auto-generated by Workflows.tt, changes to this
// file will be overwritten automatically when the T4 template is run again</remarks>
// </autogenerated>
#nullable enable
// ReSharper disable ConvertToUsingDeclaration, InvertIf, ReturnValueOfPureMethodIsNotUsed, UnusedMember.Local
#pragma warning disable CS0618,CS1711,CS1572,CS1580,CS1581,CS1584
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Mapper;
    using Utilities;

    /// <summary>A workflow for AccountFile entities.</summary>
    /// <seealso cref="NameableWorkflowBase{IAccountFileModel, IAccountFileSearchModel, IAccountFile, AccountFile}"/>
    /// <seealso cref="IAccountFileWorkflow"/>
    public partial class AccountFileWorkflow
        : NameableWorkflowBase<IAccountFileModel, IAccountFileSearchModel, IAccountFile, AccountFile>
            , IAccountFileWorkflow
    {
        #region Mappers
        /// <inheritdoc/>
        protected override Func<AccountFile?, string?, IAccountFileModel?> MapFromConcreteFull
            => ModelMapperForAccountFile.MapAccountFileModelFromEntityFull;

        /// <inheritdoc/>
        protected override Func<IQueryable<AccountFile>, string?, IEnumerable<IAccountFileModel>> SelectLiteAndMapToModel
            => ModelMapperForAccountFile.SelectLiteAccountFileAndMapToAccountFileModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<AccountFile>, string?, IEnumerable<IAccountFileModel>> SelectListAndMapToModel
            => ModelMapperForAccountFile.SelectListAccountFileAndMapToAccountFileModel;

        /// <inheritdoc/>
        protected override Func<IQueryable<AccountFile>, string?, IAccountFileModel?> SelectFirstFullAndMapToModel
            => ModelMapperForAccountFile.SelectFirstFullAccountFileAndMapToAccountFileModel;

        /// <inheritdoc/>
        protected override Func<IAccountFile, IAccountFileModel, DateTime, DateTime?, IAccountFile> UpdateEntityFromModel
            => ModelMapperForAccountFile.UpdateAccountFileFromModel;
        #endregion

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForBySeoUrlResultAsync(string seoUrl, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForBySeoUrlResultAsync(seoUrl, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForBySeoUrlResultAsync(string seoUrl, IClarityEcommerceEntities context)
        {
            return await context.Set<AccountFile>()
                .AsNoTracking()
                .FilterBySeoUrl(Contract.RequiresValidKey(seoUrl), true, false)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IAccountFileModel?> GetBySeoUrlAsync(
            string seoUrl,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<IAccountFileModel?> GetBySeoUrlAsync(
            string seoUrl,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                SelectFirstFullAndMapToModel(
                    context.Set<AccountFile>()
                        .AsNoTracking()
                        .FilterBySeoUrl(Contract.RequiresValidKey(seoUrl), true, false),
                    context.ContextProfileName));
        }

        /// <inheritdoc/>
        public virtual async Task<int?> CheckExistsBySeoUrlAsync(string seoUrl, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckExistsBySeoUrlAsync(seoUrl, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<int?> CheckExistsBySeoUrlAsync(string seoUrl, IClarityEcommerceEntities context)
        {
            return context.Set<AccountFile>()
                .FilterByActive(true)
                .FilterBySeoUrl(Contract.RequiresValidKey(seoUrl), true, false)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync();
        }

        /// <summary>Gets entity by SEO URL.</summary>
        /// <param name="seoUrl"> URL of the SEO.</param>
        /// <param name="context">The context.</param>
        /// <returns>The entity by SEO URL.</returns>
        protected virtual Task<AccountFile> GetEntityBySeoUrlAsync(
            string seoUrl,
            IClarityEcommerceEntities context)
        {
            return context.Set<AccountFile>()
                .FilterByActive(true)
                .FilterBySeoUrl(Contract.RequiresValidKey(seoUrl), true, false)
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<AccountFile>> FilterQueryByModelExtensionAsync(
            IQueryable<AccountFile> query,
            IAccountFileSearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterAccountFilesBySearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IAccountFile entity,
            IAccountFileModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultAssociateWorkflowsAsync(
            IAccountFile entity,
            IAccountFileModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await Workflows.AssociateJsonAttributes.AssociateObjectsAsync(entity, model, context).ConfigureAwait(false);
            SetDefaultJsonAttributesIfNull(entity);
            // None to process
        }

        #region Relate Workflows
        /// <summary>Relate Required Slave.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">            The entity that has a Required Slave.</param>
        /// <param name="model">             The model that has a Required Slave.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        protected virtual async Task RelateRequiredSlaveAsync(
            IAccountFile entity,
            IAccountFileModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RelateRequiredSlaveAsync(
                    entity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Relate Required Slave.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="entity">   The entity that has a Required Slave.</param>
        /// <param name="model">    The model that has a Required Slave.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        // ReSharper disable once CyclomaticComplexity, CognitiveComplexity
        protected virtual async Task RelateRequiredSlaveAsync(
            IAccountFile entity,
            IAccountFileModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Must have the core objects on both sides
            Contract.RequiresNotNull(entity);
            Contract.RequiresNotNull(model);
            // Look up the Object Model
            // Allowed to auto-generate if not found
            var resolved = await Workflows.StoredFiles.ResolveWithAutoGenerateOptionalAsync(
                    byID: model.SlaveID, // By Other ID
                    byKey: model.SlaveKey, // By Flattened Other Key
                    byName: model.Slave?.Name, // By Name from the sub-object
                    model: model.Slave, // The other property as a model
                    context: context)
                .ConfigureAwait(false);
            // Check for IDs and objects
            var entityIDIsNull = !Contract.CheckValidID(entity.SlaveID);
            var modelIDIsNull = !Contract.CheckValidID(resolved.Result?.ID);
            var entityObjectIsNull = entity.Slave == null;
            if (!resolved.ActionSucceeded && model.Slave != null)
            {
                resolved.Result = model.Slave;
            }
            var modelObjectIsNull = !resolved.ActionSucceeded;
            if (modelIDIsNull && modelObjectIsNull)
            {
                // [Required] Scenario 1: Trying to put bad data in the database
                throw new ArgumentException("Cannot assign a null to a non-nullable SlaveID");
            }
            var entityIDAndModelIDHaveSameID = !entityIDIsNull && !modelIDIsNull && entity.SlaveID == resolved.Result!.ID;
            var entityObjectAndModelObjectHaveSameID = !entityObjectIsNull && !modelObjectIsNull && entity.SlaveID == resolved.Result!.ID;
            if (entityIDAndModelIDHaveSameID || entityObjectAndModelObjectHaveSameID)
            {
                // [Optional/Required] Scenario 2: They match IDs, just update the entity from the model if it is present
                if (!entityObjectIsNull && !modelObjectIsNull)
                {
                    entity.Slave!.UpdateStoredFileFromModel(resolved.Result!, timestamp, timestamp);
                }
                return;
            }
            if (!modelIDIsNull)
            {
                // [Optional/Required] Scenario 3: We have IDs but they don't match, assign the model's ID to the entity's ID
                entity.SlaveID = resolved.Result!.ID;
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
            var modelObjectIDIsNull = !modelObjectIsNull && !Contract.CheckValidID(resolved.Result!.ID);
            var modelObjectIsActive = !modelObjectIsNull && resolved.Result!.Active;
            if (!modelObjectIDIsNull && !modelObjectIsActive)
            {
                // [Required] Scenario 4: We have an ID on the model, but the object is inactive
                throw new InvalidOperationException("Cannot assign an inactive SlaveID to the AccountFile entity");
            }
            if (!modelObjectIDIsNull)
            {
                // [Required] Scenario 5: We have IDs but they don't match, assign the model's ID to the entity's ID (from the model object)
                entity.SlaveID = resolved.Result!.ID;
                return;
            }
            if (modelObjectIsActive)
            {
                // [Required] Scenario 7: We have an entity id, but a new model, remove the id on the entity and assign the new model
                entity.SlaveID = 0;
                entity.Slave = (StoredFile)resolved.Result!.CreateStoredFileEntity(timestamp, context.ContextProfileName);
                return;
            }
            // [Required] Scenarios 8,9 are only for Optional relationships, skipped
            // [Optional/Required] Scenario 10: Could not figure out what to do
            throw new InvalidOperationException(
                "Couldn't figure out how to relate the given Slave to the AccountFile entity");
        }

        /// <inheritdoc/>
        protected override async Task RunDefaultRelateWorkflowsAsync(
            IAccountFile entity,
            IAccountFileModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        // ReSharper disable AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
#pragma warning disable 1998
        protected override async Task RunDefaultRelateWorkflowsAsync(
#pragma warning restore 1998
            IAccountFile entity,
            IAccountFileModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // NOTE: Can't use Task.WhenAll as the context isn't thread safe and will throw an exception is more than
            // one thing is running at the same time on the same context.
            await RelateRequiredSlaveAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }
        // ReSharper restore AsyncConverter.AsyncAwaitMayBeElidedHighlighting, RedundantAwait
        #endregion // Relate Workflows
    }
}
