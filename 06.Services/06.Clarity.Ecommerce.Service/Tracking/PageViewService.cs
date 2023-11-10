// <copyright file="PageViewService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;

    public partial class PageViewService
    {
        public override async Task<object?> Post(UpsertPageView request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPageViewDataAsync,
                    () => Workflows.PageViews.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        public override async Task<object?> Post(CreatePageView request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedPageViewDataAsync,
                    () => Workflows.PageViews.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
    }
}
