// <copyright file="CartTypeService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart type service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Runtime.Remoting.Contexts;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentUser/UsedCartTypes", "POST",
            Summary = "Use to get a list of the cart types this user has.")]
    public class GetCurrentUserCartTypes : IReturn<CartTypePagedResults>
    {
        [ApiMember(Name = nameof(IncludeNotCreated), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "Include Cart Types not created by the User, such as the default Cart, Wish List, etc. Defaults to True if not set")]
        public bool? IncludeNotCreated { get; set; }

        [ApiMember(Name = nameof(FilterCartsByOrderRequest), DataType = "bool?", ParameterType = "body", IsRequired = false,
            Description = "Return only order requests")]
        public bool? FilterCartsByOrderRequest { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentUser/CartType", "GET",
            Summary = "Gets a custom cart type assigned to the current user (custom shopping lists).")]
    public class GetCartTypeForCurrentUser : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse<CartTypeModel>>
    {
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentUser/CreateCartType", "POST",
            Summary = "Create a custom cart type assigned to the current user (custom shopping lists).")]
    public class CreateCartTypeForCurrentUser : CartTypeModel, IReturn<CartTypeModel>
    {
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentUser/DeleteCartType", "DELETE",
            Summary = "Delete a custom cart type assigned to the current user (custom shopping lists).")]
    public class DeleteCartTypeForCurrentUser : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInStorefront,
        Authenticate, RequiredRole("Supervisor"),
        Route("/Shopping/CurrentUser/ShareShoppingCart", "POST",
            Summary = "Delete a custom cart type assigned to the current user (custom shopping lists).")]
    public class ShareShoppingCart : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(IncludedUsers), DataType = "int[]?", ParameterType = "body", IsRequired = true,
            Description = "Users to copy the shopping list to.")]
        public int[]? IncludedUsers { get; set; }

        [ApiMember(Name = nameof(ShoppingListTypeID), DataType = "int?", ParameterType = "body", IsRequired = true,
            Description = "The shopping list to copy.")]
        public int? ShoppingListTypeID { get; set; }

        [ApiMember(Name = nameof(ProductQuantities), DataType = "Dictionary<int, int>?", ParameterType = "body", IsRequired = false,
            Description = "A dictionary holding the product quantities")]
        public Dictionary<int, int>? ProductQuantities { get; set; }
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(ShareShoppingCart request)
        {
            if (Contract.CheckEmpty(request.IncludedUsers))
            {
                return CEFAR.FailingCEFAR("No Users were selected.");
            }
            if (Contract.CheckInvalidID(request.ShoppingListTypeID))
            {
                return CEFAR.FailingCEFAR("The shopping list ID provided was invalid.");
            }
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var res = await Workflows.CartTypes.ShareShoppingCartsWithSelectedUsersAsync(
                    brandID: await context.BrandUsers
                        .AsNoTracking()
                        .Where(x => x.SlaveID == CurrentUserIDOrThrow401)
                        .Select(y => y.MasterID)
                        .FirstOrDefaultAsync()
                        .ConfigureAwait(false),
                    userIDs: request.IncludedUsers,
                    cartTypeID: request.ShoppingListTypeID,
                    currentAccountID: await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false),
                    currentUserID: CurrentUserID,
                    productQuantities: request.ProductQuantities,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            return res;
        }

        public async Task<object?> Post(GetCurrentUserCartTypes request)
        {
            var userID = CurrentUserIDOrThrow401;
            var types = await Workflows.CartTypes.GetTypesForUserAsync(
                    userID: userID,
                    includeNotCreatedByUser: request.IncludeNotCreated ?? true,
                    filterCartsByOrderRequest: request.FilterCartsByOrderRequest ?? false,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            var selectedAccountID = await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false);
            types = types.Where(x => x.SerializableAttributes.Keys
                .Contains("accountID")
                    && x.SerializableAttributes.TryGetValue("accountID", out var acctID)
                    && acctID.Value == selectedAccountID
                    .ToString())
                .ToList();
            return new CartTypePagedResults
            {
                CurrentCount = types.Count,
                CurrentPage = 1,
                TotalCount = types.Count,
                TotalPages = 1,
                Results = types.Cast<CartTypeModel>().ToList(),
            };
        }

        public async Task<object?> Get(GetCartTypeForCurrentUser request)
        {
            return await Workflows.CartTypes.GetForUserAsync(
                    userID: CurrentUserIDOrThrow401,
                    typeName: request.TypeName ?? "Cart",
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(CreateCartTypeForCurrentUser request)
        {
            request.CreatedByUserID = CurrentUserIDOrThrow401;
            request.SerializableAttributes ??= new();
            request.SerializableAttributes.TryAdd(
                "accountID",
                new SerializableAttributeObject
                {
                    Key = "accountID",
                    Value = (await LocalAdminAccountIDOrThrow401Async(CurrentAccountIDOrThrow401).ConfigureAwait(false)).ToString(),
                });
            request.SerializableAttributes.TryAdd(
                "listOwnerUserID",
                new SerializableAttributeObject
                {
                    Key = "listOwnerUserID",
                    Value = CurrentUserIDOrThrow401.ToString(),
                });
            return await Workflows.CartTypes.UpsertForUserAsync(
                    userID: CurrentUserIDOrThrow401,
                    model: request,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(DeleteCartTypeForCurrentUser request)
        {
            return await Workflows.CartTypes.DeleteForUserAsync(
                    userID: CurrentUserIDOrThrow401,
                    cartTypeName: request.TypeName ?? "Cart",
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
