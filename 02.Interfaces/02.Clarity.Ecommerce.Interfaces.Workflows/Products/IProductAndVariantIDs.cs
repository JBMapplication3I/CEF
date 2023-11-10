// <copyright file="IProductAndVariantIDs.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductAndVariantIDs interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;
    using DataModel;

    /// <summary>Interface for product and variant identifiers.</summary>
    public interface IProductAndVariantIDs
    {
        /// <summary>Gets or sets the product.</summary>
        /// <value>The product.</value>
        IProduct Product { get; set; }

        /// <summary>Gets or sets the variant identifiers.</summary>
        /// <value>The variant IDs.</value>
        List<int> VariantIDs { get; set; }
    }
}
