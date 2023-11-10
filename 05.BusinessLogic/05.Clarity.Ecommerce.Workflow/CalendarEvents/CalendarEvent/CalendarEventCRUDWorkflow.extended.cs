// <copyright file="CalendarEventCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the calendar event workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Threading.Tasks;
    using DataModel;
    using Interfaces.DataModel;
    using Interfaces.Models;
    using JSConfigs;
    using Mapper;
    using Models;
    using Utilities;

    public partial class CalendarEventWorkflow
    {
        /// <summary>Gets or sets the calendar event status identifier of normal.</summary>
        /// <value>The calendar event status identifier of normal.</value>
        protected static int CalendarEventStatusIDOfNormal { get; set; }

        /// <summary>Gets or sets the calendar event type identifier of general.</summary>
        /// <value>The calendar event type identifier of general.</value>
        protected static int CalendarEventTypeIDOfGeneral { get; set; }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> CanUserChangePackageAsync(int eventID, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var eventStartDate = await context.CalendarEvents
                .AsNoTracking()
                .FilterByID(eventID)
                .Select(x => (DateTime?)x.StartDate)
                .SingleOrDefaultAsync()
                .ConfigureAwait(false);
            if (eventStartDate == null)
            {
                return CEFAR.FailingCEFAR("Cannot locate event");
            }
            var packageChangeLimit = eventStartDate.Value
                .AddDays(-1 * CEFConfigDictionary.CalendarEventsChangePackageLimitInDays);
            return packageChangeLimit < DateExtensions.GenDateTime
                ? CEFAR.FailingCEFAR("Date limit for package change is passed")
                : CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse> ChangePackageAsync(int eventID, int productID, int currentUserID, string? contextProfileName)
        {
            Contract.RequiresValidID(eventID);
            Contract.RequiresValidID(productID);
            Contract.RequiresValidID(currentUserID);
            // GRAB the event
            string? key;
            using (var context = RegistryLoaderWrapper.GetContext(contextProfileName))
            {
                key = await context.CalendarEvents
                    .AsNoTracking()
                    .FilterByID(eventID)
                    .FilterCalendarEventsByProductID(productID)
                    .Select(x => x.CustomKey)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
                if (key == null)
                {
                    return CEFAR.FailingCEFAR("Couldn't find event");
                }
            }
            // Grab the current invoice for the event and the user
            var results = (await Workflows.SalesInvoices.SearchAsync(
                        search: new SalesInvoiceSearchModel { UserID = currentUserID, ProductKey = key },
                        asListing: false,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false))
                .results;
            var salesInvoice = results.FirstOrDefault();
            if (!Contract.CheckValidID(salesInvoice?.ID) || salesInvoice?.SalesItems == null)
            {
                return CEFAR.FailingCEFAR("No sales invoice was found for this event.");
            }
            // Grab the invoice to get all the related objects
            salesInvoice = await Workflows.SalesInvoices.GetAsync(salesInvoice.ID, contextProfileName).ConfigureAwait(false);
            // Make sure the user doesn't already have the package
            if (salesInvoice!.SalesItems!.Any(si => si.ProductID == productID))
            {
                return CEFAR.FailingCEFAR("You cannot change to the same package.");
            }
            var newInvoice = CreateNewInvoice(currentUserID, salesInvoice, productID, contextProfileName);
            try
            {
                // Save new invoice
                await Workflows.SalesInvoices.CreateAsync(newInvoice, contextProfileName).ConfigureAwait(false);
                // De-activate/VOID previous invoice
                salesInvoice.Active = false;
                salesInvoice.StatusID = 0;
                salesInvoice.Status = null;
                salesInvoice.StatusKey = "VOID";
                salesInvoice.StateKey = "HISTORY";
                await Workflows.SalesInvoices.UpdateAsync(salesInvoice, contextProfileName).ConfigureAwait(false);
            }
            catch (Exception ex)
            {
                return CEFAR.FailingCEFAR(ex.Message, ex.StackTrace!);
            }
            return CEFAR.PassingCEFAR();
        }

        /// <inheritdoc/>
        public Task<List<ICalendarEventModel>> GetDoneEventsWithNoSurveyAsync(
            ICalendarEventSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return Task.FromResult(
                context.CalendarEvents
                    .AsNoTracking()
                    .FilterByActive(search.Active)
                    .FilterByStatusKey<CalendarEvent, CalendarEventStatus>(search.StatusKey)
                    .FilterCalendarEventsByEndDateBeforeDate(search.EndDate)
                    .SelectFullCalendarEventAndMapToCalendarEventModel(contextProfileName)
                    .ToList());
        }

        /// <inheritdoc/>
        public async Task<List<string>> GetEventKeysInLastStatementStateAsync(int days, string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            return await context.CalendarEvents
                .AsNoTracking()
                .FilterByActive(true)
                .FilterCalendarEventByLastStatementState(days, DateExtensions.GenDateTime)
                .Select(evt => evt.CustomKey!)
                .Distinct()
                .ToListAsync()
                .ConfigureAwait(false);
        }

        /// <inheritdoc/>
        public async Task<CEFActionResponse<(List<ICalendarEventModel> results, int totalPages, int totalCount)>> GetCalendarEventsWithAdminUsersAsync(
            ICalendarEventSearchModel search,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var query = context.CalendarEvents.AsNoTracking().AsQueryable();
            query = (await FilterQueryByModelExtensionAsync(query, search, context).ConfigureAwait(false))
                .ApplySorting(search.Sorts, search.Groupings, contextProfileName)
                .FilterByPaging(search.Paging, out var totalPages, out var totalCount);
            search.UserAttendanceTypeKeys ??= Array.Empty<string>();
            return (query
                    .Select(x => new
                    {
                        x.ID,
                        x.CreatedDate,
                        x.Name,
                        x.CustomKey,
                        x.StartDate,
                        x.EndDate,
                        x.TypeID,
                        x.StatusID,
                        x.EventDuration,
                        Users = x.UserEventAttendances!
                            .Where(y => search.UserAttendanceTypeKeys.Contains(y.Type!.CustomKey))
                            .Select(y => y.Slave!.Contact!.FirstName + " " + y.Slave.Contact.LastName),
                    }
                    !)
                    .AsEnumerable()
                    .Select(x => new CalendarEventModel
                    {
                        Dates = $"{x.StartDate:MM/dd/yyyy} - {x.EndDate:MM/dd/yyyy}",
                        StartDate = x.StartDate,
                        EndDate = x.EndDate,
                        UserEventAttendances = x.Users
                            .Select(y => new UserEventAttendanceModel
                            {
                                SlaveKey = y,
                            })
                            .ToList(),
                        ID = x.ID,
                        CreatedDate = x.CreatedDate,
                        Active = true,
                        CustomKey = x.CustomKey,
                        Name = x.Name,
                        EventDuration = x.EventDuration,
                        StatusID = x.StatusID,
                        TypeID = x.TypeID,
                        SerializableAttributes = new()
                        {
                            ["IsPaidInFull"] = new()
                            {
                                Key = "IsPaidInFull",
                                Value = IsPaidInFullAsync(x.ID, search.UserID, contextProfileName).ToString()!,
                            },
                        },
                    })
                    .ToList<ICalendarEventModel>(),
                    totalPages,
                    totalCount)
                .WrapInPassingCEFAR();
        }

        /// <inheritdoc/>
        protected override async Task AssignAdditionalPropertiesAsync(
            ICalendarEvent entity,
            ICalendarEventModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdateCalendarEventFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            // Related Objects
            await AssignDefaultStatusIfNullAsync(model, context.ContextProfileName).ConfigureAwait(false);
            await AssignDefaultTypeIfNullAsync(model, context.ContextProfileName).ConfigureAwait(false);
            await model.Contact.AssignPrePropertiesToContactAndAddressAsync(Workflows.Addresses, context.ContextProfileName).ConfigureAwait(false);
            await RunDefaultRelateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
            entity.Contact.AssignPostPropertiesToContactAndAddress(model.Contact, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null, context.ContextProfileName);
            // Associated Objects
            await RunDefaultAssociateWorkflowsAsync(entity, model, timestamp, context.ContextProfileName).ConfigureAwait(false);
        }

        /// <inheritdoc/>
        protected override async Task<IQueryable<CalendarEvent>> FilterQueryByModelCustomAsync(
            IQueryable<CalendarEvent> query,
            ICalendarEventSearchModel search,
            IClarityEcommerceEntities context)
        {
            return query
                .FilterCalendarEventsByDaysUntilDeparture(search.DaysUntilDeparture)
                .FilterCalendarEventsByStrictDaysUntilDeparture(search.StrictDaysUntilDeparture)
                .FilterCalendarEventsByCurrentEvents(search.CurrentEventsOnly);
        }

        /// <summary>Creates new invoice.</summary>
        /// <param name="currentUserID">     Identifier for the current user.</param>
        /// <param name="invoice">           The invoice.</param>
        /// <param name="productID">         Identifier for the product.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>The new new invoice.</returns>
        private static ISalesInvoiceModel CreateNewInvoice(
            int currentUserID,
            ISalesInvoiceModel invoice,
            int productID,
            string? contextProfileName)
        {
            // Create new invoice with new amount
            var newSalesInvoice = new SalesInvoiceModel
            {
                Active = true,
                UserID = currentUserID,
                BillingContactID = invoice.BillingContactID,
                AccountID = invoice.AccountID,
                SalesItems = new(),
                SalesInvoicePayments = new(),
                StatusKey = "Pending",
                TypeKey = "General",
            };
            // Add new items
            // Check if Item is a kit. If so, add the component items
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var productEntity = context.Products.AsNoTracking().Single(x => x.ID == productID);
            var product = ModelMapperForProduct.MapLiteProductOld(productEntity);
            if (product.TypeKey == "KIT")
            {
                foreach (var componentItem in product.ProductAssociations!.Where(x => x.Active))
                {
                    newSalesInvoice.SalesItems.Add(MapProductToSalesItem(componentItem.Slave!));
                }
            }
            else
            {
                newSalesInvoice.SalesItems.Add(MapProductToSalesItem(product));
            }
            var totalAmount = newSalesInvoice.SalesItems.Sum(x => x.ExtendedPrice);
            var paymentTotal = 0m;
            // Transfer payments
            foreach (var invoicePayment in invoice.SalesInvoicePayments!)
            {
                newSalesInvoice.SalesInvoicePayments.Add(new()
                {
                    Active = true,
                    CreatedDate = DateExtensions.GenDateTime,
                    SlaveID = invoicePayment.SlaveID,
                    Slave = (PaymentModel)invoicePayment.Slave!,
                });
                paymentTotal += invoicePayment.Slave!.Amount ?? 0m;
            }
            // Update balance due
            newSalesInvoice.BalanceDue = totalAmount - paymentTotal;
            if (newSalesInvoice.BalanceDue == 0)
            {
                newSalesInvoice.StatusKey = "Paid";
            }
            return newSalesInvoice;
        }

        /// <summary>Map product to sales item.</summary>
        /// <param name="product">The product.</param>
        /// <returns>A SalesItemBaseModel{IAppliedSalesInvoiceItemDiscountModel,AppliedSalesInvoiceItemDiscountModel}.</returns>
        private static SalesItemBaseModel<IAppliedSalesInvoiceItemDiscountModel, AppliedSalesInvoiceItemDiscountModel>
            MapProductToSalesItem(IBaseModel product)
        {
            return new()
            {
                Active = true,
                CreatedDate = product.CreatedDate,
                ProductID = product.ID,
                Sku = product.CustomKey,
                UnitCorePrice = 0m,
                UnitSoldPrice = 0m,
            };
        }

        /// <summary>Query if 'eventID' is paid in full.</summary>
        /// <param name="eventID">           Identifier for the event.</param>
        /// <param name="userID">            Identifier for the user.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>True if paid in full, false if not.</returns>
        private static async Task<bool> IsPaidInFullAsync(int eventID, int? userID, string? contextProfileName)
        {
            var provider = RegistryLoaderWrapper.GetSalesInvoiceQueriesProvider(contextProfileName);
            if (provider is null)
            {
                return false;
            }
            var salesInvoice = await provider.GetRecordByUserAndEventAsync(
                    userID: userID ?? 0,
                    calendarEventID: eventID,
                    contextProfileName: contextProfileName)
                .ConfigureAwait(false);
            return salesInvoice.Result!.StatusKey == "Paid" || salesInvoice.Result.BalanceDue == 0;
        }

        /// <summary>Assign default status if null.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AssignDefaultStatusIfNullAsync(ICalendarEventModel model, string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                    model.Status?.ID ?? model.StatusID,
                    model.StatusKey,
                    model.StatusName,
                    model.Status?.CustomKey,
                    model.Status?.Name,
                    model.Status?.DisplayName))
            {
                return;
            }
            if (Contract.CheckInvalidID(CalendarEventStatusIDOfNormal))
            {
                CalendarEventStatusIDOfNormal = await Workflows.CalendarEventStatuses.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "NORMAL",
                        byName: "Normal",
                        byDisplayName: "Normal",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            model.StatusID = CalendarEventStatusIDOfNormal;
        }

        /// <summary>Assign default type if null.</summary>
        /// <param name="model">             The model.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Task.</returns>
        private async Task AssignDefaultTypeIfNullAsync(ICalendarEventModel model, string? contextProfileName)
        {
            if (Contract.CheckValidIDOrAnyValidKey(
                    model.Type?.ID ?? model.TypeID,
                    model.TypeKey,
                    model.TypeName,
                    model.Type?.CustomKey,
                    model.Type?.Name,
                    model.Type?.DisplayName))
            {
                return;
            }
            if (Contract.CheckInvalidID(CalendarEventTypeIDOfGeneral))
            {
                CalendarEventTypeIDOfGeneral = await Workflows.CalendarEventTypes.ResolveWithAutoGenerateToIDAsync(
                        byID: null,
                        byKey: "GENERAL",
                        byName: "General",
                        byDisplayName: "General",
                        model: null,
                        contextProfileName: contextProfileName)
                    .ConfigureAwait(false);
            }
            model.TypeID = CalendarEventTypeIDOfGeneral;
        }
    }
}
