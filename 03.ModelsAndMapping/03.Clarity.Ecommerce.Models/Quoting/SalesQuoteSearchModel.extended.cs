// <copyright file="SalesQuoteSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales quote search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the sales quote search.</summary>
    /// <seealso cref="SalesCollectionBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.ISalesQuoteSearchModel"/>
    public partial class SalesQuoteSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CategoryIDs), DataType = "int[]", ParameterType = "query", IsRequired = false)]
        public int[]? CategoryIDs { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSalesGroupAsMaster), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSalesGroupAsMaster { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSalesGroupAsSub), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSalesGroupAsSub { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSalesGroupAsResponse), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSalesGroupAsResponse { get; set; }

        /// <inheritdoc/>
        [ApiMember(Name = nameof(HasSalesGroupAsRequest), DataType = "bool?", ParameterType = "query", IsRequired = false)]
        public bool? HasSalesGroupAsRequest { get; set; }
    }
}
