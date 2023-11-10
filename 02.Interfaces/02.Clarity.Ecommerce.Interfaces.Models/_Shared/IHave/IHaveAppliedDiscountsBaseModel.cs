// <copyright file="IHaveAppliedDiscountsBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAppliedDiscountsBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have applied discounts base model.</summary>
    /// <typeparam name="TIAppliedModel">Type of the applied discount model's interface.</typeparam>
    public interface IHaveAppliedDiscountsBaseModel<TIAppliedModel>
        : IBaseModel
        where TIAppliedModel : IBaseModel, IAppliedDiscountBaseModel
    {
        #region Associated Objects
        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        List<TIAppliedModel>? Discounts { get; set; }
        #endregion
    }
}
