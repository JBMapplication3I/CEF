// <copyright file="AdZoneAccessService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the AdZoneAccess service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;

    [Authenticate,
     Route("/Advertising/AdZoneAccess/UserID/{ID}", "GET", Summary = "Use to get all Zones by userID based on AdZoneAccess")]
    public class GetAdZoneAccessByUserID : ImplementsIDBase, IReturn<List<AdZoneAccessModel>>
    {
    }

    [Authenticate,
     Route("/Advertising/AdZoneAccess/CurrentUser", "GET", Summary = "Use to get all Zones by current user based on AdZoneAccess")]
    public class GetAdZoneAccessByCurrentUser : IReturn<List<AdZoneAccessModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetAdZoneAccessByUserID request)
        {
            return await Workflows.AdZoneAccesses.GetByUserIDAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(GetAdZoneAccessByCurrentUser _)
        {
            return await Workflows.AdZoneAccesses.GetByUserIDAsync(CurrentUserIDOrThrow401, null).ConfigureAwait(false);
        }
    }
}
