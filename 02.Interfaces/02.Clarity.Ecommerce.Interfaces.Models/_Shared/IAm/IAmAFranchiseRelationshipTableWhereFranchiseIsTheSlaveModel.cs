// <copyright file="IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a franchise relationship table model.</summary>
    public interface IAmAFranchiseRelationshipTableWhereFranchiseIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IFranchiseModel>,
          IAmFilterableByFranchiseModel
    {
    }
}
