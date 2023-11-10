// <copyright file="IAmFilterableByVendorSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByVendorSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by vendor search model.</summary>
    public interface IAmFilterableByVendorSearchModel
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor key.</summary>
        /// <value>The vendor key.</value>
        string? VendorKey { get; set; }

        /// <summary>Gets or sets the name of the vendor.</summary>
        /// <value>The name of the vendor.</value>
        string? VendorName { get; set; }
    }
}
