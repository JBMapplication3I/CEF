// <copyright file="IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a category relationship table model.</summary>
    public interface IAmACategoryRelationshipTableWhereCategoryIsTheSlaveModel
        : IAmARelationshipTableBaseModel<ICategoryModel>,
            IAmFilterableByCategoryModel
    {
    }
}
