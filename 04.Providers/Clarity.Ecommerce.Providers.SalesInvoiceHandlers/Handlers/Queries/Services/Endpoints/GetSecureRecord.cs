// <copyright file="GetSecureRecord.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the get secure record class</summary>
namespace Clarity.Ecommerce.Providers.SalesInvoiceHandlers.Queries.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A get secure sales invoice.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{SalesInvoiceModel}"/>
    [PublicAPI, UsedInStorefront,
        Authenticate,
        Route("/Providers/Invoicing/Queries/Secured/{ID}", "GET", Priority = 1,
            Summary = "Use to get a specific sales invoice and check for ownership by the current Account.")]
    public class GetSecureSalesInvoice : ImplementsIDBase, IReturn<SalesInvoiceModel>
    {
    }
}
