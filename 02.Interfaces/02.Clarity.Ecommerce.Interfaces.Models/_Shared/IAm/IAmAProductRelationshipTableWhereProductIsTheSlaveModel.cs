// <copyright file="IAmAProductRelationshipTableWhereProductIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAProductRelationshipTableWhereProductIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a product relationship table model.</summary>
    public interface IAmAProductRelationshipTableWhereProductIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IProductModel>,
            IAmFilterableByProductModel
    {
    }
}
