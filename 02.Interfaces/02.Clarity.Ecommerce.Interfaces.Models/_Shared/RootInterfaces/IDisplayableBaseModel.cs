// <copyright file="IDisplayableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDisplayableBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for displayable base model.</summary>
    /// <seealso cref="INameableBaseModel"/>
    public interface IDisplayableBaseModel : INameableBaseModel
    {
        /// <summary>Gets or sets the sort order.</summary>
        /// <value>The sort order.</value>
        int? SortOrder { get; set; }

        /// <summary>Gets or sets the name of the display.</summary>
        /// <value>The name of the display.</value>
        string? DisplayName { get; set; }

        /// <summary>Gets or sets the translation key.</summary>
        /// <value>The translation key.</value>
        string? TranslationKey { get; set; }
    }
}
