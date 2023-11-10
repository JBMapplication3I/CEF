// <copyright file="IAmFilterableByNullableVendorModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByNullableVendorModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by nullable vendor model.</summary>
    public interface IAmFilterableByNullableVendorModel
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        IVendorModel? Vendor { get; set; }

        /// <summary>Gets or sets the vendor key.</summary>
        /// <value>The vendor key.</value>
        string? VendorKey { get; set; }

        /// <summary>Gets or sets the name of the vendor.</summary>
        /// <value>The name of the vendor.</value>
        string? VendorName { get; set; }
    }
}
