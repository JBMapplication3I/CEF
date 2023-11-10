// <copyright file="StatusSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the status search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using Interfaces.Models;

    /// <summary>A data Model for the status search.</summary>
    /// <seealso cref="StatusableBaseSearchModel"/>
    /// <seealso cref="IStatusSearchModel"/>
    public class StatusSearchModel : StatusableBaseSearchModel, IStatusSearchModel
    {
    }
}
