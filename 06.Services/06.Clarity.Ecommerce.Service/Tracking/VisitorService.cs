// <copyright file="VisitorService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the visitor service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;

    public partial class VisitorService
    {
        public override async Task<object?> Post(UpsertVisitor request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitorDataAsync,
                    () => Workflows.Visitors.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        public override async Task<object?> Post(CreateVisitor request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedVisitorDataAsync,
                    () => Workflows.Visitors.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }
    }
}
