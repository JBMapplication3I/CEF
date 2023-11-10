// <copyright file="CalendarEventWithUserEventAttendancesAssociationWorkflow.extended.cs" company="clarity-ventures.com">
// Copyright (c) 2017-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the associate calendar event user attendance workflow class</summary>
namespace Clarity.Ecommerce.Workflow
{
    public partial class CalendarEventWithUserEventAttendancesAssociationWorkflow
    {
        ////protected override IUserEventAttendance ModelToNewObject(IUserEventAttendanceModel model, DateTime timestamp, string? contextProfileName)
        ////{
        ////    Contract.RequiresNotNull(model);
        ////    var typeID = model.TypeID;
        ////    var typeKey = model.TypeKey ?? model.Type?.CustomKey;
        ////    if (typeID == 0 && !string.IsNullOrEmpty(typeKey))
        ////    {
        ////        var type = context.UserEventAttendanceTypes.FirstOrDefault(c => c.CustomKey == typeKey);
        ////        if (type != null) { typeID = type.ID; }
        ////    }
        ////    //
        ////    var newEntity = RegistryLoaderWrapper.GetInstance<IUserEventAttendance>(contextProfileName);
        ////    newEntity.CustomKey = model.CustomKey;
        ////    newEntity.Active = true;
        ////    newEntity.CreatedDate = timestamp;
        ////    newEntity.UserID = model.UserID;
        ////    newEntity.CalendarEventID = model.CalendarEventID;
        ////    newEntity.Date = model.Date;
        ////    newEntity.HasAttended = model.HasAttended;
        ////    newEntity.TypeID = typeID;
        ////    //
        ////    return newEntity;
        ////}
    }
}
