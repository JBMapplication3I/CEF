// <copyright file="IDigestModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IDigestModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for digest model.</summary>
    public interface IDigestModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int ID { get; set; }

        /// <summary>Gets or sets the key.</summary>
        /// <value>The key.</value>
        string? Key { get; set; }

        /// <summary>Gets or sets the hash.</summary>
        /// <value>The hash.</value>
        long Hash { get; set; }
    }
}
