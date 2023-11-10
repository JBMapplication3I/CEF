// <copyright file="IAmAManufacturerRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAManufacturerRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a manufacturer relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Manufacturer>, IAmFilterableByManufacturer
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Manufacturer? Manufacturer { get; set; }
    }

    /// <summary>Interface for am a manufacturer relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAManufacturerRelationshipTableWhereManufacturerIsTheMaster<TSlave>
        : IAmARelationshipTable<Manufacturer, TSlave>, IAmFilterableByManufacturer
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the manufacturer.</summary>
        /// <value>The identifier of the manufacturer.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int ManufacturerID { get; set; }

        /// <summary>Gets or sets the manufacturer.</summary>
        /// <value>The manufacturer.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Manufacturer? Manufacturer { get; set; }
    }
}
