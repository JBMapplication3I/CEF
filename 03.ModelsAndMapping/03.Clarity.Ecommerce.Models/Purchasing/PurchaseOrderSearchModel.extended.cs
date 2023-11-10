// <copyright file="PurchaseOrderSearchModel.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the purchase order search model class</summary>
namespace Clarity.Ecommerce.Models
{
    using ServiceStack;

    /// <summary>A data Model for the purchase order search.</summary>
    /// <seealso cref="SalesCollectionBaseSearchModel"/>
    /// <seealso cref="Interfaces.Models.IPurchaseOrderSearchModel"/>
    public partial class PurchaseOrderSearchModel
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(VendorName), DataType = "string", ParameterType = "query", IsRequired = false,
            Description = "Vendor Name")]
        public string? VendorName { get; set; }
    }
}
