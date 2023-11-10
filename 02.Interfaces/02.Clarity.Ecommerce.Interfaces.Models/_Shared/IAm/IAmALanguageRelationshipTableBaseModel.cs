// <copyright file="IAmALanguageRelationshipTableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmALanguageRelationshipTableBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a language relationship table base model.</summary>
    /// <seealso cref="IAmARelationshipTableBaseModel{ILanguageModel}"/>
    public interface IAmALanguageRelationshipTableBaseModel
        : IAmARelationshipTableBaseModel<ILanguageModel>
    {
        #region Related Objects
        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        int LanguageID { get; set; }

        /// <summary>Gets or sets the language key.</summary>
        /// <value>The language key.</value>
        string? LanguageKey { get; set; }

        /// <summary>Gets or sets the name of the language.</summary>
        /// <value>The name of the language.</value>
        string? LanguageName { get; set; }

        /// <summary>Gets or sets the language.</summary>
        /// <value>The language.</value>
        ILanguageModel? Language { get; set; }
        #endregion
    }
}
