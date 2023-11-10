// <copyright file="IAmFilterableByVendorModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByVendorModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by vendor model.</summary>
    /// <typeparam name="TModel">Type of the vendor relationship model.</typeparam>
    public interface IAmFilterableByVendorModel<TModel>
    {
        /// <summary>Gets or sets the vendors.</summary>
        /// <value>The vendors.</value>
        List<TModel>? Vendors { get; set; }
    }

    /// <summary>Interface for am filterable by vendor model.</summary>
    public interface IAmFilterableByVendorModel
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int VendorID { get; set; }

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
