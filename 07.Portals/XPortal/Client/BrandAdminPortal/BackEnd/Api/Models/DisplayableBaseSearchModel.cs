// <copyright file="DisplayableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.MVC.Api.Models
{
    /// <summary>A data Model for the displayable base search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    public abstract partial class DisplayableBaseSearchModel : NameableBaseSearchModel
    {
        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        public string? DisplayName { get; set; }

        /// <summary>Gets or sets the display name strict.</summary>
        /// <value>The display name strict.</value>
        public bool? DisplayNameStrict { get; set; }

        /// <summary>Gets or sets the display name include null.</summary>
        /// <value>The display name include null.</value>
        public bool? DisplayNameIncludeNull { get; set; }

        /// <summary>Gets or sets the translation key.</summary>
        /// <value>The translation key.</value>
        public string? TranslationKey { get; set; }

        /// <summary>Gets or sets the translation key strict.</summary>
        /// <value>The translation key strict.</value>
        public bool? TranslationKeyStrict { get; set; }

        /// <summary>Gets or sets the translation key include null.</summary>
        /// <value>The translation key include null.</value>
        public bool? TranslationKeyIncludeNull { get; set; }
    }
}
