// <copyright file="EventService.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the event service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using JetBrains.Annotations;
    using ServiceStack;

    /// <summary>A get common keywords.</summary>
    /// <seealso cref="IReturn{Dictionary_string_int}"/>
    [PublicAPI,
        Authenticate,
        Route("/Tracking/Analytics/GetCommonKeywords", "POST",
            Summary = "Use to get the keywords searched in the website and the counts for each")]
    public partial class GetCommonKeywords : IReturn<Dictionary<string, int>>
    {
        /// <summary>Gets or sets the start date.</summary>
        /// <value>The start date.</value>
        [ApiMember(Name = nameof(StartDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? StartDate { get; set; }

        /// <summary>Gets or sets the end date.</summary>
        /// <value>The end date.</value>
        [ApiMember(Name = nameof(StartDate), DataType = "DateTime?", ParameterType = "body", IsRequired = false)]
        public DateTime? EndDate { get; set; }
    }

    public partial class EventService
    {
        public override async Task<object?> Post(UpsertEvent request)
        {
            request.IPAddress = Request.RemoteIp;
            ////request.Browser = Request.UserAgent;
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedEventDataAsync,
                    () => Workflows.Events.UpsertAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        public override async Task<object?> Post(CreateEvent request)
        {
            request.IPAddress = Request.RemoteIp;
            /*request.Browser = Request.UserAgent;*/
            if (IsAuthenticated)
            {
                request.UserID = int.Parse(GetSession().UserAuthId);
            }
            return await DoClearCacheActionAndReturnResult(
                    ClearCachedEventDataAsync,
                    () => Workflows.Events.CreateAsync(request, contextProfileName: null))
                .ConfigureAwait(false);
        }

        public async Task<object?> Post(GetCommonKeywords request)
        {
            using var context = RegistryLoaderWrapper.GetContext(ServiceContextProfileName);
            return await context.Events
                .FilterByActive(true)
                .FilterByName("Product Catalog Search Keyword", true)
                .FilterByUpdatedOrCreatedDate(request.StartDate, request.EndDate)
                .GroupBy(x => x.Keywords)
                .ToDictionaryAsync(x => x.Key, x => x.Count())
                .ConfigureAwait(false);
        }
    }
}
