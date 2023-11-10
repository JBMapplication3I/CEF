// <copyright file="IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlaveModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a manufacturer relationship table model.</summary>
    public interface IAmAManufacturerRelationshipTableWhereManufacturerIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IManufacturerModel>,
            IAmFilterableByManufacturerModel
    {
    }
}
