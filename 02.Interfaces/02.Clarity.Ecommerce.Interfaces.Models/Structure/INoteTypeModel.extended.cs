// <copyright file="INoteTypeModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the INoteTypeModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for note type model.</summary>
    /// <seealso cref="ITypableBaseModel"/>
    public partial interface INoteTypeModel
    {
        /// <summary>Gets or sets a value indicating whether this INoteTypeModel is customer.</summary>
        /// <value>True if this INoteTypeModel is customer, false if not.</value>
        bool IsCustomer { get; set; }

        /// <summary>Gets or sets a value indicating whether this INoteTypeModel is public.</summary>
        /// <value>True if this INoteTypeModel is public, false if not.</value>
        bool IsPublic { get; set; }
    }
}
