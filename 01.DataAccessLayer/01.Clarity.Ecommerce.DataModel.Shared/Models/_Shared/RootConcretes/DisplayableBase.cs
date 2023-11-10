// <copyright file="DisplayableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base class</summary>
#nullable enable
namespace Clarity.Ecommerce.DataModel
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Interfaces.DataModel;

    /// <summary>A displayable base.</summary>
    /// <seealso cref="NameableBase"/>
    /// <seealso cref="IDisplayableBase"/>
    public abstract class DisplayableBase : NameableBase, IDisplayableBase
    {
        /// <inheritdoc/>
        [StringLength(256), StringIsUnicode(false), Index, DefaultValue(null)]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        [Index, DefaultValue(null)]
        public int? SortOrder { get; set; }

        /// <inheritdoc/>
        [StringLength(128), StringIsUnicode(false), DefaultValue(null)]
        public string? TranslationKey { get; set; }
    }
}
