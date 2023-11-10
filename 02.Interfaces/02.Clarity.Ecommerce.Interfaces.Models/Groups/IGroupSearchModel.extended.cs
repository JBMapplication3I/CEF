// <copyright file="IGroupSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IGroupSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for group search model.</summary>
    public partial interface IGroupSearchModel
    {
        /// <summary>Gets or sets the identifier that owns this item.</summary>
        /// <value>The identifier of the owner.</value>
        int? OwnerID { get; set; }
    }
}
