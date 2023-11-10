// <copyright file="IAmAVendorRelationshipTableWhereVendorIsTheSlaveModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAVendorRelationshipTableModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a vendor relationship table model.</summary>
    public interface IAmAVendorRelationshipTableWhereVendorIsTheSlaveModel
        : IAmARelationshipTableBaseModel<IVendorModel>,
            IAmFilterableByVendorModel
    {
    }
}
