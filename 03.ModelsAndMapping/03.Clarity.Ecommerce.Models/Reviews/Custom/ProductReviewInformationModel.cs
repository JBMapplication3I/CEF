// <copyright file="ProductReviewInformationModel.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product review information model class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces.Models;

    /// <summary>A data Model for the product review information.</summary>
    /// <seealso cref="BaseModel"/>
    /// <seealso cref="IProductReviewInformationModel"/>
    public class ProductReviewInformationModel : BaseModel, IProductReviewInformationModel
    {
        /// <inheritdoc/>
        public int ProductID { get; set; }

        /// <inheritdoc/>
        public int? ReviewTypeID { get; set; }

        /// <inheritdoc/>
        public decimal? Value { get; set; }

        /// <inheritdoc/>
        public int Count { get; set; }

        /// <inheritdoc cref="IProductReviewInformationModel.Reviews"/>
        public List<ReviewModel>? Reviews { get; set; }

        /// <inheritdoc/>
        List<IReviewModel>? IProductReviewInformationModel.Reviews { get => Reviews?.ToList<IReviewModel>(); set => Reviews = value?.Cast<ReviewModel>().ToList(); }
    }
}
