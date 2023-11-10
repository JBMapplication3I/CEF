// <copyright file="ReviewService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the review service class</summary>
#nullable enable
#pragma warning disable AsyncFixer01 // Unnecessary async/await usage
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Authenticate, RequiredPermission("Reviews.Review.Update"),
        Route("/Reviews/Review/Approve", "PATCH", Summary = "Approve a review")]
    public partial class ApproveReview : ImplementsIDOnBodyBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI,
        Authenticate, RequiredPermission("Reviews.Review.Update"),
        Route("/Reviews/Review/Unapprove", "PATCH", Summary = "Unapprove a review")]
    public partial class UnapproveReview : ImplementsIDOnBodyBase, IReturn<CEFActionResponse>
    {
    }

    public partial class ReviewService
    {
        protected override List<string> AdditionalUrnIDs => new()
        {
            UrnId.Create<GetProductReview>(string.Empty),
        };

        public override async Task<object?> Post(CreateReview request)
        {
            request.SubmittedByUserID = CurrentUserIDOrThrow401;
            return await base.Post(request).ConfigureAwait(false);
        }

        public async Task<object?> Patch(ApproveReview request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedReviewDataAsync,
                    () => Workflows.Reviews.ApproveAsync(request.ID, CurrentUserIDOrThrow401, ServiceContextProfileName))
                .ConfigureAwait(false);
        }

        public async Task<object?> Patch(UnapproveReview request)
        {
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedReviewDataAsync,
                    () => Workflows.Reviews.UnapproveAsync(request.ID, CurrentUserIDOrThrow401, ServiceContextProfileName))
                .ConfigureAwait(false);
        }
    }
}
