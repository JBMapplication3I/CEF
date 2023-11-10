// <copyright file="DistrictService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the district service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI, UsedInStorefront, UsedInAdmin,
        Route("/Geography/District/GetDistrictByNameAndRegionID", "GET",
            Summary = "Gets the district by the district name and region id")]
    public class GetDistrictByNameAndRegionID : IReturn<DistrictModel>
    {
        [ApiMember(Name = nameof(RegionID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int RegionID { get; set; }

        [ApiMember(Name = nameof(Name), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string Name { get; set; } = null!;
    }

    public partial class DistrictService
    {
        public async Task<object?> Get(GetDistrictByNameAndRegionID request)
        {
            return await Workflows.Districts.GetDistrictByNameAndRegionIDAsync(request.RegionID, request.Name, null).ConfigureAwait(false);
        }
    }
}
