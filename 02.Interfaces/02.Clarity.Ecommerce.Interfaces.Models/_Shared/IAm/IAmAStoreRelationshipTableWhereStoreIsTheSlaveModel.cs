// <copyright file="IAmAStoreRelationshipTableWhereStoreIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAStoreRelationshipTableWhereStoreIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a store relationship table model.</summary>
    public interface IAmAStoreRelationshipTableWhereStoreIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IStoreModel>,
            IAmFilterableByStoreModel
    {
    }
}
