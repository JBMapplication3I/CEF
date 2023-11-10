// <copyright file="SettingGroupService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting group service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using Models;
    using ServiceStack;

    [Route("/Structure/Setting/Group/{ID}", "GET", Summary = "Use to get a specific setting group")]
    public class GetSettingGroupsByID : ImplementsIDBase, IReturn<SettingGroupModel>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetSettingGroupsByID request)
        {
            return await Workflows.SettingGroups.GetAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }
    }
}
