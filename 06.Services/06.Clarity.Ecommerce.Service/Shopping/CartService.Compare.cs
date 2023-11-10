// <copyright file="CartService.Compare.cs" company="clarity-ventures.com">
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
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCompareCart", "POST",
            Summary = "Use to get a specific compare cart for the current user (or anon)")]
    public class GetCurrentCompareCart
        : ImplementsCartLookupForStorefrontBase, IReturn<CartModel>
    {
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCompareCart/Items", "POST",
            Summary = "Use to get a specific compare cart for the current user (or anon)")]
    public class GetCurrentCompareCartItems
        : ImplementsCartLookupForStorefrontBase,
            IReturn<List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>>
    {
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCompareCart/Item/Add", "POST",
            Summary = "Use to add an item to the compare cart for the current user (or anon)")]
    public class AddCompareCartItem
        : ImplementsCartLookupForStorefrontBase,
            IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The ID of the product to add")]
        public int ProductID { get; set; }

        [ApiMember(Name = nameof(SerializableAttributes), DataType = "SerializableAttributesDictionary", ParameterType = "body", IsRequired = false,
            Description = "Cart Item Serializable JSON Attributes")]
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCompareCart/Item/Remove/ByProductID/{ProductID}", "DELETE",
            Summary = "Use to deactivate an item from the compare cart for the current user (or anon)")]
    public class RemoveCompareCartItemByProductID
        : ImplementsCartLookupForStorefrontBase,
            IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "path", IsRequired = true)]
        public int ProductID { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCompareCart/Clear", "DELETE",
            Summary = "Use to clear a specific compare cart for the current user (or anon)")]
    public class ClearCurrentCompareCart
        : ImplementsCartLookupForStorefrontBase,
            IReturn<CEFActionResponse>
    {
    }

    [PublicAPI]
    public partial class CartService
    {
        public async Task<object?> Post(GetCurrentCompareCart _)
        {
            await PickupCompareCartCookieAsync().ConfigureAwait(false);
            var (result, updatedID) = await Workflows.Carts.CompareGetAsync(
                    lookupKey: await GenCompareLookupKeyAsync().ConfigureAwait(false),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedID is not null
                && updatedID != default(Guid)
                && await GetSessionCompareCartGuidAsync().ConfigureAwait(false) != updatedID)
            {
                await OverrideCompareCartSessionGuidAsync(updatedID.Value).ConfigureAwait(false);
            }
            return result;
        }

        public async Task<object?> Post(GetCurrentCompareCartItems _)
        {
            await PickupCompareCartCookieAsync().ConfigureAwait(false);
            return (await Post(new GetCurrentCompareCart()).ConfigureAwait(false) as ICartModel)
                ?.SalesItems!
                .ToList();
        }

        public async Task<object?> Post(AddCompareCartItem request)
        {
            _ = Contract.RequiresValidID(request.ProductID);
            await PickupCompareCartCookieAsync().ConfigureAwait(false);
            var result = await Workflows.Carts.CompareAddItemAsync(
                    lookupKey: await GenCompareLookupKeyAsync().ConfigureAwait(false),
                    productID: request.ProductID,
                    attributes: request.SerializableAttributes,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (result.Result is not null
                && result.Result != default(Guid)
                && await GetSessionCompareCartGuidAsync().ConfigureAwait(false) != result.Result)
            {
                await OverrideCompareCartSessionGuidAsync(result.Result.Value).ConfigureAwait(false);
            }
            return result;
        }

        public async Task<object?> Delete(RemoveCompareCartItemByProductID request)
        {
            _ = Contract.RequiresValidID(request.ProductID);
            await PickupCompareCartCookieAsync().ConfigureAwait(false);
            return await Workflows.Carts.CompareRemoveAsync(
                    lookupKey: await GenCompareLookupKeyAsync().ConfigureAwait(false),
                    productID: request.ProductID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(ClearCurrentCompareCart _)
        {
            await PickupCompareCartCookieAsync().ConfigureAwait(false);
            var result = await Workflows.Carts.CompareClearAsync(
                    lookupKey: await GenCompareLookupKeyAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            await OverrideCompareCartSessionGuidAsync(Guid.NewGuid()).ConfigureAwait(false);
            return result;
        }
    }
}
