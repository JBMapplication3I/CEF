// <copyright file="ProductSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A data Model for the product search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="IProductSearchModel"/>
    public partial class ProductSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasAnyAncestorCategoryID), DataType = "int?", ParameterType = "query", IsRequired = false)]
        public int? HasAnyAncestorCategoryID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ParentCategories), DataType = "int[]", ParameterType = "query", IsRequired = false)]
        public int[]? ParentCategories { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(SearchTerm), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? SearchTerm { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Price), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? Price { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Weight), DataType = "decimal?", ParameterType = "query", IsRequired = false)]
        public decimal? Weight { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryTypeName), DataType = "string", ParameterType = "query", IsRequired = false)]
        public string? CategoryTypeName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(Keywords), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "List of words to search against in various product info's")]
        public string? Keywords { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ComparisonIDs), DataType = "int[]", ParameterType = "query", IsRequired = false)]
        public int[]? ComparisonIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductIDs), DataType = "int[]", ParameterType = "query", IsRequired = false)]
        public int[]? ProductIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(BrandCategoryIDs), DataType = "int[]", ParameterType = "query", IsRequired = false)]
        public int[]? BrandCategoryIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryJsonAttributes), DataType = "Dictionary<string, string[]>", ParameterType = "query", IsRequired = false,
            Description = "Filter by a dictionary of attribute values (JSON version) of the categories this product belongs to")]
        public Dictionary<string, string?[]?>? CategoryJsonAttributes { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsVendorAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, will only return values as part of the vendor's admin's filter. This value is set by the server and cannot be overriden.")]
        public bool? IsVendorAdmin { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(VendorAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "When set, will only return values as part of the vendor's admin's filter. This value is set by the server and cannot be overriden.")]
        public int? VendorAdminID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(IsManufacturerAdmin), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "When true, will only return values as part of the manufacturer's admin's filter. This value is set by the server and cannot be overriden.")]
        public bool? IsManufacturerAdmin { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ManufacturerAdminID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "When set, will only return values as part of the manufacturer's admin's filter. This value is set by the server and cannot be overriden.")]
        public int? ManufacturerAdminID { get; set; }

        /// <inheritdoc/>
        [IgnoreDataMember] // Excluded from the API output
        public IPricingFactoryContextModel? PricingFactoryContext { get; set; }
    }
}
