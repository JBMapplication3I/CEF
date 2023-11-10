// <copyright file="IHaveReviewsBase.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveReviewsBase interface</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    using System.Collections.Generic;
    using Ecommerce.DataModel;

    /// <summary>Interface for have reviews.</summary>
    public interface IHaveReviewsBase
    {
        /// <summary>Gets or sets the reviews.</summary>
        /// <value>The reviews.</value>
        ICollection<Review>? Reviews { get; set; }
    }
}
