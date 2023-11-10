// <copyright file="INameableBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the INameableBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for nameable base model.</summary>
    /// <seealso cref="IBaseModel"/>
    public interface INameableBaseModel : IBaseModel
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        string? Description { get; set; }
    }
}
