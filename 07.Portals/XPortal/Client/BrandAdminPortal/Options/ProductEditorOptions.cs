// <copyright file="ProductEditorOptions.cs" company="clarity-ventures.com">
// Copyright (c) 2022-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product editor options class</summary>
namespace Clarity.Ecommerce.MVC.Api.Options
{
    using System.Collections.Generic;

    /// <summary>A product editor options.</summary>
    public class ProductEditorOptions
    {
        /// <summary>Gets or sets the allowed editor attribute IDs. If set, only these attributes can show up in the
        /// editor.</summary>
        /// <value>The allowed editor attribute IDs.</value>
        public HashSet<int>? AllowedEditorAttributeIDs { get; set; }
    }
}
