// <copyright file="INameableWorkflowBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the INameableWorkflowBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;

    /// <summary>Interface for workflow for nameable bases.</summary>
    /// <typeparam name="TIModel">      Type of the model interface.</typeparam>
    /// <typeparam name="TISearchModel">Type of the search model interface.</typeparam>
    /// <typeparam name="TIEntity">     Type of the entity interface.</typeparam>
    /// <typeparam name="TEntity">      Type of the entity.</typeparam>
    /// <seealso cref="IWorkflowBase{TIModel,TISearchModel,TIEntity,TEntity}"/>
    public interface INameableWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        : IWorkflowBase<TIModel, TISearchModel, TIEntity, TEntity>
        where TIModel : INameableBaseModel
        where TIEntity : INameableBase
        where TEntity : class, TIEntity, new()
    {
        /// <summary>Gets the last modified for by name result.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The last modified for by name result.</returns>
        Task<DateTime?> GetLastModifiedForByNameResultAsync(string name, string? contextProfileName);

        /// <summary>Gets the last modified for by name result.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="context">The context.</param>
        /// <returns>The last modified for by name result.</returns>
        Task<DateTime?> GetLastModifiedForByNameResultAsync(string name, IClarityEcommerceEntities context);

        /// <summary>Gets by name.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The by name.</returns>
        Task<TIModel?> GetByNameAsync(string name, string? contextProfileName);

        /// <summary>Gets by name.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="context">The context.</param>
        /// <returns>The by name.</returns>
        Task<TIModel?> GetByNameAsync(string name, IClarityEcommerceEntities context);

        /// <summary>Check exists by name.</summary>
        /// <param name="name">              The name.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByNameAsync(string name, string? contextProfileName);

        /// <summary>Check exists by name.</summary>
        /// <param name="name">   The name.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> CheckExistsByNameAsync(string name, IClarityEcommerceEntities context);

        /// <summary>Resolves to an object by matching it against information as best possible.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolves to an object by matching it against information as best possible.</summary>
        /// <param name="byID">   Identifier for the by.</param>
        /// <param name="byKey">  The by key.</param>
        /// <param name="byName"> Name of the by.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="isInner">True if this call instance is an inner part of another Resolve.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false);

        /// <summary>Resolves to an object by matching it against information as best possible.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the existing identifier or 0.</returns>
        Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolves to an object by matching it against information as best possible.</summary>
        /// <param name="byID">   Identifier for the by.</param>
        /// <param name="byKey">  The by key.</param>
        /// <param name="byName"> Name of the by.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <param name="isInner">True if this call instance is an inner part of another Resolve.</param>
        /// <returns>An int, the existing identifier or 0.</returns>
        Task<int> ResolveToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context,
            bool isInner = false);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found.</summary>
        /// <param name="byID">   Identifier for the by.</param>
        /// <param name="byKey">  The by key.</param>
        /// <param name="byName"> Name of the by.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found, may auto-generate if enough data is provided.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The by key.</param>
        /// <param name="byName">            Name of the by.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to identifier of the object based on identifying information optionally, may return null if
        /// not found, may auto-generate if enough data is provided.</summary>
        /// <param name="byID">   Identifier for the by.</param>
        /// <param name="byKey">  The by key.</param>
        /// <param name="byName"> Name of the by.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int?.</returns>
        Task<int?> ResolveWithAutoGenerateToIDOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to an object by matching it against information as best possible with automatic generation
        /// if not found.</summary>
        /// <param name="byID">              The Identifier to try to resolve with.</param>
        /// <param name="byKey">             The Custom Key to try to resolve with.</param>
        /// <param name="byName">            The Name to try to resolve with.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to an object by matching it against information as best possible with automatic generation
        /// if not found.</summary>
        /// <param name="byID">   The Identifier to try to resolve with.</param>
        /// <param name="byKey">  The Custom Key to try to resolve with.</param>
        /// <param name="byName"> The Name to try to resolve with.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel>> ResolveWithAutoGenerateAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve to an object by matching it against information as best possible with automatic generation
        /// if not found.</summary>
        /// <param name="byID">              The Identifier to try to resolve with.</param>
        /// <param name="byKey">             The Custom Key to try to resolve with.</param>
        /// <param name="byName">            The Name to try to resolve with.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve to an object by matching it against information as best possible with automatic generation
        /// if not found.</summary>
        /// <param name="byID">   The Identifier to try to resolve with.</param>
        /// <param name="byKey">  The Custom Key to try to resolve with.</param>
        /// <param name="byName"> The Name to try to resolve with.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>A TIModel.</returns>
        Task<CEFActionResponse<TIModel?>> ResolveWithAutoGenerateOptionalAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context);

        /// <summary>Resolve with automatic generate to identifier.</summary>
        /// <param name="byID">              Identifier for the by.</param>
        /// <param name="byKey">             The key.</param>
        /// <param name="byName">            The name.</param>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            string? contextProfileName);

        /// <summary>Resolve with automatic generate to identifier.</summary>
        /// <param name="byID">   Identifier for the by.</param>
        /// <param name="byKey">  The key.</param>
        /// <param name="byName"> The name.</param>
        /// <param name="model">  The model.</param>
        /// <param name="context">The context.</param>
        /// <returns>An int, the new or existing identifier.</returns>
        Task<int> ResolveWithAutoGenerateToIDAsync(
            int? byID,
            string? byKey,
            string? byName,
            TIModel? model,
            IClarityEcommerceEntities context);
    }
}
