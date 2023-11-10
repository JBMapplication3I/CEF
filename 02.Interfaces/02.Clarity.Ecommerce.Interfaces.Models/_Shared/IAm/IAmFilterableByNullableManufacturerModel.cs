// <copyright file="IAmFilterableByNullableManufacturerModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByNullableManufacturerModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by nullable manufacturer model.</summary>
    public interface IAmFilterableByNullableManufacturerModel
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

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
