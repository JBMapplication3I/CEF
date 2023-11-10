// <autogenerated>
// <copyright file="CalendarEventTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event type service class</summary>
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

    /// <summary>A ServiceStack Route to get a list of calendar event types.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{CalendarEventTypePagedResults}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/CalendarEvents/CalendarEventTypes", "GET", Priority = 1,
            Summary = "Use to get a list of calendar event types")]
    public partial class GetCalendarEventTypes : TypeSearchModel, IReturn<CalendarEventTypePagedResults> { }

    /// <summary>A ServiceStack Route to get calendar event types for connect.</summary>
    /// <seealso cref="TypeSearchModel"/>
    /// <seealso cref="IReturn{List{TypeModel}}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.View"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventTypesForConnect", "POST,GET", Priority = 1,
            Summary = "Get all calendar event types")]
    public partial class GetCalendarEventTypesForConnect : TypeSearchModel, IReturn<List<TypeModel>> { }

    /// <summary>A ServiceStack Route to get a digest of all calendar event types.</summary>
    /// <seealso cref="IReturn{List{DigestModel}}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.View"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventTypesDigest", "GET",
            Summary = "Use to get a hash representing each calendar event types")]
    public partial class GetCalendarEventTypesDigest : IReturn<List<DigestModel>> { }

    /// <summary>A ServiceStack Route to get calendar event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/CalendarEvents/CalendarEventType/ID/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific calendar event type")]
    public partial class GetCalendarEventTypeByID : ImplementsIDBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get calendar event type.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Key/{Key*}", "GET", Priority = 1,
            Summary = "Use to get a specific calendar event type by the custom key")]
    public partial class GetCalendarEventTypeByKey : ImplementsKeyBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get calendar event type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Name", "GET", Priority = 1,
            Summary = "Use to get a specific calendar event type by the name")]
    public partial class GetCalendarEventTypeByName : ImplementsNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to get calendar event type.</summary>
    /// <seealso cref="IReturn{TypeModel}"/>
    [PublicAPI,
        Route("/CalendarEvents/CalendarEventType/DisplayName", "GET", Priority = 1,
            Summary = "Use to get a specific calendar event type by the name")]
    public partial class GetCalendarEventTypeByDisplayName : ImplementsDisplayNameBase, IReturn<TypeModel> { }

    /// <summary>A ServiceStack Route to check calendar event type exists.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.View"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Exists/ID/{ID}", "GET", Priority = 1,
            Summary = "Check if this ID exists and return the id if it does (null if it does not)")]
    public partial class CheckCalendarEventTypeExistsByID : ImplementsIDBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check calendar event type exists by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.View"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Exists/Key/{Key*}", "GET", Priority = 1,
            Summary = "Check if this key exists and return the id if it does (null if it does not)")]
    public partial class CheckCalendarEventTypeExistsByKey : ImplementsKeyBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check calendar event type exists by Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.View"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Exists/Name", "GET", Priority = 1,
            Summary = "Check if this Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCalendarEventTypeExistsByName : ImplementsNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to check calendar event type exists by Display Name.</summary>
    /// <seealso cref="IReturn{int?}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.View"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Exists/DisplayName", "GET", Priority = 1,
            Summary = "Check if this Display Name exists and return the id if it does (null if it does not)")]
    public partial class CheckCalendarEventTypeExistsByDisplayName : ImplementsDisplayNameBase, IReturn<int?> { }

    /// <summary>A ServiceStack Route to create calendar event type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Create"),
        UsedInAdmin,
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Create", "POST", Priority = 1,
            Summary = "Use to create a new calendar event type.")]
    public partial class CreateCalendarEventType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to upsert calendar event type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate,
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Upsert", "POST", Priority = 1,
            Summary = "Use to create a new or update an existing calendar event type (as needed).")]
    public partial class UpsertCalendarEventType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to update calendar event type.</summary>
    /// <seealso cref="TypeModel"/>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Update"),
        UsedInAdmin,
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Update", "PUT", Priority = 1,
            Summary = "Use to update an existing calendar event type.")]
    public partial class UpdateCalendarEventType : TypeModel, IReturn<CEFActionResponse<int>> { }

    /// <summary>A ServiceStack Route to deactivate calendar event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Deactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Deactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific calendar event type from the system [Soft-Delete]")]
    public partial class DeactivateCalendarEventTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to deactivate calendar event type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Deactivate"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Deactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Deactivate a specific calendar event type from the system [Soft-Delete]")]
    public partial class DeactivateCalendarEventTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate calendar event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Reactivate"),
        UsedInAdmin,
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Reactivate/ID/{ID}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific calendar event type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCalendarEventTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to reactivate calendar event type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Reactivate"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Reactivate/Key/{Key*}", "PATCH", Priority = 1,
            Summary = "Reactivate a specific calendar event type from the system [Restore from Soft-Delete]")]
    public partial class ReactivateCalendarEventTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete calendar event type.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Delete"),
        UsedInAdmin,
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Delete/ID/{ID}", "DELETE", Priority = 1,
            Summary = "Removes a specific calendar event type from the system [Hard-Delete]")]
    public partial class DeleteCalendarEventTypeByID : ImplementsIDBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to delete calendar event type by key.</summary>
    /// <seealso cref="ImplementsKeyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [Authenticate, RequiredPermission("CalendarEvents.CalendarEventType.Delete"),
        PublicAPI,
        Route("/CalendarEvents/CalendarEventType/Delete/Key/{Key*}", "DELETE", Priority = 1,
            Summary = "Removed a specific calendar event type from the system [Hard-Delete]")]
    public partial class DeleteCalendarEventTypeByKey : ImplementsKeyBase, IReturn<CEFActionResponse> { }

    /// <summary>A ServiceStack Route to clear calendar event type cache.</summary>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI,
        UsedInAdmin,
        Route("/CalendarEvents/CalendarEventType/ClearCache", "DELETE",
            Summary = "Empties the server-side data cache for all calendar event type calls.")]
    public class ClearCalendarEventTypeCache : IReturn<bool> { }

    [PublicAPI]
    public abstract partial class CalendarEventTypeServiceBase : ClarityEcommerceServiceBase
    {
        private List<string>? coreUrnIDs;

        protected virtual List<string> AdditionalUrnIDs { get; } = new();

        #region Get Collections
        /// <summary>GET handler for the <see cref="GetCalendarEventTypes"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCalendarEventTypes request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultSetAsync<ITypeModel, TypeModel, ITypeSearchModel, CalendarEventTypePagedResults>(
                    request,
                    request.AsListing,
                    Workflows.CalendarEventTypes)
                .ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCalendarEventTypesForConnect"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Any(GetCalendarEventTypesForConnect request)
        {
            return await Workflows.CalendarEventTypes.SearchForConnectAsync(request, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCalendarEventTypesDigest"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCalendarEventTypesDigest request)
        {
            return await Workflows.CalendarEventTypes.GetDigestAsync(null).ConfigureAwait(false);
        }
        #endregion

        #region Get Singles
        /// <summary>GET handler for the <see cref="GetCalendarEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCalendarEventTypeByID request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByIDSingleAsync(request, Workflows.CalendarEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCalendarEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCalendarEventTypeByKey request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByKeySingleAsync(request, Workflows.CalendarEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCalendarEventTypeByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCalendarEventTypeByName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByNameSingleAsync(request, Workflows.CalendarEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="GetCalendarEventTypeByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(GetCalendarEventTypeByDisplayName request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultByDisplayNameSingleAsync(request, Workflows.CalendarEventTypes, noCache: request.noCache).ConfigureAwait(false);
        }
        #endregion

        #region Check if it exists
        /// <summary>GET handler for the <see cref="CheckCalendarEventTypeExistsByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCalendarEventTypeExistsByID request)
        {
            return await Workflows.CalendarEventTypes.CheckExistsAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCalendarEventTypeExistsByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCalendarEventTypeExistsByKey request)
        {
            return await Workflows.CalendarEventTypes.CheckExistsAsync(request.Key, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCalendarEventTypeExistsByName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCalendarEventTypeExistsByName request)
        {
            return await Workflows.CalendarEventTypes.CheckExistsByNameAsync(request.Name, contextProfileName: null).ConfigureAwait(false);
        }

        /// <summary>GET handler for the <see cref="CheckCalendarEventTypeExistsByDisplayName"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Get(CheckCalendarEventTypeExistsByDisplayName request)
        {
            return await Workflows.CalendarEventTypes.CheckExistsByDisplayNameAsync(request.DisplayName, contextProfileName: null).ConfigureAwait(false);
        }
        #endregion

        #region Create/Update
        /// <summary>POST handler for the <see cref="UpsertCalendarEventType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(UpsertCalendarEventType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>POST handler for the <see cref="CreateCalendarEventType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Post(CreateCalendarEventType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PUT handler for the <see cref="UpdateCalendarEventType"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Put(UpdateCalendarEventType request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.UpdateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Deactivate
        /// <summary>PATCH handler for the <see cref="DeactivateCalendarEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCalendarEventTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.DeactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="DeactivateCalendarEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(DeactivateCalendarEventTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.DeactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Reactivate
        /// <summary>PATCH handler for the <see cref="ReactivateCalendarEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCalendarEventTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.ReactivateAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>PATCH handler for the <see cref="ReactivateCalendarEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Patch(ReactivateCalendarEventTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.ReactivateAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Delete
        /// <summary>DELETE handler for the <see cref="DeleteCalendarEventTypeByID"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCalendarEventTypeByID request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.DeleteAsync(request.ID, contextProfileName: null))
                .ConfigureAwait(false);
        }

        /// <summary>DELETE handler for the <see cref="DeleteCalendarEventTypeByKey"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(DeleteCalendarEventTypeByKey request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedCalendarEventTypeDataAsync,
                    () => Workflows.CalendarEventTypes.DeleteAsync(request.Key, contextProfileName: null))
                .ConfigureAwait(false);
        }
        #endregion

        #region Clearing Caches
        /// <summary>DELETE handler for the <see cref="ClearCalendarEventTypeCache"/> endpoint.</summary>
        /// <param name="request">The request body DTO.</param>
        /// <returns>The content for the reply over the wire.</returns>
        public virtual async Task<object?> Delete(ClearCalendarEventTypeCache request)
        {
            await ClearCachedCalendarEventTypeDataAsync().ConfigureAwait(false);
            return true;
        }

        protected async Task ClearCachedCalendarEventTypeDataAsync()
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
                    UrnId.Create<GetCalendarEventTypes>(string.Empty),
                    UrnId.Create<GetCalendarEventTypeByID>(string.Empty),
                    UrnId.Create<GetCalendarEventTypeByKey>(string.Empty),
                    UrnId.Create<GetCalendarEventTypeByName>(string.Empty),
                    UrnId.Create<CheckCalendarEventTypeExistsByID>(string.Empty),
                    UrnId.Create<CheckCalendarEventTypeExistsByKey>(string.Empty),
                    UrnId.Create<CheckCalendarEventTypeExistsByName>(string.Empty),
                    UrnId.Create<CheckCalendarEventTypeExistsByDisplayName>(string.Empty),
                };
            }
        }
        #endregion
    }

    public partial class CalendarEventTypeService : CalendarEventTypeServiceBase { }
}
