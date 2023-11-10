// <copyright file="OracleRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the oracle request class</summary>
namespace Clarity.Ecommerce.Providers.Shipping.Oracle.Models
{
    /// <summary>An oracle request.</summary>
    public class OracleRequest
    {
        /// <summary>Gets or sets the origin.</summary>
        /// <value>The origin.</value>
        public Origin? Origin { get; set; }

        /// <summary>Gets or sets the shipping.</summary>
        /// <value>The shipping.</value>
        public Shipping? Shipping { get; set; }

        /// <summary>Gets or sets the subtotal.</summary>
        /// <value>The subtotal.</value>
        public decimal Subtotal { get; set; }
    }
}
