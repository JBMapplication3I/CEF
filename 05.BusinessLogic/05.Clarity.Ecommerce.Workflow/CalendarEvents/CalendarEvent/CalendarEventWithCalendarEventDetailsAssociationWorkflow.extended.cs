// <copyright file="CalendarEventWithCalendarEventDetailsAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate calendar event detail workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    using System.Threading.Tasks;
    using Interfaces.DataModel;
    using Interfaces.Models;

    public partial class CalendarEventWithCalendarEventDetailsAssociationWorkflow
    {
        /// <inheritdoc/>
        protected override Task<bool> MatchObjectModelWithObjectEntityAdditionalChecksAsync(
            ICalendarEventDetailModel model,
            ICalendarEventDetail entity,
            IClarityEcommerceEntities context)
        {
            return Task.FromResult(
                entity.CalendarEventID == model.CalendarEventID
                && entity.Day == model.Day
                && entity.StartTime == model.StartTime
                && entity.EndTime == model.EndTime
                && entity.Location == model.Location);
        }

        ////protected override ICalendarEventDetail ModelToNewObject(ICalendarEventDetailModel model, DateTime timestamp, string? contextProfileName)
        ////{
        ////    Contract.RequiresNotNull(model);
        ////    Contract.Requires<InvalidOperationException>(!string.IsNullOrEmpty(model.Name), "Must specify at least the name of the event detail");
        ////    //
        ////    var newEntity = RegistryLoaderWrapper.GetInstance<ICalendarEventDetail>(contextProfileName);
        ////    newEntity.CustomKey = model.CustomKey;
        ////    newEntity.Active = true;
        ////    newEntity.CreatedDate = timestamp;
        ////    newEntity.Name = model.Name ?? string.Empty;
        ////    newEntity.Day = model.Day;
        ////    newEntity.StartTime = model.StartTime;
        ////    newEntity.EndTime = model.EndTime;
        ////    newEntity.Location = model.Location;
        ////    //
        ////    return newEntity;
        ////}
    }
}
