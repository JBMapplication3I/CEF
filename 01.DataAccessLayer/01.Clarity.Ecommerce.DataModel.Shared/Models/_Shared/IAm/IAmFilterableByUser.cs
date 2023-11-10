// <copyright file="IAmFilterableByUser.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmFilterableByUser interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for am filterable by user.</summary>
    /// <typeparam name="TEntity">Type of the entity.</typeparam>
    public interface IAmFilterableByUser<TEntity> : IBase
    {
        /// <summary>Gets or sets the users.</summary>
        /// <value>The users.</value>
        ICollection<TEntity>? Users { get; set; }
    }

    /// <summary>Interface for am filterable by user id.</summary>
    public interface IAmFilterableByUser : IBase
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }
    }

    /// <summary>Interface for am filterable by (nullable) user id.</summary>
    public interface IAmFilterableByNullableUser : IBase
    {
        /// <summary>Gets or sets the identifier of the user.</summary>
        /// <value>The identifier of the user.</value>
        int? UserID { get; set; }

        /// <summary>Gets or sets the user.</summary>
        /// <value>The user.</value>
        User? User { get; set; }
    }
}
