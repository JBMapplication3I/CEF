// <copyright file="ICreated.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ICreated interface</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Payments
{
    using System;

    /// <summary>Interface for Created model.</summary>
    public interface ICreated
    {
        /// <summary>Gets or sets the create through.</summary>
        /// <value>The create through.</value>
        string? Through { get; set; }

        /// <summary>Gets or sets the created at date.</summary>
        /// <value>The created at date.</value>
        DateTime? At { get; set; }

        /// <summary>Gets or sets the created by.</summary>
        /// <value>The created by.</value>
        string? By { get; set; }

        /// <summary>Gets or sets the created from IP.</summary>
        /// <value>The created from IP.</value>
        string? FromIP { get; set; }
    }
}
