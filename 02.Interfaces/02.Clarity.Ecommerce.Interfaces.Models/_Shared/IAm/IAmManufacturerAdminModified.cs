// <copyright file="IAmManufacturerAdminModified.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmManufacturerAdminModified interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am manufacturer admin modified.</summary>
    public interface IAmManufacturerAdminModified
    {
        /// <summary>Gets or sets the is manufacturer admin.</summary>
        /// <value>The is manufacturer admin.</value>
        bool? IsManufacturerAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the manufacturer admin.</summary>
        /// <value>The identifier of the manufacturer admin.</value>
        int? ManufacturerAdminID { get; set; }
    }
}
