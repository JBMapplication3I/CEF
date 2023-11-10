// <copyright file="StatusModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the status model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the status.</summary>
    /// <seealso cref="StatusableBaseModel"/>
    /// <seealso cref="IStatusModel"/>
    public class StatusModel : StatusableBaseModel, IStatusModel
    {
    }
}
