// <copyright file="IAmAnAccountRelationshipTableWhereAccountIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAnAccountRelationshipTableWhereAccountIsTheMasterModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am an account relationship table where account is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmAnAccountRelationshipTableWhereAccountIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByAccountModel
    {
    }
}
