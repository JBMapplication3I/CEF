// <copyright file="IAmFilterableByManufacturerSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByManufacturerSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by manufacturer search model.</summary>
    public interface IAmFilterableByManufacturerSearchModel
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        int? ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer key.</summary>
        /// <value>The manufacturer key.</value>
        string? ManufacturerKey { get; set; }

        /// <summary>Gets or sets the name of the manufacturer.</summary>
        /// <value>The name of the manufacturer.</value>
        string? ManufacturerName { get; set; }
    }
}
