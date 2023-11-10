// <copyright file="UserEventAttendanceService.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance service class</summary>
#nullable enable
namespace Clarity.Ecommerce.Service
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces.Models;
    using LinqToExcel.Extensions;
    using Models;
    using ServiceStack;
    using Utilities;

    /// <summary>Get attendees of an event.</summary>
    /// <seealso cref="UserEventAttendanceSearchModel"/>
    [Authenticate]
    [Route("/CalendarEvents/Event/Attendees", "POST",
        Summary = "Use to get a list attendees to an event", Priority = 1)]
    public class GetCalendarEventAttendeesByEvent : UserEventAttendanceSearchModel, IReturn<CEFActionResponse<List<UserEventAttendanceModel>>>
    {
    }

    [Authenticate]
    [Route("/CalendarEvents/Event/CancelAttendance", "POST",
        Summary = "Use to cancel the attendance of the event", Priority = 1)]
    public class CancelAttendance : ImplementsIDBase, IReturn<CEFActionResponse>
    {
    }

    [Authenticate]
    [Route("/CalendarEvents/Event/{ID}/Attendees/ExportToExcel", "GET",
        Summary = "Use to export users for a given event")]
    public class GetEventUsersAsExcelDoc : ImplementsIDBase, IReturn<DownloadFileResult>
    {
    }

    [Authenticate]
    [Route("/CalendarEvents/CurrentUser/EventList", "POST", Summary = "Events for current user")]
    public class GetEventListingForCurrentUser : UserEventAttendanceSearchModel, IReturn<CEFActionResponse<List<UserEventAttendanceModel>>>
    {
    }

    public partial class UserEventAttendanceService
    {
        public async Task<object?> Post(CancelAttendance request)
        {
            Contract.RequiresValidID(request.ID);
            return await Workflows.UserEventAttendances.CancelAttendanceAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
        }

        public async Task<object?> Post(GetCalendarEventAttendeesByEvent request)
        {
            Contract.RequiresValidIDOrKey(request.CalendarEventID, request.CalendarEventKey);
            var response = await Workflows.UserEventAttendances.GetEventAttendeesByEventIDAsync(request, contextProfileName: null).ConfigureAwait(false);
            return response.ChangeCEFARListType<IUserEventAttendanceModel, UserEventAttendanceModel>();
        }

        public async Task<object?> Post(GetEventListingForCurrentUser request)
        {
            request.UserID = CurrentUserIDOrThrow401;
            var response = await Workflows.UserEventAttendances.GetEventAttendeesByEventIDAsync(request, contextProfileName: null).ConfigureAwait(false);
            return new CEFActionResponse<List<UserEventAttendanceModel>>(response, response.Result.Cast<UserEventAttendanceModel>().ToList());
        }

        public async Task<object?> Get(GetEventUsersAsExcelDoc request)
        {
            Contract.RequiresValidID(request.ID);
            var provider = RegistryLoaderWrapper.GetFilesProvider(contextProfileName: null);
            if (provider is null)
            {
                throw new("Failed to build export file");
            }
            var ds = await Workflows.UserEventAttendances.ExportToExcelAsync(request.ID, contextProfileName: null).ConfigureAwait(false);
            var fullPath = await provider.GetFileSaveRootPathFromFileEntityTypeAsync(Enums.FileEntityType.StoredFileUser).ConfigureAwait(false);
            if (!ExportToExcel.CreateExcelFile.CreateExcelDocument(ds!, fullPath).ActionSucceeded)
            {
                throw new("Failed to build export file");
            }
            var fileName = $"EventUsersExport-{DateExtensions.GenDateTime:yyyy-MM-dd-HH.mm.ss}.xlsx";
            return new DownloadFileResult
            {
                DownloadUrl = await provider.GetFileUrlAsync(fileName, Enums.FileEntityType.StoredFileUser).ConfigureAwait(false),
            };
        }
    }
}
