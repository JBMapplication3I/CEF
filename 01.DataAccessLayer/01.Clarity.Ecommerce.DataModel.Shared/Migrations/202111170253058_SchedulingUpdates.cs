// <copyright file="202111170253058_SchedulingUpdates.cs" company="clarity-ventures.com">
// Copyright (c) 2021-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 202111170253058 scheduling updates class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class SchedulingUpdates : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("Scheduling.ScheduledEvent", "GroupID", "Groups.Group");
            DropForeignKey("Scheduling.ScheduledEvent", "RescheduledEventID", "Scheduling.ScheduledEvent");
            DropForeignKey("Scheduling.ScheduledEvent", "ScheduleID", "Scheduling.Schedule");
            DropForeignKey("Stores.Store", "ScheduleID", "Scheduling.Schedule");
            DropIndex("Stores.Store", new[] { "ScheduleID" });
            DropIndex("Scheduling.Schedule", new[] { "ID" });
            DropIndex("Scheduling.Schedule", new[] { "CustomKey" });
            DropIndex("Scheduling.Schedule", new[] { "CreatedDate" });
            DropIndex("Scheduling.Schedule", new[] { "UpdatedDate" });
            DropIndex("Scheduling.Schedule", new[] { "Active" });
            DropIndex("Scheduling.Schedule", new[] { "Hash" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "ID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "ScheduleID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "GroupID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "RescheduledEventID" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "CustomKey" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "CreatedDate" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "UpdatedDate" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "Active" });
            DropIndex("Scheduling.ScheduledEvent", new[] { "Hash" });
            CreateTable(
                "Scheduling.Appointment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TypeID = c.Int(nullable: false),
                        StatusID = c.Int(nullable: false),
                        AppointmentStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        AppointmentEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        SalesOrderID = c.Int(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Ordering.SalesOrder", t => t.SalesOrderID)
                .ForeignKey("Scheduling.AppointmentStatus", t => t.StatusID, cascadeDelete: true)
                .ForeignKey("Scheduling.AppointmentType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.TypeID)
                .Index(t => t.StatusID)
                .Index(t => t.SalesOrderID)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scheduling.CalendarAppointment",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MasterID = c.Int(nullable: false),
                        SlaveID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Scheduling.Calendar", t => t.MasterID, cascadeDelete: true)
                .ForeignKey("Scheduling.Appointment", t => t.SlaveID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.MasterID)
                .Index(t => t.SlaveID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scheduling.Calendar",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        MondayHoursStart = c.Decimal(precision: 18, scale: 2),
                        MondayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        TuesdayHoursStart = c.Decimal(precision: 18, scale: 2),
                        TuesdayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        WednesdayHoursStart = c.Decimal(precision: 18, scale: 2),
                        WednesdayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        ThursdayHoursStart = c.Decimal(precision: 18, scale: 2),
                        ThursdayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        FridayHoursStart = c.Decimal(precision: 18, scale: 2),
                        FridayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        SaturdayHoursStart = c.Decimal(precision: 18, scale: 2),
                        SaturdayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        SundayHoursStart = c.Decimal(precision: 18, scale: 2),
                        SundayHoursEnd = c.Decimal(precision: 18, scale: 2),
                        AccountID = c.Int(nullable: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("Accounts.Account", t => t.AccountID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.AccountID)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scheduling.AppointmentStatus",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            CreateTable(
                "Scheduling.AppointmentType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        TranslationKey = c.String(maxLength: 128, unicode: false),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder)
                .Index(t => t.Name)
                .Index(t => t.CustomKey)
                .Index(t => t.CreatedDate)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash);

            AddColumn("System.Note", "Appointment_ID", c => c.Int());
            CreateIndex("System.Note", "Appointment_ID");
            AddForeignKey("System.Note", "Appointment_ID", "Scheduling.Appointment", "ID");
            DropColumn("Stores.Store", "ScheduleID");
            DropTable("Scheduling.Schedule");
            DropTable("Scheduling.ScheduledEvent");
        }

        public override void Down()
        {
            CreateTable(
                "Scheduling.ScheduledEvent",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        EventStart = c.DateTime(precision: 7, storeType: "datetime2"),
                        EventEnd = c.DateTime(precision: 7, storeType: "datetime2"),
                        Memo = c.String(maxLength: 256),
                        ScheduleID = c.Int(),
                        GroupID = c.Int(),
                        RescheduledEventID = c.Int(),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "Scheduling.Schedule",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID);

            AddColumn("Stores.Store", "ScheduleID", c => c.Int());
            DropForeignKey("Scheduling.Appointment", "TypeID", "Scheduling.AppointmentType");
            DropForeignKey("Scheduling.Appointment", "StatusID", "Scheduling.AppointmentStatus");
            DropForeignKey("Scheduling.Appointment", "SalesOrderID", "Ordering.SalesOrder");
            DropForeignKey("System.Note", "Appointment_ID", "Scheduling.Appointment");
            DropForeignKey("Scheduling.CalendarAppointment", "SlaveID", "Scheduling.Appointment");
            DropForeignKey("Scheduling.CalendarAppointment", "MasterID", "Scheduling.Calendar");
            DropForeignKey("Scheduling.Calendar", "AccountID", "Accounts.Account");
            DropIndex("Scheduling.AppointmentType", new[] { "Hash" });
            DropIndex("Scheduling.AppointmentType", new[] { "Active" });
            DropIndex("Scheduling.AppointmentType", new[] { "UpdatedDate" });
            DropIndex("Scheduling.AppointmentType", new[] { "CreatedDate" });
            DropIndex("Scheduling.AppointmentType", new[] { "CustomKey" });
            DropIndex("Scheduling.AppointmentType", new[] { "Name" });
            DropIndex("Scheduling.AppointmentType", new[] { "SortOrder" });
            DropIndex("Scheduling.AppointmentType", new[] { "DisplayName" });
            DropIndex("Scheduling.AppointmentType", new[] { "ID" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "Hash" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "Active" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "UpdatedDate" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "CreatedDate" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "CustomKey" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "Name" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "SortOrder" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "DisplayName" });
            DropIndex("Scheduling.AppointmentStatus", new[] { "ID" });
            DropIndex("Scheduling.Calendar", new[] { "Hash" });
            DropIndex("Scheduling.Calendar", new[] { "Active" });
            DropIndex("Scheduling.Calendar", new[] { "UpdatedDate" });
            DropIndex("Scheduling.Calendar", new[] { "CreatedDate" });
            DropIndex("Scheduling.Calendar", new[] { "CustomKey" });
            DropIndex("Scheduling.Calendar", new[] { "AccountID" });
            DropIndex("Scheduling.Calendar", new[] { "ID" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "Hash" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "Active" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "UpdatedDate" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "CreatedDate" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "CustomKey" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "SlaveID" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "MasterID" });
            DropIndex("Scheduling.CalendarAppointment", new[] { "ID" });
            DropIndex("Scheduling.Appointment", new[] { "Hash" });
            DropIndex("Scheduling.Appointment", new[] { "Active" });
            DropIndex("Scheduling.Appointment", new[] { "UpdatedDate" });
            DropIndex("Scheduling.Appointment", new[] { "CreatedDate" });
            DropIndex("Scheduling.Appointment", new[] { "CustomKey" });
            DropIndex("Scheduling.Appointment", new[] { "Name" });
            DropIndex("Scheduling.Appointment", new[] { "SalesOrderID" });
            DropIndex("Scheduling.Appointment", new[] { "StatusID" });
            DropIndex("Scheduling.Appointment", new[] { "TypeID" });
            DropIndex("Scheduling.Appointment", new[] { "ID" });
            DropIndex("System.Note", new[] { "Appointment_ID" });
            DropColumn("System.Note", "Appointment_ID");
            DropTable("Scheduling.AppointmentType");
            DropTable("Scheduling.AppointmentStatus");
            DropTable("Scheduling.Calendar");
            DropTable("Scheduling.CalendarAppointment");
            DropTable("Scheduling.Appointment");
            CreateIndex("Scheduling.ScheduledEvent", "Hash");
            CreateIndex("Scheduling.ScheduledEvent", "Active");
            CreateIndex("Scheduling.ScheduledEvent", "UpdatedDate");
            CreateIndex("Scheduling.ScheduledEvent", "CreatedDate");
            CreateIndex("Scheduling.ScheduledEvent", "CustomKey");
            CreateIndex("Scheduling.ScheduledEvent", "RescheduledEventID");
            CreateIndex("Scheduling.ScheduledEvent", "GroupID");
            CreateIndex("Scheduling.ScheduledEvent", "ScheduleID");
            CreateIndex("Scheduling.ScheduledEvent", "ID");
            CreateIndex("Scheduling.Schedule", "Hash");
            CreateIndex("Scheduling.Schedule", "Active");
            CreateIndex("Scheduling.Schedule", "UpdatedDate");
            CreateIndex("Scheduling.Schedule", "CreatedDate");
            CreateIndex("Scheduling.Schedule", "CustomKey");
            CreateIndex("Scheduling.Schedule", "ID");
            CreateIndex("Stores.Store", "ScheduleID");
            AddForeignKey("Stores.Store", "ScheduleID", "Scheduling.Schedule", "ID");
            AddForeignKey("Scheduling.ScheduledEvent", "ScheduleID", "Scheduling.Schedule", "ID");
            AddForeignKey("Scheduling.ScheduledEvent", "RescheduledEventID", "Scheduling.ScheduledEvent", "ID");
            AddForeignKey("Scheduling.ScheduledEvent", "GroupID", "Groups.Group", "ID");
        }
    }
}
