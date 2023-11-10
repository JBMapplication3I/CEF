// <copyright file="WorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the workflow base class</summary>
// ReSharper disable StyleCop.SA1201, StyleCop.SA1202
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Core;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using Models;
    using Utilities;

    /// <summary>A workflow for bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="IWorkflowBaseHasAll{TIModel, TISearchModel, TIEntity, TEntity}"/>
    public abstract class WorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : IWorkflowBaseHasAll<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : class, IBaseModel
        where TISearchModel : IBaseSearchModel
        where TIEntity : IBase
        where TEntity : class, TIEntity, new()
    {
        /// <summary>Gets or sets the on record updated hook.</summary>
        /// <value>The on record updated hook.</value>
        protected static Func<TIModel, IClarityEcommerceEntities, Task>? OnRecordUpdatedAsyncHook { get; set; }

        /// <summary>Gets the logger.</summary>
        /// <value>The logger.</value>
        protected ILogger Logger { get; }
            = RegistryLoaderWrapper.GetInstance<ILogger>();

        /// <summary>Gets the workflows.</summary>
        /// <value>The workflows.</value>
        protected IWorkflowsController Workflows { get; }
            = RegistryLoaderWrapper.GetInstance<IWorkflowsController>();

        /// <summary>Gets a value indicating whether the override duplicate check.</summary>
        /// <value>True if override duplicate check, false if not.</value>
        protected virtual bool OverrideDuplicateCheck => false;

        /// <summary>Gets the map from concrete full.</summary>
        /// <value>The map from concrete full.</value>
        protected abstract Func<TEntity?, string?, TIModel?> MapFromConcreteFull { get; }

        /// <summary>Gets the select lite and map to model.</summary>
        /// <value>The select lite and map to model.</value>
        protected abstract Func<IQueryable<TEntity>, string?, IEnumerable<TIModel>> SelectLiteAndMapToModel { get; }

        /// <summary>Gets the select list and map to model.</summary>
        /// <value>The select list and map to model.</value>
        protected abstract Func<IQueryable<TEntity>, string?, IEnumerable<TIModel>> SelectListAndMapToModel { get; }

        /// <summary>Gets the select first full and map to model.</summary>
        /// <value>The select first full and map to model.</value>
        protected abstract Func<IQueryable<TEntity>, string?, TIModel?> SelectFirstFullAndMapToModel { get; }

        /// <summary>Gets the update entity from model.</summary>
        /// <value>The update entity from model.</value>
        protected abstract Func<TIEntity, TIModel, DateTime, DateTime?, TIEntity> UpdateEntityFromModel { get; }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForResultAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultAsync(int id, IClarityEcommerceEntities context)
        {
            return await context.Set<TEntity>()
                .FilterByActive(true)
                .FilterByID(id)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForResultAsync(key, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultAsync(string key, IClarityEcommerceEntities context)
        {
            return await context.Set<TEntity>()
                .FilterByActive(true)
                .FilterByCustomKey(key, true)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<TIModel?> GetAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<TIModel?> GetAsync(int id, IClarityEcommerceEntities context)
        {
            //return Task.FromResult(
            //    SelectFirstFullAndMapToModel(
            //        context.Set<TEntity>()
            //            // .AsNoTracking() Prevent an error, TODO: Research fixes for this
            //            .FilterByID(Contract.RequiresValidID(id)),
            //        context.ContextProfileName))!;
            var testing = SelectFirstFullAndMapToModel(context.Set<TEntity>().FilterByID(Contract.RequiresValidID(id)), context.ContextProfileName);
            return Task.FromResult(testing);
        }

        /// <inheritdoc/>
        public virtual async Task<TIModel?> GetAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetAsync(key, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<TIModel?> GetAsync(string key, IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                SelectFirstFullAndMapToModel(
                    context.Set<TEntity>()
                        // .AsNoTracking() Prevent an error, TODO: Research fixes for this
                        .FilterByActive(true)
                        .FilterByCustomKey(Contract.RequiresValidKey(key), true),
                    context.ContextProfileName))!;
        }

        /// <inheritdoc/>
        public virtual async Task<int?> CheckExistsAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckExistsAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<int?> CheckExistsAsync(int id, IClarityEcommerceEntities context)
        {
            return context.Set<TEntity>()
                .FilterByID(Contract.RequiresValidID(id))
                .Select(x => (int?)x.ID)
                .SingleOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<int?> CheckExistsAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CheckExistsAsync(key, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual Task<int?> CheckExistsAsync(string key, IClarityEcommerceEntities context)
        {
            return context.Set<TEntity>()
                .FilterByActive(true)
                .FilterByCustomKey(Contract.RequiresValidKey(key), true)
                .Select(x => (int?)x.ID)
                .FirstOrDefaultAsync();
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultSetAsync(
            TISearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetLastModifiedForResultSetAsync(search, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<DateTime?> GetLastModifiedForResultSetAsync(
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            var query = context.Set<TEntity>().AsNoTracking().AsQueryable();
            return await (await FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                .Select(x => x.UpdatedDate ?? x.CreatedDate)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<(List<TIModel> results, int totalPages, int totalCount)> SearchAsync(
            TISearchModel search,
            bool asListing,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await SearchAsync(search, asListing, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<(List<TIModel> results, int totalPages, int totalCount)> SearchAsync(
            TISearchModel search,
            bool asListing,
            IClarityEcommerceEntities context)
        {
            (List<TIModel> results, int totalPages, int totalCount) retVal;
            var query = context.Set<TEntity>().AsQueryable();
            query = (await FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .ApplySorting(search.Sorts, search.Groupings, context.ContextProfileName)
                .FilterByPaging(search.Paging, out retVal.totalPages, out retVal.totalCount);
            retVal.results = (asListing
                    ? SelectListAndMapToModel(query, context.ContextProfileName)
                    : SelectLiteAndMapToModel(query, context.ContextProfileName))
                .ToList();
            return retVal;
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TIModel>> SearchForConnectAsync(
            TISearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await SearchForConnectAsync(search, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<IEnumerable<TIModel>> SearchForConnectAsync(
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            return (await SearchAsync(search, true, context).ConfigureAwait(false)).results;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            if (Contract.CheckValidID(byID))
            {
                var attempt = await GetAsync(byID!.Value, context).ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            if (Contract.CheckValidKey(byKey))
            {
                var attempt = await GetAsync(byKey!, context).ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            if (Contract.CheckValidID(model?.ID))
            {
                var attempt = await GetAsync(model!.ID, context).ConfigureAwait(false);
                if (attempt is not null)
                {
                    return attempt.WrapInPassingCEFAR()!;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.CustomKey))
            {
                var attempt = await GetAsync(model!.CustomKey!, context).ConfigureAwait(false);
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
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveToIDAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false)
        {
            if (Contract.CheckValidID(byID))
            {
                var attempt = await CheckExistsAsync(byID!.Value, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidKey(byKey))
            {
                var attempt = await CheckExistsAsync(byKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidID(model?.ID))
            {
                var attempt = await CheckExistsAsync(model!.ID, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.CustomKey))
            {
                var attempt = await CheckExistsAsync(model!.CustomKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (isInner)
            {
                return 0;
            }
            return 0;
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveToIDOptionalAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidID(byID))
            {
                var attempt = await CheckExistsAsync(byID!.Value, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidKey(byKey))
            {
                var attempt = await CheckExistsAsync(byKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidID(model?.ID))
            {
                var attempt = await CheckExistsAsync(model!.ID, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            // ReSharper disable once InvertIf
            if (Contract.CheckValidKey(model?.CustomKey))
            {
                var attempt = await CheckExistsAsync(model!.CustomKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            return null;
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateToIDOptionalAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            if (Contract.CheckValidID(byID))
            {
                var attempt = await CheckExistsAsync(byID!.Value, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidKey(byKey))
            {
                var attempt = await CheckExistsAsync(byKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidID(model?.ID))
            {
                var attempt = await CheckExistsAsync(model!.ID, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (Contract.CheckValidKey(model?.CustomKey))
            {
                var attempt = await CheckExistsAsync(model!.CustomKey!, context).ConfigureAwait(false);
                if (Contract.CheckValidID(attempt))
                {
                    return attempt!.Value;
                }
            }
            if (model is null)
            {
                return null;
            }
            var createdResponse = await CreateAsync(model, context).ConfigureAwait(false);
            return createdResponse.ActionSucceeded ? createdResponse.Result : null;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(byID, byKey, model, context, isInner: true).ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing;
            }
            if (model is null)
            {
                throw new System.IO.InvalidDataException(
                    "Unable to auto-generate object entity with the provided information");
            }
            return await GetAsync(
                    (await CreateAsync(model, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false)).Result,
                    context)
                .AwaitAndWrapResultInPassingCEFARAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateOptionalAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveAsync(byID, byKey, model, context, isInner: true).ConfigureAwait(false);
            if (existing.ActionSucceeded)
            {
                return existing!;
            }
            if (model is null)
            {
                return CEFAR.FailingCEFAR<TIModel?>("WARNING! Unable to auto-generate with the provided information");
            }
            var createResponse = await CreateAsync(model, context).ConfigureAwait(false);
            if (!createResponse.ActionSucceeded)
            {
                return createResponse.ChangeFailingCEFARType<TIModel?>();
            }
            return await GetAsync(createResponse.Result, context).AwaitAndWrapResultInPassingCEFARIfNotNullAsync().ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ResolveWithAutoGenerateToIDAsync(byID, byKey, model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context)
        {
            var existing = await ResolveToIDAsync(byID, byKey, model, context).ConfigureAwait(false);
            if (Contract.CheckValidID(existing))
            {
                return existing;
            }
            if (model is null)
            {
                throw new System.IO.InvalidDataException(
                    "Unable to auto-generate object entity with the provided information");
            }
            return (await CreateAsync(model, context).AwaitAndThrowIfFailedAsync().ConfigureAwait(false)).Result;
        }

        /// <inheritdoc/>
        public async Task<List<IDigestModel>> GetDigestAsync(string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return (await context.Set<TEntity>()
                    .AsNoTracking()
                    .FilterByActive(true)
                    .Where(x => x.CustomKey != null)
                    .Select(x => new { x.ID, x.CustomKey, Hash = x.Hash ?? 0 })
                    .ToListAsync()
                    .ConfigureAwait(false))
                .Select(x => new DigestModel { ID = x.ID, Key = x.CustomKey, Hash = x.Hash })
                .ToList<IDigestModel>();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TEntity?>> CreateEntityWithoutSavingAsync(
            TIModel model,
            DateTime? timestamp,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CreateEntityWithoutSavingAsync(model, timestamp, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<TEntity?>> CreateEntityWithoutSavingAsync(
            TIModel model,
            DateTime? timestamp,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            timestamp ??= DateExtensions.GenDateTime;
            var entity = RegistryLoaderWrapper.GetInstance<TEntity>(context.ContextProfileName);
            // Base Properties
            entity.Active = true;
            entity.CreatedDate = timestamp.Value;
            entity.CustomKey = model.CustomKey;
            entity.Hash = model.Hash;
            entity.JsonAttributes = Contract.CheckNull(model.SerializableAttributes) ? "{}" : model.SerializableAttributes.SerializeAttributesDictionary();
            await AssignAdditionalPropertiesAsync(entity, model, timestamp.Value, context).ConfigureAwait(false);
            return entity.WrapInPassingCEFARIfNotNull("ERROR! Result was null")!;
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int>> CreateAsync(TIModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await CreateAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int>> CreateAsync(TIModel model, IClarityEcommerceEntities context)
        {
            Contract.RequiresInvalidID(Contract.RequiresNotNull(model).ID);
            if (!OverrideDuplicateCheck)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            var entityResponse = await CreateEntityWithoutSavingAsync(model, null, context).ConfigureAwait(false);
            if (!entityResponse.ActionSucceeded)
            {
                return entityResponse.ChangeFailingCEFARType<int>();
            }
            context.Set<TEntity>().Add(entityResponse.Result!);
            var saveWorked = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new System.IO.InvalidDataException(
                    $"Something about creating '{model.GetType().FullName}' and saving it failed");
            }
            // Pull the entity fresh from the database and return it
            return entityResponse.Result!.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int>> UpdateAsync(
            TIModel model,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await UpdateAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int>> UpdateAsync(
            TIModel model,
            IClarityEcommerceEntities context)
        {
            Contract.RequiresNotNull(model);
            Contract.RequiresValidIDOrAnyValidKey<InvalidOperationException>(model.ID, model.CustomKey);
            var entity = Contract.CheckValidID(model.ID)
                ? await context.Set<TEntity>().FilterByID(model.ID).SingleOrDefaultAsync().ConfigureAwait(false)
                : null;
            if (entity is null && Contract.CheckValidKey(model.CustomKey))
            {
                entity = await context.Set<TEntity>()
                    .FilterByActive(true)
                    .FilterByCustomKey(model.CustomKey, true)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
            }
            Contract.RequiresNotNull<ArgumentException, TEntity>(
                entity!,
                "Must supply an ID or CustomKey that matches an existing record");
            if (entity!.CustomKey != model.CustomKey && !OverrideDuplicateCheck)
            {
                // This will throw if it finds another entity with this model's key
                await DuplicateCheckAsync(model, context).ConfigureAwait(false);
            }
            var timestamp = DateExtensions.GenDateTime;
            // Base Properties
            entity.Active = model.Active;
            entity.CustomKey = model.CustomKey;
            entity.UpdatedDate = timestamp;
            entity.Hash = model.Hash;
            await AssignAdditionalPropertiesAsync(entity, model, timestamp, context).ConfigureAwait(false);
            var saveWorked = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            if (!saveWorked)
            {
                throw new System.IO.InvalidDataException(
                    "Something about updating this object and saving it failed");
            }
            return entity.ID.WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int>> UpsertAsync(TIModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await UpsertAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse<int>> UpsertAsync(
            TIModel model,
            IClarityEcommerceEntities context)
        {
            int? id = null;
            var exists = Contract.CheckValidID(Contract.RequiresNotNull(model).ID)
                && Contract.CheckValidID(id = await CheckExistsAsync(model.ID, context).ConfigureAwait(false))
                || Contract.CheckValidKey(model.CustomKey)
                && Contract.CheckValidID(id = await CheckExistsAsync(model.CustomKey!, context).ConfigureAwait(false));
            if (exists && Contract.CheckInvalidID(model.ID))
            {
                model.ID = id!.Value;
            }
            return exists
                ? await UpdateAsync(model, context).ConfigureAwait(false)
                : await CreateAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeactivateAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeactivateAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeactivateAsync(int id, IClarityEcommerceEntities context)
        {
            return await DeactivateAsync(
                    await GetEntityAsync(Contract.RequiresValidID(id), context).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeactivateAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeactivateAsync(key, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeactivateAsync(string key, IClarityEcommerceEntities context)
        {
            return await DeactivateAsync(
                    await GetEntityAsync(Contract.RequiresValidKey(key), context, null).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ReactivateAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ReactivateAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ReactivateAsync(int id, IClarityEcommerceEntities context)
        {
            return await ReactivateAsync(
                    await GetEntityAsync(Contract.RequiresValidID(id), context).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ReactivateAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await ReactivateAsync(key, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> ReactivateAsync(string key, IClarityEcommerceEntities context)
        {
            return await ReactivateAsync(
                    await GetEntityAsync(Contract.RequiresValidKey(key), context, null).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeleteAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeleteAsync(id, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeleteAsync(int id, IClarityEcommerceEntities context)
        {
            return await DeleteAsync(
                    await GetEntityAsync(Contract.RequiresValidID(id), context).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeleteAsync(string key, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeleteAsync(key, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<CEFActionResponse> DeleteAsync(string key, IClarityEcommerceEntities context)
        {
            return await DeleteAsync(
                    await GetEntityAsync(Contract.RequiresValidKey(key), context).ConfigureAwait(false),
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DuplicateCheckAsync(TIModel model, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DuplicateCheckAsync(model, context).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public virtual async Task<bool> DuplicateCheckAsync(TIModel model, IClarityEcommerceEntities context)
        {
            if (!Contract.CheckValidKey(model.CustomKey))
            {
                return false;
            }
            if (await context.Set<TEntity>().DuplicateCheckAsync(model.ID, model.CustomKey).ConfigureAwait(false))
            {
                throw new InvalidOperationException(
                    "Another record with a matching CustomKey was found. Cannot perform this operation.");
            }
            return false;
        }

        /// <summary>Bulk delete.</summary>
        /// <param name="idsToDelete">       The identifiers to delete.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected virtual async Task<CEFActionResponse> BulkDeleteAsync(
            List<int> idsToDelete,
            string? contextProfileName)
        {
            await PrepBulkDeleteAsync(idsToDelete, contextProfileName).ConfigureAwait(false);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var entities = new List<TEntity>();
            foreach (var id in idsToDelete)
            {
                var entity = await context.Set<TEntity>()
                    .FilterByID(id)
                    .SingleOrDefaultAsync()
                    .ConfigureAwait(false);
                if (entity is null)
                {
                    continue;
                }
                entities.Add(entity);
            }
            if (Contract.CheckEmpty(entities))
            {
                // Nothing to delete
                return CEFAR.PassingCEFAR();
            }
            try
            {
                context.Set<TEntity>().RemoveRange(entities);
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                // Updated/Deleted elsewhere
                return CEFAR.PassingCEFAR();
            }
            catch (OptimisticConcurrencyException)
            {
                // Updated/Deleted elsewhere
                return CEFAR.PassingCEFAR();
            }
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save Delete");
        }

        /// <summary>Prep bulk delete.</summary>
        /// <param name="idsToDelete">       The identifiers to delete.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected virtual Task PrepBulkDeleteAsync(List<int> idsToDelete, string? contextProfileName)
        {
            // By default, do nothing. Override to take specific action
            return Task.CompletedTask;
        }

        /// <summary>Gets an entity.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>The entity.</returns>
        protected virtual Task<TEntity> GetEntityAsync(
            int id,
            IClarityEcommerceEntities context)
        {
            return context.Set<TEntity>()
                .FilterByID(Contract.RequiresValidID(id))
                .SingleOrDefaultAsync();
        }

        /// <summary>Gets an entity.</summary>
        /// <param name="key">    The key to get.</param>
        /// <param name="context">The context.</param>
        /// <param name="active"> [Optional (default=true)] The active state to look for.</param>
        /// <returns>The entity.</returns>
        protected virtual Task<TEntity> GetEntityAsync(
            string key,
            IClarityEcommerceEntities context,
            bool? active = true)
        {
            return context.Set<TEntity>()
                .FilterByActive(active)
                .FilterByCustomKey(Contract.RequiresValidKey(key), true)
                .FirstOrDefaultAsync();
        }

        /// <summary>Filter query by model extension.</summary>
        /// <param name="query">  The query.</param>
        /// <param name="search"> The search.</param>
        /// <param name="context">The context.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        protected virtual async Task<IQueryable<TEntity>> FilterQueryByModelExtensionAsync(
            IQueryable<TEntity> query,
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            query = await FilterQueryByModelCustomAsync(
                    query.FilterByBaseSearchModel(Contract.RequiresNotNull(search)),
                    search,
                    context)
                .ConfigureAwait(false);
            // The following makes it so that relationship tables are all checked for their base level checkable things
            // without needing to write it into every workflow manually. There is only the tiny overhead of the
            // reflection call to generate the method in memory (it's not reflection all the way through to EF or
            // anything).
            var @interface = typeof(TEntity).GetInterface(typeof(IAmARelationshipTable<,>).Name, true);
            if (@interface is null)
            {
                return query;
            }
            var types = @interface.GetGenericArguments();
            // ReSharper disable once PossibleNullReferenceException (Definitely exists)
            var method = typeof(AmARelationshipTableSQLSearchExtensions).GetMethod(
                    nameof(AmARelationshipTableSQLSearchExtensions.FilterByIAmARelationshipTableBaseSearchModel))!
                .MakeGenericMethod(typeof(TEntity), types[0], types[1]);
            query = (IQueryable<TEntity>)method.Invoke(
                obj: null, // Static functions use null for the object
                parameters: new object[]
                {
                    query, // query
                    (IAmARelationshipTableBaseSearchModel)search, // model
                })!;
            return query!;
        }

        /// <summary>Filter query by model custom applications.</summary>
        /// <remarks>This method is intended to be overriden to provide non-T4-generated FilterBys to search queries.</remarks>
        /// <param name="query">  The query.</param>
        /// <param name="search"> The search.</param>
        /// <param name="context">The context.</param>
        /// <returns>An <see cref="IQueryable{TEntity}"/>.</returns>
        protected virtual async Task<IQueryable<TEntity>> FilterQueryByModelCustomAsync(
            IQueryable<TEntity> query,
            TISearchModel search,
            IClarityEcommerceEntities context)
        {
            return query;
        }

        /// <summary>Deactivates the record.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected virtual async Task<CEFActionResponse> DeactivateAsync(
            TEntity entity,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await DeactivateAsync(entity, context).ConfigureAwait(false);
        }

        /// <summary>Deactivate the record.</summary>
        /// <param name="entity"> The entity.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected virtual async Task<CEFActionResponse> DeactivateAsync(
            TEntity? entity,
            IClarityEcommerceEntities context)
        {
            if (entity is null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Deactivate a null record");
            }
            if (!entity.Active)
            {
                // Already inactive
                return CEFAR.PassingCEFAR();
            }
            var e = await context.Set<TEntity>().FilterByID(entity.ID).SingleAsync().ConfigureAwait(false);
            e.UpdatedDate = DateExtensions.GenDateTime;
            e.Active = false;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save Deactivate");
        }

        /// <summary>Deactivate associated objects.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeactivateAssociatedAsMasterObjectsAsync<TSet>(
                int relatedID,
                DateTime timestamp,
                IClarityEcommerceEntities context)
            where TSet : class, IAmARelationshipTable
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                // Must wrap in null check for unit tests
                return;
            }
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.MasterID == relatedID).ConfigureAwait(false);)
            {
                var e = await context.Set<TSet>()
                    .FirstOrDefaultAsync(x => x.Active && x.MasterID == relatedID)
                    .ConfigureAwait(false);
                if (e is null)
                {
                    break;
                }
                e.Active = false;
                e.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>Deactivate associated objects.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeactivateAssociatedAsSlaveObjectsAsync<TSet>(
                int relatedID,
                DateTime timestamp,
                IClarityEcommerceEntities context)
            where TSet : class, IAmARelationshipTable
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                // Must wrap in null check for unit tests
                return;
            }
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.SlaveID == relatedID).ConfigureAwait(false);)
            {
                var e = await context.Set<TSet>().FirstOrDefaultAsync(x => x.Active && x.SlaveID == relatedID).ConfigureAwait(false);
                if (e is null)
                {
                    break;
                }
                e.Active = false;
                e.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>Throw if associated as master object asynchronous.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task ThrowIfAssociatedAsMasterObjectAsync<TSet>(
                int relatedID,
                IClarityEcommerceEntities context)
            where TSet : class, IAmARelationshipTable
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                // Must wrap in null check for unit tests
                return;
            }
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.MasterID == relatedID).ConfigureAwait(false);)
            {
                var e = await context.Set<TSet>()
                    .FilterByActive(true)
                    .FirstOrDefaultAsync(x => x.MasterID == relatedID)
                    .ConfigureAwait(false);
                if (e is null)
                {
                    continue;
                }
                throw new InvalidOperationException(
                    "Cannot delete or deactivate this record as it is tied by association to at least one record of"
                    + $" type {typeof(TSet).Name} which should not be deleted or deactivated. If you still wish to"
                    + " delete or deactivate this record, you must delete or deactivate the association first, which"
                    + " may require performing the action against the database directly.");
            }
        }

        /// <summary>Throw if associated as slave object asynchronous.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task ThrowIfAssociatedAsSlaveObjectAsync<TSet>(
                int relatedID,
                IClarityEcommerceEntities context)
            where TSet : class, IAmARelationshipTable
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                // Must wrap in null check for unit tests
                return;
            }
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.SlaveID == relatedID).ConfigureAwait(false);)
            {
                var e = await context.Set<TSet>()
                    .FirstOrDefaultAsync(x => x.Active && x.SlaveID == relatedID)
                    .ConfigureAwait(false);
                if (e is null)
                {
                    continue;
                }
                throw new InvalidOperationException(
                    "Cannot delete or deactivate this record as it is tied by association to at least one record of"
                    + $" type {typeof(TSet).Name} which should not be deleted or deactivated. If you still wish to"
                    + " delete or deactivate this record, you must delete or deactivate the association first, which"
                    + " may require performing the action against the database directly.");
            }
        }

        /// <summary>Deactivate associated images.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeactivateAssociatedImagesAsync<TSet>(
                int relatedID,
                DateTime timestamp,
                IClarityEcommerceEntities context)
            where TSet : class, IImageBase
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                return;
            }
            // Must wrap in null check for unit tests
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.MasterID == relatedID).ConfigureAwait(false);)
            {
                var e = await context.Set<TSet>()
                    .FirstOrDefaultAsync(x => x.Active && x.MasterID == relatedID)
                    .ConfigureAwait(false);
                if (e is null)
                {
                    break;
                }
                e.Active = false;
                e.UpdatedDate = timestamp;
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>Reactivates the record.</summary>
        /// <param name="entity"> The entity.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected virtual async Task<CEFActionResponse> ReactivateAsync(
            TEntity? entity,
            IClarityEcommerceEntities context)
        {
            if (entity is null)
            {
                return CEFAR.FailingCEFAR("ERROR! Cannot Reactivate a null record");
            }
            if (entity.Active)
            {
                // Already active
                return CEFAR.PassingCEFAR();
            }
            var e = await context.Set<TEntity>().FilterByID(entity.ID).SingleAsync().ConfigureAwait(false);
            e.UpdatedDate = DateExtensions.GenDateTime;
            e.Active = true;
            return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                .BoolToCEFAR("ERROR! Failed to save Reactivate");
        }

        /// <summary>Delete associated objects.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeleteAssociatedAsMasterObjectsAsync<TSet>(
                int relatedID,
                IClarityEcommerceEntities context)
            where TSet : class, IAmARelationshipTable
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                return;
            }
            // Must wrap in null check for unit tests
            for (var i = 0; i < context.Set<TSet>().Count(x => x.Active && x.MasterID == relatedID);)
            {
                context.Set<TSet>().Remove(
                    await context.Set<TSet>().FirstAsync(x => x.Active && x.MasterID == relatedID).ConfigureAwait(false));
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>Delete associated objects.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeleteAssociatedAsSlaveObjectsAsync<TSet>(
                int relatedID,
                IClarityEcommerceEntities context)
            where TSet : class, IAmARelationshipTable
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                return;
            }
            // Must wrap in null check for unit tests
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.SlaveID == relatedID).ConfigureAwait(false);)
            {
                context.Set<TSet>().Remove(
                    await context.Set<TSet>().FirstAsync(x => x.Active && x.SlaveID == relatedID).ConfigureAwait(false));
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>Delete associated images.</summary>
        /// <typeparam name="TSet">Type of the set.</typeparam>
        /// <param name="relatedID">Identifier for the related.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected async Task DeleteAssociatedImagesAsync<TSet>(
                int relatedID,
                IClarityEcommerceEntities context)
            where TSet : class, IImageBase
        {
            // ReSharper disable once ConditionIsAlwaysTrueOrFalse
            if (context.Set<TSet>() is null)
            {
                return;
            }
            // Must wrap in null check for unit tests
            for (var i = 0; i < await context.Set<TSet>().CountAsync(x => x.Active && x.MasterID == relatedID).ConfigureAwait(false);)
            {
                context.Set<TSet>().Remove(
                    await context.Set<TSet>().FirstAsync(x => x.Active && x.MasterID == relatedID).ConfigureAwait(false));
                await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            }
        }

        /// <summary>Deletes the record.</summary>
        /// <param name="entity"> The entity.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        protected virtual async Task<CEFActionResponse> DeleteAsync(
            TEntity? entity,
            IClarityEcommerceEntities context)
        {
            if (entity is null)
            {
                return CEFAR.PassingCEFAR();
            }
            try
            {
                context.Set<TEntity>().Remove(entity);
                return (await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false))
                    .BoolToCEFAR("ERROR! Failed to save Delete");
            }
            catch (System.Data.Entity.Infrastructure.DbUpdateConcurrencyException)
            {
                // Updated/Deleted elsewhere
                return CEFAR.PassingCEFAR();
            }
            catch (OptimisticConcurrencyException)
            {
                // Updated/Deleted elsewhere
                return CEFAR.PassingCEFAR();
            }
        }

        /// <summary>Assign additional properties.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="context">           The context.</param>
        /// <returns>A Task.</returns>
        protected virtual async Task AssignAdditionalPropertiesAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            UpdateEntityFromModel(entity, model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            await RunDefaultRelateWorkflowsAsync(
                    entity: entity,
                    model: model,
                    timestamp: timestamp,
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
            await RunDefaultAssociateWorkflowsAsync(
                    entity: entity,
                    model: model,
                    timestamp: timestamp,
                    contextProfileName: context.ContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Sets default JSON attributes if null.</summary>
        /// <param name="entity">The entity.</param>
        protected void SetDefaultJsonAttributesIfNull(TIEntity entity)
        {
            if (string.IsNullOrWhiteSpace(entity.JsonAttributes))
            {
                entity.JsonAttributes = "{}";
            }
        }

        /// <summary>Executes the default associate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected abstract Task RunDefaultAssociateWorkflowsAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            string? contextProfileName);

        /// <summary>Executes the default associate workflows operation.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected abstract Task RunDefaultAssociateWorkflowsAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context);

        /// <summary>Executes the default relate workflows operation.</summary>
        /// <param name="entity">            The entity.</param>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        protected abstract Task RunDefaultRelateWorkflowsAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            string? contextProfileName);

        /// <summary>Executes the default relate workflows operation.</summary>
        /// <param name="entity">   The entity.</param>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp Date/Time.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A Task.</returns>
        protected abstract Task RunDefaultRelateWorkflowsAsync(
            TIEntity entity,
            TIModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context);
    }
}
