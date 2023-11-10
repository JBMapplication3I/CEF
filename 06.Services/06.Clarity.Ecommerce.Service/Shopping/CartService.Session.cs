// <copyright file="CartService.Session.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart service class</summary>
// ReSharper disable MissingXmlDoc
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using JSConfigs;
    using Models;
    using Providers.CartValidation;
    using ServiceStack;
    using Utilities;

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart", "POST",
            Summary = "Use to get a specific session cart for the current user (Shopping, Quote, Samples)")]
    public class GetCurrentCart : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse<CartModel>>
    {
        [ApiMember(Name = nameof(Validate), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? Validate { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/Clear", "DELETE",
            Summary = "Use to clear a specific session cart for the current user (Shopping, Quote, Samples)")]
    public class ClearCurrentCart : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/UpdateAttributes", "PUT",
            Summary = "Use to update the current user's session cart (Shopping, Quote, Samples) attributes")]
    public class CurrentCartUpdateAttributes : CartModel, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/SetSameAsBilling", "PATCH",
            Summary = "Set the shipping contact on the current user's session cart to be the same as billing or not for shipping calculations (Shopping, Quote, Samples)")]
    public class CurrentCartSetSetSameAsBilling : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(CartID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Cart ID (requires admin to set)")]
        public int? CartID { get; set; }

        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The User ID (requires admin to set)")]
        public int? UserID { get; set; }

        [ApiMember(Name = nameof(IsSameAsBilling), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "The flag value")]
        public bool IsSameAsBilling { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/GetShippingContact", "POST",
            Summary = "Get the shipping contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class CurrentCartGetShippingContact : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse<ContactModel>>
    {
        [ApiMember(Name = nameof(CartID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Cart ID (requires admin to set)")]
        public int? CartID { get; set; }

        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The User ID (requires admin to set)")]
        public int? UserID { get; set; }

        [ApiMember(Name = nameof(AccountID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Account ID (requires admin to set)")]
        public int? AccountID { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/SetShippingContact", "PATCH",
            Summary = "Set the shipping contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class CurrentCartSetShippingContact : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(CartID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Cart ID (requires admin to set)")]
        public int? CartID { get; set; }

        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The User ID (requires admin to set)")]
        public int? UserID { get; set; }

        [ApiMember(Name = nameof(ShippingContact), DataType = "ContactModel", ParameterType = "body", IsRequired = false,
            Description = "The shipping contact")]
        public ContactModel? ShippingContact { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/ClearShippingContact", "PATCH",
            Summary = "Clear the shipping contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class CurrentCartClearShippingContact : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/SetBillingContact", "PATCH",
            Summary = "Set the billing contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class CurrentCartSetBillingContact : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [ApiMember(Name = nameof(CartID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Cart ID (requires admin to set)")]
        public int? CartID { get; set; }

        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The User ID (requires admin to set)")]
        public int? UserID { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        [ApiMember(Name = nameof(BillingContact), DataType = "ContactModel", ParameterType = "body", IsRequired = false,
            Description = "The billing contact")]
        public ContactModel? BillingContact { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/ClearBillingContact", "PATCH",
            Summary = "Clear the shipping contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class CurrentCartClearBillingContact : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/AddDiscount", "PATCH",
            Summary = "Use to add and validate a discount code to the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)")]
    public class CurrentCartAddDiscount : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Discount Code")]
        public string Code { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/RemoveDiscount", "DELETE",
            Summary = "Use to remove an Cart Discount Code from the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)")]
    public class CurrentCartRemoveDiscount : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = false,
            Description = "Cart Discount ID")]
        public int ID { get; set; }
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/AddBufferFee", "PATCH",
            Summary = "Use to add the Buffer Sku in a quantity that will allow the requirements to be met for the current session Shopping cart (not available for any other cart types)")]
    public class CurrentCartAddBufferFee : ImplementsTypeNameForStorefrontBase, IReturn<bool>
    {
        [ApiMember(Name = nameof(AmountToFee), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Amount to Fee")]
        public string AmountToFee { get; set; } = null!;
    }

    [PublicAPI, UsedInStorefront,
        Route("/Shopping/CurrentCart/ValidateUnderOrderLimit", "POST",
            Summary = "Used to validate whether the current cart subtotal is lower than the user's ordering limit.")]
    public class ValidateUnderOrderLimit : ImplementsTypeNameForStorefrontBase, IReturn<CEFActionResponse>
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

        [ApiMember(Name = nameof(PriceForUnitOfMeasure), DataType = "decimal", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public decimal? PriceForUnitOfMeasure { get; set; }

        [ApiMember(Name = nameof(SelectedUnitOfMeasure), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "[Optional]")]
        public decimal? SelectedUnitOfMeasure { get; set; }
    }

    public partial class CartService
    {
        public async Task<object?> Post(ValidateUnderOrderLimit request)
        {
            if (CEFConfigDictionary.EnforceOrderLimits && !Contract.CheckNotNull(request.Quantity, request.PriceForUnitOfMeasure))
            {
                var typeName = request.TypeName ?? "Cart";
                if (!await CartValidator.CheckCartTotalUnderUserPurchasingLimitAsync(
                        quantity: request.Quantity,
                        price: request.PriceForUnitOfMeasure,
                        typeName: typeName,
                        sessID: await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false),
                        pricingContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                {
                    return CEFAR.FailingCEFAR("The order limit has been met. You must wait 24 hours from your last order to place another.");
                }
                return CEFAR.PassingCEFAR();
            }
            return CEFAR.PassingCEFAR();
        }

        public async Task<object?> Post(GetCurrentCart request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var taxesProvider = await GetTaxProviderAsync().ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            lookupKey.AltAccountID = await LocalAdminAccountIDOrThrow401Async(CurrentAccountID);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: taxesProvider,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            var cart = cartResponse.Result;
            var response = cart.WrapInPassingCEFAR();
            if (cart is null)
            {
                return response;
            }
            if ((request.TypeName ?? "Cart").StartsWith("Target-"))
            {
                // Don't validate target carts
                return response;
            }
            var validateResponse = await Workflows.CartValidator.ValidateReadyForCheckoutAsync(
                    cart: cart,
                    currentAccount: await CurrentAccountAsync().ConfigureAwait(false),
                    taxesProvider: taxesProvider,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    currentUserID: CurrentUserID,
                    currentAccountID: lookupKey.AccountID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (!validateResponse.ActionSucceeded)
            {
                // Cart has been modified, get the cart again with changes
                (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                        lookupKey: lookupKey,
                        pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                        taxesProvider: taxesProvider,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false);
                cart = cartResponse.Result;
                if (updatedSessionID is not null
                    && updatedSessionID != default(Guid)
                    && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
                {
                    await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
                }
                response.ActionSucceeded = cart is not null;
                response.Result = cart;
            }
            response.Messages.AddRange(validateResponse.Messages);
            return response;
        }

        public async Task<object?> Delete(ClearCurrentCart request)
        {
            var typeName = request.TypeName ?? "Cart";
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
            return await Workflows.Carts.SessionClearAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Put(CurrentCartUpdateAttributes request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            _ = Contract.RequiresValidID(id);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            var idLookupKey = await GenCartByIDLookupKeyAsync(id!.Value).ConfigureAwait(false);
            return await Workflows.Carts.UpdateAttributesAsync(
                    idLookupKey,
                    request.SerializableAttributes,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(CurrentCartSetSetSameAsBilling request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = Contract.CheckAllValidIDs(request.CartID, request.UserID)
                ? (await Workflows.Carts.CheckExistsAsync(request.CartID!.Value, contextProfileName: ServiceContextProfileName).ConfigureAwait(false), null)
                : await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.SetSameAsBillingAsync(
                        id!.Value,
                        request.IsSameAsBilling,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Post(CurrentCartGetShippingContact request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = Contract.CheckAllValidIDs(request.CartID, request.UserID)
                ? (await Workflows.Carts.CheckExistsAsync(request.CartID!.Value, contextProfileName: ServiceContextProfileName).ConfigureAwait(false), null)
                : await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.GetShippingContactAsync(
                        id!.Value,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Patch(CurrentCartSetShippingContact request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = Contract.CheckAllValidIDs(request.CartID, request.UserID)
                ? (await Workflows.Carts.CheckExistsAsync(request.CartID!.Value, contextProfileName: ServiceContextProfileName).ConfigureAwait(false), null)
                : await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.SetShippingContactAsync(
                        id!.Value,
                        request.ShippingContact,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Patch(CurrentCartSetBillingContact request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = Contract.CheckAllValidIDs(request.CartID, request.UserID)
                ? (await Workflows.Carts.CheckExistsAsync(request.CartID!.Value, contextProfileName: ServiceContextProfileName).ConfigureAwait(false), null)
                : await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.SetBillingContactAsync(
                        id!.Value,
                        request.BillingContact,
                        ServiceContextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Patch(CurrentCartClearShippingContact request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.ClearShippingContactAsync(id!.Value, ServiceContextProfileName).ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Patch(CurrentCartClearBillingContact request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.ClearBillingContactAsync(id!.Value, ServiceContextProfileName).ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Patch(CurrentCartAddDiscount request)
        {
            if (!Contract.CheckValidKey(request.Code))
            {
                return CEFAR.FailingCEFAR("Invalid discount code");
            }
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var pricing = await GetPricingFactoryContextAsync().ConfigureAwait(false);
            var taxesProvider = await GetTaxProviderAsync().ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (id, updatedSessionID) = await Workflows.Carts.CheckExistsByTypeNameAndSessionIDAsync(
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
            return await Workflows.DiscountManager.AddDiscountByCodeAsync(
                    request.Code,
                    id!.Value,
                    pricing,
                    taxesProvider,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(CurrentCartRemoveDiscount request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            if (Contract.CheckInvalidID(request.ID))
            {
                return CEFAR.FailingCEFAR("Invalid Discount ID");
            }
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    await GetTaxProviderAsync().ConfigureAwait(false),
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
            return await Workflows.DiscountManager.RemoveDiscountByAppliedCartDiscountIDAsync(
                    cart: cartResponse.Result,
                    appliedID: request.ID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(CurrentCartAddBufferFee request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartResponse, updatedSessionID) = await Workflows.Carts.SessionGetAsync(
                    lookupKey: lookupKey,
                    await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
            if (updatedSessionID is not null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            return await Workflows.Carts.AddCartFeeAsync(
                        cartResponse.Result!,
                        request.AmountToFee,
                        ServiceContextProfileName)
                    .ConfigureAwait(false)
                is not null;
        }
    }
}
