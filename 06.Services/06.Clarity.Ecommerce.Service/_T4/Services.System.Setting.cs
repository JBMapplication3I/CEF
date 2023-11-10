// <autogenerated>
// <copyright file="SettingService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of settings.</summary>
    /// <seealso cref="SettingSearchModel"/>
    /// <seealso cref="IReturn{SettingPagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Structure/Settings", "GET", Priority = 1,
            Summary = "Use to get a list of settings")]
    public partial class GetSettings : SettingSearchModel, IReturn<SettingPagedResults> { }

    /// <summary>A ServiceStack Route to get settings for connect.</summary>
    /// <seealso cref="SettingSearchModel"/>
    /// <seealso cref="IReturn{List{SettingModel}}"/>
    [Authenticate, RequiredPermission("Structure.Setting.View"),
        PublicAPI,
        Route("/Structure/SettingsForConnect", "POST,GET", Priority = 1,
            Summary = "Get all settings")]
    public partial class GetSettingsForConnect : SettingSearchModel, IReturn<List<SettingModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all settings.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("Structure.Setting.View"),
        PublicAPI,
        Route("/Structure/SettingsDigest", "GET",
            Summary = "Use to get a hash representing each settings")]
    public partial class GetSettingsDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get setting.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SettingModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Structure/Setting/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific setting")]
    public partial class GetSettingByID : ImplementsIDBase, IReturn<SettingModel> { }

    /// <summary>A ServiceStack Route to get setting.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{SettingModel}"/>
    [PublicAPI,
        Route("/Structure/Setting/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific setting by the custom key")]
    public partial class GetSettingByKey : ImplementsKeyBase, IReturn<SettingModel> { }

    /// <summary>A ServiceStack Route to check setting exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Structure.Setting.View"),
        PublicAPI,
        Route("/Structure/Setting/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckSettingExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check setting exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("Structure.Setting.View"),
        PublicAPI,
        Route("/Structure/Setting/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckSettingExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create setting.</summary>
    /// <seealso cref="SettingModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/Structure/Setting/Create", "POST", Priority = 1,
            Summary = "Use to create a new setting.")]
    public partial class CreateSetting : SettingModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert setting.</summary>
    /// <seealso cref="SettingModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/Structure/Setting/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing setting (as needed).")]
    public partial class UpsertSetting : SettingModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update setting.</summary>
    /// <seealso cref="SettingModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/Structure/Setting/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing setting.")]
    public partial class UpdateSetting : SettingModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate setting.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Structure/Setting/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific setting from the system [Soft-Delete]")]
    public partial class DeactivateSettingByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate setting by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Deactivate"),
        PublicAPI,
        Route("/Structure/Setting/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific setting from the system [Soft-Delete]")]
    public partial class DeactivateSettingByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate setting.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/Structure/Setting/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific setting from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSettingByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate setting by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Reactivate"),
        PublicAPI,
        Route("/Structure/Setting/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific setting from the system [Restore from Soft-Delete]")]
    public partial class ReactivateSettingByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete setting.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/Structure/Setting/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific setting from the system [Hard-Delete]")]
    public partial class DeleteSettingByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete setting by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("Structure.Setting.Delete"),
        PublicAPI,
        Route("/Structure/Setting/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific setting from the system [Hard-Delete]")]
    public partial class DeleteSettingByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear setting cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/Structure/Setting/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all setting calls.")]
    public class ClearSettingCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class SettingServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetSettings"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSettings request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ISettingModel, SettingModel, ISettingSearchModel, SettingPagedResults>(
                    request,
                    request.AsListing,
                    Workflows.Settings)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSettingsForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetSettingsForConnect request)
        {
            return await Workflows.Settings.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSettingsDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSettingsDigest request)
        {
            return await Workflows.Settings.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetSettingByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSettingByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.Settings, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetSettingByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetSettingByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.Settings, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckSettingExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSettingExistsByID request)
        {
            return await Workflows.Settings.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckSettingExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckSettingExistsByKey request)
        {
            return await Workflows.Settings.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertSetting"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertSetting request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateSetting"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateSetting request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateSetting"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateSetting request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateSettingByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSettingByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateSettingByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateSettingByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateSettingByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSettingByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateSettingByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateSettingByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteSettingByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSettingByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteSettingByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteSettingByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedSettingDataAsync,
                    () => Workflows.Settings.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearSettingCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearSettingCache request)
        {
            await ClearCachedSettingDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedSettingDataAsync()
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
                    UrnId.Create<GetSettings>(string.Empty),
                    UrnId.Create<GetSettingByID>(string.Empty),
                    UrnId.Create<GetSettingByKey>(string.Empty),
                    UrnId.Create<CheckSettingExistsByID>(string.Empty),
                    UrnId.Create<CheckSettingExistsByKey>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class SettingService : SettingServiceBase { }
}
