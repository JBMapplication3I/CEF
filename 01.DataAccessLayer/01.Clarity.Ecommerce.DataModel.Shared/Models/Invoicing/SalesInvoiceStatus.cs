// <copyright file="SalesInvoiceStatus.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the sales invoice status class</summary>
#nullable enable
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesInvoiceStatus : IStatusableBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Invoicing", "SalesInvoiceStatus")]
    public class SalesInvoiceStatus : StatusableBase, ISalesInvoiceStatus
    {
    }
}
