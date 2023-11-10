// <copyright file="IAmAnAccountRelationshipTableWhereAccountIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAnAccountRelationshipTableWhereAccountIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am an account relationship table where account is the slave model.</summary>
    public interface IAmAnAccountRelationshipTableWhereAccountIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IAccountModel>,
            IAmFilterableByAccountModel
    {
    }
}
