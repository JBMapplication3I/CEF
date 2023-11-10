// <copyright file="VisitService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visit service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;

    public partial class VisitService
    {
        public override async Task<object?> Post(UpsertVisit request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitDataAsync,
                    () => Workflows.Visits.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        public override async Task<object?> Post(CreateVisit request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitDataAsync,
                    () => Workflows.Visits.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
    }
}
