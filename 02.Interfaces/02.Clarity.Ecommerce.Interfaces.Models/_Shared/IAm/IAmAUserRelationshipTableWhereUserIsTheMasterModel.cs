// <copyright file="IAmAUserRelationshipTableWhereUserIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAUserRelationshipTableWhereUserIsTheMasterModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a user relationship table where user is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmAUserRelationshipTableWhereUserIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByUserModel
    {
        /// <summary>Gets or sets the username of the master user.</summary>
        /// <value>The username of the master user.</value>
        string? MasterUserName { get; set; }
    }
}
