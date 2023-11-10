// <copyright file="CartItemService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart item service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Providers.CartValidation;
    using Providers.Emails;
    using ServiceStack;
    using Utilities;

    /// <summary>A get current cart items.</summary>
    /// <seealso cref="ImplementsTypeNameForStorefrontBase"/>
    /// <seealso cref="IReturn{List_SalesItemBaseModel_IAppliedCartItemDiscountModel_AppliedCartItemDiscountModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Items", "POST",
            Summary = "Use to access all items in the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class GetCurrentCartItems
        : ImplementsTypeNameForStorefrontBase,
            IReturn<List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>>
    {
    }

    /// <summary>An add cart item.</summary>
    /// <seealso cref="ImplementsTypeNameForStorefrontBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_Nullable_int}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Item/Add", "POST",
            Summary = "Use to add an item to the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class AddCartItem : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse<int?>>
    {
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

        [ApiMember(Name = nameof(PriceForUnitOfMeasure), DataType = "decimal", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public decimal? PriceForUnitOfMeasure { get; set; }

        [ApiMember(Name = nameof(SelectedUnitOfMeasure), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public decimal? SelectedUnitOfMeasure { get; set; }
    }

    /// <summary>An add cart items.</summary>
    /// <seealso cref="ImplementsTypeNameForStorefrontBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_List_Nullable_int}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Items/Add", "POST",
            Summary = "Use to add multiple items to the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class AddCartItems : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse<List<int?>>>
    {
        /// <summary>Gets or sets the items.</summary>
        /// <value>The items.</value>
        [ApiMember(Name = nameof(Items), DataType = "List<SalesItemBaseModel>", ParameterType = "body", IsRequired = true,
            Description = "Cart items to add")]
        public List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>> Items { get; set; } = null!;
    }

    /// <summary>An update cart item quantity.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_decimal}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Item/UpdateQuantity", "PUT",
            Summary = "Updates only the Quantity value for the item, overrides any previous value, not trying to adjust by offset. Returns the value supplied unless it needed to be modified for purchasing or stock limits.")]
    public class UpdateCartItemQuantity : IReturn<CEFActionResponse<decimal>>
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

    /// <summary>An update cart items.</summary>
    /// <seealso cref="ImplementsTypeNameForStorefrontBase"/>
    /// <seealso cref="IReturn{List_SalesItemBaseModel_IAppliedCartItemDiscountModel_AppliedCartItemDiscountModel}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Items/Update", "PUT",
            Summary = "Use to update multiple items in the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class UpdateCartItems : ImplementsTypeNameForStorefrontBase, IReturn<List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>>
    {
        /// <summary>Gets or sets the items.</summary>
        /// <value>The items.</value>
        [ApiMember(Name = nameof(Items), DataType = "List<SalesItemBaseModel>", ParameterType = "body", IsRequired = true,
            Description = "Cart items to update")]
        // ReSharper disable once CollectionNeverUpdated.Global
        public List<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>> Items { get; set; } = null!;
    }

    /// <summary>An add buffer SKU cart item.</summary>
    /// <seealso cref="ImplementsTypeNameBase"/>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/AddBufferSkuCartItem", "PATCH",
            Summary = "Use to add the Buffer Sku in a quantity that will allow the requirements to be met in the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class AddBufferSkuCartItem : ImplementsTypeNameForStorefrontBase, IReturn<bool>
    {
        [ApiMember(Name = nameof(BufferSkuSEOURL), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Buffer Sku SEO URL")]
        public string BufferSkuSEOURL { get; set; } = null!;

        [ApiMember(Name = nameof(AmountToFill), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Amount to Fill (will divide cost of item to get quantity to add)")]
        public string AmountToFill { get; set; } = null!;
    }

    /// <summary>A remove cart item by product identifier and type.</summary>
    /// <seealso cref="ImplementsTypeNameBase"/>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Item/Remove/ByProductIDAndType", "DELETE",
            Summary = "Use to deactivate an item from the specified session cart for the current user (Shopping, Quote, Samples)")]
    public class RemoveCartItemByProductIDAndType : ImplementsTypeNameForStorefrontBase, IReturn<bool>
    {
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Product ID")]
        public int ProductID { get; set; }

        [ApiMember(Name = nameof(ForceUniqueLineItemKey), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public string? ForceUniqueLineItemKey { get; set; }
    }

    /// <summary>A remove cart item by identifier.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{bool}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Item/Remove/ByID/{ID}", "GET",
            Summary = "Use to deactivate a specific cart item by it's identifier (regardless of cart type name or session vs static cart type)")]
    public class RemoveCartItemByID : ImplementsIDBase, IReturn<bool>
    {
    }

    /// <summary>A remove cart item discount.</summary>
    /// <seealso cref="ImplementsIDOnBodyBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Item/Discount/Remove", "DELETE",
            Summary = "Removes a Discount from an item in the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)")]
    public class RemoveCartItemDiscount : ImplementsIDOnBodyBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>A share current cart items by email.</summary>
    /// <seealso cref="ImplementsTypeNameBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/ShareByEmail", "POST",
            Summary = "Use to send an email containing the current user's cart items to an email address")]
    public class ShareCurrentCartItemsByEmail : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(Email), DataType = "int", ParameterType = "body", IsRequired = true)]
        public string Email { get; set; } = null!;
    }

    [PublicAPI]
    public partial class CartItemService
    {
        public override async Task<object?> Get(GetCartItems request)
        {
            var (results, totalPages, totalCount) = await Workflows.CartItems.SearchAsync(
                    request,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
            return new CartItemPagedResults
            {
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalCount = totalCount,
                TotalPages = totalPages,
                Groupings = request.Groupings,
                Sorts = request.Sorts,
                Results = results
                    .Cast<SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>>()
                    .ToList(),
            };
        }

        public override async Task<object?> Put(UpdateCartItem request)
        {
            // TODO: Research anything necessary for 304 caching resets
            // Validations
            _ = Contract.RequiresValidID(request.ID);
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var typeName = await context.CartItems
                .AsNoTracking()
                .FilterByID(request.ID)
                .Select(x => x.Master!.Type!.Name!)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidKey(typeName))
            {
                return CEFAR.FailingCEFAR("Could not locate the item to determine cart type");
            }
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            // Filter out products the current account isn't allowed to purchase
            var currentAccount = await CurrentAccountAsync().ConfigureAwait(false);
            Contract.Requires<InvalidOperationException>(
                currentAccount is null
                || (await Workflows.CartItems.CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
                        currentAccount.Type?.Name,
                        request.ID,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                .ActionSucceeded,
                "This account is not allowed to purchase this product. Please contact support for assistance");
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Actions
            if (!(await Workflows.CartItems.UpdateAsync(
                        lookupKey: lookupKey,
                        model: new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                        {
                            // TODO: Verify if we need to be consuming this way instead of just passing the request directly
                            Active = true,
                            CreatedDate = DateExtensions.GenDateTime,
                            ProductID = request.ID,
                            Quantity = request.Quantity,
                            QuantityBackOrdered = request.QuantityBackOrdered,
                            QuantityPreSold = request.QuantityPreSold,
                            SerializableAttributes = request.SerializableAttributes,
                            Targets = request.Targets,
                        },
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                .ActionSucceeded)
            {
                return null;
            }
            return await Workflows.CartItems.GetAsync(
                    request.ID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(GetCurrentCartItems request)
        {
            // Validations
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            // Actions
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            if (!cartResponse.ActionSucceeded)
            {
                return cartResponse;
            }
            return cartResponse.Result!.SalesItems;
        }

        public async Task<object?> Post(AddCartItem request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            decimal price = 0m;
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            if (CEFConfigDictionary.CartValidationSingleStoreInCartEnabled)
            {
                if (!await CartValidator.CheckMultipleStoresInCartAsync(
                        productID: request.ProductID,
                        typeName: typeName,
                        sessID: await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false),
                        pricingContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR("Cannot add item. Cart cannot contain items from multiple stores.", new[] { "MULTIPLE_STORES_ERROR!" });
                }
            }
            if (CEFConfigDictionary.EnforceOrderLimits)
            {
                if (request.SerializableAttributes != null
                    && request.SerializableAttributes!.TryGetValue("SoldPrice", out var soldPrice))
                {
                    price = decimal.Parse(soldPrice.Value);
                }
                else
                {
                    var productPrices = (await context.Products
                            .AsNoTracking()
                            .FilterByActive(true)
                            .Where(x => x.ID == request.ProductID)
                            .Select(x => new
                            {
                                x.PriceBase,
                                x.PriceSale,
                            })
                            .ToListAsync()
                            .ConfigureAwait(false))
                        .Select(x => new
                        {
                            PriceBase = x.PriceBase,
                            PriceSale = x.PriceSale,
                        })
                        .SingleOrDefault();
                    if (productPrices != null)
                    {
                        price = (decimal)(productPrices!.PriceSale! > 0 ? productPrices!.PriceSale! : productPrices!.PriceBase!);
                    }
                }
                if (!await CartValidator.CheckCartTotalUnderUserPurchasingLimitAsync(
                        quantity: request.Quantity,
                        price: price,
                        typeName: typeName,
                        sessID: await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false),
                        pricingContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR("The order limit has been met. You must wait 24 hours from your last order to place another.");
                }
            }
            if (CEFConfigDictionary.EnforceProductLicensingInCart
                && !(await CartValidator.CheckProductRequiresLicenseAndValidateAccountLicenseExpirationDate(
                    productID: request.ProductID,
                    sessID: await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false),
                    typeName: typeName,
                    contextProfileName: ServiceContextProfileName)))
            {
                return CEFAR.FailingCEFAR("The product licensing requirments have not been met. This item will not be added to the cart.");
            }
            return await Workflows.CartItems.AddCartItemAsync(
                    lookupKey: lookupKey,
                    quantity: request.Quantity,
                    targets: request.Targets?.ToList<ISalesItemTargetBaseModel>(),
                    productID: Contract.RequiresValidID(request.ProductID),
                    forceUniqueLineItemKey: request.ForceUniqueLineItemKey,
                    serializableAttributes: request.SerializableAttributes ?? new SerializableAttributesDictionary(),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AddCartItems request)
        {
            // Validations
            if (Contract.CheckEmpty(request.Items))
            {
                return CEFAR.FailingCEFAR<List<int?>>(
                    "ERROR: This operation requires the Items list to be not null and contain items to add");
            }
            var typeName = request.TypeName ?? "Cart";
            if (!CheckSessionVsStaticCartTypeName(typeName, false))
            {
                return CEFAR.FailingCEFAR<List<int?>>(
                    "ERROR: This operation requires a Session Cart type but was provided a Static Cart type");
            }
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Actions
            var accountTypeName = await CurrentAccountTypeNameAsync().ConfigureAwait(false);
            var productIDs = accountTypeName is null
                ? new()
                : request.Items.Select(x => x.ProductID).Where(Contract.CheckValidID).Distinct().ToList();
            // Filter out products the current account isn't allowed to purchase
            var approvedProductIDs = new List<int?>();
            foreach (var x in productIDs)
            {
                if ((await Workflows.CartItems.CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
                            accountTypeName,
                            x ?? 0,
                            ServiceContextProfileName)
                        .ConfigureAwait(false))
                    .ActionSucceeded)
                {
                    approvedProductIDs.Add(x);
                }
            }
            var items = request.Items
                .Where(x => accountTypeName is null
                    || !Contract.CheckAnyValidID(x.ProductID)
                    || !CEFConfigDictionary.CartValidationDoProductRestrictions
                    || approvedProductIDs.Contains(x.ProductID))
                .ToList<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>();
            return await Workflows.CartItems.UpsertMultipleAsync(
                    lookupKey: lookupKey,
                    models: items,
                    pricingFactory: RegistryLoaderWrapper.GetInstance<IPricingFactory>(ServiceContextProfileName),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Put(UpdateCartItemQuantity request)
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

        public async Task<object?> Put(UpdateCartItems request)
        {
            // Validations
            _ = Contract.RequiresNotNull(request.Items);
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            lookupKey.AltAccountID = await LocalAdminAccountIDAsync(CurrentAccountID).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Actions
            var response = await Workflows.CartItems.UpdateMultipleAsync(
                    lookupKey: lookupKey,
                    models: request.Items
                        .Where(x => x.ItemType == Enums.ItemType.Item)
                        //.Select(i => new SalesItemBaseModel<IAppliedCartItemDiscountModel, AppliedCartItemDiscountModel>
                        //{
                        //    ProductID = i.ProductID,
                        //    SerializableAttributes = i.SerializableAttributes,
                        //    Quantity = i.Quantity,
                        //    Targets = i.Targets,
                        //    Discounts = i.Discounts,
                        //})
                        .ToList<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>(),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (!response.ActionSucceeded)
            {
                throw new ArgumentException(
                    "ERROR! Unable to complete the Update of these cart items.",
                    new ArgumentException(response.Messages.Aggregate((c, n) => $"{c}\r\n{n}")));
            }
            var (cartResponse, updatedSessionID2) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID2 is not null
                && updatedSessionID2 != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID2)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID2.Value).ConfigureAwait(false);
            }
            return cartResponse.Result!.SalesItems;
        }

        public async Task<object?> Patch(AddBufferSkuCartItem request)
        {
            // Validations
            _ = Contract.RequiresValidKey(request.BufferSkuSEOURL);
            _ = Contract.RequiresValidKey(request.AmountToFill);
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Actions
            return await Workflows.CartItems.AddBufferSkuCartItemAsync(
                    lookupKey: lookupKey,
                    bufferSkuSeoUrl: request.BufferSkuSEOURL,
                    amountToFill: request.AmountToFill.Replace("$", string.Empty),
                    pricingFactory: RegistryLoaderWrapper.GetInstance<IPricingFactory>(ServiceContextProfileName),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(RemoveCartItemByProductIDAndType request)
        {
            // Validations
            _ = Contract.RequiresValidID(request.ProductID);
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            // Actions
            return await Workflows.CartItems.DeactivateAsync(
                    lookupKey: lookupKey,
                    productID: request.ProductID,
                    forceUniqueLineItemKey: request.ForceUniqueLineItemKey,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Get(RemoveCartItemByID request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var typeName = await context.CartItems
                .AsNoTracking()
                .FilterByID(request.ID)
                .Select(x => x.Master!.Type!.Name!)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidKey(typeName))
            {
                // Just return a positive result as it wasn't there
                return CEFAR.PassingCEFAR();
            }
            var retVal = await Workflows.CartItems.DeactivateAsync(
                    Contract.RequiresValidID(request.ID),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartID, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            if (Contract.CheckValidID(cartID))
            {
                await Workflows.DiscountManager.VerifyCurrentDiscountsAsync(
                        cartID: cartID!.Value,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
            }
            return retVal;
        }

        public async Task<object?> Delete(RemoveCartItemDiscount request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            var typeName = await context.AppliedCartItemDiscounts
                .AsNoTracking()
                .FilterByID(request.ID)
                .Select(x => x.Master!.Master!.Type!.Name!)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidKey(typeName))
            {
                // Just return a positive result as it wasn't there
                return CEFAR.PassingCEFAR();
            }
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            if (Contract.CheckInvalidID(request.ID))
            {
                return CEFAR.FailingCEFAR("Invalid Discount ID");
            }
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            if (cartResponse.Result is null)
            {
                return CEFAR.FailingCEFAR("ERROR! There is no cart to remove a discount from");
            }
            return await Workflows.DiscountManager.RemoveDiscountByAppliedCartItemDiscountIDAsync(
                    cart: cartResponse.Result,
                    appliedID: request.ID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ShareCurrentCartItemsByEmail request)
        {
            // Validations
            var typeName = request.TypeName ?? "Cart";
            Contract.Requires<InvalidOperationException>(
                CheckSessionVsStaticCartTypeName(typeName, false),
                "This operation requires a Session Cart type but was provided a Static Cart type");
            var session = GetSession();
            if (session is null)
            {
                throw HttpError.Unauthorized("No active session");
            }
            if (string.IsNullOrWhiteSpace(session.UserName))
            {
                throw HttpError.Unauthorized("No active user in session.");
            }
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            // Actions
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            cartResponse.Result!.User = await CurrentUserAsync().ConfigureAwait(false);
            var result = await new ShoppingCartItemsSubmittedToSpecificEmailEmail().QueueAsync(
                    contextProfileName: ServiceContextProfileName,
                    to: request.Email,
                    parameters: new() { ["cart"] = cartResponse.Result })
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
                    if (cartTypeName.IndexOf("target", StringComparison.OrdinalIgnoreCase) >= 0)
                    {
                        return !requireStatic;
                    }
                    return requireStatic;
                }
            }
        }
    }
}
