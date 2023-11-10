// <copyright file="PhonePrefixLookupService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the phone prefix lookup service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using LinqToExcel.Extensions;
    using Models;
    using ServiceStack;

    [PublicAPI,
        Route("/Geography/PhonePrefixLookups/ReversePhonePrefixToCityRegionCountry", "GET",
            Summary = "Use to get geographical data from the phone number")]
    public partial class ReversePhonePrefixToCityRegionCountry : IReturn<PhonePrefixLookupPagedResults>
    {
        [ApiMember(Name = nameof(Prefix), DataType = "string", ParameterType = "query", IsRequired = true,
            Description = "The prefix, will be limited to 5 characters ignoring any symbols or spaces.")]
        public string Prefix { get; set; } = null!;
    }

    public partial class PhonePrefixLookupService
    {
        public async Task<object?> Post(ReversePhonePrefixToCityRegionCountry request)
        {
            // TODO: Cached Research
            var retVal = new PhonePrefixLookupPagedResults();
            var (results, totalPages, totalCount) = await Workflows.PhonePrefixLookups.ReversePhonePrefixToCityRegionCountryAsync(
                    request.Prefix,
                    null)
                .ConfigureAwait(false);
            retVal.Results = results?.Cast<PhonePrefixLookupModel>().ToList();
            retVal.TotalCount = totalCount;
            retVal.TotalPages = totalPages;
            retVal.CurrentPage = 1;
            retVal.CurrentCount = 1;
            return retVal;
        }
    }
}
