// <copyright file="IHaveCurrenciesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveCurrenciesBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have currencies base model.</summary>
    /// <typeparam name="TCurrencyModel">Type of the currency model.</typeparam>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveCurrenciesBaseModel<TCurrencyModel> : IBaseModel
        where TCurrencyModel : IBaseModel
    {
        /// <summary>Gets or sets the currencies.</summary>
        /// <value>The currencies.</value>
        List<TCurrencyModel>? Currencies { get; set; }
    }
}
