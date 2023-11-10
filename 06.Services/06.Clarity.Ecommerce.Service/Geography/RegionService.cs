// <copyright file="RegionService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the region service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Geography/Region/RestrictedRegionCheck", "GET",
            Summary = "Validates if the region is in a restricted region.")]
    public partial class RestrictedRegionCheck : ImplementsNameBase, IReturn<CEFActionResponse>
    {
        [ApiMember(Name = nameof(CountryID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int CountryID { get; set; }

        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string Code { get; set; } = null!;

        [ApiMember(Name = nameof(RegionID), DataType = "int", ParameterType = "query", IsRequired = true)]
        public int RegionID { get; set; }
    }

    /// <summary>A ServiceStack Route to check region exists by Code.</summary>
    /// <seealso cref="IReturn{T}"/>
    [PublicAPI,
        Route("/Geography/Region/Exists/Code", "GET", Priority = 1,
            Summary = "Check if this Code exists and return the id if it does (null if it does not)")]
    public partial class CheckRegionExistsByCode : IReturn<int?>
    {
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "query", IsRequired = true)]
        public string Code { get; set; } = null!;
    }

    public partial class RegionService
    {
        public async Task<object?> Get(RestrictedRegionCheck request)
        {
            // TODO: Cached Research
            return await Workflows.Regions.ValidateRestrictedRegionAsync(request.CountryID, request.RegionID, null).ConfigureAwait(false);
        }

        public async Task<object?> Get(CheckRegionExistsByCode request)
        {
            // TODO: Cached Research
            return await Workflows.Regions.CheckExistsByCodeAsync(request.Code, contextProfileName: null).ConfigureAwait(false);
        }
    }
}
