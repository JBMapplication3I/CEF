// <copyright file="CalendarEventService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using JetBrains.Annotations;
    using LinqToExcel.Extensions;
    using Models;
    using Providers.Emails;
    using ServiceStack;

    [PublicAPI, Authenticate]
    [Route("/CalendarEvents/GetWithAdminUsers", "POST", Summary = "Use to get a list attendees to an event", Priority = 1)]
    public class GetCalendarEventsWithAdminUsers : CalendarEventSearchModel, IReturn<CalendarEventPagedResults>
    {
    }

    [PublicAPI, Authenticate]
    [Route("/CalendarEvents/CanChangePackage/{ID}", "GET", Summary = "Check if the current user can change package", Priority = 1)]
    public class CanChangePackage : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [PublicAPI, Authenticate]
    [Route("/CalendarEvents/CurrentUser/ChangePackage", "POST", Summary = "Change package for an event", Priority = 1)]
    public class ChangePackage : IReturn<CEFActionResponse>
    {
        /// <summary>Gets or sets the identifier.</summary>
        /// <value>The identifier.</value>
        [ApiMember(Name = nameof(ID), DataType = "int", ParameterType = "Body", IsRequired = true)]
        public int ID { get; set; }

        /// <summary>Gets or sets the identifier of the product.</summary>
        /// <value>The identifier of the product.</value>
        [ApiMember(Name = nameof(ProductID), DataType = "int", ParameterType = "Body", IsRequired = true)]
        public int ProductID { get; set; }
    }

    [PublicAPI, Authenticate]
    [Route("/CalendarEvents/CurrentUser/Events", "POST", Summary = "Use to get a list attendees to an event", Priority = 1)]
    public class GetCurrentUserCalendarEvents : CalendarEventSearchModel, IReturn<CalendarEventPagedResults>
    {
    }

    public partial class CalendarEventServiceBase
    {
        public async Task<object?> Get(CanChangePackage request)
        {
            return await Workflows.CalendarEvents.CanUserChangePackageAsync(request.ID, null).ConfigureAwait(false);
        }

        public async Task<object?> Post(ChangePackage request)
        {
            var currentUser = await CurrentUserOrThrow401Async().ConfigureAwait(false);
            var ret = await Workflows.CalendarEvents.ChangePackageAsync(
                    request.ID,
                    request.ProductID,
                    currentUser.ID,
                    null)
                .ConfigureAwait(false);
            if (!ret.ActionSucceeded)
            {
                return ret;
            }
            try
            {
                var @event = await Workflows.CalendarEvents.GetAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
                await new CalendarEventsPackageChangedNotificationToBackOfficeEmail().QueueAsync(
                        contextProfileName: null,
                        to: null,
                        parameters: new()
                        {
                            ["firstName"] = currentUser.ContactFirstName,
                            ["middleName"] = currentUser.Contact?.MiddleName,
                            ["lastName"] = currentUser.ContactLastName,
                            ["tourNumber"] = @event!.CustomKey,
                            ["tourName"] = @event.Name,
                        })
                    .ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                await Logger.LogErrorAsync("PackageChangedNotifications", ex.Message, ex, null).ConfigureAwait(false);
                (ret.Messages ??= new List<string>()).Add("There was an error sending the back-office package change confirmation.");
            }
            return ret;
        }

        public async Task<object?> Post(GetCalendarEventsWithAdminUsers request)
        {
            var response = await Workflows.CalendarEvents.GetCalendarEventsWithAdminUsersAsync(request, null).ConfigureAwait(false);
            return new CalendarEventPagedResults
            {
                CurrentCount = request.Paging?.Size ?? response.Result.totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = response.Result.totalPages,
                TotalCount = response.Result.totalCount,
                Results = response.Result.results.Cast<CalendarEventModel>().ToList(),
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }

        public async Task<object?> Post(GetCurrentUserCalendarEvents request)
        {
            request.UserID = CurrentUserIDOrThrow401;
            var response = await Workflows.CalendarEvents.GetCalendarEventsWithAdminUsersAsync(request, null).ConfigureAwait(false);
            return new CalendarEventPagedResults
            {
                CurrentCount = request.Paging?.Size ?? response.Result.totalCount,
                CurrentPage = request.Paging?.StartIndex ?? 1,
                TotalPages = response.Result.totalPages,
                TotalCount = response.Result.totalCount,
                Results = response.Result.results.Cast<CalendarEventModel>().ToList(),
                Sorts = request.Sorts,
                Groupings = request.Groupings,
            };
        }
    }
}
