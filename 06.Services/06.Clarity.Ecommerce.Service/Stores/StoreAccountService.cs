// <copyright file="StoreAccountService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the store account service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, Authenticate,
     Route("/Stores/StoreAccounts/ByAccountID", "POST",
         Summary = "Get stores by AccountID")]
    public class GetStoreAccountsByAccount : StoreAccountSearchModel, IReturn<StoreAccountPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetStoreAccountsByAccount request)
        {
            return await GetPagedResultsAsync<IStoreAccountModel, StoreAccountModel, IStoreAccountSearchModel, StoreAccountPagedResults>(
                request, false, Workflows.StoreAccounts)
                .ConfigureAwait(false);
        }
    }
}
