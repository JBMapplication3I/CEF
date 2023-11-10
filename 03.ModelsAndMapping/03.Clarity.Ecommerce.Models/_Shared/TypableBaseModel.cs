// <copyright file="TypableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the typable base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the typable base.</summary>
    /// <seealso cref="DisplayableBaseModel"/>
    /// <seealso cref="ITypableBaseModel"/>
    public abstract class TypableBaseModel : DisplayableBaseModel, ITypableBaseModel
    {
    }
}
