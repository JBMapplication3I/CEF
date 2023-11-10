// <copyright file="SetSalesQuotesAsExpired.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the set sales quotes as expired class</summary>
namespace Clarity.Ecommerce.Providers.SalesQuoteHandlers.Actions.Services.Endpoints
{
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>A set sales quotes as expired.</summary>
    /// <seealso cref="IReturn{Boolean}"/>
    [PublicAPI,
     Authenticate,
     Route("/Providers/Quoting/Actions/SetRecordsExpired", "GET",
        Summary = "Use to Set Quotes past specified day length as expired")]
    public class SetSalesQuotesAsExpired : IReturn<bool>
    {
    }
}
