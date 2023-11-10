// <copyright file="DisplayableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the displayable base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the displayable base search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="IDisplayableBaseSearchModel"/>
    public abstract class DisplayableBaseSearchModel : NameableBaseSearchModel, IDisplayableBaseSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(DisplayName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Display Name of the Object")]
        public string? DisplayName { get; set; }

        /// <inheritdoc/>
        public bool? DisplayNameStrict { get; set; }

        /// <inheritdoc/>
        public bool? DisplayNameIncludeNull { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(TranslationKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Translation Key of the Object")]
        public string? TranslationKey { get; set; }

        /// <inheritdoc/>
        public bool? TranslationKeyStrict { get; set; }

        /// <inheritdoc/>
        public bool? TranslationKeyIncludeNull { get; set; }
    }
}
