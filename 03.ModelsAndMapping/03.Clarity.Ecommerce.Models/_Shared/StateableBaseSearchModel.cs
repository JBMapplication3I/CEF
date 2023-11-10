// <copyright file="StateableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the stateable base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the stateable base search.</summary>
    /// <seealso cref="NameableBaseSearchModel"/>
    /// <seealso cref="IStateableBaseSearchModel"/>
    public abstract class StateableBaseSearchModel : DisplayableBaseSearchModel, IStateableBaseSearchModel
    {
    }
}
