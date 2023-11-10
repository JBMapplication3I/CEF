// <copyright file="CountryService.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the country service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>A ServiceStack Route to check country exists by Code.</summary>
    /// <seealso cref="IReturn{T}"/>
    [PublicAPI,
        Route("/Geography/Country/Exists/Code", "GET", Priority = 1,
            Summary = "Check if this Code exists and return the id if it does (null if it does not)")]
    public partial class CheckCountryExistsByCode : IReturn<int?>
    {
        [ApiMember(Name = nameof(Code), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The Country Code to look up")]
        public string Code { get; set; } = null!;
    }

    public partial class CountryService
    {
        protected override List<string> AdditionalUrnIDs => new()
        {
            UrnId.Create<CheckCountryExistsByCode>(string.Empty),
        };

        public virtual async Task<object?> Get(CheckCountryExistsByCode request)
        {
            // TODO: Cached Research
            return await Workflows.Countries.CheckExistsByCodeAsync(request.Code, contextProfileName: null).ConfigureAwait(false);
        }
    }
}
