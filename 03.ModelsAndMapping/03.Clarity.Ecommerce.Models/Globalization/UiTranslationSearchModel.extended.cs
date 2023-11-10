// <copyright file="UiTranslationSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the translation search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the translation search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IUiTranslationSearchModel"/>
    public partial class UiTranslationSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(LanguageID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Language ID for search")]
        public int? LanguageID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(KeyStartsWith), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The key must start with this string [Case-Insensitive]")]
        public string? KeyStartsWith { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(KeyContains), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The key must contain this string [Case-Insensitive]")]
        public string? KeyContains { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(KeyEndsWith), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "The key must end with this string [Case-Insensitive]")]
        public string? KeyEndsWith { get; set; }
    }
}
