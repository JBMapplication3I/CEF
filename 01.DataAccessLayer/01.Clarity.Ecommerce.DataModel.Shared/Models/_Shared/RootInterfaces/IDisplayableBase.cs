// <copyright file="IDisplayableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDisplayableBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for displayable base.</summary>
    /// <seealso cref="INameableBase"/>
    public interface IDisplayableBase : INameableBase
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
