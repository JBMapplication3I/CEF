// <copyright file="TaxService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the tax service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Framework.Taxing
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using Service;
    using ServiceStack;

    [PublicAPI, UsedInAdmin,
        Authenticate,
        Route("/Tax/TestConnection", "GET", Summary = "Test the connection to the tax provider")]
    public partial class TestConnection : IReturn<CEFActionResponse>
    {
    }

    [PublicAPI]
    public class TaxService : ClarityEcommerceServiceBase
    {
        public async Task<object?> Get(TestConnection _)
        {
            return (await GetTaxProviderAsync().ConfigureAwait(false))!.TestServiceAsync();
        }
    }
}
