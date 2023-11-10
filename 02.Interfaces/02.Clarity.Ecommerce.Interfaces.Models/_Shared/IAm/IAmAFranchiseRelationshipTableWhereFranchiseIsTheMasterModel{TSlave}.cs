// <copyright file="IAmAFranchiseRelationshipTableWhereFranchiseIsTheMasterModel{TSlave}.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAFranchiseRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a franchise relationship table where franchise is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmAFranchiseRelationshipTableWhereFranchiseIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByFranchiseModel
    {
    }
}
