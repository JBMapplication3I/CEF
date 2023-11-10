// <copyright file="ICartWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartWorkflow interface</summary>
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;
    using Providers.Shipping;
    using Providers.Taxes;

    /// <summary>Interface for cart workflow.</summary>
    public partial interface ICartWorkflow
    {
        #region Session Carts
        /// <summary>Session get.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ICartModel} and an updated Session Guid if necessary.</returns>
        Task<(CEFActionResponse<ICartModel?> cartResponse, Guid? updatedSessionID)> SessionGetAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Session get.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ICartModel} and an updated Session Guid if necessary.</returns>
        Task<(CEFActionResponse<ICartModel?> cartResponse, Guid? updatedSessionID)> SessionGetAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Session get as identifier and type key.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The identifier of the cart and an updated Session Guid if necessary.</returns>
        Task<(int? cartID, Guid? updatedSessionID)> SessionGetAsIDAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Check exists by type name and session identifier.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>The identifier of the cart and an updated Session Guid if necessary.</returns>
        Task<(int? cartID, Guid? updatedSessionID)> CheckExistsByTypeNameAndSessionIDAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Applies the rate quote to cart.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="rateQuoteID">       Identifier for the rate quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ApplyRateQuoteToCartAsync(
            CartByIDLookupKey lookupKey,
            int? rateQuoteID,
            string? contextProfileName);

        /// <summary>Applies the rate quote to cart.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="rateQuoteID">       Identifier for the rate quote.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ApplyRateQuoteToCartAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            int? rateQuoteID,
            string? contextProfileName);

        /// <summary>Clears the rate quote from cart.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ClearRateQuoteFromCartAsync(
            CartByIDLookupKey lookupKey,
            string? contextProfileName);

        /// <summary>Clears the rate quote from cart.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> ClearRateQuoteFromCartAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            string? contextProfileName);

        /// <summary>Sets quantity back ordered for item.</summary>
        /// <param name="salesItemID">       Identifier for the sales item.</param>
        /// <param name="quantity">          The quantity.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        Task SetQuantityBackOrderedForItemAsync(
            int salesItemID,
            int quantity,
            string? contextProfileName);

        /// <summary>Adds a cart fee.</summary>
        /// <param name="currentCart">       The current cart.</param>
        /// <param name="amountToFee">       The amount to fee.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int}.</returns>
        Task<CEFActionResponse<int>> AddCartFeeAsync(
            ICartModel currentCart,
            string amountToFee,
            string? contextProfileName);

        /// <summary>Gets shipping contact.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The shipping contact.</returns>
        Task<CEFActionResponse<IContactModel>> GetShippingContactAsync(
            int id,
            string? contextProfileName);

        /// <summary>Sets shipping contact.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="newContact">        The new contact.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetShippingContactAsync(
            int id,
            IContactModel? newContact,
            string? contextProfileName);

        /// <summary>Sets same as billing flag.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="isSame">            The flag value.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetSameAsBillingAsync(
            int id,
            bool isSame,
            string? contextProfileName);

        /// <summary>Clears the shipping contact.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ClearShippingContactAsync(
            int id,
            string? contextProfileName);

        /// <summary>Sets billing contact.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="newContact">        The new contact.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetBillingContactAsync(
            int id,
            IContactModel? newContact,
            string? contextProfileName);

        /// <summary>Clears the billing contact.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ClearBillingContactAsync(
            int id,
            string? contextProfileName);

        /// <summary>Sets a store.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="storeID">           Identifier for the store.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetStoreAsync(
            CartByIDLookupKey lookupKey,
            int? storeID,
            string? contextProfileName);

        /// <summary>Sets requested ship date.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="date">              The date.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetRequestedShipDateAsync(
            CartByIDLookupKey lookupKey,
            DateTime? date,
            string? contextProfileName);

        /// <summary>Sets the contacts.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contacts">          The contacts.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetContactsAsync(
            CartByIDLookupKey lookupKey,
            List<ICartContactModel>? contacts,
            string? contextProfileName);

        /// <summary>Sets user and account.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="accountID">         Identifier for the account.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SetUserAndAccountAsync(
            CartByIDLookupKey lookupKey,
            int? userID,
            int? accountID,
            string? contextProfileName);

        /// <summary>Sets the cart totals.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="totals">            The totals.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> SetCartTotalsAsync(
            CartByIDLookupKey lookupKey,
            ICartTotals totals,
            string? contextProfileName);

        /// <summary>Gets rate quotes.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="origin">            The origin.</param>
        /// <param name="expedited">         True if expedited.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The rate quotes.</returns>
        Task<CEFActionResponse<List<IRateQuoteModel>>> GetRateQuotesAsync(
            CartByIDLookupKey lookupKey,
            IContactModel origin,
            bool expedited,
            string? contextProfileName);

        /// <summary>Gets rate quotes.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="origin">            The origin.</param>
        /// <param name="expedited">         True if expedited.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The rate quotes.</returns>
        Task<CEFActionResponse<List<IRateQuoteModel>>> GetRateQuotesAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IContactModel origin,
            bool expedited,
            string? contextProfileName);

        /// <summary>Is shipping required.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="shippingProviders"> The shipping providers.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> IsShippingRequiredAsync(
            CartByIDLookupKey lookupKey,
            List<IShippingProviderBase> shippingProviders,
            string? contextProfileName);

        /// <summary>Is shipping required.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="shippingProviders"> The shipping providers.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> IsShippingRequiredAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<IShippingProviderBase> shippingProviders,
            string? contextProfileName);

        /// <summary>Session clear.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> SessionClearAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);
        #endregion

        #region Admin Cart Manipulation
        /// <summary>Admin get cart for another user.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        Task<ICartModel?> AdminGetAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Admin get cart for another user.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        Task<ICartModel?> AdminGetAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Admin cart update for another user.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> AdminUpdateAsync(
            ICartModel model,
            string? contextProfileName);

        /// <summary>Admin cart create for another user.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int}.</returns>
        Task<CEFActionResponse<int>> AdminCreateAsync(
            ICartModel model,
            string? contextProfileName);

        /// <summary>Admin cart clear for .</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> AdminClearAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);
        #endregion

        #region Static Carts
        /// <summary>Static get.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        Task<ICartModel?> StaticGetAsync(
            StaticCartLookupKey lookupKey,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Static get.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        Task<ICartModel?> StaticGetAsync(
            CartByIDLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Static add item.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="lotID">             Identifier for the lot.</param>
        /// <param name="quantity">          The quantity (only applicable to Notify Me When In Stock static cart.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> StaticAddLotAsync(
            StaticCartLookupKey lookupKey,
            int lotID,
            decimal? quantity,
            string? contextProfileName);

        /// <summary>Static add item.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="quantity">          The quantity (only applicable to Notify Me When In Stock static cart.</param>
        /// <param name="attributes">        The attributes.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> StaticAddItemAsync(
            StaticCartLookupKey lookupKey,
            int productID,
            decimal? quantity,
            SerializableAttributesDictionary attributes,
            string? contextProfileName);

        /// <summary>Static remove.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> StaticRemoveAsync(
            CartByIDLookupKey lookupKey,
            string? contextProfileName);

        /// <summary>Static remove.</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="forceUniqueLineItemKey">The force unique line item key.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> StaticRemoveAsync(
            StaticCartLookupKey lookupKey,
            int productID,
            string? forceUniqueLineItemKey,
            string? contextProfileName);

        /// <summary>Static clear.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> StaticClearAsync(
            StaticCartLookupKey lookupKey,
            string? contextProfileName);
        #endregion

        #region Compare Carts
        /// <summary>Compare get.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>An ICartModel.</returns>
        Task<(ICartModel? cart, Guid? updatedSessionID)> CompareGetAsync(
            CompareCartBySessionLookupKey lookupKey,
            IPricingFactoryContextModel? pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Compare add item.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="attributes">        The attributes.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse<Guid?>> CompareAddItemAsync(
            CompareCartBySessionLookupKey lookupKey,
            int productID,
            SerializableAttributesDictionary? attributes,
            string? contextProfileName);

        /// <summary>Compare remove.</summary>
        /// <param name="id">                The identifier.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CompareRemoveAsync(
            int id,
            string? contextProfileName);

        /// <summary>Compare remove.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CompareRemoveAsync(
            CompareCartBySessionLookupKey lookupKey,
            int productID,
            string? contextProfileName);

        /// <summary>Compare clear.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CompareClearAsync(
            CompareCartBySessionLookupKey lookupKey,
            string? contextProfileName);
        #endregion

        #region Attributes
        /// <summary>Gets attributes.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The attributes.</returns>
        Task<CEFActionResponse<SerializableAttributesDictionary>> GetAttributesAsync(
            CartByIDLookupKey lookupKey,
            string? contextProfileName);

        /// <summary>Updates the attributes.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="attrs">             The attributes.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> UpdateAttributesAsync(
            CartByIDLookupKey lookupKey,
            SerializableAttributesDictionary attrs,
            string? contextProfileName);
        #endregion

        /// <summary>Adds a cart and save safely.</summary>
        /// <param name="context">The context.</param>
        /// <param name="cart">   The cart.</param>
        /// <param name="doAdd">  True to do add.</param>
        /// <returns>A CEFActionResponse{ICart}.</returns>
        Task<CEFActionResponse<ICart>> AddCartAndSaveSafelyAsync(
            IClarityEcommerceEntities context,
            ICart cart,
            bool doAdd);

        #region Cleanup
        /// <summary>Removes the carts that are empty described by contextProfileName.</summary>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> RemoveCartsThatAreEmptyAsync(string? contextProfileName);

        /// <summary>Removes the carts that are empty described by contextProfileName.</summary>
        /// <param name="context">The context.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> RemoveCartsThatAreEmptyAsync(IClarityEcommerceEntities context);

        /// <summary>Deactivate or deletes expired Carts based on Cart Expiration settings.</summary>
        /// <param name="expiredThreshold">  The expired threshold.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> ExpireCartsAsync(
            int expiredThreshold,
            string? contextProfileName);
        #endregion
    }
}
