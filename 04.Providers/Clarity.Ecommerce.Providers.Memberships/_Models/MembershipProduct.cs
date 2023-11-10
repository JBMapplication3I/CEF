// <copyright file="MembershipProduct.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the membership product class</summary>
namespace Clarity.Ecommerce.Providers.Memberships
{
    using System.Collections.Generic;
    using Interfaces.DataModel;

    /// <summary>A membership product.</summary>
    public class MembershipProduct
    {
        /// <summary>Gets or sets the product membership levels.</summary>
        /// <value>The product membership levels.</value>
        public List<IProductMembershipLevel>? ProductMembershipLevels { get; set; }

        /// <summary>Gets or sets the name of the product.</summary>
        /// <value>The name of the product.</value>
        public string? ProductName { get; set; }

        /// <summary>Gets or sets information describing the product.</summary>
        /// <value>Information describing the product.</value>
        public string? ProductDescription { get; set; }

        /// <summary>Gets or sets the price.</summary>
        /// <value>The price.</value>
        public decimal Price { get; set; }
    }
}
