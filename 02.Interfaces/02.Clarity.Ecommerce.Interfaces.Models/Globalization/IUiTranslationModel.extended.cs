// <copyright file="IUiTranslationModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IUiTranslationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for user interface translation model.</summary>
    public partial interface IUiTranslationModel
    {
        /// <summary>Gets or sets the locale.</summary>
        /// <value>The locale.</value>
        string? Locale { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        string? Value { get; set; }

        #region Related Objects
        /// <summary>Gets or sets the identifier of the key.</summary>
        /// <value>The identifier of the key.</value>
        int UiKeyID { get; set; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The user interface key.</value>
        string? UiKeyKey { get; set; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The user interface key.</value>
        IUiKeyModel? UiKey { get; set; }
        #endregion
    }
}
