// <copyright file="CategorySearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the category search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using Newtonsoft.Json;
    using ServiceStack;

    /// <summary>A data Model for the category search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ICategorySearchModel"/>
    public partial class CategorySearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(ExcludeProductCategories), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "Exclude Product Categories")]
        public bool? ExcludeProductCategories { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ParentSEOURL), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Parent SEO URL")]
        public string? ParentSEOURL { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ChildJsonAttributes), DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false,
            Description = "Filter by a dictionary of attribute values (JSON version) of the children (up to 2 levels below current)")]
        public Dictionary<string, string?[]?>? ChildJsonAttributes { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasProductsUnderAnotherCategoryID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Advanced filtering")]
        public int? HasProductsUnderAnotherCategoryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasProductsUnderAnotherCategoryKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Advanced filtering")]
        public string? HasProductsUnderAnotherCategoryKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasProductsUnderAnotherCategoryName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Advanced filtering")]
        public string? HasProductsUnderAnotherCategoryName { get; set; }

        /// <inheritdoc/>
        [JsonIgnore]
        public string?[]? CurrentRoles { get; set; }
    }
}
