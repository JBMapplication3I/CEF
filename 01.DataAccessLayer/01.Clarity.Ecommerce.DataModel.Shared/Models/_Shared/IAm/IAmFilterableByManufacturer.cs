// <copyright file="IAmFilterableByManufacturer.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByManufacturer interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by manufacturer.</summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IAmFilterableByManufacturer<TEntity> : IBase
    {
        /// <summary>Gets or sets the manufacturers.</summary>
        /// <value>The manufacturers.</value>
        ICollection<TEntity>? Manufacturers { get; set; }
    }

    /// <summary>Interface for am filterable by manufacturer id.</summary>
    public interface IAmFilterableByManufacturer : IBase
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        Manufacturer? Manufacturer { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) manufacturer id.</summary>
    public interface IAmFilterableByNullableManufacturer : IBase
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        Manufacturer? Manufacturer { get; set; }
    }
}
