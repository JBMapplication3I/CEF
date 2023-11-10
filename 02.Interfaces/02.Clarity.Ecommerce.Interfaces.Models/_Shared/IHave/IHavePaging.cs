// <copyright file="IHavePaging.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHavePaging interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for have paging.</summary>
    public interface IHavePaging
    {
        /// <summary>Gets or sets the paging.</summary>
        /// <value>The paging.</value>
        Paging? Paging { get; set; }
    }
}
