// <copyright file="StoreUserModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store user model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the store user.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="Interfaces.Models.IStoreUserModel"/>
    public partial class StoreUserModel
    {
        /// <inheritdoc/>
        public string? UserName { get; set; }
    }
}
