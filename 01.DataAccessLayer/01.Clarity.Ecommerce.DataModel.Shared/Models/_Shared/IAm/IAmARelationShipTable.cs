// <copyright file="IAmARelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmARelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    /// <summary>Interface for am a relationship table.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    /// <typeparam name="TSlave"> Type of the slave.</typeparam>
    /// <seealso cref="IAmARelationshipTable"/>
    public interface IAmARelationshipTable<out TMaster, TSlave>
        : IAmARelationshipTable<TSlave>
        where TMaster : IBase
        where TSlave : IBase
    {
        /// <summary>Gets the master.</summary>
        /// <value>The master.</value>
        TMaster? Master { get; }
    }

    /// <summary>Interface for am a relationship table.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmARelationshipTable<TSlave>
        : IAmARelationshipTable
        where TSlave : IBase
    {
        /// <summary>Gets or sets the slave.</summary>
        /// <value>The slave.</value>
        TSlave? Slave { get; set; }
    }

    /// <summary>Interface for am a relationship table.</summary>
    public interface IAmARelationshipTable
        : IBase
    {
        /// <summary>Gets or sets the identifier of the master.</summary>
        /// <value>The identifier of the master.</value>
        int MasterID { get; set; }

        /// <summary>Gets or sets the identifier of the slave.</summary>
        /// <value>The identifier of the slave.</value>
        int SlaveID { get; set; }
    }
}
