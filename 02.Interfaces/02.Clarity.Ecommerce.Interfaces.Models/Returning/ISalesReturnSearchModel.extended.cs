// <copyright file="ISalesReturnSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Declares the ISalesReturnSearchModel interface</summary>
namespace Clarity.Ecommerce.Interfaces.Models
{
    public partial interface ISalesReturnSearchModel
    {
        /// <summary>Gets or sets the identifier of the sales order.</summary>
        /// <value>The identifier of the sales order.</value>
        int? SalesOrderID { get; set; }
    }
}
