// <copyright file="CartService.Static.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart service class</summary>
// ReSharper disable MissingXmlDoc
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
    using Providers.Emails;
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart", "POST",
            Summary = "Use to get a specific static cart for the current user (Wish List, Notify Me When In Stock, Favorites List)")]
    public class GetCurrentStaticCart : ImplementsTypeNameBase, IReturn<CartModel>
    {
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart/Items", "POST",
            Summary = "Use to access all items in the specified static cart for the current user (Wish List, Notify Me When In Stock, Favorites List)")]
    public class GetCurrentStaticCartItems
        : ImplementsTypeNameBase,
            IReturn<List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>>
    {
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart/Item/Add", "POST",
            Summary = "Use to add an item to the specified static cart for the current user (Wish List, Notify Me When In Stock, Favorites List)")]
    public class AddStaticCartItem : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The ID of the product to add")]
        public int ProductID { get; set; }

        [ApiMember(Name = nameof(Quantity), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The quantity of the product to add (Only applicable to Notify Me When In Stock static cart")]
        public decimal? Quantity { get; set; }

        [ApiMember(Name = nameof(SerializableAttributes), DataType = "SerializableAttributesDictionary", ParameterType = "body", IsRequired = false,
            Description = "Cart Item Serializable JSON Attributes")]
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart/Item/Remove", "DELETE",
            Summary = "Use to deactivate an item from the specified static cart for the current user (Wish List, Notify Me When In Stock, Favorites List)")]
    public class RemoveStaticCartItemByProductIDAndType : ImplementsTypeNameBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Product ID")]
        public int ProductID { get; set; }

        [ApiMember(Name = nameof(ForceUniqueLineItemKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public string? ForceUniqueLineItemKey { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart/Clear", "DELETE",
            Summary = "Use to clear a specific static cart for the current user (Wish List, Notify Me When In Stock, Favorites List)")]
    public class ClearCurrentStaticCart : ImplementsTypeNameBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart/ShareByEmail", "POST",
            Summary = "Use to send an email containing the current user's static cart items from the specified type to an email address")]
    public class ShareStaticCartItemsByEmail : ImplementsTypeNameBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(Email), DataType = "int", ParameterType = "body", IsRequired = true)]
        public string Email { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Shopping/CurrentStaticCart/Lot/Add", "POST",
            Summary = "Use to add a Lot to the specified static cart for the current user (Wish List, Notify Me When In Stock, Favorites List)")]
    public class AddStaticCartLot : ImplementsTypeNameBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(LotID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The ID of the lot to add")]
        public int LotID { get; set; }

        [ApiMember(Name = nameof(Quantity), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The quantity of the product to add (Only applicable to Notify Me When In Stock static cart")]
        public decimal? Quantity { get; set; }
    }

    public partial class CartService
    {
        public async Task<object?> Post(GetCurrentStaticCart request)
        {
            Contract.Requires<InvalidOperationException>(
                request.TypeName != "Compare Cart",
                "ERROR: Compare Cart has a specific endpoint.");
            return await Workflows.Carts.StaticGetAsync(
                    lookupKey: await GenStaticLookupKeyAsync(request.TypeName ?? "Wish List").ConfigureAwait(false),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(GetCurrentStaticCartItems request)
        {
            Contract.Requires<InvalidOperationException>(
                request.TypeName != "Compare Cart",
                "ERROR: Compare Cart has a specific endpoint.");
            return (await Post(new GetCurrentStaticCart { TypeName = request.TypeName }).ConfigureAwait(false) as ICartModel)
                ?.SalesItems!
                .ToList();
        }

        public async Task<object?> Post(AddStaticCartLot request)
        {
            return await Workflows.Carts.StaticAddLotAsync(
                    lookupKey: await GenStaticLookupKeyAsync(request.TypeName ?? "Wish List").ConfigureAwait(false),
                    Contract.RequiresValidID(request.LotID),
                    request.Quantity,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AddStaticCartItem request)
        {
            return await Workflows.Carts.StaticAddItemAsync(
                    lookupKey: await GenStaticLookupKeyAsync(request.TypeName ?? "Wish List").ConfigureAwait(false),
                    productID: Contract.RequiresValidID(request.ProductID),
                    quantity: request.Quantity,
                    attributes: request.SerializableAttributes ?? new(),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(RemoveStaticCartItemByProductIDAndType request)
        {
            return await Workflows.Carts.StaticRemoveAsync(
                    lookupKey: await GenStaticLookupKeyAsync(request.TypeName ?? "Wish List").ConfigureAwait(false),
                    productID: Contract.RequiresValidID(request.ProductID),
                    forceUniqueLineItemKey: request.ForceUniqueLineItemKey,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(ClearCurrentStaticCart request)
        {
            return await Workflows.Carts.StaticClearAsync(
                    lookupKey: await GenStaticLookupKeyAsync(request.TypeName ?? "Wish List").ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ShareStaticCartItemsByEmail request)
        {
            // Validations
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, true),
                "This operation requires a Static Cart type but was provided a Session Cart type");
            var session = GetSession();
            if (session is null)
            {
                throw HttpError.Unauthorized("No active session");
            }
            if (string.IsNullOrWhiteSpace(session.UserName))
            {
                throw HttpError.Unauthorized("No active user in session.");
            }
            // Actions
            var cart = await Workflows.Carts.StaticGetAsync(
                    lookupKey: await GenStaticLookupKeyAsync(typeName).ConfigureAwait(false),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (cart is null)
            {
                return CEFAR.FailingCEFAR("No cart to share");
            }
            cart.SalesItems = (await Workflows.CartItems.SearchAsync(new SalesItemBaseSearchModel { MasterID = cart.ID }, ServiceContextProfileName).ConfigureAwait(false))
                .results
                .ToList();
            cart.User = await CurrentUserAsync().ConfigureAwait(false);
            var result = await new ShoppingCartItemsSubmittedToSpecificEmailEmail().QueueAsync(
                    contextProfileName: ServiceContextProfileName,
                    to: request.Email,
                    parameters: new() { ["cart"] = cart })
                .ConfigureAwait(false);
            return result.ActionSucceeded ? CEFAR.PassingCEFAR() : result;
        }

        private static bool CheckSessionVsStaticCartTypeName(string cartTypeName, bool requireStatic)
        {
            switch (cartTypeName)
            {
                case "Cart":
                case "Shopping Cart":
                case "Quote Cart":
                case "Sample Cart":
                {
                    return !requireStatic;
                }
                // case "Wish List":
                // case "Notify Me When In Stock":
                // case "Favorites List":
                default:
                {
                    return requireStatic;
                }
            }
        }
    }
}
