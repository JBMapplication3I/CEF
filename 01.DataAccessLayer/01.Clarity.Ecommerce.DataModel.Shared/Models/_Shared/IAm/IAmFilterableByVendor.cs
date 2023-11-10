// <copyright file="IAmFilterableByVendor.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByVendor interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by vendor.</summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IAmFilterableByVendor<TEntity> : IBase
    {
        /// <summary>Gets or sets the vendors.</summary>
        /// <value>The vendors.</value>
        ICollection<TEntity>? Vendors { get; set; }
    }

    /// <summary>Interface for am filterable by vendor id.</summary>
    public interface IAmFilterableByVendor : IBase
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        Vendor? Vendor { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) vendor id.</summary>
    public interface IAmFilterableByNullableVendor : IBase
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        int? VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        Vendor? Vendor { get; set; }
    }
}
