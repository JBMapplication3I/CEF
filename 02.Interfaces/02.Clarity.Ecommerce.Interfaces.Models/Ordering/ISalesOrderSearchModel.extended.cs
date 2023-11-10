// <copyright file="ISalesOrderSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesOrderSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    /// <summary>Interface for sales order search model.</summary>
    public partial interface ISalesOrderSearchModel
    {
        /// <summary>Gets or sets the has sales group as master.</summary>
        /// <value>The has sales group as master.</value>
        bool? HasSalesGroupAsMaster { get; set; }

        /// <summary>Gets or sets the has sales group as sub.</summary>
        /// <value>The has sales group as sub.</value>
        bool? HasSalesGroupAsSub { get; set; }
    }
}
