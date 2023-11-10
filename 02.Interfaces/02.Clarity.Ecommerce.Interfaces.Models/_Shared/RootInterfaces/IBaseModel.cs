// <copyright file="IBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for base model.</summary>
    /// <see cref="IHaveJsonAttributesBaseModel" />
    public interface IBaseModel : IHaveJsonAttributesBaseModel
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        int ID { get; set; }

        /// <summary>Gets or sets the custom key.</summary>
        /// <value>The custom key.</value>
        string? CustomKey { get; set; }

        /// <summary>Gets or sets a value indicating whether the active.</summary>
        /// <value>True if active, false if not.</value>
        bool Active { get; set; }

        /// <summary>Gets or sets the created date.</summary>
        /// <value>The created date.</value>
        DateTime CreatedDate { get; set; }

        /// <summary>Gets or sets the updated date.</summary>
        /// <value>The updated date.</value>
        DateTime? UpdatedDate { get; set; }

        /// <summary>Gets or sets the hash.</summary>
        /// <value>The hash.</value>
        long? Hash { get; set; }
    }
}
