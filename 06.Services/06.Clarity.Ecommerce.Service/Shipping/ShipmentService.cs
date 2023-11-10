// <copyright file="ShipmentService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipment service class</summary>
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
    using JSConfigs;
    using Models;
    using ServiceStack;
    using Utilities;

    [PublicAPI,
        Route("/Shopping/Cart/IsShippingRequired/{ID}", "POST", Summary = "Use to get shipping rates for the current cart.")]
    public partial class IsShippingRequiredForCart : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Route("/Shopping/CurrentCart/IsShippingRequired", "POST", Summary = "Use to get shipping rates for the current cart.")]
    public partial class IsShippingRequired : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(TypeName), Description = "Cart Type Name", IsRequired = false)]
        public string? TypeName { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Shopping/AdminForUser/CartShippingRateQuotes", "POST",
            Summary = "Use to get shipping rates for the specified cart.")]
    public partial class GetCartShippingRateQuotes : IReturn<CEFActionResponse<List<RateQuoteModel>>>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The identifier for the record to call.")]
        public int ID { get; set; }

        /// <summary>Gets or sets a value indicating whether the expedited.</summary>
        /// <value>True if expedited, false if not.</value>
        [ApiMember(Name = nameof(Expedited), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Adding the Expedited flag will activate the fee and change to target ship by with expedited timeline.")]
        public bool Expedited { get; set; }
    }

    [PublicAPI,
        Route("/Shopping/CurrentCart/ShippingRateQuotes", "POST",
            Summary = "Use to get shipping rates for the current cart.")]
    public partial class GetCurrentCartShippingRateQuotes : IReturn<CEFActionResponse<List<RateQuoteModel>>>
    {
        [ApiMember(Name = nameof(TypeName), Description = "Cart Type Name", IsRequired = false)]
        public string? TypeName { get; set; }

        /// <summary>Gets or sets a value indicating whether the expedited.</summary>
        /// <value>True if expedited, false if not.</value>
        [ApiMember(Name = nameof(Expedited), DataType = "bool", ParameterType = "body", IsRequired = true,
            Description = "Adding the Expedited flag will activate the fee and change to target ship by with expedited timeline.")]
        public bool Expedited { get; set; }
    }

    [PublicAPI,
        Authenticate,
        Route("/Shopping/Cart/ShippingRateQuotes/Apply", "POST",
            Summary = "Use to get shipping rates for the current cart.")]
    public partial class ApplyCartShippingRateQuote : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "body", IsRequired = true,
            Description = "The Identifier of the Cart.")]
        public int ID { get; set; }

        [ApiMember(Name = nameof(RateQuoteID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Key of the rate quote to select. Send null to un-assign the current rate quote.")]
        public int? RateQuoteID { get; set; }

        [ApiMember(Name = nameof(RequestedShipDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false,
            Description = "The requested ship by date from the Customer.")]
        public DateTime? RequestedShipDate { get; set; }
    }

    [PublicAPI,
        Route("/Shopping/CurrentCart/ShippingRateQuotes/Apply", "POST",
            Summary = "Use to get shipping rates for the current cart.")]
    public partial class ApplyCurrentCartShippingRateQuote : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The type name of the session cart to apply the selected rate quote to.")]
        public string? TypeName { get; set; }

        [ApiMember(Name = nameof(RateQuoteID), DataType = "int?", ParameterType = "body", IsRequired = false,
            Description = "The Key of the rate quote to select. Send null to un-assign the current rate quote.")]
        public int? RateQuoteID { get; set; }

        [ApiMember(Name = nameof(RequestedShipDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false,
            Description = "The requested ship by date from the Customer.")]
        public DateTime? RequestedShipDate { get; set; }
    }

    [PublicAPI,
        Route("/Shopping/CurrentCart/ShippingRateQuotes/Clear", "POST",
            Summary = "Use to get shipping rates for the current cart.")]
    public partial class ClearCurrentCartShippingRateQuote : IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(TypeName), DataType = "string", ParameterType = "body", IsRequired = true,
            Description = "The type name of the session cart to clear the selected rate quote from.")]
        public string? TypeName { get; set; }
    }

    /// <summary>An admin clear cart shipping rate quote for user.</summary>
    /// <seealso cref="IReturn{CEFActionResponse}"/>
    [PublicAPI,
        Authenticate,
        Route("/Shopping/AdminForUser/ShippingRateQuotes/Clear", "POST",
            Summary = "Use to clear the selected shipping rate for the specified cart.")]
    public partial class AdminClearCartShippingRateQuoteForUser : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier of the cart.</summary>
        /// <value>The identifier of the cart.</value>
        [ApiMember(Name = nameof(CartID), DataType = "int", ParameterType = "body", IsRequired = true)]
        public int CartID { get; set; }
    }

    public partial class ShipmentService
    {
        private static IContactModel ShipmentOrigin => new ContactModel
        {
            Address = new()
            {
                Company = CEFConfigDictionary.CompanyName,
                Street1 = CEFConfigDictionary.ShippingOriginAddressStreet1,
                Street2 = CEFConfigDictionary.ShippingOriginAddressStreet2,
                Street3 = CEFConfigDictionary.ShippingOriginAddressStreet3,
                City = CEFConfigDictionary.ShippingOriginAddressCity,
                PostalCode = CEFConfigDictionary.ShippingOriginAddressPostalCode,
                RegionCode = CEFConfigDictionary.ShippingOriginAddressRegionCode,
                CountryCode = CEFConfigDictionary.ShippingOriginAddressCountryCode,
            },
        };

        public async Task<object?> Post(IsShippingRequiredForCart request)
        {
            using var context = RegistryLoaderWrapper.GetContext(null);
            var typeName = await context.Carts
                .AsNoTracking()
                .FilterByID(Contract.RequiresValidID(request.ID))
                .Select(x => x.Type!.Name!)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (cartID, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            var shippingProviders = RegistryLoaderWrapper.GetShippingProviders(null);
            return await Workflows.Carts.IsShippingRequiredAsync(
                    lookupKey: await GenCartByIDLookupKeyAsync(cartID: cartID!.Value).ConfigureAwait(false),
                    shippingProviders: shippingProviders,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(IsShippingRequired request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            var shippingProviders = RegistryLoaderWrapper.GetShippingProviders(null);
            return await Workflows.Carts.IsShippingRequiredAsync(
                    lookupKey: lookupKey,
                    shippingProviders: shippingProviders,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(GetCartShippingRateQuotes request)
        {
            var result = await Workflows.Carts.GetRateQuotesAsync(
                    lookupKey: await GenCartByIDLookupKeyAsync(cartID: request.ID).ConfigureAwait(false),
                    origin: ShipmentOrigin,
                    expedited: request.Expedited,
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (!result.ActionSucceeded)
            {
                return result;
            }
            return result.Result!.Cast<RateQuoteModel>()
                .ToList()
                .WrapInPassingCEFARIfNotNullOrEmpty<List<RateQuoteModel>, RateQuoteModel>(result.Messages.ToArray());
        }

        public async Task<object?> Post(GetCurrentCartShippingRateQuotes request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            return await Workflows.Carts.GetRateQuotesAsync(
                    lookupKey: lookupKey,
                    origin: ShipmentOrigin,
                    expedited: request.Expedited,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ApplyCartShippingRateQuote request)
        {
            return await Workflows.Carts.ApplyRateQuoteToCartAsync(
                    lookupKey: await GenCartByIDLookupKeyAsync(cartID: request.ID).ConfigureAwait(false),
                    rateQuoteID: request.RateQuoteID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ApplyCurrentCartShippingRateQuote request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            return await Workflows.Carts.ApplyRateQuoteToCartAsync(
                    lookupKey: lookupKey,
                    rateQuoteID: request.RateQuoteID,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(ClearCurrentCartShippingRateQuote request)
        {
            var typeName = request.TypeName ?? "Cart";
            await PickupCorrectCartCookieAsync(typeName).ConfigureAwait(false);
            var lookupKey = await GenSessionLookupKeyAsync(typeName).ConfigureAwait(false);
            var (_, updatedSessionID) = await Workflows.Carts.SessionGetAsIDAsync(
                    lookupKey: lookupKey,
                    pricingFactoryContext: await GetPricingFactoryContextAsync().ConfigureAwait(false),
                    contextProfileName: null)
                .ConfigureAwait(false);
            if (updatedSessionID != null
                && updatedSessionID != default(Guid)
                && await GetSessionCorrectCartGuidAsync(typeName).ConfigureAwait(false) != updatedSessionID)
            {
                await OverrideSessionCorrectCartGuidAsync(typeName, updatedSessionID.Value).ConfigureAwait(false);
            }
            return await Workflows.Carts.ClearRateQuoteFromCartAsync(
                    lookupKey: lookupKey,
                    contextProfileName: null)
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(AdminClearCartShippingRateQuoteForUser request)
        {
            return await Workflows.Carts.ClearRateQuoteFromCartAsync(
                    lookupKey: new CartByIDLookupKey(request.CartID),
                    contextProfileName: ServiceContextProfileName)
                .ConfigureAwait(false);
        }
    }
}
