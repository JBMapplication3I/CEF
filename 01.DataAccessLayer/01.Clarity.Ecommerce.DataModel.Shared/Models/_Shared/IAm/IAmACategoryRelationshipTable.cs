// <copyright file="IAmACategoryRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmACategoryRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a category relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmACategoryRelationshipTableWhereCategoryIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Category>, IAmFilterableByCategory
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Category? Category { get; set; }
    }

    /// <summary>Interface for am a category relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmACategoryRelationshipTableWhereCategoryIsTheMaster<TSlave>
        : IAmARelationshipTable<Category, TSlave>, IAmFilterableByCategory
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the category.</summary>
        /// <value>The identifier of the category.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int CategoryID { get; set; }

        /// <summary>Gets or sets the category.</summary>
        /// <value>The category.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Category? Category { get; set; }
    }
}
