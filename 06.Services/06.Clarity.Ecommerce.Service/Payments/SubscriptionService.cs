// <copyright file="SubscriptionService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the subscription service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate,
        Route("/Payments/CurrentAccount/Subscriptions", "POST",
            Summary = "Use to get subscriptions by Current Account.")]
    public class GetCurrentAccountSubscriptions : SubscriptionSearchModel, IReturn<SubscriptionPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetCurrentAccountSubscriptions request)
        {
            request.AccountID = await LocalAdminAccountIDOrThrow401Async(
                    request.AccountID ?? CurrentAccountIDOrThrow401)
                .ConfigureAwait(false);
            var (results, totalPages, totalCount) = await Workflows.Subscriptions.SearchAsync(
                    request,
                    request.AsListing,
                    contextProfileName: null)
                .ConfigureAwait(false);
            return new SubscriptionPagedResults
            {
                Results = results.Cast<SubscriptionModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }
    }

    [PublicAPI,
        Authenticate,
        Route("/Payments/CurrentUser/Subscriptions", "POST",
            Summary = "Use to get subscriptions by Current User as a dropdown-appropriate listing.")]
    public partial class GetCurrentUserSubscriptions : SubscriptionSearchModel, IReturn<SubscriptionPagedResults>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Post(GetCurrentUserSubscriptions request)
        {
            request.UserID = CurrentUserIDOrThrow401;
            var (results, totalPages, totalCount) = await Workflows.Subscriptions.SearchAsync(
                    search: request,
                    asListing: request.AsListing,
                    contextProfileName: null)
                .ConfigureAwait(false);
            return new SubscriptionPagedResults
            {
                Results = results.Cast<SubscriptionModel>().ToList(),
                CurrentCount = request.Paging?.Size ?? totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = totalPages,
                TotalCount = totalCount,
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }
    }
}
