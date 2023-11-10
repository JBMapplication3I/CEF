// <copyright file="NameableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow for nameable bases class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
#if NET5_0_OR_GREATER
    using System.Data.Entity.Core;
#else
    using System.Data;
#endif
    using System.Data.Entity;
    using System.IO;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Utilities;

    /// <summary>A workflow for nameable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="WorkflowBase{TIModel, TISearchModel, TIEntity, TEntity}"/>
    /// <seealso cref="INameableWorkflowBase{TIModel, TISearchModel, TIEntity, TEntity}"/>
    public abstract class NameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : WorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>,
            INameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, INameableBaseModel
        where TISearchModel : INameableBaseSearchModel
        where TIEntity : INameableBase
        where TEntity : class, TIEntity, new()
    {
        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForByNameResultAsync(
            string name,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForByNameResultAsync(name, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForByNameResultAsync(
            string name,
            IClarityEcommerceEntities context)
        {
            return await context.Set<TEntity>()
                .FilterByActive(true)
                .FilterByName(name, true)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<TIModel?> GetByNameAsync(string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetByNameAsync(name, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<TIModel?> GetByNameAsync(string name, IClarityEcommerceEntities context)
        {
            var entity = await GetEntityByNameAsync(Contract.RequiresValidKey(name), context).ConfigureAwait(false);
            return entity == null ? null : MapFromConcreteFull(entity, context.ContextProfileName);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> CheckExistsByNameAsync(string name, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckExistsByNameAsync(name, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<int?> CheckExistsByNameAsync(string name, IClarityEcommerceEntities context)
        {
            return context.Set<TEntity>()
                .FilterByName(Contract.RequiresValidKey(name), true)
                .OrderByDescending(x => x.Active)
                .ThenBy(x => x.ID)
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveAsync(byID, byKey, byName, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            var existing = await ResolveAsync(
                    byID: byID,
                    byKey: byKey,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (Contract.CheckValidKey(byName))
            {
                var attempt = await GetByNameAsync(byName!, context).ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            // ReSharper disable once InvertIf
            if (model != null && Contract.CheckValidKey(model.Name))
            {
                var attempt = await GetByNameAsync(model.Name!, context).ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            if (isInner)
            {
                return CEFAR.FailingCEFAR<TIModel>();
            }
            throw new ObjectNotFoundException(
                "ERROR! Could not locate a required record with the provided information.");
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveToIDAsync(byID, byKey, byName, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            var existing = await ResolveToIDAsync(
                    byID: byID,
                    byKey: byKey,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (Contract.CheckValidKey(byName))
            {
                var attempt = await CheckExistsByNameAsync(byName!, context).ConfigureAwait(false) ?? 0;
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.Name))
            {
                var attempt = await CheckExistsByNameAsync(model!.Name!, context).ConfigureAwait(false) ?? 0;
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            if (isInner)
            {
                return 0;
            }
            throw new ObjectNotFoundException(
                "ERROR! Must supply information that identifies an existing record.");
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveToIDOptionalAsync(byID, byKey, byName, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveToIDOptionalAsync(byID, byKey, model, context).ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (Contract.CheckValidKey(byName))
            {
                var attempt = await CheckExistsByNameAsync(byName!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.Name))
            {
                var attempt = await CheckExistsByNameAsync(model!.Name!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            return null;
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateToIDOptionalAsync(byID, byKey, byName, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidID(byID))
            {
                var attempt = await CheckExistsAsync(byID!.Value, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            if (Contract.CheckValidKey(byKey))
            {
                var attempt = await CheckExistsAsync(byKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            if (Contract.CheckValidID(model?.ID))
            {
                var attempt = await CheckExistsAsync(model!.ID, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            if (Contract.CheckValidKey(model?.CustomKey))
            {
                var attempt = await CheckExistsAsync(model!.CustomKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            if (Contract.CheckValidKey(model?.Name))
            {
                var attempt = await CheckExistsByNameAsync(model!.Name!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt;
                }
            }
            if (model == null)
            {
                return null;
            }
            var createResponse = await CreateAsync(model, context).ConfigureAwait(false);
            return createResponse.ActionSucceeded ? createResponse.Result : null;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (model == null
                && (model = await GetInstanceAsync(byKey, byName, context.ContextProfileName).ConfigureAwait(false)) == null)
            {
                throw new InvalidDataException("Unable to auto-generate object entity with the provided information");
            }
            return (await GetAsync(
                    (await CreateAsync(model!, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false)).Result,
                    context)
                .ConfigureAwait(false))!
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateOptionalAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing!;
            }
            if (model == null
                && (model = await GetInstanceAsync(byKey, byName, context.ContextProfileName).ConfigureAwait(false)) == null)
            {
                return CEFAR.FailingCEFAR<TIModel?>();
            }
            var createResponse = await CreateAsync(model!, context).ConfigureAwait(false);
            if (!createResponse.ActionSucceeded)
            {
                return createResponse.ChangeFailingCEFARType<TIModel?>();
            }
            return (await GetAsync(createResponse.Result, context).ConfigureAwait(false)).WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateToIDAsync(byID, byKey, byName, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveToIDAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (model == null
                && (model = await GetInstanceAsync(byKey, byName, context.ContextProfileName).ConfigureAwait(false)) == null)
            {
                throw new InvalidDataException(
                    "Unable to auto-generate object entity with the provided information");
            }
            var createResponse = await CreateAsync(model, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false);
            return Contract.RequiresValidID(createResponse.Result);
        }

        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> UpsertAsync(
            TIModel model,
            IClarityEcommerceEntities context)
        {
            int? id = null;
            var exists = Contract.CheckValidID(model.ID)
                   && Contract.CheckValidID(id = await CheckExistsAsync(model.ID, context).ConfigureAwait(false))
                || Contract.CheckValidKey(model.CustomKey)
                   && Contract.CheckValidID(id = await CheckExistsAsync(model.CustomKey!, context).ConfigureAwait(false))
                || Contract.CheckValidKey(model.Name)
                   && Contract.CheckValidID(id = await CheckExistsByNameAsync(model.Name!, context).ConfigureAwait(false));
            if (exists && !Contract.CheckValidID(model.ID))
            {
                model.ID = id!.Value;
            }
            return await (exists ? UpdateAsync(model, context) : CreateAsync(model, context)).ConfigureAwait(false);
        }

        /// <summary>Gets an instance.</summary>
        /// <param name="byKey">             The key.</param>
        /// <param name="byName">            The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The instance.</returns>
        protected static Task<TIModel?> GetInstanceAsync(string? byKey, string? byName, string? contextProfileName)
        {
            if (!Contract.CheckAnyValidKey(byKey, byName))
            {
                return Task.FromResult<TIModel?>(null);
            }
            var model = (TIModel)RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance(typeof(TIModel));
            model.Active = true;
            model.CustomKey = byKey ?? byName;
            // ReSharper disable once ConstantNullCoalescingCondition
            model.Name = byName ?? byKey;
            return Task.FromResult<TIModel?>(model);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<TEntity>> FilterQueryByModelExtensionAsync(
            IQueryable<TEntity> query,
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterByNameableBaseSearchModel(search);
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            await base.AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            entity.Name = model.Name;
            entity.Description = model.Description;
        }

        /// <summary>Gets entity by name.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="context">The context.</param>
        /// <returns>The entity by name.</returns>
        protected virtual Task<TEntity?> GetEntityByNameAsync(string name, IClarityEcommerceEntities context)
        {
            return context.Set<TEntity>()
                .FilterByActive(true)
                .FilterByName(Contract.RequiresValidKey(name), true)
                .OrderBy(x => x.ID)
                .FirstOrDefaultAsync()!;
        }
    }
}
