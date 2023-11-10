// <copyright file="CreateInvoiceForSampleRequest.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the create invoice for sample request class</summary>
#pragma warning disable CS1584,CS0081,CS1658 // XML comment has syntactically incorrect cref attribute
namespace Clarity.Ecommerce.Providers.SampleRequestHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    /// <summary>A create invoice for sample request.</summary>
    /// <seealso cref="ImplementsIDBase"/>
    /// <seealso cref="IReturn{CEFActionResponse{SalesInvoiceModel}}"/>
    [PublicAPI, UsedInAdmin,
     Authenticate,
     Route("/Providers/Sampling/Actions/CreateInvoiceFor/{ID}", "PATCH",
         Summary = "Generates an Invoice with the same information as this Order with the Balance Due amount. An email"
             + " notification will be sent to the customer. No status change will occur.")]
    public class CreateInvoiceForSampleRequest : ImplementsIDBase, IReturn<CEFActionResponse<SalesInvoiceModel>>
    {
    }
}
