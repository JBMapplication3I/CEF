// <copyright file="IAmFilterableByUserModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByUserModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for am filterable by user model.</summary>
    /// <typeparam name="TModel">Type of the user relationship model.</typeparam>
    public interface IAmFilterableByUserModel<TModel>
    {
        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        List<TModel>? Users { get; set; }
    }

    /// <summary>Interface for am filterable by user model.</summary>
    public interface IAmFilterableByUserModel
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the user username.</summary>
        /// <value>The user username.</value>
        string? UserUsername { get; set; }
    }
}
