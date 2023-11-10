// <copyright file="IAmACategoryRelationshipTableWhereCategoryIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmACategoryRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a category relationship table where category is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmACategoryRelationshipTableWhereCategoryIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
            IAmFilterableByCategoryModel
    {
    }
}
