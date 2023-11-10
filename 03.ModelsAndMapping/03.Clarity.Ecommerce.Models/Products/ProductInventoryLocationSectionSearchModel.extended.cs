// <copyright file="ProductInventoryLocationSectionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product inventory location section search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using ServiceStack;

    /// <summary>A data Model for the product inventory location section search.</summary>
    /// <seealso cref="BaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IProductInventoryLocationSectionSearchModel"/>
    public partial class ProductInventoryLocationSectionSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(InventoryLocationID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Inventory Location ID")]
        public int? InventoryLocationID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(InventoryLocationKey), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Inventory Location Key")]
        public string? InventoryLocationKey { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(InventoryLocationName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Inventory Location Name")]
        public string? InventoryLocationName { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasBrokenQuantity), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "If the location contains broken quantities")]
        public bool? HasBrokenQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasPreSoldQuantity), DataType = "bool?", ParameterType = "query", IsRequired = false,
            Description = "If the location contains pre-sold quantities")]
        public bool? HasPreSoldQuantity { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreID), DataType = "int?", ParameterType = "query", IsRequired = false,
            Description = "Store ID (single, And'd if set)")]
        public int? StoreID { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(StoreIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "Store IDs (multiple, Internally Or'd together)")]
        public List<int>? StoreIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(ProductIDs), DataType = "List<int>", ParameterType = "query", IsRequired = false,
            Description = "Product IDs (multiple, Internally Or'd together)")]
        public List<int>? ProductIDs { get; set; }
    }
}
