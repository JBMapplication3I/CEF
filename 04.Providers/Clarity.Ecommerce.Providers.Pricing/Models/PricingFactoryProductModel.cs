// <copyright file="PricingFactoryProductModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the pricing factory product model class</summary>
namespace Clarity.Ecommerce.Providers.Pricing
{
    using System.Collections.Generic;
    using Interfaces.Models;
    using Interfaces.Providers.Pricing;

    /// <summary>A data Model for the pricing factory product.</summary>
    /// <seealso cref="IPricingFactoryProductModel"/>
    public class PricingFactoryProductModel : IPricingFactoryProductModel
    {
        /// <inheritdoc/>
        public int ProductID { get; set; }

        /// <inheritdoc/>
        public int? ProductTypeID { get; set; }

        /// <inheritdoc/>
        public string? ProductTypeKey { get; set; }

        /// <inheritdoc/>
        public string? ProductKey { get; set; }

        /// <inheritdoc/>
        public string? ProductUnitOfMeasure { get; set; }

        /// <inheritdoc/>
        public decimal? KitBaseQuantityPriceMultiplier { get; set; }

        /// <inheritdoc/>
        public List<int> ManufacturerIDs { get; set; } = new();

        /// <inheritdoc/>
        public List<int> StoreIDs { get; set; } = new();

        /// <inheritdoc/>
        public List<int> FranchiseIDs { get; set; } = new();

        /// <inheritdoc/>
        public List<int> BrandIDs { get; set; } = new();

        /// <inheritdoc/>
        public List<int> VendorIDs { get; set; } = new();

        /// <inheritdoc/>
        public List<int> CategoryIDs { get; set; } = new();

        /// <inheritdoc/>
        public List<string> CategoryKeys { get; set; } = new();

        /// <inheritdoc/>
        public decimal? PriceBase { get; set; }

        /// <inheritdoc/>
        public decimal? PriceSale { get; set; }

        /// <inheritdoc/>
        public SerializableAttributesDictionary? SerializableAttributes { get; set; }

        /// <inheritdoc/>
        public SerializableAttributesDictionary? CartItemSerializableAttributes { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IProductPricePointModel>? ProductPricePoints { get; set; }
    }
}
