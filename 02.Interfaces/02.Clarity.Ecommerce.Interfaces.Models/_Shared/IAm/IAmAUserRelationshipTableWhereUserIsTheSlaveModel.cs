// <copyright file="IAmAUserRelationshipTableWhereUserIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAUserRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a user relationship table where user is the slave model.</summary>
    public interface IAmAUserRelationshipTableWhereUserIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IUserModel>,
            IAmFilterableByUserModel
    {
        /// <summary>Gets or sets the username of the slave user.</summary>
        /// <value>The username of the slave user.</value>
        string? SlaveUserName { get; set; }
    }
}
