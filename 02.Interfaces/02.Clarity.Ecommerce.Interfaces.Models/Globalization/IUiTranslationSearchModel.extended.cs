// <copyright file="IUiTranslationSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUiTranslationSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for user interface translation search model.</summary>
    public partial interface IUiTranslationSearchModel
    {
        /// <summary>Gets or sets the identifier of the language.</summary>
        /// <value>The identifier of the language.</value>
        int? LanguageID { get; set; }

        /// <summary>Gets or sets the key starts with.</summary>
        /// <value>The key starts with.</value>
        string? KeyStartsWith { get; set; }

        /// <summary>Gets or sets the key ends with.</summary>
        /// <value>The key ends with.</value>
        string? KeyEndsWith { get; set; }

        /// <summary>Gets or sets the key contains.</summary>
        /// <value>The key contains.</value>
        string? KeyContains { get; set; }
    }
}
