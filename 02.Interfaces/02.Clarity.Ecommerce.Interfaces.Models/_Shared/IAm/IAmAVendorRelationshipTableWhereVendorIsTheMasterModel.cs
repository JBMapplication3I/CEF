// <copyright file="IAmAVendorRelationshipTableWhereVendorIsTheMasterModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAVendorRelationshipTableWhereVendorIsTheMasterModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for am a vendor relationship table where vendor is the master model.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    public interface IAmAVendorRelationshipTableWhereVendorIsTheMasterModel<TSlave>
        : IAmARelationshipTableBaseModel<TSlave>,
          IAmFilterableByVendorModel
    {
    }
}
