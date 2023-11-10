// <copyright file="IAmAProductRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAProductRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a product relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmAProductRelationshipTableWhereProductIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Product>, IAmFilterableByProduct
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Product? Product { get; set; }
    }

    /// <summary>Interface for am a product relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmAProductRelationshipTableWhereProductIsTheMaster<TSlave>
        : IAmARelationshipTable<Product, TSlave>, IAmFilterableByProduct
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int ProductID { get; set; }

        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Product? Product { get; set; }
    }
}
