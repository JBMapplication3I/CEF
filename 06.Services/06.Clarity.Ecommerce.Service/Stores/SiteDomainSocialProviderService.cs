// <copyright file="SiteDomainSocialProviderService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the site domain social provider service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, Authenticate]
    [Route("/Stores/SiteDomainSocialProviders/BySocialProviderID", "POST", Summary = "Get site domains by SocialProviderID")]
    public class GetSiteDomainSocialProvidersBySocialProviderID : SiteDomainSocialProviderSearchModel, IReturn<SiteDomainSocialProviderPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetSiteDomainSocialProvidersBySocialProviderID request)
        {
            return await GetPagedResultsAsync<ISiteDomainSocialProviderModel, SiteDomainSocialProviderModel, ISiteDomainSocialProviderSearchModel, SiteDomainSocialProviderPagedResults>(
                request, false, Workflows.SiteDomainSocialProviders)
                .ConfigureAwait(false);
        }
    }
}
