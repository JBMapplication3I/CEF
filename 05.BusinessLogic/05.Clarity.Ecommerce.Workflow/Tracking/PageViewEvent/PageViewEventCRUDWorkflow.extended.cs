// <copyright file="PageViewEventCRUDWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2016-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the page view event workflow class</summary>
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
    using Mapper;
    using Models;
    using Utilities;

    public partial class PageViewEventWorkflow
    {
        /// <inheritdoc/>
        public async Task<CEFActionResponse<IEndUserEventModel>> CreateFromEndUserEventAsync(
            IEndUserEventModel request,
            string? contextProfileName)
        {
            var timestamp = DateExtensions.GenDateTime;
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            // Prep Visitor
            // <do nothing>
            // Upsert Visitor
            ////var visitor = VisitorWorkflow.Upsert(request.Visitor, false);
            var (cefActionResponse, visitorID) = await ResolveVisitorIDAsync(request, timestamp, context, contextProfileName).ConfigureAwait(false);
            if (cefActionResponse != null)
            {
                return cefActionResponse;
            }
            if (visitorID == null)
            {
                return CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Visitor Failed");
            }
            // Apply Visitor to Request
            request.VisitorID = visitorID;
            request.Visitor = null;
            // Upsert Visit
            var (cefActionResponse2, visitID) = await ResolveVisitIDAsync(request, timestamp, context, contextProfileName).ConfigureAwait(false);
            if (cefActionResponse2 != null)
            {
                return cefActionResponse2;
            }
            if (visitID == null)
            {
                return CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Visit Failed");
            }
            // Apply Visit to Request
            request.VisitID = visitID;
            request.Visit = null;
            // Upsert Event
            var (cefActionResponse3, eventID) = await ResolveEventIDAsync(request, timestamp, context, contextProfileName).ConfigureAwait(false);
            if (cefActionResponse3 != null)
            {
                return cefActionResponse3;
            }
            if (eventID == null)
            {
                return CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Event Failed");
            }
            // Apply Event to request
            request.EventID = eventID;
            request.Event = null;
            // Upsert PageView
            var (cefActionResponse4, pageViewID) = await ResolvePageViewIDAsync(request, timestamp, context, contextProfileName).ConfigureAwait(false);
            if (cefActionResponse4 != null)
            {
                return cefActionResponse4;
            }
            if (pageViewID == null)
            {
                return CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Page View Failed");
            }
            // Apply PageView to request
            request.PageViewID = pageViewID;
            request.PageView = null;
            // Create PageViewEvent
            var (cefActionResponse5, pageViewEventID) = await ResolvePageViewEventIDAsync(request, timestamp, context).ConfigureAwait(false);
            if (cefActionResponse5 != null)
            {
                return cefActionResponse5;
            }
            if (pageViewEventID == null)
            {
                return CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Page View Event Failed");
            }
            // Apply PageViewEvent to request
            request.PageViewEventID = pageViewEventID;
            request.PageViewEvent = null;
            // Blank out values the UI doesn't need to read
            request.PageViewEventID = null;
            request.PageViewID = null;
            request.EventID = null;
            // Save anything that didn't already save
            await context.SaveUnitOfWorkAsync().ConfigureAwait(false);
            // Return all is good
            return new EndUserEventModel { VisitorID = request.VisitorID, VisitID = request.VisitID }
                .WrapInPassingCEFAR<IEndUserEventModel>()!;
        }

        /// <inheritdoc/>
        public async Task<List<(int ProductID, DateTime CreatedDate)>> GetRecentlyViewedProductIDsForCurrentVisitorAsync(
            string requestUserHostAddress,
            Guid? sessionVisitGuid,
            Guid? sessionVisitorGuid,
            Paging paging,
            string? contextProfileName)
        {
            using var context = RegistryLoaderWrapper.GetContext(contextProfileName);
            var visitorID = await context.Visitors
                    .FilterByActive(true)
                    .Where(x => x.Visits!.Any(y => y.CustomKey == sessionVisitGuid.ToString()))
                    .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                    .ThenByDescending(x => x.CreatedDate)
                    .Select(x => (int?)x.ID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false)
                ?? await context.Visitors
                    .FilterByActive(true)
                    .FilterByCustomKey(sessionVisitorGuid.ToString(), true)
                    .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                    .ThenByDescending(x => x.CreatedDate)
                    .Select(x => (int?)x.ID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false)
                ?? await context.Visitors
                    .FilterByActive(true)
                    .Where(x => x.IPAddress == requestUserHostAddress)
                    .OrderByDescending(x => x.UpdatedDate ?? x.CreatedDate)
                    .ThenByDescending(x => x.CreatedDate)
                    .Select(x => x.ID)
                    .FirstOrDefaultAsync()
                    .ConfigureAwait(false);
            var productViewEvents = await context.PageViewEvents
                .FilterByActive(true)
                .Where(x => x.Master!.VisitorID == visitorID && x.Master.ProductID.HasValue && x.Master.Product!.Active)
                .Select(x => new
                {
                    ProductID = x.Master!.ProductID!.Value,
                    x.CreatedDate,
                })
                .OrderByDescending(x => x.CreatedDate)
                .ToListAsync();
            var hasPaging = !(paging?.Size == null
                || paging.StartIndex == null
                || paging.Size <= 0
                || paging.StartIndex < 0);
            // Manually find distinct entries from newest to oldest, as Distinct() and DistinctBy() ordering is not deterministic
            var result = new List<(int ProductID, DateTime CreatedDate)>();
            foreach (var e in productViewEvents.Skip(hasPaging ? paging!.Size!.Value * (paging.StartIndex!.Value - 1) : 0))
            {
                if (result.Any(x => x.ProductID == e.ProductID))
                {
                    continue;
                }
                result.Add((e.ProductID, e.CreatedDate));
                if (hasPaging && result.Count >= paging!.Size!.Value)
                {
                    break;
                }
            }
            return result;
        }

        /// <inheritdoc/>
        protected override Task AssignAdditionalPropertiesAsync(
            IPageViewEvent entity,
            IPageViewEventModel model,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            entity.UpdatePageViewEventFromModel(model, timestamp, Contract.CheckValidID(entity.ID) ? timestamp : null);
            return Task.CompletedTask;
        }

        /// <summary>Resolve page view event.</summary>
        /// <param name="request">          The request.</param>
        /// <param name="timestamp">        The timestamp Date/Time.</param>
        /// <param name="context">          The context.</param>
        /// <returns>A PageViewEvent.</returns>
        private static async Task<(CEFActionResponse<IEndUserEventModel>? failure, int? id)> ResolvePageViewEventIDAsync(
            IEndUserEventModel request,
            DateTime timestamp,
            IClarityEcommerceEntities context)
        {
            var query = context.PageViewEvents.AsNoTracking().FilterByActive(true);
            if (Contract.CheckValidID(request.PageViewEventID))
            {
                var pageViewEventID = await query.FilterByID(request.PageViewEventID!.Value).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewEventID))
                {
                    return (null, pageViewEventID);
                }
            }
            if (Contract.CheckValidKey(request.PageViewEventKey))
            {
                var pageViewEventID = await query.FilterByCustomKey(request.PageViewEventKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewEventID))
                {
                    return (null, pageViewEventID);
                }
            }
            if (request.PageViewEvent == null)
            {
                return (null, null);
            }
            if (Contract.CheckValidID(request.PageViewEvent.ID))
            {
                var pageViewEventID = await query.FilterByID(request.PageViewEvent.ID).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewEventID))
                {
                    return (null, pageViewEventID);
                }
            }
            if (Contract.CheckValidKey(request.PageViewEvent.CustomKey))
            {
                var pageViewEventID = await query.FilterByCustomKey(request.PageViewEvent.CustomKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewEventID))
                {
                    return (null, pageViewEventID);
                }
            }
            var pageViewEvent = (PageViewEvent)request.PageViewEvent.CreatePageViewEventEntity(timestamp, context.ContextProfileName);
            if (!Contract.CheckValidID(pageViewEvent.SlaveID))
            {
                pageViewEvent.SlaveID = request.EventID!.Value;
            }
            if (!Contract.CheckValidID(pageViewEvent.MasterID))
            {
                pageViewEvent.MasterID = request.PageViewID!.Value;
            }
            context.PageViewEvents.Add(pageViewEvent);
            var result = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (result)
            {
                return (null, pageViewEvent.ID);
            }
            return (CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: PageViewEvent Failed"), null);
        }

        /// <summary>Resolve visitor.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Visitor.</returns>
        private async Task<(CEFActionResponse<IEndUserEventModel>? failure, int? id)> ResolveVisitorIDAsync(
            IEndUserEventModel request,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            var query = context.Visitors.AsNoTracking().FilterByActive(true);
            if (Contract.CheckValidID(request.VisitorID))
            {
                var visitorID = await query.FilterByID(request.VisitorID!.Value).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitorID))
                {
                    return (null, visitorID);
                }
            }
            if (Contract.CheckValidKey(request.VisitorKey))
            {
                var visitorID = await query.FilterByCustomKey(request.VisitorKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitorID))
                {
                    return (null, visitorID);
                }
            }
            if (request.Visitor == null)
            {
                return (null, null);
            }
            if (Contract.CheckValidID(request.Visitor.ID))
            {
                var visitorID = await query.FilterByID(request.Visitor.ID).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitorID))
                {
                    return (null, visitorID);
                }
            }
            if (Contract.CheckValidKey(request.Visitor.CustomKey))
            {
                var visitorID = await query.FilterByCustomKey(request.Visitor.CustomKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitorID))
                {
                    return (null, visitorID);
                }
            }
            var model = request.Visitor;
            if (model.Address != null)
            {
                model.Address = await Workflows.Addresses.ResolveAddressAsync(model.Address, contextProfileName).ConfigureAwait(false);
            }
            var visitor = (Visitor)model.CreateVisitorEntity(timestamp, contextProfileName);
            context.Visitors.Add(visitor);
            var result = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (result)
            {
                return (null, visitor.ID);
            }
            return (CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Visitor Failed"), null);
        }

        /// <summary>Resolve visit.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A Visit.</returns>
        private async Task<(CEFActionResponse<IEndUserEventModel>? failure, int? id)> ResolveVisitIDAsync(
            IEndUserEventModel request,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            var query = context.Visits.AsNoTracking().FilterByActive(true);
            if (Contract.CheckValidID(request.VisitID))
            {
                var visitID = await query.FilterByID(request.VisitID!.Value).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitID))
                {
                    return (null, visitID);
                }
            }
            if (Contract.CheckValidKey(request.VisitKey))
            {
                var visitID = await query.FilterByCustomKey(request.VisitKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitID))
                {
                    return (null, visitID);
                }
            }
            if (request.Visit == null)
            {
                return (null, null);
            }
            if (Contract.CheckValidID(request.Visit.ID))
            {
                var visitID = await query.FilterByID(request.Visit.ID).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitID))
                {
                    return (null, visitID);
                }
            }
            if (Contract.CheckValidKey(request.Visit.CustomKey))
            {
                var visitID = await query.FilterByCustomKey(request.Visit.CustomKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(visitID))
                {
                    return (null, visitID);
                }
            }
            var model = request.Visit;
            model.VisitorID = request.VisitorID;
            if (model.Address != null)
            {
                model.Address = await Workflows.Addresses.ResolveAddressAsync(model.Address, contextProfileName).ConfigureAwait(false);
            }
            var visit = (Visit)model.CreateVisitEntity(timestamp, contextProfileName);
            visit.StatusID = request.Visit.StatusID;
            context.Visits.Add(visit);
            var result = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (result)
            {
                return (null, visit.ID);
            }
            return (CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Visit Failed"), null);
        }

        /// <summary>Resolve event.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>An Event.</returns>
        private async Task<(CEFActionResponse<IEndUserEventModel>? failure, int? id)> ResolveEventIDAsync(
            IEndUserEventModel request,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            var query = context.Events.AsNoTracking().FilterByActive(true);
            if (Contract.CheckValidID(request.EventID))
            {
                var eventID = await query.FilterByID(request.EventID!.Value).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(eventID))
                {
                    return (null, eventID);
                }
            }
            if (Contract.CheckValidKey(request.EventKey))
            {
                var eventID = await query.FilterByCustomKey(request.EventKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(eventID))
                {
                    return (null, eventID);
                }
            }
            if (request.Event == null)
            {
                return (null, null);
            }
            if (Contract.CheckValidID(request.Event.ID))
            {
                var eventID = await query.FilterByID(request.Event.ID).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(eventID))
                {
                    return (null, eventID);
                }
            }
            if (Contract.CheckValidKey(request.Event.CustomKey))
            {
                var eventID = await query.FilterByCustomKey(request.Event.CustomKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(eventID))
                {
                    return (null, eventID);
                }
            }
            var model = request.Event;
            if (model.Address != null)
            {
                model.Address = await Workflows.Addresses.ResolveAddressAsync(model.Address, contextProfileName).ConfigureAwait(false);
            }
            model.VisitorID = request.VisitorID;
            model.VisitID = request.VisitID;
            var @event = (Event)model.CreateEventEntity(timestamp, contextProfileName);
            @event.StatusID = request.Event.StatusID;
            @event.TypeID = request.Event.TypeID;
            context.Events.Add(@event);
            var result = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (result)
            {
                return (null, @event.ID);
            }
            return (CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: Event Failed"), null);
        }

        /// <summary>Resolve page view.</summary>
        /// <param name="request">           The request.</param>
        /// <param name="timestamp">         The timestamp Date/Time.</param>
        /// <param name="context">           The context.</param>
        /// <param name="contextProfileName">Name of the context profile.</param>
        /// <returns>A PageView.</returns>
        private async Task<(CEFActionResponse<IEndUserEventModel>? failure, int? id)> ResolvePageViewIDAsync(
            IEndUserEventModel request,
            DateTime timestamp,
            IClarityEcommerceEntities context,
            string? contextProfileName)
        {
            var query = context.PageViews.AsNoTracking().FilterByActive(true);
            if (Contract.CheckValidID(request.PageViewID))
            {
                var pageViewID = await query.FilterByID(request.PageViewID!.Value).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewID))
                {
                    return (null, pageViewID);
                }
            }
            if (Contract.CheckValidKey(request.PageViewKey))
            {
                var pageViewID = await query.FilterByCustomKey(request.PageViewKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewID))
                {
                    return (null, pageViewID);
                }
            }
            if (request.PageView == null)
            {
                return (null, null);
            }
            if (Contract.CheckValidID(request.PageView.ID))
            {
                var pageViewID = await query.FilterByID(request.PageView.ID).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewID))
                {
                    return (null, pageViewID);
                }
            }
            if (Contract.CheckValidKey(request.PageView.CustomKey))
            {
                var pageViewID = await query.FilterByCustomKey(request.PageView.CustomKey, true).Select(x => (int?)x.ID).SingleOrDefaultAsync();
                if (Contract.CheckValidID(pageViewID))
                {
                    return (null, pageViewID);
                }
            }
            var model = request.PageView;
            if (model.Address != null)
            {
                model.Address = await Workflows.Addresses.ResolveAddressAsync(model.Address, contextProfileName).ConfigureAwait(false);
            }
            var pageView = (PageView)model.CreatePageViewEntity(timestamp, contextProfileName);
            pageView.StatusID = request.PageView.StatusID;
            pageView.TypeID = request.PageView.TypeID;
            pageView.Browser = request.PageView.Browser;
            pageView.ProductID = request.PageView.ProductID;
            pageView.CategoryID = request.PageView.CategoryID;
            pageView.UserID = request.PageView.UserID;
            pageView.VisitorID = request.VisitorID;
            context.PageViews.Add(pageView);
            var result = await context.SaveUnitOfWorkAsync(true).ConfigureAwait(false);
            if (result)
            {
                return (null, pageView.ID);
            }
            return (CEFAR.FailingCEFAR<IEndUserEventModel>("ERROR: PageView Failed"), null);
        }
    }
}
