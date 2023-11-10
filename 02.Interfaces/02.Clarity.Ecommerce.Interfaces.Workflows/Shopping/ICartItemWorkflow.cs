// <copyright file="ICartItemWorkflow.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICartItemWorkflow interface</summary>
// ReSharper disable InvalidXmlDocComment
namespace Clarity.Ecommerce.Interfaces.Workflow
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;
    using Models;
    using Providers.Pricing;
    using Providers.Taxes;

    public partial interface ICartItemWorkflow
    {
        /// <summary>Check if product is purchasable by current account by account restrictions.</summary>
        /// <param name="currentAccountTypeName">The current account type name.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> CheckIfProductIsPurchasableByCurrentAccountByAccountRestrictionsAsync(
            string? currentAccountTypeName,
            int productID,
            string? contextProfileName);

        /// <summary>Check if product quantity meets minimum and maximum purchase quantities for account identifier or
        /// user identifier.</summary>
        /// <param name="lookupKey">          The lookup key.</param>
        /// <param name="entity">             The entity.</param>
        /// <param name="quantity">           The quantity.</param>
        /// <param name="quantityBackOrdered">The quantity back ordered.</param>
        /// <param name="quantityPreSold">    The quantity pre sold.</param>
        /// <param name="productID">          Identifier for the product.</param>
        /// <param name="isUnlimitedCache">   The is unlimited cache.</param>
        /// <param name="allowPreSaleCache">  The allow pre sale cache.</param>
        /// <param name="allowBackOrderCache">The allow back order cache.</param>
        /// <param name="flatStockCache">     The flat stock cache.</param>
        /// <param name="isForQuote">         True if this ICartItemWorkflow is for quote.</param>
        /// <param name="contextProfileName"> Name of the context profile.</param>
        /// <returns>An ICartValidatorItemModificationResult.</returns>
        Task<ICartValidatorItemModificationResult?> CheckIfProductQuantityMeetsMinimumAndMaximumPurchaseQuantitiesForAccountIDOrUserIDAsync(
            CartByIDLookupKey lookupKey,
            ICartItem entity,
            decimal quantity,
            decimal quantityBackOrdered,
            decimal quantityPreSold,
            int productID,
            IDictionary<int, bool> isUnlimitedCache,
            IDictionary<int, bool> allowPreSaleCache,
            IDictionary<int, bool> allowBackOrderCache,
            IDictionary<int, decimal> flatStockCache,
            bool isForQuote,
            string? contextProfileName);

        /// <summary>Adds a cart item.</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="quantity">              The quantity.</param>
        /// <param name="targets">               The targets.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="forceUniqueLineItemKey">The force unique line item key.</param>
        /// <param name="serializableAttributes">The serializable attributes.</param>
        /// <param name="pricingFactoryContext"> Context for the pricing factory.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{int?}}.</returns>
        Task<CEFActionResponse<int?>> AddCartItemAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            decimal quantity,
            List<ISalesItemTargetBaseModel>? targets,
            int productID,
            string? forceUniqueLineItemKey,
            SerializableAttributesDictionary serializableAttributes,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Adds a buffer SKU cart item.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="bufferSkuSeoUrl">      URL of the buffer SKU seo.</param>
        /// <param name="amountToFill">         The amount to fill.</param>
        /// <param name="pricingFactory">       The pricing factory.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> AddBufferSkuCartItemAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            string bufferSkuSeoUrl,
            string amountToFill,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Searches for the first match.</summary>
        /// <param name="search">            The search.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A List{ISalesItemBaseModel{IAppliedCartItemDiscountModel}} in a tuple with totalPages and totalCount.</returns>
        Task<(List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> results, int totalPages, int totalCount)> SearchAsync(
            ISalesItemBaseSearchModel search,
            string? contextProfileName);

        /// <summary>Upsert multiple sales items.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="models">               The models.</param>
        /// <param name="pricingFactory">       The pricing factory.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{List{int?}} (list of cart item IDs).</returns>
        Task<CEFActionResponse<List<int?>>> UpsertMultipleAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Upserts a cart item.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="model">                The model.</param>
        /// <param name="pricingFactory">       The pricing factory.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int?}.</returns>
        Task<CEFActionResponse<int?>> UpsertAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            IPricingFactory pricingFactory,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Admin update multiple cart items.</summary>
        /// <param name="lookupKey">         The lookup key.</param>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{List{int?}}.</returns>
        Task<CEFActionResponse<List<int?>>> AdminUpdateMultipleAsync(
            CartByIDLookupKey lookupKey,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            string? contextProfileName);

        /// <summary>Updates multiple cart items.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="models">               The models.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{List{int?}}.</returns>
        Task<CEFActionResponse<List<int?>>> UpdateMultipleAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Updates the multiple.</summary>
        /// <param name="models">            The models.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A list of.</returns>
        Task<CEFActionResponse<List<int>>> UpdateMultipleAsync(
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> models,
            string? contextProfileName);

        /// <summary>Updates this ICartItemWorkflow.</summary>
        /// <param name="lookupKey">            The lookup key.</param>
        /// <param name="model">                The model.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{int?}.</returns>
        Task<CEFActionResponse<int?>> UpdateAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            ISalesItemBaseModel<IAppliedCartItemDiscountModel> model,
            IPricingFactoryContextModel pricingFactoryContext,
            string? contextProfileName);

        /// <summary>Deactivates the cart item by product identifier, cart type and session identifier.</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="productID">             Identifier for the product.</param>
        /// <param name="forceUniqueLineItemKey">The force unique line item key.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A CEFActionResponse.</returns>
        Task<CEFActionResponse> DeactivateAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            int productID,
            string? forceUniqueLineItemKey,
            string? contextProfileName);

        /// <summary>Updates the quantity.</summary>
        /// <param name="cartItemID">           Identifier for the cart item.</param>
        /// <param name="quantity">             The quantity.</param>
        /// <param name="quantityBackOrdered">  The quantity back ordered.</param>
        /// <param name="quantityPreSold">      The quantity pre sold.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">          The tax provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A CEFActionResponse{decimal}.</returns>
        Task<CEFActionResponse<decimal>> UpdateQuantityAsync(
            int cartItemID,
            decimal quantity,
            decimal quantityBackOrdered,
            decimal quantityPreSold,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Updates the targets.</summary>
        /// <param name="cartItemID">        Identifier for the cart item.</param>
        /// <param name="targets">           The targets.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A CEFActionResponse{ISalesItemBaseModel{IAppliedCartItemDiscountModel}}.</returns>
        Task<CEFActionResponse<ISalesItemBaseModel<IAppliedCartItemDiscountModel>>> UpdateTargetsAsync(
            int cartItemID,
            List<ISalesItemTargetBaseModel> targets,
            string? contextProfileName);

        /// <summary>Adds an item by product key.</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="productKey">            The request product key.</param>
        /// <param name="forceUniqueLineItemKey">The request force unique line item key.</param>
        /// <param name="quantity">              The request quantity.</param>
        /// <param name="pricingFactoryContext"> Context for the pricing factory.</param>
        /// <param name="targets">               The targets.</param>
        /// <param name="attributes">            The attributes.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse{int?}}.</returns>
        Task<CEFActionResponse<int?>> AddByKeyAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            string productKey,
            string? forceUniqueLineItemKey,
            int quantity,
            IPricingFactoryContextModel pricingFactoryContext,
            List<ISalesItemTargetBaseModel>? targets,
            SerializableAttributesDictionary attributes,
            string? contextProfileName);

        /// <summary>Adds a by keys.</summary>
        /// <param name="lookupKey">             The lookup key.</param>
        /// <param name="productKeys">           The product keys.</param>
        /// <param name="forceUniqueLineItemKey">The force unique line item key.</param>
        /// <param name="quantity">              The quantity.</param>
        /// <param name="pricingFactoryContext"> Context for the pricing factory.</param>
        /// <param name="targets">               The targets.</param>
        /// <param name="attributes">            The attributes.</param>
        /// <param name="contextProfileName">    Name of the context profile.</param>
        /// <returns>A list of.</returns>
        Task<CEFActionResponse<List<int?>>> AddByKeysAsync(
            SessionCartBySessionAndTypeLookupKey lookupKey,
            List<string> productKeys,
            string? forceUniqueLineItemKey,
            int quantity,
            IPricingFactoryContextModel pricingFactoryContext,
            List<ISalesItemTargetBaseModel>? targets,
            SerializableAttributesDictionary attributes,
            string? contextProfileName);
    }
}
