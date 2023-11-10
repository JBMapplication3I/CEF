// <copyright file="ShippingFactoryProductModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the shipping factory product model class</summary>
namespace Clarity.Ecommerce.Providers.Shipping
{
    using System.Collections.Generic;
    using Interfaces.Models;
    using Interfaces.Providers.Shipping;

    /// <summary>A data Model for the shipping factory product.</summary>
    /// <seealso cref="IShippingFactoryProductModel"/>
    public class ShippingFactoryProductModel : IShippingFactoryProductModel
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
        public List<int>? ManufacturerIDs { get; set; }

        /// <inheritdoc/>
        public List<int>? StoreIDs { get; set; }

        /// <inheritdoc/>
        public List<int>? BrandIDs { get; set; }

        /// <inheritdoc/>
        public List<int>? VendorIDs { get; set; }

        /// <inheritdoc/>
        public List<int>? CategoryIDs { get; set; }

        /// <inheritdoc/>
        public List<string>? CategoryKeys { get; set; }

        /// <inheritdoc/>
        public IEnumerable<IProductShipCarrierMethodModel>? ProductShipCarrierMethods { get; set; }
    }
}
