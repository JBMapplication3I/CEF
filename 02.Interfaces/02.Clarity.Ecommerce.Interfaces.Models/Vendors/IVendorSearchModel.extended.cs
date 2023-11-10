// <copyright file="IVendorSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IVendorSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for vendor search model.</summary>
    public partial interface IVendorSearchModel
    {
        /// <summary>Gets or sets the notes.</summary>
        /// <value>The notes.</value>
        string? Notes { get; set; }

        /// <summary>Gets or sets the identifier of the address.</summary>
        /// <value>The identifier of the address.</value>
        int? AddressID { get; set; }

        /// <summary>Gets or sets the identifier of the term.</summary>
        /// <value>The identifier of the term.</value>
        int? TermID { get; set; }

        /// <summary>Gets or sets the identifier of the vendors ship via.</summary>
        /// <value>The identifier of the vendors ship via.</value>
        int? VendorsShipViaID { get; set; }
    }
}
