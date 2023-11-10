// <copyright file="BrandSiteDomainService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the brand site domain service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using ServiceStack;

    [Authenticate]
    [Route("/Stores/BrandSiteDomains/BySiteDomainID", "POST", Summary = "Get brands by SiteDomainID")]
    public class GetBrandSiteDomainsBySiteDomainID : BrandSiteDomainSearchModel, IReturn<BrandSiteDomainPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetBrandSiteDomainsBySiteDomainID request)
        {
            return await GetPagedResultsAsync<IBrandSiteDomainModel, BrandSiteDomainModel, IBrandSiteDomainSearchModel, BrandSiteDomainPagedResults>(
                    request,
                    false,
                    Workflows.BrandSiteDomains)
                .ConfigureAwait(false);
        }
    }
}
