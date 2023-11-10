// <copyright file="IShippingFactoryProductModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IShippingFactoryProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Shipping
{
    using System.Collections.Generic;
    using Models;

    /// <summary>Interface for pricing factory product model.</summary>
    public interface IShippingFactoryProductModel
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int ProductID { get; set; }

        /// <summary>Gets or sets the identifier of the product type.</summary>
        /// <value>The identifier of the product type.</value>
        int? ProductTypeID { get; set; }

        /// <summary>Gets or sets the product type key.</summary>
        /// <value>The product type key.</value>
        string? ProductTypeKey { get; set; }

        /// <summary>Gets or sets the product key.</summary>
        /// <value>The product key.</value>
        string? ProductKey { get; set; }

        /// <summary>Gets or sets the product unit of measure.</summary>
        /// <value>The product unit of measure.</value>
        string? ProductUnitOfMeasure { get; set; }

        /// <summary>Gets or sets a list of identifiers of the vendors.</summary>
        /// <value>A list of identifiers of the vendors.</value>
        List<int>? VendorIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the manufacturers.</summary>
        /// <value>A list of identifiers of the manufacturers.</value>
        List<int>? ManufacturerIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the stores.</summary>
        /// <value>A list of identifiers of the stores.</value>
        List<int>? StoreIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the brands.</summary>
        /// <value>A list of identifiers of the brands.</value>
        List<int>? BrandIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the categories.</summary>
        /// <value>A list of identifiers of the categories.</value>
        List<int>? CategoryIDs { get; set; }

        /// <summary>Gets or sets a list of keys of the categories.</summary>
        /// <value>A list of keys of the categories.</value>
        List<string>? CategoryKeys { get; set; }

        /// <summary>Gets or sets the product ship carrier methods.</summary>
        /// <value>The product ship carrier methods.</value>
        IEnumerable<IProductShipCarrierMethodModel>? ProductShipCarrierMethods { get; set; }
    }
}
