// <copyright file="IAmAFavoriteRelationshipTable.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmAFavoriteRelationshipTable interface</summary>
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for am a favorite relationship table.</summary>
    /// <typeparam name="TSlave">Type of the slave entity.</typeparam>
    public interface IAmAFavoriteRelationshipTable<TSlave>
        : IAmARelationshipTable<User, TSlave>
        where TSlave : IBase
    {
    }
}
