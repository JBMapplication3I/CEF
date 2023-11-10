// <copyright file="IAmALanguageRelationshipTableBase.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IAmALanguageRelationshipTableBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using Ecommerce.DataModel;

    /// <summary>Interface for object language base.</summary>
    /// <typeparam name="TMaster">Type of the master.</typeparam>
    /// <seealso cref="IAmARelationshipTable{TMaster,Language}"/>
    public interface IAmALanguageRelationshipTableBase<out TMaster>
        : IAmARelationshipTable<TMaster, Language>
        where TMaster : IBase
    {
    }
}
