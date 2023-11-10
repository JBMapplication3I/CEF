// <copyright file="IAmFilterableByManufacturerModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByManufacturerModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by manufacturer model.</summary>
    /// <typeparam name="TModel">Type of the manufacturer relationship model.</typeparam>
    public interface IAmFilterableByManufacturerModel<TModel>
    {
        /// <summary>Gets or sets the manufacturers.</summary>
        /// <value>The manufacturers.</value>
        List<TModel>? Manufacturers { get; set; }
    }

    /// <summary>Interface for am filterable by manufacturer model.</summary>
    public interface IAmFilterableByManufacturerModel
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        IManufacturerModel? Manufacturer { get; set; }

        /// <summary>Gets or sets the manufacturer key.</summary>
        /// <value>The manufacturer key.</value>
        string? ManufacturerKey { get; set; }

        /// <summary>Gets or sets the name of the manufacturer.</summary>
        /// <value>The name of the manufacturer.</value>
        string? ManufacturerName { get; set; }
    }
}
