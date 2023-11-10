// <copyright file="IAmFilterableByUserSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByUserSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am filterable by user search model.</summary>
    public interface IAmFilterableByUserSearchModel
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user identifier include null.</summary>
        /// <value>The user identifier include null.</value>
        bool? UserIDIncludeNull { get; set; }

        /// <summary>Gets or sets the user key.</summary>
        /// <value>The user key.</value>
        string? UserKey { get; set; }

        /// <summary>Gets or sets the username of the user.</summary>
        /// <value>The username of the user.</value>
        string? UserUsername { get; set; }

        /// <summary>Gets or sets the name of the user identifier or custom key or user.</summary>
        /// <value>The name of the user identifier or custom key or user.</value>
        string? UserIDOrCustomKeyOrUserName { get; set; }
    }
}
