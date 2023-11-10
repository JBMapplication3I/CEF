// <copyright file="StatusableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the statusable base search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the statusable base search.</summary>
    /// <seealso cref="DisplayableBaseSearchModel"/>
    /// <seealso cref="IStatusableBaseSearchModel"/>
    public class StatusableBaseSearchModel : DisplayableBaseSearchModel, IStatusableBaseSearchModel
    {
    }
}
