// <copyright file="IProductReviewInformationModel.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IProductReviewInformationModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System.Collections.Generic;

    /// <summary>Interface for product review information model.</summary>
    public interface IProductReviewInformationModel : IBaseModel
    {
        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        int ProductID { get; set; }

        /// <summary>Gets or sets the identifier of the review type.</summary>
        /// <value>The identifier of the review type.</value>
        int? ReviewTypeID { get; set; }

        /// <summary>Gets or sets the value.</summary>
        /// <value>The value.</value>
        decimal? Value { get; set; }

        /// <summary>Gets or sets the number of. </summary>
        /// <value>The count.</value>
        int Count { get; set; }

        /// <summary>Gets or sets the reviews.</summary>
        /// <value>The reviews.</value>
        List<IReviewModel>? Reviews { get; set; }
    }
}
