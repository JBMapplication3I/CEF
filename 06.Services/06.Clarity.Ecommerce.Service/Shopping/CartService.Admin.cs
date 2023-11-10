// <copyright file="CartService.Admin.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the cart service class</summary>
// ReSharper disable MissingXmlDoc
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using Providers.Emails.Shopping;
    using ServiceStack;
    using Utilities;

    /// <summary>An upsert cart for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_int}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/UpsertCartForUser", "POST",
            Summary = "Creates a Cart and assigns it to the User. Admins Only. Returns the ID of the cart (created or existing) wrapped in a CEFActionResponse")]
    public class AdminUpsertCartForUser : IReturn<CEFActionResponse<int>>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier for the record to call.")]
        public int ID { get; set; }

        /// <summary>Gets or sets the cart.</summary>
        /// <value>The cart.</value>
        [ApiMember(Name = nameof(Cart), DataType = "CartModel", ParameterType = "body", IsRequired = true,
            Description = "The optional cart model to use.")]
        public CartModel Cart { get; set; } = null!;
    }

    /// <summary>A get carts for user.</summary>
    /// <seealso cref="IReturn{List_CartModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/GetCartsForUserID/{UserID}", "POST",
            Summary = "Gets the list of carts the user has created. Admins Only")]
    public class AdminGetCartsForUser : IReturn<List<CartModel>>
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        [ApiMember(Name = nameof(UserID), DataType = "int", ParameterType = "path", IsRequired = true,
            Description = "The identifier of the user.")]
        public int UserID { get; set; }
    }

    /// <summary>A get user cart by identifier.</summary>
    /// <seealso cref="ImplementsIDOnBodyForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse_CartModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/GetUserCart", "POST",
            Summary = "Gets the Cart data by Cart ID under the context of the User ID. Admins Only")]
    public class AdminGetUserCartByID : ImplementsIDOnBodyForAdminBase, IReturn<CEFActionResponse<CartModel>>
    {
    }

    /// <summary>An update user cart.</summary>
    /// <seealso cref="CartModel"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/UpdateUserCart", "PUT",
            Summary = "Modifies a cart with updated data. Admins Only")]
    public class AdminUpdateUserCart : CartModel, IReturn<CEFActionResponse>
    {
    }

    /// <summary>An update cart attributes for user.</summary>
    /// <seealso cref="ImplementsIDForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/UpdateAttributes", "PUT",
            Summary = "Use to update the target user's session cart (Shopping, Quote, Samples) attributes. Admins Only")]
    public class AdminUpdateCartAttributesForUser : ImplementsIDForAdminBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the attributes.</summary>
        /// <value>The attributes.</value>
        [ApiMember(Name = nameof(Attributes), DataType = "SerializableAttributesDictionary", ParameterType = "body", IsRequired = true)]
        public SerializableAttributesDictionary Attributes { get; set; } = null!;
    }

    /// <summary>A get cart shipping contact for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse_ContactModel}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/GetShippingContact", "POST",
            Summary = "Get the shipping contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class AdminGetCartShippingContactForUser : IReturn<CEFActionResponse<ContactModel>>
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The Cart ID (requires admin to set)")]
        public int CartID { get; set; }
    }

    /// <summary>A set cart billing contact for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/SetBillingContact", "PATCH",
            Summary = "Set the billing contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class AdminSetCartBillingContactForUser : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The Cart ID (requires admin to set)")]
        public int CartID { get; set; }

        /// <summary>Gets or sets the billing contact.</summary>
        /// <value>The billing contact.</value>
        [ApiMember(Name = nameof(BillingContact), DataType = "ContactModel", ParameterType = "body", IsRequired = false,
            Description = "The billing contact")]
        public ContactModel? BillingContact { get; set; }
    }

    /// <summary>A set cart shipping contact for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/SetShippingContact", "PATCH",
            Summary = "Set the shipping contact on the current user's session cart (Shopping, Quote, Samples)")]
    public class AdminSetCartShippingContactForUser : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The Cart ID (requires admin to set)")]
        public int CartID { get; set; }

        /// <summary>Gets or sets the shipping contact.</summary>
        /// <value>The shipping contact.</value>
        [ApiMember(Name = nameof(ShippingContact), DataType = "ContactModel", ParameterType = "body", IsRequired = false,
            Description = "The shipping contact")]
        public ContactModel? ShippingContact { get; set; }
    }

    /// <summary>A clear cart for user.</summary>
    /// <seealso cref="ImplementsIDOnBodyForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/Clear", "DELETE",
            Summary = "Use to clear a specific session cart for the current user (Shopping, Quote, Samples)")]
    public class AdminClearCartForUser : ImplementsIDOnBodyForAdminBase, IReturn<CEFActionResponse>
    {
    }

    /// <summary>An add cart discount for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/AddDiscount", "PATCH",
            Summary = "Use to add and validate a discount code to the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)")]
    public class AdminAddCartDiscountForUser : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier for the record to call.")]
        public int CartID { get; set; }

        /// <summary>Gets or sets the code.</summary>
        /// <value>The code.</value>
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "Discount Code")]
        public string Code { get; set; } = null!;
    }

    /// <summary>A remove cart discount for user.</summary>
    /// <seealso cref="ImplementsIDOnBodyForAdminBase"/>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/RemoveDiscount", "DELETE",
            Summary = "Use to remove an Cart Discount Code from the current user's session cart (Shopping, Quote) (Not available for Static carts or Samples carts)")]
    public class AdminRemoveCartDiscountForUser : ImplementsIDOnBodyForAdminBase, IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the applied cart discount.</summary>
        /// <value>The identifier of the cart discount.</value>
        [ApiMember(Name = nameof(AppliedCartDiscountID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "Applied Cart Discount ID")]
        public int AppliedCartDiscountID { get; set; }
    }

    /// <summary>A clear hard soft stops caches.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Shopping/AdminForUser/Cart/ClearHardSoftStopsCaches", "DELETE",
            Summary = "Clears the caches for Hard and Soft Stops, use after editing the data in the Store, Product, etc.")]
    public class ClearHardSoftStopsCaches : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the pattern.</summary>
        /// <value>The pattern.</value>
        [ApiMember(Name = nameof(Pattern), DataType = "string", ParameterType = "body", IsRequired = false,
            Description = "Optionally, provide key pattern to specifically clear. If not set, all the Hard/Soft Stops caches are cleared.")]
        public string Pattern { get; set; } = null!;
    }

    /// <summary>A transfer quote cart to supervisor shopping list.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        UsedInStorefront,
        Authenticate,
        Route("/Shopping/TransferCartToSupervisor", "POST",
            Summary = "Use to send an email containing the current user's cart items to an email address")]
    public class TransferCartItemsToSupervisorShoppingCart : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int CartID { get; set; }

        [ApiMember(Name = nameof(SupervisorUserID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int SupervisorUserID { get; set; }

        [ApiMember(Name = nameof(CurrentUserUsername), DataType = "string?", ParameterType = "body", IsRequired = true)]
        public string? CurrentUserUsername { get; set; }

        [ApiMember(Name = nameof(ShippingAddressID), DataType = "int?", ParameterType = "body", IsRequired = false)]
        public int? ShippingAddressID { get; set; }
    }

    public partial class CartService
    {
        public async Task<object?> Post(TransferCartItemsToSupervisorShoppingCart request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            const string OrderRequest = "OrderRequest";
            var supervisorAccountID = await context.Users
                .AsNoTracking()
                .FilterByID(request.SupervisorUserID)
                .Select(x => x.AccountID)
                .FirstOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidID(supervisorAccountID))
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not locate an account associated to the supervisor.");
            }
            var userCart = await Workflows.Carts.ResolveAsync(request.CartID, null, null, context.ContextProfileName).ConfigureAwait(false);
            if (!userCart.ActionSucceeded)
            {
                return CEFAR.FailingCEFAR($"ERROR! Could not locate a cart with ID: {request.CartID}");
            }
            ICartTypeModel cartType = new CartTypeModel()
            {
                Active = true,
                CustomKey = $"{request.CurrentUserUsername}|{OrderRequest}|{DateTime.Now}",
                Name = $"{request.CurrentUserUsername}|{OrderRequest}|{DateTime.Now}",
                DisplayName = $"{request.CurrentUserUsername}|{OrderRequest}|{DateTime.Now}",
                CreatedByUserID = request.SupervisorUserID,
            };
            cartType.SerializableAttributes ??= new();
            cartType.SerializableAttributes.TryAdd("accountID", new SerializableAttributeObject { Key = "accountID", Value = CurrentAccountIDOrThrow401.ToString() });
            userCart.Result!.CustomKey = $"{request.CurrentUserUsername}|{OrderRequest}|{userCart.Result.ID}";
            userCart.Result!.AccountID = supervisorAccountID;
            userCart.Result!.SessionID = null;
            userCart.Result.Type = null;
            userCart.Result.TypeKey = $"{cartType.CustomKey}|{OrderRequest}|{userCart.Result.ID}";
            userCart.Result.User = null;
            userCart.Result.UserID = request.SupervisorUserID;
            userCart.Result.ShippingContactID = request.ShippingAddressID;
            userCart.Result.SerializableAttributes ??= new();
            userCart.Result.SerializableAttributes.TryAdd("originalAccountID", new SerializableAttributeObject { Key = "originalAccountID", Value = CurrentAccountIDOrThrow401.ToString() });
            var resolvedType = await Workflows.CartTypes.ResolveWithAutoGenerateToIDAsync(null, cartType.CustomKey, cartType, context).ConfigureAwait(false);
            if (!Contract.CheckValidID(resolvedType))
            {
                return CEFAR.FailingCEFAR("Error! Unable to process the cart type.");
            }
            userCart.Result.TypeID = resolvedType;
            var res = await Workflows.Carts.AdminUpdateAsync(userCart.Result, context.ContextProfileName).ConfigureAwait(false);
            try
            {
                var brandID = await this.ParseRequestUrlReferrerToBrandIDAsync().ConfigureAwait(false);
                var supervisorUser = await Workflows.Users.GetAsync(request.SupervisorUserID, ServiceContextProfileName).ConfigureAwait(false);
                var shopperUser = await this.CurrentUserAsync();
                await new ShoppingCartTransferredToSupervisorEmail()
                    .QueueAsync(
                        contextProfileName: ServiceContextProfileName,
                        to: null,
                        parameters: new Dictionary<string, object?>()
                        {
                            ["supervisorUser"] = supervisorUser,
                            ["shopperUser"] = shopperUser,
                            ["cart"] = userCart.Result,
                            ["brandID"] = brandID,
                        },
                        customReplacements: null)
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                res.Messages.Add($"{nameof(ShoppingCartTransferredToSupervisorEmail)} ERROR: {ex.Message}.");
            }
            return res.ActionSucceeded
                ? res
                : CEFAR.FailingCEFAR("ERROR! Unable to transfer the cart to the supervisor.");
        }

        public async Task<object?> Post(AdminUpsertCartForUser request)
        {
            if (Contract.CheckInvalidID(request.Cart.ID))
            {
                var response1 = await Workflows.Carts.AdminCreateAsync(request.Cart, ServiceContextProfileName).ConfigureAwait(false);
                return response1.ActionSucceeded
                    ? response1.Result.WrapInPassingCEFAR()
                    : response1.ChangeFailingCEFARType<int>();
            }
            var response2 = await Workflows.Carts.AdminUpdateAsync(request.Cart, ServiceContextProfileName).ConfigureAwait(false);
            return response2.ActionSucceeded
                ? request.Cart.ID.WrapInPassingCEFAR()
                : response2.ChangeFailingCEFARType<int>();
        }

        public async Task<object?> Post(AdminGetCartsForUser request)
        {
            return (await Workflows.Carts.SearchAsync(
                        search: new CartSearchModel { UserID = request.UserID, Active = true },
                        asListing: true,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                .results
                .Cast<CartModel>()
                .ToList();
        }

        public async Task<object?> Post(AdminGetUserCartByID request)
        {
            return ((CartModel?)await Workflows.Carts.AdminGetAsync(
                        lookupKey: await GenCartByIDLookupKeyAsync(
                                cartID: Contract.RequiresValidID(request.ID),
                                userID: Contract.RequiresValidID(request.UserID),
                                accountID: await Workflows.Accounts.GetIDByUserIDAsync(request.UserID, ServiceContextProfileName).ConfigureAwait(false),
                                storeID: request.StoreID,
                                brandID: request.BrandID)
                            .ConfigureAwait(false),
                        pricingFactoryContext: await PricingFactoryContextForOtherUserAsync(request.UserID).ConfigureAwait(false),
                        taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false))
                .WrapInPassingCEFARIfNotNull();
        }

        public async Task<object?> Put(AdminUpdateUserCart request)
        {
            return await Workflows.Carts.AdminUpdateAsync(request, ServiceContextProfileName).ConfigureAwait(false);
        }

        public async Task<object?> Put(AdminUpdateCartAttributesForUser request)
        {
            return await Workflows.Carts.UpdateAttributesAsync(
                    lookupKey: GenCartByIDLookupKey(request),
                    attrs: request.Attributes,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AdminGetCartShippingContactForUser request)
        {
            return (await Workflows.Carts.GetShippingContactAsync(request.CartID, ServiceContextProfileName).ConfigureAwait(false))
                .ChangeCEFARType<IContactModel, ContactModel>();
        }

        public async Task<object?> Patch(AdminSetCartBillingContactForUser request)
        {
            return await Workflows.Carts.SetBillingContactAsync(
                    request.CartID,
                    request.BillingContact,
                    ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(AdminSetCartShippingContactForUser request)
        {
            var id = await Workflows.Carts.CheckExistsAsync(request.CartID, ServiceContextProfileName).ConfigureAwait(false);
            return Contract.CheckValidID(id)
                ? await Workflows.Carts.SetShippingContactAsync(
                        id: id!.Value,
                        newContact: request.ShippingContact,
                        contextProfileName: ServiceContextProfileName)
                    .ConfigureAwait(false)
                : CEFAR.FailingCEFAR("ERROR! No cart found.");
        }

        public async Task<object?> Patch(AdminAddCartDiscountForUser request)
        {
            if (!Contract.CheckValidKey(request.Code))
            {
                return CEFAR.FailingCEFAR("ERROR! Invalid discount code");
            }
            return await Workflows.DiscountManager.AddDiscountByCodeAsync(
                    code: request.Code,
                    cartID: request.CartID,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    taxesProvider: await GetTaxProviderAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(AdminRemoveCartDiscountForUser request)
        {
            if (Contract.CheckInvalidID(request.AppliedCartDiscountID))
            {
                return CEFAR.FailingCEFAR("Invalid Discount ID");
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
            return await Workflows.DiscountManager.RemoveDiscountByAppliedCartDiscountIDAsync(
                    cart: cart,
                    appliedID: request.AppliedCartDiscountID,
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(AdminClearCartForUser request)
        {
            return await Workflows.Carts.AdminClearAsync(
                    lookupKey: GenCartByIDLookupKey(request),
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }

        public async Task<object?> Delete(ClearHardSoftStopsCaches request)
        {
            return await Workflows.CartValidator.ClearCachesAsync(request.Pattern).ConfigureAwait(false);
        }
    }
}
