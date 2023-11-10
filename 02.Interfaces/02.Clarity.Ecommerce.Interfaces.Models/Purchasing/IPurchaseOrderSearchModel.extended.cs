// <copyright file="IPurchaseOrderSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IPurchaseOrderSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for purchase order search model.</summary>
    public partial interface IPurchaseOrderSearchModel
    {
        /// <summary>Gets or sets the name of the vendor.</summary>
        /// <value>The name of the vendor.</value>
        string? VendorName { get; set; }
    }
}
