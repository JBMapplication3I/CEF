// <copyright file="IHaveAParentBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAParentBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;

    /// <summary>Interface for have a parent base.</summary>
    /// <typeparam name="TThis">Type of this object.</typeparam>
    public interface IHaveAParentBase<TThis> : IBase
    {
        /// <summary>Gets or sets the identifier of the parent.</summary>
        /// <value>The identifier of the parent.</value>
        int? ParentID { get; set; }

        /// <summary>Gets or sets the parent.</summary>
        /// <value>The parent.</value>
        TThis? Parent { get; set; }

        /// <summary>Gets or sets the children.</summary>
        /// <value>The children.</value>
        ICollection<TThis>? Children { get; set; }
    }
}
