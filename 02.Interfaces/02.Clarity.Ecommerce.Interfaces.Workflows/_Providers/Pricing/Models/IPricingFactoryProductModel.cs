// <copyright file="IPricingFactoryProductModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPricingFactoryProductModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Pricing
{
    using System.Collections.Generic;
    using Models;

    /// <summary>Interface for pricing factory product model.</summary>
    public interface IPricingFactoryProductModel
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

        /// <summary>Gets or sets the kit base quantity price multiplier.</summary>
        /// <value>The kit base quantity price multiplier.</value>
        decimal? KitBaseQuantityPriceMultiplier { get; set; }

        /// <summary>Gets or sets a list of identifiers of the vendors.</summary>
        /// <value>A list of identifiers of the vendors.</value>
        List<int> VendorIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the manufacturers.</summary>
        /// <value>A list of identifiers of the manufacturers.</value>
        List<int> ManufacturerIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the stores.</summary>
        /// <value>A list of identifiers of the stores.</value>
        List<int> StoreIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the franchises.</summary>
        /// <value>A list of identifiers of the franchises.</value>
        List<int> FranchiseIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the brands.</summary>
        /// <value>A list of identifiers of the brands.</value>
        List<int> BrandIDs { get; set; }

        /// <summary>Gets or sets a list of identifiers of the categories.</summary>
        /// <value>A list of identifiers of the categories.</value>
        List<int> CategoryIDs { get; set; }

        /// <summary>Gets or sets a list of keys of the categories.</summary>
        /// <value>A list of keys of the categories.</value>
        List<string> CategoryKeys { get; set; }

        /// <summary>Gets or sets the price base.</summary>
        /// <value>The price base.</value>
        decimal? PriceBase { get; set; }

        /// <summary>Gets or sets the price sale.</summary>
        /// <value>The price sale.</value>
        decimal? PriceSale { get; set; }

        /// <summary>Gets or sets the serializable attributes.</summary>
        /// <value>The serializable attributes.</value>
        SerializableAttributesDictionary? SerializableAttributes { get; set; }

        /// <summary>Gets or sets the cart item serializable attributes.</summary>
        /// <value>The cart item serializable attributes.</value>
        SerializableAttributesDictionary? CartItemSerializableAttributes { get; set; }

        /// <summary>Gets or sets the product price points.</summary>
        /// <value>The product price points.</value>
        IEnumerable<IProductPricePointModel>? ProductPricePoints { get; set; }
    }
}
