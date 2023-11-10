// <copyright file="IAmFilterableByNullableUserModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByNullableUserModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by nullable user model.</summary>
    public interface IAmFilterableByNullableUserModel
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        IUserModel? User { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }
    }
}
