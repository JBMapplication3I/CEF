// <copyright file="SettingService.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the setting service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Structure/Setting/ByName/{Name}", "GET",
            Summary = "Use to get a specific setting")]
    public partial class GetSettingByName : ImplementsNameBase, IReturn<SettingModel>
    {
    }

    [PublicAPI,
        Route("/Structure/Setting/ByGroupName/{Name}", "GET",
            Summary = "Use to get a specific setting")]
    public partial class GetSettingsByGroupName : ImplementsNameBase, IReturn<List<SettingModel>>
    {
    }

    public partial class CEFSharedService
    {
        public async Task<object?> Get(GetSettingByName request)
        {
            return (await Workflows.Settings.GetSettingByTypeNameAsync(request.Name, ServiceContextProfileName).ConfigureAwait(false)).FirstOrDefault();
        }

        public async Task<object?> Get(GetSettingsByGroupName request)
        {
            return await Workflows.Settings.GetSettingsByGroupNameAsync(request.Name, ServiceContextProfileName).ConfigureAwait(false);
        }
    }
}
