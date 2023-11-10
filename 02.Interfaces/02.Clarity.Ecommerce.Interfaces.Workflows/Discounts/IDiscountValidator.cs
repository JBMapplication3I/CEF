// <copyright file="IDiscountValidator.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDiscountValidator interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using DataModel;
    using Ecommerce.Models;

    /// <summary>Interface for discount validator.</summary>
    public interface IDiscountValidator
    {
        /// <summary>Validates the discount.</summary>
        /// <param name="isAdd">           True if this is trying to add the discount or just make sure it's still
        ///                                valid.</param>
        /// <param name="userID">          Identifier for the user.</param>
        /// <param name="cartID">          Identifier for the cart.</param>
        /// <param name="subtotal">        The subtotal.</param>
        /// <param name="discount">        The discount.</param>
        /// <param name="currentDiscounts">The current discounts.</param>
        /// <param name="cartItems">       The cart items.</param>
        /// <param name="rateQuotes">      The rate quotes.</param>
        /// <param name="context">         The context.</param>
        /// <returns>A Task{CEFActionResponse{List{IAppliedDiscountBaseModel}}}.</returns>
        Task<CEFActionResponse<List<IAppliedDiscountBaseModel>>> ValidateDiscountAsync(
            bool isAdd,
            int userID,
            int cartID,
            decimal? subtotal,
            IDiscount discount,
            List<IAppliedDiscountBaseModel> currentDiscounts,
            List<ISalesItemBaseModel<IAppliedCartItemDiscountModel>> cartItems,
            List<IRateQuoteModel> rateQuotes,
            IClarityEcommerceEntities context);
    }
}
