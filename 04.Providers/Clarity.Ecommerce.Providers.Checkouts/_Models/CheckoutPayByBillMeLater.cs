// <copyright file="CheckoutPayByBillMeLater.cs" company="clarity-ventures.com">
// Copyright (c) 2020-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the checkout pay by bill me later class</summary>
namespace Clarity.Ecommerce.Models
{
    using System.ComponentModel;
    using Interfaces.Models;
    using ServiceStack;

    /// <summary>A checkout pay by bill me later.</summary>
    /// <seealso cref="ICheckoutPayByBillMeLater"/>
    public class CheckoutPayByBillMeLater : ICheckoutPayByBillMeLater
    {
        /// <inheritdoc/>
        [ApiMember(Name = nameof(CustomerPurchaseOrderNumber), DataType = "string", ParameterType = "body", IsRequired = true,
             Description = "Customer's PO Number"), DefaultValue(null)]
        public string? CustomerPurchaseOrderNumber { get; set; }
    }
}
