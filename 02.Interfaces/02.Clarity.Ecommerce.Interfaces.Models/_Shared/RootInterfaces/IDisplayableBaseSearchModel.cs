// <copyright file="IDisplayableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDisplayableBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for displayable base search model.</summary>
    public interface IDisplayableBaseSearchModel : INameableBaseSearchModel
    {
        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the display name strict.</summary>
        /// <value>The display name strict.</value>
        bool? DisplayNameStrict { get; set; }

        /// <summary>Gets or sets the display name include null.</summary>
        /// <value>The display name include null.</value>
        bool? DisplayNameIncludeNull { get; set; }

        /// <summary>Gets or sets the translation key.</summary>
        /// <value>The translation key.</value>
        string? TranslationKey { get; set; }

        /// <summary>Gets or sets the translation key strict.</summary>
        /// <value>The translation key strict.</value>
        bool? TranslationKeyStrict { get; set; }

        /// <summary>Gets or sets the translation key include null.</summary>
        /// <value>The translation key include null.</value>
        bool? TranslationKeyIncludeNull { get; set; }
    }
}
