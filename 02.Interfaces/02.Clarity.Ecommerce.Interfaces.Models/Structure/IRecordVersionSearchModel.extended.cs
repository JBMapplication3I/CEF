// <copyright file="IRecordVersionSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the IRecordVersionSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    using System;

    /// <summary>Interface for record version search model.</summary>
    public partial interface IRecordVersionSearchModel
    {
        /// <summary>Gets or sets the minimum either publish date.</summary>
        /// <value>The minimum either publish date.</value>
        DateTime? MinEitherPublishDate { get; set; }

        /// <summary>Gets or sets the maximum either publish date.</summary>
        /// <value>The maximum either publish date.</value>
        DateTime? MaxEitherPublishDate { get; set; }
    }
}
