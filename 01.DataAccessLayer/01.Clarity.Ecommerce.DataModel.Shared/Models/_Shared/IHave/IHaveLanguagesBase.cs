// <copyright file="IHaveLanguagesBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveLanguagesBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have languages base.</summary>
    /// <typeparam name="TLanguage">Type of the language.</typeparam>
    /// <seealso cref="IBase"/>
    public interface IHaveLanguagesBase<TLanguage> : IBase
        where TLanguage : IBase
    {
        /// <summary>Gets or sets the languages.</summary>
        /// <value>The languages.</value>
        ICollection<TLanguage>? Languages { get; set; }
    }
}
