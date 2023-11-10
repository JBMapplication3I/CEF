// <copyright file="IAmVendorAdminModified.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmVendorAdminModified interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am vendor admin modified.</summary>
    public interface IAmVendorAdminModified
    {
        /// <summary>Gets or sets the is vendor admin.</summary>
        /// <value>The is vendor admin.</value>
        bool? IsVendorAdmin { get; set; }

        /// <summary>Gets or sets the identifier of the vendor admin.</summary>
        /// <value>The identifier of the vendor admin.</value>
        int? VendorAdminID { get; set; }
    }
}
