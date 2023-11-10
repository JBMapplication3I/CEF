// <copyright file="IDiscountManager.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountManager interface</summary>
namespace Clarity.Ecommerce.Providers.Discounts
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Interfaces.Providers.Taxes;
    using Models;

    /// <summary>Interface for discount manager.</summary>
    public interface IDiscountManager
    {
        /// <summary>Adds a discount by code.</summary>
        /// <param name="code">                 The code.</param>
        /// <param name="cartID">               Identifier for the cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> AddDiscountByCodeAsync(
            string code,
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Adds the discounts by their IDs to the cart.</summary>
        /// <param name="discountIDs">          The discount identifiers.</param>
        /// <param name="cartID">               Identifier for the cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> AddDiscountsByIDsAsync(
            List<int> discountIDs,
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Verify current discounts.</summary>
        /// <param name="cartID">               Name of the cart type.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> VerifyCurrentDiscountsAsync(
            int cartID,
            IPricingFactoryContextModel pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Verify current discounts.</summary>
        /// <param name="cart">                 The cart.</param>
        /// <param name="pricingFactoryContext">Context for the pricing factory.</param>
        /// <param name="taxesProvider">        The taxes provider.</param>
        /// <param name="contextProfileName">   Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> VerifyCurrentDiscountsAsync(
            ICartModel cart,
            IPricingFactoryContextModel? pricingFactoryContext,
            ITaxesProviderBase? taxesProvider,
            string? contextProfileName);

        /// <summary>Removes the discount by applied cart discount identifier.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="appliedID">         Identifier for the discount application.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> RemoveDiscountByAppliedCartDiscountIDAsync(
            ICartModel cart,
            int appliedID,
            string? contextProfileName);

        /// <summary>Removes the discount by applied cart item discount identifier.</summary>
        /// <param name="cart">              The cart.</param>
        /// <param name="appliedID">         Identifier for the discount application.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task{CEFActionResponse}.</returns>
        Task<CEFActionResponse> RemoveDiscountByAppliedCartItemDiscountIDAsync(
            ICartModel cart,
            int appliedID,
            string? contextProfileName);
    }
}
