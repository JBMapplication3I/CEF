// <copyright file="AssociateObjectsWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate objects workflow base class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Utilities;

    /// <summary>An associate objects workflow base.</summary>
    /// <typeparam name="TIMasterModel"> Type of the master's model interface.</typeparam>
    /// <typeparam name="TIMasterEntity">Type of the master's entity interface.</typeparam>
    /// <typeparam name="TISlaveModel">  Type of the slave's model interface.</typeparam>
    /// <typeparam name="TISlaveEntity"> Type of the slave's entity interface.</typeparam>
    /// <typeparam name="TSlaveEntity">  Type of the slave's entity.</typeparam>
    /// <seealso cref="IAssociateObjectsWorkflowBase{TIMasterModel, TIMasterEntity}"/>
    public abstract class AssociateObjectsWorkflowBase<TIMasterModel, TIMasterEntity, TISlaveModel, TISlaveEntity, TSlaveEntity>
        : IAssociateObjectsWorkflowBase<TIMasterModel, TIMasterEntity>
        where TIMasterModel : IBaseModel
        where TIMasterEntity : IBase
        where TISlaveModel : IBaseModel
        where TISlaveEntity : IBase
        where TSlaveEntity : class, TISlaveEntity, new()
    {
        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        protected IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <inheritdoc/>
        public virtual async Task AssociateObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await AssociateObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task AssociateObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            // New Entity may not have the object list initialized yet
            InitializeObjectListIfNull(entity);
            // If the model doesn't have objects, deactivate any active objects and quit
            if (!ModelHasObjects(model))
            {
                await DeactivateAllActiveObjectsAsync(entity, timestamp).ConfigureAwait(false);
                return;
            }
            await DeactivateRemovedObjectsAsync(entity, model, timestamp).ConfigureAwait(false);
            await UpdateExistingObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await AddNewObjectsAsync(entity, model, timestamp, context).ConfigureAwait(false);
            await DeactivateReplacedObjectsByCustomKeyAsync(entity, timestamp).ConfigureAwait(false);
        }

        #region Override these
        // ReSharper disable BadDeclarationBracesLineBreaks, MissingLinebreak, MultipleStatementsOnOneLine

        /// <summary>Validates the object model is good for database described by model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected abstract bool ValidateObjectModelIsGoodForDatabase(TISlaveModel model);

        /// <summary>Validates the object model is good for database additional checks described by model.</summary>
        /// <param name="model">The model.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual bool ValidateObjectModelIsGoodForDatabaseAdditionalChecks(TISlaveModel model)
        {
            // Do Nothing by default
            return true;
        }

        /// <summary>Match object model with object entity.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="entity"> The entity.</param>
        /// <param name="context">The context.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected abstract Task<bool> MatchObjectModelWithObjectEntityAsync(
            TISlaveModel model,
            TISlaveEntity entity,
            IClarityEcommerceEntities context);

        /// <summary>Match object model with object entity additional checks.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="entity"> The entity.</param>
        /// <param name="context">The context.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        protected virtual Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            TISlaveModel model,
            TISlaveEntity entity,
            IClarityEcommerceEntities context)
        {
            // Do Nothing by default
            return Task.FromResult(true);
        }

        /// <summary>Gets objects collection.</summary>
        /// <param name="entity">The entity.</param>
        /// <returns>The objects collection.</returns>
        protected abstract ICollection<TSlaveEntity>? GetObjectsCollection(TIMasterEntity entity);

        /// <summary>Gets model objects list.</summary>
        /// <param name="model">The model.</param>
        /// <returns>The model objects list.</returns>
        protected abstract List<TISlaveModel>? GetModelObjectsList(TIMasterModel model);

        /// <summary>Adds an object to objects list to 'newObject'.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="newObject">The new object.</param>
        /// <returns>A Task.</returns>
        protected abstract Task AddObjectToObjectsListAsync(TIMasterEntity entity, TISlaveEntity newObject);

        /// <summary>Initializes the object list if null.</summary>
        /// <param name="entity">The entity.</param>
        protected abstract void InitializeObjectListIfNull(TIMasterEntity entity);

        /// <summary>Deactivate object.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        protected abstract Task DeactivateObjectAsync(TISlaveEntity entity, DateTime timestamp);

        /// <summary>Deactivate object additional properties.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        protected virtual Task DeactivateObjectAdditionalPropertiesAsync(TISlaveEntity entity, DateTime timestamp)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }

        /// <summary>Model to new object.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TISlaveEntity.</returns>
        protected abstract Task<TISlaveEntity> ModelToNewObjectAsync(
            TISlaveModel model,
            DateTime timestamp,
            string? contextProfileName);

        /// <summary>Model to new object.</summary>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A TISlaveEntity.</returns>
        protected abstract Task<TISlaveEntity> ModelToNewObjectAsync(
            TISlaveModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context);

        /// <summary>Model to new object additional properties.</summary>
        /// <param name="newEntity">         The new entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task ModelToNewObjectAdditionalPropertiesAsync(
            TISlaveEntity newEntity,
            TISlaveModel model,
            DateTime timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            await ModelToNewObjectAdditionalPropertiesAsync(
                    newEntity,
                    model,
                    timestamp,
                    context)
                .ConfigureAwait(false);
        }

        /// <summary>Model to new object additional properties.</summary>
        /// <param name="newEntity">The new entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected virtual Task ModelToNewObjectAdditionalPropertiesAsync(
            TISlaveEntity newEntity,
            TISlaveModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Do Nothing by default
            return Task.CompletedTask;
        }
        // ReSharper restore BadDeclarationBracesLineBreaks, MissingLinebreak, MultipleStatementsOnOneLine
        #endregion

        /// <summary>Deactivate removed objects.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task DeactivateRemovedObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp)
        {
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            _ = Contract.RequiresNotNull(GetObjectsCollection(entity));
            _ = Contract.RequiresNotNull(GetModelObjectsList(model));
            // Determine if any existing objects were removed and deactivate them
            var foundIDs = GetModelObjectsList(model)!
                .Where(m => m.ID > 0)
                .Select(m => GetObjectsCollection(entity)!.FirstOrDefault(pa => pa.ID == m.ID))
                .Where(existing => existing != null)
                .Select(existing => existing!.ID)
                .ToList();
            // Cannot do Task.WhenAll with a context as it's not thread safe
            foreach (var e in GetObjectsCollection(entity)!.Where(y => y.Active && !foundIDs.Contains(y.ID)))
            {
                await DeactivateObjectAsync(e, timestamp).ConfigureAwait(false);
            }
        }

        /// <summary>Query if 'model' has objects.</summary>
        /// <exception cref="ArgumentNullException">Thrown when one or more required arguments are null.</exception>
        /// <param name="model">The product model.</param>
        /// <returns>true if objects, false if not.</returns>
        private bool ModelHasObjects(TIMasterModel model)
        {
            return GetModelObjectsList(Contract.RequiresNotNull(model))?.Count > 0;
        }

        /// <summary>Validates the model and adds if valid.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private async Task ValidateAndAddAsync(
            TIMasterEntity entity,
            TISlaveModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            // Validate the model before trying to add it
            if (!ValidateObjectModelIsGoodForDatabase(model))
            {
                return;
            }
            var newEntity = await ModelToNewObjectAsync(model, timestamp, context).ConfigureAwait(false);
            await AddObjectToObjectsListAsync(entity, newEntity).ConfigureAwait(false);
            if (Contract.CheckValidKey(context.ContextProfileName))
            {
                // In tests, add it to the set so it initializes an ID properly
                Contract.RequiresNotNull(context.Set<TSlaveEntity>())
                    .Add((TSlaveEntity)newEntity);
            }
        }

        /// <summary>Deactivate all active objects.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        private async Task DeactivateAllActiveObjectsAsync(TIMasterEntity entity, DateTime timestamp)
        {
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(GetObjectsCollection(entity));
            // Cannot do Task.WhenAll with a context as it's not thread safe
            foreach (var e in GetObjectsCollection(entity)!.Where(y => y.Active))
            {
                await DeactivateObjectAsync(e, timestamp).ConfigureAwait(false);
            }
        }

        /// <summary>Updates the existing objects.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private async Task UpdateExistingObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            _ = Contract.RequiresNotNull(GetObjectsCollection(entity));
            _ = Contract.RequiresNotNull(GetModelObjectsList(model));
            // Determine if any objects provided by the model have IDs (they existed in the system previously)
            foreach (var m in GetModelObjectsList(model)!.Where(a => Contract.CheckValidID(a.ID)))
            {
                var e = GetObjectsCollection(entity)!.FirstOrDefault(pa => pa.ID == m.ID);
                if (e == null)
                {
                    continue;
                }
                // See if it matches
                if (await MatchObjectModelWithObjectEntityAsync(m, e, context).ConfigureAwait(false))
                {
                    continue;
                }
                // Deactivate the original and add a new one (preserve history)
                await DeactivateObjectAsync(e, timestamp).ConfigureAwait(false);
                await ValidateAndAddAsync(entity, m, timestamp, context).ConfigureAwait(false);
            }
        }

        /// <summary>Adds new objects.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        private async Task AddNewObjectsAsync(
            TIMasterEntity entity,
            TIMasterModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            _ = Contract.RequiresNotNull(entity);
            _ = Contract.RequiresNotNull(model);
            _ = Contract.RequiresNotNull(GetObjectsCollection(entity));
            _ = Contract.RequiresNotNull(GetModelObjectsList(model));
            // Determine if any objects provided by the model don't have IDs (they did not exist in the system previously)
            // Cannot do Task.WhenAll with a context as it's not thread safe
            foreach (var m in GetModelObjectsList(model)!.Where(a => !Contract.CheckValidID(a.ID)))
            {
                await ValidateAndAddAsync(entity, m, timestamp, context).ConfigureAwait(false);
            }
        }

        /// <summary>Deactivate replaced objects by custom key.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <returns>A Task.</returns>
        private Task DeactivateReplacedObjectsByCustomKeyAsync(TIMasterEntity entity, DateTime timestamp)
        {
            foreach (var group in GetObjectsCollection(entity)!
                                    .Where(x => Contract.CheckValidKey(x.CustomKey) && x.Active)
                                    .GroupBy(x => x.CustomKey)
                                    .Where(x => x.Count() > 1))
            {
                // Something was replaced but the older one still needs to be deactivated
                foreach (var item in group.OrderByDescending(x => x.CreatedDate).Skip(1))
                {
                    item.Active = false;
                    item.UpdatedDate = timestamp;
                }
            }
            return Task.CompletedTask;
        }
    }
}
