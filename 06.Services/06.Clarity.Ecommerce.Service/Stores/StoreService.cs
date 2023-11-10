// <copyright file="StoreService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Stores/Store/Full/ID/{ID}", "GET",
            Summary = "Use to get a specific store")]
    public partial class AdminGetStoreFull : ImplementsIDBase, IReturn<StoreModel>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Stores/Store/Current/Revenue/{Days}", "GET",
            Summary = "Use to get the current store's revenue (limited to store admins)")]
    public partial class AdminGetRevenueForStore : IReturn<CEFActionResponse<decimal>>
    {
        [ApiMember(Name = nameof(Days), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int Days { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Stores/Store/Current/SalesCount", "GET",
            Summary = "Use to get the current store's sales count by type (Order, Invoice, etc.) and Status (pending, shipped, void, approved, etc.) (limited to store admins)")]
    public partial class AdminGetSalesCountForStore : IReturn<CEFActionResponse<decimal>>
    {
        [ApiMember(Name = nameof(Type), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "Filter to the sales type (Quote, Order, Invoice)")]
        public string Type { get; set; } = null!;

        [ApiMember(Name = nameof(Status), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "Filter to a single status")]
        public string Status { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Stores/Store/Current/SalesCount", "POST",
            Summary = "Use to get the current store's sales count by type (Order, Invoice, etc.) and Status (pending, shipped, void, approved, etc.) (limited to store admins)")]
    public partial class AdminGetSalesCountMultipleStatusesForStore : IReturn<CEFActionResponse<decimal>>
    {
        [ApiMember(Name = nameof(Type), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Filter to the sales type (Quote, Order, Invoice)")]
        public string Type { get; set; } = null!;

        [ApiMember(Name = nameof(Statuses), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Filter to any of these statuses")]
        public string[] Statuses { get; set; } = null!;
    }

    [PublicAPI,
        Route("/Stores/Store/Current", "GET", Priority = 1,
            Summary = "Use to get the current store based on url, sub-domain or sub-folder")]
    public partial class GetCurrentStore : IReturn<CEFActionResponse<StoreModel>>
    {
    }

    [PublicAPI,
        Authenticate,
        Route("/Stores/Store/Current/Administration", "GET",
            Summary = "Use to get the store that the current user has administrative rights to (limited to store admins)")]
    public partial class GetCurrentStoreAdministration : IReturn<CEFActionResponse<StoreModel>>
    {
    }

    [PublicAPI,
        Route("/Stores/InventoryLocationsMatrix", "GET", Priority = 1,
            Summary = "Use to get the store that the current user has administrative rights to (limited to store admins)")]
    public partial class GetStoreInventoryLocationsMatrix : IReturn<CEFActionResponse<List<StoreInventoryLocationsMatrixModel>>>
    {
    }

    [PublicAPI,
        Route("/Stores/StoreAdministratorUser/{StoreID}", "GET", Priority = 1,
            Summary = "Use to get the Administrator of the Store's User info")]
    public partial class GetStoreAdministratorUser : IReturn<CEFActionResponse<UserModel>>
    {
        [ApiMember(Name = nameof(StoreID), DataType = "int", ParameterType = "path", IsRequired = true,
            Description = "The ID of the Store to find the attached User with Store Administrator rights.")]
        public int StoreID { get; set; }
    }

    [PublicAPI,
        Route("/Stores/Store/Current/Administration/SetAttributes", "PATCH", Summary = "")]
    public partial class UpdateCurrentAdminStoreAttributes : IReturn<CEFActionResponse<SerializableAttributesDictionary>>
    {
        [ApiMember(Name = nameof(SerializableAttributes), DataType = "SerializableAttributesDictionary", ParameterType = "body", IsRequired = true)]
        public SerializableAttributesDictionary SerializableAttributes { get; set; } = null!;
    }

    [PublicAPI,
        Authenticate,
        Route("/Stores/Store/Clone/ID/{ID}", "GET", Summary = "Use to clone a specific store")]
    public partial class CloneStore : ImplementsIDBase, IReturn<CEFActionResponse<StoreModel>>
    {
    }

    [PublicAPI]
    public partial class StoreService
    {
        // Multi-Store
        public Task<object?> Get(GetCurrentStore _)
        {
            throw HttpError.NotFound("This endpoint is non-functional");
            ////return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
            ////        request,
            ////        async () => await ParseRequestUrlReferrerToStoreLastModifiedAsync().ConfigureAwait(false),
            ////        async () => (await ParseRequestUrlReferrerToStoreAsync().ConfigureAwait(false) as StoreModel).WrapInPassingCEFARIfNotNull())
            ////    .ConfigureAwait(false);
            ////try
            ////{
            ////    var result = await ParseRequestUrlReferrerToStoreAsync().ConfigureAwait(false);
            ////    return result == null
            ////        ? CEFAR.FailingCEFAR<StoreModel>()
            ////        : ((StoreModel)result).WrapInPassingCEFAR();
            ////}
            ////catch (Exception ex)
            ////{
            ////    await Logger.LogErrorAsync("GetCurrentStore Error", ex.Message, ex, null).ConfigureAwait(false);
            ////    return CEFAR.FailingCEFAR<StoreModel>("Unable to locate current store");
            ////}
        }

        public async Task<object?> Get(GetStoreInventoryLocationsMatrix request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Stores.GetStoreInventoryLocationsMatrixLastModifiedAsync(null),
                    async () => (await Workflows.Stores.GetStoreInventoryLocationsMatrixAsync(null).ConfigureAwait(false))
                        .Cast<StoreInventoryLocationsMatrixModel>()
                        .ToList()
                        .WrapInPassingCEFAR())
                .ConfigureAwait(false);
        }

        // Store Administration
        public async Task<object?> Get(GetCurrentStoreAdministration _)
        {
            // NOTE: Never cached, admins only
            try
            {
                var result = (StoreModel?)await Workflows.Stores.GetAsync(
                        await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false),
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return result.WrapInPassingCEFARIfNotNull();
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync(
                        "GetCurrentStoreAdministration Error",
                        ex.Message,
                        ex,
                        null)
                    .ConfigureAwait(false);
                return CEFAR.FailingCEFAR<StoreModel>(
                    "Unable to locate current store the user would be administrator of");
            }
        }

        public async Task<object?> Get(AdminGetRevenueForStore request)
        {
            // NOTE: Never cached, admins only
            return await Workflows.Stores.GetRevenueAsync(
                    await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false),
                    request.Days,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(AdminGetSalesCountForStore request)
        {
            // NOTE: Never cached, admins only
            return await Workflows.Stores.GetSalesCountAsync(
                    await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false),
                    request.Type,
                    request.Status,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AdminGetSalesCountMultipleStatusesForStore request)
        {
            // NOTE: Never cached, admins only
            return await Workflows.Stores.GetSalesCountAsync(
                    await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false),
                    request.Type,
                    request.Statuses,
                    null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(GetStoreAdministratorUser request)
        {
            // NOTE: Never cached, admins only
            var result = await Workflows.Stores.GetStoreAdministratorUserAsync(request.StoreID, null).ConfigureAwait(false);
            return new CEFActionResponse<UserModel>(result, (UserModel?)result.Result);
        }

        public async Task<object?> Patch(UpdateCurrentAdminStoreAttributes request)
        {
            return await Workflows.Stores.UpdateAttributesAsync(
                    await CurrentStoreForStoreAdminIDOrThrow401Async().ConfigureAwait(false),
                    request.SerializableAttributes,
                    null)
                .ConfigureAwait(false);
        }

        // CEF Administration
        public async Task<object?> Get(AdminGetStoreFull request)
        {
            // NOTE: Never cached, admins only
            return await Workflows.Stores.GetFullAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(CloneStore request)
        {
            try
            {
                var result = await Workflows.Stores.CloneStoreAsync(
                        request.ID,
                        contextProfileName: null)
                    .ConfigureAwait(false);
                return result;
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("CloneStore Error", ex.Message, ex, null).ConfigureAwait(false);
                return false;
            }
        }
    }
}
