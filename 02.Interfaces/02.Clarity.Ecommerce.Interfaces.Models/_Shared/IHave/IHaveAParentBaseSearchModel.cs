// <copyright file="IHaveAParentBaseSearchModel.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveAParentBaseSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have a parent base search model.</summary>
    public interface IHaveAParentBaseSearchModel : IBaseSearchModel
    {
        /// <summary>Gets or sets the identifier of the parent.</summary>
        /// <value>The identifier of the parent.</value>
        int? ParentID { get; set; }

        /// <summary>Gets or sets the parent identifier include null.</summary>
        /// <value>The parent identifier include null.</value>
        bool? ParentIDIncludeNull { get; set; }

        /// <summary>Gets or sets the parent key.</summary>
        /// <value>The parent key.</value>
        string? ParentKey { get; set; }

        /// <summary>Gets or sets the disregard parents.</summary>
        /// <value>The disregard parents.</value>
        bool? DisregardParents { get; set; }

        /// <summary>Gets or sets the has children.</summary>
        /// <value>The has children.</value>
        bool? HasChildren { get; set; }

        /// <summary>Gets or sets a value indicating whether the children in results should be included.</summary>
        /// <value>True if include children in results, false if not.</value>
        bool IncludeChildrenInResults { get; set; }

        /// <summary>Gets or sets the identifier of the child.</summary>
        /// <value>The identifier of the child.</value>
        int? ChildID { get; set; }

        /// <summary>Gets or sets the child key.</summary>
        /// <value>The child key.</value>
        string? ChildKey { get; set; }
    }
}
