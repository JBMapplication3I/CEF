// <copyright file="GenericServiceBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the generic service base class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Interfaces.Workflow;
    using JetBrains.Annotations;
    using ServiceStack;

    [PublicAPI]
    public abstract class GenericServiceBase<TIModel,
            TModel, TIEntity, TEntity, TISearchModel, TPagedResults,
            TGetMany, TGetManyForConnect,
            TGetByID, TGetByKey,
            TCheckExistsByID, TCheckExistsByKey,
            TCreate, TUpdate,
            TDeactivateByID, TDeactivateByKey,
            TReactivateByID, TReactivateByKey,
            TDeleteByID, TDeleteByKey>
        : ClarityEcommerceServiceBase
        where TIModel : IBaseModel
        where TModel : class, TIModel
        where TIEntity : IBase
        where TEntity : class, TIEntity, new()
        where TISearchModel : IBaseSearchModel
        where TPagedResults : PagedResultsBase<TModel>, new()
        where TGetMany : TISearchModel, IReturn<TPagedResults>
        where TGetManyForConnect : TISearchModel, IReturn<List<TModel>>
        where TGetByID : ImplementsIDBase, IReturn<TModel>
        where TGetByKey : ImplementsKeyBase, IReturn<TModel>
        where TCheckExistsByID : ImplementsIDBase, IReturn<int?>
        where TCheckExistsByKey : ImplementsKeyBase, IReturn<int?>
        where TCreate : TIModel, IReturn<TModel>
        where TUpdate : TIModel, IReturn<TModel>
        where TDeactivateByID : ImplementsIDBase, IReturn<bool>
        where TDeactivateByKey : ImplementsKeyBase, IReturn<bool>
        where TReactivateByID : ImplementsIDBase, IReturn<bool>
        where TReactivateByKey : ImplementsKeyBase, IReturn<bool>
        where TDeleteByID : ImplementsIDBase, IReturn<bool>
        where TDeleteByKey : ImplementsKeyBase, IReturn<bool>
    {
        protected abstract IWorkflowBaseHasAll<TIModel, TISearchModel, TIEntity, TEntity> Workflow { get; }

        public virtual async Task<object?> Post(TGetMany request)
        {
            return await GetPagedResultsAsync(request, false).ConfigureAwait(false);
        }

        public virtual async Task<object?> Post(TGetManyForConnect request)
        {
            return await Workflow.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Get(TGetByID request)
        {
            return await Workflow.GetAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Get(TGetByKey request)
        {
            return await Workflow.GetAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Get(TCheckExistsByID request)
        {
            return await Workflow.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Get(TCheckExistsByKey request)
        {
            return await Workflow.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Post(TCreate request)
        {
            return await Workflow.CreateAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Put(TUpdate request)
        {
            return await Workflow.UpdateAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Patch(TDeactivateByID request)
        {
            return await Workflow.DeactivateAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Patch(TDeactivateByKey request)
        {
            return await Workflow.DeactivateAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Patch(TReactivateByID request)
        {
            return await Workflow.ReactivateAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Patch(TReactivateByKey request)
        {
            return await Workflow.ReactivateAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Delete(TDeleteByID request)
        {
            return await Workflow.DeleteAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public virtual async Task<object?> Delete(TDeleteByKey request)
        {
            return await Workflow.DeleteAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>Gets paged results.</summary>
        /// <param name="request">  The request.</param>
        /// <param name="asListing">True to as listing.</param>
        /// <returns>The paged results.</returns>
        protected virtual async Task<TPagedResults> GetPagedResultsAsync(TISearchModel request, bool asListing)
        {
            var (results, totalPages, totalCount) = await Workflow.SearchAsync(request, asListing, contextProfileName: null).ConfigureAwait(false);
            return new()
            {
                Results = results.Cast<TModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }
    }
}
