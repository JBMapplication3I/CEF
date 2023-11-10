// <copyright file="IAmABrandRelationshipTableWhereBrandIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmABrandRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a brand relationship table where brand is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmABrandRelationshipTableWhereBrandIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByBrandModel
    {
    }
}
