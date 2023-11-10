// <copyright file="IAmADiscountFilterRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmADiscountFilterRelationshipTable interface</summary>
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for am a stored file relationship table.</summary>
    /// <typeparam name="TSlave">Type of the slave.</typeparam>
    /// <seealso cref="IAmARelationshipTable{Discount,TSlave}"/>
    public interface IAmADiscountFilterRelationshipTable<TSlave>
        : IAmARelationshipTable<Discount, TSlave>
        where TSlave : IBase
    {
    }
}
