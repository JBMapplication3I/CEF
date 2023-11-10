// <copyright file="IAmAVendorRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAVendorRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a vendor relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAVendorRelationshipTableWhereVendorIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Vendor>, IAmFilterableByVendor
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Vendor? Vendor { get; set; }
    }

    /// <summary>Interface for am a vendor relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAVendorRelationshipTableWhereVendorIsTheMaster<TSlave>
        : IAmARelationshipTable<Vendor, TSlave>, IAmFilterableByVendor
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the vendor.</summary>
        /// <value>The identifier of the vendor.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int VendorID { get; set; }

        /// <summary>Gets or sets the vendor.</summary>
        /// <value>The vendor.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Vendor? Vendor { get; set; }
    }
}
