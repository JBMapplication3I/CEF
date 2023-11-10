// <copyright file="CartItemService.Admin.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item service class</summary>
// ReSharper disable MissingXmlDoc
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>A get cart items for user.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{List_SalesItemBaseModel_IAppliedCartItemDiscountModel_AppliedCartItemDiscountModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Items/{ID}/UserID/{UserID}/AccountID/{AccountID}", "POST",
            Summary = "Use to access all items in the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class AdminGetCartItemsForUser
        : ImplementsIDForAdminBase,
            IReturn<List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>>
    {
    }

    /// <summary>An add cart item for user.</summary>
    /// <seealso cref="ImplementsCartLookupForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_Nullable_int}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Item/Add", "POST",
            Summary = "Use to add an item to the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class AdminAddCartItemForUser : ImplementsCartLookupForAdminBase, IReturn<CEFActionResponse<int?>>
    {
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The ID of the cart to add to")]
        public int CartID { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The ID of the product to add")]
        public int ProductID { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [ApiMember(Name = nameof(Quantity), DataType = "decimal", ParameterType = "body", IsRequired = false,
            Description = "The quantity of the product to add")]
        public decimal Quantity { get; set; }

        /// <summary>Gets or sets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        [ApiMember(Name = nameof(SerializableAttributes), DataType = "SerializableAttributesDictionary", ParameterType = "body", IsRequired = false,
            Description = "Cart Item Serializable JSON Attributes")]
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }

        /// <summary>Gets or sets the targets.</summary>
        /// <value>The targets.</value>
        [ApiMember(Name = nameof(Targets), DataType = "List<SalesItemTargetBaseModel>", ParameterType = "body", IsRequired = false,
            Description = "Shipment to add to item")]
        public List<SalesItemTargetBaseModel>? Targets { get; set; }

        /// <summary>Gets or sets the force unique line item key.</summary>
        /// <value>The force unique line item key.</value>
        [ApiMember(Name = nameof(ForceUniqueLineItemKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public string? ForceUniqueLineItemKey { get; set; }
    }

    /// <summary>An add cart items for user.</summary>
    /// <seealso cref="ImplementsIDOnBodyForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_List_Nullable_int}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Items/Add", "POST",
            Summary = "Use to add multiple items to the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class AdminAddCartItemsForUser : ImplementsIDOnBodyForAdminBase, IReturn<CEFActionResponse<List<int?>>>
    {
        /// <summary>Gets or sets the items.</summary>
        /// <value>The items.</value>
        [ApiMember(Name = nameof(Items), DataType = "List<SalesItemBaseModel>", ParameterType = "body", IsRequired = true,
            Description = "Cart items to add")]
        public List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>> Items { get; set; } = null!;
    }

    /// <summary>An update cart items for user.</summary>
    /// <seealso cref="ImplementsIDForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Items/Update", "PUT",
            Summary = "Use to update multiple items in the specified session cart for the cart of the target user")]
    public class AdminUpdateCartItemsForUser : ImplementsIDForAdminBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the items.</summary>
        /// <value>The items.</value>
        [ApiMember(Name = nameof(Items), DataType = "List<SalesItemBaseModel>", ParameterType = "body", IsRequired = true,
            Description = "Cart items to update")]
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>> Items { get; set; } = null!;
    }

    /// <summary>An update cart item quantity for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_decimal}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Item/UpdateQuantity", "PUT",
            Summary = "Updates only the Quantity value for the item, overrides any previous value, not trying to adjust by offset. Returns the value supplied unless it needed to be modified for purchasing or stock limits.")]
    public class AdminUpdateCartItemQuantityForUser : IReturn<CEFActionResponse<decimal>>
    {
        /// <summary>Gets or sets the identifier of the cart item.</summary>
        /// <value>The identifier of the cart item.</value>
        [ApiMember(Name = nameof(CartItemID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier of the cart item to change the quantity value on.")]
        public int CartItemID { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        [ApiMember(Name = nameof(Quantity), DataType = "decimal", ParameterType = "body", IsRequired = true,
            Description = "The new Quantity value to set (actual number, not an offset).")]
        public decimal Quantity { get; set; }

        /// <summary>Gets or sets the quantity back ordered.</summary>
        /// <value>The quantity back ordered.</value>
        [ApiMember(Name = nameof(QuantityBackOrdered), DataType = "decimal", ParameterType = "body", IsRequired = true,
            Description = "The new Quantity Back-Ordered value to set (actual number, not an offset).")]
        public decimal QuantityBackOrdered { get; set; }

        /// <summary>Gets or sets the quantity pre sold.</summary>
        /// <value>The quantity pre sold.</value>
        [ApiMember(Name = nameof(QuantityPreSold), DataType = "decimal", ParameterType = "body", IsRequired = true,
            Description = "The new Quantity Pre-Sold value to set (actual number, not an offset).")]
        public decimal QuantityPreSold { get; set; }
    }

    /// <summary>A remove cart item by identifier.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Item/Remove/ByID/{ID}", "DELETE",
            Summary = "Use to deactivate a specific cart item by it's identifier (regardless of cart type name or session vs static cart type)")]
    public class AdminRemoveCartItemByIDForUser : ImplementsIDBase, IReturn<bool>
    {
    }

    /// <summary>A remove cart item discount.</summary>
    /// <seealso cref="ImplementsIDOnBodyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Item/Discount/Remove", "DELETE",
            Summary = "Removes a Discount from an item in the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)")]
    public class AdminRemoveCartItemDiscountForUser : ImplementsIDOnBodyForAdminBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the applied cart item discount.</summary>
        /// <value>The identifier of the applied cart item discount.</value>
        [ApiMember(Name = nameof(AppliedCartItemDiscountID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int AppliedCartItemDiscountID { get; set; }
    }

    public partial class CartItemService
    {
        public async Task<object?> Post(AdminGetCartItemsForUser request)
        {
            return (await Workflows.Carts.AdminGetAsync(
                        lookupKey: await GenCartByIDLookupKeyAsync(
                                cartID: request.ID,
                                userID: request.UserID,
                                accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.UserID, ServiceContextProfileName).ConfigureAwait(false),
                                storeID: request.StoreID,
                                franchiseID: request.FranchiseID,
                                brandID: request.BrandID)
                            .ConfigureAwait(false),
                        pricingFactoryContext: await PricingFactoryContextForOtherUserAsync(request.UserID).ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))!
                .SalesItems;
        }

        public async Task<object?> Post(AdminAddCartItemForUser request)
        {
            /* TODO: Client-specific action needs to be behind a setting
            if (!await CartValidator.CheckMultipleStoresInCartAsync(
                    productID: request.ProductID,
                    typeName: typeName,
                    sessionID: await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false),
                    pricingContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false))
            {
                return CEFAR.FailingCEFAR("Cannot add item. Cart cannot contain items from multiple stores.");
            }
            */
            return await Workflows.CartItems.AddCartItemAsync(
                    lookupKey: await LoadCartKeyDataForUseInSessionFunctionsAsync(request, request.CartID).ConfigureAwait(false),
                    quantity: request.Quantity,
                    targets: request.Targets?.ToList<ISalesItemTargetBaseModel>(),
                    productID: Contract.RequiresValidID(request.ProductID),
                    forceUniqueLineItemKey: request.ForceUniqueLineItemKey,
                    serializableAttributes: request.SerializableAttributes ?? new SerializableAttributesDictionary(),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AdminAddCartItemsForUser request)
        {
            if (Contract.CheckEmpty(request.Items))
            {
                return CEFAR.FailingCEFAR<List<int?>>(
                    "ERROR: This operation requires the Items list to be not null and contain items to add");
            }
            return await Workflows.CartItems.AdminUpdateMultipleAsync(
                    lookupKey: GenCartByIDLookupKey(request),
                    models: request.Items.ToList<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Put(AdminUpdateCartItemQuantityForUser request)
        {
            return await Workflows.CartItems.UpdateQuantityAsync(
                    cartItemID: request.CartItemID,
                    quantity: request.Quantity,
                    quantityBackOrdered: request.QuantityBackOrdered,
                    quantityPreSold: request.QuantityPreSold,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Put(AdminUpdateCartItemsForUser request)
        {
            return await Workflows.CartItems.AdminUpdateMultipleAsync(
                    lookupKey: new(
                        cartID: request.ID,
                        brandID: request.BrandID,
                        franchiseID: request.FranchiseID,
                        storeID: request.StoreID),
                    models: Contract.RequiresNotEmpty(request.Items)
                        .Where(x => x.ItemType == Enums.ItemType.Item)
                        .ToList<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(AdminRemoveCartItemByIDForUser request)
        {
            return await Workflows.CartItems.DeactivateAsync(
                    Contract.RequiresValidID(request.ID),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(AdminRemoveCartItemDiscountForUser request)
        {
            if (Contract.CheckInvalidID(request.AppliedCartItemDiscountID))
            {
                return CEFAR.FailingCEFAR("ERROR! Invalid Discount ID");
            }
            var cart = await Workflows.Carts.AdminGetAsync(
                    lookupKey: GenCartByIDLookupKey(request),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (cart is null)
            {
                return CEFAR.FailingCEFAR("ERROR! There is no cart to remove a discount from");
            }
            return await Workflows.DiscountManager.RemoveDiscountByAppliedCartItemDiscountIDAsync(
                    cart: cart,
                    appliedID: request.AppliedCartItemDiscountID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        /// <summary>Loads cart key data for use in session functions.</summary>
        /// <param name="request">The request.</param>
        /// <param name="cartID"> Identifier for the cart.</param>
        /// <returns>The cart key data for use in session functions.</returns>
        private async Task<SessionCartBySessionAndTypeLookupKey> LoadCartKeyDataForUseInSessionFunctionsAsync(
            ImplementsCartLookupForAdminBase request,
            int cartID)
        {
            var byIDLookupKey = new CartByIDLookupKey(
                cartID: cartID,
                userID: request.UserID,
                accountID: request.AccountID,
                brandID: request.BrandID,
                franchiseID: request.FranchiseID,
                storeID: request.StoreID);
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var toConvert = await context.Carts
                .AsNoTracking()
                .FilterCartsByLookupKey(byIDLookupKey)
                .Select(x => new { x.SessionID, TypeKey = x.Type!.CustomKey })
                .SingleAsync()
                .ConfigureAwait(false);
            return byIDLookupKey.ToSessionLookupKey(toConvert.TypeKey!, toConvert.SessionID!.Value);
        }
    }
}
