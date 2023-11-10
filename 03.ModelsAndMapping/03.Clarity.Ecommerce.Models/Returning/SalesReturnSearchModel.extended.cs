// <copyright file="SalesReturnSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales return search model class</summary>
namespace Clarity.Ecommerce.Models
{
    /// <summary>A data Model for the sales return search.</summary>
    /// <seealso cref="SalesCollectionBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ISalesReturnSearchModel"/>
    public partial class SalesReturnSearchModel
    {
        /// <summary>Gets or sets the status exclusion.</summary>
        /// <value>The status exclusion.</value>
        public string? StatusExclusion { get; set; }

        /// <inheritdoc/>
        public int? SalesOrderID { get; set; }
    }
}
