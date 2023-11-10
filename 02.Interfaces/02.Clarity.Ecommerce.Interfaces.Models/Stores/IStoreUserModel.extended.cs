// <copyright file="IStoreUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IStoreUserModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for store user model.</summary>
    public partial interface IStoreUserModel
    {
        #region Related Objects
        /// <summary>Gets or sets the name of the user.</summary>
        /// <value>The name of the user.</value>
        string? UserName { get; set; }
        #endregion
    }
}
