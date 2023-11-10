// <copyright file="IHaveGrouping.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveGrouping interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have grouping.</summary>
    public interface IHaveGrouping
    {
        /// <summary>Gets or sets the groupings.</summary>
        /// <value>The groupings.</value>
        Grouping[]? Groupings { get; set; }
    }
}
