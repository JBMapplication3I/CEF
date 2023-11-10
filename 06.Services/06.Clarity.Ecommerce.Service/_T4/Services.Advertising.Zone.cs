// <autogenerated>
// <copyright file="ZoneService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the zone service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of zones.</summary>
    /// <seealso cref="ZoneSearchModel"/>
    /// <seealso cref="IReturn{ZonePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/Zones", "GET", Priority = 1,
            Summary = "Use to get a list of zones")]
    public partial class GetZones : ZoneSearchModel, IReturn<ZonePagedResults> { }

    /// <summary>A ServiceStack Route to get zones for connect.</summary>
    /// <seealso cref="ZoneSearchModel"/>
    /// <seealso cref="IReturn{List{ZoneModel}}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.View"),
        PublicAPI,
        Route("/Advertising/ZonesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all zones")]
    public partial class GetZonesForConnect : ZoneSearchModel, IReturn<List<ZoneModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all zones.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.View"),
        PublicAPI,
        Route("/Advertising/ZonesDigest", "GET",
            Summary = "Use to get a hash representing each zones")]
    public partial class GetZonesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get zone.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{ZoneModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/Zone/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific zone")]
    public partial class GetZoneByID : ImplementsIDBase, IReturn<ZoneModel> { }

    /// <summary>A ServiceStack Route to get zone.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{ZoneModel}"/>
    [PublicAPI,
        Route("/Advertising/Zone/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific zone by the custom key")]
    public partial class GetZoneByKey : ImplementsKeyBase, IReturn<ZoneModel> { }

    /// <summary>A ServiceStack Route to get zone.</summary>
    /// <seealso cref="IReturn{ZoneModel}"/>
    [PublicAPI,
        Route("/Advertising/Zone/Name", "GET", Priority = 1,
            Summary = "Use to get a specific zone by the name")]
    public partial class GetZoneByName : ImplementsNameBase, IReturn<ZoneModel> { }

    /// <summary>A ServiceStack Route to check zone exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.View"),
        PublicAPI,
        Route("/Advertising/Zone/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckZoneExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check zone exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.View"),
        PublicAPI,
        Route("/Advertising/Zone/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckZoneExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check zone exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.View"),
        PublicAPI,
        Route("/Advertising/Zone/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckZoneExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create zone.</summary>
    /// <seealso cref="ZoneModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Zone/Create", "POST", Priority = 1,
            Summary = "Use to create a new zone.")]
    public partial class CreateZone : ZoneModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert zone.</summary>
    /// <seealso cref="ZoneModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Advertising/Zone/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing zone (as needed).")]
    public partial class UpsertZone : ZoneModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update zone.</summary>
    /// <seealso cref="ZoneModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Zone/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing zone.")]
    public partial class UpdateZone : ZoneModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate zone.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Zone/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific zone from the system [Soft-Delete]")]
    public partial class DeactivateZoneByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate zone by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Deactivate"),
        PublicAPI,
        Route("/Advertising/Zone/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific zone from the system [Soft-Delete]")]
    public partial class DeactivateZoneByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate zone.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Zone/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific zone from the system [Restore from Soft-Delete]")]
    public partial class ReactivateZoneByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate zone by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Reactivate"),
        PublicAPI,
        Route("/Advertising/Zone/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific zone from the system [Restore from Soft-Delete]")]
    public partial class ReactivateZoneByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete zone.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Advertising/Zone/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific zone from the system [Hard-Delete]")]
    public partial class DeleteZoneByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete zone by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Advertising.Zone.Delete"),
        PublicAPI,
        Route("/Advertising/Zone/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific zone from the system [Hard-Delete]")]
    public partial class DeleteZoneByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear zone cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Advertising/Zone/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all zone calls.")]
    public class ClearZoneCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class ZoneServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetZones"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetZones request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IZoneModel, ZoneModel, IZoneSearchModel, ZonePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Zones)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetZonesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetZonesForConnect request)
        {
            return await Workflows.Zones.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetZonesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetZonesDigest request)
        {
            return await Workflows.Zones.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetZoneByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetZoneByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Zones, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetZoneByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetZoneByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Zones, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetZoneByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetZoneByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.Zones, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckZoneExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckZoneExistsByID request)
        {
            return await Workflows.Zones.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckZoneExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckZoneExistsByKey request)
        {
            return await Workflows.Zones.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckZoneExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckZoneExistsByName request)
        {
            return await Workflows.Zones.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertZone"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertZone request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateZone"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateZone request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateZone"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateZone request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateZoneByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateZoneByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateZoneByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateZoneByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateZoneByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateZoneByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateZoneByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateZoneByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteZoneByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteZoneByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteZoneByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteZoneByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedZoneDataAsync,
                    () => Workflows.Zones.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearZoneCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearZoneCache request)
        {
            await ClearCachedZoneDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedZoneDataAsync()
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
                    UrnId.Create<GetZones>(string.Empty),
                    UrnId.Create<GetZoneByID>(string.Empty),
                    UrnId.Create<GetZoneByKey>(string.Empty),
                    UrnId.Create<GetZoneByName>(string.Empty),
                    UrnId.Create<CheckZoneExistsByID>(string.Empty),
                    UrnId.Create<CheckZoneExistsByKey>(string.Empty),
                    UrnId.Create<CheckZoneExistsByName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class ZoneService : ZoneServiceBase { }
}
