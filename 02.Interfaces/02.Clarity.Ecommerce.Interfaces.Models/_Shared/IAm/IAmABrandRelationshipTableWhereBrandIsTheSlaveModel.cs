// <copyright file="IAmABrandRelationshipTableWhereBrandIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmABrandRelationshipTableWhereBrandIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a brand relationship table model.</summary>
    public interface IAmABrandRelationshipTableWhereBrandIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IBrandModel>,
          IAmFilterableByBrandModel
    {
    }
}
