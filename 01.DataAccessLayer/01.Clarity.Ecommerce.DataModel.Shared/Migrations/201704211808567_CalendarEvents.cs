// <copyright file="201704211808567_CalendarEvents.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201704211808567 calendar events class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CalendarEvents : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CalendarEvents.UserEventAttendance",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        TypeID = c.Int(nullable: false),
                        UserID = c.Int(nullable: false),
                        CalendarEventID = c.Int(nullable: false),
                        HasAttended = c.Boolean(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CalendarEvents.CalendarEvent", t => t.CalendarEventID)
                .ForeignKey("CalendarEvents.UserEventAttendanceType", t => t.TypeID, cascadeDelete: true)
                .ForeignKey("Contacts.User", t => t.UserID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.TypeID)
                .Index(t => t.UserID)
                .Index(t => t.CalendarEventID)
                .Index(t => t.Date);

            CreateTable(
                "CalendarEvents.CalendarEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        ContactID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                        ShortDescription = c.String(unicode: false),
                        StartDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EndDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        EventDuration = c.Int(nullable: false),
                        EventDurationUnitOfMeasure = c.String(),
                        RecurrenceString = c.String(),
                        MaxAttendees = c.Int(nullable: false),
                        GroupID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Contacts.Contact", t => t.ContactID, cascadeDelete: true)
                .ForeignKey("Groups.Group", t => t.GroupID)
                .ForeignKey("CalendarEvents.CalendarEventStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("CalendarEvents.CalendarEventType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.ContactID)
                .Index(t => t.GroupID);

            CreateTable(
                "CalendarEvents.CalendarEventDetail",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        JsonAttributes = c.String(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        Day = c.Int(nullable: false),
                        StartTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        EndTime = c.DateTime(precision: 7, storeType: "datetime2"),
                        Location = c.String(),
                        CalendarEventID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CalendarEvents.CalendarEvent", t => t.CalendarEventID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.CalendarEventID);

            CreateTable(
                "CalendarEvents.CalendarEventFile",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SeoKeywords = c.String(maxLength: 512, unicode: false),
                        SeoUrl = c.String(maxLength: 512, unicode: false),
                        SeoPageTitle = c.String(maxLength: 75, unicode: false),
                        SeoDescription = c.String(maxLength: 256, unicode: false),
                        SeoMetaData = c.String(maxLength: 512, unicode: false),
                        FileAccessTypeID = c.Int(nullable: false),
                        SortOrder = c.Int(),
                        CalendarEventID = c.Int(nullable: false),
                        FileID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CalendarEvents.CalendarEvent", t => t.CalendarEventID, cascadeDelete: true)
                .ForeignKey("Media.File", t => t.FileID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.CalendarEventID)
                .Index(t => t.FileID);

            CreateTable(
                "CalendarEvents.CalendarEventProducts",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        CalendarEventID = c.Int(nullable: false),
                        ProductID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CalendarEvents.CalendarEvent", t => t.CalendarEventID, cascadeDelete: true)
                .ForeignKey("Products.Product", t => t.ProductID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.CalendarEventID)
                .Index(t => t.ProductID);

            CreateTable(
                "CalendarEvents.CalendarEventStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "CalendarEvents.CalendarEventType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);

            CreateTable(
                "CalendarEvents.UserEventAttendanceType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
        }

        public override void Down()
        {
            DropForeignKey("CalendarEvents.UserEventAttendance", "UserID", "Contacts.User");
            DropForeignKey("CalendarEvents.UserEventAttendance", "TypeID", "CalendarEvents.UserEventAttendanceType");
            DropForeignKey("CalendarEvents.UserEventAttendance", "CalendarEventID", "CalendarEvents.CalendarEvent");
            DropForeignKey("CalendarEvents.CalendarEvent", "TypeID", "CalendarEvents.CalendarEventType");
            DropForeignKey("CalendarEvents.CalendarEvent", "StatusID", "CalendarEvents.CalendarEventStatus");
            DropForeignKey("CalendarEvents.CalendarEvent", "GroupID", "Groups.Group");
            DropForeignKey("CalendarEvents.CalendarEvent", "ContactID", "Contacts.Contact");
            DropForeignKey("CalendarEvents.CalendarEventProducts", "ProductID", "Products.Product");
            DropForeignKey("CalendarEvents.CalendarEventProducts", "CalendarEventID", "CalendarEvents.CalendarEvent");
            DropForeignKey("CalendarEvents.CalendarEventFile", "FileID", "Media.File");
            DropForeignKey("CalendarEvents.CalendarEventFile", "CalendarEventID", "CalendarEvents.CalendarEvent");
            DropForeignKey("CalendarEvents.CalendarEventDetail", "CalendarEventID", "CalendarEvents.CalendarEvent");
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "SortOrder" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "DisplayName" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "Name" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "Active" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "CustomKey" });
            DropIndex("CalendarEvents.UserEventAttendanceType", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "SortOrder" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "DisplayName" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventType", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "SortOrder" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "DisplayName" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventStatus", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "ProductID" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "CalendarEventID" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventProducts", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "FileID" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "CalendarEventID" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventFile", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "CalendarEventID" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventDetail", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "GroupID" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "ContactID" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "StatusID" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "TypeID" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEvent", new[] { "ID" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "Date" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "CalendarEventID" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "UserID" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "TypeID" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "Active" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "CustomKey" });
            DropIndex("CalendarEvents.UserEventAttendance", new[] { "ID" });
            DropTable("CalendarEvents.UserEventAttendanceType");
            DropTable("CalendarEvents.CalendarEventType");
            DropTable("CalendarEvents.CalendarEventStatus");
            DropTable("CalendarEvents.CalendarEventProducts");
            DropTable("CalendarEvents.CalendarEventFile");
            DropTable("CalendarEvents.CalendarEventDetail");
            DropTable("CalendarEvents.CalendarEvent");
            DropTable("CalendarEvents.UserEventAttendance");
        }
    }
}
