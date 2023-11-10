// <copyright file="IAmFilterableByNullableBrandModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByNullableBrandModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by nullable brand model.</summary>
    public interface IAmFilterableByNullableBrandModel
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int? BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        IBrandModel? Brand { get; set; }

        /// <summary>Gets or sets the brand key.</summary>
        /// <value>The brand key.</value>
        string? BrandKey { get; set; }

        /// <summary>Gets or sets the name of the brand.</summary>
        /// <value>The name of the brand.</value>
        string? BrandName { get; set; }
    }
}
