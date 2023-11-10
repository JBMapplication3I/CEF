// <copyright file="IDisplayableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDisplayableWorkflowBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for workflow for displayable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="INameableWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface IDisplayableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : INameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : IDisplayableBaseModel
        where TIEntity : IDisplayableBase
        where TEntity : class, TIEntity, new()
    {
        /// <summary>Gets the last modified for by display name result.</summary>
        /// <param name="displayName">       The display name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by display name result.</returns>
        Task<DateTime?> GetLastModifiedForByDisplayNameResultAsync(string displayName, string? contextProfileName);

        /// <summary>Gets the last modified for by display name result.</summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="context">    The context.</param>
        /// <returns>The last modified for by display name result.</returns>
        Task<DateTime?> GetLastModifiedForByDisplayNameResultAsync(string displayName, IClarityEcommerceEntities context);

        /// <summary>Gets by display name.</summary>
        /// <param name="displayName">       The display name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by display name.</returns>
        Task<TIModel?> GetByDisplayNameAsync(string displayName, string? contextProfileName);

        /// <summary>Gets by display name.</summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="context">    The context.</param>
        /// <returns>The by display name.</returns>
        Task<TIModel?> GetByDisplayNameAsync(string displayName, IClarityEcommerceEntities context);

        /// <summary>Check exists by display name.</summary>
        /// <param name="displayName">       The display name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByDisplayNameAsync(string displayName, string? contextProfileName);

        /// <summary>Check exists by display name.</summary>
        /// <param name="displayName">The display name.</param>
        /// <param name="context">    The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByDisplayNameAsync(string displayName, IClarityEcommerceEntities context);

        /// <summary>Resolves to an object.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="byDisplayName">     Name of the by display.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{TIModel}.</returns>
        Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolves to an object.</summary>
        /// <param name="byID">         Identifier for the by.</param>
        /// <param name="byKey">        The by key.</param>
        /// <param name="byName">       Name of the by.</param>
        /// <param name="byDisplayName">Name of the by display.</param>
        /// <param name="model">        The model.</param>
        /// <param name="context">      The context.</param>
        /// <param name="isInner">      True if this call instance is an inner part of another Resolve.</param>
        /// <returns>A CEFActionResponse{TIModel}.</returns>
        Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false);

        /// <summary>Resolves to identifier.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="byDisplayName">     Name of the by display.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the existing identifier or 0.</returns>
        Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolves to identifier.</summary>
        /// <param name="byID">         Identifier for the by.</param>
        /// <param name="byKey">        The by key.</param>
        /// <param name="byName">       Name of the by.</param>
        /// <param name="byDisplayName">Name of the by display.</param>
        /// <param name="model">        The model.</param>
        /// <param name="context">      The context.</param>
        /// <param name="isInner">      True if this call instance is part of another Resolve.</param>
        /// <returns>An int, the existing identifier or 0.</returns>
        Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false);

        /// <summary>Resolve with automatic generate.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="byName">            The name.</param>
        /// <param name="byDisplayName">     The display name.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve with automatic generate.</summary>
        /// <param name="byID">         Identifier for the by.</param>
        /// <param name="byKey">        The key.</param>
        /// <param name="byName">       The name.</param>
        /// <param name="byDisplayName">The display name.</param>
        /// <param name="model">        The model.</param>
        /// <param name="context">      The context.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve with automatic generate.</summary>
        /// <param name="byID">         Identifier for the by.</param>
        /// <param name="byKey">        The key.</param>
        /// <param name="byName">       The name.</param>
        /// <param name="byDisplayName">The display name.</param>
        /// <param name="model">        The model.</param>
        /// <param name="context">      The context.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve with automatic generate to identifier.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="byName">            The name.</param>
        /// <param name="byDisplayName">     The display name.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve with automatic generate to identifier.</summary>
        /// <param name="byID">         Identifier for the by.</param>
        /// <param name="byKey">        The key.</param>
        /// <param name="byName">       The name.</param>
        /// <param name="byDisplayName">The display name.</param>
        /// <param name="model">        The model.</param>
        /// <param name="context">      The context.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve with automatic generate to identifier (optionally, may return null if it can't auto-generate.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="byName">            The name.</param>
        /// <param name="byDisplayName">     The display name.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve with automatic generate to identifier (optionally, may return null if it can't auto-generate.</summary>
        /// <param name="byID">         Identifier for the by.</param>
        /// <param name="byKey">        The key.</param>
        /// <param name="byName">       The name.</param>
        /// <param name="byDisplayName">The display name.</param>
        /// <param name="model">        The model.</param>
        /// <param name="context">      The context.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            string? byDisplayName,
            TIModel? model,
            IClarityEcommerceEntities context);
    }
}
