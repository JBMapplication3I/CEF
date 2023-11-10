// <autogenerated>
// <copyright file="ManufacturerService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the manufacturer service class</summary>
// </autogenerated>
// ReSharper disable InvalidXmlDocComment, PartialTypeWithSinglePart, RedundantUsingDirective
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    /// <summary>A ServiceStack Route to get a list of manufacturers.</summary>
    /// <seealso cref="ManufacturerSearchModel"/>
    /// <seealso cref="IReturn{ManufacturerPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Manufacturers/Manufacturers", "GET", Priority = 1,
            Summary = "Use to get a list of manufacturers")]
    public partial class GetManufacturers : ManufacturerSearchModel, IReturn<ManufacturerPagedResults> { }

    /// <summary>A ServiceStack Route to get manufacturers for connect.</summary>
    /// <seealso cref="ManufacturerSearchModel"/>
    /// <seealso cref="IReturn{List{ManufacturerModel}}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.View"),
        PublicAPI,
        Route("/Manufacturers/ManufacturersForConnect", "POST,GET", Priority = 1,
            Summary = "Get all manufacturers")]
    public partial class GetManufacturersForConnect : ManufacturerSearchModel, IReturn<List<ManufacturerModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all manufacturers.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.View"),
        PublicAPI,
        Route("/Manufacturers/ManufacturersDigest", "GET",
            Summary = "Use to get a hash representing each manufacturers")]
    public partial class GetManufacturersDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{ManufacturerModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Manufacturers/Manufacturer/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific manufacturer")]
    public partial class GetManufacturerByID : ImplementsIDBase, IReturn<ManufacturerModel> { }

    /// <summary>A ServiceStack Route to get manufacturer.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{ManufacturerModel}"/>
    [PublicAPI,
        Route("/Manufacturers/Manufacturer/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific manufacturer by the custom key")]
    public partial class GetManufacturerByKey : ImplementsKeyBase, IReturn<ManufacturerModel> { }

    /// <summary>A ServiceStack Route to get manufacturer.</summary>
    /// <seealso cref="IReturn{ManufacturerModel}"/>
    [PublicAPI,
        Route("/Manufacturers/Manufacturer/Name", "GET", Priority = 1,
            Summary = "Use to get a specific manufacturer by the name")]
    public partial class GetManufacturerByName : ImplementsNameBase, IReturn<ManufacturerModel> { }

    /// <summary>A ServiceStack Route to check manufacturer exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.View"),
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckManufacturerExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check manufacturer exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.View"),
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckManufacturerExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check manufacturer exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.View"),
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckManufacturerExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create manufacturer.</summary>
    /// <seealso cref="ManufacturerModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Create", "POST", Priority = 1,
            Summary = "Use to create a new manufacturer.")]
    public partial class CreateManufacturer : ManufacturerModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert manufacturer.</summary>
    /// <seealso cref="ManufacturerModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing manufacturer (as needed).")]
    public partial class UpsertManufacturer : ManufacturerModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update manufacturer.</summary>
    /// <seealso cref="ManufacturerModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing manufacturer.")]
    public partial class UpdateManufacturer : ManufacturerModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific manufacturer from the system [Soft-Delete]")]
    public partial class DeactivateManufacturerByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate manufacturer by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Deactivate"),
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific manufacturer from the system [Soft-Delete]")]
    public partial class DeactivateManufacturerByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific manufacturer from the system [Restore from Soft-Delete]")]
    public partial class ReactivateManufacturerByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate manufacturer by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Reactivate"),
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific manufacturer from the system [Restore from Soft-Delete]")]
    public partial class ReactivateManufacturerByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete manufacturer.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific manufacturer from the system [Hard-Delete]")]
    public partial class DeleteManufacturerByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete manufacturer by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Manufacturers.Manufacturer.Delete"),
        PublicAPI,
        Route("/Manufacturers/Manufacturer/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific manufacturer from the system [Hard-Delete]")]
    public partial class DeleteManufacturerByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear manufacturer cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Manufacturers/Manufacturer/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all manufacturer calls.")]
    public class ClearManufacturerCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class ManufacturerServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetManufacturers"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetManufacturers request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IManufacturerModel, ManufacturerModel, IManufacturerSearchModel, ManufacturerPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Manufacturers)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetManufacturersForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetManufacturersForConnect request)
        {
            return await Workflows.Manufacturers.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetManufacturersDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetManufacturersDigest request)
        {
            return await Workflows.Manufacturers.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetManufacturerByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Manufacturers, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetManufacturerByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Manufacturers, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetManufacturerByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetManufacturerByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.Manufacturers, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckManufacturerExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckManufacturerExistsByID request)
        {
            return await Workflows.Manufacturers.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckManufacturerExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckManufacturerExistsByKey request)
        {
            return await Workflows.Manufacturers.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckManufacturerExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckManufacturerExistsByName request)
        {
            return await Workflows.Manufacturers.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertManufacturer"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertManufacturer request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateManufacturer"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateManufacturer request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateManufacturer"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateManufacturer request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateManufacturerByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateManufacturerByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateManufacturerByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateManufacturerByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteManufacturerByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteManufacturerByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteManufacturerByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteManufacturerByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedManufacturerDataAsync,
                    () => Workflows.Manufacturers.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearManufacturerCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearManufacturerCache request)
        {
            await ClearCachedManufacturerDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedManufacturerDataAsync()
        {
            var urn = string.Empty;
            if (JSConfigs.CEFConfigDictionary.BrandsEnabled)
            {
                urn += ":" + new System.Uri(Request.AbsoluteUri).Host.Replace(":", "{colon}");
            }
            foreach (var key in CoreUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
            if (AdditionalUrnIDs == null) { return; }
            foreach (var key in AdditionalUrnIDs)
            {
                await ClearCachePrefixedAsync($"{key}{urn}").ConfigureAwait(false);
            }
        }

        private List<string> CoreUrnIDs
        {
            get
            {
                if (coreUrnIDs != null) { return coreUrnIDs; }
                return coreUrnIDs = new()
                {
                    UrnId.Create<GetManufacturers>(string.Empty),
                    UrnId.Create<GetManufacturerByID>(string.Empty),
                    UrnId.Create<GetManufacturerByKey>(string.Empty),
                    UrnId.Create<GetManufacturerByName>(string.Empty),
                    UrnId.Create<CheckManufacturerExistsByID>(string.Empty),
                    UrnId.Create<CheckManufacturerExistsByKey>(string.Empty),
                    UrnId.Create<CheckManufacturerExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class ManufacturerService : ManufacturerServiceBase { }
}
