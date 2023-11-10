// <copyright file="IHaveAppliedDiscountsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAppliedDiscountsBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have discounts base.</summary>
    /// <typeparam name="TMaster"> Type of the master.</typeparam>
    /// <typeparam name="TApplied">Type of the applied discount.</typeparam>
    public interface IHaveAppliedDiscountsBase<TMaster, TApplied>
        : IBase
        where TMaster : IHaveAppliedDiscountsBase<TMaster, TApplied>
        where TApplied : IAppliedDiscountBase<TMaster, TApplied>
    {
        /// <summary>Gets or sets the discounts.</summary>
        /// <value>The discounts.</value>
        ICollection<TApplied>? Discounts { get; set; }
    }
}
