// <copyright file="StatusableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the statusable base model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the statusable base.</summary>
    /// <seealso cref="DisplayableBaseModel"/>
    /// <seealso cref="IStateableBaseModel"/>
    public abstract class StatusableBaseModel : DisplayableBaseModel, IStatusableBaseModel
    {
    }
}
