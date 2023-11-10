// <copyright file="ProductReviewService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the product review service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Products/ReviewSummary/ByProductID/{ID}", "GET",
            Summary = "Get product reviews summary information")]
    public partial class GetProductReview : ImplementsIDBase, IReturn<ProductReviewInformationModel>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetProductReview request)
        {
            return await UseLastModifiedFor304OrCreateAndReturnCachedResultAsync(
                    request,
                    () => Workflows.Products.GetProductReviewInformationLastModifiedAsync(request.ID, null),
                    () => Workflows.Products.GetProductReviewInformationAsync(request.ID, null))
                .ConfigureAwait(false);
        }
    }
}
