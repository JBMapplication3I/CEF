// <copyright file="IAmACurrencyRelationshipTableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmACurrencyRelationshipTableBase interface</summary>
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for object currency base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    /// <seealso cref="IAmARelationshipTable{TMaster,Currency}"/>
    public interface IAmACurrencyRelationshipTableBase<out TMaster>
        : IAmARelationshipTable<TMaster, Currency>
        where TMaster : IBase
    {
    }
}
