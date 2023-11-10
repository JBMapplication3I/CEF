// <autogenerated>
// <copyright file="AttributeTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the attribute type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of attribute types.</summary>
    /// <seealso cref="AttributeTypeSearchModel"/>
    /// <seealso cref="IReturn{AttributeTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Attributes/AttributeTypes", "GET", Priority = 1,
            Summary = "Use to get a list of attribute types")]
    public partial class GetAttributeTypes : AttributeTypeSearchModel, IReturn<AttributeTypePagedResults> { }

    /// <summary>A ServiceStack Route to get attribute types for connect.</summary>
    /// <seealso cref="AttributeTypeSearchModel"/>
    /// <seealso cref="IReturn{List{AttributeTypeModel}}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.View"),
        PublicAPI,
        Route("/Attributes/AttributeTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all attribute types")]
    public partial class GetAttributeTypesForConnect : AttributeTypeSearchModel, IReturn<List<AttributeTypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all attribute types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.View"),
        PublicAPI,
        Route("/Attributes/AttributeTypesDigest", "GET",
            Summary = "Use to get a hash representing each attribute types")]
    public partial class GetAttributeTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get attribute type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{AttributeTypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Attributes/AttributeType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific attribute type")]
    public partial class GetAttributeTypeByID : ImplementsIDBase, IReturn<AttributeTypeModel> { }

    /// <summary>A ServiceStack Route to get attribute type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{AttributeTypeModel}"/>
    [PublicAPI,
        Route("/Attributes/AttributeType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific attribute type by the custom key")]
    public partial class GetAttributeTypeByKey : ImplementsKeyBase, IReturn<AttributeTypeModel> { }

    /// <summary>A ServiceStack Route to get attribute type.</summary>
    /// <seealso cref="IReturn{AttributeTypeModel}"/>
    [PublicAPI,
        Route("/Attributes/AttributeType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific attribute type by the name")]
    public partial class GetAttributeTypeByName : ImplementsNameBase, IReturn<AttributeTypeModel> { }

    /// <summary>A ServiceStack Route to get attribute type.</summary>
    /// <seealso cref="IReturn{AttributeTypeModel}"/>
    [PublicAPI,
        Route("/Attributes/AttributeType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific attribute type by the name")]
    public partial class GetAttributeTypeByDisplayName : ImplementsDisplayNameBase, IReturn<AttributeTypeModel> { }

    /// <summary>A ServiceStack Route to check attribute type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.View"),
        PublicAPI,
        Route("/Attributes/AttributeType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckAttributeTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check attribute type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.View"),
        PublicAPI,
        Route("/Attributes/AttributeType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckAttributeTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check attribute type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.View"),
        PublicAPI,
        Route("/Attributes/AttributeType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckAttributeTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check attribute type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.View"),
        PublicAPI,
        Route("/Attributes/AttributeType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckAttributeTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create attribute type.</summary>
    /// <seealso cref="AttributeTypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Attributes/AttributeType/Create", "POST", Priority = 1,
            Summary = "Use to create a new attribute type.")]
    public partial class CreateAttributeType : AttributeTypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert attribute type.</summary>
    /// <seealso cref="AttributeTypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Attributes/AttributeType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing attribute type (as needed).")]
    public partial class UpsertAttributeType : AttributeTypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update attribute type.</summary>
    /// <seealso cref="AttributeTypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Attributes/AttributeType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing attribute type.")]
    public partial class UpdateAttributeType : AttributeTypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate attribute type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Attributes/AttributeType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific attribute type from the system [Soft-Delete]")]
    public partial class DeactivateAttributeTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate attribute type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Deactivate"),
        PublicAPI,
        Route("/Attributes/AttributeType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific attribute type from the system [Soft-Delete]")]
    public partial class DeactivateAttributeTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate attribute type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Attributes/AttributeType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific attribute type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAttributeTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate attribute type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Reactivate"),
        PublicAPI,
        Route("/Attributes/AttributeType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific attribute type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateAttributeTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete attribute type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Attributes/AttributeType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific attribute type from the system [Hard-Delete]")]
    public partial class DeleteAttributeTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete attribute type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Attributes.AttributeType.Delete"),
        PublicAPI,
        Route("/Attributes/AttributeType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific attribute type from the system [Hard-Delete]")]
    public partial class DeleteAttributeTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear attribute type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Attributes/AttributeType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all attribute type calls.")]
    public class ClearAttributeTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class AttributeTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetAttributeTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAttributeTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<IAttributeTypeModel, AttributeTypeModel, IAttributeTypeSearchModel, AttributeTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.AttributeTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAttributeTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetAttributeTypesForConnect request)
        {
            return await Workflows.AttributeTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAttributeTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAttributeTypesDigest request)
        {
            return await Workflows.AttributeTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetAttributeTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAttributeTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.AttributeTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAttributeTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAttributeTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.AttributeTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAttributeTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAttributeTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.AttributeTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetAttributeTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetAttributeTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.AttributeTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckAttributeTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAttributeTypeExistsByID request)
        {
            return await Workflows.AttributeTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAttributeTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAttributeTypeExistsByKey request)
        {
            return await Workflows.AttributeTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAttributeTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAttributeTypeExistsByName request)
        {
            return await Workflows.AttributeTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckAttributeTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckAttributeTypeExistsByDisplayName request)
        {
            return await Workflows.AttributeTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertAttributeType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertAttributeType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateAttributeType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateAttributeType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateAttributeType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateAttributeType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateAttributeTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAttributeTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateAttributeTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateAttributeTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateAttributeTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAttributeTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateAttributeTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateAttributeTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteAttributeTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAttributeTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteAttributeTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteAttributeTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedAttributeTypeDataAsync,
                    () => Workflows.AttributeTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearAttributeTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearAttributeTypeCache request)
        {
            await ClearCachedAttributeTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedAttributeTypeDataAsync()
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
                    UrnId.Create<GetAttributeTypes>(string.Empty),
                    UrnId.Create<GetAttributeTypeByID>(string.Empty),
                    UrnId.Create<GetAttributeTypeByKey>(string.Empty),
                    UrnId.Create<GetAttributeTypeByName>(string.Empty),
                    UrnId.Create<CheckAttributeTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckAttributeTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckAttributeTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckAttributeTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class AttributeTypeService : AttributeTypeServiceBase { }
}
