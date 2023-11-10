// <copyright file="IAmFilterableByBrand.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByBrand interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by brand.</summary>
    /// <typeparam name="TEntity">Type of the brand link entity.</typeparam>
    public interface IAmFilterableByBrand<TEntity> : IBase
    {
        /// <summary>Gets or sets the brands.</summary>
        /// <value>The brands.</value>
        ICollection<TEntity>? Brands { get; set; }
    }

    /// <summary>Interface for am filterable by brand id.</summary>
    public interface IAmFilterableByBrand : IBase
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        Brand? Brand { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) brand id.</summary>
    public interface IAmFilterableByNullableBrand : IBase
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        int? BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        Brand? Brand { get; set; }
    }
}
