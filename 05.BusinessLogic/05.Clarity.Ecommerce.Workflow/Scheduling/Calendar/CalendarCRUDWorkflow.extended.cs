// <copyright file="CalendarCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the Calendar Workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using Models;
    using Utilities;

    public partial class CalendarWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> GetCalendarIDForAccountAsync(
            int accountID,
            string? contextProfileName)
        {
            Contract.RequiresValidID(accountID);
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await GetCalendarIDForAccountAsync(
                    accountID,
                    context)
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<int>> GetCalendarIDForAccountAsync(
            int accountID,
            IClarityEcommerceEntities context)
        {
            var calendarID = await context.Calendars
                    .FilterByActive(true)
                    .FilterCalendarsByAccountID(accountID)
                    .Select(x => x.ID)
                    .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (!Contract.CheckValidID(calendarID))
            {
                var calendar = RegistryLoaderWrapper.GetInstance<ICalendar>();
                calendar.AccountID = accountID;
                calendar.Active = true;
                calendar.CreatedDate = DateExtensions.GenDateTime;
                context.Calendars.Add((Calendar)calendar);
                if (!await context.SaveUnitOfWorkAsync(true))
                {
                    return CEFAR.FailingCEFAR<int>("ERROR! Something about creating and saving the calendar failed.");
                }
                calendarID = calendar.ID;
            }
            return calendarID.WrapInPassingCEFAR();
        }
    }
}
