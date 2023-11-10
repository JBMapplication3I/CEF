// <copyright file="IHaveAParentBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAParentBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have a parent base model.</summary>
    /// <typeparam name="TIThis">Type of This (the the object class) as an Interface.</typeparam>
    public interface IHaveAParentBaseModel<TIThis>
        where TIThis : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the parent.</summary>
        /// <value>The identifier of the parent.</value>
        int? ParentID { get; set; }

        /// <summary>Gets or sets the parent key.</summary>
        /// <value>The parent key.</value>
        string? ParentKey { get; set; }

        /// <summary>Gets or sets the parent.</summary>
        /// <value>The parent.</value>
        TIThis? Parent { get; set; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The children.</value>
        List<TIThis>? Children { get; set; }

        /// <summary>Gets or sets a value indicating whether this IHaveAParentBaseModel{TIThis} has children.</summary>
        /// <value>True if this IHaveAParentBaseModel{TIThis} has children, false if not.</value>
        bool HasChildren { get; set; }
    }
}
