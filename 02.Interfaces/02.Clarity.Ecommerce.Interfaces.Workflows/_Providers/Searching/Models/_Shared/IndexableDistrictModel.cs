// <copyright file="IndexableDistrictModel.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the indexable district model class</summary>
namespace Clarity.Ecommerce.Interfaces.Providers.Searching
{
    /// <summary>An indexable district model.</summary>
    /// <seealso cref="IndexableDistrictModel"/>
    public class IndexableDistrictModel : IndexableFilterBase
    {
        /// <summary>Gets or sets the number of. </summary>
        /// <value>The count.</value>
        public long? DocCount { get; set; }
    }
}
