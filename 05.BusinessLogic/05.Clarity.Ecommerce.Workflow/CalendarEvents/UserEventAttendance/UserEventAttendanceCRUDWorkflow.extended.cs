// <copyright file="UserEventAttendanceCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2018-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the user event attendance workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Providers.Emails;
    using Utilities;

    public partial class UserEventAttendanceWorkflow
    {
        /// <inheritdoc/>
        public override async Task<CEFActionResponse<int>> CreateAsync(
            IUserEventAttendanceModel model,
            string? contextProfileName)
        {
            // Create User Attendance
            var retVal = await base.CreateAsync(model, contextProfileName).ConfigureAwait(false);
            if (!CEFConfigDictionary.UserEventAttendancesSendGroupLeaderEmailsOnCreate)
            {
                return retVal;
            }
            ICalendarEventModel? @event = null;
            var newUser = model.Slave;
            if (Contract.CheckValidID(model.MasterID))
            {
                @event = await Workflows.CalendarEvents.GetAsync(model.MasterID, contextProfileName).ConfigureAwait(false);
            }
            if (@event is null)
            {
                return CEFAR.FailingCEFAR<int>("ERROR! Could not locate event");
            }
            if (newUser == null && model.SlaveID > 0)
            {
                newUser = await Workflows.Users.GetAsync(model.SlaveID, contextProfileName).ConfigureAwait(false);
            }
            // Send email to Group Leader to inform them of the new registration
            try
            {
                // Get Group Leaders
                var groupLeadersIDs = await GetGroupLeadersIDsAsync(model.MasterID, contextProfileName).ConfigureAwait(false);
                foreach (var groupLeaderID in groupLeadersIDs)
                {
                    var groupLeader = await Workflows.Users.GetAsync(groupLeaderID, contextProfileName).ConfigureAwait(false);
                    if (groupLeader is null)
                    {
                        continue;
                    }
                    try
                    {
                        await new CalendarEventsNewRegistrationNotificationToGroupLeaderEmail().QueueAsync(
                                contextProfileName: contextProfileName,
                                to: null,
                                parameters: new()
                                {
                                    ["firstName"] = groupLeader.ContactFirstName,
                                    ["middleName"] = groupLeader.Contact?.MiddleName,
                                    ["lastName"] = groupLeader.ContactLastName,
                                    ["tourNumber"] = @event!.CustomKey,
                                    ["tourName"] = @event.Name,
                                    ["registeredFirstName"] = newUser!.ContactFirstName,
                                    ["registeredLastName"] = newUser.ContactLastName,
                                    ["registeredCity"] = newUser.Contact?.Address?.City,
                                    ["registeredState"] = newUser.Contact?.Address?.RegionCode,
                                })
                            .ConfigureAwait(false);
                    }
                    catch
                    {
                        // Do Nothing
                    }
                    // Check new capacity on the event and notify the admin if the max capacity is reached.
                    // Send the email from here because there are different ways to subscribe someone to an event
                    // (Checkout, Admin, connect...)
                    if (@event!.CurrentAttendeesCount >= @event!.MaxAttendees)
                    {
                        try
                        {
                            await new CalendarEventsMaxCapacityNotificationToGroupLeaderEmail().QueueAsync(
                                    contextProfileName: contextProfileName,
                                    to: null,
                                    parameters: new()
                                    {
                                        ["tourNumber"] = @event!.CustomKey,
                                        ["tourName"] = @event.Name,
                                        ["tourMaxCapacity"] = @event.MaxAttendees,
                                    })
                                .ConfigureAwait(false);
                        }
                        catch
                        {
                            // Do nothing
                        }
                    }
                }
            }
            catch
            {
                // Do nothing
            }
            return retVal;
        }

        /// <inheritdoc/>
        public async Task<List<int>> GetGroupLeadersIDsAsync(int eventID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.UserEventAttendances
                .AsNoTracking()
                .FilterUserEventAttendancesByCalendarEventID(eventID)
                .FilterByActive(true)
                .FilterByTypeKey<UserEventAttendance, UserEventAttendanceType>("SPIRITUAL")
                .Select(x => x.SlaveID)
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CancelAttendanceAsync(int id, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var userEventAttendance = context.UserEventAttendances.Single(x => x.ID == id);
            userEventAttendance.Active = false;
            userEventAttendance.UpdatedDate = DateExtensions.GenDateTime;
            var retVal = await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            var user = await Workflows.Users.GetAsync(userEventAttendance.SlaveID, contextProfileName).ConfigureAwait(false);
            Contract.RequiresValidID(user?.ID);
            var @event = await Workflows.CalendarEvents.GetAsync(userEventAttendance.MasterID, contextProfileName).ConfigureAwait(false);
            Contract.RequiresValidID(@event?.ID);
            // Email to admin
            try
            {
                await new CalendarEventsPassengerCancellationNotificationToBackOfficeEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["firstName"] = user!.ContactFirstName,
                            ["middleName"] = user.Contact?.MiddleName,
                            ["lastName"] = user.ContactLastName,
                            ["tourNumber"] = @event!.CustomKey,
                            ["tourName"] = @event.Name,
                        })
                    .ConfigureAwait(false);
            }
            catch
            {
                // Do Nothing
            }
            // Email to Group Leader
            var groupLeadersIds = await GetGroupLeadersIDsAsync(@event!.ID, contextProfileName).ConfigureAwait(false);
            foreach (var groupLeaderID in groupLeadersIds)
            {
                var groupLeader = await Workflows.Users.GetAsync(groupLeaderID, contextProfileName).ConfigureAwait(false);
                if (groupLeader is null)
                {
                    continue;
                }
                await new CalendarEventsPassengerCancellationNotificationToGroupLeaderEmail().QueueAsync(
                        contextProfileName: contextProfileName,
                        to: null,
                        parameters: new()
                        {
                            ["firstName"] = groupLeader.ContactFirstName,
                            ["middleName"] = groupLeader.Contact?.MiddleName,
                            ["lastName"] = groupLeader.ContactLastName,
                            ["tourNumber"] = @event.CustomKey,
                            ["tourName"] = @event.Name,
                            ["registeredFirstName"] = user!.ContactFirstName,
                            ["registeredLastName"] = user.ContactLastName,
                            ["registeredCity"] = user.Contact?.Address?.City,
                            ["registeredState"] = user.Contact?.Address?.RegionCode,
                        })
                    .ConfigureAwait(false);
            }
            return new(retVal);
        }

        /// <inheritdoc/>
        public override async Task<IUserEventAttendanceModel?> GetAsync(int id, string? contextProfileName)
        {
            var model = await base.GetAsync(id, contextProfileName).ConfigureAwait(false);
            if (model == null)
            {
                return null;
            }
            var so = GetOrderByUserAndEvent(model.SlaveID, model.MasterID, contextProfileName);
            model.SalesOrder = so.Result;
            return model;
        }

        /// <inheritdoc/>
        public override async Task<IUserEventAttendanceModel?> GetAsync(string key, string? contextProfileName)
        {
            var model = await base.GetAsync(key, contextProfileName).ConfigureAwait(false);
            if (model == null)
            {
                return null;
            }
            var so = GetOrderByUserAndEvent(model.SlaveID, model.MasterID, contextProfileName);
            model.SalesOrder = so.Result;
            return model;
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<List<IUserEventAttendanceModel>>> GetEventAttendeesByEventIDAsync(
            IUserEventAttendanceSearchModel request,
            string? contextProfileName)
        {
            try
            {
                using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
                var eventAttendees = context.UserEventAttendances
                    .FilterByActive(request.Active)
                    .FilterUserEventAttendancesByUserID(request.UserID)
                    .FilterUserEventAttendancesByCalendarEventID(request.CalendarEventID)
                    .FilterUserEventAttendancesByCalendarEventKey(request.CalendarEventKey)
                    .Select(x => new
                    {
                        x.ID,
                        x.Active,
                        x.CreatedDate,
                        x.SlaveID,
                        User = new
                        {
                            ContactFirstName = x.Slave!.Contact!.FirstName,
                            ContactLastName = x.Slave.Contact.LastName,
                            x.Slave.Email,
                            x.Slave.JsonAttributes,
                            PhoneNumber = x.Slave.Contact.Phone1,
                            x.Slave.StatusID,
                            x.Slave.TypeID,
                        },
                        x.MasterID,
                        CalendarEvent = new
                        {
                            x.Master!.Name,
                            x.Master.StartDate,
                            x.Master.EndDate,
                            x.Master.CustomKey,
                            x.Master.EventDuration,
                        },
                        Date = x.Master.StartDate,
                        x.HasAttended,
                        x.TypeID,
                        x.CustomKey,
                    })
                    .AsEnumerable()
                    .Select(x => new UserEventAttendanceModel
                    {
                        ID = x.ID,
                        Active = x.Active,
                        CreatedDate = x.CreatedDate,
                        SlaveID = x.SlaveID,
                        Slave = new()
                        {
                            ContactFirstName = x.User.ContactFirstName,
                            ContactLastName = x.User.ContactLastName,
                            Email = x.User.Email,
                            SerializableAttributes = x.User.JsonAttributes.DeserializeAttributesDictionary(),
                            PhoneNumber = x.User.PhoneNumber,
                            StatusID = x.User.StatusID,
                            TypeID = x.User.TypeID,
                        },
                        MasterID = x.MasterID,
                        Master = new()
                        {
                            CustomKey = x.CalendarEvent.CustomKey,
                            StartDate = x.CalendarEvent.StartDate,
                            EndDate = x.CalendarEvent.EndDate,
                            EventDuration = x.CalendarEvent.EventDuration,
                            Name = x.CalendarEvent.Name,
                            Dates = $"{x.CalendarEvent.StartDate:MM/dd/yyyy} - {x.CalendarEvent.EndDate:MM/dd/yyyy}",
                        },
                        Date = x.Date,
                        HasAttended = x.HasAttended,
                        TypeID = x.TypeID,
                    })
                    .ToList<IUserEventAttendanceModel>();
                List<IUserEventAttendanceModel> validAttendees = new();
                var provider = RegistryLoaderWrapper.GetSalesInvoiceQueriesProvider(contextProfileName);
                if (provider is not null)
                {
                    foreach (var attendee in eventAttendees.Where(x => PreSalesOrderFiltering(x, request)))
                    {
                        var response = await provider.GetRecordByUserAndEventAsync(
                                userID: attendee.SlaveID,
                                calendarEventID: attendee.MasterID,
                                contextProfileName: contextProfileName)
                            .ConfigureAwait(false);
                        if (!response.ActionSucceeded || response.Result == null)
                        {
                            continue;
                        }
                        var salesInvoice = response.Result;
                        ////if (!PostSalesOrderFiltering(attendee, request, invoice)) { continue; }
                        if (!PostSalesInvoiceFiltering(attendee, request, salesInvoice))
                        {
                            continue;
                        }
                        validAttendees.Add(attendee);
                    }
                }
                return validAttendees.WrapInPassingCEFAR()!;
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR<List<IUserEventAttendanceModel>>(ex.Message);
            }
        }

        ////private static bool PostSalesOrderFiltering(
        ////    IUserEventAttendanceModel attendee, IUserEventAttendanceSearchModel request, ISalesOrderModel so)
        ////{
        ////    var product = so?.SalesItems?.FirstOrDefault(si => si.Product.Name.Contains("by"));
        ////    if (product == null) { return false; }
        ////    attendee.EventPackageType = product.Name
        ////        .Substring(0, product.Name.IndexOf("by", StringComparison.OrdinalIgnoreCase) - 1);
        ////    var paidSum = so.AssociatedSalesInvoices?.Sum(x => x.SalesInvoice.Totals.Total);
        ////    attendee.UserEventTotal = so.Totals.Total;
        ////    attendee.Balance = attendee.UserEventTotal - paidSum;
        ////    return !request.IsPaidInFull.HasValue
        ////        || (!request.IsPaidInFull.Value || attendee.Balance <= 0.00m)
        ////        && (request.IsPaidInFull.Value || attendee.Balance > 0.00m);
        ////}

        /// <inheritdoc/>
        public async Task<DataSet?> ExportToExcelAsync(int eventID, string? contextProfileName)
        {
            var eventAttendance = await GetEventAttendeesByEventIDAsync(
                    new UserEventAttendanceSearchModel { ID = eventID },
                    contextProfileName)
                .ConfigureAwait(false);
            if (eventAttendance?.Result is null)
            {
                return null;
            }
            return ExportToExcelDataSet(eventAttendance.Result);
        }

        /// <summary>Export to excel data set.</summary>
        /// <param name="users">The users.</param>
        /// <returns>A DataSet.</returns>
        private static DataSet ExportToExcelDataSet(IEnumerable<IUserEventAttendanceModel> users)
        {
            var dataSet = new DataSet();
            var userTable = dataSet.Tables.Add("Users");
            // Base Properties
            userTable.Columns.Add("ID");
            userTable.Columns.Add("CustomKey");
            userTable.Columns.Add("Active");
            userTable.Columns.Add("CreatedDate");
            userTable.Columns.Add("UpdatedDate");
            // Other Properties
            userTable.Columns.Add("FirstName");
            userTable.Columns.Add("LastName");
            userTable.Columns.Add("Email");
            userTable.Columns.Add("Phone");
            userTable.Columns.Add("PackageType");
            foreach (var user in users)
            {
                var row = userTable.NewRow();
                // Base Properties
                row["ID"] = user.Slave!.ID;
                row["CustomKey"] = user.Slave.CustomKey;
                row["Active"] = user.Active;
                row["CreatedDate"] = user.Slave.CreatedDate;
                row["UpdatedDate"] = user.Slave.UpdatedDate;
                // Other Properties
                row["FirstName"] = user.Slave.ContactFirstName;
                row["LastName"] = user.Slave.ContactLastName;
                row["Email"] = user.Slave.Email;
                row["Phone"] = user.Slave.PhoneNumber;
                row["PackageType"] = user.EventPackageType;
                userTable.Rows.Add(row);
            }
            return dataSet;
        }

        /// <summary>Gets order by user and event.</summary>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="calendarEventID">   Identifier for the calendar event.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The order by user and event.</returns>
        private static CEFActionResponse<ISalesOrderModel> GetOrderByUserAndEvent(
            int userID,
            int calendarEventID,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var possibleProductIDs = context.CalendarEvents
                .AsNoTracking()
                .FilterByID(calendarEventID)
                .SelectMany(x => x.Products!
                    .Where(y => y.Active && y.Slave!.Active)
                    .Select(y => y.SlaveID))
                .Distinct()
                .ToList();
            if (possibleProductIDs.Count == 0)
            {
                return CEFAR.FailingCEFAR<ISalesOrderModel>("No Possible Products");
            }
            var salesOrder = context.SalesOrders
                .AsNoTracking()
                .FilterByActive(true)
                .FilterSalesCollectionsByUserID<SalesOrder, SalesOrderStatus, SalesOrderType, SalesOrderItem, AppliedSalesOrderDiscount, SalesOrderState, SalesOrderFile, SalesOrderContact, SalesOrderEvent, SalesOrderEventType>(userID)
                .FilterSalesCollectionsBySalesItemProductIDs<SalesOrder, SalesOrderStatus, SalesOrderType, SalesOrderItem, AppliedSalesOrderDiscount, SalesOrderState, SalesOrderFile, SalesOrderContact, SalesOrderEvent, SalesOrderEventType, AppliedSalesOrderItemDiscount, SalesOrderItemTarget>(possibleProductIDs)
                .OrderByDescending(x => x.ID)
                .Take(1)
                .SelectFirstFullSalesOrderAndMapToSalesOrderModel(contextProfileName);
            return salesOrder.WrapInPassingCEFARIfNotNull(
                "Unable to locate a sales order for this user and event");
        }

        /// <summary>Pre sales order filtering.</summary>
        /// <param name="user">   The user.</param>
        /// <param name="request">The request.</param>
        /// <returns>True if it succeeds, false if it fails.</returns>
        private static bool PreSalesOrderFiltering(
            IUserEventAttendanceModel user,
            IUserEventAttendanceSearchModel request)
        {
            if (Contract.CheckValidID(request.TypeID))
            {
                return user.TypeID == request.TypeID!.Value;
            }
            if (Contract.CheckValidKey(request.TypeKey))
            {
                return user.TypeKey == request.TypeKey;
            }
            if (Contract.CheckValidKey(request.TypeName))
            {
                return user.TypeName == request.TypeName;
            }
            if (Contract.CheckValidKey(request.ContactFirstName))
            {
                return user.Slave!.ContactFirstName == request.ContactFirstName;
            }
            if (Contract.CheckValidKey(request.ContactLastName))
            {
                return user.Slave!.ContactLastName == request.ContactLastName;
            }
            // ReSharper disable once InvertIf
            if (request.HasValidPassport.GetValueOrDefault(false))
            {
                if (user.Slave!.SerializableAttributes == null)
                {
                    return false;
                }
                var success = user.Slave.SerializableAttributes
                    .TryGetValue("PassportDateOfExpiration", out var attribute);
                if (!success)
                {
                    return false;
                }
                success = DateTime.TryParse(attribute?.Value, out var passportExpirationDate);
                return success && passportExpirationDate > user.Date.AddDays(180);
            }
            return true;
        }

        private static bool PostSalesInvoiceFiltering(
            IUserEventAttendanceModel attendee,
            IUserEventAttendanceSearchModel request,
            ISalesInvoiceModel salesInvoice)
        {
            var product = salesInvoice.SalesItems?.FirstOrDefault(si => si.ProductName!.Contains(" by "));
            if (product == null)
            {
                return false;
            }
            attendee.EventPackageType = product.Name![..(product.Name!.IndexOf(" by ", StringComparison.OrdinalIgnoreCase) - 1)];
            attendee.UserEventTotal = salesInvoice.Totals!.Total;
            attendee.Balance = salesInvoice.BalanceDue;
            var lastPaymentLast4 = salesInvoice.SalesInvoicePayments!
                .OrderByDescending(x => x.CreatedDate)
                .Select(p => p.Slave!.Last4CardDigits)
                .FirstOrDefault();
            var isCreditCard = !string.IsNullOrEmpty(lastPaymentLast4);
            attendee.SerializableAttributes ??= new();
            attendee.SerializableAttributes["InvoiceID"] = new()
            {
                Key = "InvoiceID",
                Value = salesInvoice.ID.ToString(),
            };
            attendee.SerializableAttributes["IsLastPaymentByCreditCard"] = new()
            {
                Key = "IsLastPaymentByCreditCard",
                Value = isCreditCard.ToString(),
            };
            return !request.IsPaidInFull.HasValue
                || (!request.IsPaidInFull.Value || attendee.Balance <= 0.00m)
                && (request.IsPaidInFull.Value || attendee.Balance > 0.00m);
        }
    }
}
