// <copyright file="ISalesQuoteSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesQuoteSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales quote search model.</summary>
    public partial interface ISalesQuoteSearchModel
    {
        /// <summary>Gets or sets the category i ds.</summary>
        /// <value>The category i ds.</value>
        int[]? CategoryIDs { get; set; }

        /// <summary>Gets or sets the has sales group as master.</summary>
        /// <value>The has sales group as master.</value>
        bool? HasSalesGroupAsMaster { get; set; }

        /// <summary>Gets or sets the has sales group as sub.</summary>
        /// <value>The has sales group as sub.</value>
        bool? HasSalesGroupAsSub { get; set; }

        /// <summary>Gets or sets the has sales group as response.</summary>
        /// <value>The has sales group as response.</value>
        bool? HasSalesGroupAsResponse { get; set; }

        /// <summary>Gets or sets the has sales group as request.</summary>
        /// <value>The has sales group as request.</value>
        bool? HasSalesGroupAsRequest { get; set; }
    }
}
