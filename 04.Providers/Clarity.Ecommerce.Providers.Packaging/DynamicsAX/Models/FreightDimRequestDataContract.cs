// <copyright file="FreightDimRequestDataContract.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the FreightDimRequestDataContract class</summary>
namespace Clarity.Ecommerce.Providers.Packaging.DynamicsAx.Models
{
    /// <summary>A request for a package dimensions calculator collection.</summary>
    public class FreightDimRequestDataContract
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        public string? ItemId { get; set; }

        /// <summary>Gets or sets the quantity.</summary>
        /// <value>The quantity.</value>
        public decimal OrderQty { get; set; }
    }
}
