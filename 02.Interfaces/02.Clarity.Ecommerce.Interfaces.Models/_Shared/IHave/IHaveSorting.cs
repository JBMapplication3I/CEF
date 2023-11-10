// <copyright file="IHaveSorting.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveSorting interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have sorting.</summary>
    public interface IHaveSorting
    {
        /// <summary>Gets or sets the sorts.</summary>
        /// <value>The sorts.</value>
        Sort[]? Sorts { get; set; }
    }
}
