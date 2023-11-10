// <copyright file="SalesInvoiceItemTarget.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the SalesInvoice item target class</summary>
// ReSharper disable MissingBlankLines
namespace Clarity.Ecommerce.Interfaces.DataModel
{
    public interface ISalesInvoiceItemTarget : ISalesItemTargetBase
    {
    }
}

namespace Clarity.Ecommerce.DataModel
{
    using Interfaces.DataModel;

    [SqlSchema("Invoicing", "SalesInvoiceItemTarget")]
    public class SalesInvoiceItemTarget
        : SalesItemTargetBase<SalesInvoiceItem>,
            ISalesInvoiceItemTarget
    {
    }
}
