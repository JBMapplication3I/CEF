// <copyright file="DisplayableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow for displayable bases class</summary>
#nullable enable
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

    /// <summary>A workflow for displayable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="NameableWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public abstract class DisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : NameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>,
            IDisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, IDisplayableBaseModel
        where TISearchModel : IDisplayableBaseSearchModel
        where TIEntity : IDisplayableBase
        where TEntity : class, TIEntity, new()
    {
        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForByDisplayNameResultAsync(
            string displayName,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForByNameResultAsync(displayName, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForByDisplayNameResultAsync(
            string displayName,
            IClarityEcommerceEntities context)
        {
            return await context.Set<TEntity>()
                .FilterByActive(true)
                .FilterDisplayablesByDisplayName(displayName, true, false)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TIModel?> GetByDisplayNameAsync(string displayName, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetByDisplayNameAsync(displayName, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<TIModel?> GetByDisplayNameAsync(string displayName, IClarityEcommerceEntities context)
        {
            var entity = await GetEntityByDisplayNameAsync(
                    Contract.RequiresValidKey(displayName),
                    context)
                .ConfigureAwait(false);
            return entity is null ? null : MapFromConcreteFull(entity, context.ContextProfileName);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> CheckExistsByDisplayNameAsync(
            string displayName,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckExistsByDisplayNameAsync(displayName, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<int?> CheckExistsByDisplayNameAsync(
            string displayName,
            IClarityEcommerceEntities context)
        {
            return context.Set<TEntity>()
                .AsNoTracking()
                .FilterByActive(true)
                .FilterDisplayablesByDisplayName(Contract.RequiresValidKey(displayName), true, false)
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            var existing = await ResolveAsync(byID, byKey, byName, model, context, isInner: true).ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (Contract.CheckValidKey(byDisplayName))
            {
                var attempt = await GetByDisplayNameAsync(byDisplayName!, context).ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.DisplayName))
            {
                var attempt = await GetByDisplayNameAsync(model!.DisplayName!, context).ConfigureAwait(false);
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
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveToIDAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            var existing = await ResolveToIDAsync(byID, byKey, byName, model, context, isInner: true).ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (Contract.CheckValidKey(byDisplayName))
            {
                var attempt = await CheckExistsByDisplayNameAsync(byDisplayName!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.DisplayName))
            {
                var attempt = await CheckExistsByDisplayNameAsync(model!.DisplayName!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (isInner)
            {
                return 0;
            }
            throw new ArgumentException("ERROR! Must supply information about an existing record.");
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (model is null
                && (model = await GetInstanceAsync(byKey, byName, byDisplayName, context.ContextProfileName).ConfigureAwait(false)) is null)
            {
                throw new InvalidDataException("Unable to auto-generate object entity with the provided information");
            }
            model!.Active = true;
            return (await GetAsync(
                        (await CreateAsync(model, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false)).Result,
                        context)
                    .ConfigureAwait(false))!
                .WrapInPassingCEFAR()!;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing!;
            }
            if (model is null
                && (model = await GetInstanceAsync(byKey, byName, byDisplayName, context.ContextProfileName).ConfigureAwait(false)) is null)
            {
                return CEFAR.FailingCEFAR<TIModel?>();
            }
            model!.Active = true;
            var createResponse = await CreateAsync(model, context).ConfigureAwait(false);
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
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateToIDAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveToIDAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context,
                    isInner: true)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (model is null
                && (model = await GetInstanceAsync(byKey, byName, byDisplayName, context.ContextProfileName).ConfigureAwait(false)) is null)
            {
                throw new InvalidDataException("Unable to auto-generate object entity with the provided information");
            }
            model.Active = true;
            return Contract.RequiresValidID(
                (await CreateAsync(model, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false)).Result);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateToIDOptionalAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveToIDAsync(
                    byID: byID,
                    byKey: byKey,
                    byName: byName,
                    byDisplayName: byDisplayName,
                    model: model,
                    context: context)
                .ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (model is null
                && (model = await GetInstanceAsync(byKey, byName, byDisplayName, context.ContextProfileName).ConfigureAwait(false)) is null)
            {
                return null;
            }
            model.Active = true;
            var createResponse = await CreateAsync(model, context).ConfigureAwait(false);
            if (!createResponse.ActionSucceeded)
            {
                return null;
            }
            return createResponse.Result;
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
                && Contract.CheckValidID(id = await CheckExistsByNameAsync(model.Name!, context).ConfigureAwait(false))
                || Contract.CheckValidKey(model.DisplayName)
                && Contract.CheckValidID(id = await CheckExistsByDisplayNameAsync(model.DisplayName!, context).ConfigureAwait(false));
            if (exists && !Contract.CheckValidID(model.ID))
            {
                model.ID = id!.Value;
            }
            return await (exists ? UpdateAsync(model, context) : CreateAsync(model, context)).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<TEntity>> FilterQueryByModelExtensionAsync(
            IQueryable<TEntity> query,
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await base.FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .FilterDisplayablesByDisplayName(
                    search.DisplayName,
                    search.DisplayNameStrict,
                    search.DisplayNameIncludeNull);
        }

        /// <summary>Gets an instance.</summary>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="byDisplayName">     Name of the by display.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The instance.</returns>
        private static async Task<TIModel?> GetInstanceAsync(
            string? byKey,
            string? byName,
            string? byDisplayName,
            string? contextProfileName)
        {
            var model = await GetInstanceAsync(byKey, byName, contextProfileName).ConfigureAwait(false);
            if (model is null && !Contract.CheckValidKey(byDisplayName))
            {
                return null;
            }
            if (model is null)
            {
                model = (TIModel)RegistryLoader.NamedContainerInstance(contextProfileName).GetInstance(typeof(TIModel));
                model.Active = true;
                model.CustomKey = byKey ?? byName ?? byDisplayName;
                model.Name = byName ?? byDisplayName ?? byKey;
            }
            model.DisplayName = byDisplayName;
            return model;
        }

        /// <summary>Gets entity by display name.</summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="context">    The context.</param>
        /// <returns>The entity by display name.</returns>
        private static Task<TEntity> GetEntityByDisplayNameAsync(string displayName, IDbContext context)
        {
            return context.Set<TEntity>()
                .FilterByActive(true)
                .FilterDisplayablesByDisplayName(Contract.RequiresValidKey(displayName), true, false)
                .FirstOrDefaultAsync();
        }
    }
}
