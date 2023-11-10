// <copyright file="SalesOrderSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales order search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the sales order search.</summary>
    /// <seealso cref="SalesCollectionBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ISalesOrderSearchModel"/>
    public partial class SalesOrderSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSalesGroupAsMaster), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSalesGroupAsMaster { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSalesGroupAsSub), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSalesGroupAsSub { get; set; }
    }
}
