// <copyright file="StoreSubscriptionService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store Subscription service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using Models;
    using ServiceStack;

    [Authenticate,
     Route("/Stores/StoreSubscriptions/BySubscriptionID", "POST",
        Summary = "Get stores by SubscriptionID")]
    public class GetStoreSubscriptionsBySubscriptionID : StoreSubscriptionSearchModel, IReturn<StoreSubscriptionPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetStoreSubscriptionsBySubscriptionID request)
        {
            return await GetPagedResultsAsync<IStoreSubscriptionModel, StoreSubscriptionModel, IStoreSubscriptionSearchModel, StoreSubscriptionPagedResults>(
                    request,
                    false,
                    Workflows.StoreSubscriptions)
                .ConfigureAwait(false);
        }
    }
}
