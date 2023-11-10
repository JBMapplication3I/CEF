﻿// <copyright file="IAmAManufacturerRelationshipTableWhereManufacturerIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAManufacturerRelationshipTableWhereManufacturerIsTheMasterModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a manufacturer relationship table where manufacturer is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmAManufacturerRelationshipTableWhereManufacturerIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByManufacturerModel
    {
    }
}
