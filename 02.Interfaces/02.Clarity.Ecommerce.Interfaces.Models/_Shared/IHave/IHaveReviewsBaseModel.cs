// <copyright file="IHaveReviewsBaseModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IHaveReviewsBaseModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for have reviews base model.</summary>
    public interface IHaveReviewsBaseModel
    {
        /// <summary>Gets or sets the reviews.</summary>
        /// <value>The reviews.</value>
        List<IReviewModel>? Reviews { get; set; }
    }
}
