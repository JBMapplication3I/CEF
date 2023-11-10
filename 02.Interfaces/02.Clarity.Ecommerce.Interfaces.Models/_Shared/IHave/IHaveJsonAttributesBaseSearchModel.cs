// <copyright file="IHaveJsonAttributesBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveJsonAttributesBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have JSON attributes base search model.</summary>
    public interface IHaveJsonAttributesBaseSearchModel
    {
        /// <summary>Gets or sets the JSON attributes.</summary>
        /// <value>The JSON attributes.</value>
        Dictionary<string, string?[]?>? JsonAttributes { get; set; }
    }
}
