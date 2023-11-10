// <copyright file="IHaveCurrenciesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveCurrenciesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have currencies base.</summary>
    /// <typeparam name="TCurrency">Type of the currency.</typeparam>
    /// <seealso cref="IBase"/>
    public interface IHaveCurrenciesBase<TCurrency> : IBase
        where TCurrency : IBase
    {
        /// <summary>Gets or sets the currencies.</summary>
        /// <value>The currencies.</value>
        ICollection<TCurrency>? Currencies { get; set; }
    }
}
