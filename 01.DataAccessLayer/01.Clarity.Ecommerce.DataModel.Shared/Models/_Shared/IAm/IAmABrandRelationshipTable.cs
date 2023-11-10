// <copyright file="IAmABrandRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmABrandRelationshipTable interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System;
    using Ecommerce.DataModel;

    /// <summary>Interface for am a brand relationship table base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    public interface IAmABrandRelationshipTableWhereBrandIsTheSlave<out TMaster>
        : IAmARelationshipTable<TMaster, Brand>, IAmFilterableByBrand
        where TMaster : IBase
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [Obsolete("Cannot use in queries, use SlaveID instead.", true)]
        new int BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        [Obsolete("Cannot use in queries, use Slave instead.", true)]
        new Brand? Brand { get; set; }
    }

    /// <summary>Interface for am a brand relationship table base.</summary>
    /// <typeparam name="TSlave">Type of the master.</typeparam>
    public interface IAmABrandRelationshipTableWhereBrandIsTheMaster<TSlave>
        : IAmARelationshipTable<Brand, TSlave>, IAmFilterableByBrand
        where TSlave : IBase
    {
        /// <summary>Gets or sets the identifier of the brand.</summary>
        /// <value>The identifier of the brand.</value>
        [Obsolete("Cannot use in queries, use MasterID instead.", true)]
        new int BrandID { get; set; }

        /// <summary>Gets or sets the brand.</summary>
        /// <value>The brand.</value>
        [Obsolete("Cannot use in queries, use Master instead.", true)]
        new Brand? Brand { get; set; }
    }
}
