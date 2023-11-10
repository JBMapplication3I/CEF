// <copyright file="IAmAProductRelationshipTableWhereProductIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAProductRelationshipTableWhereProductIsTheMasterModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a product relationship table where product is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmAProductRelationshipTableWhereProductIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByProductModel
    {
    }
}
