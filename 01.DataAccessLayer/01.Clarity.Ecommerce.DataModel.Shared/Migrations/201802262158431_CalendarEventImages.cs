// <copyright file="201802262158431_CalendarEventImages.cs" company="clarity-ventures.com">
// Copyright (c) 2019-2023 clarity-ventures.com. All rights reserved.
// </copyright>
// <summary>Implements the 201802262158431 calendar event images class</summary>
namespace Clarity.Ecommerce.DataModel.Migrations
{
    using System.Data.Entity.Migrations;

    public partial class CalendarEventImages : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "CalendarEvents.CalendarEventImage",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        SortOrder = c.Int(),
                        DisplayName = c.String(),
                        SeoTitle = c.String(),
                        Author = c.String(),
                        MediaDate = c.DateTime(),
                        Copyright = c.String(),
                        Location = c.String(),
                        Latitude = c.Decimal(precision: 18, scale: 8),
                        Longitude = c.Decimal(precision: 18, scale: 8),
                        IsPrimary = c.Boolean(nullable: false),
                        OriginalWidth = c.Int(),
                        OriginalHeight = c.Int(),
                        OriginalFileFormat = c.String(),
                        OriginalFileName = c.String(),
                        OriginalIsStoredInDB = c.Boolean(nullable: false),
                        OriginalBytes = c.Binary(),
                        ThumbnailWidth = c.Int(),
                        ThumbnailHeight = c.Int(),
                        ThumbnailFileFormat = c.String(),
                        ThumbnailFileName = c.String(),
                        ThumbnailIsStoredInDB = c.Boolean(nullable: false),
                        ThumbnailBytes = c.Binary(),
                        MasterID = c.Int(),
                        TypeID = c.Int(nullable: false),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("CalendarEvents.CalendarEvent", t => t.MasterID)
                .ForeignKey("CalendarEvents.CalendarEventImageType", t => t.TypeID, cascadeDelete: true)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.MasterID)
                .Index(t => t.TypeID);

            CreateTable(
                "CalendarEvents.CalendarEventImageType",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CustomKey = c.String(maxLength: 128, unicode: false),
                        CreatedDate = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        UpdatedDate = c.DateTime(precision: 7, storeType: "datetime2"),
                        Active = c.Boolean(nullable: false),
                        Hash = c.Long(),
                        Name = c.String(maxLength: 256, unicode: false),
                        Description = c.String(unicode: false),
                        DisplayName = c.String(maxLength: 256, unicode: false),
                        SortOrder = c.Int(),
                        JsonAttributes = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.CustomKey)
                .Index(t => t.UpdatedDate)
                .Index(t => t.Active)
                .Index(t => t.Hash)
                .Index(t => t.Name)
                .Index(t => t.DisplayName)
                .Index(t => t.SortOrder);
        }

        public override void Down()
        {
            DropForeignKey("CalendarEvents.CalendarEventImage", "TypeID", "CalendarEvents.CalendarEventImageType");
            DropForeignKey("CalendarEvents.CalendarEventImage", "MasterID", "CalendarEvents.CalendarEvent");
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "SortOrder" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "DisplayName" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventImageType", new[] { "ID" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "TypeID" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "MasterID" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "Name" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "Hash" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "Active" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "UpdatedDate" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "CustomKey" });
            DropIndex("CalendarEvents.CalendarEventImage", new[] { "ID" });
            DropTable("CalendarEvents.CalendarEventImageType");
            DropTable("CalendarEvents.CalendarEventImage");
        }
    }
}
