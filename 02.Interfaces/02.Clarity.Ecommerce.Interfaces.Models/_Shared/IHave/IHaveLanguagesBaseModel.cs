// <copyright file="IHaveLanguagesBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveLanguagesBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have languages base model.</summary>
    /// <typeparam name="TLanguageModel">Type of the language model.</typeparam>
    /// <seealso cref="IBaseModel"/>
    public interface IHaveLanguagesBaseModel<TLanguageModel> : IBaseModel
        where TLanguageModel : IBaseModel
    {
        /// <summary>Gets or sets the languages.</summary>
        /// <value>The languages.</value>
        List<TLanguageModel>? Languages { get; set; }
    }
}
