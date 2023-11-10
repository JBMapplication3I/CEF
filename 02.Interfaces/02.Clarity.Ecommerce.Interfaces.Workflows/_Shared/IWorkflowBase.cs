// <copyright file="IWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IWorkflowBase interface</summary>
// ReSharper disable TypeParameterCanBeVariant
#nullable enable
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for workflow for bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="IWorkflowBaseHasAll{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface IWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : IWorkflowBaseHasAll<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : IBaseModel
        where TIEntity : IBase
        where TEntity : class, TIEntity, new()
    {
    }

    /// <summary>Interface for workflow base has all.</summary>
    /// <typeparam name="TIModel">      Type of the ti model.</typeparam>
    /// <typeparam name="TISearchModel">Type of the ti search model.</typeparam>
    /// <typeparam name="TIEntity">     Type of the ti entity.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    public interface IWorkflowBaseHasAll<TIModel, TISearchModel, TIEntity, TEntity>
        : IWorkflowBaseHasGet<TIModel>,
          IWorkflowBaseHasSearch<TIModel, TISearchModel>,
          IWorkflowBaseHasCreateUpdate<TIModel, TEntity>,
          IWorkflowBaseHasDeactivate,
          IWorkflowBaseHasReactivate,
          IWorkflowBaseHasDelete
        where TIModel : IBaseModel
        where TIEntity : IBase
        where TEntity : class, TIEntity, new()
    {
        // Implements all CRUD operations
    }

    /// <summary>Interface for workflow base has get.</summary>
    /// <typeparam name="TIModel">Type of the ti model.</typeparam>
    public interface IWorkflowBaseHasGet<TIModel>
        where TIModel : IBaseModel
    {
        /// <summary>Gets the last modified for result.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for result.</returns>
        Task<DateTime?> GetLastModifiedForResultAsync(int id, string? contextProfileName);

        /// <summary>Gets the last modified for result.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for result.</returns>
        Task<DateTime?> GetLastModifiedForResultAsync(int id, IClarityEcommerceEntities context);

        /// <summary>Gets the last modified for result.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for result.</returns>
        Task<DateTime?> GetLastModifiedForResultAsync(string key, string? contextProfileName);

        /// <summary>Gets the last modified for result.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for result.</returns>
        Task<DateTime?> GetLastModifiedForResultAsync(string key, IClarityEcommerceEntities context);

        /// <summary>Gets the entity by it's identifier converted to the DTO.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<TIModel?> GetAsync(int id, string? contextProfileName);

        /// <summary>Gets the entity by it's identifier converted to the DTO.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>The record.</returns>
        Task<TIModel?> GetAsync(int id, IClarityEcommerceEntities context);

        /// <summary>Gets the entity by it's CustomKey converted to the DTO.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<TIModel?> GetAsync(string key, string? contextProfileName);

        /// <summary>Gets the entity by it's CustomKey converted to the DTO.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="context">The context.</param>
        /// <returns>The record.</returns>
        Task<TIModel?> GetAsync(string key, IClarityEcommerceEntities context);

        /// <summary>Check exists by identifier.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsAsync(int id, string? contextProfileName);

        /// <summary>Check exists by identifier.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task{int?}.</returns>
        Task<int?> CheckExistsAsync(int id, IClarityEcommerceEntities context);

        /// <summary>Check exists by key.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsAsync(string key, string? contextProfileName);

        /// <summary>Check exists by key.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="context">The context.</param>
        /// <returns>A Task{int?}.</returns>
        Task<int?> CheckExistsAsync(string key, IClarityEcommerceEntities context);

        /// <summary>Resolves to the object based on identifying information, will throw if not found.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolves to the object based on identifying information, will throw if not found.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="isInner">True if this call instance is an inner part of another Resolve.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false);

        /// <summary>Resolve to identifier of the object based on identifying information, will throw if not found.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the existing identifier (will throw is not found).</returns>
        Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to identifier of the object based on identifying information, will throw if not found.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="isInner">True if this call instance is an inner part of another Resolve.</param>
        /// <returns>A Task{int}.</returns>
        Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found, may auto-generate if enough data is provided.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found, may auto-generate if enough data is provided.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to the object based on identifying information with automatic generate, will throw if not
        /// able to generate.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to the object based on identifying information with automatic generate, will throw if not
        /// able to generate.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to the object based on identifying information with automatic generate optionally, may
        /// return null if not able to generate.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to the object based on identifying information with automatic generate optionally, may
        /// return null if not able to generate.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to the object based on identifying information with automatic generate to identifier, will
        /// throw if not able to generate.</summary>
        /// <param name="byID">              The identifier.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to the object based on identifying information with automatic generate to identifier, will
        /// throw if not able to generate.</summary>
        /// <param name="byID">   The identifier.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Gets a digest.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The digest.</returns>
        Task<List<IDigestModel>> GetDigestAsync(string? contextProfileName);
    }

    /// <summary>Interface for workflow base has get by SEO url.</summary>
    /// <typeparam name="TIModel">Type of the ti model.</typeparam>
    public interface IWorkflowBaseHasGetBySeoUrl<TIModel>
        where TIModel : IBaseModel, IHaveSeoBaseModel
    {
        /// <summary>Gets the last modified for by SEO URL result.</summary>
        /// <param name="seoUrl">            URL of the SEO.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by SEO URL result.</returns>
        Task<DateTime?> GetLastModifiedForBySeoUrlResultAsync(string seoUrl, string? contextProfileName);

        /// <summary>Gets the last modified for by SEO URL result.</summary>
        /// <param name="seoUrl">            URL of the SEO.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for by SEO URL result.</returns>
        Task<DateTime?> GetLastModifiedForBySeoUrlResultAsync(string seoUrl, IClarityEcommerceEntities context);

        /// <summary>Gets by SEO URL.</summary>
        /// <param name="seoUrl">            URL of the SEO.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by SEO URL.</returns>
        Task<TIModel?> GetBySeoUrlAsync(string seoUrl, string? contextProfileName);

        /// <summary>Gets by SEO URL.</summary>
        /// <param name="seoUrl"> URL of the SEO.</param>
        /// <param name="context">The context.</param>
        /// <returns>The by SEO URL.</returns>
        Task<TIModel?> GetBySeoUrlAsync(string seoUrl, IClarityEcommerceEntities context);

        /// <summary>Check exists by SEO URL.</summary>
        /// <param name="seoUrl">            URL of the SEO.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsBySeoUrlAsync(string seoUrl, string? contextProfileName);

        /// <summary>Check exists by SEO URL.</summary>
        /// <param name="seoUrl"> URL of the SEO.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsBySeoUrlAsync(string seoUrl, IClarityEcommerceEntities context);
    }

    /// <summary>Interface for workflow base has search.</summary>
    /// <typeparam name="TIModel">      Type of the ti model.</typeparam>
    /// <typeparam name="TISearchModel">Type of the ti search model.</typeparam>
    public interface IWorkflowBaseHasSearch<TIModel, TISearchModel>
        where TIModel : IBaseModel
    {
        /// <summary>Gets the last modified for result set.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for result set.</returns>
        Task<DateTime?> GetLastModifiedForResultSetAsync(TISearchModel search, string? contextProfileName);

        /// <summary>Gets the last modified for result set.</summary>
        /// <param name="search"> The search.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for result set.</returns>
        Task<DateTime?> GetLastModifiedForResultSetAsync(TISearchModel search, IClarityEcommerceEntities context);

        /// <summary>Searches for the first match.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="asListing">         True to as listing.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A ValueTuple{List{TIModel},int,int}.</returns>
        Task<(List<TIModel> results, int totalPages, int totalCount)> SearchAsync(
            TISearchModel search,
            bool asListing,
            string? contextProfileName);

        /// <summary>Searches for the first match.</summary>
        /// <param name="search">   The search.</param>
        /// <param name="asListing">True to as listing.</param>
        /// <param name="context">  The context.</param>
        /// <returns>A ValueTuple{List{TIModel},int,int}.</returns>
        Task<(List<TIModel> results, int totalPages, int totalCount)> SearchAsync(
            TISearchModel search,
            bool asListing,
            IClarityEcommerceEntities context);

        /// <summary>Searches the connects in this collection.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An enumerator that allows foreach to be used to process the connects in this collection.</returns>
        Task<IEnumerable<TIModel>> SearchForConnectAsync(TISearchModel search, string? contextProfileName);

        /// <summary>Searches the connects in this collection.</summary>
        /// <param name="search"> The search.</param>
        /// <param name="context">The context.</param>
        /// <returns>An enumerator that allows foreach to be used to process the connects in this collection.</returns>
        Task<IEnumerable<TIModel>> SearchForConnectAsync(TISearchModel search, IClarityEcommerceEntities context);
    }

    /// <summary>Interface for workflow base has create update.</summary>
    /// <typeparam name="TIModel">Type of the ti model.</typeparam>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IWorkflowBaseHasCreateUpdate<TIModel, TEntity>
        where TIModel : IBaseModel
    {
        /// <summary>Duplicate check.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> DuplicateCheckAsync(TIModel model, string? contextProfileName);

        /// <summary>Duplicate check.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        Task<bool> DuplicateCheckAsync(TIModel model, IClarityEcommerceEntities context);

        /// <summary>Creates entity without saving. WARNING! This does not add the item to the database. Use
        /// <see cref="CreateAsync(TIModel, string)"/> if you want that. This function is to create an entity that
        /// is to be assigned to another object before saving.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="timestamp">         The timestamp.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new entity without saving.</returns>
        Task<CEFActionResponse<TEntity?>> CreateEntityWithoutSavingAsync(
            TIModel model,
            DateTime? timestamp,
            string? contextProfileName);

        /// <summary>Creates entity without saving. WARNING! This does not add the item to the database. Use
        /// <see cref="CreateAsync(TIModel, IClarityEcommerceEntities)"/> if you want that. This function is to
        /// create an entity that is to be assigned to another object before saving.</summary>
        /// <param name="model">    The model.</param>
        /// <param name="timestamp">The timestamp.</param>
        /// <param name="context">  The context.</param>
        /// <returns>The new entity without saving.</returns>
        Task<CEFActionResponse<TEntity?>> CreateEntityWithoutSavingAsync(
            TIModel model,
            DateTime? timestamp,
            IClarityEcommerceEntities context);

        /// <summary>Creates a new TIModel.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_int}"/>.</returns>
        Task<CEFActionResponse<int>> CreateAsync(TIModel model, string? contextProfileName);

        /// <summary>Creates a new TIModel.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_int}"/>.</returns>
        Task<CEFActionResponse<int>> CreateAsync(TIModel model, IClarityEcommerceEntities context);

        /// <summary>Updates the record.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_int}"/>.</returns>
        Task<CEFActionResponse<int>> UpdateAsync(TIModel model, string? contextProfileName);

        /// <summary>Updates the record.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_int}"/>.</returns>
        Task<CEFActionResponse<int>> UpdateAsync(TIModel model, IClarityEcommerceEntities context);

        /// <summary>Upserts by checking if the object exists and creates if it doesn't or updates if it does.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_int}"/>.</returns>
        Task<CEFActionResponse<int>> UpsertAsync(TIModel model, string? contextProfileName);

        /// <summary>Upserts by checking if the object exists and creates if it doesn't or updates if it does.</summary>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A <see cref="Task{CEFActionResponse_int}"/>.</returns>
        Task<CEFActionResponse<int>> UpsertAsync(TIModel model, IClarityEcommerceEntities context);
    }

    /// <summary>Interface for workflow base has deactivate.</summary>
    public interface IWorkflowBaseHasDeactivate
    {
        /// <summary>Deactivates the record.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(int id, string? contextProfileName);

        /// <summary>Deactivates the record.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(int id, IClarityEcommerceEntities context);

        /// <summary>Deactivates the record.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(string key, string? contextProfileName);

        /// <summary>Deactivates the record.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(string key, IClarityEcommerceEntities context);
    }

    /// <summary>Interface for workflow base has reactivate.</summary>
    public interface IWorkflowBaseHasReactivate
    {
        /// <summary>Reactivates the record.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReactivateAsync(int id, string? contextProfileName);

        /// <summary>Reactivates the record.</summary>
        /// <param name="id">     The identifier.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReactivateAsync(int id, IClarityEcommerceEntities context);

        /// <summary>Reactivates the record.</summary>
        /// <param name="key">               The key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReactivateAsync(string key, string? contextProfileName);

        /// <summary>Reactivates the record.</summary>
        /// <param name="key">    The key.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ReactivateAsync(string key, IClarityEcommerceEntities context);
    }

    /// <summary>Interface for workflow base has delete.</summary>
    public interface IWorkflowBaseHasDelete
    {
        /// <summary>Deletes the given id.</summary>
        /// <param name="id">                The Identifier to delete.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeleteAsync(int id, string? contextProfileName);

        /// <summary>Deletes the given id.</summary>
        /// <param name="id">     The Identifier to delete.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeleteAsync(int id, IClarityEcommerceEntities context);

        /// <summary>Deletes the given key.</summary>
        /// <param name="key">               The key to delete.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeleteAsync(string key, string? contextProfileName);

        /// <summary>Deletes the given key.</summary>
        /// <param name="key">    The key to delete.</param>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeleteAsync(string key, IClarityEcommerceEntities context);
    }
}
