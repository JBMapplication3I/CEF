// <copyright file="INameableBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the INameableBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for nameable base search model.</summary>
    public interface INameableBaseSearchModel : IBaseSearchModel
    {
        /// <summary>Gets or sets the name.</summary>
        /// <value>The name.</value>
        string? Name { get; set; }

        /// <summary>Gets or sets the name strict.</summary>
        /// <value>The name strict.</value>
        bool? NameStrict { get; set; }

        /// <summary>Gets or sets the description.</summary>
        /// <value>The description.</value>
        string? Description { get; set; }

        /// <summary>Gets or sets the name of the custom key or.</summary>
        /// <value>The name of the custom key or.</value>
        string? CustomKeyOrName { get; set; }

        /// <summary>Gets or sets information describing the custom key or name or.</summary>
        /// <value>Information describing the custom key or name or.</value>
        string? CustomKeyOrNameOrDescription { get; set; }

        /// <summary>Gets or sets the name of the identifier or custom key or.</summary>
        /// <value>The name of the identifier or custom key or.</value>
        string? IDOrCustomKeyOrName { get; set; }

        /// <summary>Gets or sets information describing the identifier or custom key or name or.</summary>
        /// <value>Information describing the identifier or custom key or name or.</value>
        string? IDOrCustomKeyOrNameOrDescription { get; set; }
    }
}
